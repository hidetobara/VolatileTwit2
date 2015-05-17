using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

using NMeCab;
using Accord.Neuro;
using Accord.Neuro.Learning;
using AForge.Neuro.Learning;
using Accord.Neuro.Networks;


namespace VolatileTweetLibrary
{
	public class EstimateManager
	{
		public const int INPUT_COUNT = 512 * 512;
		public const int MIDDLE_COUNT = 64;
		public const int OUTPUT_COUNT = 8;

		private MorphemeManager _Morpheme;
		private Dictionary<string, int> _WordTable = new Dictionary<string, int>();
		private DeepBeliefNetwork _Network;

		private const string WORD_TABLE_FILENAME = "words.csv";
		private const string LEARNING_FILENAME = "learning.bin";

		private string _NetworkDirectory;

		public EstimateManager(string dir)
		{
			_NetworkDirectory = dir;
		}

		public IEnumerator Prepare(string inputDir)
		{
			if (!InitializeTagger()) yield break;

			LoadWordTable();
			List<Strip> strips = GetStripsWithMorphemes(inputDir);
			int index = 0;
			Log.Instance.Info("Start: strips=" + strips.Count);
			foreach (var strip in strips)
			{
				foreach (var item in Strip2Items(strip))
				{
					if (item.Origin == null || !item.IsUsefull()) continue;
					if (_WordTable.ContainsKey(item.Origin)) _WordTable[item.Origin]++;
					else _WordTable[item.Origin] = 1;
				}
				index++;
				if (index % 1000 == 0)
				{
					Log.Instance.Info("[" + index + "]");
					yield return 0;
				}
			}
			SaveWordTable();
			Log.Instance.Info("Done: Prepare");
		}

		public IEnumerator Learn(string inputDir)
		{
			List<Strip> strips = GetStripsWithMorphemes(inputDir);
			strips = strips.OrderBy(x => Guid.NewGuid()).ToList();	// ランダムに
			Log.Instance.Info("strips=" + strips.Count);

			return Learn(strips);
		}
		public IEnumerator Learn(List<Strip> strips)
		{
			// 初期化
			if (!InitializeTagger()) yield break;
			LoadWordTable();
			double countMax = (double)_WordTable.Max((pair) => pair.Value);

			string filepath = Path.Combine(_NetworkDirectory, LEARNING_FILENAME);
			if (File.Exists(filepath))
			{
				_Network = DeepBeliefNetwork.Load(filepath);
			}
			else
			{
				_Network = new DeepBeliefNetwork(INPUT_COUNT, new int[] { MIDDLE_COUNT, OUTPUT_COUNT });
				// ネットワークの重みをガウス分布で初期化する
				new GaussianWeights(_Network).Randomize();
				_Network.UpdateVisibleWeights();
			}
			// 学習
			BackPropagationLearning teacher = new BackPropagationLearning(_Network);

			int index = 0;
			double[] input = new double[INPUT_COUNT];
			double[] output = new double[OUTPUT_COUNT];
			foreach (var strip in strips)
			{
				// 初期化
				Array.Clear(input, 0, input.Length);
				Array.Clear(output, 0, output.Length);

				List<MorphemeManager.Item> items = Strip2Items(strip);
				foreach (var item in items)
				{
					if (item.Origin == null || !item.IsUsefull()) continue;

					int used = _WordTable.ContainsKey(item.Origin) ? _WordTable[item.Origin] : 1;
					double usefull = countMax / (used * used);
					int hash = String2Hash(item.Origin);
					input[hash] += usefull / items.Count;
				}
				int number = Screen2Number(strip.ScreenName);
				if (number > 0) output[number] = 1;
				teacher.RunEpoch(new double[][] { input }, new double[][] { output });
				_Network.UpdateVisibleWeights();
				index++;
				if (index % 1000 == 0)
				{
					Log.Instance.Info("[" + index + "]: " + strip.Text);
					_Network.Save(filepath);
					yield return 0;
				}
			}
			// 保存
			_Network.Save(filepath);
			Log.Instance.Info("Done: Learn");
		}

		public StripEstimated Compute(string screenName, string text)
		{
			if (!InitializeTagger()) return null;
			LoadWordTable();
			double countMax = (double)_WordTable.Max((pair) => pair.Value);

			if (_Network == null)
			{
				string filepath = Path.Combine(_NetworkDirectory, LEARNING_FILENAME);
				if (!File.Exists(filepath)) return null;
				_Network = DeepBeliefNetwork.Load(filepath);
			}

			double[] input = new double[INPUT_COUNT];
			Strip s = new Strip() { Text = text };
			List<MorphemeManager.Item> items = Strip2Items(s);
			foreach (var item in items)
			{
				if (item.IsTerminal()) continue;

				int used = _WordTable.ContainsKey(item.Origin) ? _WordTable[item.Origin] : 1;
				double usefull = countMax / (used * used);
				int hash = String2Hash(item.Origin);
				input[hash] += usefull / items.Count;
			}
			double[] output = _Network.Compute(input);
			string result = "";
			foreach (var o in output) result += "\t" + o;
			Log.Instance.Info(result);

			for (int i = 0; i < output.Length; i++)
			{
				string name = Number2Screen(i);
				if (name == null || name != screenName) continue;
				return new StripEstimated() { ScreenName = screenName, Text = text, Value = output[i] };
			}
			return null;
		}

		private bool InitializeTagger()
		{
			if (_Morpheme != null) return true;

			_Morpheme = MorphemeManager.Instance;
			return true;
		}

		private List<Strip> GetStripsWithMorphemes(string dir)
		{
			StripManager mgr = new StripManager();

			List<Strip> strips = new List<Strip>();
			foreach (var path in Directory.GetFiles(dir, "*.log"))
			{
				foreach (var strip in mgr.LoadStrips(path))
				{
					strips.Add(strip);
				}
			}
			return strips;
		}

		// 単語テーブルの読み書き
		private void SaveWordTable()
		{
			var table = _WordTable.OrderByDescending((pair) => { return pair.Value; });

			string filepath = Path.Combine(_NetworkDirectory, WORD_TABLE_FILENAME);
			using (StreamWriter writer = new StreamWriter(filepath))
			{
				foreach (var pair in table)
				{
					if (pair.Value < 2) break;
					writer.WriteLine(pair.Key + "\t" + pair.Value + "\t" + String2Hash(pair.Key));
				}
				writer.Close();
			}
		}
		private void LoadWordTable()
		{
			string filepath = Path.Combine(_NetworkDirectory, WORD_TABLE_FILENAME);
			if (!File.Exists(filepath)) return;
			_WordTable.Clear();
			using (StreamReader reader = new StreamReader(filepath))
			{
				if (reader == null) return;
				while (!reader.EndOfStream)
				{
					string line = reader.ReadLine();
					string[] cells = line.Split('\t');
					int count = 0;
					if (cells.Length >= 2 && int.TryParse(cells[1], out count)) _WordTable[cells[0]] = count;
				}
			}
		}

		private Dictionary<string, int> _ScreenTable = new Dictionary<string, int>() { { "hidetobara", 1 }, { "shokos", 2 }, { "yamasiro", 3 }, { "kusigahama", 4 } };
		private int Screen2Number(string s)
		{
			if (!_ScreenTable.ContainsKey(s)) return -1;
			return _ScreenTable[s];
		}
		private string Number2Screen(int i)
		{
			foreach (var p in _ScreenTable) if (p.Value == i) return p.Key;
			return null;
		}

		private List<MorphemeManager.Item> Strip2Items(Strip s)
		{
			return _Morpheme.Parse(s.Text);
		}

		private System.Security.Cryptography.SHA256CryptoServiceProvider _Sha256;
		// MAX: 512*512
		private int String2Hash(string s)
		{
			byte[] data = System.Text.Encoding.UTF8.GetBytes(s);
			//MD5CryptoServiceProviderオブジェクトを作成
			if (_Sha256 == null) _Sha256 = new System.Security.Cryptography.SHA256CryptoServiceProvider();
			//ハッシュ値を計算する
			byte[] bs = _Sha256.ComputeHash(data);
			return 256 * 256 * (bs[0] / 64) + 256 * bs[1] + bs[2];	// これで十分か判断は難しい
		}
	}
}

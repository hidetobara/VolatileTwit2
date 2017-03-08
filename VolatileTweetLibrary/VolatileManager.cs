using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace VolatileTweetLibrary
{
	public class VolatileManager
	{
		const int START = -1;
		const int END = -2;

		private List<string> _Ignores = new List<string>() { "、", "。", "「", "」", "(", ")", "[", "]", "{", "}" };

		private string _NetworkDir;
		private string _TargetName;

		private Dictionary<string, int> _TextTable = new Dictionary<string ,int>();
		private Dictionary<int, Flow> _FlowMatrix = new Dictionary<int, Flow>();

		public VolatileManager(string networkDir, string name)
		{
			_NetworkDir = networkDir;
			_TargetName = name;
		}

		public IEnumerator LearningMatrix(string inputDir)
		{
			StripManager mgr = new StripManager();
			foreach(var path in Directory.GetFiles(inputDir, "*.log"))
			{
				foreach(var strip in mgr.LoadStrips(path))
				{
					if(strip.ScreenName != _TargetName) continue;
					Learn(strip.Text);
				}
				yield return null;
			}
		}

		public void Load()
		{
			LoadTexts(TextsPath);
			LoadMatrix(MatrixPath);
		}
		public void Save()
		{
			SaveTexts(TextsPath);
			SaveMatrix(MatrixPath);
		}

		private string TextsPath { get { return Path.Combine(_NetworkDir, _TargetName + ".txt"); } }
		private string MatrixPath { get { return Path.Combine(_NetworkDir, _TargetName + ".mtx"); } }

		private bool LoadTexts(string path)
		{
			if (!File.Exists(path)) return false;

			using(StreamReader reader = new StreamReader(path))
			{
				_TextTable.Clear();
				while(!reader.EndOfStream)
				{
					string line = reader.ReadLine();
					string[] cells = line.Split('\t');
					if (cells.Length < 2) continue;
					int count;
					if (string.IsNullOrEmpty(cells[1]) || !int.TryParse(cells[1], out count)) continue;
					_TextTable[cells[0]] = count;
				}
			}
			return true;
		}
		private bool LoadMatrix(string path)
		{
			if (!File.Exists(path)) return false;

			using (StreamReader reader = new StreamReader(path))
			{
				_FlowMatrix.Clear();
				while (!reader.EndOfStream)
				{
					string line = reader.ReadLine();
					string[] cells = line.Split('\t');
					if (cells.Length < 2) continue;
					int num;
					if (!int.TryParse(cells[0], out num)) continue;
					Flow flow = new Flow();
					flow.Retrieve(cells, 1);
					_FlowMatrix[num] = flow;
				}
			}
			return true;
		}
		private bool SaveTexts(string path)
		{
			using(StreamWriter writer = new StreamWriter(path))
			{
				foreach (var pair in _TextTable) writer.WriteLine(pair.Key + "\t" + pair.Value);
				if (writer != null) writer.Close();
			}
			return true;
		}
		private bool SaveMatrix(string path)
		{
			using(StreamWriter writer = new StreamWriter(path))
			{
				foreach(var pair in _FlowMatrix)
				{
					writer.WriteLine(pair.Key + pair.Value.ToString());
				}
				if (writer != null) writer.Close();
			}
			return true;
		}

		public bool Learn(string input)
		{
			List<string> blocks = new List<string>();
			MorphemeManager.MorphemeType previousType = MorphemeManager.MorphemeType.OTHERS;
			string block = "";
			foreach (var item in MorphemeManager.Instance.Parse(input))
			{
				if (item.Origin == null || _Ignores.Contains(item.Origin)) continue;

				switch (item.Type)
				{
					case MorphemeManager.MorphemeType.PARTICLE:
					case MorphemeManager.MorphemeType.ADJECTIVE:
					case MorphemeManager.MorphemeType.ADVERB:
						block += item.Variation;
						blocks.Add(block);
						block = "";
						break;
					case MorphemeManager.MorphemeType.NOUN:
						if (previousType == MorphemeManager.MorphemeType.NOUN) { blocks.Add(block); block = ""; }
						block += item.Variation;
						break;
					default:
						block += item.Variation;
						break;

				}
				previousType = item.Type;
			}

			int previous = START;
			foreach(var b in blocks)
			{
				if (!_TextTable.ContainsKey(b)) _TextTable[b] = _TextTable.Count;
				int number = _TextTable[b];
				if (!_FlowMatrix.ContainsKey(previous)) _FlowMatrix[previous] = new Flow();
				Flow f = _FlowMatrix[previous];
				if (!f.Texts.ContainsKey(number)) f.Texts[number] = 1; else f.Texts[number]++;
				previous = number;
			}
			if (!_FlowMatrix.ContainsKey(previous)) _FlowMatrix[previous] = new Flow();
			Flow fend = _FlowMatrix[previous];
			if (!fend.Texts.ContainsKey(END)) fend.Texts[END] = 1; else fend.Texts[END]++;
			return true;
		}

		public string Tweet()
		{
			Dictionary<int, string> reverse = new Dictionary<int, string>();
			foreach (var pair in _TextTable) reverse[pair.Value] = pair.Key;

			int index = START;
			int next = START;
			const int TextLimit = 128;  // 文字数制限
			const int UseLimit = 3;	// 単語使用制限
			Dictionary<int, int> used = new Dictionary<int, int>();
			string result = "";
			while(index != END)
			{
				if (reverse.ContainsKey(index)) result += reverse[index];
				if (!used.ContainsKey(index)) used[index] = 1; else used[index]++;
				if (used[index] > UseLimit) break;
				if (result.Length > TextLimit) break;
				next = _FlowMatrix[index].Invoke();				
				index = next;
			}
			Log.Instance.Info("Tweet(): " + result);
			return result;
		}

		class Flow
		{
			public Dictionary<int, int> Texts = new Dictionary<int, int>();
			public string ToString()
			{
				StringBuilder builder = new StringBuilder();
				foreach (var kv in Texts) builder.Append("\t" + kv.Key + "=" + kv.Value);
				return builder.ToString();
			}
			public void Retrieve(string[] cells, int offset)
			{
				for(int i = offset; i < cells.Length; i++)
				{
					string[] kv = cells[i].Split('=');
					if (kv.Length != 2) continue;
					int key, value;
					if (int.TryParse(kv[0], out key) && int.TryParse(kv[1], out value)) Texts[key] = value;
				}
			}
			public int Invoke()
			{
				int amount = Texts.Sum(x => x.Value);
				Random r = new Random();
				int value = r.Next(0, amount);
				int sum = 0;
				foreach(var pair in Texts)
				{
					sum += pair.Value;
					if (value <= sum) return pair.Key;
				}
				return END;
			}
		}
	}
}

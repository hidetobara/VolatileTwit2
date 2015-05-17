using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using NMeCab;


namespace VolatileTweetLibrary
{
	public class MorphemeManager
	{
		MeCabTagger _Tagger;

		private static MorphemeManager _Instance;
		public static MorphemeManager Instance
		{
			get
			{
				if (_Instance == null) _Instance = new MorphemeManager();
				return _Instance;
			}
		}
		private MorphemeManager() { }

		public MorphemeManager Initialize(string dir)
		{
			if (_Tagger != null) return this;

			MeCabParam p = new MeCabParam();
			p.LatticeLevel = MeCabLatticeLevel.Zero;
			p.OutputFormatType = "lattice";
			p.AllMorphs = false;
			p.Partial = false;
			p.DicDir = dir;
			_Tagger = MeCabTagger.Create(p);
			return this;
		}

		private List<string> _Codes = new List<string>() { ")", "(", "\"" };

		public List<Item> Parse(string input, bool removeAlphabets = true)
		{
			if (removeAlphabets) input = Regex.Replace(input, "[ @_a-zA-Z0-9!:/#\\.\\-]+", "");

			List<Item> list = new List<Item>() { new Item(MorphemeType.START) };
			MeCabNode node = _Tagger.ParseToNode(input);
			node = node.Next;
			while(node != null)
			{
				if (node.Length > 0 && node.Feature != null)
				{
					if(_Codes.Contains(node.Surface))
					{
						list.Add(new Item(MorphemeType.OTHERS, node.Surface));
					}
					else if (node.Feature.StartsWith("名詞"))
					{
						list.Add(new Item(MorphemeType.NOUN, node.Surface));
					}
					else if (node.Feature.StartsWith("動詞"))
					{
						string[] cells = node.Feature.Split(',');
						list.Add(new Item(MorphemeType.VERB, cells.Length > 6 ? cells[6] : node.Surface, node.Surface));
					}
					else if (node.Feature.StartsWith("副詞"))
					{
						string[] cells = node.Feature.Split(',');
						list.Add(new Item(MorphemeType.ADVERB, cells.Length > 6 ? cells[6] : node.Surface, node.Surface));
					}
					else if (node.Feature.StartsWith("形容詞") || node.Feature.StartsWith("形容動詞"))
					{
						string[] cells = node.Feature.Split(',');
						list.Add(new Item(MorphemeType.ADJECTIVE, cells.Length > 6 ? cells[6] : node.Surface, node.Surface));
					}
					else if (node.Feature.StartsWith("助詞"))
					{
						string[] cells = node.Feature.Split(',');
						list.Add(new Item(MorphemeType.PARTICLE, cells.Length > 6 ? cells[6] : node.Surface, node.Surface));
					}
					else
					{
						list.Add(new Item(MorphemeType.OTHERS, node.Surface));
					}
				}
				node = node.Next;	// なぜか例外が起きる
			}
			list.Add(new Item(MorphemeType.END));
			return list;
		}

		public enum MorphemeType { NOUN /*名詞*/, VERB /*動詞*/, ADJECTIVE /*形容詞*/, PARTICLE /*助詞*/, ADVERB /*副詞*/, OTHERS, START, END }
		public class Item
		{
			public MorphemeType Type;
			public string Origin;
			public string Variation;
			public Item(MorphemeType t) { Type = t; }
			public Item(MorphemeType t, string origin) { Type = t; Origin = origin; Variation = origin; }
			public Item(MorphemeType t, string origin, string variation) { Type = t; Origin = origin; Variation = variation; }

			public bool IsTerminal() { return Type == MorphemeType.START || Type == MorphemeType.END; }
			public bool IsUsefull()
			{
				switch(Type)
				{
					case MorphemeType.NOUN:
					case MorphemeType.VERB:
					case MorphemeType.ADJECTIVE:
					case MorphemeType.ADVERB:
						return true;
					default:
						return false;
				}
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	static class TraitPool
	{
		public static Dictionary<TraitMap, int> Pool { get; set; }

		public static void Constructor()
		{
			Pool = new Dictionary<TraitMap, int>();
			GenerateTraits();
		}

		public static int GetTraitValue(SearchableAsset keyValues, SearchableAsset matchValues)
		{
			int fullValue = 0;
			//loop through every trait against every other trait and return a full sum
			fullValue += GetTraitValue(keyValues.sin, matchValues.sin);
			fullValue += GetTraitValue(keyValues.sin, matchValues.virtue);
			fullValue += GetTraitValue(keyValues.virtue, matchValues.sin);
			fullValue += GetTraitValue(keyValues.virtue, matchValues.virtue);

			return fullValue;
		}

		public static int GetTraitValue(object keyValue, object matchValue)
		{
			TraitMap getKey = new TraitMap(keyValue, matchValue);

			//check the dictionary against values and return the match
			return Pool[getKey];
		}

		public static void GenerateTraits()
		{
			List<object> values = Enum.GetValues(typeof(Sin)).Cast<object>().ToList();
			values.Add(Enum.GetValues(typeof(Sin)).Cast<object>().ToList());

			foreach (object firstKey in values)
			{
				foreach (object secondKey in values)
				{
					TraitMap entry = new TraitMap(firstKey, secondKey);
					if (!Pool.ContainsKey(entry))
					{
						Pool.Add(entry, 0);
					}
				}
			}

			Pool[new TraitMap(Sin.envy, Sin.gluttony)] = 0;
		}
	}

	struct TraitMap
	{
		object firstKey;
		object secondKey;

		public TraitMap(object key1, object key2)
		{
			firstKey = key1;
			secondKey = key2;
		}

		public override int GetHashCode()
		{
			return firstKey.GetHashCode() + secondKey.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Equals(firstKey, secondKey);
		}
	}
}

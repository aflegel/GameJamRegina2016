using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	static class TraitPool
	{
		public static Dictionary<TraitMap, int> traitPool;

		public static void Constructor()
		{
			traitPool = new Dictionary<TraitMap, int>();
			GeneratePeople();
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
			return traitPool[getKey];
		}

		public static void GeneratePeople()
		{

			List<object> values = Enum.GetValues(typeof(Sin)).Cast<object>().ToList();
			values.Add(Enum.GetValues(typeof(Sin)).Cast<object>().ToList());

			foreach (object firstKey in values)
			{
				foreach (object secondKey in values)
				{
					TraitMap entry = new TraitMap(firstKey, secondKey);
					if (!traitPool.ContainsKey(entry))
					{
						traitPool.Add(entry, 0);
					}
				}
			}

			traitPool[new TraitMap(Sin.envy, Sin.gluttony)] = 0;


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

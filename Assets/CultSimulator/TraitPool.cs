using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	class TraitPool
	{
		public Dictionary<TraitMap, int> traitPool;

		public TraitPool()
		{
			traitPool = new Dictionary<TraitMap, int>();
			AddTraits();
		}



		public int GetTraitValue(SearchableAsset keyValues, SearchableAsset matchValues)
		{
			int fullValue = 0;
			//loop through every trait against every other trait and return a full sum
			fullValue += GetTraitValue(keyValues.sin, matchValues.sin);
			fullValue += GetTraitValue(keyValues.sin, matchValues.virtue);
			fullValue += GetTraitValue(keyValues.virtue, matchValues.sin);
			fullValue += GetTraitValue(keyValues.virtue, matchValues.virtue);

			return fullValue;
		}

		public int GetTraitValue(object keyValue, object matchValue)
		{
			TraitMap getKey = new TraitMap(keyValue, matchValue);

			//check the dictionary against values and return the match
			return traitPool[getKey];
		}

		protected void AddTraits()
		{
			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);

			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);

			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);

			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);

			traitPool.Add(new TraitMap(0, 0), 0);
			traitPool.Add(new TraitMap(0, 0), 0);

			traitPool.Add(new TraitMap(0, 0), 0);


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

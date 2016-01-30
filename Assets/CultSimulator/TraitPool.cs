using Assets.Standard_Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	class TraitPool
	{
		public Dictionary<Sin, Dictionary<Sin, int>> traitPool;

		public TraitPool()
		{
			traitPool = new Dictionary<Sin, Dictionary<Sin, int>>();

		}

		public int GetTraitValue(SearchableAsset keyValues, SearchableAsset matchValues)
		{
			int fullValue = 0;
			//loop through every trait against every other trait and return a full sum
			//			fullValue += GetTraitValue(trait, match);

			return fullValue;
		}

		public int GetTraitValue(Sin keyValue, Sin matchValue)
		{
			//check the dictionary against values and return the match
			if (traitPool.ContainsKey(keyValue))
				return traitPool[keyValue].ContainsKey(matchValue) ? traitPool[keyValue][matchValue] : 0;
			else
				return traitPool[matchValue].ContainsKey(keyValue) ? traitPool[matchValue][keyValue] : 0;
		}
	}

	struct TraitMap
	{

	}
}

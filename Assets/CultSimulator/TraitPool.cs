﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	class TraitPool
	{
		public Dictionary<TraitMap, int> Pool { get; set; }

		public TraitPool()
		{
			Pool = new Dictionary<TraitMap, int>();
			GenerateTraits();
		}

		public int GetTraitValue(SearchableAsset keyValues, SearchableAsset matchValues)
		{
			int fullValue = 0;
			int temp = 0;

			//loop through every trait against every other trait and return a full sum
			fullValue += GetTraitValue(keyValues.Sin, matchValues.Sin);
			temp = GetTraitValue(keyValues.Sin, matchValues.Sin);
			fullValue += GetTraitValue(keyValues.Sin, matchValues.Virtue);
			temp = GetTraitValue(keyValues.Sin, matchValues.Virtue);
			fullValue += GetTraitValue(keyValues.Virtue, matchValues.Sin);
			temp = GetTraitValue(keyValues.Virtue, matchValues.Sin);
			fullValue += GetTraitValue(keyValues.Virtue, matchValues.Virtue);
			temp = GetTraitValue(keyValues.Virtue, matchValues.Virtue);

			return fullValue;
		}

		public int GetTraitValue(object keyValue, object matchValue)
		{
			TraitMap getKey = new TraitMap(keyValue, matchValue);

			//check the dictionary against values and return the match

			if (Pool.ContainsKey(getKey))
				return Pool[getKey];
			return 0;
		}

		public void GenerateTraits()
		{
			List<object> firstKeyValues = Enum.GetValues(typeof(Sin)).Cast<object>().ToList();
			List<object> secondKeyValues = Enum.GetValues(typeof(Sin)).Cast<object>().ToList();

			firstKeyValues.AddRange(Enum.GetValues(typeof(Virtue)).Cast<object>().ToList());
			secondKeyValues.AddRange(Enum.GetValues(typeof(Virtue)).Cast<object>().ToList());

			foreach (object firstKey in firstKeyValues)
			{
				foreach (object secondKey in secondKeyValues)
				{
					TraitMap entry = new TraitMap(firstKey, secondKey);
					if (!Pool.ContainsKey(entry))
					{
						Pool.Add(entry, (firstKey == secondKey ? 10 : 0));
					}
				}
			}

			Pool[new TraitMap(Virtue.Forgiving, Sin.Wrathful)] = -10;
			Pool[new TraitMap(Virtue.Humble, Sin.Proud)] = -10;
			Pool[new TraitMap(Virtue.Charitable, Sin.Greedy)] = -10;
			Pool[new TraitMap(Virtue.Temperant, Sin.Gluttonous)] = -10;
			Pool[new TraitMap(Virtue.Diligent, Sin.Lazy)] = -10;
			Pool[new TraitMap(Virtue.Chaste, Sin.Lusty)] = -10;
			Pool[new TraitMap(Virtue.Kind, Sin.Envious)] = -10;


			Pool[new TraitMap(Sin.Proud, Sin.Wrathful)] = 5;
			Pool[new TraitMap(Sin.Greedy, Sin.Proud)] = 5;
			Pool[new TraitMap(Sin.Gluttonous, Sin.Greedy)] = 5;
			Pool[new TraitMap(Sin.Lazy, Sin.Gluttonous)] = 5;
			Pool[new TraitMap(Sin.Lusty, Sin.Lazy)] = 5;
			Pool[new TraitMap(Sin.Envious, Sin.Lusty)] = 5;
			Pool[new TraitMap(Sin.Wrathful, Sin.Envious)] = 5;

			Pool[new TraitMap(Virtue.Forgiving, Virtue.Kind)] = 5;
			Pool[new TraitMap(Virtue.Humble, Virtue.Forgiving)] = 5;
			Pool[new TraitMap(Virtue.Charitable, Virtue.Humble)] = 5;
			Pool[new TraitMap(Virtue.Temperant, Virtue.Charitable)] = 5;
			Pool[new TraitMap(Virtue.Diligent, Virtue.Temperant)] = 5;
			Pool[new TraitMap(Virtue.Chaste, Virtue.Diligent)] = 5;
			Pool[new TraitMap(Virtue.Kind, Virtue.Chaste)] = 5;


			Pool[new TraitMap(Sin.Wrathful, Virtue.Humble)] = -5 ;
			Pool[new TraitMap(Sin.Proud, Virtue.Charitable)] = -5 ;
			Pool[new TraitMap(Sin.Greedy, Virtue.Temperant)] = -5 ;
			Pool[new TraitMap(Sin.Gluttonous, Virtue.Diligent)] = -5 ;
			Pool[new TraitMap(Sin.Lazy, Virtue.Chaste)] = -5 ;
			Pool[new TraitMap(Sin.Lusty, Virtue.Kind)] = -5 ;
			Pool[new TraitMap(Sin.Envious, Virtue.Forgiving)] = -5 ;

			Pool[new TraitMap(Sin.Wrathful, Virtue.Kind)] = -5;
			Pool[new TraitMap(Sin.Proud, Virtue.Forgiving)] = -5;
			Pool[new TraitMap(Sin.Greedy, Virtue.Humble)] = -5;
			Pool[new TraitMap(Sin.Gluttonous, Virtue.Charitable)] = -5;
			Pool[new TraitMap(Sin.Lazy, Virtue.Temperant)] = -5;
			Pool[new TraitMap(Sin.Lusty, Virtue.Diligent)] = -5;
			Pool[new TraitMap(Sin.Envious, Virtue.Chaste)] = -5;


		}

		public static SearchableAsset GenerateRandomAsset(bool profession, bool sin, bool virtue, bool animal, Random randomNumber)
		{
			//arrays to generate random values
			Array sins = Enum.GetValues(typeof(Sin));
			Array virtues = Enum.GetValues(typeof(Virtue));
			Array professions = Enum.GetValues(typeof(Profession));
			SearchableAsset simple = new SearchableAsset();


			if (sin)
				simple.Sin = (Sin)sins.GetValue(randomNumber.Next(1, sins.Length));
			else
				simple.Sin = Sin.None;

			if (virtue)
				simple.Virtue = (Virtue)virtues.GetValue(randomNumber.Next(1, virtues.Length));
			else
				simple.Virtue = Virtue.None;

			if (profession)
				simple.Profession = (Profession)professions.GetValue(randomNumber.Next(1, professions.Length));
			else
				simple.Profession = Profession.None;

			return simple;
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

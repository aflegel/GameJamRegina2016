using System;
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
			//loop through every trait against every other trait and return a full sum
			fullValue += GetTraitValue(keyValues.Sin, matchValues.Sin);
			fullValue += GetTraitValue(keyValues.Sin, matchValues.Virtue);
			fullValue += GetTraitValue(keyValues.Virtue, matchValues.Sin);
			fullValue += GetTraitValue(keyValues.Virtue, matchValues.Virtue);

			return fullValue;
		}

		public int GetTraitValue(object keyValue, object matchValue)
		{
			TraitMap getKey = new TraitMap(keyValue, matchValue);

			//check the dictionary against values and return the match
			return Pool[getKey];
		}

		public void GenerateTraits()
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

			Pool[new TraitMap(Sin.Envious, Sin.Gluttonous)] = 0;
		}

		public static SearchableAsset GenerateRandomAsset(bool profession, bool sin, bool virtue, bool animal, Random randomNumber)
		{
			//arrays to generate random values
			Array sins = Enum.GetValues(typeof(Sin));
			Array virtues = Enum.GetValues(typeof(Virtue));
			Array professions = Enum.GetValues(typeof(Profession));
			SearchableAsset simple = new SearchableAsset();


			if (sin)
			{
				simple.Sin = (Sin)sins.GetValue(randomNumber.Next(1, sins.Length));
			}
			else
			{
				simple.Sin = Sin.None;
			}

			if (virtue)
			{
				simple.Virtue = (Virtue)virtues.GetValue(randomNumber.Next(1, virtues.Length));
			}
			else
			{
				simple.Virtue = Virtue.None;
			}

			if (profession)
			{
				simple.Profession = (Profession)professions.GetValue(randomNumber.Next(1, professions.Length));
			}
			else
			{
				simple.Profession = Profession.None;
			}

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

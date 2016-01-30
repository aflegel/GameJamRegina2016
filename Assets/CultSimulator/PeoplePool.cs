using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets.Models
{
	class PeoplePool
	{
		public Dictionary<int, FullPerson> activePool;
		public Dictionary<Sin, Dictionary<Sin, int>> traitPool;

		public PeoplePool()
		{
			activePool = new Dictionary<int, FullPerson>();
			traitPool = new Dictionary<Sin, Dictionary<Sin, int>>();
		}

		public void Startup()
		{
			GeneratePeople();
			GenerateTraits();
		}

		public void GeneratePeople()
		{
			Array sins = Enum.GetValues(typeof(Sin));
			Array virtues = Enum.GetValues(typeof(Virtue));
			Array professions = Enum.GetValues(typeof(Profession));

			PersonNames names = new PersonNames();

			Random ranomNumber = new Random();

			for (int i = 0; i < 20; i++)
			{
				FullPerson freshPerson = new FullPerson();

				string name = names.GetNextName(null);
				if (name == "")
					break;

				freshPerson.person = new Person();
				freshPerson.person.name = name;

				freshPerson.assets = new SearchableAsset();
				freshPerson.assets.sin = (Sin)sins.GetValue(ranomNumber.Next(sins.Length));
				freshPerson.assets.virtue = (Virtue)virtues.GetValue(ranomNumber.Next(virtues.Length));
				freshPerson.assets.profession = (Profession)professions.GetValue(ranomNumber.Next(professions.Length));

				activePool.Add(activePool.Count + 1, freshPerson);
			}

		}

		public void GenerateTraits()
		{

		}



		public Dictionary<int, FullPerson> SearchPeople(SearchableAsset assets)
		{
			return new Dictionary<int, FullPerson>();
		}

		public Dictionary<int, FullPerson> SearchPeopleByID(int id)
		{
			return new Dictionary<int, FullPerson>();
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
}

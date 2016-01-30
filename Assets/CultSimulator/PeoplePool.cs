using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets.Models
{
	class PeoplePool
	{
		public Dictionary<int, FullPerson> activePool;
		public Dictionary<Trait, Dictionary<Trait, int>> traitPool;

		public PeoplePool()
		{
			activePool = new Dictionary<int, FullPerson>();
			traitPool = new Dictionary<Trait, Dictionary<Trait, int>>();
		}

		public void Startup()
		{
			GeneratePeople();
			GenerateTraits();
		}

		public void GeneratePeople()
		{

			PersonNames names = new PersonNames();

			for( int i=0; i<20; i++)
			{
				FullPerson freshPerson = new FullPerson();

				string name = names.GetNextName(null);
				if (name == "")
					break;

				freshPerson.person = new Person();
				freshPerson.person.Name = name;

				freshPerson.assets = new SearchableAsset();
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
			foreach(Trait trait in keyValues.traits)
			{
				foreach (Trait match in matchValues.traits)
				{
					fullValue += GetTraitValue(trait, match);
				}
			}

			return fullValue;
		}

		public int GetTraitValue(Trait keyValue, Trait matchValue)
		{
			//check the dictionary against values and return the match
			if (traitPool.ContainsKey(keyValue))
				return traitPool[keyValue].ContainsKey(matchValue) ? traitPool[keyValue][matchValue] : 0;
			else
				return traitPool[matchValue].ContainsKey(keyValue) ? traitPool[matchValue][keyValue] : 0;
		}
	}
}

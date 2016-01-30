using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	class PeoplePool
	{
		public Dictionary<int, Person> activePool;

		public PeoplePool()
		{
			activePool = new Dictionary<int, Person>();
		}

		public void Startup()
		{
			GeneratePeople();

		}

		public void GeneratePeople()
		{
			Array sins = Enum.GetValues(typeof(Sin));
			Array virtues = Enum.GetValues(typeof(Virtue));
			Array professions = Enum.GetValues(typeof(Profession));

			NamePool names = new NamePool();

			Random randomNumber = new Random();

			for (int i = 0; i < 20; i++)
			{
				Person freshPerson = new Person();

				string name = names.GetNextName(null);
				if (name == "")
					break;

				freshPerson = new Person();
				freshPerson.Name = name;

				freshPerson.PersonID = activePool.Count + 1;
				freshPerson.Active = true;
				freshPerson.assets = new SearchableAsset();
				freshPerson.assets.sin = (Sin)sins.GetValue(randomNumber.Next(sins.Length));
				freshPerson.assets.virtue = (Virtue)virtues.GetValue(randomNumber.Next(0,virtues.Length));
				freshPerson.assets.profession = (Profession)professions.GetValue(randomNumber.Next(0,professions.Length));
				freshPerson.Gender = randomNumber.Next(0, 1) == 0;

				activePool.Add(freshPerson.PersonID, freshPerson);
			}

		}

		public void GenerateTraits()
		{

		}

		public Dictionary<int, Person> SearchPeople(SearchableAsset assets)
		{
			return new Dictionary<int, Person>();
		}

		public Person SearchPeopleByID(int id)
		{
			return activePool[id];
		}


	}
}

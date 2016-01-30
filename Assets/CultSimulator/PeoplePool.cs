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
			GenerateTraits();
		}

		public void GeneratePeople()
		{
			Array sins = Enum.GetValues(typeof(Sin));
			Array virtues = Enum.GetValues(typeof(Virtue));
			Array professions = Enum.GetValues(typeof(Profession));

			NamePool names = new NamePool();

			Random ranomNumber = new Random();

			for (int i = 0; i < 20; i++)
			{
				Person freshPerson = new Person();

				string name = names.GetNextName(null);
				if (name == "")
					break;

				freshPerson = new Person();
				freshPerson.Name = name;

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

		public Dictionary<int, Person> SearchPeople(SearchableAsset assets)
		{
			return new Dictionary<int, Person>();
		}

		public Dictionary<int, Person> SearchPeopleByID(int id)
		{
			return new Dictionary<int, Person>();
		}


	}
}

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

		public void GeneratePeople(int numberOfNewRecords, List<SearchableAsset> requiredAssets)
		{


			NamePool names = new NamePool();


			for (int i = 0; i < (numberOfNewRecords - requiredAssets.Count); i++)
			{

				string name = names.GetNextName(null);
				if (name == "")
					break;

				Person freshPerson = GeneratePerson(activePool.Count + 1, name, null);

				activePool.Add(freshPerson.PersonID, freshPerson);
			}

			foreach (SearchableAsset asset in requiredAssets)
			{
				string name = names.GetNextName(null);
				if (name == "")
					break;

				Person freshPerson = GeneratePerson(activePool.Count + 1, name, asset);

				activePool.Add(freshPerson.PersonID, freshPerson);
			}

		}

		public Person GeneratePerson(int id, string name, SearchableAsset? requiredAsset)
		{
			//arrays to generate random values
			Array sins = Enum.GetValues(typeof(Sin));
			Array virtues = Enum.GetValues(typeof(Virtue));
			Array professions = Enum.GetValues(typeof(Profession));
			Person freshPerson = new Person();
			Random randomNumber = new Random();


			freshPerson.Active = true;
			freshPerson.assets = new SearchableAsset();
			freshPerson.assets.sin = (Sin)sins.GetValue(randomNumber.Next(sins.Length));
			freshPerson.assets.virtue = (Virtue)virtues.GetValue(randomNumber.Next(0, virtues.Length));
			freshPerson.assets.profession = (Profession)professions.GetValue(randomNumber.Next(0, professions.Length));

			freshPerson.Gender = randomNumber.Next(0, 1) == 0;
			freshPerson.Abduction = randomNumber.NextDouble();
			freshPerson.AbductionDefense = randomNumber.NextDouble();
			freshPerson.Investigation = randomNumber.NextDouble();
			freshPerson.InvestigationDefense = randomNumber.NextDouble();
			freshPerson.Recruitment = randomNumber.NextDouble();
			freshPerson.RecruitmentDefense = randomNumber.NextDouble();

			return freshPerson;
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

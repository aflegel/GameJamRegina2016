using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	class PeoplePool
	{
		public Dictionary<int, Person> activePool;
		public ProfessionPool professionPool;

		public PeoplePool()
		{
			activePool = new Dictionary<int, Person>();
			professionPool = new ProfessionPool();

		}

		public void GeneratePeople(int numberOfNewRecords, List<SearchableAsset> requiredAssets)
		{
			NamePool names = new NamePool();
			Random randomNumber = new Random();

			for (int i = 0; i < (numberOfNewRecords - requiredAssets.Count); i++)
			{

				Person freshPerson = GeneratePerson(activePool.Count + 1, null, randomNumber);

				string name = names.GetNextName(null, freshPerson.Gender, randomNumber);

				if (name == "")
					break;

				freshPerson.Name = name;
				activePool.Add(freshPerson.PersonID, freshPerson);
			}

			foreach (SearchableAsset asset in requiredAssets)
			{

				Person freshPerson = GeneratePerson(activePool.Count + 1, null, randomNumber);

				string name = names.GetNextName(null, freshPerson.Gender, randomNumber);

				if (name == "")
					break;

				freshPerson.Name = name;
				activePool.Add(freshPerson.PersonID, freshPerson);
			}

		}

		public Person GeneratePerson(int id, SearchableAsset? requiredAsset, Random randomNumber)
		{
			//arrays to generate random values
			Array sins = Enum.GetValues(typeof(Sin));
			Array virtues = Enum.GetValues(typeof(Virtue));
			Array professions = Enum.GetValues(typeof(Profession));
			Person freshPerson = new Person();

			freshPerson.PersonID = id;
			freshPerson.Active = true;
			freshPerson.assets = new SearchableAsset();
			freshPerson.assets.sin = (Sin)sins.GetValue( randomNumber.Next(1, sins.Length));
			freshPerson.assets.virtue = (Virtue)virtues.GetValue(randomNumber.Next(1, virtues.Length));
			freshPerson.assets.profession = (Profession)professions.GetValue(randomNumber.Next(1, professions.Length));
			freshPerson.Gender = randomNumber.Next(0, 2) == 0;


			SkillMap professionSkills = professionPool.GetProfessionValue(freshPerson.assets);

			freshPerson.Abduction = randomNumber.Next(0,100) + professionSkills.Abduction;
			freshPerson.AbductionDefense = randomNumber.Next(0, 100) + professionSkills.AbductionDefense;
			freshPerson.Investigation = randomNumber.Next(0, 100) + professionSkills.Investigation;
			freshPerson.InvestigationDefense = randomNumber.Next(0, 100) + professionSkills.InvestigationDefense;
			freshPerson.Recruitment = randomNumber.Next(0, 100) + professionSkills.Recruitment;
			freshPerson.RecruitmentDefense = randomNumber.Next(0, 100) + professionSkills.RecruitmentDefense;


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

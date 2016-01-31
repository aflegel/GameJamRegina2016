using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

namespace Assets.CultSimulator
{
	class PeoplePool
	{
		public Dictionary<int, Person> activePool;
		public ProfessionPool professionPool;
		public NamePool namesPool;

		public PeoplePool()
		{
			activePool = new Dictionary<int, Person>();
			professionPool = new ProfessionPool();
			namesPool = new NamePool();

		}

		public void GeneratePeople(int numberOfNewRecords, List<SearchableAsset> requiredAssets, bool buildAnimals, bool buildSacrifices)
		{
			Random randomNumber = new Random();

			for (int i = 0; i < (numberOfNewRecords - requiredAssets.Count); i++)
			{

				Person freshPerson = GeneratePerson(activePool.Count + 1, null, randomNumber);

				string name = namesPool.GetNextName(null, freshPerson.Gender, buildAnimals, randomNumber);

				if (name == "")
					break;

				freshPerson.Name = name;
				freshPerson.Sacrifice = buildSacrifices;
				activePool.Add(freshPerson.PersonID, freshPerson);
			}

			foreach (SearchableAsset asset in requiredAssets)
			{

				Person freshPerson = GeneratePerson(activePool.Count + 1, null, randomNumber);

				string name = namesPool.GetNextName(null, freshPerson.Gender, buildAnimals, randomNumber);

				if (name == "")
					break;

				freshPerson.Name = name;
				freshPerson.Sacrifice = buildSacrifices;
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
			if(requiredAsset.HasValue)
			{
				freshPerson.assets.Sin = (requiredAsset.Value.Sin == null ? (Sin)sins.GetValue(randomNumber.Next(1, sins.Length)) : requiredAsset.Value.Sin);
				freshPerson.assets.Virtue = (requiredAsset.Value.Virtue == null ? (Virtue)virtues.GetValue(randomNumber.Next(1, virtues.Length)) : requiredAsset.Value.Virtue);
				freshPerson.assets.Profession = (requiredAsset.Value.Profession == null ? (Profession)professions.GetValue(randomNumber.Next(1, professions.Length)) : requiredAsset.Value.Profession);
			}
			else
			{
				freshPerson.assets.Sin = (Sin)sins.GetValue(randomNumber.Next(1, sins.Length));
				freshPerson.assets.Virtue = (Virtue)virtues.GetValue(randomNumber.Next(1, virtues.Length));
				freshPerson.assets.Profession = (Profession)professions.GetValue(randomNumber.Next(1, professions.Length));
			freshPerson.Gender = randomNumber.Next(0, 2) == 0 ? Gender.Male : Gender.Female;



			SkillMap professionSkills = professionPool.GetProfessionValue(freshPerson.assets);

			freshPerson.Abduction = randomNumber.Next(0,100) + professionSkills.Abduction;
			freshPerson.AbductionDefense = randomNumber.Next(0, 100) + professionSkills.AbductionDefense;
			freshPerson.Investigation = randomNumber.Next(0, 100) + professionSkills.Investigation;
			freshPerson.InvestigationDefense = randomNumber.Next(0, 100) + professionSkills.InvestigationDefense;
			freshPerson.Recruitment = randomNumber.Next(0, 100) + professionSkills.Recruitment;
			freshPerson.RecruitmentDefense = randomNumber.Next(0, 100) + professionSkills.RecruitmentDefense;

			var descriptions = freshPerson.assets.Profession.GetAttributes<ProfessionDescriptionAttribute>().Where(d => d.Gender == null || d.Gender == freshPerson.Gender).ToArray();
			freshPerson.ProfessionDescription = descriptions[randomNumber.Next(0, descriptions.Length)].Description;

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

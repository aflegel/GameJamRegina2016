using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using System.Security.Cryptography;

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

				Person freshPerson = GeneratePerson(activePool.Count + 1, asset, randomNumber);

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
			if (requiredAsset.HasValue)
			{
				freshPerson.assets.Sin = (requiredAsset.Value.Sin == Sin.None ? (Sin)sins.GetValue(randomNumber.Next(1, sins.Length)) : requiredAsset.Value.Sin);
				freshPerson.assets.Virtue = (requiredAsset.Value.Virtue == Virtue.None ? (Virtue)virtues.GetValue(randomNumber.Next(1, virtues.Length)) : requiredAsset.Value.Virtue);
				freshPerson.assets.Profession = (requiredAsset.Value.Profession == Profession.None ? (Profession)professions.GetValue(randomNumber.Next(1, professions.Length)) : requiredAsset.Value.Profession);
			}
			else
			{
				freshPerson.assets.Sin = (Sin)sins.GetValue(randomNumber.Next(1, sins.Length));
				freshPerson.assets.Virtue = (Virtue)virtues.GetValue(randomNumber.Next(1, virtues.Length));
				freshPerson.assets.Profession = (Profession)professions.GetValue(randomNumber.Next(1, professions.Length));
			}
			freshPerson.Gender = randomNumber.Next(0, 2) == 0 ? Gender.Male : Gender.Female;

			SkillMap professionSkills = professionPool.GetProfessionValue(freshPerson.assets);

			freshPerson.Abduction = randomNumber.Next(0, 100) + professionSkills.Abduction;
			freshPerson.AbductionDefense = randomNumber.Next(0, 100) + professionSkills.AbductionDefense;
			freshPerson.Investigation = randomNumber.Next(0, 100) + professionSkills.Investigation;
			freshPerson.InvestigationDefense = randomNumber.Next(0, 100) + professionSkills.InvestigationDefense;
			freshPerson.Recruitment = randomNumber.Next(0, 100) + professionSkills.Recruitment;
			freshPerson.RecruitmentDefense = randomNumber.Next(0, 100) + professionSkills.RecruitmentDefense;

			var descriptions = freshPerson.assets.Profession.GetAttributes<ProfessionDescriptionAttribute>().Where(d => d.Gender == null || d.Gender == freshPerson.Gender).ToArray();
			freshPerson.ProfessionDescription = descriptions[randomNumber.Next(0, descriptions.Length)].Description;

			return freshPerson;
		}

		public Dictionary<int, Person> SearchPeople(SearchableAsset assets, int positives, int negatives, Random randomNumber)
		{

			var searchSuccess = activePool.Values.Where(s => s.Active == true);
			var searchFail = activePool.Values.Where(s => s.Active == true);
			Dictionary<int, Person> returnVal = new Dictionary<int, Person>();

			if (assets.Sin != Sin.None)
			{
				searchSuccess.Where(s => s.assets.Sin == assets.Sin);
				searchFail.Where(s => s.assets.Sin != assets.Sin);
			}

			if (assets.Virtue != Virtue.None)
			{
				searchSuccess.Where(s => s.assets.Virtue == assets.Virtue);
				searchFail.Where(s => s.assets.Virtue != assets.Virtue);
			}

			if (assets.Profession != Profession.None)
			{
				searchSuccess.Where(s => s.assets.Profession == assets.Profession);
				searchFail.Where(s => s.assets.Profession != assets.Profession);
			}

			//shuffle the results
			searchSuccess.OrderBy(a => Guid.NewGuid());
			searchFail.OrderBy(a => Guid.NewGuid());

			searchSuccess.Take(positives);
			searchFail.Take(negatives);

			foreach(Person person in searchSuccess)
				returnVal.Add(person.PersonID, person);

			foreach (Person person in searchFail)
				returnVal.Add(person.PersonID, person);

			return returnVal;
		}

		public Person SearchPeopleByID(int id)
		{
			return activePool[id];
		}


	}
}

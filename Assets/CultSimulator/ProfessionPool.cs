using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	class ProfessionPool
	{
		public Dictionary<Profession, SkillMap> Pool { get; set; }


		public ProfessionPool()
		{
			Pool = new Dictionary<Profession, SkillMap>();
			GenerateProfessions();
		}

		public SkillMap GetProfessionValue(SearchableAsset keyValue)
		{

			//check the dictionary against values and return the match
			return Pool[keyValue.Profession];
		}

		public void GenerateProfessions()
		{
			List<object> values = Enum.GetValues(typeof(Profession)).Cast<object>().ToList();

			foreach (object firstKey in values)
			{
				SkillMap entry = new SkillMap();
				entry.Abduction = 0;
				entry.AbductionDefense = 0;
				entry.Investigation = 0;
				entry.InvestigationDefense = 0;
				entry.Recruitment = 0;
				entry.RecruitmentDefense = 0;


				if (!Pool.ContainsKey((Profession)firstKey))
				{
					Pool.Add((Profession)firstKey, entry);
				}
			}

			SkillMap temp = Pool[Profession.Cow];
			temp.Investigation = 0;
		}

	}

	public struct SkillMap
	{
		public int Abduction;
		public int AbductionDefense;
		public int Investigation;
		public int InvestigationDefense;
		public int Recruitment;
		public int RecruitmentDefense;
	}

}

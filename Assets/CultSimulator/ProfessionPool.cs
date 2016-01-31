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

			Pool.Add(Profession.Medical, GenerateSkillMap(5, 5, 10, 10, 0, 0));
			Pool.Add(Profession.Religious, GenerateSkillMap(0, 0, 5, 5, 10, 10));
			Pool.Add(Profession.Law, GenerateSkillMap(10, 10, 5, 5, 0, 0));
			Pool.Add(Profession.Politics, GenerateSkillMap(0, 0, 10, -10, 5, 5));
			Pool.Add(Profession.Trades, GenerateSkillMap(10, 5, 5, 0, 0, 10));
			Pool.Add(Profession.Merchant, GenerateSkillMap(-10, -10, 0, 0, 5, 5));
			Pool.Add(Profession.Educator, GenerateSkillMap(-10, -10, 5, 5, 0, 0));
			Pool.Add(Profession.Rural, GenerateSkillMap(5, 0, 0, 5, -10, -10));


			Pool.Add(Profession.Cow, GenerateSkillMap(0, 1000, 0, 0, 0, 1000));
			Pool.Add(Profession.Goat, GenerateSkillMap(0, 1000, 0, 0, 0, 1000));

		}

		public SkillMap GenerateSkillMap(int abduction, int AbductionDefense, int investigation, int investigationDefense, int recruitment, int recruitmentDefense)
		{
			SkillMap entry = new SkillMap();
			entry.Abduction = abduction;
			entry.AbductionDefense = AbductionDefense;
			entry.Investigation = investigation;
			entry.InvestigationDefense = investigationDefense;
			entry.Recruitment = recruitment;
			entry.RecruitmentDefense = recruitmentDefense;

			return entry;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	public class Person
	{
		public int PersonID { get; set; }
		public bool Active { get; set; }
		public bool Sacrifice { get; set; }
		public string Name { get; set; }
		public string FlavourText { get; set; }
		public int Abduction { get; set; }
		public int AbductionDefense { get; set; }
		public int Investigation { get; set; }
		public int InvestigationDefense { get; set; }
		public int Recruitment { get; set; }
		public int RecruitmentDefense { get; set; }
		public bool Gender { get; set; }

		public SearchableAsset assets;
	}
}

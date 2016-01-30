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
		public string Name { get; set; }
		public string FlavourText { get; set; }
		public double Abduction { get; set; }
		public double AbductionDefense { get; set; }
		public double Investigation { get; set; }
		public double InvestigationDefense { get; set; }
		public double Recruitment { get; set; }
		public double RecruitmentDefense { get; set; }
		public bool Gender { get; set; }

		public SearchableAsset assets;
	}
}

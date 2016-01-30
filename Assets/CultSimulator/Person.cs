using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets.Models
{
	public class Person
	{
		public string Name;
		public string FlavourText;
		public decimal Abduction { get; set; }
		public decimal AbductionDefense { get; set; }
		public decimal Investigation { get; set; }
		public decimal InvestigationDefense { get; set; }
		public decimal Recruitment { get; set; }
		public decimal RecruitmentDefense { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets.Models
{
	class Sacrifice
	{
		public decimal abduction { get; set; }
		public decimal abductionDefense { get; set; }
		public decimal investigation { get; set; }
		public decimal investigationDefense { get; set; }
		public decimal recruitment { get; set; }
		public decimal recruitmentDefense { get; set; }
		public ProfessionTrait.professions profession;
	}
}

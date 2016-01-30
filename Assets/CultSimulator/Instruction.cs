using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	public class Instruction
	{
		public int PersonID { get; set; }
		public ActionType Action { get; set; }
		public int TargetID { get; set; }
	}
}

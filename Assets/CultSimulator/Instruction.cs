using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	public class Instruction
	{
		public ActionType Action { get; set; }
		public int? TargetID { get; set; }
		public SuccessRating IsSuccess { get; set; }
	}

	public enum SuccessRating
	{
		GreatSuccess = 0,
		GoodSuccess = 1,
		NornalSuccess = 2,
		Failure = 3,
		BadFailure = 4,
		TerribleFailure = 5,
		Pending = 6

	}
}

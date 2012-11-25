namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion 

	public class Vote
	{
		[Key]
		[Required]
		[Column(Order = 0)]
		public String UserId { get; set; }

		[Key]
		[Required]
		[Column(Order = 1)]
		public String StudentNumber { get; set; }
	}
}

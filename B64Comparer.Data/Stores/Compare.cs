using System.ComponentModel.DataAnnotations;

namespace B64Comparer.Data.Stores
{
	public sealed class Compare
	{
		[Key]
		public int Id { get; set; }

		public string Left { get; set; }

		public string Right { get; set; }
	}
}

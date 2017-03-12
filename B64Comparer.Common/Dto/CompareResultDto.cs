using System.Collections.Generic;

namespace B64Comparer.Common.Dto
{
	#region Enumeration
	public enum Status
	{
		Unknown = 0,
		Equal = 1,
		Left = 2,
		Right = 3,
	}
	#endregion

	public sealed class CompareResultDto
	{
		public CompareResultDto()
		{
			Offsets = new List<int>();
		}

		public Status Status { get; set; }

		public IList<int> Offsets { get; private set; }

		public int Length { get { return Offsets.Count; } }
	}
}

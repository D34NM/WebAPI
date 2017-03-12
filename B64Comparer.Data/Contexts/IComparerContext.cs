
using B64Comparer.Data.Stores;
using System.Data.Entity;

namespace B64Comparer.Data.Contexts
{
	public interface IComparerContext
	{
		DbSet<Compare> Compares { get; set; }
	}
}

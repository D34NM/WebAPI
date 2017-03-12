using B64Comparer.Data.Contexts;
using B64Comparer.Data.Stores;
using System.Data.Entity;

namespace B64Comparer.Data
{
	public class B64ComparerContext : DbContext, IComparerContext
	{
		public B64ComparerContext()
			: base()
		{ }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{ }

		public virtual DbSet<Compare> Compares { get; set; }
	}
}

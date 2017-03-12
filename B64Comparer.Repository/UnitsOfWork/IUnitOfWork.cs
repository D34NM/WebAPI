using B64Comparer.Repository.Repositories;

namespace B64Comparer.Repository.UnitsOfWork
{
	public interface IUnitOfWork
	{
		IRepository B64ComparerRepository { get; }

		int Save();
	}
}

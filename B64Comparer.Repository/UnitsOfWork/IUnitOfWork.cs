using B64Comparer.Repository.Repositories;

namespace B64Comparer.Repository.UnitsOfWork
{
	/// <summary>
	/// UnitOfWork pattern. 
	/// Works great in combination with the Repository pattern to abstract the DAL of the framework that is in use.
	/// In this case it is Entity framework.
	/// </summary>
	public interface IUnitOfWork
	{
		/// <summary>
		/// The Repository used in this specific API for DAL logic.
		/// </summary>
		IRepository B64ComparerRepository { get; }

		/// <summary>
		/// Invokes the save of the changes in the current context.
		/// </summary>
		/// <returns></returns>
		int Save();
	}
}

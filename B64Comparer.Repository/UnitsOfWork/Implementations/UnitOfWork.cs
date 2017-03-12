using B64Comparer.Data.Contexts;
using B64Comparer.Repositories.Implementations;
using B64Comparer.Repository.Repositories;
using System.Data.Entity;

namespace B64Comparer.Repository.UnitsOfWork.Implementations
{
	/// <summary>
	/// UnitOfWork pattern. 
	/// Works great in combination with the Repository pattern to abstract the DAL of the framework that is in use.
	/// In this case it is Entity framework.
	/// </summary>
	public sealed class UnitOfWork : IUnitOfWork
	{
		#region Fields

		private readonly DbContext _context;
		private readonly IRepository _repository;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the unit of work.
		/// Initializes a repository with provided context
		/// </summary>
		/// <param name="context">The database context.</param>
		public UnitOfWork(IComparerContext context)
		{
			_context = context as DbContext;
			_repository = new B64Repository(_context);
		}

		#endregion

		/// <summary>
		/// The Repository used in this specific API for DAL logic.
		/// </summary>
		public IRepository B64ComparerRepository
		{
			get { return _repository; }
		}

		/// <summary>
		/// Invokes the save of the changes in the current context.
		/// </summary>
		/// <returns></returns>
		public int Save()
		{
			return _context.SaveChanges();
		}
	}
}

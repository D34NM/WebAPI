using B64Comparer.Data.Contexts;
using B64Comparer.Repositories.Implementations;
using B64Comparer.Repository.Repositories;
using System.Data.Entity;

namespace B64Comparer.Repository.UnitsOfWork.Implementations
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		#region Fields

		private readonly DbContext _context;
		private readonly IRepository _repository;

		#endregion

		#region Constructor

		public UnitOfWork(IComparerContext context)
		{
			_context = context as DbContext;
			_repository = new B64Repository(_context);
		}

		#endregion

		public IRepository B64ComparerRepository
		{
			get { return _repository; }
		}

		public int Save()
		{
			return _context.SaveChanges();
		}
	}
}

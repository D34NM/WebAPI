using B64Comparer.Data;
using B64Comparer.Data.Stores;
using B64Comparer.Repository.Repositories;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace B64Comparer.Repositories.Implementations
{
	public sealed class B64Repository : IRepository
	{
		#region Fields

		private readonly B64ComparerContext _context;

		#endregion

		#region Constructor

		public B64Repository(DbContext context)
		{
			_context = (B64ComparerContext)context;
		}

		#endregion

		public async Task<Compare> GetAsync(int id)
		{
			return await _context.Compares.FindAsync(id);
		}

		public async Task<int> UpdateLeft(int id, string data)
		{
			Compare existing = await GetAsync(id);

			return
				WithExceptionHandling(() =>
				{
					if (existing == null)
					{
						existing = new Compare
						{
							Id = id,
							Left = data
						};
					}

					existing.Left = data;

					_context.Compares.AddOrUpdate(existing);
				});
		}

		public async Task<int> UpdateRight(int id, string data)
		{
			Compare existing = await GetAsync(id);

			return
				WithExceptionHandling(() =>
				{
					if (existing == null)
					{
						existing = new Compare
						{
							Id = id,
							Right = data
						};
					}

					existing.Right = data;
					_context.Compares.AddOrUpdate(existing);
				});
		}

		#region Private methods

		private int WithExceptionHandling(Action action)
		{
			try
			{
				action.Invoke();
				return 0;
			}
			catch (SqlException)
			{
				return -1;
			}
		}

		#endregion
	}
}

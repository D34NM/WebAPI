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
	/// <summary>
	/// Simple Repository pattern to wrap the Entity framework and provide more clearity how to work with data.
	/// </summary>
	public sealed class B64Repository : IRepository
	{
		#region Fields

		private readonly B64ComparerContext _context;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the repository.
		/// </summary>
		/// <param name="context">The database context/</param>
		public B64Repository(DbContext context)
		{
			_context = (B64ComparerContext)context;
		}

		#endregion

		/// <summary>
		/// Get the <see cref="Compare"/> based on the id.
		/// </summary>
		/// <param name="id">The request id.</param>
		/// <returns>Null if no data by the provided id, <see cref="Compare"/> otherwise.</returns>
		public async Task<Compare> GetAsync(int id)
		{
			return await _context.Compares.FindAsync(id);
		}

		/// <summary>
		/// Updates the entity in the database (or creates a new one) with provided id and data. 
		/// This data comes from the "left side" of the API.
		/// </summary>
		/// <param name="id">The request id.</param>
		/// <param name="data">The data to store.</param>
		/// <returns>0 if it is saved, -1 if there is an exception.</returns>
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

		/// <summary>
		/// Updates the entity in the database (or creates a new one) with provided id and data. 
		/// This data comes from the "right side" of the API.
		/// </summary>
		/// <param name="id">The request id.</param>
		/// <param name="data">The data to store.</param>
		/// <returns>0 if it is saved, -1 if there is an exception.</returns>
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

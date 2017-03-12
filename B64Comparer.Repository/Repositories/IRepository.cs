using B64Comparer.Data.Stores;
using System.Threading.Tasks;

namespace B64Comparer.Repository.Repositories
{
	/// <summary>
	/// Simple Repository pattern to wrap the Entity framework and provide more clearity how to work with data.
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Get the <see cref="Compare"/> based on the id.
		/// </summary>
		/// <param name="id">The request id.</param>
		/// <returns>Null if no data by the provided id, <see cref="Compare"/> otherwise.</returns>
		Task<Compare> GetAsync(int id);

		/// <summary>
		/// Updates the entity in the database (or creates a new one) with provided id and data. 
		/// This data comes from the "left side" of the API.
		/// </summary>
		/// <param name="id">The request id.</param>
		/// <param name="data">The data to store.</param>
		/// <returns>0 if it is saved, -1 if there is an exception.</returns>
		Task<int> UpdateLeft(int id, string data);

		/// <summary>
		/// Updates the entity in the database (or creates a new one) with provided id and data. 
		/// This data comes from the "right side" of the API.
		/// </summary>
		/// <param name="id">The request id.</param>
		/// <param name="data">The data to store.</param>
		/// <returns>0 if it is saved, -1 if there is an exception.</returns>
		Task<int> UpdateRight(int id, string data);
	}
}

using B64Comparer.Common.Dto;
using System.Threading.Tasks;

namespace B64Comparer.Services
{
	/// <summary>
	/// Service to handle the comparison of the provided binary64 encoded data.
	/// </summary>
	public interface ICompareService
	{
		/// <summary>
		/// Get the <see cref="CompareDto"/> by the id.
		/// </summary>
		/// <param name="id">Id to lookup.</param>
		/// <returns>Null if it is not found by the id, <see cref="CompareDto"/> otherwise.</returns>
		Task<CompareDto> GetAsync(int id);

		/// <summary>
		/// Stores the data provided by the "left side" of input.
		/// </summary>
		/// <param name="id">Id of the request.</param>
		/// <param name="data">The binary64 encoded data.</param>
		/// <returns>Yes if it is saved, No otherwise.</returns>
		Task<string> StoreLeftAsync(int id, string data);

		/// <summary>
		/// Stores the data provided by the "right side" of input.
		/// </summary>
		/// <param name="id">Id of the request.</param>
		/// <param name="data">The binary64 encoded data.</param>
		/// <returns>Yes if it is saved, No otherwise.</returns>
		Task<string> StoreRightAsync(int id, string data);

		/// <summary>
		/// Compares the data that was stored based on the request id that is provided.
		/// </summary>
		/// <param name="id">Request id.</param>
		/// <returns><see cref="CompareResultDto"/> containt information about the differences between two sides.</returns>
		Task<CompareResultDto> Compare(int id);
	}
}

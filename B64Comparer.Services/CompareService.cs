using B64Comparer.Common.Dto;
using B64Comparer.Data.Stores;
using B64Comparer.Repository.UnitsOfWork;
using System;
using System.Threading.Tasks;

namespace B64Comparer.Services
{
	public class CompareService : ICompareService
	{
		#region Fields

		private readonly IUnitOfWork _unitOfWork;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the service.
		/// </summary>
		/// <param name="unitOfWork">The unit of work to handle the data side.</param>
		public CompareService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		#endregion

		/// <summary>
		/// Get the <see cref="CompareDto"/> by the id.
		/// </summary>
		/// <param name="id">Id to lookup.</param>
		/// <returns>Null if it is not found by the id, <see cref="CompareDto"/> otherwise.</returns>
		public async Task<CompareDto> GetAsync(int id)
		{
			Compare store = await _unitOfWork.B64ComparerRepository.GetAsync(id);

			return new CompareDto { Id = store.Id, Left = store.Left, Right = store.Right };
		}

		/// <summary>
		/// Stores the data provided by the "left side" of input.
		/// </summary>
		/// <param name="id">Id of the request.</param>
		/// <param name="data">The binary64 encoded data.</param>
		/// <returns>Yes if it is saved, No otherwise.</returns>
		public async Task<string> StoreLeftAsync(int id, string data)
		{
			int result = await _unitOfWork.B64ComparerRepository.UpdateLeft(id, data);
			_unitOfWork.Save();

			return OutputConverter(result);
		}

		/// <summary>
		/// Stores the data provided by the "right side" of input.
		/// </summary>
		/// <param name="id">Id of the request.</param>
		/// <param name="data">The binary64 encoded data.</param>
		/// <returns>Yes if it is saved, No otherwise.</returns>
		public async Task<string> StoreRightAsync(int id, string data)
		{
			int result = await _unitOfWork.B64ComparerRepository.UpdateRight(id, data);
			_unitOfWork.Save();

			return OutputConverter(result);
		}

		/// <summary>
		/// Compares the data that was stored based on the request id that is provided.
		/// </summary>
		/// <param name="id">Request id.</param>
		/// <returns><see cref="CompareResultDto"/> containt information about the differences between two sides.</returns>
		public async Task<CompareResultDto> Compare(int id)
		{
			CompareDto dto = await GetAsync(id);

			if (dto.Left == null || dto.Right == null)
			{
				return new CompareResultDto { Status = Status.Unknown };
			}

			if (dto.Left.Length > dto.Right.Length)
			{
				return new CompareResultDto { Status = Status.Left };
			}

			if (dto.Right.Length > dto.Left.Length)
			{
				return new CompareResultDto { Status = Status.Right };
			}

			byte[] left = Convert.FromBase64String(dto.Left);
			byte[] right = Convert.FromBase64String(dto.Right);

			CompareResultDto result = new CompareResultDto { Status = Status.Equal };

			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					result.Offsets.Add(i);
				}
			}

			return result;
		}

		#region Private methods

		private string OutputConverter(int result)
		{
			return (result == 0) ? "Yes" : "No";
		}

		#endregion
	}
}

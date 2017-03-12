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

		public CompareService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		#endregion

		public async Task<CompareDto> GetAsync(int id)
		{
			Compare store = await _unitOfWork.B64ComparerRepository.GetAsync(id);

			return new CompareDto { Id = store.Id, Left = store.Left, Right = store.Right };
		}

		public async Task<string> StoreLeftAsync(int id, string data)
		{
			int result = await _unitOfWork.B64ComparerRepository.UpdateLeft(id, data);
			_unitOfWork.Save();

			return string.Empty;
		}

		public async Task<string> StoreRightAsync(int id, string data)
		{
			int result = await _unitOfWork.B64ComparerRepository.UpdateRight(id, data);
			_unitOfWork.Save();

			return string.Empty;
		}

		public async Task<CompareResultDto> Compare(int id)
		{
			CompareDto dto = await GetAsync(id);

			// todo
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

			CompareResultDto result = new CompareResultDto { };

			int mid = (left.Length) / 2;

			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					result.Offsets.Add(i);
				}
			}

			return result;
		}
	}
}

using B64Comparer.Common.Dto;
using System.Threading.Tasks;

namespace B64Comparer.Services
{
	public interface ICompareService
	{
		Task<CompareDto> GetAsync(int id);

		Task<string> StoreLeftAsync(int id, string data);

		Task<string> StoreRightAsync(int id, string data);

		Task<CompareResultDto> Compare(int id);
	}
}

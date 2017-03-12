using B64Comparer.Data.Stores;
using System.Threading.Tasks;

namespace B64Comparer.Repository.Repositories
{
	public interface IRepository
	{
		Task<Compare> GetAsync(int id);

		Task<int> UpdateLeft(int id, string data);

		Task<int> UpdateRight(int id, string data);
	}
}

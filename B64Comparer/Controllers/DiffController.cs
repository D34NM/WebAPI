using B64Comparer.Services;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace B64Comparer.Controllers
{
	public class DiffController : ApiController
	{
		private readonly ICompareService _service;

		#region Constructor

		public DiffController(ICompareService service)
		{
			_service = service;
		}

		#endregion

		[HttpPost]
		public async Task<string> Left(int? id, [FromBody] string data)
		{
			ValidateIdAndData(id, data);

			await _service.StoreLeftAsync(id.Value, data);

			return null;
		}

		[HttpPost]
		public async Task<string> Right(int? id, [FromBody] string data)
		{
			ValidateIdAndData(id, data);

			await _service.StoreRightAsync(id.Value, data);

			return null;
		}

		[HttpPost]
		public async Task<string> Diff(int? id)
		{
			ValidateIdAndData(id, string.Empty);

			await _service.Compare(id.Value);

			return null;
		}

		#region Private methods

		private void ValidateIdAndData(int? id, string data)
		{
			if (!id.HasValue || data == null)
			{
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NoContent));
			}
		}

		#endregion
	}
}

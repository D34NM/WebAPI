using Microsoft.AspNet.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace B64Comparer.Integration.Tests
{
	[TestClass]
	public class IntegrationFacts
	{
		private TestServer _server;

		[TestInitialize]
		public void TestMethod1()
		{
		}

		[TestMethod]
		public async Task Should_ReturnEqual_When_DataIsValid()
		{
			StringContent content = new StringContent("adadaasdaskdjaka");
			var response = await new HttpClient().PutAsync("http://localhost/v1/diff/1/left", content);

			Assert.IsNotNull(response);
		}
	}
}

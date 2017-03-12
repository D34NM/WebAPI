using AutoMoq;
using B64Comparer.Repository.Repositories;
using B64Comparer.Repository.UnitsOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace B64Comparer.Services.Tests
{
	[TestClass]
	public class CompareServiceFacts
	{
		private AutoMoqer _moqer;
		private CompareService _service;

		private string _data = "AAAASDASDADASDMASKDJAKLSJDLAFKLASJFLAJSFKLAJSFKLJASFASJFKLASJFKLASJFKLASJFKLASJFKLJASFKLJASFAAS";

		[TestInitialize]
		public void TestInit()
		{
			_moqer = new AutoMoqer();

			_moqer.GetMock<IRepository>()
				.Setup(m => m.UpdateLeft(It.IsAny<int>(), It.IsAny<string>()))
				.Returns(Task.FromResult(0));
			_moqer.GetMock<IRepository>()
				.Setup(m => m.UpdateRight(It.IsAny<int>(), It.IsAny<string>()))
				.Returns(Task.FromResult(0));
			_moqer.GetMock<IUnitOfWork>()
				.Setup(m => m.B64ComparerRepository)
				.Returns(_moqer.GetMock<IRepository>().Object);

			_service = _moqer.Create<CompareService>();
		}

		[TestClass]
		public class TheStoreLeftAsync : CompareServiceFacts
		{
			[TestMethod]
			public async Task Should_ReturnYes_When_DataIsSaved()
			{
				// act
				var result = await _service.StoreLeftAsync(1, _data);

				// assert
				Assert.AreEqual("Yes", result);
			}
		}

		[TestClass]
		public class TheStoreRightAsync : CompareServiceFacts
		{
			[TestMethod]
			public async Task Should_ReturnYes_When_DataIsSaved()
			{
				// act
				var result = await _service.StoreRightAsync(1, _data);

				// assert
				Assert.AreEqual("Yes", result);
			}
		}
	}
}

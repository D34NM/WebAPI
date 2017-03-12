using AutoMoq;
using B64Comparer.Common.Dto;
using B64Comparer.Controllers;
using B64Comparer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace B64Comparer.Tests
{
	[TestClass]
	class DiffControllerFacts
	{
		private AutoMoqer _moqer;
		private DiffController _controller;

		private string _data = "AAAASDASDADASDMASKDJAKLSJDLAFKLASJFLAJSFKLAJSFKLJASFASJFKLASJFKLASJFKLASJFKLASJFKLJASFKLJASFAAS";

		[TestInitialize]
		public void TestInit()
		{
			_moqer = new AutoMoqer();

			_moqer.GetMock<ICompareService>()
				.Setup(m => m.StoreLeftAsync(It.IsAny<int>(), It.IsAny<string>()))
				.Returns(Task.FromResult("Yes"));
			_moqer.GetMock<ICompareService>()
				.Setup(m => m.StoreRightAsync(It.IsAny<int>(), It.IsAny<string>()))
				.Returns(Task.FromResult("Yes"));
			_moqer.GetMock<ICompareService>()
				.Setup(m => m.Compare(It.IsAny<int>()))
				.Returns(Task.FromResult(new CompareResultDto { Status = Status.Equal }));

			_controller = _moqer.Create<DiffController>();
		}

		[TestClass]
		public class TheLeftMethod : DiffControllerFacts
		{
			[TestMethod]
			public async Task Should_ThrowException_When_IdParameterIsNull()
			{
				// arrange
				bool result = false;

				// act
				try
				{
					await _controller.Left(null, string.Empty);
				}
				catch (Exception)
				{
					result = true;
				}


				// assert
				Assert.IsTrue(result);
			}

			[TestMethod]
			public async Task Should_ThrowException_When_DataParameterIsNull()
			{
				// arrange
				bool result = false;

				// act
				try
				{
					await _controller.Left(1, null);
				}
				catch (Exception)
				{
					result = true;
				}


				// assert
				Assert.IsTrue(result);
			}

			[TestMethod]
			public async Task Should_ReturnYes_When_IsSuccesfull()
			{
				// act
				string json = await _controller.Left(1, _data);

				// assert
				Assert.IsTrue(json.Contains("Yes"));
			}
		}

		[TestClass]
		public class TheRightMethod : DiffControllerFacts
		{
			[TestMethod]
			public async Task Should_ThrowException_When_IdParameterIsNull()
			{
				// arrange
				bool result = false;

				// act
				try
				{
					await _controller.Right(null, string.Empty);
				}
				catch (Exception)
				{
					result = true;
				}


				// assert
				Assert.IsTrue(result);
			}

			[TestMethod]
			public async Task Should_ThrowException_When_DataParameterIsNull()
			{
				// arrange
				bool result = false;

				// act
				try
				{
					await _controller.Right(1, null);
				}
				catch (Exception)
				{
					result = true;
				}


				// assert
				Assert.IsTrue(result);
			}

			[TestMethod]
			public async Task Should_ReturnYes_When_IsSuccesfull()
			{
				// act
				string json = await _controller.Right(1, _data);

				// assert
				Assert.IsTrue(json.Contains("Yes"));
			}
		}

		[TestClass]
		public class TheDiffMethod : DiffControllerFacts
		{
			[TestMethod]
			public async Task Should_ThrowException_When_IdParameterIsNull()
			{
				// arrange
				bool result = false;

				// act
				try
				{
					await _controller.Diff(null);
				}
				catch (Exception)
				{
					result = true;
				}

				// assert
				Assert.IsTrue(result);
			}

			[TestMethod]
			public async Task Should_ReturnEqualResult_When_DataIsValid()
			{
				// act
				var json = await _controller.Diff(1);

				// assert
				Assert.IsTrue(json.Contains(Status.Equal.ToString()));
			}
		}
	}
}

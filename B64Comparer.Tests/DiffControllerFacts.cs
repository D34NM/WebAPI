using AutoMoq;
using B64Comparer.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace B64Comparer.Tests
{
	[TestClass]
	class DiffControllerFacts
	{
		private AutoMoqer _moqer;
		private DiffController _controller;

		[TestInitialize]
		public void TestInit()
		{
			_moqer = new AutoMoqer();
			_controller = _moqer.Create<DiffController>();
		}

		private bool ThrowsException(Action action)
		{
			try
			{
				action.Invoke();
				return false;
			}
			catch
			{
				return true;
			}
		}

		[TestClass]
		public class TheLeftMethod : DiffControllerFacts
		{
			[TestMethod]
			public void Should_ThrowException_When_IdParameterIsNull()
			{
				// act
				var result = ThrowsException(async () =>
				{
					await _controller.Left(null, string.Empty);
				});

				// assert
				Assert.IsTrue(result);
			}
		}
	}
}

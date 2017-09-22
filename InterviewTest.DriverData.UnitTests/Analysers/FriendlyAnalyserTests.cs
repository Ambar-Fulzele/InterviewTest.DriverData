using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class FriendlyAnalyserTests
	{
		[Test]
		public void ShouldAnalyseWholePeriodAndReturn1ForDriverRating()
		{
            // BONUS: What is AAA?
            // The AAA stands for Arrange Act Assert which is pattern to structure unit tests.
            // Arrange: First step of a unit test application. Necessary setup of the test is done
            // Act: Middle step of a unit test application. Execution of unit test
            // Assert: Final step of a unit test application. Check and verify actual result with expected results.


            // This is Arrange
            var data = new[]
			{
				new Period
				{
					Start = new DateTimeOffset(2001, 1, 1, 0, 0, 0, TimeSpan.Zero),
					End = new DateTimeOffset(2001, 1, 1, 12, 0, 0, TimeSpan.Zero),
					AverageSpeed = 20m
				},
				new Period
				{
					Start = new DateTimeOffset(2001, 1, 1, 12, 0, 0, TimeSpan.Zero),
					End = new DateTimeOffset(2001, 1, 2, 0, 0, 0, TimeSpan.Zero),
					AverageSpeed = 15m
				}
			};

			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = TimeSpan.FromDays(1),
				DriverRating = 1m
			};

            // This is Act
            var actualResult = new FriendlyAnalyser().Analyse(data,false);

            // This is Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
		}
	}
}

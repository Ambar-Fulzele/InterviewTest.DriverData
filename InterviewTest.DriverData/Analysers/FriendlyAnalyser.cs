using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
	// BONUS: Why internal?
	internal class FriendlyAnalyser : IAnalyser
	{
		public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history, bool isPenalise)
		{
            HistoryAnalysis result = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.Zero,
                DriverRating = 0,
                UnDocumentedPeriod = false
            };

            if (history == null)
                return result;

            if (history.Count == 0)
                return result;

            result.AnalysedDuration = history.Last().End - history.First().Start;
            result.DriverRating =  1m;
            
            DateTimeOffset startDate = new DateTimeOffset(history.First().Start.Date);
            DateTimeOffset endDate = new DateTimeOffset(history.Last().End.Date);

            DateTime driverStartPeriod = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour , startDate.Minute, startDate.Second);
            DateTime driverEndPeriod = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, endDate.Second);

            result.UnDocumentedPeriod = AnalyserHelper.getUndocumentedPeriod(history, driverStartPeriod, driverEndPeriod);

            result = AnalyserHelper.penaliseAnalyser(result, isPenalise && result.UnDocumentedPeriod);

            return result;
		}
	}
}
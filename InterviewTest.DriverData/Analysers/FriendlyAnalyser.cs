using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    // With internal, classes are not accessable outside the assembly. The access is limited to the program only and no external program can access these.
    // This helps in information/implementation hiding.
    // In our case since all the analyser related operations are happening in single assembly "InterviewTest.DriverData", making this as internal will ensure that 
    // the classes within same assembly will be able to accessible them but will restirct to use them outside assembly.

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
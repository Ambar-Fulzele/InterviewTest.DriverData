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
                AnalysedDuration = history.Last().End - history.First().Start,
                DriverRating = 1m,
                UnDocumentedPeriod = false
            };

            result.UnDocumentedPeriod = AnalyserHelper.getUndocumentedPeriod(history, history.First().Start.TimeOfDay, history.Last().End.TimeOfDay);

            result = AnalyserHelper.penaliseAnalyser(result, isPenalise && result.UnDocumentedPeriod);

            return result;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
	// BONUS: Why internal?
	internal class FormulaOneAnalyser : IAnalyser
	{
        const decimal MINIMUMSPEED = 0, MAXIMUMSPEED = 200, ZEROSPEED = 0;
        
        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history, bool isPenalise)
		{
            HistoryAnalysis result = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.Zero,
                DriverRating = 0,
                UnDocumentedPeriod = false
            };

            List<HistoryAnalysis> histCollection = new List<HistoryAnalysis>();

            //Ignore anything before the first non-zero speed in a day, and after the last
            history.OrderBy(h => h.Start).SkipWhile(h => h.AverageSpeed == 0).Reverse().SkipWhile(h => h.AverageSpeed == 0).ToList().ForEach(h =>
                {
                    histCollection.Add(getValidHistoryData(h, h.Start.TimeOfDay, h.End.TimeOfDay));
                }
            );

            DateTimeOffset startDate = history.OrderBy(h => h.Start).SkipWhile(h => h.AverageSpeed == 0).First().Start;
            DateTimeOffset endDate = history.OrderBy(h => h.Start).SkipWhile(h => h.AverageSpeed == 0).Reverse().SkipWhile(h => h.AverageSpeed == 0).First().End;

            DateTime driverStartPeriod = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, startDate.Second);
            DateTime driverEndPeriod = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, endDate.Second);

            //DateTimeOffset driverStartPeriod = history.OrderBy(h => h.Start).SkipWhile(h => h.AverageSpeed == 0).First().Start;
            //DateTimeOffset driverEndPeriod = history.OrderBy(h => h.Start).SkipWhile(h => h.AverageSpeed == 0).Reverse().SkipWhile(h => h.AverageSpeed == 0).First().End;

            result = AnalyserHelper.getFinalRating( histCollection, (endDate-startDate).Ticks);

            result.UnDocumentedPeriod = AnalyserHelper.getUndocumentedPeriod(history, driverStartPeriod, driverEndPeriod);

            result = AnalyserHelper.penaliseAnalyser(result, isPenalise && result.UnDocumentedPeriod);

            return result;
        }

        private HistoryAnalysis getValidHistoryData(Period p, TimeSpan startingTime, TimeSpan endingTime)
        {
            HistoryAnalysis histdata = new HistoryAnalysis();

            histdata.AnalysedDuration = (endingTime - startingTime);

            //Speeds between 0 and 200 map linearly to [0,1] and Speeds above 200 map to 1
            histdata.DriverRating = ((p.AverageSpeed > MAXIMUMSPEED) ? 1 :
                                            ((p.AverageSpeed <= MAXIMUMSPEED && p.AverageSpeed > MINIMUMSPEED) ? (p.AverageSpeed / MAXIMUMSPEED) : ZEROSPEED));

            return histdata;
        }
    }
}
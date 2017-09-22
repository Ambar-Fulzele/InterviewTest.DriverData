using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
	// BONUS: Why internal?
	internal class GetawayDriverAnalyser : IAnalyser
	{
        const decimal MINIMUMSPEED = 0, MAXIMUMSPEED = 80, ZEROSPEED = 0;
        TimeSpan startTime = new TimeSpan(13, 0, 0);
        TimeSpan endTime = new TimeSpan(14, 0, 0);

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

            List<HistoryAnalysis> histCollection = new List<HistoryAnalysis>();

            //Ignore anything before the first non-zero speed in a day, and after the last
            history.OrderBy(h => h.Start).SkipWhile(h => h.AverageSpeed == 0).Reverse().SkipWhile(h => h.AverageSpeed == 0).ToList().ForEach(h =>
            {
                // Start and End time are in the range
                if (h.Start.TimeOfDay >= startTime && h.Start.TimeOfDay <= endTime
                    && h.End.TimeOfDay >= startTime && h.End.TimeOfDay <= endTime)
                {
                    // Add valid data to collection for weighted average rating calculation
                    histCollection.Add(getValidHistoryData(h, h.Start.TimeOfDay, h.End.TimeOfDay));
                }

                // Start time is out of range and end time is in the range
                if (h.Start.TimeOfDay < startTime
                    && h.End.TimeOfDay > startTime && h.End.TimeOfDay <= endTime)
                {
                    // Add valid data to collection for weighted average rating calculation
                    histCollection.Add(getValidHistoryData(h, startTime, h.End.TimeOfDay));
                }

                // Start time in the range and end time is out of range
                if (h.Start.TimeOfDay >= startTime && h.Start.TimeOfDay < endTime
                    && h.End.TimeOfDay > endTime)
                {
                    // Add valid data to collection for weighted average rating calculation
                    histCollection.Add(getValidHistoryData(h, h.Start.TimeOfDay, endTime));
                }

                // Start time and end time both are out of range
                if (h.Start.TimeOfDay < startTime && h.End.TimeOfDay > endTime)
                {
                    // Add valid data to collection for weighted average rating calculation
                    histCollection.Add(getValidHistoryData(h, startTime, endTime));
                }
            });

            if (histCollection == null)
                return result;

            if (histCollection.Count == 0)
                return result;

            DateTimeOffset startDate = new DateTimeOffset(history.OrderBy(h => h.Start).First().Start.Date);
            DateTimeOffset endDate = new DateTimeOffset(history.OrderBy(h => h.Start).Last().Start.Date);

            DateTime driverStartPeriod = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hours, startTime.Minutes, startTime.Seconds);
            DateTime driverEndPeriod = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hours, endTime.Minutes, endTime.Seconds);

            result = AnalyserHelper.getFinalRating(histCollection, (endTime - startTime).Ticks);

            result.UnDocumentedPeriod = AnalyserHelper.getUndocumentedPeriod(history, driverStartPeriod, driverEndPeriod);

            result = AnalyserHelper.penaliseAnalyser(result, isPenalise && result.UnDocumentedPeriod);

            return result;
        }

        private HistoryAnalysis getValidHistoryData(Period p, TimeSpan startingTime, TimeSpan endingTime)
        {
            HistoryAnalysis histdata = new HistoryAnalysis();

            histdata.AnalysedDuration = (endingTime - startingTime);

            //Speeds between 0 and 80 map linearly to [0,1] and Speeds above 80 map to 1
            histdata.DriverRating = ((p.AverageSpeed > MAXIMUMSPEED) ? 1 :
                                            ((p.AverageSpeed <= MAXIMUMSPEED && p.AverageSpeed > MINIMUMSPEED) ? (p.AverageSpeed / MAXIMUMSPEED) : ZEROSPEED));

            return histdata;
        }
    }
}
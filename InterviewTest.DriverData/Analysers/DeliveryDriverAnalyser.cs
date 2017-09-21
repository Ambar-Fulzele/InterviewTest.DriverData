using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
	// BONUS: Why internal?
	internal class DeliveryDriverAnalyser : IAnalyser
	{
        const decimal MINIMUMSPEED = 0, MAXIMUMSPEED = 30, ZEROSPEED = 0;
        TimeSpan startTime = new TimeSpan(9, 0, 0);
        TimeSpan endTime = new TimeSpan(17, 0, 0);

        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history, bool isPenalise)
		{
            HistoryAnalysis result = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.Zero,
                DriverRating = 0,
                UnDocumentedPeriod = false
            };
            
            List<HistoryAnalysis> histCollection = new List<HistoryAnalysis>();

            history.OrderBy(h => h.Start).ToList().ForEach(h =>
            {
                // Ignore anything outside of 9-5;
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

            result = AnalyserHelper.getFinalRating(histCollection);

            result.UnDocumentedPeriod = AnalyserHelper.getUndocumentedPeriod(history, startTime, endTime);

            result = AnalyserHelper.penaliseAnalyser(result, isPenalise && result.UnDocumentedPeriod);
            
            return result;
        }

        private HistoryAnalysis getValidHistoryData(Period p, TimeSpan startingTime, TimeSpan endingTime)
        {
            HistoryAnalysis histdata = new HistoryAnalysis();

            histdata.AnalysedDuration = (endingTime - startingTime);

            //Speeds between 0 and 30 map linearly to [0,1] and Speeds above 30 map to 0
            histdata.DriverRating = ((p.AverageSpeed <= MAXIMUMSPEED && p.AverageSpeed > MINIMUMSPEED) ? (p.AverageSpeed / MAXIMUMSPEED) : ZEROSPEED);

            return histdata;
        }
    }
}
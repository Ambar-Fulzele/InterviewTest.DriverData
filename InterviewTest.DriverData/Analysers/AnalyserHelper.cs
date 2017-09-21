using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace InterviewTest.DriverData.Analysers
{
    public static class AnalyserHelper
    {
        public static HistoryAnalysis getFinalRating(ICollection<HistoryAnalysis> histCollection)
        {
            try
            { 
                HistoryAnalysis histAnalysis = new HistoryAnalysis
                {
                    AnalysedDuration = TimeSpan.Zero,
                    DriverRating = 0
                };

                if (histCollection.Count > 0)
                {
                    histCollection.ToList().ForEach(h =>
                        {
                            histAnalysis.AnalysedDuration += h.AnalysedDuration;
                            histAnalysis.DriverRating += (h.AnalysedDuration.Ticks * h.DriverRating);
                        }
                    );

                    histAnalysis.DriverRating = histAnalysis.DriverRating / histAnalysis.AnalysedDuration.Ticks;
                }

                return histAnalysis;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static bool getUndocumentedPeriod(IReadOnlyCollection<Period> history, TimeSpan startTime, TimeSpan endTime)
        {
            try
            {
                bool undocumentedFlag = false;

                //check if there is undocumented period between first record and starting time
                if (startTime > TimeSpan.Zero && !undocumentedFlag
                    && history.OrderBy(h => h.Start).Where(h => h.End.TimeOfDay > startTime).ToList().First().Start.TimeOfDay > startTime)
                    undocumentedFlag = true;

                // To check undocumented period between the records
                for (int i = 1; i < history.Count && !undocumentedFlag; i++)
                {
                    if (history.OrderBy(h => h.Start).ToList()[i].End.TimeOfDay < history.OrderBy(h => h.Start).ToList()[i + 1].Start.TimeOfDay)
                        undocumentedFlag = true;
                }

                //check if there is undocumented period between last record and ending time
                if (endTime > TimeSpan.Zero && !undocumentedFlag
                    && history.OrderBy(h => h.Start).Where(h => h.Start.TimeOfDay < endTime).ToList().Last().End.TimeOfDay < endTime)
                    undocumentedFlag = true;

                return undocumentedFlag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static HistoryAnalysis penaliseAnalyser(HistoryAnalysis historyAnalysis, bool penaliseFlag)
        {
            try
            { 
                if (penaliseFlag)
                {
                    historyAnalysis.DriverRating = historyAnalysis.DriverRating / 2;
                }

                return historyAnalysis;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

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
        public static HistoryAnalysis getFinalRating(ICollection<HistoryAnalysis> histCollection, long timeInTicks)
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

                    histAnalysis.DriverRating = decimal.Round(histAnalysis.DriverRating / timeInTicks, 4);
                }

                return histAnalysis;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static bool getUndocumentedPeriod(IReadOnlyCollection<Period> history, DateTime startTime, DateTime endTime)
        {
            try
            {
                bool undocumentedFlag = false;

                if (!undocumentedFlag && history.Any(h => h.End > startTime) == false)
                    undocumentedFlag = true;

                if (!undocumentedFlag && history.Any(h => h.Start < endTime) == false)
                    undocumentedFlag = true;

                //check if there is undocumented period between first record and starting time
                if (!undocumentedFlag
                    && history.OrderBy(h => h.Start).Where(h => h.End > startTime).ToList().First().Start > startTime)
                    undocumentedFlag = true;

                // To check undocumented period between the records
                for (int i = 0; i < history.Count-1 && !undocumentedFlag; i++)
                {
                    if (history.OrderBy(h => h.Start).ToList()[i].End < history.OrderBy(h => h.Start).ToList()[i + 1].Start)
                        undocumentedFlag = true;
                }

                //check if there is undocumented period between last record and ending time
                if (!undocumentedFlag
                    && history.OrderBy(h => h.Start).Where(h => h.Start < endTime).ToList().Last().End < endTime)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

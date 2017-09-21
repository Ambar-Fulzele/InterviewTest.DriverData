using System;
using System.Collections.Generic;
using System.Linq;
using InterviewTest.DriverData;
using InterviewTest.DriverData.Analysers;

namespace InterviewTest.Commands
{
	public class AnalyseHistoryCommand
	{
		// BONUS: What's great about readonly?
		private readonly IAnalyser _analyser;

        private bool _penalise = false;

        public AnalyseHistoryCommand(IReadOnlyCollection<string> arguments)
		{
			var analysisType = arguments.ElementAt(0);

            _analyser = AnalyserLookup.GetAnalyser(analysisType);
            
            if (arguments.Count > 1 && arguments.ElementAt(1) != null && arguments.ElementAt(1).ToLower() == "true")
                _penalise = true;
		}

		public void Execute()
		{
            var analysis = _analyser.Analyse(CannedDrivingData.History,_penalise);
            
            Console.Out.WriteLine($"Analysed period: {analysis.AnalysedDuration:g}");
			Console.Out.WriteLine($"Driver rating: {analysis.DriverRating:P}");
            Console.Out.WriteLine($"Driver penalise: {_penalise}");
            Console.Out.WriteLine($"UnDocumented Period: {analysis.UnDocumentedPeriod}");
        }
	}
}

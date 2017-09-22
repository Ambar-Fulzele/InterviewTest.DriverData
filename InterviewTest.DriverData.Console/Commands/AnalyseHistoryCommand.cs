using System;
using System.Collections.Generic;
using System.Linq;
using InterviewTest.DriverData;
using InterviewTest.DriverData.Analysers;
using System.IO;

namespace InterviewTest.Commands
{
    public class AnalyseHistoryCommand
    {
        // BONUS: What's great about readonly?
        private readonly IAnalyser _analyser;

        private string _drivingDataFilePath = string.Empty;

        private bool _penalise = false;

        public AnalyseHistoryCommand(IReadOnlyCollection<string> arguments)
        {
            var analysisType = arguments.ElementAt(0);

            if (arguments.Count > 1 && arguments.ElementAt(1) != null)
                _drivingDataFilePath = arguments.ElementAt(1);

            _analyser = AnalyserLookup.GetAnalyser(analysisType);

            if (arguments.Count > 2 && arguments.ElementAt(2) != null && arguments.ElementAt(2).ToLower() == "true")
                _penalise = true;
        }

        public void Execute()
        {
            if (string.IsNullOrEmpty(_drivingDataFilePath) || !File.Exists(_drivingDataFilePath))
                throw new FileNotFoundException("Filepath is Empth or does not exist");

            //var analysis = _analyser.Analyse(CannedDrivingData.History, _penalise);
            var analysis = _analyser.Analyse(CannedDrivingData.GetDrivingData(_drivingDataFilePath), _penalise);

            Console.Out.WriteLine($"Analysed period: {analysis.AnalysedDuration:g}");
            Console.Out.WriteLine($"Driver rating: {analysis.DriverRating:P}");
            Console.Out.WriteLine($"Driver penalise: {_penalise}");
            Console.Out.WriteLine($"UnDocumented Period: {analysis.UnDocumentedPeriod}");
        }
    }
}
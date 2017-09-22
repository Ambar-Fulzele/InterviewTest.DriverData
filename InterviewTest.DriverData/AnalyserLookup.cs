using System;
using InterviewTest.DriverData.Analysers;
using System.Collections.Generic;

namespace InterviewTest.DriverData
{
	public static class AnalyserLookup
	{
        public interface ICreateAnalyser
        {
            IAnalyser CreateAnalyser();
        }

        public class DeliveryDriver : ICreateAnalyser
        {
            public IAnalyser CreateAnalyser()
            {
                return new DeliveryDriverAnalyser();
            }
        }

        public class FormulaOne : ICreateAnalyser
        {
            public IAnalyser CreateAnalyser()
            {
                return new FormulaOneAnalyser();
            }
        }

        public class Friendly : ICreateAnalyser
        {
            public IAnalyser CreateAnalyser()
            {
                return new FriendlyAnalyser();
            }
        }

        public class GetawayDriver : ICreateAnalyser
        {
            public IAnalyser CreateAnalyser()
            {
                return new GetawayDriverAnalyser();
            }
        }

        private static Dictionary<string, Func<IAnalyser>> analyserType = new Dictionary<string, Func<IAnalyser>>()
        {
            {"DeliveryDriver", () => { return new DeliveryDriver().CreateAnalyser(); }},
            {"FormulaOne", () => { return new FormulaOne().CreateAnalyser(); }},
            {"Friendly", () => { return new Friendly().CreateAnalyser(); }},
            {"GetawayDriver", () => {return new GetawayDriver().CreateAnalyser(); } }
        };
        
        public static IAnalyser GetAnalyser(string type)
		{
            if (analyserType.ContainsKey(type))
                return analyserType[type]();
            else
                throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised analyser type");            
        }
	}
}

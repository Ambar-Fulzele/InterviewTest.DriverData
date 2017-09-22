using System;
using System.Diagnostics;

namespace InterviewTest.DriverData
{
	[DebuggerDisplay("{_DebuggerDisplay,nq}")]
	public class Period
	{
        // BONUS: What's the difference between DateTime and DateTimeOffset?
        // DateTimeOffset provides a greater degree of time zone awareness than the DateTime structure
        // DateTime - representing time relative to some place in particular
        // DateTimeOffset - represents absolute time universal for everyone

        public DateTimeOffset Start;
		public DateTimeOffset End;

        // BONUS: What's the difference between decimal and double?
        // Double - 64 bit(15-16 digits) & Decimal - 128 bit(28-29 significant digits)
        // performance wise Decimals are slower than double 
        // Decimal can 100% accurately represent any number within the precision of the decimal format


        public decimal AverageSpeed;

		private string _DebuggerDisplay => $"{Start:t} - {End:t}: {AverageSpeed}";
	}
}

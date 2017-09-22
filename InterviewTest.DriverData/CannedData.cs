using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace InterviewTest.DriverData
{
	public static class CannedDrivingData
	{
		private static readonly DateTimeOffset _day = new DateTimeOffset(2016, 10, 13, 0, 0, 0, 0, TimeSpan.Zero);

		// BONUS: What's so great about IReadOnlyCollections?
		public static readonly IReadOnlyCollection<Period> History = new[]
		{
			new Period
			{
				Start = _day + new TimeSpan(0, 0, 0),
				End = _day + new TimeSpan(8, 54, 0),
				AverageSpeed = 0m
			},
			new Period
			{
				Start = _day + new TimeSpan(8, 54, 0),
				End = _day + new TimeSpan(9, 28, 0),
				AverageSpeed = 28m
			},
			new Period
			{
				Start = _day + new TimeSpan(9, 28, 0),
				End = _day + new TimeSpan(9, 35, 0),
				AverageSpeed = 33m
			},
			new Period
			{
				Start = _day + new TimeSpan(9, 50, 0),
				End = _day + new TimeSpan(12, 35, 0),
				AverageSpeed = 25m
			},
			new Period
			{
				Start = _day + new TimeSpan(12, 35, 0),
				End = _day + new TimeSpan(13, 30, 0),
				AverageSpeed = 0m
			},
			new Period
			{
				Start = _day + new TimeSpan(13, 30, 0),
				End = _day + new TimeSpan(19, 12, 0),
				AverageSpeed = 29m
			},
			new Period
			{
				Start = _day + new TimeSpan(19, 12, 0),
				End = _day + new TimeSpan(24, 0, 0),
				AverageSpeed = 0m
			}
		};

        public static List<Period> GetDrivingData(string filepath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DrivingData));

            FileStream fs = new FileStream(filepath, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            DrivingData drivingDataObj;

            drivingDataObj = (DrivingData)serializer.Deserialize(reader);

            fs.Close();

            List<Period> periodList = new List<Period>();

            DateTimeOffset dayObj = new DateTimeOffset( drivingDataObj.GSTDate.Year,
                                                        drivingDataObj.GSTDate.Month,
                                                        drivingDataObj.GSTDate.Day,
                                                        drivingDataObj.GSTDate.Hours,
                                                        drivingDataObj.GSTDate.Minutes,
                                                        drivingDataObj.GSTDate.Seconds,
                                                        new TimeSpan(   drivingDataObj.GSTDate.OffsetTime.Hours,
                                                                        drivingDataObj.GSTDate.OffsetTime.Minutes,
                                                                        drivingDataObj.GSTDate.OffsetTime.Seconds
                                                                    )
                                                        );

            foreach(DrivingDataPeriod dp in drivingDataObj.Period)
            {
                Period p = new Period();

                p.Start = dayObj + new TimeSpan(dp.StartTime.Hours, dp.StartTime.Minutes, dp.StartTime.Seconds);
                p.End = dayObj + new TimeSpan(dp.EndTime.Hours, dp.EndTime.Minutes, dp.EndTime.Seconds);
                p.AverageSpeed = dp.AverageSpeed;

                periodList.Add(p);
            }
                
            return periodList; ; 
        }
	}
}

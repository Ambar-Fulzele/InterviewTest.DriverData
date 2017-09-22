using System;
using System.Xml.Serialization;

namespace InterviewTest.DriverData
{
    public class DrivingData
    {

        private DrivingDataGSTDate gSTDateField;

        private DrivingDataPeriod[] periodField;

        /// <remarks/>
        public DrivingDataGSTDate GSTDate
        {
            get
            {
                return this.gSTDateField;
            }
            set
            {
                this.gSTDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Period")]
        public DrivingDataPeriod[] Period
        {
            get
            {
                return this.periodField;
            }
            set
            {
                this.periodField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DrivingDataGSTDate
    {

        private int yearField;

        private int monthField;

        private int dayField;

        private int hoursField;

        private int minutesField;

        private int secondsField;

        private DrivingDataGSTDateOffsetTime offsetTimeField;

        /// <remarks/>
        public int Year
        {
            get
            {
                return this.yearField;
            }
            set
            {
                this.yearField = value;
            }
        }

        /// <remarks/>
        public int Month
        {
            get
            {
                return this.monthField;
            }
            set
            {
                this.monthField = value;
            }
        }

        /// <remarks/>
        public int Day
        {
            get
            {
                return this.dayField;
            }
            set
            {
                this.dayField = value;
            }
        }

        /// <remarks/>
        public int Hours
        {
            get
            {
                return this.hoursField;
            }
            set
            {
                this.hoursField = value;
            }
        }

        /// <remarks/>
        public int Minutes
        {
            get
            {
                return this.minutesField;
            }
            set
            {
                this.minutesField = value;
            }
        }

        /// <remarks/>
        public int Seconds
        {
            get
            {
                return this.secondsField;
            }
            set
            {
                this.secondsField = value;
            }
        }

        /// <remarks/>
        public DrivingDataGSTDateOffsetTime OffsetTime
        {
            get
            {
                return this.offsetTimeField;
            }
            set
            {
                this.offsetTimeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DrivingDataGSTDateOffsetTime
    {

        private int hoursField;

        private int minutesField;

        private int secondsField;

        /// <remarks/>
        public int Hours
        {
            get
            {
                return this.hoursField;
            }
            set
            {
                this.hoursField = value;
            }
        }

        /// <remarks/>
        public int Minutes
        {
            get
            {
                return this.minutesField;
            }
            set
            {
                this.minutesField = value;
            }
        }

        /// <remarks/>
        public int Seconds
        {
            get
            {
                return this.secondsField;
            }
            set
            {
                this.secondsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DrivingDataPeriod
    {

        private DrivingDataPeriodStartTime startTimeField;

        private DrivingDataPeriodEndTime endTimeField;

        private decimal averageSpeedField;

        /// <remarks/>
        public DrivingDataPeriodStartTime StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
            }
        }

        /// <remarks/>
        public DrivingDataPeriodEndTime EndTime
        {
            get
            {
                return this.endTimeField;
            }
            set
            {
                this.endTimeField = value;
            }
        }

        /// <remarks/>
        public decimal AverageSpeed
        {
            get
            {
                return this.averageSpeedField;
            }
            set
            {
                this.averageSpeedField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DrivingDataPeriodStartTime
    {

        private int hoursField;

        private int minutesField;

        private int secondsField;

        /// <remarks/>
        public int Hours
        {
            get
            {
                return this.hoursField;
            }
            set
            {
                this.hoursField = value;
            }
        }

        /// <remarks/>
        public int Minutes
        {
            get
            {
                return this.minutesField;
            }
            set
            {
                this.minutesField = value;
            }
        }

        /// <remarks/>
        public int Seconds
        {
            get
            {
                return this.secondsField;
            }
            set
            {
                this.secondsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DrivingDataPeriodEndTime
    {

        private int hoursField;

        private int minutesField;

        private int secondsField;

        /// <remarks/>
        public int Hours
        {
            get
            {
                return this.hoursField;
            }
            set
            {
                this.hoursField = value;
            }
        }

        /// <remarks/>
        public int Minutes
        {
            get
            {
                return this.minutesField;
            }
            set
            {
                this.minutesField = value;
            }
        }

        /// <remarks/>
        public int Seconds
        {
            get
            {
                return this.secondsField;
            }
            set
            {
                this.secondsField = value;
            }
        }
    }

}
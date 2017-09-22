using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class FormulaOneAnalyserTests
	{
        #region TestCases

        [Test]
        // Test to get correct values for the canned data
        public void ShouldYieldCorrectValuesForNullCollection()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            var actualResult = new FormulaOneAnalyser().Analyse(null, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        // Test to get correct values for the canned data
        public void ShouldYieldCorrectValues()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(10, 3, 0),
                DriverRating = 0.1231m
            };

            var actualResult = new FormulaOneAnalyser().Analyse(CannedDrivingData.History, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the zero period
        public void ShouldYieldCorrectValuesForZeroPeriod()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 0, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 0, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 20m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the zero speed
        public void ShouldYieldCorrectValuesForZeroSpeed()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 13, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 14, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the zero period and zero speed
        public void ShouldYieldCorrectValuesForZeroPeriodSpeed()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 0, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 0, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the full period and full speed
        public void ShouldYieldCorrectValuesForFullPeriodSpeed()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 1m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 13, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 14, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 200m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
        
        [Test]
        //Test for the data available with first zero speed value
        public void ShouldYieldCorrectValuesWithFirstZeroSpeedPeriod()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(3, 30, 0),
                DriverRating = 0.1500m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 3, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 30m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
        
        [Test]
        //Test for the data available with last zero speed value
        public void ShouldYieldCorrectValuesWithLastZeroSpeedPeriod()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(2, 30, 0),
                DriverRating = 0.1500m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 17, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 20, 00, 0, TimeSpan.Zero),
                        AverageSpeed = 30m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 20, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 21, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available with first & last zero speed value
        public void ShouldYieldCorrectValuesWithFirstLastZeroSpeedPeriod()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(2, 30, 0),
                DriverRating = 0.1500m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 12, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 17, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    },new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 17, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 20, 00, 0, TimeSpan.Zero),
                        AverageSpeed = 30m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 20, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 21, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }


        #endregion

        #region AboveSpeedLimit

        // Test for speed more than limit - 200
        [Test]
        //Test for the zero period & speed more than limit
        public void ShouldYieldCorrectValuesForZeroPeriodSpeedAboveLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 0, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 0, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 250m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the full period & speed more than limit
        public void ShouldYieldCorrectValuesForSpeedAboveLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 1m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 13, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 14, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 250m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
                
        [Test]
        //Test for the data available before start time & speed more than limit
        public void ShouldYieldCorrectValuesWithFirstZeroSpeedAboveLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(3, 30, 0),
                DriverRating = 1m
            };

            var data = new[]
             {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 3, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 250
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
        
        [Test]
        //Test for the data available after end time & speed more than limit
        public void ShouldYieldCorrectValuesWithLastZeroSpeedAboveLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(2, 0, 0),
                DriverRating = 1m
            };

            var data = new[]
             {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 3, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 250m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
        
        [Test]
        //Test for the data overlap before start time & speed more than limit
        public void ShouldYieldCorrectValuesWithFirstLastZeroSpeedAboveLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(2, 00, 0),
                DriverRating = 1m
            };

            var data = new[]
             {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 3, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 7, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 250m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 8, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        #endregion AboveSpeedLimit


        // Test for Penalise flag true
        #region Penalise       

        [Test]
        // Test to get correct values for the canned data with Penalise flag true
        public void ShouldYieldCorrectValuesForNullDataWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            var actualResult = new FormulaOneAnalyser().Analyse(null, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        // Test to get correct values for the canned data
        public void ShouldYieldCorrectValuesWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(10, 3, 0),
                DriverRating = 0.0615m
            };

            var actualResult = new FormulaOneAnalyser().Analyse(CannedDrivingData.History, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the zero period
        public void ShouldYieldCorrectValuesForZeroPeriodWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 0, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 0, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 20m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the zero speed
        public void ShouldYieldCorrectValuesForZeroSpeedWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 13, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 14, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the zero period and zero speed
        public void ShouldYieldCorrectValuesForZeroPeriodSpeedWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 0, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 0, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the full period and full speed
        public void ShouldYieldCorrectValuesForFullPeriodSpeedWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 0.5m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 13, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 14, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 200m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available with first zero speed value
        public void ShouldYieldCorrectValuesWithFirstZeroSpeedPeriodWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(2, 30, 0),
                DriverRating = 0.0750m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 3, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 5, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 8, 00, 0, TimeSpan.Zero),
                        AverageSpeed = 30m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available with last zero speed value
        public void ShouldYieldCorrectValuesWithLastZeroSpeedPeriodWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(2, 30, 0),
                DriverRating = 0.0750m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 17, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 20, 00, 0, TimeSpan.Zero),
                        AverageSpeed = 30m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 20, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 21, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available with first & last zero speed value
        public void ShouldYieldCorrectValuesWithFirstLastZeroSpeedPeriodWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(2, 30, 0),
                DriverRating = 0.0750m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 12, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 17, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    },new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 17, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 20, 00, 0, TimeSpan.Zero),
                        AverageSpeed = 30m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 20, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 21, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }


        #region AboveSpeedLimitWithPenalise

        // Test for speed more than limit - 200
        [Test]
        //Test for the zero period & speed more than limit
        public void ShouldYieldCorrectValuesForZeroPeriodSpeedAboveLimitWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 0, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 0, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 250m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the full period & speed more than limit
        public void ShouldYieldCorrectValuesForSpeedAboveLimitWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 0.5m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 13, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 14, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 250m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
        
        [Test]
        //Test for the data available before start time & speed more than limit
        public void ShouldYieldCorrectValuesWithFirstZeroSpeedAboveLimitWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(3, 30, 0),
                DriverRating = 0.5m
            };

            var data = new[]
             {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 3, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 250
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available after end time & speed more than limit
        public void ShouldYieldCorrectValuesWithLastZeroSpeedAboveLimitWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(2, 0, 0),
                DriverRating = 0.5m
            };

            var data = new[]
             {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 3, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 250m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap before start time & speed more than limit
        public void ShouldYieldCorrectValuesWithFirstLastZeroSpeedAboveLimitWithPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(2, 00, 0),
                DriverRating = 0.5m
            };

            var data = new[]
             {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 3, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 7, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 250m
                    },
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 8, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 0m
                    }
                };

            var actualResult = new FormulaOneAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        #endregion AboveSpeedLimitWithPenalise
        
        #endregion
    }
}

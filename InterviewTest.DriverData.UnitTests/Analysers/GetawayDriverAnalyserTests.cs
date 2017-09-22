using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class GetawayDriverAnalyserTests
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

            var actualResult = new GetawayDriverAnalyser().Analyse(null, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        // Test to get correct values for the canned data
        public void ShouldYieldCorrectValues()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.FromHours(1),
                DriverRating = 0.1813m
            };

            var actualResult = new GetawayDriverAnalyser().Analyse(CannedDrivingData.History, false);

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

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

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

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

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

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

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
                        AverageSpeed = 80m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available before start time
        public void ShouldYieldCorrectValuesForOutofBeginningPeriod()
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
                        Start = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 30m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available after end time
        public void ShouldYieldCorrectValuesForOutofEndingPeriod()
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
                        Start = new DateTimeOffset(2016, 10, 13, 17, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 20, 00, 0, TimeSpan.Zero),
                        AverageSpeed = 30m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap before start time
        public void ShouldYieldCorrectValuesForOverLaplingStartPeriod()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 30, 0),
                DriverRating = 0.1875m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 12, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 13, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 30m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap after end time
        public void ShouldYieldCorrectValuesForOverLaplingEndPeriod()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 30, 0),
                DriverRating = 0.2500m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 13, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 19, 12, 0, TimeSpan.Zero),
                        AverageSpeed = 40m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap before start time and after end time
        public void ShouldYieldCorrectValuesForOverLaplingStartEndPeriod()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 0.875m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 19, 12, 0, TimeSpan.Zero),
                        AverageSpeed = 70m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        #endregion

        #region AboveSpeedLimit

        // Test for speed more than limit - 80
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
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

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
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available before start time & speed more than limit
        public void ShouldYieldCorrectValuesForOutofBeginningPeriodSpeedAboveLimit()
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
                        Start = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available after end time & speed more than limit
        public void ShouldYieldCorrectValuesForOutofEndingPeriodSpeedAboveLimit()
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
                        Start = new DateTimeOffset(2016, 10, 13, 17, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 20, 00, 0, TimeSpan.Zero),
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap before start time & speed more than limit
        public void ShouldYieldCorrectValuesForOverLaplingStartPeriodSpeedAboveLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 30, 0),
                DriverRating = 0.5000m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 12, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 13, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap after end time & speed more than limit
        public void ShouldYieldCorrectValuesForOverLaplingEndPeriodSpeedAboveLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 30, 0),
                DriverRating = 0.5000m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 13, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 14, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap before start time and after end time & speed more than limit
        public void ShouldYieldCorrectValuesForOverLaplingStartEndPeriodSpeedAboveLimit()
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
                        Start = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 19, 12, 0, TimeSpan.Zero),
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        #endregion


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

            var actualResult = new GetawayDriverAnalyser().Analyse(null, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        // Test to get correct values for the canned data with Penalise flag true
        public void ShouldYieldCorrectValuesforPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 00, 0),
                DriverRating = 0.0906m
            };

            var actualResult = new GetawayDriverAnalyser().Analyse(CannedDrivingData.History, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the zero period with Penalise flag true
        public void ShouldYieldCorrectValuesForZeroPeriodforPenalise()
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

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the zero speed with Penalise flag true
        public void ShouldYieldCorrectValuesForZeroSpeedforPenalise()
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

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the zero period and zero speed with Penalise flag true
        public void ShouldYieldCorrectValuesForZeroPeriodSpeedforPenalise()
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

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the full period and full speed with Penalise flag true
        public void ShouldYieldCorrectValuesForFullPeriodSpeedforPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 0.500m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 13, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 14, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 80m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available before start time with Penalise flag true
        public void ShouldYieldCorrectValuesForOutofBeginningPeriodforPenalise()
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
                        Start = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 30m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available after end time with Penalise flag true
        public void ShouldYieldCorrectValuesForOutofEndingPeriodforPenalise()
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
                        Start = new DateTimeOffset(2016, 10, 13, 17, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 20, 00, 0, TimeSpan.Zero),
                        AverageSpeed = 30m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap before start time with Penalise flag true
        public void ShouldYieldCorrectValuesForOverLaplingStartPeriodforPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 30, 0),
                DriverRating = 0.2187m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 12, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 13, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 70m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap after end time with Penalise flag true
        public void ShouldYieldCorrectValuesForOverLaplingEndPeriodforPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 30, 0),
                DriverRating = 0.2187m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 13, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 14, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 70m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap before start time and after end time with Penalise flag true
        public void ShouldYieldCorrectValuesForOverLaplingStartEndPeriodforPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 0.43756m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 19, 12, 0, TimeSpan.Zero),
                        AverageSpeed = 70m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        #region AboveSpeedLimitWithPenalise

        [Test]
        //Test for the zero period & speed more than limit with Penalise flag true
        public void ShouldYieldCorrectValuesForZeroPeriodSpeedAboveLimitforPenalise()
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
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the full period & speed more than limit with Penalise flag true
        public void ShouldYieldCorrectValuesForSpeedAboveLimitforPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 0.5000m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 13, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 14, 0, 0, TimeSpan.Zero),
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available before start time & speed more than limit with Penalise flag true
        public void ShouldYieldCorrectValuesForOutofBeginningPeriodSpeedAboveLimitforPenalise()
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
                        Start = new DateTimeOffset(2016, 10, 13, 5, 0, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 8, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data available after end time & speed more than limit with Penalise flag true
        public void ShouldYieldCorrectValuesForOutofEndingPeriodSpeedAboveLimitforPenalise()
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
                        Start = new DateTimeOffset(2016, 10, 13, 17, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 20, 00, 0, TimeSpan.Zero),
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap before start time & speed more than limit with Penalise flag true
        public void ShouldYieldCorrectValuesForOverLaplingStartPeriodSpeedAboveLimitforPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 30, 0),
                DriverRating = 0.2500m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 12, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 13, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap after end time & speed more than limit with Penalise flag true
        public void ShouldYieldCorrectValuesForOverLaplingEndPeriodSpeedAboveLimitforPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 30, 0),
                DriverRating = 0.2500m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 13, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 14, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        //Test for the data overlap before start time and after end time & speed more than limit with Penalise flag true
        public void ShouldYieldCorrectValuesForOverLaplingStartEndPeriodSpeedAboveLimitforPenalise()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 0.5000m
            };

            var data = new[]
            {
                    new Period
                    {
                        Start = new DateTimeOffset(2016, 10, 13, 12, 30, 0, TimeSpan.Zero),
                        End = new DateTimeOffset(2016, 10, 13, 14, 30, 0, TimeSpan.Zero),
                        AverageSpeed = 100m
                    }
                };

            var actualResult = new GetawayDriverAnalyser().Analyse(data, true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
        #endregion AboveSpeedLimitWithPenalise

        #endregion

    }
}

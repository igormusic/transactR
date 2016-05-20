using System.Collections.Generic;
using TransactRules.Configuration;

namespace transactR.Tests
{
    public class Utility
    {
        public static AccountType CreateLoanGivenAccountType()
        {
            var conversionInterestPosition = new PositionType { Name = "ConversionInterest" };
            var earlyRedemptionFeePosition = new PositionType { Name = "EarlyRedemptionFee" };
            var interestAccruedPosition = new PositionType { Name = "InterestAccrued" };
            var interestCapitalizedPosition = new PositionType { Name = "InterestCapitalized" };
            var principalPosition = new PositionType { Name = "Principal" };

            var startDate = new DateType { Name = "StartDate" };
            var accrualStart = new DateType { Name = "AccrualStart" };
            var endDate = new DateType { Name = "EndDate" };

            var accrualSchedule = new ScheduleType
            {
                Name = "AccrualSchedule",
                Frequency = ScheduleFrequency.Daily,
                StartDateExpression = "StartDate",
                IntervalExpression = "1",
                EndType = ScheduleEndType.NoEnd,
                IsCalculated = true
            };

            var interestSchedule = new ScheduleType
            {
                Name = "InterestSchedule",
                Frequency = ScheduleFrequency.Monthly,
                IntervalExpression = "1",
                EndType = ScheduleEndType.EndDate,
                IsCalculated = false
            };

            var redemptionSchedule = new ScheduleType
            {
                Name = "RedemptionSchedule",
                Frequency = ScheduleFrequency.Monthly,
                IntervalExpression = "1",
                EndType = ScheduleEndType.EndDate,
                IsCalculated = false
            };

            var interestAccruedTransactionType = new TransactionType
            {
                Name = "InterestAccrued",
                HasMaximumPrecission = true,
                TransactionRules = new List<TransactionRuleType> {
                    new TransactionRuleType { PositionTypeName = interestAccruedPosition.Name, TransactionOperation = TransactionOperation.Add }
                }
            };

            var interestCapitalizedTransactionType = new TransactionType
            {
                Name = "InterestCapitalized",
                TransactionRules = new List<TransactionRuleType> {
                                new TransactionRuleType { PositionTypeName = principalPosition.Name, TransactionOperation = TransactionOperation.Add },
                                new TransactionRuleType { PositionTypeName = interestAccruedPosition.Name, TransactionOperation = TransactionOperation.Subtract },
                                new TransactionRuleType { PositionTypeName = interestCapitalizedPosition.Name, TransactionOperation = TransactionOperation.Add }
                            }
            };

            var redemptionTransactionType = new TransactionType
            {
                Name = "Redemption",
                TransactionRules = new List<TransactionRuleType> {
                            new TransactionRuleType { PositionTypeName = principalPosition.Name, TransactionOperation = TransactionOperation.Subtract }
                            }
            };

            var advanceTransactionType = new TransactionType
            {
                Name = "Advance",
                TransactionRules = new List<TransactionRuleType> {
                                new TransactionRuleType { PositionTypeName = principalPosition.Name, TransactionOperation = TransactionOperation.Add }
                            }
            };


            var accountType =
                new AccountType
                {
                    Name = "LoanGiven",
                    PositionTypes =
                    new List<PositionType> { 
                        conversionInterestPosition,
                        earlyRedemptionFeePosition,
                        interestAccruedPosition,
                        interestCapitalizedPosition,
                        principalPosition
                    },
                    TransactionTypes =
                    new List<TransactionType> { 
                        
                        new TransactionType { 
                            Name = "AdditionalAdvance", 
                            TransactionRules = new List<TransactionRuleType> {
                                new TransactionRuleType { PositionTypeName = principalPosition.Name, TransactionOperation = TransactionOperation.Add }
                            }
                        },
                        advanceTransactionType,
                        new TransactionType { 
                            Name = "ConversionInterest", 
                            TransactionRules = new List<TransactionRuleType> {
                                new TransactionRuleType { PositionTypeName = conversionInterestPosition.Name, TransactionOperation = TransactionOperation.Add }
                            }
                        },

                        new TransactionType { 
                            Name = "EarlyRedemptionFee", 
                            TransactionRules = new List<TransactionRuleType> {
                                new TransactionRuleType { PositionTypeName = earlyRedemptionFeePosition.Name, TransactionOperation = TransactionOperation.Add }
                            }
                        },

                         new TransactionType { 
                            Name = "FXResultInterest", 
                            TransactionRules = new List<TransactionRuleType> {
                                new TransactionRuleType { PositionTypeName = interestAccruedPosition.Name, TransactionOperation = TransactionOperation.Add }
                            }
                        },

                         new TransactionType { 
                            Name = "FXResultPrincipal", 
                            TransactionRules = new List<TransactionRuleType> {
                                new TransactionRuleType { PositionTypeName = principalPosition.Name, TransactionOperation = TransactionOperation.Add }
                            }
                        },
                        interestAccruedTransactionType,

                        interestCapitalizedTransactionType,

                         new TransactionType { 
                            Name = "InterestPayment", 
                            TransactionRules = new List<TransactionRuleType> {
                                new TransactionRuleType { PositionTypeName = interestAccruedPosition.Name, TransactionOperation = TransactionOperation.Subtract }
                            }
                        },

                        redemptionTransactionType
                    },

                    AmountTypes = new List<AmountType> { 
                        new AmountType { Name = "RedemptionAmount" },
                        new AmountType { Name = "AdditionalAdvanceAmount" },
                        new AmountType { Name = "ConversionInterestAmount" },
                        new AmountType { Name = "AdvanceAmount" },
                     },

                    DateTypes = new List<DateType> { 
                        accrualStart,
                        startDate,
                        endDate,
                    },

                    ScheduleTypes = new List<ScheduleType>(){
                        accrualSchedule,
                        interestSchedule,
                        redemptionSchedule
                    },

                    RateTypes = new List<RateType>() { 
                        new RateType { Name = "InterestRate" }
                    },

                    OptionTypes = new List<OptionType>() { 
                        new OptionType {Name = "AccrualOption",
                                        OptionListExpression = "TransactRules.Calculations.AccrualCalculation.AccrualOptions()"}
                    }
                    ,
                    ScheduledTransactions = new List<ScheduledTransaction>() {
                         new ScheduledTransaction {
                             AmountExpression = "AdvanceAmount",
                             DateTypeName = startDate.Name,
                             Timing = ScheduledTransactionTiming.StartOfDay,
                             TransactionTypeName = advanceTransactionType.Name,
                             Sequence =1
                        },
                        new ScheduledTransaction {
                             AmountExpression = "TransactRules.Calculations.AccrualCalculation.InterestAccrued(accrualOption: AccrualOption, principal: Principal, rate: InterestRate, valueDate:  TransactRules.Core.Utilities.SessionState.Current.ValueDate)",
                             ScheduleTypeName = accrualSchedule.Name,
                             Timing = ScheduledTransactionTiming.EndOfDay,
                             TransactionTypeName = interestAccruedTransactionType.Name,
                             Sequence =1
                        },
                        new ScheduledTransaction {
                             AmountExpression = "InterestAccrued",
                             ScheduleTypeName = interestSchedule.Name,
                             Timing = ScheduledTransactionTiming.EndOfDay,
                             TransactionTypeName = interestCapitalizedTransactionType.Name,
                             Sequence =2
                        }
                    },

                    InstalmentTypes = new List<InstalmentType>() { 
                        new InstalmentType {
                             InterestCapitalizedPositionName = interestCapitalizedPosition.Name,
                             InterestAccruedPositionName = interestAccruedPosition.Name,
                             Name ="Redemptions",
                             PrincipalPositionTypeName = principalPosition.Name,
                             ScheduleTypeName = redemptionSchedule.Name,
                             Timing = ScheduledTransactionTiming.StartOfDay,
                             TransactionTypeName = redemptionTransactionType.Name
                        }
                    }
                };

            return accountType;
        }

        public static AccountType CreateSavingsAccountType()
        {
            var currentPosition = new PositionType { Name = "Current" };
            var interestAccruedPosition = new PositionType { Name = "InterestAccrued" };

            var accountType =
                new AccountType
                {
                    Name = "SavingsAccount",
                    PositionTypes =
                    new List<PositionType> { 
                        currentPosition, 
                        interestAccruedPosition
                    },
                    TransactionTypes =
                    new List<TransactionType> { 
                        
                        new TransactionType { 
                            Name = "Deposit", 
                            TransactionRules = new List<TransactionRuleType> {
                                new TransactionRuleType { PositionTypeName = currentPosition.Name, TransactionOperation = TransactionOperation.Add }
                            }
                        },

                        new TransactionType { 
                            Name = "Withdrawal", 
                            TransactionRules =  new List<TransactionRuleType> {
                                new TransactionRuleType { PositionTypeName = currentPosition.Name, TransactionOperation = TransactionOperation.Subtract }
                            }
                        },

                        new TransactionType { 
                            Name = "IntrerestAccrued", 
                            TransactionRules = new List<TransactionRuleType> {
                                new TransactionRuleType { PositionTypeName = interestAccruedPosition.Name, TransactionOperation = TransactionOperation.Add }
                            }
                        },

                        new TransactionType { 
                            Name = "IntrerestCapitalized", 
                            TransactionRules = new List<TransactionRuleType> {
                                new TransactionRuleType { PositionTypeName = interestAccruedPosition.Name, TransactionOperation = TransactionOperation.Subtract },
                                new TransactionRuleType { PositionTypeName = currentPosition.Name, TransactionOperation = TransactionOperation.Add }
                            }
                        },
                    }
                };

            return accountType;
        }

        //public static Calendar CreateEuroZoneCalendar()
        //{
        //    return new Calendar
        //    {
        //        Name ="Euro Zone",
        //        Holidays = new List<HolidayDate> { 
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2000-04-21T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2000-04-24T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2000-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2000-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2000-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2001-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2001-04-13T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2001-04-16T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2001-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2001-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2001-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2002-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2002-03-29T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2002-04-01T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2002-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2002-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2002-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2003-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2003-04-18T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2003-04-21T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2003-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2003-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2003-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2004-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2004-04-09T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2004-04-12T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2005-03-25T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2005-03-28T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2005-12-26T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2006-04-14T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2006-04-17T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2006-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2006-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2006-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2007-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2007-04-06T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2007-04-09T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2007-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2007-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2007-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2008-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2008-03-21T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2008-03-24T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2008-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2008-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2008-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2009-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2009-04-10T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2009-04-13T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2009-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2009-12-25T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2010-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2010-04-02T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2010-04-05T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2011-04-22T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2011-04-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2011-12-26T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2012-04-06T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2012-04-09T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2012-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2012-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2012-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2013-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2013-03-29T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2013-04-01T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2013-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2013-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2013-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2014-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2014-04-18T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2014-04-21T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2014-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2014-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2014-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2015-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2015-04-03T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2015-04-06T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2015-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2015-12-25T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2016-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2016-03-25T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2016-03-28T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2016-12-26T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2017-04-14T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2017-04-17T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2017-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2017-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2017-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2018-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2018-03-30T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2018-04-02T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2018-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2018-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2018-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2019-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2019-04-19T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2019-04-22T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2019-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2019-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2019-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2020-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2020-04-10T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2020-04-13T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2020-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2020-12-25T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2021-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2021-04-02T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2021-04-05T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2022-04-15T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2022-04-18T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2022-12-26T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2023-04-07T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2023-04-10T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2023-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2023-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2023-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2024-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2024-03-29T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2024-04-01T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2024-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2024-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2024-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2025-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2025-04-18T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2025-04-21T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2025-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2025-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2025-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2026-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2026-04-03T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2026-04-06T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2026-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2026-12-25T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2027-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2027-03-26T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2027-03-29T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2028-04-14T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2028-04-17T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2028-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2028-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2028-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (01JAN)", Value = Convert.ToDateTime("2029-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2029-03-30T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2029-04-02T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY (01 MAY)", Value = Convert.ToDateTime("2029-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2029-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2029-12-26T00:00:00")},
        //                    new HolidayDate { Description ="NEW YEARS DAY (1 JAN)", Value = Convert.ToDateTime("2030-01-01T00:00:00")},
        //                    new HolidayDate { Description ="GOOD FRIDAY", Value = Convert.ToDateTime("2030-04-19T00:00:00")},
        //                    new HolidayDate { Description ="EASTER MONDAY", Value = Convert.ToDateTime("2030-04-22T00:00:00")},
        //                    new HolidayDate { Description ="LABOUR DAY", Value = Convert.ToDateTime("2030-05-01T00:00:00")},
        //                    new HolidayDate { Description ="CHRISTMAS DAY (25 DEC)", Value = Convert.ToDateTime("2030-12-25T00:00:00")},
        //                    new HolidayDate { Description ="BOXING DAY (26 DEC)", Value = Convert.ToDateTime("2030-12-26T00:00:00")}
        //        }
        //    };
        //}
    }
}

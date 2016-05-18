namespace TransactRules.Configuration
{
    public enum BusinessDayCalculation
    {
        AnyDay,
        NextBusinessDay,
        PreviousBusinessDay,
        ClosestBusinessDayOrNext,
        NextBusinessDayThisMonthOrPrevious
    }
}

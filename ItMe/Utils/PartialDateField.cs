namespace ItMe.Utils
{
    public enum PartialDateField
    {
        None,

        /// <summary>
        /// A literal that will be omitted if the next non-literal field is absent
        /// </summary>
        PrefixLiteral,

        /// <summary>
        /// A literal that will be omitted if the previous non-literal field is absent
        /// </summary>
        SuffixLiteral,

        /// <summary>
        /// Outputs the year component of a PartialDate
        /// </summary>
        Year,

        /// <summary>
        /// Outputs the month component of a PartialDate
        /// </summary>
        Month,

        /// <summary>
        /// Outputs the day component of a PartialDate
        /// </summary>
        Day
    }
}
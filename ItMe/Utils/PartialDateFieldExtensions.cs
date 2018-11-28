namespace ItMe.Utils
{
    public static class PartialDateFieldExtensions
    {
        public static bool IsDateField(this PartialDateField field)
        {
            return field == PartialDateField.Year || field == PartialDateField.Month || field == PartialDateField.Day;
        }

        public static bool IsLiteralField(this PartialDateField field)
        {
            return field == PartialDateField.PrefixLiteral || field == PartialDateField.SuffixLiteral;
        }
    }
}
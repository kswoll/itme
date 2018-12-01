using System;

namespace ItMe.Utils
{
    public struct PartialDateFormat
    {
        public PartialDateField Field { get; }
        public string Format { get; }

        public PartialDateFormat(PartialDateField field) : this()
        {
            if (field == PartialDateField.PrefixLiteral || field == PartialDateField.SuffixLiteral || field == PartialDateField.None)
                throw new ArgumentException(nameof(field));

            Field = field;
            switch (field)
            {
                case PartialDateField.Year:
                    Format = "yyyy";
                    break;
                case PartialDateField.Month:
                    Format = "MMMM";
                    break;
                case PartialDateField.Day:
                    Format = "d";
                    break;
            }
        }

        public PartialDateFormat(string literal) : this()
        {
            Format = literal;
        }

        public PartialDateFormat(PartialDateField field, string format) : this()
        {
            if (field == PartialDateField.None)
                throw new ArgumentException(nameof(field));

            Field = field;
            Format = format;
        }

        public static implicit operator PartialDateFormat(PartialDateField field)
        {
            return new PartialDateFormat(field);
        }

        public static implicit operator PartialDateFormat(string literal)
        {
            return Suffix(literal);
        }

        public static PartialDateFormat Prefix(string prefixLiteral)
        {
            return new PartialDateFormat(PartialDateField.PrefixLiteral, prefixLiteral);
        }

        public static PartialDateFormat Suffix(string suffixLiteral)
        {
            return new PartialDateFormat(PartialDateField.SuffixLiteral, suffixLiteral);
        }
    }
}
using System;
using System.Text;

namespace ItMe.Utils
{
    public struct PartialDate
    {
        public int? Year { get; }
        public int? Month { get; }
        public int? Day { get; }

        public PartialDate(int? year, int? month, int? day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public string Encode()
        {
            return $"{Year?.ToString() ?? "?"}-{Month?.ToString() ?? "?"}-{Day?.ToString() ?? "?"}";
        }

        public static PartialDate Decode(string value)
        {
            if (string.IsNullOrEmpty(value))
                return default;

            var parts = value.Split('-');
            int? ParsePart(string s) => s == "?" ? (int?)null : int.Parse(s);
            return new PartialDate(ParsePart(parts[0]), ParsePart(parts[1]), ParsePart(parts[2]));
        }

        public override string ToString()
        {
            return ToString(PartialDateField.Month, " ", PartialDateField.Day, PartialDateFormat.Suffix(", "), PartialDateField.Year);
        }

        public int? this[PartialDateField field]
        {
            get
            {
                switch (field)
                {
                    case PartialDateField.Year:
                        return Year;
                    case PartialDateField.Month:
                        return Month;
                    case PartialDateField.Day:
                        return Day;
                    default:
                        throw new ArgumentException("Only date fields are allowed in the indexer", nameof(field));
                }
            }
        }

        public string ToString(params PartialDateFormat[] formats)
        {
            var builder = new StringBuilder();
            var date = new DateTime(Year ?? 1, Month ?? 1, Day ?? 1);
            for (var i = 0; i < formats.Length; i++)
            {
                var format = formats[i];
                switch (format.Field)
                {
                    case PartialDateField.PrefixLiteral:
                        if (i == formats.Length - 1 || !formats[i + 1].Field.IsDateField())
                            throw new ArgumentException("A prefix literal must come directly before a date field", nameof(formats));

                        var prefixValue = this[formats[i + 1].Field];
                        if (prefixValue != null)
                            builder.Append(format.Format);
                        break;
                    case PartialDateField.SuffixLiteral:
                        if (i == 0 || !formats[i - 1].Field.IsDateField())
                            throw new ArgumentException("A suffix literal must come directly after a date field");

                        var suffixValue = this[formats[i - 1].Field];
                        if (suffixValue != null)
                            builder.Append(format.Format);
                        break;
                    case PartialDateField.Year:
                    case PartialDateField.Month:
                    case PartialDateField.Day:
                        var value = date.ToString(format.Format);
                        builder.Append(value);
                        break;
                }
            }

            return builder.ToString();
        }
    }
}
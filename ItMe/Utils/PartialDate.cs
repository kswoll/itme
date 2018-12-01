using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItMe.Utils
{
    public struct PartialDate
    {
        public int? Year { get; }
        public int? Month { get; }
        public int? Day { get; }

        private static readonly PartialDateFormat[] DefaultFormat =
        {
            PartialDateField.Month, " ", PartialDateField.Day, ", ", PartialDateField.Year
        };

        private static readonly PartialDateFormat[] YearFormat = { PartialDateField.Year };

        private static readonly PartialDateFormat[] MonthYearFormat =
        {
            PartialDateField.Month, " ", PartialDateField.Year
        };

        public PartialDate(int? year = null, int? month = null, int? day = null)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public static PartialDate Parse(string s)
        {
            return Parse(s, new[] { DefaultFormat, MonthYearFormat, YearFormat });
        }

        public static PartialDate Parse(string s, PartialDateFormat[][] patterns)
        {
            foreach (var pattern in patterns)
            {
                var literals = new Queue<PartialDateFormat>(pattern.Where(x => x.Field.IsLiteralField()));
                foreach (var format in pattern)
                {
                    if (format.Field.IsDateField())
                    {
                        var nextLiteral = literals.Dequeue();

                    }
                }
            }
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
            return ToString(DefaultFormat);
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
            formats = formats.Length == 0 ? DefaultFormat : formats;

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
                        var value = this[format.Field];
                        if (value != null)
                        {
                            var formattedValue = date.ToString(" " + format.Format).Trim();
                            builder.Append(formattedValue);
                        }
                        break;
                }
            }

            return builder.ToString();
        }

        public bool Equals(PartialDate other)
        {
            return Year == other.Year && Month == other.Month && Day == other.Day;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is PartialDate other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Year.GetHashCode();
                hashCode = (hashCode * 397) ^ Month.GetHashCode();
                hashCode = (hashCode * 397) ^ Day.GetHashCode();
                return hashCode;
            }
        }
    }
}
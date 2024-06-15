namespace BookShop.PostgresSql.Constants;

/// <summary>
///     Represent set of constant.
/// </summary>
internal static class CommonConstant
{
    internal static class DataType
    {
        internal const string TIMESTAMPTZ = "TIMESTAMPTZ";

        internal const string TEXT = "TEXT";

        internal const string MONEY = "MONEY";

        /// <summary>
        ///     Nvarchar datatype resolver.
        /// </summary>
        internal static class VarcharGenerator
        {
            private static readonly Dictionary<int, string> _storage = [];
            private const string VarcharDataTypeName = "VARCHAR";

            /// <summary>
            ///     Generate Varchar datatype with given length.
            /// </summary>
            /// <param name="length">
            ///     Length of Varchar.
            /// </param>
            /// <returns>
            ///     The old instance if it is already existed
            ///     or the new one.
            /// </returns>
            /// <remarks>
            ///     The extension cannot generate max length.
            /// </remarks>
            internal static string Get(int length)
            {
                if (_storage.TryGetValue(key: length, value: out var value))
                {
                    return value;
                }

                var newValue = $"{VarcharDataTypeName}({length})";

                _storage.Add(key: length, value: newValue);

                return newValue;
            }
        }
    }

    internal static class DbDefaultValue
    {
        internal static readonly DateTime MIN_DATE_TIME = DateTime.MinValue.ToUniversalTime();
    }

    internal static class DbCollation
    {
        internal const string CASE_INSENSITIVE = "case_insensitive";
    }
}

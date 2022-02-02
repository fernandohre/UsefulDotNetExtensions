using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace UsefulDotNetExtensions.Core
{
    public static class DataRowsExtensions
    {
        private const short True = 1;
        private const short False = 0;

        public static bool ParseBool(this DataRow row, string columnName)
        {
            var kvp = new KeyValuePair<string, DataRow>(columnName, row);

            return ValidateAndThrowExecption(kvp, () => 
            { 
                if (bool.TryParse(row[columnName].ToString(), out var result)) return result;

                short.TryParse(row[columnName].ToString(), out var shortValue);
                if (shortValue == True) return true;
                else if (shortValue == False) return false;
                else throw new FormatException("Value cannot be parsed to boolean.");
            });
        }

        public static bool ParseBoolOrDefault(this DataRow row, string columnName, bool defaultValue)
        {
            try
            {
                return ParseBool(row, columnName);
            }
            catch 
            {
                return defaultValue;
            }
        }

        public static DateTime ParseDateTime(this DataRow row, string columnName) 
        {
            var kvp = new KeyValuePair<string, DataRow>(columnName, row);
            return ValidateAndThrowExecption(kvp, () => 
            {
                return DateTime.Parse(row[columnName].ToString());
            });
        }

        private static T ValidateAndThrowExecption<T>(KeyValuePair<string, DataRow> map, Func<T> method)
        {
            if (map.Value == null) throw new ArgumentNullException(nameof(map.Value));
            if (map.Key == null) throw new ArgumentException(nameof(map.Key));

            if (!map.Value.Table.Columns.Contains(map.Key)) throw new ArgumentException($"Column {map.Key} was not founded on the Table {map.Value.Table.TableName}.");

            try
            {
                return method();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}

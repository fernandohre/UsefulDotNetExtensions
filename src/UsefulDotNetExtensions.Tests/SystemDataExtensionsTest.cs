using System.Data;
using Xunit;
using UsefulDotNetExtensions.Core;
using System;

namespace UsefulDotNetExtensions.Tests
{
    public class SystemDataExtensionsTest
    {
        private DataRow CreateNewRow<T>(string columnName, T value)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(columnName);

            DataRow row = dataTable.NewRow();

            row[columnName] = value;

            return row;
        }
        [Fact]
        public void ParseBoolTestFalse()
        {
            string columnName = "Flag";
            var row = CreateNewRow(columnName, 0);
            var booleanVal = row.ParseBool(columnName);
            Assert.False(booleanVal);
        }
        [Fact]
        public void ParseBoolTestTrue() 
        {
            string columnName = "Flag";
            var row = CreateNewRow(columnName, 1);
            var booleanVal = row.ParseBool(columnName);
            Assert.True(booleanVal);
        }
        [Fact]
        public void ParseBoolOrDefaultTrue() 
        {
            string columnName = "IsSensitive";

            DataTable dataTable = new DataTable();

            DataRow row = dataTable.NewRow();

            Assert.True(row.ParseBoolOrDefault(columnName, true));
        }
        [Fact]
        public void ParseBoolOrDefaultFalse() 
        {
            string columnName = "IsSensitive";

            DataTable dataTable = new DataTable();

            DataRow row = dataTable.NewRow();

            Assert.False(row.ParseBoolOrDefault(columnName, false));
        }

        [Fact]
        public void ParseDateTimeTest() 
        {
            string columnName = "BirthdayDate";

            var currentDateTime = DateTime.Today;

            var row = CreateNewRow(columnName, currentDateTime);

            Assert.True(DateTime.Compare(row.ParseDateTime(columnName), currentDateTime) == 0);
        }
    }
}
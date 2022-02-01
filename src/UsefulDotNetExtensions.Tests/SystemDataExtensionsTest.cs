using System.Data;
using Xunit;
using UsefulDotNetExtensions.SystemData;

namespace UsefulDotNetExtensions.Tests
{
    public class SystemDataExtensionsTest
    {
        private DataRow CreateNewRow(string columnName, int value) 
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
    }
}
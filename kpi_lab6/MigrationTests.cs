using System;
using Xunit;
using kpi_lab6.Comparers;

namespace kpi_lab6
{
    public class MigrationTests
    {
        [Fact]
        public void TestWhereOrderByExport()
        {
            SimpleTblExporter.ExportWhereOrderBy();
            var expectedFile = @$"{Settings.Path}\Expected.csv";
            var exportFile = @$"{Settings.Path}\Export.csv";
            var differenceFile = @$"{Settings.Path}\Difference.csv";
            Comparer.Compare(expectedFile, exportFile, differenceFile);
        }

        [Fact]
        public void TestGroupByExport()
        {
            SimpleTblExporter.ExportGroupBy();
            var expectedFile = @$"{Settings.Path}\Expected2.csv";
            var exportFile = @$"{Settings.Path}\Export2.csv";
            var differenceFile = @$"{Settings.Path}\Difference2.csv";
            Comparer.Compare2(expectedFile, exportFile, differenceFile);
        }
    }
}

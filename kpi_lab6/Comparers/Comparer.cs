using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace kpi_lab6.Comparers
{
    public class Comparer
    {
        public static void Compare(string expectedFile, string exportFile, string differenceFile)
        {
            using var expectedReader = new StreamReader(expectedFile);
            using var exportReader = new StreamReader(exportFile);
            using var differenceWriter = new StreamWriter(differenceFile);

            var expected = expectedReader.ReadToEnd()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(x => x.Split(","))
                .Select(y => (Id: Int32.Parse(y[0]), Title: y[1]))
                .ToList();

            var exported = exportReader.ReadToEnd()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(x => x.Split(","))
                .Select(y => (Id: Int32.Parse(y[0]), Title: y[1]))
                .ToList();

            var difference = expected
                .Concat(exported)
                .GroupBy(x => x.Id)
                .Select(x => x.ToList())
                .Where(x => x.Count == 2)
                .Where(x => x[0].Title != x[1].Title)
                .Select(x => (Id: x[0].Id, Expected: x[0].Title, Exported: x[1].Title))
                .ToList();

            if (!difference.Any()) return;

            differenceWriter.WriteLine($"id,expected,exported,column");
            difference.ForEach(x => differenceWriter.WriteLine($"{x.Id},{x.Expected},{x.Exported},Title"));
            throw new Exception($"There is {difference.Count} differences");
        }

        public static void Compare2(string expectedFile, string exportFile, string differenceFile)
        {
            using var expectedReader = new StreamReader(expectedFile);
            using var exportReader = new StreamReader(exportFile);
            using var differenceWriter = new StreamWriter(differenceFile);

            var expected = expectedReader.ReadToEnd()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(x => x.Split(","))
                .Select(y => (BlodId: Int32.Parse(y[0]), ArticlesCount: Int32.Parse(y[2])))
                .ToList();

            var exported = exportReader.ReadToEnd()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(x => x.Split(","))
                .Select(y => (BlodId: Int32.Parse(y[0]), ArticlesCount: Int32.Parse(y[2])))
                .ToList();

            var difference = expected
                .Concat(exported)
                .GroupBy(x => x.BlodId)
                .Select(x => x.ToList())
                .Where(x => x.Count == 2)

                .Where(x => x[0].ArticlesCount != x[1].ArticlesCount)
                .Select(x => (BlodId: x[0].BlodId, Expected: x[0].ArticlesCount, Exported: x[1].ArticlesCount))
                .ToList();

            if (!difference.Any()) return;

            differenceWriter.WriteLine($"id,expected,exported,column");
            difference.ForEach(x => differenceWriter.WriteLine($"{x.BlodId},{x.Expected},{x.Exported},ArticlesCount"));
            throw new Exception($"There is {difference.Count} differences");
        }
    }
}
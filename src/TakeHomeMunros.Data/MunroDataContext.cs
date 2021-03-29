using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvHelper;

namespace TakeHomeMunros.Data
{
    public class MunroDataContext : IMunroDataContext
    {
        public MunroDataContext()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using var reader = new StreamReader($@"{directory}\munrotab_v6.2.csv");
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            Munros = csvReader.GetRecords<Munro>().ToList();
        }

        public IEnumerable<Munro> Munros { get; }
    }
}
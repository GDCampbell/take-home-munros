using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;

using TakeHomeMunrosApi.Domain;
using TakeHomeMunrosApi.Models;

namespace TakeHomeMunrosApi.DataContext
{
    public class MunroDataContext : IMunroDataContext
    {
        public MunroDataContext(IWebHostEnvironment environment, IMapper mapper)
        {
            using var reader = new StreamReader(environment.ContentRootPath + @"\Assets\munrotab_v6.2.csv");
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csvReader.GetRecords<Munro>();

            Munros = records.Select(mapper.Map<MunroModel>).ToList();
        }

        public IEnumerable<MunroModel> Munros { get; }
    }
}

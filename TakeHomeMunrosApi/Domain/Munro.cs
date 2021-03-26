using CsvHelper.Configuration.Attributes;

namespace TakeHomeMunrosApi.Domain
{
    public class Munro
    {
        [Name("Name")]
        public string Name { get; set; }
        [Name("Height (m)")]
        public double HeightInMetres { get; set; }
        [Name("Grid Ref")]
        public string GridRef { get; set; }
        [Name("Post 1997")]
        public HillCategory? Category { get; set; }
        //public string Category { get; set; }
    }
}

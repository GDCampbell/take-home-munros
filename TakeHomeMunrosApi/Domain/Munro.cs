using CsvHelper.Configuration.Attributes;

namespace TakeHomeMunrosApi.Domain
{
    public class Munro
    {
        [Name("Running No")]
        public int RunningNumber { get; set; }
        [Name("DoBIH Number")]
        public int BritishAndIrishHillsDatabaseNumber { get; set; }
        [Name("Streetmap")]
        public string StreetmapUrl { get; set; }
        [Name("Geograph")]
        public string GeographUrl { get; set; }
        [Name("Hill-bagging")]
        public string HillBaggingUrl { get; set; }
        [Name("Name")]
        public string Name { get; set; }
        [Name("SMC Section")]
        public int SmcSection { get; set; }
        [Name("RHB Section")]
        public string RhbSection { get; set; }
        [Name("_Section")]
        public string Section { get; set; }
        [Name("Height (m)")]
        public double HeightInMetres { get; set; }
        [Name("Height (ft)")]
        public double HeightInFeet { get; set; }
        [Name("Map 1:50")]
        public string Map1To50 { get; set; }
        [Name("Map 1:25")]
        public string Map1To25 { get; set; }
        [Name("Grid Ref")]
        public string GridRef { get; set; }
        [Name("GridRefXY")]
        public string GridRefXy { get; set; }
        [Name("xcoord")]
        public int XCoord { get; set; }
        [Name("ycoord")]
        public int YCoord { get; set; }

        [Name("1891")]
        public string HillCategory1891 { get; set; }
        [Name("1921")]
        public string HillCategory1921 { get; set; }
        [Name("1933")]
        public string HillCategory1933 { get; set; }
        [Name("1953")]
        public string HillCategory1953 { get; set; }
        [Name("1969")]
        public string HillCategory1969 { get; set; }
        [Name("1974")]
        public string HillCategory1974 { get; set; }
        [Name("1981")]
        public string HillCategory1981 { get; set; }
        [Name("1984")]
        public string HillCategory1984 { get; set; }
        [Name("1990")]
        public string HillCategory1990 { get; set; }
        [Name("1997")]
        public string HillCategory1997 { get; set; }
        [Name("Post 1997")]
        public string HillCategoryPost1997 { get; set; }
        [Name("Comments")]
        public string Comments { get; set; }
    }
}

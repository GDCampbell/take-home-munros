
namespace TakeHomeMunrosApi.Models
{
    public class MunroModel
    {
        public string Name { get; set; }
        public double HeightInMetres { get; set; }
        public string GridRef { get; set; }
        public HillCategory? Category { get; set; }
    }
}

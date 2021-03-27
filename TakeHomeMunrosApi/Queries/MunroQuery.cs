namespace TakeHomeMunrosApi.Queries
{
    public class MunroQuery
    {
        
        public double? MinHeightInMetres { get; set; }
        public double? MaxHeightInMetres { get; set; }

        public string SortBy { get; set; }
        public int? Limit { get; set; }
        public HillCategory? Category { get; set; } = HillCategory.Either;
    }
}

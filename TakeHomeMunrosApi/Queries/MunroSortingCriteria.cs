namespace TakeHomeMunrosApi.Queries
{
    public class MunroSortingCriteria : ISortingCriteria
    {
        public MunroSortingCriteria(string queryParameter)
        {
            IsAscending = queryParameter.StartsWith("asc");
            PropertyName = queryParameter.Contains("name") ? "Name" :
                queryParameter.Contains("height") ? "HeightInMetres" : null;
        }

        public string PropertyName { get; }
        public bool IsAscending { get; }
        public int? Priority { get; } = 0;
    }
}

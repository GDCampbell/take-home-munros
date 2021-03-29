using System.Text.RegularExpressions;

namespace TakeHomeMunrosApi.Queries
{
    public class MunroSortingCriteria : ISortingCriteria
    {
        public MunroSortingCriteria(string queryParameter)
        {
            IsAscending = queryParameter.StartsWith("asc");
            PropertyName = queryParameter.Contains("name") ? "Name" :
                queryParameter.Contains("height") ? "HeightInMetres" : null;

            var priorityRegex = new Regex(@"(?<=\)#)[0-9]{1}$", RegexOptions.IgnoreCase);

            var match = priorityRegex.Match(queryParameter);

            if (match.Success && (int.TryParse(match.Value, out var priority)))
            {
                Priority = priority;
            }
            
        }

        public string PropertyName { get; }
        public bool IsAscending { get; }
        public int? Priority { get; } = int.MaxValue;
    }
}

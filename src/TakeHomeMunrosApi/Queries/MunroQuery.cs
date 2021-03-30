using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TakeHomeMunrosApi.Queries
{
    public class MunroQuery
    {
        public double? MinHeightInMetres { get; set; }
        public double? MaxHeightInMetres { get; set; }
        
        [RegularExpression(@"(?:(?:a|de)sc\((?:name|height)\)(?:p[0-9])?(?:\,)?)+", 
            ErrorMessage = "Requires the parameter to be in the format of asc|desc(name|height), optionally followed by # and a number from 0-9 to denote its priority.")]
        public string SortBy { get; set; }
        public int? Limit { get; set; }
        public HillCategory? Category { get; set; } = HillCategory.Either;

        public IList<ISortingCriteria> SortingCriterias => 
            string.IsNullOrEmpty(SortBy) ? new List<ISortingCriteria>()
            : SortBy.Split(",").Where(q => !string.IsNullOrEmpty(q)).Select(p => new MunroSortingCriteria(p) as ISortingCriteria).ToList();
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper.Configuration.Annotations;

namespace TakeHomeMunrosApi.Queries
{
    public class MunroQuery
    {
        public double? MinHeightInMetres { get; set; }
        public double? MaxHeightInMetres { get; set; }
        
        [RegularExpression(@"(?:(?:(?:a|de){1}sc){1}(?:\((?:name|height){1}\)){1}(?:\,)?)+", ErrorMessage = "Requires the parameter to be in the format of asc|desc(name|height+p1) where the number following the p denotes its priority")]
        public string SortBy { get; set; }
        public int? Limit { get; set; }
        public HillCategory? Category { get; set; } = HillCategory.Either;

        public IList<ISortingCriteria> SortingCriterias => 
            string.IsNullOrEmpty(SortBy) ? new List<ISortingCriteria>()
            : SortBy.Split(",").Select(p => new MunroSortingCriteria(p) as ISortingCriteria).ToList();
    }
}

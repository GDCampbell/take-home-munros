using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using TakeHomeMunros.Data;
using TakeHomeMunrosApi.Helpers;
using TakeHomeMunrosApi.Models;
using TakeHomeMunrosApi.Queries;

namespace TakeHomeMunrosApi.Services
{
    public class MunroService : IMunroService
    {
        readonly IMunroDataContext dataContext;
        readonly IMapper mapper;

        public MunroService(IMunroDataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public IEnumerable<MunroModel> GetMunros(MunroQuery sortQuery)
        {
            var allMunros = dataContext.Munros
                .Where(m => !string.IsNullOrEmpty(m.HillCategoryPost1997))
                .Select(mapper.Map<MunroModel>).ToList();
            
            var filteredMunros = allMunros.Where(m => m.HeightInMetres >= (sortQuery.MinHeightInMetres ?? 0)
                                                      && m.HeightInMetres <= (sortQuery.MaxHeightInMetres ?? double.MaxValue)
                                                      && (sortQuery.Category == HillCategory.Either || GetHillCategoryStringAsEnum(m.HillCategory) == sortQuery.Category)).ToList();

            if (sortQuery.SortingCriterias.Any())
            {
                var distinctSortingCriterias = sortQuery.SortingCriterias
                    .GroupBy(c => c.PropertyName)
                    .Select(g => g.OrderBy(c => c.Priority).First()).OrderBy(c => c.Priority).ToList();

                var sortedMunros = filteredMunros.AsQueryable().OrderBySortingCriterias(distinctSortingCriterias);

                filteredMunros = sortedMunros.ToList();
            }
            
            return sortQuery.Limit != null ? filteredMunros.Take(sortQuery.Limit.Value) : filteredMunros;
        }

        static HillCategory GetHillCategoryStringAsEnum(string category)
        {
            switch (category)
            {
                case "MUN":
                    return HillCategory.Munro;
                case "TOP":
                    return HillCategory.Top;
            }

            return HillCategory.Either;
        }
    }
}

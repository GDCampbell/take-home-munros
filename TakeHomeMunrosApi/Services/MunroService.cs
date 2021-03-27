using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TakeHomeMunrosApi.DataContext;
using TakeHomeMunrosApi.Models;

namespace TakeHomeMunrosApi.Services
{
    public class MunroService
    {
        readonly IMunroDataContext dataContext;
        readonly IMapper mapper;

        public MunroService(IMunroDataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public IEnumerable<MunroModel> GetMunros()
        {
            var munros = dataContext.Munros
                .Where(m => !string.IsNullOrEmpty(m.HillCategoryPost1997))
                .Select(mapper.Map<MunroModel>).ToList();

            return munros;
        }


    }
}

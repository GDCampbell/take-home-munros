using System.Collections.Generic;

using TakeHomeMunrosApi.DataContext;
using TakeHomeMunrosApi.Models;

namespace TakeHomeMunrosApi.Services
{
    public class MunroService
    {
        readonly IMunroDataContext dataContext;

        public MunroService(IMunroDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<MunroModel> GetMunros()
        {
            return dataContext.Munros;
        }
    }
}

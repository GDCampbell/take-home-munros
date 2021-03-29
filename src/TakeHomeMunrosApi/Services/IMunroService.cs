using System.Collections.Generic;

using TakeHomeMunrosApi.Models;
using TakeHomeMunrosApi.Queries;

namespace TakeHomeMunrosApi.Services
{
    public interface IMunroService
    {
        IEnumerable<MunroModel> GetMunros(MunroQuery sortQuery);
    }
}

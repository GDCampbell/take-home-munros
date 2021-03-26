using System.Collections.Generic;

using TakeHomeMunrosApi.Models;

namespace TakeHomeMunrosApi.DataContext
{
    public interface IMunroDataContext
    {
        IEnumerable<MunroModel> Munros { get; }
    }
}

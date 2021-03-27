using System.Collections.Generic;
using TakeHomeMunrosApi.Domain;

namespace TakeHomeMunrosApi.DataContext
{
    public interface IMunroDataContext
    {
        IEnumerable<Munro> Munros { get; }
    }
}

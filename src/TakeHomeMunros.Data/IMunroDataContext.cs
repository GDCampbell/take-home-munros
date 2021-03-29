using System.Collections.Generic;

namespace TakeHomeMunros.Data
{
    public interface IMunroDataContext
    {
        IEnumerable<Munro> Munros { get; }
    }
}

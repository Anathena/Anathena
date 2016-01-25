using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASMSharedInterface
{
    public interface ISharedAssemblyInterface
    {
        Int32 Addition(Int32 a, Int32 b);
        Int32 Multipliation(Int32 a, Int32 b);
    }
}

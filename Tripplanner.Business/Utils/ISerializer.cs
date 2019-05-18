using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Utils
{
    public interface ISerializer
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string flat);
    }
}

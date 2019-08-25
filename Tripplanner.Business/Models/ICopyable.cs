using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Models
{
    public interface ICopyable<T>
    {
        void CopyFrom(T other);
    }
}

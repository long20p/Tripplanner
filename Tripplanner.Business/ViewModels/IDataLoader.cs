using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.ViewModels
{
    public interface IDataLoader
    {
        bool IndeterminateLoading { get; }
        bool IsLoading { get; }
    }
}

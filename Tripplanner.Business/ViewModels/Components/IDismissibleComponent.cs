using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.ViewModels.Components
{
    public interface IDismissibleComponent
    {
        Action OnFinish { get; set; }
    }
}

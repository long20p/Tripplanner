using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Services
{
    public interface IDispatcherService
    {
        void RunOnUiThread(Action action);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Configs
{
    public class Confirmation
    {
        public string Message { get; set; }
        public Action OnProceed { get; set; }
        public Action OnCancel { get; set; }
    }
}

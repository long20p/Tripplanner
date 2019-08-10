using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.Models;

namespace Tripplanner.Business.ViewModels
{
    public abstract class TripAwareViewModelBase : ViewModelBase<Trip>
    {
        protected Trip Trip;
        public override void Prepare(Trip parameter)
        {
            Trip = parameter;
        }
    }
}

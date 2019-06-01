using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Tripplanner.Business.Services;
using Tripplanner.Business.ViewModels;

namespace Tripplanner.Business
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MainViewModel>();
        }
    }
}

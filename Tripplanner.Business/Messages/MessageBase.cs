using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Plugin.Messenger;

namespace Tripplanner.Business.Messages
{
    public abstract class MessageBase : MvxMessage
    {
        protected MessageBase(object sender) : base(sender)
        {
        }
    }
}

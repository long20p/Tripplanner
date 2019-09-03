using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Models
{
    public interface IUnique
    {
        Guid UniqueId { get; }
    }
}

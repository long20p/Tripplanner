using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Tripplanner.Business.Models
{
    public abstract class BaseEntity : IUnique
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public Guid UniqueId { get; set; }
    }
}

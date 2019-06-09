using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Tripplanner.Business.Models
{
    public abstract class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}

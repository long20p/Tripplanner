using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Configs
{
    public enum ArchiveEntryType
    {
        Text,
        Binary
    }

    public class ArchiveEntry
    {
        public ArchiveEntry(string name, ArchiveEntryType type, object content)
        {
            Name = name;
            Type = type;
            Content = content;
        }

        public string Name { get; }
        public ArchiveEntryType Type { get; }
        public object Content { get; }
    }
}

using System.Collections.Generic;

namespace ArchivePreviewer.Model
{
    class ArchiveFolder : IArchiveItem
    {
        public ArchiveFolder(string name)
        {
            Name = name;
            Items = new List<IArchiveItem>();
        }
        public string Name { get; set; }
        public IList<IArchiveItem> Items { get; set; }
    }
}

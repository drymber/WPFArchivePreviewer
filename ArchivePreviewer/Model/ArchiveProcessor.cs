using System.Collections.Generic;
using System.Linq;

namespace ArchivePreviewer.Model
{
    class ArchiveProcessor
    {
        private List<IArchiveItem> _archiveItems;
        public IEnumerable<IArchiveItem> ProcessEntries(IEnumerable<string> entries)
        {
            try
            {
                _archiveItems = new List<IArchiveItem>();
                foreach (var entry in entries)
                {
                    if (entry.EndsWith("/"))
                    {
                        AddFolder(entry);
                    }
                    else
                        AddFile(entry);
                }
                return _archiveItems;
            }
            catch
            {
                return new List<IArchiveItem>();
            }
        }

        private void AddFile(string path)
        {
            var splittedPath = path.Split('/');
            var newFolder = new ArchiveFile(splittedPath.LastOrDefault());
            if (splittedPath.Length > 1)
            {
                var parentFolder = GetParentFolder(splittedPath[splittedPath.Length - 2], _archiveItems);
                parentFolder.Items.Add(newFolder);
            }
        }

        public void AddFolder(string path)
        {
            var splittedPath = path.Split('/');
            var newFolder = new ArchiveFolder(splittedPath[splittedPath.Length - 2]);
            if (splittedPath.Length > 2)
            {
                var parentFolder = GetParentFolder(splittedPath[splittedPath.Length - 3], _archiveItems);
                parentFolder.Items.Add(newFolder);
            }
            else
                _archiveItems.Add(newFolder);
        }
        private ArchiveFolder GetParentFolder(string name, IEnumerable<IArchiveItem> archiveItems)
        {
            foreach (var folder in archiveItems)
            {
                if (folder is ArchiveFolder archiveFolder)
                {
                    if (folder.Name == name)
                        return archiveFolder;
                    else
                        return GetParentFolder(name, archiveFolder.Items);
                }
            }
            return null;
        }
    }
}

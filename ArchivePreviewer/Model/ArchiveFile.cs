namespace ArchivePreviewer.Model
{
    class ArchiveFile : IArchiveItem
    {
        public ArchiveFile(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}

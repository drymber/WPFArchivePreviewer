using ArchivePreviewer.Model;
using System.Windows;
using System.Windows.Controls;

namespace ArchivePreviewer
{
    class TreeViewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FolderDataTemplate { get; set; }
        public DataTemplate FileDataTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var archiveItem = item as IArchiveItem;
            if (archiveItem is ArchiveFile)
                return FileDataTemplate;
            return FolderDataTemplate;
        }
    }
}

using ArchivePreviewer.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArchivePreviewer
{
    /// <summary>
    /// Interaction logic for ArchiveViewer.xaml
    /// </summary>
    public partial class ArchiveViewer : UserControl
    {
        private ArchiveProcessor _archiveProcessor = new ArchiveProcessor();

        public ArchiveViewer()
        {
            InitializeComponent();
        }


        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(ArchiveViewer), new PropertyMetadata(new PropertyChangedCallback(OnChangeFilePath)));

        private static void OnChangeFilePath(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var archiveViewer = d as ArchiveViewer;
            archiveViewer.ChangeFilePath();
        }
        private void ChangeFilePath()
        {
            var entries = new List<string>();
            StopwatchDecorator(() =>
            {
                using (ZipArchive archive = ZipFile.OpenRead(FilePath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        entries.Add(entry.FullName);
                    }
                }
            }, "To read archive");
            var tree = new List<IArchiveItem>();
            StopwatchDecorator(() =>
            {
                tree = _archiveProcessor.ProcessEntries(entries).ToList();
            }, "To process entries");
            treeView.ItemsSource = tree;
        }

        public void StopwatchDecorator(Action action, string text)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            Debug.WriteLine($"{text}: {stopwatch.ElapsedMilliseconds} milliseconds");
        }
    }
}

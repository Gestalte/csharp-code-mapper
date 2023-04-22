using DeclarationCollector;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Editor;
using Microsoft.VisualStudio.Extensibility.UI;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media;

namespace CodeMapperExtension
{
    /// <summary>
    /// ViewModel for the CodeMapContent remote user control.
    /// </summary>
    [DataContract]
    internal class CodeMapData : NotifyPropertyChangedObject
    {
        public CodeMapData()
        {
            UpdateCommand = new AsyncCommand(async (parameter, cancellationToken) =>
            {
                //var output = Collector.GetDeclarations(programText);

                Tree.Add(new TreeItem
                {
                    Name = "Update " + count
                });

                count++;
            });
        }

        private int count = 0;

        [DataMember]
        public AsyncCommand UpdateCommand { get; }

        [DataMember]
        public ObservableCollection<TreeItem> Tree { get; } = new();

    }

    [DataContract]
    public class TreeItem
    {
         [DataMember]
        public int LineNumber { get; set; } = 0;
         [DataMember]
        public string Type { get; set; } = "";
         [DataMember]
        public string Name { get; set; } = "";
         [DataMember]
        public string Parameters { get; set; } = "";
         [DataMember]
        public string DeclarationType { get; set; } = "";
        [DataMember]
        public List<TreeItem> Children { get; set; } = new();
    }
}
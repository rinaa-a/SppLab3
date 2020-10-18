using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SppLab3
{
    public class AssemblyData
    {
        public string Name { get; }
        public ObservableCollection<NamespaceData> Namespaces { get; set; }
        public AssemblyData(string name)
        {
            Name = name;
            Namespaces = new ObservableCollection<NamespaceData>();
        }
    }
}

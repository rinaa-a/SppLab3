using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SppLab3
{
    public class NamespaceData
    {
        public string Name { get; }
        public ObservableCollection<ClassData> Classes { get; set; }
        public NamespaceData(string name)
        {
            Name = name;
            Classes = new ObservableCollection<ClassData>();
        }
    }
}

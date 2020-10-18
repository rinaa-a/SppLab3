using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SppLab3
{
    public class ClassMemberData
    {
        public string Name { get; }
        public ObservableCollection<string> Items { get; set; }
        public ClassMemberData(string name)
        {
            Name = name;
            Items = new ObservableCollection<string>();
        }
    }
}

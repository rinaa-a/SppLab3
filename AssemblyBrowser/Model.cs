using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SppLab3;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser
{
    class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string assemblyFileName;
        private ObservableCollection<AssemblyData> assemblyData;
        public string AssemblyFileName {
            get => assemblyFileName;
            set
            {
                assemblyFileName = value;
                OnPropertyChanged("AssemblyFileName");
            }
        }

        public ObservableCollection<AssemblyData> AssemblyData
        {
            get => assemblyData;
            set
            {
                assemblyData = value;
                OnPropertyChanged("AssemblyData");
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

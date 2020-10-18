using Microsoft.Win32;
using SppLab3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace AssemblyBrowser
{
    class ViewModel : INotifyPropertyChanged
    {
        private Model model;
        private ICommand openFileCommand;
        private ICommand exitCommand;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ViewModel(Model model)
        {
            this.model = model;
        }
        public string AssemblyFileName
        {
            get => model.AssemblyFileName;
            set
            {
                model.AssemblyFileName = value;
                AssemblyData = new ObservableCollection<AssemblyData>
                {
                    SppLab3.AssemblyBrowser.GetAssemblyData(model.AssemblyFileName)
                };

                OnPropertyChanged("AssemblyFileName");
            }
        }
        public ObservableCollection<AssemblyData> AssemblyData
        {
            get => model.AssemblyData;
            set
            {
                model.AssemblyData = value;
                OnPropertyChanged("AssemblyData");
            }
        }

        public ICommand OpenFileCommand
        {
            get
            {
                return openFileCommand ??= new Command(obj =>
                  {
                      OpenFileDialog dialog = new OpenFileDialog();
                      if (dialog.ShowDialog() == true)
                      {
                          AssemblyFileName = dialog.FileName;
                      }
                  });
            }
        }
        public ICommand ExitCommand
        {
            get
            {
                return exitCommand ??= new Command(obj =>
                {
                    Application.Current.Shutdown();
                });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.ViewModels
{
    internal class VM_Base:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Action? CloseWindowAction {  get; set; }

        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void CloseWindow()
        {
            CloseWindowAction?.Invoke();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EVote.Utilities.Commands
{
    // https://stackoverflow.com/questions/29152627/visibility-binding-on-grid-not-working
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}

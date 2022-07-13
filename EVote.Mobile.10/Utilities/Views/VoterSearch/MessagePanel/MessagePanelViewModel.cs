using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EVote.Utilities.Commands;

namespace EVote.Utilities.Views
{
    public class MessagePanelViewModel : ViewModelBase
    {
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        public bool IsOk { get; set; }

        public MessagePanelViewModel(string message)
        {
            Message = message;
        }

        #region Commands
        private RelayCommand _okCommand;
        public ICommand OkCommand
        {
            get
            {
                if (_okCommand == null)
                {
                    _okCommand = new RelayCommand(param => this.OkClick());
                }
                return _okCommand;
            }
        }

        // Force parent frame to navigate back to the search page
        private void OkClick()
        {
            RaisePropertyChanged("IsOk");
        }
        #endregion
    }
}

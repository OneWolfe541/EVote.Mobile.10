using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace EVote.Utilities.Controls
{
    public interface INavigator
    {
        UserControl CurrentView { get; set; }

        UserControl CurrentMenu { get; set; }
    }
}

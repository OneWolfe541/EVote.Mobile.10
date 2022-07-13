using System;
using System.Collections.Generic;
using System.Text;

namespace EVote.Utilities.Controls
{
    /// <summary>
    /// Defines a touch keyboard interface.
    /// </summary>
    public interface ITouchKeyboardProvider
    {
        /// <summary>
        /// Display the touch keyboard.
        /// </summary>
        void ShowTouchKeyboard();

        /// <summary>
        /// Hide or minimize the touch keyboard.
        /// </summary>
        void HideTouchKeyboard();
    }
}

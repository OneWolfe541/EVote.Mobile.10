using EVote.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;

namespace EVote.Utilities.Controls
{
    public class TouchKeyboardProvider : ITouchKeyboardProvider
    {
        #region Private: Fields

        private readonly string _virtualKeyboardPath;
        private readonly bool _hasTouchScreen;

        #endregion

        #region ITouchKeyboardProvider Methods

        public TouchKeyboardProvider()
        {
            _hasTouchScreen = HasTouchInput();

            _virtualKeyboardPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles),
                @"Microsoft Shared\ink\TabTip.exe");
            //_virtualKeyboardPath = System.IO.Path.Combine(
            //    Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles),
            //    @"C:\Windows\System32\osk.exe");
        }

        public void ShowTouchKeyboard()
        {
            if (!_hasTouchScreen) return;

            try
            {
                Process.Start(_virtualKeyboardPath);
            }
            catch (Exception ex)
            {
                EVoteLogger _keyboardLogger = new EVoteLogger("EVoteLogs", true);
                _keyboardLogger.WriteLog("Keyboard Error: " + ex.Message);

                // TODO: Log error.
                Debug.WriteLine(ex.ToString());
            }
        }

        public void HideTouchKeyboard()
        {
            if (!_hasTouchScreen) return;

            var nullIntPtr = new IntPtr(0);
            const uint wmSyscommand = 0x0112;
            var scClose = new IntPtr(0xF060);

            var keyboardWnd = FindWindow("IPTip_Main_Window", null);
            if (keyboardWnd != nullIntPtr)
            {
                SendMessage(keyboardWnd, wmSyscommand, scClose, nullIntPtr);
            }
        }

        #endregion

        #region Private: Win32 API Methods

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string sClassName, string sAppName);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        #endregion

        #region Private: Methods

        private static bool HasTouchInput()
        {
            return Tablet.TabletDevices.Cast<TabletDevice>().Any(
                tabletDevice => tabletDevice.Type == TabletDeviceType.Touch);
        }

        #endregion
    }
}

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MAIDE.Utilit.WinAPI;

namespace MAIDE.Utilit
{
    public partial class InputHook
    {
        private static event MouseEventHandler s_MouseMove = new MouseEventHandler(nop);
        private static event MouseEventHandler s_MouseDown = new MouseEventHandler(nop);
        private static event MouseEventHandler s_MouseUp = new MouseEventHandler(nop);
        private static event MouseEventHandler s_MouseClick = new MouseEventHandler(nop);
        private static event MouseEventHandler s_MouseDoubleClick = new MouseEventHandler(nop);
        private static event EventHandler<MouseEventExtArgs> s_MouseMoveExt = new EventHandler<MouseEventExtArgs>(nop);
        private static event EventHandler<MouseEventExtArgs> s_MouseClickExt = new EventHandler<MouseEventExtArgs>(nop);
        private static event MouseEventHandler s_MouseWheel = new MouseEventHandler(nop);
        private static event KeyEventHandler s_KeyDown = new KeyEventHandler(nop);
        private static event KeyEventHandler s_KeyUp = new KeyEventHandler(nop);
        private static event KeyPressEventHandler s_KeyPress = new KeyPressEventHandler(nop);
        private static MouseButtons s_PrevClickedButton;
        private static Timer s_DoubleClickTimer;

        private static HookProc s_MouseDelegate;
        private static int s_MouseHookHandle;
        private static HookProc s_KeyboardDelegate;
        private static int s_KeyboardHookHandle;

        private static int x, y;

        private static void nop(object sender, object e) { }

        private static int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MouseLLHookStruct mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));

                MouseButtons button = MouseButtons.None;
                short mouseDelta = 0;
                int clickCount = 0;
                bool mouseDown = false;
                bool mouseUp = false;

                switch (wParam)
                {
                    case Consts.WM_LBUTTONDOWN:
                        mouseDown = true;
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case Consts.WM_LBUTTONUP:
                        mouseUp = true;
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case Consts.WM_LBUTTONDBLCLK:
                        button = MouseButtons.Left;
                        clickCount = 2;
                        break;
                    case Consts.WM_RBUTTONDOWN:
                        mouseDown = true;
                        button = MouseButtons.Right;
                        clickCount = 1;
                        break;
                    case Consts.WM_RBUTTONUP:
                        mouseUp = true;
                        button = MouseButtons.Right;
                        clickCount = 1;
                        break;
                    case Consts.WM_RBUTTONDBLCLK:
                        button = MouseButtons.Right;
                        clickCount = 2;
                        break;
                    case Consts.WM_MOUSEWHEEL:
                        mouseDelta = (short)((mouseHookStruct.MouseData >> 16) & 0xffff);
                        break;
                }

                MouseEventExtArgs e = new MouseEventExtArgs(button, clickCount, mouseHookStruct.Point.X, mouseHookStruct.Point.Y, mouseDelta);

                if (s_MouseUp != null && mouseUp)
                    s_MouseUp.Invoke(null, e);

                if (s_MouseDown != null && mouseDown)
                    s_MouseDown.Invoke(null, e);

                if (s_MouseClick != null && clickCount > 0)
                    s_MouseClick.Invoke(null, e);

                if (s_MouseClickExt != null && clickCount > 0)
                    s_MouseClickExt.Invoke(null, e);

                if (s_MouseDoubleClick != null && clickCount == 2)
                    s_MouseDoubleClick.Invoke(null, e);

                if (s_MouseWheel != null && mouseDelta != 0)
                    s_MouseWheel.Invoke(null, e);

                if ((s_MouseMove != null || s_MouseMoveExt != null) && (x != mouseHookStruct.Point.X || y != mouseHookStruct.Point.Y))
                {
                    x = mouseHookStruct.Point.X;
                    y = mouseHookStruct.Point.Y;

                    s_MouseMove?.Invoke(null, e);
                    s_MouseMoveExt?.Invoke(null, e);
                }

                if (e.Handled)
                    return -1;
            }

            return API.CallNextHookEx(s_MouseHookHandle, nCode, wParam, lParam);
        }

        private static void EnsureSubscribedToGlobalMouseEvents()
        {
            if (s_MouseHookHandle == 0)
            {
                s_MouseDelegate = MouseHookProc;
                s_MouseHookHandle = API.SetWindowsHookEx(Consts.WH_MOUSE_LL, s_MouseDelegate, (IntPtr)0, 0);
                if (s_MouseHookHandle == 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        private static void TryUnsubscribeFromGlobalMouseEvents()
        {
            if (s_MouseClick == null &&
                s_MouseDown == null &&
                s_MouseMove == null &&
                s_MouseUp == null &&
                s_MouseClickExt == null &&
                s_MouseMoveExt == null &&
                s_MouseWheel == null)
            {
                ForceUnsunscribeFromGlobalMouseEvents();
            }
        }

        private static void ForceUnsunscribeFromGlobalMouseEvents()
        {
            if (s_MouseHookHandle != 0)
            {
                int result = API.UnhookWindowsHookEx(s_MouseHookHandle);
                s_MouseHookHandle = 0;
                s_MouseDelegate = null;
                if (result == 0)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(errorCode);
                }
            }
        }

        private static int KeyboardHookProc(int nCode, int wParam, IntPtr lParam)
        {
            bool handled = false;

            if (nCode >= 0)
            {
                KeyboardHookStruct keyboardStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                if (s_KeyDown != null && (wParam == Consts.WM_KEYDOWN || wParam == Consts.WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)keyboardStruct.VirtualKeyCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    s_KeyDown.Invoke(null, e);
                    handled = e.Handled;
                }

                if (s_KeyPress != null && wParam == Consts.WM_KEYDOWN)
                {
                    bool isDownShift = (API.GetKeyState(Consts.VK_SHIFT) & 0x80) == 0x80;
                    bool isDownCapslock = API.GetKeyState(Consts.VK_CAPITAL) != 0;

                    byte[] keyState = new byte[256];
                    API.GetKeyboardState(keyState);
                    byte[] inBuffer = new byte[2];
                    if (API.ToAscii(keyboardStruct.VirtualKeyCode, keyboardStruct.ScanCode, keyState, inBuffer, keyboardStruct.Flags) == 1)
                    {
                        char key = (char)inBuffer[0];
                        if ((isDownCapslock ^ isDownShift) && Char.IsLetter(key)) key = Char.ToUpper(key);
                        KeyPressEventArgs e = new KeyPressEventArgs(key);
                        s_KeyPress.Invoke(null, e);
                        handled = handled || e.Handled;
                    }
                }

                if (s_KeyUp != null && (wParam == Consts.WM_KEYUP || wParam == Consts.WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)keyboardStruct.VirtualKeyCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    s_KeyUp.Invoke(null, e);
                    handled = handled || e.Handled;
                }
            }

            if (handled)
                return -1;

            return API.CallNextHookEx(s_KeyboardHookHandle, nCode, wParam, lParam);
        }

        private static void EnsureSubscribedToGlobalKeyboardEvents()
        {
            if (s_KeyboardHookHandle == 0)
            {
                s_KeyboardDelegate = KeyboardHookProc;
                s_KeyboardHookHandle = API.SetWindowsHookEx(Consts.WH_KEYBOARD_LL, s_KeyboardDelegate, (IntPtr)0, 0);
                if (s_KeyboardHookHandle == 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        private static void TryUnsubscribeFromGlobalKeyboardEvents()
        {
            if (s_KeyDown == null && s_KeyUp == null && s_KeyPress == null && s_KeyboardHookHandle != 0)
            {
                int result = API.UnhookWindowsHookEx(s_KeyboardHookHandle);
                s_KeyboardHookHandle = 0;
                s_KeyboardDelegate = null;
                if (result == 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}

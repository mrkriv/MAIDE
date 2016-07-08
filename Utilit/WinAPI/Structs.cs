using System;
using System.Runtime.InteropServices;

namespace MAIDE.Utilit.WinAPI
{
    public delegate int HookProc(int nCode, int wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    public struct LVITEM
    {
        public uint mask;
        public int iItem;
        public int iSubItem;
        public uint state;
        public uint stateMask;
        public IntPtr pszText;
        public int cchTextMax;
        public int iImage;
        public IntPtr lParam;
        public int iIndent;
        public int iGroupId;
        public uint cColumns;
        public UIntPtr puColumns;
        public IntPtr piColFmt;
        public int iGroup;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMHDR
    {
        public IntPtr HWND;
        public uint idFrom;
        public int code;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseLLHookStruct
    {
        public Point Point;
        public int MouseData;
        public int Flags;
        public int Time;
        public int ExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardHookStruct
    {
        public int VirtualKeyCode;
        public int ScanCode;
        public int Flags;
        public int Time;
        public int ExtraInfo;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int X;
        public int Y;

        public Point(int X, int Y)
        {
            this.X = X; this.Y = Y;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Size
    {
        public int Width;
        public int Height;

        public Size(int Width, int Height)
        {
            this.Width = Width; this.Height = Height;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BLENDFUNCTION
    {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public byte AlphaFormat;
    }
}

using System;
using System.Runtime.InteropServices;

namespace MAIDE.Utilit.WinAPI
{
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
    public struct TPoint
    {
        public int X;
        public int Y;

        public TPoint(int X, int Y)
        {
            this.X = X; this.Y = Y;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TSize
    {
        public int Width;
        public int Height;

        public TSize(int Width, int Height)
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

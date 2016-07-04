﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIDE.Utilit.WinAPI
{
    [Flags]
    public enum ProcessAccessFlags : uint
    {
        All = 0x001F0FFF,
        Terminate = 0x00000001,
        CreateThread = 0x00000002,
        VirtualMemoryOperation = 0x00000008,
        VirtualMemoryRead = 0x00000010,
        VirtualMemoryWrite = 0x00000020,
        DuplicateHandle = 0x00000040,
        CreateProcess = 0x000000080,
        SetQuota = 0x00000100,
        SetInformation = 0x00000200,
        QueryInformation = 0x00000400,
        QueryLimitedInformation = 0x00001000,
        Synchronize = 0x00100000
    }

    public enum TBool
    {
        False = 0,
        True
    };

    [Flags]
    public enum AllocationType
    {
        Commit = 0x1000,
        Reserve = 0x2000,
        Decommit = 0x4000,
        Release = 0x8000,
        Reset = 0x80000,
        Physical = 0x400000,
        TopDown = 0x100000,
        WriteWatch = 0x200000,
        LargePages = 0x20000000
    }

    [Flags]
    public enum MemoryProtection
    {
        Execute = 0x10,
        ExecuteRead = 0x20,
        ExecuteReadWrite = 0x40,
        ExecuteWriteCopy = 0x80,
        NoAccess = 0x01,
        ReadOnly = 0x02,
        ReadWrite = 0x04,
        WriteCopy = 0x08,
        GuardModifierflag = 0x100,
        NoCacheModifierflag = 0x200,
        WriteCombineModifierflag = 0x400
    }

    [Flags]
    public enum FreeType
    {
        Decommit = 0x4000,
        Release = 0x8000,
    }

    public enum HitTestValues : int
    {
        HTERROR = -2,
        HTTRANSPARENT = -1,
        HTNOWHERE = 0,
        HTCLIENT = 1,
        HTCAPTION = 2,
        HTSYSMENU = 3,
        HTGROWBOX = 4,
        HTMENU = 5,
        HTHSCROLL = 6,
        HTVSCROLL = 7,
        HTMINBUTTON = 8,
        HTMAXBUTTON = 9,
        HTLEFT = 10,
        HTRIGHT = 11,
        HTTOP = 12,
        HTTOPLEFT = 13,
        HTTOPRIGHT = 14,
        HTBOTTOM = 15,
        HTBOTTOMLEFT = 16,
        HTBOTTOMRIGHT = 17,
        HTBORDER = 18,
        HTOBJECT = 19,
        HTCLOSE = 20,
        HTHELP = 21
    }
    public enum LVM : uint
    {
        FIRST = 0x1000,
        SETUNICODEFORMAT = 0x2005,        // CCM_SETUNICODEFORMAT,
        GETUNICODEFORMAT = 0x2006,        // CCM_GETUNICODEFORMAT,
        GETBKCOLOR = (FIRST + 0),
        SETBKCOLOR = (FIRST + 1),
        GETIMAGELIST = (FIRST + 2),
        SETIMAGELIST = (FIRST + 3),
        GETITEMCOUNT = (FIRST + 4),
        GETITEMA = (FIRST + 5),
        GETITEMW = (FIRST + 75),
        GETITEM = GETITEMW,
        //GETITEM                = GETITEMA,
        SETITEMA = (FIRST + 6),
        SETITEMW = (FIRST + 76),
        SETITEM = SETITEMW,
        //SETITEM                = SETITEMA,
        INSERTITEMA = (FIRST + 7),
        INSERTITEMW = (FIRST + 77),
        INSERTITEM = INSERTITEMW,
        //INSERTITEM             = INSERTITEMA,
        DELETEITEM = (FIRST + 8),
        DELETEALLITEMS = (FIRST + 9),
        GETCALLBACKMASK = (FIRST + 10),
        SETCALLBACKMASK = (FIRST + 11),
        GETNEXTITEM = (FIRST + 12),
        FINDITEMA = (FIRST + 13),
        FINDITEMW = (FIRST + 83),
        GETITEMRECT = (FIRST + 14),
        SETITEMPOSITION = (FIRST + 15),
        GETITEMPOSITION = (FIRST + 16),
        GETSTRINGWIDTHA = (FIRST + 17),
        GETSTRINGWIDTHW = (FIRST + 87),
        HITTEST = (FIRST + 18),
        ENSUREVISIBLE = (FIRST + 19),
        SCROLL = (FIRST + 20),
        REDRAWITEMS = (FIRST + 21),
        ARRANGE = (FIRST + 22),
        EDITLABELA = (FIRST + 23),
        EDITLABELW = (FIRST + 118),
        EDITLABEL = EDITLABELW,
        //EDITLABEL              = EDITLABELA,
        GETEDITCONTROL = (FIRST + 24),
        GETCOLUMNA = (FIRST + 25),
        GETCOLUMNW = (FIRST + 95),
        SETCOLUMNA = (FIRST + 26),
        SETCOLUMNW = (FIRST + 96),
        INSERTCOLUMNA = (FIRST + 27),
        INSERTCOLUMNW = (FIRST + 97),
        DELETECOLUMN = (FIRST + 28),
        GETCOLUMNWIDTH = (FIRST + 29),
        SETCOLUMNWIDTH = (FIRST + 30),
        GETHEADER = (FIRST + 31),
        CREATEDRAGIMAGE = (FIRST + 33),
        GETVIEWRECT = (FIRST + 34),
        GETTEXTCOLOR = (FIRST + 35),
        SETTEXTCOLOR = (FIRST + 36),
        GETTEXTBKCOLOR = (FIRST + 37),
        SETTEXTBKCOLOR = (FIRST + 38),
        GETTOPINDEX = (FIRST + 39),
        GETCOUNTPERPAGE = (FIRST + 40),
        GETORIGIN = (FIRST + 41),
        UPDATE = (FIRST + 42),
        SETITEMSTATE = (FIRST + 43),
        GETITEMSTATE = (FIRST + 44),
        GETITEMTEXTA = (FIRST + 45),
        GETITEMTEXTW = (FIRST + 115),
        SETITEMTEXTA = (FIRST + 46),
        SETITEMTEXTW = (FIRST + 116),
        SETITEMCOUNT = (FIRST + 47),
        SORTITEMS = (FIRST + 48),
        SETITEMPOSITION32 = (FIRST + 49),
        GETSELECTEDCOUNT = (FIRST + 50),
        GETITEMSPACING = (FIRST + 51),
        GETISEARCHSTRINGA = (FIRST + 52),
        GETISEARCHSTRINGW = (FIRST + 117),
        GETISEARCHSTRING = GETISEARCHSTRINGW,
        //GETISEARCHSTRING       = GETISEARCHSTRINGA,
        SETICONSPACING = (FIRST + 53),
        SETEXTENDEDLISTVIEWSTYLE = (FIRST + 54),            // optional wParam == mask
        GETEXTENDEDLISTVIEWSTYLE = (FIRST + 55),
        GETSUBITEMRECT = (FIRST + 56),
        SUBITEMHITTEST = (FIRST + 57),
        SETCOLUMNORDERARRAY = (FIRST + 58),
        GETCOLUMNORDERARRAY = (FIRST + 59),
        SETHOTITEM = (FIRST + 60),
        GETHOTITEM = (FIRST + 61),
        SETHOTCURSOR = (FIRST + 62),
        GETHOTCURSOR = (FIRST + 63),
        APPROXIMATEVIEWRECT = (FIRST + 64),
        SETWORKAREAS = (FIRST + 65),
        GETWORKAREAS = (FIRST + 70),
        GETNUMBEROFWORKAREAS = (FIRST + 73),
        GETSELECTIONMARK = (FIRST + 66),
        SETSELECTIONMARK = (FIRST + 67),
        SETHOVERTIME = (FIRST + 71),
        GETHOVERTIME = (FIRST + 72),
        SETTOOLTIPS = (FIRST + 74),
        GETTOOLTIPS = (FIRST + 78),
        SORTITEMSEX = (FIRST + 81),
        SETBKIMAGEA = (FIRST + 68),
        SETBKIMAGEW = (FIRST + 138),
        GETBKIMAGEA = (FIRST + 69),
        GETBKIMAGEW = (FIRST + 139),
        SETSELECTEDCOLUMN = (FIRST + 140),
        SETVIEW = (FIRST + 142),
        GETVIEW = (FIRST + 143),
        INSERTGROUP = (FIRST + 145),
        SETGROUPINFO = (FIRST + 147),
        GETGROUPINFO = (FIRST + 149),
        REMOVEGROUP = (FIRST + 150),
        MOVEGROUP = (FIRST + 151),
        GETGROUPCOUNT = (FIRST + 152),
        GETGROUPINFOBYINDEX = (FIRST + 153),
        MOVEITEMTOGROUP = (FIRST + 154),
        GETGROUPRECT = (FIRST + 98),
        SETGROUPMETRICS = (FIRST + 155),
        GETGROUPMETRICS = (FIRST + 156),
        ENABLEGROUPVIEW = (FIRST + 157),
        SORTGROUPS = (FIRST + 158),
        INSERTGROUPSORTED = (FIRST + 159),
        REMOVEALLGROUPS = (FIRST + 160),
        HASGROUP = (FIRST + 161),
        GETGROUPSTATE = (FIRST + 92),
        GETFOCUSEDGROUP = (FIRST + 93),
        SETTILEVIEWINFO = (FIRST + 162),
        GETTILEVIEWINFO = (FIRST + 163),
        SETTILEINFO = (FIRST + 164),
        GETTILEINFO = (FIRST + 165),
        SETINSERTMARK = (FIRST + 166),
        GETINSERTMARK = (FIRST + 167),
        INSERTMARKHITTEST = (FIRST + 168),
        GETINSERTMARKRECT = (FIRST + 169),
        SETINSERTMARKCOLOR = (FIRST + 170),
        GETINSERTMARKCOLOR = (FIRST + 171),
        GETSELECTEDCOLUMN = (FIRST + 174),
        ISGROUPVIEWENABLED = (FIRST + 175),
        GETOUTLINECOLOR = (FIRST + 176),
        SETOUTLINECOLOR = (FIRST + 177),
        CANCELEDITLABEL = (FIRST + 179),
        MAPINDEXTOID = (FIRST + 180),
        MAPIDTOINDEX = (FIRST + 181),
        ISITEMVISIBLE = (FIRST + 182),
        GETACCVERSION = (FIRST + 193),
        GETEMPTYTEXT = (FIRST + 204),
        GETFOOTERRECT = (FIRST + 205),
        GETFOOTERINFO = (FIRST + 206),
        GETFOOTERITEMRECT = (FIRST + 207),
        GETFOOTERITEM = (FIRST + 208),
        GETITEMINDEXRECT = (FIRST + 209),
        SETITEMINDEXSTATE = (FIRST + 210),
        GETNEXTITEMINDEX = (FIRST + 211),
        SETPRESERVEALPHA = (FIRST + 212),
        SETBKIMAGE = SETBKIMAGEW,
        GETBKIMAGE = GETBKIMAGEW,
        //SETBKIMAGE             = SETBKIMAGEA,
        //GETBKIMAGE             = GETBKIMAGEA,
    }
}

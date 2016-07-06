using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace MAIDE.UI
{
    public class DockContentProxy : DockContent
    {
        public DockContentProxy()
        {
            new Palette() { Owner = this };
        }
    }
}
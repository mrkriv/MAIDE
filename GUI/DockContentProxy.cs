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
        protected Palette Palette;

        public DockContentProxy()
        {
            Palette = new Palette();
            Palette.Owner = this;
        }
    }
}
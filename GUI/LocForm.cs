using MAIDE.Utilit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAIDE.UI
{
    public class LocForm : Form
    {
        public static ResourceManager Resource;
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SetLanguage(Resource);
        }
    }
}

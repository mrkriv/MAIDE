using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAIDE.UI;

namespace MAIDE
{
    public static class Log
    {
        public static readonly ObservableCollection<ErrorMessage> Errors = new ObservableCollection<ErrorMessage>();
        
        public static void AddError(CodeEditBox.RowReadonly row, string message, params object[] args)
        {
            string msg = string.Format(message, args);
            Errors.Add(new ErrorMessage(msg, row.Index, row.Owner));
        }
    }
}
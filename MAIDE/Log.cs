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
        public delegate void MessageDelegate(string message);

        private static MessageDelegate onMessage = (m)=> { };

        public static event MessageDelegate OnMessage
        {
            add { onMessage += value; }
            remove { onMessage -= value; }
        }
        
        public static void AddError(CodeEditBox.RowReadonly row, string message, params object[] args)
        {
            string msg = string.Format(message, args);
            Errors.Add(new ErrorMessage(msg, row.Index, row.Owner));
        }

        public static void AddMessage(string message, params object[] args)
        {
            var msg = string.Format(message, args);
            onMessage(msg);
        }
    }
}
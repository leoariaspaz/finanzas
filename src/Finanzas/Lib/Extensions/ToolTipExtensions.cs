using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finanzas.Lib.Extensions
{
    public static class ToolTipExtensions
    {
        public static void ShowError(this ToolTip toolTip, IWin32Window window, TextBox textBox, string message, 
            int duration = 3000)
        {
            Point p = new Point(textBox.Left + textBox.Size.Width / 2, textBox.Top + textBox.Size.Height / 2 - 20);
            if (!textBox.Multiline) p.Y -= 25;
            p = textBox.FindForm().PointToClient(textBox.Parent.PointToScreen(p));
            var tt = new ToolTip();
            tt.IsBalloon = true;
            tt.ToolTipIcon = ToolTipIcon.Error;
            tt.ToolTipTitle = "Error";
            tt.Show(message, window, p, duration);
        }
    }
}

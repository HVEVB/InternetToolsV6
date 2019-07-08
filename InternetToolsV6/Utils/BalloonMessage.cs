using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetToolsV6.Utils
{
    public class BalloonMessage
    {
        public static void SideMessage(string title, string text, int TaskbarTimeout, Icon icon)
        {
            NotifyIcon Note = new NotifyIcon();
            Note.Visible = true;
            Note.BalloonTipTitle = (string)title;
            Note.BalloonTipText = (string)text;
            Note.Icon = icon;
            Note.ShowBalloonTip(TaskbarTimeout);
            Clipboard.SetText(text);
            Note.BalloonTipClosed += (object sender, EventArgs e) =>
            {
                Note.Visible = false;
            };
            Note.Click += (object sender, EventArgs e) =>
            {
                Note.Visible = false;
            };
        }

        public static void SimpleSideMessage(string title, string text)
        {
            NotifyIcon Note = new NotifyIcon();
            Note.Visible = true;
            Note.BalloonTipTitle = (string)title;
            Note.BalloonTipText = (string)text;
            Note.Icon = SystemIcons.Information;
            Note.ShowBalloonTip(5);
            Clipboard.SetText(text);
            Note.BalloonTipClosed += (object sender, EventArgs e) =>
            {
                Note.Visible = false;
            };
            Note.Click += (object sender, EventArgs e) =>
            {
                Note.Visible = false;
            };
        }
    }
}

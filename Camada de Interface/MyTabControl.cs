using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ETdA.Camada_de_Interface
{
    class MyTabControl : System.Windows.Forms.TabControl
    {
        Image img;
        List<string> tabpages;

        public List<string> Pages
        {
            get
            {
                List<string> _new = new List<string>();
                foreach (string s in tabpages)
                    _new.Add(s);
                return _new;
            }
        }

        public void AddPage(string page)
        {
            tabpages.Add(page);
        }

        public MyTabControl(Image _img)
        {
            img = _img;
            tabpages = new List<string>();
            DrawMode = TabDrawMode.OwnerDrawFixed;
            // used to expand the tab header, find a better way
            Padding = new Point(16,0);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            RectangleF tabTextArea = RectangleF.Empty;
            for (int nIndex = 0; nIndex < this.TabCount; nIndex++)
            {
                tabTextArea = (RectangleF)this.GetTabRect(nIndex);
                LinearGradientBrush br = new LinearGradientBrush(tabTextArea,
                    SystemColors.Control, SystemColors.Control,
                    LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(br, tabTextArea);

                if (SelectedIndex == nIndex)
                {
                    /*if active draw ,inactive close button*/
                    using (Bitmap bm = new Bitmap(img))
                    {
                        e.Graphics.DrawImage(bm, tabTextArea.X + tabTextArea.Width - 16, (tabTextArea.Height - img.Height) / 2);
                    }
                    br.Dispose();
                }

                string str = this.TabPages[nIndex].Text;
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Center;
                using (SolidBrush brush = new SolidBrush(
                    this.TabPages[nIndex].ForeColor))
                {
                    /*Draw the tab header text */
                    e.Graphics.DrawString(str, this.Font, brush, tabTextArea, stringFormat);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!DesignMode && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Rectangle rect = GetTabRect(SelectedIndex);
                rect = GetCloseBtnRect(rect);
                Point pt = new Point(e.X, e.Y);
                if (rect.Contains(pt))
                {
                    CloseTab(SelectedTab);
                }
            }
        }

        private Rectangle GetCloseBtnRect(Rectangle tabRect)
        {
            Rectangle rect = new Rectangle(tabRect.X + tabRect.Width - 16, (tabRect.Height - img.Height) / 2, img.Width, img.Height);
            return rect;
        }

        private void CloseTab(int tabindex)
        {
            CloseTab(TabPages[tabindex]);
        }

        private void CloseTab(TabPage tp)
        {
            TabPages.Remove(tp);
            tabpages.Remove(tp.Name);
            tp.Dispose();
        }
    }
}

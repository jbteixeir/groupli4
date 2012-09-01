using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ETdAnalyser.CamadaInterface
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
                tabTextArea = new RectangleF(this.GetTabRect(nIndex).X-2, this.GetTabRect(nIndex).Y -2,this.GetTabRect(nIndex).Width +2, this.GetTabRect(nIndex).Height+ 4);
                LinearGradientBrush brl = new LinearGradientBrush(tabTextArea,
                    SystemColors.GradientInactiveCaption, Color.White,
                    LinearGradientMode.Vertical);

                LinearGradientBrush brh = new LinearGradientBrush(tabTextArea,
                    SystemColors.GradientActiveCaption, Color.White,
                    LinearGradientMode.Vertical);

                if (SelectedIndex == nIndex)
                {
                    e.Graphics.FillRectangle(brl, tabTextArea);
                    /*if active draw ,inactive close button*/
                    using (Bitmap bm = new Bitmap(img))
                    {
                        e.Graphics.DrawImage(bm, tabTextArea.X + tabTextArea.Width - 18, ((tabTextArea.Height - img.Height) / 2));
                    }

                    brl.Dispose();
                }
                else
                {
                    e.Graphics.FillRectangle(brh, tabTextArea);
                    brh.Dispose();
                }

                string str = " " + this.TabPages[nIndex].Text;
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Center;
                using (SolidBrush brush = new SolidBrush(
                    this.TabPages[nIndex].ForeColor))
                {
                    /*Draw the tab header text */
                    e.Graphics.DrawString(str, this.Font, brush, tabTextArea, stringFormat);
                }

                Brush background_brush = new SolidBrush(SystemColors.GradientActiveCaption);
                Rectangle LastTabRect = this.GetTabRect(this.TabPages.Count - 1);
                Rectangle rect = new Rectangle();
                rect.Location = new Point(LastTabRect.Right + this.Left, this.Top);
                rect.Size = new Size(this.Right - rect.Left, LastTabRect.Height+2);
                e.Graphics.FillRectangle(background_brush, rect);
                background_brush.Dispose();

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

        public void CloseTab(int tabindex)
        {
            CloseTab(TabPages[tabindex]);
        }

        public void CloseTab(TabPage tp)
        {
            TabPages.Remove(tp);
            tabpages.Remove(tp.Name);
            Pages.Remove(tp.Name);
            tp.Dispose();
        }
    }
}

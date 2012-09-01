using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ETdAnalyser.CamadaInterface
{
    class MessageBoxPortuguese
    {
        public static int Button_OKCancel = 1;
        public static int Button_RetryCancel = 2;
        public static int Button_YesNo = 3;
        public static int Button_AbortRetryIgnore = 4;
        public static int Icon_Error = 1;
        public static int Icon_Warning = 2;
        public static int Icon_Info = 3;
        public static int Icon_Question = 4;

        private static Form form;

        private static void init_form(string _title, string _text, int _button, int _icon)
        {
            form = new Form();
            form.MaximizeBox = false;
            form.AutoScaleMode = AutoScaleMode.Font;
            form.AutoSize = true;
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.StartPosition = FormStartPosition.CenterScreen;
            Label text = new Label();
            int text_location = 10;

            if (_icon > 0)
            {
                PictureBox pic = getPicture(_icon);
                pic.Location = new Point(10, 10);
                form.Controls.Add(pic);
                text_location = 50;
            }

            form.Text = _title;
            text.Text = _text;
            text.Location = new Point(text_location, 10);
            text.AutoSize = true;
            form.Controls.Add(text);

            Panel p = getButtonsPanel(_button);

            p.Location = new Point((form.Width - p.Width) / 2,52);
            form.Controls.Add(p);
        }

        private static PictureBox getPicture(int _icon)
        {
            PictureBox b = new PictureBox();
            b.Size = new Size(32,32);
            switch (_icon)
            {
                case 1:
                    b.Image = global::ETdAnalyser.Properties.Resources.Error;
                    break;
                case 2:
                    b.Image = global::ETdAnalyser.Properties.Resources.Warning;
                    break;
                case 3:
                    b.Image = global::ETdAnalyser.Properties.Resources.Info;
                    break;
                case 4:
                    b.Image = global::ETdAnalyser.Properties.Resources.Help;
                    break;
            }
            return b;
        }

        private static Panel getButtonsPanel(int _button)
        {

            List<Button> lst = getButtons(_button);
            Panel p = new Panel();

            int width = 0;
            for (int i = 0 ; i < lst.Count; i++)
            {
                if (i != 0)
                    width += 5;
                lst[i].Location = new Point(width, 0);
                width += lst[i].Width;
                p.Controls.Add(lst[i]);
            }
            p.Width = width;
            p.Height = lst[0].Height;

            return p;
        }

        private static List<Button> getButtons(int _button)
        {
            List<Button> lst = new List<Button>();

            switch (_button)
            {
                case 0:
                    Button b = new Button();
                    b.Text = "OK";
                    b.DialogResult = DialogResult.OK;
                    lst.Add(b);
                    break;
                case 1:
                    Button b1 = new Button();
                    Button b2 = new Button();
                    b1.Text = "OK";
                    b1.DialogResult = DialogResult.OK;
                    b2.Text = "Cancelar";
                    b2.DialogResult = DialogResult.Cancel;
                    lst.Add(b1);
                    lst.Add(b2);
                    break;
                case 2:
                    Button b3 = new Button();
                    Button b4 = new Button();
                    b3.Text = "Tentar Nov.";
                    b3.DialogResult = DialogResult.Retry;
                    b4.Text = "Cancelar";
                    b4.DialogResult = DialogResult.Cancel;
                    lst.Add(b3);
                    lst.Add(b4);
                    break;
                case 3:
                    Button b5 = new Button();
                    Button b6 = new Button();
                    b5.Text = "Sim";
                    b5.DialogResult = DialogResult.Yes;
                    b6.Text = "Não";
                    b6.DialogResult = DialogResult.No;
                    lst.Add(b5);
                    lst.Add(b6);
                    break;
                case 4:
                    Button b7 = new Button();
                    Button b8 = new Button();
                    Button b9 = new Button();
                    b7.Text = "Abortar";
                    b7.DialogResult = DialogResult.Abort;
                    b8.Text = "Tentar Nov.";
                    b8.DialogResult = DialogResult.Retry;
                    b9.Text = "Ignorar";
                    b9.DialogResult = DialogResult.Ignore;
                    lst.Add(b7);
                    lst.Add(b8);
                    lst.Add(b9);
                    break;
            }
            return lst;
        }

        public static DialogResult Show(string title, string text)
        {
            init_form(title, text, 0, 0);
            return form.ShowDialog();
        }

        public static DialogResult Show(string title, string text, int icon)
        {
            init_form(title, text, 0, icon);
            return form.ShowDialog();
        }

        public static DialogResult Show(string title, string text, int buttons, int icon)
        {
            init_form(title, text, buttons, icon);
            return form.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ETdA.Camada_de_Interface
{
    class MessageBoxPortuguese
    {
        public int Button_OKCancel = 1;
        public int Button_RetryCancel = 2;
        public int Button_YesNo = 3;
        public int Button_AbortRetryIgnore = 4;
        public int Icon_Error = 1;
        public int Icon_Warning = 2;
        public int Icon_Info = 3;
        public int Icon_Question = 4;

        private void init_form(string _title, string _text, int _button, int _icon)
        {
            Form form = new Form();
            Label text = new Label();
            Button button1 = new Button();
            Button button2;
            Button button3;
            PictureBox pic;

            if (_button > 0)
                button2 = new Button();
            if (_button == 4)
                button3 = new Button();
            if (_icon > 0)
                pic = new PictureBox();

            //pic.Image = global::System.Windows.;

            form.Text = _title;
        }

        private DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public DialogResult Show()
        {
            return DialogResult.OK;
        }


    }
}

namespace ETdA.Camada_de_Interface
{
    partial class InterfaceRegisto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Username = new System.Windows.Forms.Label();
            this.Password1 = new System.Windows.Forms.Label();
            this.Password2 = new System.Windows.Forms.Label();
            this.Registar = new System.Windows.Forms.Button();
            this.Cancelar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Location = new System.Drawing.Point(12, 13);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(50, 13);
            this.Username.TabIndex = 0;
            this.Username.Text = "Utilizador";
            // 
            // Password1
            // 
            this.Password1.AutoSize = true;
            this.Password1.Location = new System.Drawing.Point(12, 39);
            this.Password1.Name = "Password1";
            this.Password1.Size = new System.Drawing.Size(77, 13);
            this.Password1.TabIndex = 1;
            this.Password1.Text = "Palavra-Chave";
            // 
            // Password2
            // 
            this.Password2.AutoSize = true;
            this.Password2.Location = new System.Drawing.Point(12, 65);
            this.Password2.Name = "Password2";
            this.Password2.Size = new System.Drawing.Size(121, 13);
            this.Password2.TabIndex = 2;
            this.Password2.Text = "Confirme Palavra-Chava";
            // 
            // Registar
            // 
            this.Registar.Location = new System.Drawing.Point(261, 95);
            this.Registar.Name = "Registar";
            this.Registar.Size = new System.Drawing.Size(75, 23);
            this.Registar.TabIndex = 3;
            this.Registar.Text = "Registar";
            this.Registar.UseVisualStyleBackColor = true;
            this.Registar.Click += new System.EventHandler(this.RegistarActionPerformed);
            // 
            // Cancelar
            // 
            this.Cancelar.Location = new System.Drawing.Point(169, 95);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(75, 23);
            this.Cancelar.TabIndex = 4;
            this.Cancelar.Text = "Cancelar";
            this.Cancelar.UseVisualStyleBackColor = true;
            this.Cancelar.Click += new System.EventHandler(this.CancelarActionPerformed);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(138, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(198, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeuPressActionPerformed);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(138, 33);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(198, 20);
            this.textBox2.TabIndex = 6;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeuPressActionPerformed);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(138, 59);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '*';
            this.textBox3.Size = new System.Drawing.Size(198, 20);
            this.textBox3.TabIndex = 7;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeuPressActionPerformed);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 95);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(134, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Manter sessão iniciada";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // InterfaceRegisto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 132);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.Registar);
            this.Controls.Add(this.Password2);
            this.Controls.Add(this.Password1);
            this.Controls.Add(this.Username);
            this.Name = "InterfaceRegisto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registar";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.Label Password1;
        private System.Windows.Forms.Label Password2;
        private System.Windows.Forms.Button Registar;
        private System.Windows.Forms.Button Cancelar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
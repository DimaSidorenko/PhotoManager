namespace TRRP1
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbUserName = new System.Windows.Forms.Label();
            this.btLogin = new System.Windows.Forms.Button();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.btCreate = new System.Windows.Forms.Button();
            this.btRead = new System.Windows.Forms.Button();
            this.btPrev = new System.Windows.Forms.Button();
            this.btNext = new System.Windows.Forms.Button();
            this.fdLoad = new System.Windows.Forms.OpenFileDialog();
            this.btLogout = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // lbUserName
            // 
            this.lbUserName.AutoSize = true;
            this.lbUserName.Location = new System.Drawing.Point(49, 776);
            this.lbUserName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(0, 32);
            this.lbUserName.TabIndex = 0;
            // 
            // btLogin
            // 
            this.btLogin.Location = new System.Drawing.Point(896, 717);
            this.btLogin.Margin = new System.Windows.Forms.Padding(6);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(140, 54);
            this.btLogin.TabIndex = 1;
            this.btLogin.Text = "Login";
            this.btLogin.UseVisualStyleBackColor = true;
            this.btLogin.Click += new System.EventHandler(this.btLogin_Click);
            // 
            // pbMain
            // 
            this.pbMain.Location = new System.Drawing.Point(74, 89);
            this.pbMain.Margin = new System.Windows.Forms.Padding(6);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(788, 584);
            this.pbMain.TabIndex = 2;
            this.pbMain.TabStop = false;
            // 
            // btCreate
            // 
            this.btCreate.Location = new System.Drawing.Point(918, 366);
            this.btCreate.Margin = new System.Windows.Forms.Padding(6);
            this.btCreate.Name = "btCreate";
            this.btCreate.Size = new System.Drawing.Size(240, 50);
            this.btCreate.TabIndex = 3;
            this.btCreate.Text = "Upload";
            this.btCreate.UseVisualStyleBackColor = true;
            this.btCreate.Click += new System.EventHandler(this.btCreate_Click);
            // 
            // btRead
            // 
            this.btRead.Location = new System.Drawing.Point(918, 428);
            this.btRead.Margin = new System.Windows.Forms.Padding(6);
            this.btRead.Name = "btRead";
            this.btRead.Size = new System.Drawing.Size(240, 50);
            this.btRead.TabIndex = 4;
            this.btRead.Text = "Get my photos";
            this.btRead.UseVisualStyleBackColor = true;
            this.btRead.Click += new System.EventHandler(this.btRead_Click);
            // 
            // btPrev
            // 
            this.btPrev.Location = new System.Drawing.Point(144, 717);
            this.btPrev.Margin = new System.Windows.Forms.Padding(6);
            this.btPrev.Name = "btPrev";
            this.btPrev.Size = new System.Drawing.Size(240, 50);
            this.btPrev.TabIndex = 8;
            this.btPrev.Text = "Previous";
            this.btPrev.UseVisualStyleBackColor = true;
            this.btPrev.Click += new System.EventHandler(this.btPrev_Click);
            // 
            // btNext
            // 
            this.btNext.Location = new System.Drawing.Point(486, 717);
            this.btNext.Margin = new System.Windows.Forms.Padding(6);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(240, 50);
            this.btNext.TabIndex = 9;
            this.btNext.Text = "Next";
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // fdLoad
            // 
            this.fdLoad.FileName = "openFileDialog1";
            // 
            // btLogout
            // 
            this.btLogout.Location = new System.Drawing.Point(1108, 717);
            this.btLogout.Margin = new System.Windows.Forms.Padding(6);
            this.btLogout.Name = "btLogout";
            this.btLogout.Size = new System.Drawing.Size(140, 54);
            this.btLogout.TabIndex = 10;
            this.btLogout.Text = "Logout";
            this.btLogout.UseVisualStyleBackColor = true;
            this.btLogout.Click += new System.EventHandler(this.btLogout_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(200, 43);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(294, 19);
            this.lblInfo.TabIndex = 11;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(74, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1084, 54);
            this.label1.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 905);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btLogout);
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.btPrev);
            this.Controls.Add(this.btRead);
            this.Controls.Add(this.btCreate);
            this.Controls.Add(this.pbMain);
            this.Controls.Add(this.btLogin);
            this.Controls.Add(this.lbUserName);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainForm";
            this.Text = "Google Photo";
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Button btLogin;
        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Button btCreate;
        private System.Windows.Forms.Button btRead;
        private System.Windows.Forms.Button btPrev;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.OpenFileDialog fdLoad;
        private System.Windows.Forms.Button btLogout;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label1;
    }
}


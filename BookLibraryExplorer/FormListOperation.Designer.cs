namespace BookLibraryExplorer
{
    partial class FormListOperation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormListOperation));
            this.gBFileList1 = new System.Windows.Forms.GroupBox();
            this.btnChoseFile1 = new System.Windows.Forms.Button();
            this.tBFileList1 = new System.Windows.Forms.TextBox();
            this.gBFileList2 = new System.Windows.Forms.GroupBox();
            this.btnChoseFile2 = new System.Windows.Forms.Button();
            this.tBFileList2 = new System.Windows.Forms.TextBox();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.tSMIFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMICreteFileList = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMICheckForRepeat = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tSProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.gBFileList1.SuspendLayout();
            this.gBFileList2.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBFileList1
            // 
            this.gBFileList1.Controls.Add(this.btnChoseFile1);
            this.gBFileList1.Controls.Add(this.tBFileList1);
            this.gBFileList1.Location = new System.Drawing.Point(0, 28);
            this.gBFileList1.Name = "gBFileList1";
            this.gBFileList1.Size = new System.Drawing.Size(504, 40);
            this.gBFileList1.TabIndex = 2;
            this.gBFileList1.TabStop = false;
            this.gBFileList1.Text = "Первый список";
            // 
            // btnChoseFile1
            // 
            this.btnChoseFile1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoseFile1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnChoseFile1.Location = new System.Drawing.Point(427, 12);
            this.btnChoseFile1.Name = "btnChoseFile1";
            this.btnChoseFile1.Size = new System.Drawing.Size(71, 24);
            this.btnChoseFile1.TabIndex = 1;
            this.btnChoseFile1.Text = "Выбрать";
            this.btnChoseFile1.UseVisualStyleBackColor = true;
            // 
            // tBFileList1
            // 
            this.tBFileList1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tBFileList1.Location = new System.Drawing.Point(3, 16);
            this.tBFileList1.Name = "tBFileList1";
            this.tBFileList1.ReadOnly = true;
            this.tBFileList1.Size = new System.Drawing.Size(418, 20);
            this.tBFileList1.TabIndex = 0;
            // 
            // gBFileList2
            // 
            this.gBFileList2.Controls.Add(this.btnChoseFile2);
            this.gBFileList2.Controls.Add(this.tBFileList2);
            this.gBFileList2.Location = new System.Drawing.Point(0, 74);
            this.gBFileList2.Name = "gBFileList2";
            this.gBFileList2.Size = new System.Drawing.Size(504, 40);
            this.gBFileList2.TabIndex = 3;
            this.gBFileList2.TabStop = false;
            this.gBFileList2.Text = "Второй список";
            // 
            // btnChoseFile2
            // 
            this.btnChoseFile2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoseFile2.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnChoseFile2.Location = new System.Drawing.Point(427, 12);
            this.btnChoseFile2.Name = "btnChoseFile2";
            this.btnChoseFile2.Size = new System.Drawing.Size(71, 24);
            this.btnChoseFile2.TabIndex = 1;
            this.btnChoseFile2.Text = "Выбрать";
            this.btnChoseFile2.UseVisualStyleBackColor = true;
            // 
            // tBFileList2
            // 
            this.tBFileList2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tBFileList2.Location = new System.Drawing.Point(3, 16);
            this.tBFileList2.Name = "tBFileList2";
            this.tBFileList2.ReadOnly = true;
            this.tBFileList2.Size = new System.Drawing.Size(418, 20);
            this.tBFileList2.TabIndex = 0;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMIFile});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(741, 24);
            this.mainMenuStrip.TabIndex = 4;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // tSMIFile
            // 
            this.tSMIFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMICreteFileList,
            this.tSMICheckForRepeat});
            this.tSMIFile.Name = "tSMIFile";
            this.tSMIFile.Size = new System.Drawing.Size(45, 20);
            this.tSMIFile.Text = "Файл";
            // 
            // tSMICreteFileList
            // 
            this.tSMICreteFileList.Name = "tSMICreteFileList";
            this.tSMICreteFileList.Size = new System.Drawing.Size(191, 22);
            this.tSMICreteFileList.Text = "Создать список";
            this.tSMICreteFileList.Click += new System.EventHandler(this.tSMICreteFileList_Click);
            // 
            // tSMICheckForRepeat
            // 
            this.tSMICheckForRepeat.Name = "tSMICheckForRepeat";
            this.tSMICheckForRepeat.Size = new System.Drawing.Size(191, 22);
            this.tSMICheckForRepeat.Text = "Проверить на повторы";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 504);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(741, 22);
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tSProgressBar
            // 
            this.tSProgressBar.Name = "tSProgressBar";
            this.tSProgressBar.Size = new System.Drawing.Size(200, 16);
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            // 
            // FormListOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 526);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.gBFileList2);
            this.Controls.Add(this.gBFileList1);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "FormListOperation";
            this.Text = "Работа со списками";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormListOperation_KeyDown);
            this.gBFileList1.ResumeLayout(false);
            this.gBFileList1.PerformLayout();
            this.gBFileList2.ResumeLayout(false);
            this.gBFileList2.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gBFileList1;
        private System.Windows.Forms.Button btnChoseFile1;
        private System.Windows.Forms.TextBox tBFileList1;
        private System.Windows.Forms.GroupBox gBFileList2;
        private System.Windows.Forms.Button btnChoseFile2;
        private System.Windows.Forms.TextBox tBFileList2;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tSMIFile;
        private System.Windows.Forms.ToolStripMenuItem tSMICreteFileList;
        private System.Windows.Forms.ToolStripMenuItem tSMICheckForRepeat;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar tSProgressBar;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}
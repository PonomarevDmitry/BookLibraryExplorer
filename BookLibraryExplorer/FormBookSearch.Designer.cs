namespace BookLibraryExplorer
{
    partial class FormBookSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBookSearch));
            this.tSTreeActions = new System.Windows.Forms.ToolStrip();
            this.tSBOpenFile = new System.Windows.Forms.ToolStripButton();
            this.tSBOpenFolder = new System.Windows.Forms.ToolStripButton();
            this.gBBookSearch = new System.Windows.Forms.GroupBox();
            this.tBBookSearch = new System.Windows.Forms.TextBox();
            this.btnPerformSearch = new System.Windows.Forms.Button();
            this.lVBookSearch = new System.Windows.Forms.ListView();
            this.colHeadFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeadFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contMSListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMILVOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMISelectInTree = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMIOpenFolderInLV = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tSSLSearchResultCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSTreeActions.SuspendLayout();
            this.gBBookSearch.SuspendLayout();
            this.contMSListView.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tSTreeActions
            // 
            this.tSTreeActions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tSTreeActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSBOpenFile,
            this.tSBOpenFolder});
            this.tSTreeActions.Location = new System.Drawing.Point(0, 0);
            this.tSTreeActions.Name = "tSTreeActions";
            this.tSTreeActions.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tSTreeActions.Size = new System.Drawing.Size(608, 25);
            this.tSTreeActions.TabIndex = 1;
            this.tSTreeActions.Text = "toolStrip1";
            this.tSTreeActions.Visible = false;
            // 
            // tSBOpenFile
            // 
            this.tSBOpenFile.Enabled = false;
            this.tSBOpenFile.Image = global::BookLibraryExplorer.Properties.Resources.OpenFile;
            this.tSBOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBOpenFile.Name = "tSBOpenFile";
            this.tSBOpenFile.Size = new System.Drawing.Size(102, 22);
            this.tSBOpenFile.Text = "Открыть файл";
            this.tSBOpenFile.Visible = false;
            this.tSBOpenFile.Click += new System.EventHandler(this.tSMILVOpenFile_Click);
            // 
            // tSBOpenFolder
            // 
            this.tSBOpenFolder.Enabled = false;
            this.tSBOpenFolder.Image = global::BookLibraryExplorer.Properties.Resources.Openfolder;
            this.tSBOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBOpenFolder.Name = "tSBOpenFolder";
            this.tSBOpenFolder.Size = new System.Drawing.Size(135, 22);
            this.tSBOpenFolder.Text = "Расположение файла";
            this.tSBOpenFolder.Visible = false;
            this.tSBOpenFolder.Click += new System.EventHandler(this.tSMIOpenFolderInLV_Click);
            // 
            // gBBookSearch
            // 
            this.gBBookSearch.Controls.Add(this.tBBookSearch);
            this.gBBookSearch.Controls.Add(this.btnPerformSearch);
            this.gBBookSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBBookSearch.Location = new System.Drawing.Point(0, 0);
            this.gBBookSearch.Name = "gBBookSearch";
            this.gBBookSearch.Size = new System.Drawing.Size(608, 40);
            this.gBBookSearch.TabIndex = 0;
            this.gBBookSearch.TabStop = false;
            this.gBBookSearch.Text = "Поиск";
            // 
            // tBBookSearch
            // 
            this.tBBookSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tBBookSearch.Location = new System.Drawing.Point(3, 16);
            this.tBBookSearch.Name = "tBBookSearch";
            this.tBBookSearch.Size = new System.Drawing.Size(531, 20);
            this.tBBookSearch.TabIndex = 0;
            this.tBBookSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBBookSearch_KeyDown);
            // 
            // btnPerformSearch
            // 
            this.btnPerformSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPerformSearch.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnPerformSearch.Location = new System.Drawing.Point(534, 16);
            this.btnPerformSearch.Name = "btnPerformSearch";
            this.btnPerformSearch.Size = new System.Drawing.Size(71, 21);
            this.btnPerformSearch.TabIndex = 1;
            this.btnPerformSearch.Text = "Найти";
            this.btnPerformSearch.UseVisualStyleBackColor = true;
            this.btnPerformSearch.Click += new System.EventHandler(this.btnPerformSearch_Click);
            // 
            // lVBookSearch
            // 
            this.lVBookSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHeadFileName,
            this.colHeadFilePath});
            this.lVBookSearch.ContextMenuStrip = this.contMSListView;
            this.lVBookSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lVBookSearch.LargeImageList = this.imageList;
            this.lVBookSearch.Location = new System.Drawing.Point(0, 40);
            this.lVBookSearch.MultiSelect = false;
            this.lVBookSearch.Name = "lVBookSearch";
            this.lVBookSearch.Size = new System.Drawing.Size(608, 406);
            this.lVBookSearch.SmallImageList = this.imageList;
            this.lVBookSearch.TabIndex = 2;
            this.lVBookSearch.UseCompatibleStateImageBehavior = false;
            this.lVBookSearch.View = System.Windows.Forms.View.Details;
            this.lVBookSearch.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lVBookSearch_ItemSelectionChanged);
            this.lVBookSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lVBookSearch_KeyDown);
            this.lVBookSearch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lVBookSearch_MouseDoubleClick);
            // 
            // colHeadFileName
            // 
            this.colHeadFileName.Text = "Имя файла";
            this.colHeadFileName.Width = 200;
            // 
            // colHeadFilePath
            // 
            this.colHeadFilePath.Text = "Путь";
            this.colHeadFilePath.Width = 511;
            // 
            // contMSListView
            // 
            this.contMSListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMILVOpenFile,
            this.tSMISelectInTree,
            this.tSMIOpenFolderInLV});
            this.contMSListView.Name = "contMSListView";
            this.contMSListView.Size = new System.Drawing.Size(185, 70);
            // 
            // tSMILVOpenFile
            // 
            this.tSMILVOpenFile.Image = global::BookLibraryExplorer.Properties.Resources.OpenFile;
            this.tSMILVOpenFile.Name = "tSMILVOpenFile";
            this.tSMILVOpenFile.Size = new System.Drawing.Size(184, 22);
            this.tSMILVOpenFile.Text = "Открыть файл";
            this.tSMILVOpenFile.Click += new System.EventHandler(this.tSMILVOpenFile_Click);
            // 
            // tSMISelectInTree
            // 
            this.tSMISelectInTree.Image = global::BookLibraryExplorer.Properties.Resources.GotoShortcuts;
            this.tSMISelectInTree.Name = "tSMISelectInTree";
            this.tSMISelectInTree.Size = new System.Drawing.Size(184, 22);
            this.tSMISelectInTree.Text = "Отобразить в дереве";
            this.tSMISelectInTree.Click += new System.EventHandler(this.tSMISelectInTree_Click);
            // 
            // tSMIOpenFolderInLV
            // 
            this.tSMIOpenFolderInLV.Image = global::BookLibraryExplorer.Properties.Resources.Openfolder;
            this.tSMIOpenFolderInLV.Name = "tSMIOpenFolderInLV";
            this.tSMIOpenFolderInLV.Size = new System.Drawing.Size(184, 22);
            this.tSMIOpenFolderInLV.Text = "Расположение файла";
            this.tSMIOpenFolderInLV.Click += new System.EventHandler(this.tSMIOpenFolderInLV_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "folder");
            this.imageList.Images.SetKeyName(1, ".djvu");
            this.imageList.Images.SetKeyName(2, ".djv");
            this.imageList.Images.SetKeyName(3, "file");
            this.imageList.Images.SetKeyName(4, ".pdf");
            this.imageList.Images.SetKeyName(5, ".chm");
            this.imageList.Images.SetKeyName(6, ".doc");
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSSLSearchResultCount});
            this.statusStrip.Location = new System.Drawing.Point(0, 446);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(608, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";
            // 
            // tSSLSearchResultCount
            // 
            this.tSSLSearchResultCount.Name = "tSSLSearchResultCount";
            this.tSSLSearchResultCount.Size = new System.Drawing.Size(0, 17);
            // 
            // FormBookSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 468);
            this.Controls.Add(this.lVBookSearch);
            this.Controls.Add(this.gBBookSearch);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tSTreeActions);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBookSearch";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Поиск Книг";
            this.tSTreeActions.ResumeLayout(false);
            this.tSTreeActions.PerformLayout();
            this.gBBookSearch.ResumeLayout(false);
            this.gBBookSearch.PerformLayout();
            this.contMSListView.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tSTreeActions;
        private System.Windows.Forms.ToolStripButton tSBOpenFile;
        private System.Windows.Forms.ToolStripButton tSBOpenFolder;
        private System.Windows.Forms.GroupBox gBBookSearch;
        private System.Windows.Forms.TextBox tBBookSearch;
        private System.Windows.Forms.Button btnPerformSearch;
        private System.Windows.Forms.ListView lVBookSearch;
        private System.Windows.Forms.ColumnHeader colHeadFileName;
        private System.Windows.Forms.ColumnHeader colHeadFilePath;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tSSLSearchResultCount;
        private System.Windows.Forms.ContextMenuStrip contMSListView;
        private System.Windows.Forms.ToolStripMenuItem tSMILVOpenFile;
        private System.Windows.Forms.ToolStripMenuItem tSMISelectInTree;
        private System.Windows.Forms.ToolStripMenuItem tSMIOpenFolderInLV;
    }
}
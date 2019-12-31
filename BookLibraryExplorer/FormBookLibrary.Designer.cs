namespace BookLibraryExplorer
{
    partial class FormBookLibrary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBookLibrary));
            this.gBLibraryAddress = new System.Windows.Forms.GroupBox();
            this.btnChoseDictionary = new System.Windows.Forms.Button();
            this.tBLibraryAddress = new System.Windows.Forms.TextBox();
            this.tVLibraryTree = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.contMSTreeViewFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMIOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMIOpenFileDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.tSTreeActions = new System.Windows.Forms.ToolStrip();
            this.tSBUpdateTree = new System.Windows.Forms.ToolStripButton();
            this.tSBCollapseAll = new System.Windows.Forms.ToolStripButton();
            this.tSBOpenFile = new System.Windows.Forms.ToolStripButton();
            this.tSBOpenFolder = new System.Windows.Forms.ToolStripButton();
            this.contMSTreeViewDirectory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMIOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMIRefreshFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tSMIFormListOperations = new System.Windows.Forms.ToolStripMenuItem();
            this.tSSLLibraryRoot = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSLSearchResultCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tSMIEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMIFind = new System.Windows.Forms.ToolStripMenuItem();
            this.gBLibraryAddress.SuspendLayout();
            this.contMSTreeViewFile.SuspendLayout();
            this.tSTreeActions.SuspendLayout();
            this.contMSTreeViewDirectory.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBLibraryAddress
            // 
            this.gBLibraryAddress.Controls.Add(this.btnChoseDictionary);
            this.gBLibraryAddress.Controls.Add(this.tBLibraryAddress);
            this.gBLibraryAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBLibraryAddress.Location = new System.Drawing.Point(0, 24);
            this.gBLibraryAddress.Name = "gBLibraryAddress";
            this.gBLibraryAddress.Size = new System.Drawing.Size(715, 40);
            this.gBLibraryAddress.TabIndex = 1;
            this.gBLibraryAddress.TabStop = false;
            this.gBLibraryAddress.Text = "Адрес библиотеки";
            // 
            // btnChoseDictionary
            // 
            this.btnChoseDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoseDictionary.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnChoseDictionary.Location = new System.Drawing.Point(638, 12);
            this.btnChoseDictionary.Name = "btnChoseDictionary";
            this.btnChoseDictionary.Size = new System.Drawing.Size(71, 24);
            this.btnChoseDictionary.TabIndex = 1;
            this.btnChoseDictionary.Text = "Выбрать";
            this.btnChoseDictionary.UseVisualStyleBackColor = true;
            this.btnChoseDictionary.Click += new System.EventHandler(this.btnChoseDictionary_Click);
            // 
            // tBLibraryAddress
            // 
            this.tBLibraryAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tBLibraryAddress.Location = new System.Drawing.Point(3, 16);
            this.tBLibraryAddress.Name = "tBLibraryAddress";
            this.tBLibraryAddress.ReadOnly = true;
            this.tBLibraryAddress.Size = new System.Drawing.Size(629, 20);
            this.tBLibraryAddress.TabIndex = 0;
            this.tBLibraryAddress.TabStop = false;
            // 
            // tVLibraryTree
            // 
            this.tVLibraryTree.AllowDrop = true;
            this.tVLibraryTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tVLibraryTree.ImageKey = "file";
            this.tVLibraryTree.ImageList = this.imageList;
            this.tVLibraryTree.Location = new System.Drawing.Point(0, 89);
            this.tVLibraryTree.Name = "tVLibraryTree";
            this.tVLibraryTree.SelectedImageKey = "file";
            this.tVLibraryTree.Size = new System.Drawing.Size(715, 470);
            this.tVLibraryTree.TabIndex = 0;
            this.tVLibraryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tVLibraryTree_AfterSelect);
            this.tVLibraryTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tVLibraryTree_NodeMouseDoubleClick);
            this.tVLibraryTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.tVLibraryTree_DragDrop);
            this.tVLibraryTree.DragOver += new System.Windows.Forms.DragEventHandler(this.tVLibraryTree_DragOver);
            this.tVLibraryTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tVLibraryTree_KeyDown);
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
            // contMSTreeViewFile
            // 
            this.contMSTreeViewFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMIOpenFile,
            this.tSMIOpenFileDirectory});
            this.contMSTreeViewFile.Name = "contMSFile";
            this.contMSTreeViewFile.Size = new System.Drawing.Size(183, 48);
            this.contMSTreeViewFile.Opened += new System.EventHandler(this.contMSTreeView_Opened);
            // 
            // tSMIOpenFile
            // 
            this.tSMIOpenFile.Image = global::BookLibraryExplorer.Properties.Resources.OpenFile;
            this.tSMIOpenFile.Name = "tSMIOpenFile";
            this.tSMIOpenFile.Size = new System.Drawing.Size(182, 22);
            this.tSMIOpenFile.Text = "Открыть файл";
            this.tSMIOpenFile.Click += new System.EventHandler(this.tSMIOpenFile_Click);
            // 
            // tSMIOpenFileDirectory
            // 
            this.tSMIOpenFileDirectory.Image = global::BookLibraryExplorer.Properties.Resources.Openfolder;
            this.tSMIOpenFileDirectory.Name = "tSMIOpenFileDirectory";
            this.tSMIOpenFileDirectory.Size = new System.Drawing.Size(182, 22);
            this.tSMIOpenFileDirectory.Text = "Расположение файла";
            this.tSMIOpenFileDirectory.Click += new System.EventHandler(this.tSMIOpenFileDirectory_Click);
            // 
            // tSTreeActions
            // 
            this.tSTreeActions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tSTreeActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSBUpdateTree,
            this.tSBCollapseAll,
            this.tSBOpenFile,
            this.tSBOpenFolder});
            this.tSTreeActions.Location = new System.Drawing.Point(0, 64);
            this.tSTreeActions.Name = "tSTreeActions";
            this.tSTreeActions.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tSTreeActions.Size = new System.Drawing.Size(715, 25);
            this.tSTreeActions.TabIndex = 2;
            this.tSTreeActions.Text = "toolStrip1";
            // 
            // tSBUpdateTree
            // 
            this.tSBUpdateTree.Image = global::BookLibraryExplorer.Properties.Resources.Refresh;
            this.tSBUpdateTree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBUpdateTree.Name = "tSBUpdateTree";
            this.tSBUpdateTree.Size = new System.Drawing.Size(117, 22);
            this.tSBUpdateTree.Text = "Обновить дерево";
            this.tSBUpdateTree.Click += new System.EventHandler(this.tSBUpdateTree_Click);
            // 
            // tSBCollapseAll
            // 
            this.tSBCollapseAll.Image = global::BookLibraryExplorer.Properties.Resources.CollapseAll;
            this.tSBCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBCollapseAll.Name = "tSBCollapseAll";
            this.tSBCollapseAll.Size = new System.Drawing.Size(96, 22);
            this.tSBCollapseAll.Text = "Свернуть все";
            this.tSBCollapseAll.Click += new System.EventHandler(this.tSBCollapseAll_Click);
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
            this.tSBOpenFile.Click += new System.EventHandler(this.tSBOpenFile_Click);
            // 
            // tSBOpenFolder
            // 
            this.tSBOpenFolder.Enabled = false;
            this.tSBOpenFolder.Image = global::BookLibraryExplorer.Properties.Resources.Openfolder;
            this.tSBOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBOpenFolder.Name = "tSBOpenFolder";
            this.tSBOpenFolder.Size = new System.Drawing.Size(106, 22);
            this.tSBOpenFolder.Text = "Открыть папку";
            this.tSBOpenFolder.Visible = false;
            this.tSBOpenFolder.Click += new System.EventHandler(this.tSBOpenFolder_Click);
            // 
            // contMSTreeViewDirectory
            // 
            this.contMSTreeViewDirectory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMIOpenFolder,
            this.tSMIRefreshFolder});
            this.contMSTreeViewDirectory.Name = "contMSDirectory";
            this.contMSTreeViewDirectory.Size = new System.Drawing.Size(158, 48);
            this.contMSTreeViewDirectory.Opened += new System.EventHandler(this.contMSTreeView_Opened);
            // 
            // tSMIOpenFolder
            // 
            this.tSMIOpenFolder.Image = global::BookLibraryExplorer.Properties.Resources.Openfolder;
            this.tSMIOpenFolder.Name = "tSMIOpenFolder";
            this.tSMIOpenFolder.Size = new System.Drawing.Size(157, 22);
            this.tSMIOpenFolder.Text = "Открыть папку";
            this.tSMIOpenFolder.Click += new System.EventHandler(this.tSMIOpenFileDirectory_Click);
            // 
            // tSMIRefreshFolder
            // 
            this.tSMIRefreshFolder.Image = global::BookLibraryExplorer.Properties.Resources.Refresh;
            this.tSMIRefreshFolder.Name = "tSMIRefreshFolder";
            this.tSMIRefreshFolder.Size = new System.Drawing.Size(157, 22);
            this.tSMIRefreshFolder.Text = "Обновить папку";
            this.tSMIRefreshFolder.Click += new System.EventHandler(this.tSMIRefreshFolder_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMIFormListOperations,
            this.tSMIEdit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(715, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tSMIFormListOperations
            // 
            this.tSMIFormListOperations.Name = "tSMIFormListOperations";
            this.tSMIFormListOperations.Size = new System.Drawing.Size(118, 20);
            this.tSMIFormListOperations.Text = "Работа со списками";
            this.tSMIFormListOperations.Click += new System.EventHandler(this.tSMIFormListOperations_Click);
            // 
            // tSSLLibraryRoot
            // 
            this.tSSLLibraryRoot.Name = "tSSLLibraryRoot";
            this.tSSLLibraryRoot.Size = new System.Drawing.Size(0, 17);
            // 
            // tSSLSearchResultCount
            // 
            this.tSSLSearchResultCount.Name = "tSSLSearchResultCount";
            this.tSSLSearchResultCount.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSSLLibraryRoot,
            this.tSSLSearchResultCount});
            this.statusStrip.Location = new System.Drawing.Point(0, 559);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(715, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip";
            // 
            // tSMIEdit
            // 
            this.tSMIEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMIFind});
            this.tSMIEdit.Name = "tSMIEdit";
            this.tSMIEdit.Size = new System.Drawing.Size(56, 20);
            this.tSMIEdit.Text = "Правка";
            // 
            // tSMIFind
            // 
            this.tSMIFind.Name = "tSMIFind";
            this.tSMIFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.tSMIFind.Size = new System.Drawing.Size(152, 22);
            this.tSMIFind.Text = "Поиск";
            this.tSMIFind.Click += new System.EventHandler(this.tSMIFind_Click);
            // 
            // FormBookLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 581);
            this.Controls.Add(this.tVLibraryTree);
            this.Controls.Add(this.tSTreeActions);
            this.Controls.Add(this.gBLibraryAddress);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormBookLibrary";
            this.Text = "Бибилиотека книг";
            this.Load += new System.EventHandler(this.FormBookLibrary_Load);
            this.gBLibraryAddress.ResumeLayout(false);
            this.gBLibraryAddress.PerformLayout();
            this.contMSTreeViewFile.ResumeLayout(false);
            this.tSTreeActions.ResumeLayout(false);
            this.tSTreeActions.PerformLayout();
            this.contMSTreeViewDirectory.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gBLibraryAddress;
        private System.Windows.Forms.TextBox tBLibraryAddress;
        private System.Windows.Forms.Button btnChoseDictionary;
        private System.Windows.Forms.ContextMenuStrip contMSTreeViewFile;
        private System.Windows.Forms.ToolStripMenuItem tSMIOpenFile;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStrip tSTreeActions;
        private System.Windows.Forms.ToolStripButton tSBCollapseAll;
        private System.Windows.Forms.ToolStripButton tSBOpenFile;
        private System.Windows.Forms.ToolStripButton tSBOpenFolder;
        private System.Windows.Forms.ToolStripMenuItem tSMIOpenFileDirectory;
        private System.Windows.Forms.ToolStripButton tSBUpdateTree;
        private System.Windows.Forms.ContextMenuStrip contMSTreeViewDirectory;
        private System.Windows.Forms.ToolStripMenuItem tSMIOpenFolder;
        private System.Windows.Forms.ToolStripMenuItem tSMIRefreshFolder;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tSMIFormListOperations;
        private System.Windows.Forms.ToolStripStatusLabel tSSLLibraryRoot;
        private System.Windows.Forms.ToolStripStatusLabel tSSLSearchResultCount;
        private System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.TreeView tVLibraryTree;
        private System.Windows.Forms.ToolStripMenuItem tSMIEdit;
        private System.Windows.Forms.ToolStripMenuItem tSMIFind;
    }
}


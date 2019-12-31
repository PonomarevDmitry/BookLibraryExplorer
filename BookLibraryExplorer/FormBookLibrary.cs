using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;

namespace BookLibraryExplorer
{
    public partial class FormBookLibrary : Form, IFormConfiguration
    {
        private string defaultLibraryPath = string.Empty;

        public FormBookLibrary()
        {
            InitializeComponent();

            LoadFormConfiguration();
        }

        #region IFormConfiguration Members

        public void LoadFormConfiguration()
        {
            ProgramConfiguraton.LoadFormParams(this, ConfigFormOption.Location | ConfigFormOption.Maximized | ConfigFormOption.Size);

            this.FormClosed += new FormClosedEventHandler(FormBookLibrary_FormClosed);
        }

        void FormBookLibrary_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveFormConfiguration();
        }

        public void SaveFormConfiguration()
        {
            ProgramConfiguraton.SaveFormParams(this, ConfigFormOption.Location | ConfigFormOption.Maximized | ConfigFormOption.Size);

            ProgramConfiguraton.SaveFormCustomParameter(this, "DefaultLibraryPath", this.defaultLibraryPath);
            if (!string.IsNullOrEmpty(this.defaultLibraryPath) && Directory.Exists(this.defaultLibraryPath))
            {
                ProgramConfiguraton.SaveExpandedTreeNode(tVLibraryTree);
            }

            ProgramConfiguraton.SaveXmlConfig();
        }

        private void FormBookLibrary_Load(object sender, EventArgs e)
        {
            string text = ProgramConfiguraton.LoadFormCustomParameter(this, "DefaultLibraryPath");
            if (!string.IsNullOrEmpty(text))
            {
                if (HandleNewLibraryDirectory(Path.Combine(Environment.CurrentDirectory, text)))
                {
                    ProgramConfiguraton.LoadExpandedTreeNode(tVLibraryTree);
                }
            }
        }

        #endregion IFormConfiguration Members

        #region Обработка корневого каталога.

        private void btnChoseDictionary_Click(object sender, EventArgs e)
        {
            SelectLibraryDirectory();
        }

        private void SelectLibraryDirectory()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;

            if (Directory.Exists(this.defaultLibraryPath))
            {
                dialog.SelectedPath = this.defaultLibraryPath;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                HandleNewLibraryDirectory(dialog.SelectedPath);
            }
        }

        private bool HandleNewLibraryDirectory(string path)
        {
            bool result = false;

            this.defaultLibraryPath = string.Empty;
            tBLibraryAddress.Text = string.Empty;
            tSSLLibraryRoot.Text = string.Empty;

            ClearTreeView();

            if (Directory.Exists(path))
            {
                result = true;

                this.defaultLibraryPath = path;
                tBLibraryAddress.Text = path;
                tSSLLibraryRoot.Text = path;

                DirectoryInfo rootFolder = new DirectoryInfo(path);

                LibraryFolder libraryRootFolder = LibraryFolder.CreateFolder(rootFolder);

                TreeNode rootNode = CreateTreeNodeForFolder(libraryRootFolder);

                tVLibraryTree.SuspendLayout();
                tVLibraryTree.BeginUpdate();

                SynchronizeSubNodes(rootNode, libraryRootFolder);

                tVLibraryTree.Nodes.Add(rootNode);

                rootNode.Expand();

                SetNodeName(rootNode);

                tVLibraryTree.EndUpdate();
                tVLibraryTree.ResumeLayout(true);
            }

            return result;
        }

        private void ClearTreeView()
        {
            tVLibraryTree.SuspendLayout();
            tVLibraryTree.BeginUpdate();

            tVLibraryTree.Nodes.Clear();

            tVLibraryTree.EndUpdate();
            tVLibraryTree.ResumeLayout(true);
        }



        private void SynchronizeSubNodes(TreeNode folderNode, LibraryFolder libraryFolder)
        {
            foreach (LibraryFolder folder in libraryFolder.SubFolders)
            {
                TreeNode subNode = CreateTreeNodeForFolder(folder);
                SynchronizeSubNodes(subNode, folder);

                folderNode.Nodes.Add(subNode);
            }

            SynchronizeSubFiles(folderNode, libraryFolder);
        }

        private void SynchronizeSubFiles(TreeNode folderNode, LibraryFolder libraryFolder)
        {
            foreach (LibraryFile file in libraryFolder.SubFiles)
            {
                TreeNode subNode = CreateTreeNodeForFile(file);
                folderNode.Nodes.Add(subNode);
            }
        }

        private void SetNodeName(TreeNode node)
        {
            node.Name = node.FullPath;
            foreach (TreeNode item in node.Nodes)
            {
                SetNodeName(item);
            }
        }

        #region Создание и доп.функции по работе с элементами дерева.

        private TreeNode CreateTreeNodeForFolder(LibraryFolder folderInfo)
        {
            TreeNode folderNode = new TreeNode(folderInfo.DirectoryName);
            folderNode.Tag = folderInfo;

            folderNode.ImageKey = "folder";
            folderNode.SelectedImageKey = "folder";

            folderNode.ContextMenuStrip = contMSTreeViewDirectory;

            return folderNode;
        }

        public bool IsDirectoryInfoTreeNode(TreeNode treeNode)
        {
            bool result = false;

            if (treeNode != null && treeNode.Tag != null && treeNode.Tag is LibraryFolder)
            {
                result = true;
            }

            return result;
        }

        private TreeNode CreateTreeNodeForFile(LibraryFile fileInFolder)
        {
            TreeNode fileNode = new TreeNode(fileInFolder.FileName);
            fileNode.Tag = fileInFolder;

            string extension = fileInFolder.FileExtension;

            if (imageList.Images.ContainsKey(extension))
            {
                fileNode.ImageKey = extension;
                fileNode.SelectedImageKey = extension;
            }
            else
            {
                fileNode.ImageKey = "file";
                fileNode.SelectedImageKey = "file";
            }

            fileNode.ContextMenuStrip = contMSTreeViewFile;

            return fileNode;
        }

        public bool IsFileTreeNode(TreeNode treeNode)
        {
            bool result = false;

            if (treeNode != null && treeNode.Tag != null && treeNode.Tag is LibraryFile)
            {
                result = true;
            }

            return result;
        }

        #endregion Создание и доп.функции по работе с элементами дерева.

        #endregion Обработка корневого каталога.

        #region Действия над деревом.

        private void tVLibraryTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            OpenTagFile(e.Node);
        }

        public void OpenTagFile(TreeNode treeNode)
        {
            if (IsFileTreeNode(treeNode))
            {
                LibraryFile labFile = (LibraryFile)treeNode.Tag;

                labFile.OpenFile();
            }
        }

        public void OpenTagFolder(TreeNode treeNode)
        {
            if (treeNode != null && treeNode.Tag != null)
            {
                if (IsDirectoryInfoTreeNode(treeNode))
                {
                    LibraryFolder libFolder = (LibraryFolder)treeNode.Tag;

                    libFolder.OpenFolder();
                }
                else if (IsFileTreeNode(treeNode))
                {
                    LibraryFile labFile = (LibraryFile)treeNode.Tag;

                    labFile.OpenFileFolder();
                }
            }
        }

        #endregion Действия над деревом.

        #region Контекстные меню.

        TreeNode clickedNode = null;

        private void tSMIOpenFile_Click(object sender, EventArgs e)
        {
            if (clickedNode != null)
            {
                OpenTagFile(clickedNode);
            }
        }

        private void tSMIOpenFileDirectory_Click(object sender, EventArgs e)
        {
            if (clickedNode != null)
            {
                OpenTagFolder(clickedNode);
            }
        }

        private void tSMIRefreshFolder_Click(object sender, EventArgs e)
        {
            if (clickedNode != null)
            {
                RefreshFolderContents(clickedNode);
            }
        }

        private void contMSTreeView_Opened(object sender, EventArgs e)
        {
            ContextMenuStrip menu = (ContextMenuStrip)sender;

            Point point = new Point(menu.Left, menu.Top);

            point = tVLibraryTree.PointToClient(point);

            TreeNode item = tVLibraryTree.GetNodeAt(point);

            clickedNode = item;
        }

        #endregion Контекстные меню.

        #region Действия панели управления.

        private void tVLibraryTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                tSBOpenFolder.Visible = tSBOpenFolder.Enabled = true;
                tSBOpenFile.Visible = tSBOpenFile.Enabled = IsFileTreeNode(e.Node);

                string tsbFolderName = string.Empty;

                if (IsFileTreeNode(e.Node))
                {
                    tsbFolderName = "Расположение файла";
                }
                else if (IsDirectoryInfoTreeNode(e.Node))
                {
                    tsbFolderName = "Открыть папку";
                }

                tSBOpenFolder.Text = tsbFolderName;
            }
            else
            {
                tSBOpenFile.Visible = tSBOpenFile.Enabled = false;
                tSBOpenFolder.Visible = tSBOpenFolder.Enabled = false;
            }
        }

        private void tSBUpdateTree_Click(object sender, EventArgs e)
        {
            HandleNewLibraryDirectory(this.defaultLibraryPath);
        }

        private void tSBCollapseAll_Click(object sender, EventArgs e)
        {
            tVLibraryTree.SuspendLayout();
            tVLibraryTree.BeginUpdate();

            tVLibraryTree.CollapseAll();
            if (tVLibraryTree.Nodes.Count == 1)
            {
                tVLibraryTree.Nodes[0].Expand();
            }

            tVLibraryTree.EndUpdate();
            tVLibraryTree.ResumeLayout(true);
        }

        private void tSBOpenFile_Click(object sender, EventArgs e)
        {
            OpenTagFile(tVLibraryTree.SelectedNode);
        }

        private void tSBOpenFolder_Click(object sender, EventArgs e)
        {
            OpenTagFolder(tVLibraryTree.SelectedNode);
        }

        #endregion Действия панели управления.

        #region Процедуры обновления всего дерева или только файлов одной папки.

        private void RefreshFolderContents(TreeNode folderNode)
        {
            if (IsDirectoryInfoTreeNode(folderNode))
            {
                LibraryFolder libFolder = (LibraryFolder)folderNode.Tag;

                libFolder.RefreshFullContent();

                tVLibraryTree.BeginUpdate();

                if (libFolder.SubFiles.Count > 0 || libFolder.SubFolders.Count > 0)
                {
                    folderNode.Nodes.Clear();

                    SynchronizeSubNodes(folderNode, libFolder);

                    tVLibraryTree.SelectedNode = folderNode;
                }
                else
                {
                    folderNode.Parent.Nodes.Remove(folderNode);
                }

                tVLibraryTree.EndUpdate();
            }
        }

        private void RefreshFolderContentsForFiles(TreeNode folderNode)
        {
            if (IsDirectoryInfoTreeNode(folderNode))
            {
                tVLibraryTree.BeginUpdate();

                RemoveFileNodes(folderNode);

                LibraryFolder libFolder = (LibraryFolder)folderNode.Tag;

                libFolder.RefreshFileContent();

                SynchronizeSubFiles(folderNode, libFolder);

                tVLibraryTree.SelectedNode = folderNode;

                tVLibraryTree.EndUpdate();
            }
        }

        private void RemoveFileNodes(TreeNode folderNode)
        {
            Collection<TreeNode> nodeForDelete = new Collection<TreeNode>();

            foreach (TreeNode node in folderNode.Nodes)
            {
                if (IsFileTreeNode(node))
                {
                    nodeForDelete.Add(node);
                }
            }

            foreach (TreeNode node in nodeForDelete)
            {
                folderNode.Nodes.Remove(node);
            }
        }

        #endregion Процедуры обновления всего дерева или только файлов одной папки.

        #region Копирование или перемещение файлов с помощью Drag-and-Drop операций.

        private void tVLibraryTree_DragOver(object sender, DragEventArgs e)
        {
            bool canAdd = false;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Point treePoint = tVLibraryTree.PointToClient(new Point(e.X, e.Y));

                TreeNode node = tVLibraryTree.GetNodeAt(treePoint);

                if (node != null)
                {
                    if (IsDirectoryInfoTreeNode(node))
                    {
                        tVLibraryTree.SelectedNode = node;
                    }
                    else if (IsFileTreeNode(node))
                    {
                        tVLibraryTree.SelectedNode = node.Parent;
                    }

                    string[] filePathCollection = (string[])e.Data.GetData(DataFormats.FileDrop);

                    foreach (string filePath in filePathCollection)
                    {
                        if (File.Exists(filePath) && LibraryFile.IsInterestingFile(Path.GetExtension(filePath)))
                        {
                            canAdd = true;
                            break;
                        }
                    }
                }
            }

            if (canAdd)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

            this.Activate();
            this.Show();
        }

        private void tVLibraryTree_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Point treePoint = tVLibraryTree.PointToClient(new Point(e.X, e.Y));

                TreeNode node = tVLibraryTree.GetNodeAt(treePoint);

                if (node != null)
                {
                    TreeNode folderNode = null;

                    if (IsDirectoryInfoTreeNode(node))
                    {
                        folderNode = node;
                    }
                    else if (IsFileTreeNode(node))
                    {
                        if (IsDirectoryInfoTreeNode(node.Parent))
                        {
                            folderNode = node.Parent;
                        }
                    }

                    if (folderNode != null)
                    {
                        LibraryFolder libFolder = (LibraryFolder)folderNode.Tag;

                        string[] filePathCollection = (string[])e.Data.GetData(DataFormats.FileDrop);

                        bool allRight = true;

                        StringBuilder sb = new StringBuilder();
                        foreach (string filePath in filePathCollection)
                        {
                            if (File.Exists(filePath))
                            {
                                string newFileName = Path.GetFileName(filePath);

                                string newFilePath = Path.Combine(libFolder.FullName, newFileName);

                                if (File.Exists(newFilePath))
                                {
                                    allRight = false;
                                    sb.AppendLine(newFileName);
                                }
                            }
                        }

                        if (!allRight)
                        {
                            MessageBox.Show(this, "В папке уже существуют следующие файлы :\r\n\r\n" + sb.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        bool moveFiles = false;

                        string question = string.Format("Произвести перемещение в папку {0}?", libFolder.DirectoryName);
                        System.Windows.Forms.DialogResult questionResult = MessageBox.Show(this, question, "Вопрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                        if (questionResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            moveFiles = true;
                        }
                        else if (questionResult == System.Windows.Forms.DialogResult.No)
                        {
                            moveFiles = false;
                        }
                        else
                        {
                            return;
                        }

                        foreach (string filePath in filePathCollection)
                        {
                            if (File.Exists(filePath))
                            {
                                string newFilePath = Path.Combine(libFolder.FullName, Path.GetFileName(filePath));

                                if (!File.Exists(newFilePath))
                                {
                                    if (moveFiles)
                                    {
                                        File.Move(filePath, newFilePath);
                                    }
                                    else
                                    {
                                        File.Copy(filePath, newFilePath);
                                    }
                                }
                            }
                        }

                        RefreshFolderContentsForFiles(folderNode);
                    }
                }
            }
        }

        #endregion Копирование или перемещение файлов с помощью Drag-and-Drop операций.

        #region Отображение дочерних форм.

        private void ShowChildForm(Form form)
        {
            form.FormClosed += new FormClosedEventHandler(childForm_FormClosed);
            form.Show(this);
            form.Select();

            this.Hide();
            this.Enabled = false;
        }

        void childForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
            this.Show();
            this.Select();
        }

        #endregion Отображение дочерних форм

        private void tSMIFormListOperations_Click(object sender, EventArgs e)
        {
            FormListOperation form = new FormListOperation();
            ShowChildForm(form);
        }

        private void tVLibraryTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tVLibraryTree.SelectedNode != null)
                {
                    if (IsDirectoryInfoTreeNode(tVLibraryTree.SelectedNode))
                    {
                        if (tVLibraryTree.SelectedNode.IsExpanded)
                        {
                            tVLibraryTree.SelectedNode.Collapse(true);
                        }
                        else
                        {
                            tVLibraryTree.SelectedNode.Expand();
                        }

                    }
                    else if (IsFileTreeNode(tVLibraryTree.SelectedNode))
                    {
                        OpenTagFile(tVLibraryTree.SelectedNode);
                    }
                }
            }
        }

        private void tSMIFind_Click(object sender, EventArgs e)
        {
            Form findForm = null;

            foreach (Form item in this.OwnedForms)
            {
                if (item is FormBookSearch)
                {
                    findForm = item;
                    break;
                }
            }

            if (findForm == null)
            {
                findForm = new FormBookSearch(this);
            }

            findForm.Activate();
            findForm.Show();
        }
    }
}

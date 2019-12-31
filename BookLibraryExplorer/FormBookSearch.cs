using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BookLibraryExplorer
{
    public partial class FormBookSearch : Form, IFormConfiguration
    {
        private FormBookLibrary ownerForm = null;

        public FormBookSearch(FormBookLibrary parentForm)
        {
            InitializeComponent();

            this.ownerForm = parentForm;
            this.Owner = parentForm;

            LoadFormConfiguration();
        }

        #region Инициализация.

        public void LoadFormConfiguration()
        {
            ProgramConfiguraton.LoadFormParams(this, ConfigFormOption.Location | ConfigFormOption.Maximized | ConfigFormOption.Size);
        }

        public void SaveFormConfiguration()
        {
            ProgramConfiguraton.SaveFormParams(this, ConfigFormOption.Location | ConfigFormOption.Maximized | ConfigFormOption.Size);

            ProgramConfiguraton.SaveXmlConfig();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            SaveFormConfiguration();
        }

        #endregion Инициализация.

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            tBBookSearch.Select();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            tBBookSearch.Select();
        }

        #region Процедуры поиска в дереве по названию книги.

        private void tBBookSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformBookSearch();
            }
        }

        private void btnPerformSearch_Click(object sender, EventArgs e)
        {
            PerformBookSearch();
        }

        private void PerformBookSearch()
        {
            string searchPattern = tBBookSearch.Text.Replace(" ", "");
            if (!string.IsNullOrEmpty(searchPattern))
            {
                lVBookSearch.BeginUpdate();

                lVBookSearch.Items.Clear();

                SearchAndAddIntoListView(this.ownerForm.tVLibraryTree.Nodes, searchPattern);

                if (lVBookSearch.Items.Count > 0)
                {
                    colHeadFileName.Width = -1;
                    colHeadFilePath.Width = -1;
                }
                else
                {
                    SetDefaultListViewColumnWidth();
                }

                lVBookSearch.EndUpdate();

                tSSLSearchResultCount.BorderSides = ToolStripStatusLabelBorderSides.Left;
                tSSLSearchResultCount.Text = string.Format("Найдено файлов в дереве: {0}.", lVBookSearch.Items.Count);

                if (lVBookSearch.Items.Count == 0)
                {
                    tSTreeActions.Visible = false;
                    MessageBox.Show(this, "Ничего не найдено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    tSTreeActions.Visible = true;
                }
            }
        }

        private void SetDefaultListViewColumnWidth()
        {
            colHeadFileName.Width = 200;
            colHeadFilePath.Width = -2;
        }

        private void SearchAndAddIntoListView(TreeNodeCollection treeNodeCollection, string searchPattern)
        {
            foreach (TreeNode node in treeNodeCollection)
            {
                if (this.ownerForm.IsFileTreeNode(node))
                {
                    LibraryFile labFile = (LibraryFile)node.Tag;

                    if (FileNameMatchPattern(labFile.FileName, searchPattern))
                    {
                        ListViewItem lvi = new ListViewItem(labFile.FileName, node.ImageKey);
                        lvi.Tag = node;
                        lvi.SubItems.Add(node.Parent.FullPath);

                        lVBookSearch.Items.Add(lvi);
                    }
                }

                SearchAndAddIntoListView(node.Nodes, searchPattern);
            }
        }

        private bool FileNameMatchPattern(string fileName, string pattern)
        {
            string patternTemp = ConvertToMatchFormat(pattern);
            string fileNameTemp = ConvertToMatchFormat(fileName);

            return fileNameTemp.Contains(patternTemp);
        }

        private string ConvertToMatchFormat(string str)
        {
            string result = str;

            result = result.ToLower();
            result = result.Replace(" ", "");
            result = result.Replace(".", "");
            result = result.Replace(",", "");
            result = result.Replace("-", "");

            result = RemoveDoubleSymbols(result);

            return result;
        }

        private string RemoveDoubleSymbols(string str)
        {
            string result = str;

            Regex doubleSymbolPattern = new Regex(@"(?<Ch>.{1})\k<Ch>");

            while (doubleSymbolPattern.IsMatch(result))
            {
                MatchCollection matches = doubleSymbolPattern.Matches(result);

                foreach (Match item in matches)
                {
                    string value = item.Groups["Ch"].Value;

                    result = result.Replace(value + value, value);
                }
            }
            return result;
        }

        #endregion Процедуры поиска в дереве по названию книги.

        #region События списка результатов поиска.

        /// <summary>
        /// Двойной клик на список поиска.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lVBookSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lVBookSearch.GetItemAt(e.X, e.Y);

            if (item != null && item.Tag != null && item.Tag is TreeNode)
            {
                this.ownerForm.OpenTagFile(item.Tag as TreeNode);
            }
        }

        /// <summary>
        /// Пункт контекстного меню списка поиска "Открыть файл".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tSMILVOpenFile_Click(object sender, EventArgs e)
        {
            if (lVBookSearch.SelectedItems.Count == 1)
            {
                ListViewItem item = lVBookSearch.SelectedItems[0];

                if (item != null && item.Tag != null && item.Tag is TreeNode)
                {
                    this.ownerForm.OpenTagFile(item.Tag as TreeNode);
                }
            }
        }

        /// <summary>
        /// Пункт контекстного меню списка поиска "Выбрать в дереве".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tSMISelectInTree_Click(object sender, EventArgs e)
        {
            if (lVBookSearch.SelectedItems.Count == 1)
            {
                ListViewItem item = lVBookSearch.SelectedItems[0];

                if (item != null && item.Tag != null && item.Tag is TreeNode)
                {
                    TreeNode node = item.Tag as TreeNode;

                    this.ownerForm.tVLibraryTree.BeginUpdate();
                    this.ownerForm.tVLibraryTree.SelectedNode = node;
                    this.ownerForm.tVLibraryTree.SelectedNode.EnsureVisible();

                    this.ownerForm.tVLibraryTree.Select();

                    this.ownerForm.tVLibraryTree.EndUpdate();

                    this.Hide();

                    this.ownerForm.Activate();
                    this.ownerForm.Show();
                }
            }
        }

        /// <summary>
        /// Пункт контекстного меню списка поиска "Открыть папку".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tSMIOpenFolderInLV_Click(object sender, EventArgs e)
        {
            if (lVBookSearch.SelectedItems.Count == 1)
            {
                ListViewItem item = lVBookSearch.SelectedItems[0];

                if (item != null && item.Tag != null && item.Tag is TreeNode)
                {
                    this.ownerForm.OpenTagFolder(item.Tag as TreeNode);
                }
            }
        }

        private void lVBookSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lVBookSearch.SelectedItems.Count == 1)
                {
                    ListViewItem item = lVBookSearch.SelectedItems[0];

                    if (item != null && item.Tag != null && item.Tag is TreeNode)
                    {
                        this.ownerForm.OpenTagFile(item.Tag as TreeNode);
                    }
                }
            }
        }

        #endregion События списка результатов поиска.

        private void ClearSearchListView()
        {
            lVBookSearch.BeginUpdate();

            lVBookSearch.Items.Clear();
            SetDefaultListViewColumnWidth();

            lVBookSearch.EndUpdate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();

                this.ownerForm.Activate();
                this.ownerForm.Show();
            }
        }

        private void lVBookSearch_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item != null && e.Item.Tag != null && e.IsSelected)
            {
                TreeNode node = (TreeNode)e.Item.Tag;

                tSBOpenFolder.Visible = tSBOpenFolder.Enabled = true;
                tSBOpenFile.Visible = tSBOpenFile.Enabled = true;
            }
            else
            {
                tSBOpenFile.Visible = tSBOpenFile.Enabled = false;
                tSBOpenFolder.Visible = tSBOpenFolder.Enabled = false;
            }
        }
    }
}

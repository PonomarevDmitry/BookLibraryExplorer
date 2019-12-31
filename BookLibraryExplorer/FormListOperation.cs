using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace BookLibraryExplorer
{
    public partial class FormListOperation : Form, IFormConfiguration
    {
        private string defaultFilePath = string.Empty;
        private string defaultFolderPath = string.Empty;

        private const string defaultRootNodeName = "BookLibraryFileList";

        public FormListOperation()
        {
            InitializeComponent();

            LoadFormConfiguration();
        }

        #region IFormConfiguration Members

        public void LoadFormConfiguration()
        {
            string text = string.Empty;

            ProgramConfiguraton.LoadFormParams(this, ConfigFormOption.Location | ConfigFormOption.Maximized | ConfigFormOption.Size);

            text = ProgramConfiguraton.LoadFormCustomParameter(this, "defaultFolderPath");
            if (!string.IsNullOrEmpty(text) && Directory.Exists(text))
            {
                this.defaultFolderPath = text;
            }

            text = ProgramConfiguraton.LoadFormCustomParameter(this, "defaultFilePath");
            if (!string.IsNullOrEmpty(text) && Directory.Exists(text))
            {
                this.defaultFilePath = text;
            }

            this.FormClosed += new FormClosedEventHandler(FormListOperation_FormClosed);
        }

        void FormListOperation_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveFormConfiguration();
        }

        public void SaveFormConfiguration()
        {
            ProgramConfiguraton.SaveFormParams(this, ConfigFormOption.Location | ConfigFormOption.Maximized | ConfigFormOption.Size);

            ProgramConfiguraton.SaveFormCustomParameter(this, "defaultFilePath", this.defaultFilePath);
            ProgramConfiguraton.SaveFormCustomParameter(this, "defaultFolderPath", this.defaultFolderPath);

            ProgramConfiguraton.SaveXmlConfig();
        }

        #endregion IFormConfiguration Members

        #region Работа с повторами файлов в папке.

        //private int repeatCount = 0;
        //private string repeatName = string.Empty;

        //private void tSMICheckForFileRepeat_Click(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(this.defaultLibraryPath))
        //    {
        //        this.repeatCount = 0;

        //        DirectoryInfo rootFolder = new DirectoryInfo(this.defaultLibraryPath);
        //        if (rootFolder.Exists)
        //        {
        //            Dictionary<string, FileInfo> uniqueFiles = new Dictionary<string, FileInfo>();

        //            AddRepeats(uniqueFiles, rootFolder);

        //            MessageBox.Show(string.Format("{0} {1}", this.repeatCount.ToString(), this.repeatName));
        //        }
        //    }
        //}

        //private void AddRepeats(Dictionary<string, FileInfo> uniqueFiles, DirectoryInfo folder)
        //{
        //    try
        //    {
        //        FileInfo[] fileCollection = folder.GetFiles();

        //        foreach (FileInfo itemFileInfo in fileCollection)
        //        {
        //            string hashString = LibraryFile.GetHashString(itemFileInfo);

        //            if (!string.IsNullOrEmpty(hashString))
        //            {
        //                if (uniqueFiles.ContainsKey(hashString))
        //                {
        //                    this.repeatCount++;
        //                    repeatName = itemFileInfo.FullName;
        //                }
        //                else
        //                {
        //                    uniqueFiles.Add(hashString, itemFileInfo);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    DirectoryInfo[] subFolderCollection = folder.GetDirectories();
        //    foreach (DirectoryInfo subFolder in subFolderCollection)
        //    {
        //        AddRepeats(uniqueFiles, subFolder);
        //    }
        //}

        #endregion Работа с повторами файлов в папке.

        #region Создание списка файлов.

        private void tSMICreteFileList_Click(object sender, EventArgs e)
        {
            if (bgWorker.IsBusy)
            {
                MessageBox.Show(this, "Идет операция. Подождите.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;

            if (Directory.Exists(this.defaultFolderPath))
            {
                dialog.SelectedPath = this.defaultFolderPath;
            }

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.AddExtension = true;
                saveFileDialog.OverwritePrompt = true;

                saveFileDialog.DefaultExt = ".xml";
                saveFileDialog.Filter = "xml |*.xml";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (Directory.Exists(this.defaultFilePath))
                {
                    saveFileDialog.InitialDirectory = this.defaultFilePath;
                }

                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    CreateNewListFile(dialog.SelectedPath, saveFileDialog.FileName);
                }
            }
        }

        private class FileListArgument
        {
            public string FolderPath { get; private set; }
            public string FilePath { get; private set; }

            public FileListArgument(string folder, string file)
            {
                this.FolderPath = folder;
                this.FilePath = file;
            }
        }

        private void CreateNewListFile(string folderPath, string targetFilePath)
        {
            this.defaultFilePath = targetFilePath;
            this.defaultFolderPath = folderPath;

            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;

            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);

            tSProgressBar.Style = ProgressBarStyle.Marquee;

            FileListArgument arg = new FileListArgument(folderPath, targetFilePath);
            bgWorker.RunWorkerAsync(arg);
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            FileListArgument arg = e.Argument as FileListArgument;

            DirectoryInfo folder = new DirectoryInfo(arg.FolderPath);
            if (folder.Exists)
            {
                XmlDocument xmlDoc = new XmlDocument();

                XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
                xmlDoc.AppendChild(declaration);

                XmlNode root = xmlDoc.CreateElement(defaultRootNodeName);
                xmlDoc.AppendChild(root);

                XmlAttribute folderPath = xmlDoc.CreateAttribute("FolderPath");
                root.Attributes.Append(folderPath);
                folderPath.Value = folder.FullName;

                FillFiles(root, folder, arg.FolderPath);

                xmlDoc.Save(arg.FilePath);
            }
        }

        private void FillFiles(XmlNode root, DirectoryInfo folder, string parentFolderName)
        {
            DirectoryInfo[] subFolderCollection = folder.GetDirectories();
            foreach (DirectoryInfo subFolder in subFolderCollection)
            {
                FillFiles(root, subFolder, parentFolderName);
            }

            IEnumerable<FileInfo> childFiles = folder.EnumerateFiles();
            //IList<FileInfo> childFiles = LibraryFile.GetChildFiles(folder);

            foreach (FileInfo fileInFolder in childFiles)
            {
                XmlNode fileNode = root.OwnerDocument.CreateElement("File");
                root.AppendChild(fileNode);

                XmlAttribute fileName = root.OwnerDocument.CreateAttribute("FileName");
                fileNode.Attributes.Append(fileName);
                fileName.Value = fileInFolder.Name;

                XmlAttribute fileHashString = root.OwnerDocument.CreateAttribute("HashString");
                fileNode.Attributes.Append(fileHashString);
                fileHashString.Value = LibraryFile.GetHashString(fileInFolder);

                XmlAttribute fileExtension = root.OwnerDocument.CreateAttribute("Extension");
                fileNode.Attributes.Append(fileExtension);
                fileExtension.Value = fileInFolder.Extension.ToLower();

                XmlAttribute filePath = root.OwnerDocument.CreateAttribute("FilePath");
                fileNode.Attributes.Append(filePath);
                filePath.Value = fileInFolder.DirectoryName;

                string relative = fileInFolder.DirectoryName.Replace(parentFolderName, string.Empty);
                relative = relative.TrimStart('\\');

                XmlAttribute relativePath = root.OwnerDocument.CreateAttribute("RelativePath");
                fileNode.Attributes.Append(relativePath);
                relativePath.Value = relative;
            }
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tSProgressBar.Style = ProgressBarStyle.Blocks;
            MessageBox.Show(this, "Список файлов сформирован.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion Создание списка файлов.

        private void FormListOperation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}

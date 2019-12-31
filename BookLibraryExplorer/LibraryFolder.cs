using System.IO;
using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Xml;

namespace BookLibraryExplorer
{
    public class LibraryFolder
    {
        private DirectoryInfo libraryFolder = null;

        public LibraryFolder(DirectoryInfo dirInfo)
        {
            this.libraryFolder = dirInfo;
        }

        private List<LibraryFolder> subFolders = new List<LibraryFolder>();
        public List<LibraryFolder> SubFolders
        {
            get { return this.subFolders; }
        }

        private List<LibraryFile> subFiles = new List<LibraryFile>();
        public List<LibraryFile> SubFiles
        {
            get { return this.subFiles; }
        }

        public string DirectoryName
        {
            get { return this.libraryFolder.Name; }
        }

        public string FullName
        {
            get { return this.libraryFolder.FullName; }
        }

        public void OpenFolder()
        {
            if (this.libraryFolder.Exists)
            {
                try
                {
                    Process.Start(this.libraryFolder.FullName);
                }
                catch (Exception)
                { }
            }
        }

        public void RefreshFullContent()
        {
            RefreshFolderContent();
            RefreshFileContent();
        }

        public void RefreshFolderContent()
        {
            this.subFolders.Clear();

            if (this.libraryFolder.Exists)
            {
                DirectoryInfo[] subFolderCollection = null;

                try
                {
                    subFolderCollection = this.libraryFolder.GetDirectories();
                }
                catch (Exception)
                {

                }

                if (subFolderCollection != null)
                {
                    foreach (DirectoryInfo folder in subFolderCollection)
                    {
                        LibraryFolder libFolder = LibraryFolder.CreateFolder(folder);

                        if (libFolder.subFiles.Count > 0 || libFolder.subFolders.Count > 0)
                        {
                            this.subFolders.Add(libFolder);
                        }
                    }
                }
            }
        }

        public void RefreshFileContent()
        {
            this.subFiles.Clear();

            if (this.libraryFolder.Exists)
            {
                IList<FileInfo> childFiles = null;

                try
                {
                    childFiles = LibraryFile.GetChildFiles(this.libraryFolder);
                }
                catch (Exception)
                {

                }

                if (childFiles != null)
                {
                    foreach (FileInfo fileInFolder in childFiles)
                    {
                        LibraryFile file = new LibraryFile(fileInFolder);
                        this.subFiles.Add(file);
                    }
                }
            }
        }

        private const string xmlNodeName = "LibraryFolder";

        public XmlNode CreateXmlDescription(XmlDocument doc)
        {
            XmlNode result = doc.CreateElement(xmlNodeName);

            XmlAttribute attr;

            attr = doc.CreateAttribute("Name");
            attr.Value = this.libraryFolder.Name;
            result.Attributes.Append(attr);

            attr = doc.CreateAttribute("FullName");
            attr.Value = this.libraryFolder.FullName;
            result.Attributes.Append(attr);

            foreach (LibraryFolder folder in this.subFolders)
            {
                XmlNode folderNode = folder.CreateXmlDescription(doc);

                result.AppendChild(folderNode);
            }

            foreach (LibraryFile file in this.subFiles)
            {
                XmlNode fileNode = file.CreateXmlDescription(doc);

                result.AppendChild(fileNode);
            }

            return result;
        }

        public static LibraryFolder CreateLibraryFolderFromXml(XmlNode node)
        {
            LibraryFolder result = null;

            if (node.Name == xmlNodeName)
            {
                XmlAttribute attr;

                attr = node.Attributes["FullName"];
                if (attr != null && !string.IsNullOrEmpty(attr.Value))
                {
                    DirectoryInfo directory = new DirectoryInfo(attr.Value);

                    result = new LibraryFolder(directory);

                    XmlNodeList list;

                    list = node.SelectNodes(xmlNodeName);
                    foreach (XmlNode item in list)
                    {
                        LibraryFolder folder = LibraryFolder.CreateLibraryFolderFromXml(item);

                        if (folder.subFiles.Count > 0 || folder.subFolders.Count > 0)
                        {
                            result.subFolders.Add(folder);
                        }
                    }

                    list = node.SelectNodes(LibraryFile.xmlNodeName);
                    foreach (XmlNode item in list)
                    {
                        LibraryFile file = LibraryFile.CreateLibraryFileFromXml(item);

                        result.subFiles.Add(file);
                    }
                }
            }

            return result;
        }

        internal static LibraryFolder CreateFolder(DirectoryInfo parentFolder)
        {
            LibraryFolder result = new LibraryFolder(parentFolder);

            result.RefreshFullContent();

            return result;
        }
    }
}

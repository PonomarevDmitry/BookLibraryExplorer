using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace BookLibraryExplorer
{
    public class LibraryFile
    {
        public LibraryFile(FileInfo fileInfo)
        {
            this.libraryFileInfo = fileInfo;
        }

        private FileInfo libraryFileInfo = null;

        public string FileName
        {
            get { return this.libraryFileInfo.Name; }
        }

        public string FileExtension
        {
            get { return this.libraryFileInfo.Extension.ToLower(); }
        }

        private string hashString = string.Empty;
        public string HashString
        {
            get { return this.hashString; }
        }

        internal void OpenFile()
        {
            if (this.libraryFileInfo.Exists)
            {
                try
                {
                    Process.Start(this.libraryFileInfo.FullName);
                }
                catch (Exception)
                { }
            }
        }

        internal void OpenFileFolder()
        {
            //DirectoryInfo folder = this.libraryFileInfo.Directory;

            if (this.libraryFileInfo.Exists)
            {
                try
                {
                    //Process.Start(folder.FullName);

                    string fileString = @"/select, " + "\"" + this.libraryFileInfo.FullName + "\"";
                    Process.Start("explorer.exe", fileString);
                }
                catch (Exception)
                { }
            }
        }

        public const string xmlNodeName = "LibraryFile";

        public XmlNode CreateXmlDescription(XmlDocument doc)
        {
            XmlNode result = doc.CreateElement(xmlNodeName);

            XmlAttribute attr;

            attr = doc.CreateAttribute("Name");
            attr.Value = this.FileName;
            result.Attributes.Append(attr);

            attr = doc.CreateAttribute("FullName");
            attr.Value = this.libraryFileInfo.FullName;
            result.Attributes.Append(attr);

            attr = doc.CreateAttribute("FileExtension");
            attr.Value = this.FileExtension;
            result.Attributes.Append(attr);

            return result;
        }

        public static LibraryFile CreateLibraryFileFromXml(XmlNode node)
        {
            LibraryFile result = null;

            if (node.Name == xmlNodeName)
            {
                XmlAttribute attr;

                attr = node.Attributes["FullName"];
                if (attr != null && !string.IsNullOrEmpty(attr.Value))
                {
                    FileInfo file = new FileInfo(attr.Value);

                    result = new LibraryFile(file);
                }
            }

            return result;
        }

        #region Статические методы.

        private static Collection<string> fileExtensions = new Collection<string>() { ".pdf", ".djv", ".djvu", ".chm", ".doc" };

        //private static HashAlgorithm hashAlgorithm = SHA512Managed.Create();
        private static HashAlgorithm hashAlgorithm = MD5Cng.Create();

        public static string GetHashString(FileInfo targetFileInfo)
        {
            string result = string.Empty;

            if (targetFileInfo.Exists)
            {

                try
                {
                    FileStream fs = File.Open(targetFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);

                    using (fs)
                    {
                        result = ConvertHashToString(hashAlgorithm.ComputeHash(fs));

                        fs.Close();
                        fs.Dispose();
                    }
                }
                catch (Exception)
                {
                }
            }

            return result;
        }

        private static string ConvertHashToString(byte[] hashValue)
        {
            string result = string.Empty;

            for (int i = 0; i < hashValue.Length; i++)
            {
                result += string.Format("{0:X2}", hashValue[i]);

            }

            return result;
        }

        public static IList<FileInfo> GetChildFiles(DirectoryInfo parentFolder)
        {
            SortedList<string, FileInfo> list = new SortedList<string, FileInfo>();

            try
            {
                IEnumerable<FileInfo> fileCollection = parentFolder.EnumerateFiles();

                foreach (FileInfo item in fileCollection)
                {
                    if (IsInterestingFile(item.Extension))
                    {
                        list.Add(item.Name, item);
                    }
                }
            }
            catch (Exception)
            {
            }

            return list.Values;
        }

        public static bool IsInterestingFile(string extension)
        {
            return fileExtensions.Contains(extension.ToLower());
        }

        #endregion Статические методы.
    }
}

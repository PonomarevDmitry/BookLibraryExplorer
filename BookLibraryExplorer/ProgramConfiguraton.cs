using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace BookLibraryExplorer
{
    /// <summary>
    /// Свойства таблицы для загрузки/сохранения в конфиг-файле.
    /// </summary>
    [Flags]
    internal enum ConfigDataGridViewOption
    {
        /// <summary>
        /// Ширина столбцов.
        /// </summary>
        Width = 1,

        /// <summary>
        /// Отображение столбцов в списке.
        /// </summary>
        Visible = 2,

        /// <summary>
        /// Порядок столбцов в списке.
        /// </summary>
        DisplayIndex = 4/*,

        /// <summary>
        /// Сортировка списка
        /// </summary>
        SortText = 8*/
    }

    /// <summary>
    /// Свойства формы для загрузки/сохранения в конфиг-файле.
    /// </summary>
    [Flags]
    internal enum ConfigFormOption
    {
        /// <summary>
        /// Размер формы.
        /// </summary>
        Size = 1,

        /// <summary>
        /// Расположение формы.
        /// </summary>
        Location = 2,

        /// <summary>
        /// Свойство FormWindowState.Maximized формы.
        /// </summary>
        Maximized = 4
    }

    /// <summary>
    /// Интерфейс для MDI-дочерней формы MainForm для on-line загрузки/сохранения профилей.
    /// </summary>
    internal interface IFormConfiguration
    {
        /// <summary>
        /// Загрузить сохраненные настройки формы.
        /// </summary>
        void LoadFormConfiguration();

        /// <summary>
        /// Записать настройки формы в конфиг-файл.
        /// </summary>
        void SaveFormConfiguration();
    }

    /// <summary>
    /// Статический класс для работы с настройками формы.
    /// </summary>
    public static class ProgramConfiguraton
    {
        private const string defaultConfigFileName = "BookLibraryExplorer.conf";
        private const string defaultSubdirectoryName = "BookLibraryExplorer";
        private const string defaultRootNodeName = "BookLibraryExplorer_Configuration";

        /// <summary>
        /// XmlDocument с настройками программы.
        /// </summary>
        private static ConfigXmlDocument programParams = new ConfigXmlDocument();

        #region Стандартные имя файла и его местоположение.

        /// <summary>
        /// Стандартная папка для хранения конфиг-файла.
        /// </summary>
        private static string DefaultFileDirectory
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + defaultSubdirectoryName;
            }
        }

        /// <summary>
        /// Стандартное имя конфиг-файла.
        /// </summary>
        private static string DefaultFileName
        {
            get
            {
                return DefaultFileDirectory + @"\" + defaultConfigFileName;
            }
        }

        #endregion Стандартные имя файла и его местоположение.

        #region Работа с файлами.

        /// <summary>
        /// Загрузить конфиг-файл.
        /// </summary>
        internal static void LoadXmlConfig()
        {
            string fileName = DefaultFileName;

            bool create = false;

            if (File.Exists(fileName))
            {
                try
                {
                    programParams.Load(fileName);
                }
                catch (Exception)
                {
                    create = true;
                }
            }

            if (create)
            {
                programParams = new ConfigXmlDocument();

                XmlDeclaration declaration = programParams.CreateXmlDeclaration("1.0", "utf-8", "yes");
                programParams.AppendChild(declaration);
                XmlNode root = programParams.CreateElement(defaultRootNodeName);
                programParams.AppendChild(root);
            }
        }

        /// <summary>
        /// Сохранить конфиг-файл.
        /// </summary>
        internal static void SaveXmlConfig()
        {
            string dirName = DefaultFileDirectory;

            if (!Directory.Exists(DefaultFileDirectory))
            {
                Directory.CreateDirectory(DefaultFileDirectory);
            }

            programParams.Save(DefaultFileName);
        }

        /// <summary>
        /// Считать внешний конфиг-файл.
        /// </summary>
        /// <param name="fileName"></param>
        internal static void LoadXmlConfig(string fileName)
        {
            if (File.Exists(fileName))
            {
                programParams.Load(fileName);
                SaveXmlConfig();
            }
        }

        /// <summary>
        /// Сохранить во внешний файл содержимое текущего конфиг-файла.
        /// </summary>
        /// <param name="fileName"></param>
        internal static void SaveXmlConfig(string fileName)
        {
            SaveXmlConfig();
            programParams.Save(fileName);
        }

        #endregion Работа с файлами.

        #region Методы для работы интерфейсом форм.

        #region Формы.

        #region Свободный параметр формы.

        /// <summary>
        /// Загрузить свободный параметр формы.
        /// </summary>
        /// <param name="form">Форма, которой нужен параметр.</param>
        /// <param name="paramName">Имя параметра.</param>
        /// <returns>Значение параметра как строка, если параметра нет, то возвращается пустая строка.</returns>
        internal static string LoadFormCustomParameter(Form form, string paramName)
        {
            // Результат.
            string result = string.Empty;

            // Форма не нуль, имя формы не пустое, имя параметра не нуль.
            if (!string.IsNullOrEmpty(paramName) && form != null && !string.IsNullOrEmpty(form.Name))
            {
                // Корневой элемент конфигурации программы.
                XmlNode root = programParams[defaultRootNodeName];
                if (root != null)
                {
                    // Элемент формы.
                    XmlNode formNode = root[form.Name];
                    if (formNode != null)
                    {
                        // Элемент параметра.
                        XmlNode formParamValue = formNode[paramName];
                        if (formParamValue != null)
                        {
                            // Загрузка значения.
                            result = formParamValue.InnerText;
                        }
                    }
                }
            }

            // Проверка правильности сохранения параметра не входит в список обязанностей этой функции.
            return result;
        }

        /// <summary>
        /// Сохранить свободный параметр формы.
        /// </summary>
        /// <param name="form">Форма, которой принадлежит этот параметр.</param>
        /// <param name="paramName">Имя параметра.</param>
        /// <param name="paramValue">Значение параметра в виде строки.</param>
        internal static void SaveFormCustomParameter(Form form, string paramName, string paramValue)
        {
            // Форма не нуль, имя формы не нуль, имя параметра не нуль.
            if (!string.IsNullOrEmpty(paramName) && form != null && !string.IsNullOrEmpty(form.Name))
            {
                // Корневой элемент конфигурации программы.
                XmlNode root = programParams[defaultRootNodeName];
                if (root == null)
                {
                    // Создаем корень, если его нет.
                    root = programParams.CreateElement(defaultRootNodeName);
                    programParams.AppendChild(root);
                }

                // Элемент формы.
                XmlNode formNode = root[form.Name];
                if (formNode == null)
                {
                    // Создаем его, если нет.
                    formNode = programParams.CreateElement(form.Name);
                    root.AppendChild(formNode);
                }

                // Элемент параметра.
                XmlNode formParamValue = formNode[paramName];
                if (formParamValue == null)
                {
                    // Создаем его, если нет.
                    formParamValue = programParams.CreateElement(paramName);
                    formNode.AppendChild(formParamValue);
                }
                // Сохраняем значение параметра.
                formParamValue.InnerText = paramValue;
            }
        }

        #endregion Свободный параметр формы.

        #region Стандартные настройки форм.

        /// <summary>
        /// Загрузка стандартных параметров формы по указанному списку флагов.
        /// </summary>
        /// <param name="form">Форма для загрузки параметров.</param>
        /// <param name="options">Флаги параметров.</param>
        internal static void LoadFormParams(Form form, ConfigFormOption options)
        {
            // Форма не нулевая и с непустым именем.
            if (form != null && !string.IsNullOrEmpty(form.Name))
            {
                // Ищем конфигурационный корень в файле.
                XmlNode root = programParams[defaultRootNodeName];
                if (root != null)
                {
                    // Ищем элемент формы.
                    XmlNode formNode = root[form.Name];
                    if (formNode != null)
                    {
                        // Переменная для конвертирования текста элементов.
                        int value = 0;

                        // Если есть флаг размеров.
                        if ((options & ConfigFormOption.Size) == ConfigFormOption.Size)
                        {
                            // Ширина.
                            XmlNode formWidth = formNode["Width"];
                            if (formWidth != null)
                            {
                                if (int.TryParse(formWidth.InnerText, out value))
                                {
                                    form.Width = value;
                                }
                            }

                            // Высота.
                            XmlNode formHeight = formNode["Height"];
                            if (formHeight != null)
                            {
                                if (int.TryParse(formHeight.InnerText, out value))
                                {
                                    form.Height = value;
                                }
                            }
                        }

                        // Если есть флаг расположения.
                        if ((options & ConfigFormOption.Location) == ConfigFormOption.Location)
                        {
                            bool locationIsNull = true;

                            // Горизонталь.
                            XmlNode formLeft = formNode["Left"];
                            if (formLeft != null)
                            {
                                if (int.TryParse(formLeft.InnerText, out value))
                                {
                                    locationIsNull = false;
                                    form.Left = value;
                                }
                            }

                            // Вертикаль.
                            XmlNode formTop = formNode["Top"];
                            if (formTop != null)
                            {
                                if (int.TryParse(formTop.InnerText, out value))
                                {
                                    locationIsNull = false;
                                    form.Top = value;
                                }
                            }

                            // Если элемент формы есть, но расположение не сохранено, то показываем форму в центре.
                            if (locationIsNull)
                            {
                                form.StartPosition = FormStartPosition.CenterScreen;
                            }
                            else
                            {
                                form.StartPosition = FormStartPosition.Manual;
                            }
                        }

                        // Если считываем Состояним Максимизации.
                        if ((options & ConfigFormOption.Maximized) == ConfigFormOption.Maximized)
                        {
                            XmlNode formMaximized = formNode[ConfigFormOption.Maximized.ToString()];
                            // Если элемент есть и его значение "1".
                            if (formMaximized != null && formMaximized.InnerText == "1")
                            {
                                form.WindowState = FormWindowState.Maximized;
                            }
                            else
                            {
                                form.WindowState = FormWindowState.Normal;
                            }
                        }
                    }
                    // Если нет элемента формы и есть считывание расположения, то задаем стандартное положение - в центре.
                    else if ((options & ConfigFormOption.Location) == ConfigFormOption.Location)
                    {
                        form.StartPosition = FormStartPosition.CenterScreen;
                    }
                }
            }
        }

        /// <summary>
        /// Сохранение в конфиг-файле настроек формы по указанному списку свойств.
        /// </summary>
        /// <param name="form">Форма для сохранения ее параметров.</param>
        /// <param name="options">Набор флагов для сохранения параметров.</param>
        internal static void SaveFormParams(Form form, ConfigFormOption options)
        {
            // Форма не нуль и имя формы непустое.
            if (form != null && !string.IsNullOrEmpty(form.Name))
            {
                // Ищем корень конфигурации программы.
                XmlNode root = programParams[defaultRootNodeName];
                if (root == null)
                {
                    // Создаем корень, если его нет.
                    root = programParams.CreateElement(defaultRootNodeName);
                    programParams.AppendChild(root);
                }

                // Ищем элемент формы.
                XmlNode formNode = root[form.Name];
                if (formNode == null)
                {
                    // Создаем его, если его нет.
                    formNode = programParams.CreateElement(form.Name);
                    root.AppendChild(formNode);
                }

                // Сохраняем расположение и размер, только если форма не в Максимизированном состоянии.
                if (form.WindowState == FormWindowState.Normal)
                {
                    // Если сохраняем размер.
                    if ((options & ConfigFormOption.Size) == ConfigFormOption.Size)
                    {
                        // Элемент ширины.
                        XmlNode formWidth = formNode["Width"];
                        if (formWidth == null)
                        {
                            // Создаем его, если нет.
                            formWidth = programParams.CreateElement("Width");
                            formNode.AppendChild(formWidth);
                        }
                        // Сохраняем ширину.
                        formWidth.InnerText = form.Width.ToString();

                        // Элемент высоты.
                        XmlNode formHeight = formNode["Height"];
                        if (formHeight == null)
                        {
                            // Создаем его, если нет.
                            formHeight = programParams.CreateElement("Height");
                            formNode.AppendChild(formHeight);
                        }
                        // Сохраняем высоту.
                        formHeight.InnerText = form.Height.ToString();
                    }

                    // Если сохраняем расположение.
                    if ((options & ConfigFormOption.Location) == ConfigFormOption.Location)
                    {
                        // Элемент горизонтали.
                        XmlNode formLeft = formNode["Left"];
                        if (formLeft == null)
                        {
                            // Создаем его, если нет.
                            formLeft = programParams.CreateElement("Left");
                            formNode.AppendChild(formLeft);
                        }
                        // Сохраняем горизонталь.
                        formLeft.InnerText = form.Left.ToString();

                        // Элемент вертикали.
                        XmlNode formTop = formNode["Top"];
                        if (formTop == null)
                        {
                            // Создаем его, если нет.
                            formTop = programParams.CreateElement("Top");
                            formNode.AppendChild(formTop);
                        }
                        // Сохраняем вертикаль.
                        formTop.InnerText = form.Top.ToString();
                    }
                }

                // Если сохраняем состояние окна.
                if ((options & ConfigFormOption.Maximized) == ConfigFormOption.Maximized)
                {
                    // Элемент максимизации.
                    XmlNode formMaximized = formNode[ConfigFormOption.Maximized.ToString()];
                    if (formMaximized == null)
                    {
                        // Создаем его, если нет.
                        formMaximized = programParams.CreateElement(ConfigFormOption.Maximized.ToString());
                        formNode.AppendChild(formMaximized);
                    }
                    // Сохраняем максимизацию.
                    formMaximized.InnerText = form.WindowState == FormWindowState.Maximized ? "1" : "0";
                }
            }
        }

        #endregion Стандартные настройки форм.

        #endregion Формы.

        #region Столбцы таблиц.

        /// <summary>
        /// Загрузка из конфиг-файла параметров таблицы по списку свойств.
        /// </summary>
        /// <param name="dataGridView">DataGridView, для которой считывать настройки.</param>
        /// <param name="options">Флаги параметров для загрузки.</param>
        internal static void LoadDataGridViewParams(DataGridView dataGridView, ConfigDataGridViewOption options)
        {
            // DataGridView не пустой, его имя не пустое, и у него есть столбцы.
            if (dataGridView != null && !string.IsNullOrEmpty(dataGridView.Name) && dataGridView.Columns.Count > 0)
            {
                // Ищем форму, содержкащую таблицу.
                Form form = dataGridView.FindForm();
                // Форма не нулевая, имя ее не пустое.
                if (form != null && !string.IsNullOrEmpty(form.Name))
                {
                    // Ищем корневой элемент настроек программы.
                    XmlNode root = programParams[defaultRootNodeName];
                    if (root != null)
                    {
                        // Ищем элемент формы.
                        XmlNode formNode = root[form.Name];
                        if (formNode != null)
                        {
                            // Ищем элемент таблицы.
                            XmlNode dataGridViewNode = formNode[dataGridView.Name];
                            if (dataGridViewNode != null)
                            {
                                // Список для сортировки, если загружаются индексы отображения.
                                SortedList list = new SortedList();
                                // Параметр для конверта строк в числа.
                                int value = 0;

                                // Цикл по всем столбцам таблицы.
                                for (int i = 0; i < dataGridView.ColumnCount; i++)
                                {
                                    // Столбец.
                                    DataGridViewColumn item = dataGridView.Columns[i];
                                    // Имя столбца не пустое. Обрабатываются все, кроме системных столбцов.
                                    if (!string.IsNullOrEmpty(item.Name) && !item.Name.ToLower().Contains("syscol"))
                                    {
                                        // Элмент столбца.
                                        XmlNode columnNode = dataGridViewNode[item.Name];
                                        if (columnNode != null)
                                        {
                                            // Если загружаем ширину.
                                            if ((options & ConfigDataGridViewOption.Width) == ConfigDataGridViewOption.Width)
                                            {
                                                // Элемент ширины.
                                                XmlNode columnWidth = columnNode[ConfigDataGridViewOption.Width.ToString()];
                                                if (columnWidth != null)
                                                {
                                                    if (int.TryParse(columnWidth.InnerText, out value))
                                                    {
                                                        item.Width = value;
                                                    }
                                                }
                                            }

                                            // Если загружаем свойство отображения.
                                            if ((options & ConfigDataGridViewOption.Visible) == ConfigDataGridViewOption.Visible)
                                            {
                                                // Элмент отображения.
                                                XmlNode columnVisible = columnNode[ConfigDataGridViewOption.Visible.ToString()];
                                                if (columnVisible != null)
                                                {
                                                    item.Visible = columnVisible.InnerText == "1";
                                                }
                                            }

                                            // Если загружаем порядковый номер отображения.
                                            if ((options & ConfigDataGridViewOption.DisplayIndex) == ConfigDataGridViewOption.DisplayIndex)
                                            {
                                                // Элемент порядкового номера.
                                                XmlNode columnDisplayIndex = columnNode[ConfigDataGridViewOption.DisplayIndex.ToString()];
                                                if (columnDisplayIndex != null)
                                                {
                                                    if (int.TryParse(columnDisplayIndex.InnerText, out value))
                                                    {
                                                        // Добавляем в список сортировки.
                                                        list.Add(value, item);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                // Если загружаем порядковый номер отображения.
                                if ((options & ConfigDataGridViewOption.DisplayIndex) == ConfigDataGridViewOption.DisplayIndex)
                                {
                                    foreach (int key in list.Keys)
                                    {
                                        // Устанавливаем столбцам порядковые номера.
                                        ((DataGridViewColumn)list[key]).DisplayIndex = key;
                                    }
                                }

                                // Была попытка сохранять сортировку списка, но пока это вызывает ошибки.
                                // Признано ненужным.

                                //if ((options & ConfigDataGridViewOption.SortText) == ConfigDataGridViewOption.SortText)
                                //{
                                //    XmlAttribute sortTexNode = dataGridViewNode.Attributes[ConfigDataGridViewOption.SortText.ToString()];

                                //    if (sortTexNode != null && dataGridView.DataSource is BindingSource)
                                //    {
                                //        ((BindingSource)dataGridView.DataSource).Sort = sortTexNode.Value;
                                //    }
                                //}
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Сохранение в конфиг-файле параметров таблицы по списку свойств.
        /// </summary>
        /// <param name="dataGridView">DataGridView, настройки которой нужно сохранять.</param>
        /// <param name="options">Флаги настроек на сохранение.</param>
        internal static void SaveDataGridViewParams(DataGridView dataGridView, ConfigDataGridViewOption options)
        {
            // Таблица не нуль, ее имя не нуль, и количество столбцов больше нуля.
            if (dataGridView != null && !string.IsNullOrEmpty(dataGridView.Name) && dataGridView.ColumnCount > 0)
            {
                // Форма, содержащая таблицу.
                Form form = dataGridView.FindForm();
                // Форма не нуль, ее имя не нуль.
                if (form != null && !string.IsNullOrEmpty(form.Name))
                {
                    // Корневой элемент настроек программы.
                    XmlNode root = programParams[defaultRootNodeName];
                    if (root == null)
                    {
                        // Создаем корень, если его нет.
                        root = programParams.CreateElement(defaultRootNodeName);
                        programParams.AppendChild(root);
                    }

                    // Элемент формы.
                    XmlNode formNode = root[form.Name];
                    if (formNode == null)
                    {
                        // Создаем его, если его нет.
                        formNode = programParams.CreateElement(form.Name);
                        root.AppendChild(formNode);
                    }

                    // Элемент таблицы.
                    XmlNode dataGridViewNode = formNode[dataGridView.Name];
                    if (dataGridViewNode == null)
                    {
                        // Создаем его, если его нет.
                        dataGridViewNode = programParams.CreateElement(dataGridView.Name);
                        formNode.AppendChild(dataGridViewNode);
                    }

                    // Сохранение сортировки списка.
                    // Признано ненужным.

                    //if ((options & ConfigDataGridViewOption.SortText) == ConfigDataGridViewOption.SortText && dataGridView.DataSource is BindingSource)
                    //{
                    //    XmlAttribute sortTexNode = dataGridViewNode.Attributes[ConfigDataGridViewOption.SortText.ToString()];

                    //    if (sortTexNode == null)
                    //    {
                    //        sortTexNode = programParams.CreateAttribute(ConfigDataGridViewOption.SortText.ToString());
                    //        dataGridViewNode.Attributes.Append(sortTexNode);
                    //    }

                    //    sortTexNode.Value = ((BindingSource)dataGridView.DataSource).Sort;
                    //}

                    // По всем столбцам.
                    for (int i = 0; i < dataGridView.ColumnCount; i++)
                    {
                        DataGridViewColumn item = dataGridView.Columns[i];
                        // Имя столбца не нуль, и не системное.
                        if (!string.IsNullOrEmpty(item.Name) && !item.Name.ToLower().Contains("syscol"))
                        {
                            // Элемент столбца.
                            XmlNode columnNode = dataGridViewNode[item.Name];
                            if (columnNode == null)
                            {
                                // Создаем его, если его нет.
                                columnNode = programParams.CreateElement(item.Name);
                                dataGridViewNode.AppendChild(columnNode);
                            }

                            // Если сохраняем ширину.
                            if ((options & ConfigDataGridViewOption.Width) == ConfigDataGridViewOption.Width)
                            {
                                // Элемент ширины.
                                XmlNode columnWidth = columnNode[ConfigDataGridViewOption.Width.ToString()];
                                if (columnWidth == null)
                                {
                                    // Создаем его, если его нет.
                                    columnWidth = programParams.CreateElement(ConfigDataGridViewOption.Width.ToString());
                                    columnNode.AppendChild(columnWidth);
                                }
                                // Сохраняем ширину.
                                columnWidth.InnerText = item.Width.ToString();
                            }

                            // Если сохраняем свойство отображения.
                            if ((options & ConfigDataGridViewOption.Visible) == ConfigDataGridViewOption.Visible)
                            {
                                // Элемент отображения.
                                XmlNode columnVisible = columnNode[ConfigDataGridViewOption.Visible.ToString()];
                                if (columnVisible == null)
                                {
                                    // Создаем его, если его нет.
                                    columnVisible = programParams.CreateElement(ConfigDataGridViewOption.Visible.ToString());
                                    columnNode.AppendChild(columnVisible);
                                }
                                // Сохраняем отображение.
                                columnVisible.InnerText = item.Visible ? "1" : "0";
                            }

                            // Если сохраняем порядковый номер.
                            if ((options & ConfigDataGridViewOption.DisplayIndex) == ConfigDataGridViewOption.DisplayIndex)
                            {
                                // Элемент порядкового номера.
                                XmlNode columnDisplayIndex = columnNode[ConfigDataGridViewOption.DisplayIndex.ToString()];
                                if (columnDisplayIndex == null)
                                {
                                    // Создаем его, если его нет.
                                    columnDisplayIndex = programParams.CreateElement(ConfigDataGridViewOption.DisplayIndex.ToString());
                                    columnNode.AppendChild(columnDisplayIndex);
                                }
                                // Сохраняем порядковый номер.
                                columnDisplayIndex.InnerText = item.DisplayIndex.ToString();
                            }
                        }
                    }
                }
            }
        }

        #endregion Столбцы таблиц.

        #region Контролы.

        #region Свободного параметр контрола.

        /// <summary>
        /// Загрузка кастомного параметра контрола из конфиг-файла.
        /// </summary>
        /// <param name="control">Контрол, которому принадлежит параметр.</param>
        /// <param name="paramName">Имя параметра.</param>
        /// <returns>Сохраненное значение параметра.</returns>
        internal static string LoadControlCustomParameter(Control control, string paramName)
        {
            string result = string.Empty;
            // Если имя параметра не нулевое, контрое и его имя не нуль.
            if (!string.IsNullOrEmpty(paramName) && control != null && !string.IsNullOrEmpty(control.Name))
            {
                // Форма, содержащая данный контрол.
                Form form = control.FindForm();
                // Форма и его имя не нуль.
                if (form != null && !string.IsNullOrEmpty(form.Name))
                {
                    // Корневой элемент конфигурации программы.
                    XmlNode root = programParams[defaultRootNodeName];
                    if (root != null)
                    {
                        // Элемент формы.
                        XmlNode formNode = root[form.Name];
                        if (formNode != null)
                        {
                            // Элемент контрола.
                            XmlNode controlNode = formNode[control.Name];
                            if (controlNode != null)
                            {
                                // Элемент параметра.
                                XmlNode controlParamValue = controlNode[paramName];
                                if (controlParamValue != null)
                                {
                                    // Считываем параметр.
                                    result = controlParamValue.InnerText;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Сохранение в конфиг-файл кастомного параметра контрола.
        /// </summary>
        /// <param name="control">Контрол, которому принадлежит параметр.</param>
        /// <param name="paramName">Имя параметра.</param>
        /// <param name="paramValue">Значение параметра.</param>
        internal static void SaveControlCustomParameter(Control control, string paramName, string paramValue)
        {
            // Если имя параметра, контрол и его имя не нулевые.
            if (!string.IsNullOrEmpty(paramName) && control != null && !string.IsNullOrEmpty(control.Name))
            {
                // Форма, содержащая данную форму.
                Form form = control.FindForm();
                // Форма и его имя не нуль.
                if (form != null && !string.IsNullOrEmpty(form.Name))
                {
                    // Корневой элемент конфиг-файла.
                    XmlNode root = programParams[defaultRootNodeName];
                    if (root == null)
                    {
                        // Создаем корневой элемент, если его нет.
                        root = programParams.CreateElement(defaultRootNodeName);
                        programParams.AppendChild(root);
                    }

                    // Элемент формы.
                    XmlNode formNode = root[form.Name];
                    if (formNode == null)
                    {
                        // Создаем его, если его нет.
                        formNode = programParams.CreateElement(form.Name);
                        root.AppendChild(formNode);
                    }

                    // Элемент контрола.
                    XmlNode controlNode = formNode[control.Name];
                    if (controlNode == null)
                    {
                        // Создаем его, если его нет.
                        controlNode = programParams.CreateElement(control.Name);
                        formNode.AppendChild(controlNode);
                    }

                    // Элемент параметра.
                    XmlNode controlParamValue = controlNode[paramName];
                    if (controlParamValue == null)
                    {
                        // Создаем его, если его нет.
                        controlParamValue = programParams.CreateElement(paramName);
                        controlNode.AppendChild(controlParamValue);
                    }
                    // Сохраняем параметр.
                    controlParamValue.InnerText = paramValue;
                }
            }
        }

        #endregion Свободного параметр контрола.

        #region Размеры контролов.

        /// <summary>
        /// Загрузка размеров списка контролов из конфиг-файла.
        /// </summary>
        /// <param name="controls">Массив контролов, для считывания размеров.</param>
        internal static void LoadControlsSize(params Control[] controls)
        {
            // Массив не нулевая и в нем более одного элемента.
            if (controls != null && controls.Length > 0)
            {
                // Форма, содержащая данные контролы (считывается с первого контрола).
                Form form = controls[0].FindForm();
                // Форма и ее имя не нуль.
                if (form != null && !string.IsNullOrEmpty(form.Name))
                {
                    // Корневой элемент конфигурации программы.
                    XmlNode root = programParams[defaultRootNodeName];
                    if (root != null)
                    {
                        // Элемент формы.
                        XmlNode formNode = root[form.Name];
                        if (formNode != null)
                        {
                            // Переменная для конверта текста в число.
                            int value = 0;
                            // По всем контролам.
                            for (int i = 0; i < controls.Length; i++)
                            {
                                Control control = controls[i];
                                // Имя контрола не нуль.
                                if (!string.IsNullOrEmpty(control.Name))
                                {
                                    // Элемент контрола.
                                    XmlNode controlNode = formNode[control.Name];
                                    if (controlNode != null)
                                    {
                                        // Элемент ширины.
                                        XmlNode controlWidth = controlNode["Width"];
                                        if (controlWidth != null)
                                        {
                                            if (int.TryParse(controlWidth.InnerText, out value))
                                            {
                                                // Загружаем ширину.
                                                control.Width = value;
                                            }
                                        }
                                        // Элемент высоты.
                                        XmlNode controlHeight = controlNode["Height"];
                                        if (controlHeight != null)
                                        {
                                            if (int.TryParse(controlHeight.InnerText, out value))
                                            {
                                                // Загружаем высоту.
                                                control.Height = value;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Сохранение конфиг-файл размеров контролов.
        /// </summary>
        /// <param name="controls">Массив контролов для сохранения размеров.</param>
        internal static void SaveControlsSize(params Control[] controls)
        {
            // Массив контролов не нулевой и в нем есть хоть один элемент.
            if (controls != null && controls.Length > 0)
            {
                // Форма, содержащая эти контролы.
                Form form = controls[0].FindForm();
                // Форма и ее имя не нулевые.
                if (form != null && !string.IsNullOrEmpty(form.Name))
                {
                    // Корневой элемент конфигурации программы.
                    XmlNode root = programParams[defaultRootNodeName];
                    if (root == null)
                    {
                        // Создать корневой элемент.
                        root = programParams.CreateElement(defaultRootNodeName);
                        programParams.AppendChild(root);
                    }
                    // Элемент формы.
                    XmlNode formNode = root[form.Name];
                    if (formNode == null)
                    {
                        // Создать его, если его нет.
                        formNode = programParams.CreateElement(form.Name);
                        root.AppendChild(formNode);
                    }
                    // По всему массиву.
                    for (int i = 0; i < controls.Length; i++)
                    {
                        Control control = controls[i];
                        // Имя контрола не нулевое.
                        if (!string.IsNullOrEmpty(control.Name))
                        {
                            // Элемент контрола.
                            XmlNode controlNode = formNode[control.Name];
                            if (controlNode == null)
                            {
                                // Создать его, если его нет.
                                controlNode = programParams.CreateElement(control.Name);
                                formNode.AppendChild(controlNode);
                            }
                            // Элемент ширины.
                            XmlNode controlWidth = controlNode["Width"];
                            if (controlWidth == null)
                            {
                                // Создать его, если его нет.
                                controlWidth = programParams.CreateElement("Width");
                                controlNode.AppendChild(controlWidth);
                            }
                            // Сохраняем ширину.
                            controlWidth.InnerText = control.Width.ToString();
                            // Элемент высоты.
                            XmlNode controlHeight = controlNode["Height"];
                            if (controlHeight == null)
                            {
                                // Создать его, если его нет.
                                controlHeight = programParams.CreateElement("Height");
                                controlNode.AppendChild(controlHeight);
                            }
                            // Сохраняем высоту.
                            controlHeight.InnerText = control.Height.ToString();
                        }
                    }
                }
            }
        }

        #endregion Размеры контролов.

        #region Отображаемость контролов.

        /// <summary>
        /// Загрузка свойства отображения набора контролов из конфиг-файла.
        /// </summary>
        /// <param name="controls">Массив контролов, для которых считывается отображаемость.</param>
        internal static void LoadControlsVisible(params Control[] controls)
        {
            // Массив не нуль, и в нем хоть один элемент.
            if (controls != null && controls.Length > 0)
            {
                // Форма, содержащая эти контролы.
                Form form = controls[0].FindForm();
                // Форма и ее имя не нуль.
                if (form != null && !string.IsNullOrEmpty(form.Name))
                {
                    // Корневой элемент конфигурации программы.
                    XmlNode root = programParams[defaultRootNodeName];
                    if (root != null)
                    {
                        // Элемент формы.
                        XmlNode formNode = root[form.Name];
                        if (formNode != null)
                        {
                            // По всему массиву.
                            for (int i = 0; i < controls.Length; i++)
                            {
                                Control control = controls[i];
                                // Имя контрола не нуль.
                                if (!string.IsNullOrEmpty(control.Name))
                                {
                                    // Элемент контрола.
                                    XmlNode controlNode = formNode[control.Name];
                                    if (controlNode != null)
                                    {
                                        // Элемент отображаемости.
                                        XmlNode controlVisible = controlNode["Visible"];
                                        if (controlVisible != null)
                                        {
                                            // Устанавливаем отображаемость.
                                            control.Visible = controlVisible.InnerText == "1";
                                            if (control.Parent != null)
                                            {
                                                // Заставляем родителя переформировать отступы.
                                                control.Parent.PerformLayout();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Сохранение в конфиг-файл свойства отображения набора контролов.
        /// </summary>
        /// <param name="controls">Контролы, отображаемость которых надо сохранять.</param>
        internal static void SaveControlsVisible(params Control[] controls)
        {
            // Массив не нуль, и в нем хоть один элемент.
            if (controls != null && controls.Length > 0)
            {
                // Форма, содержащая данный контрол.
                Form form = controls[0].FindForm();
                // Форма и ее имя не нуль.
                if (form != null && !string.IsNullOrEmpty(form.Name))
                {
                    // Корневой элемент конфигурации программы.
                    XmlNode root = programParams[defaultRootNodeName];
                    if (root == null)
                    {
                        // Создаем корневой элемент.
                        root = programParams.CreateElement(defaultRootNodeName);
                        programParams.AppendChild(root);
                    }
                    // Элемент формы.
                    XmlNode formNode = root[form.Name];
                    if (formNode == null)
                    {
                        // Создаем его, если его нет.
                        formNode = programParams.CreateElement(form.Name);
                        root.AppendChild(formNode);
                    }
                    // По всему массиву.
                    for (int i = 0; i < controls.Length; i++)
                    {
                        Control control = controls[i];
                        // Имя контрола не нуль.
                        if (!string.IsNullOrEmpty(control.Name))
                        {
                            // Элемент контрола.
                            XmlNode controlNode = formNode[control.Name];
                            if (controlNode == null)
                            {
                                // Создаем его, если его нет.
                                controlNode = programParams.CreateElement(control.Name);
                                formNode.AppendChild(controlNode);
                            }
                            // Элемент отображаемости.
                            XmlNode controlVisible = controlNode["Visible"];
                            if (controlVisible == null)
                            {
                                // Создаем его, если его нет.
                                controlVisible = programParams.CreateElement("Visible");
                                controlNode.AppendChild(controlVisible);
                            }
                            // Сохраняем отображаемость.
                            controlVisible.InnerText = control.Visible ? "1" : "0";
                        }
                    }
                }
            }
        }

        #endregion Отображаемость контролов.

        #endregion Контролы.

        #region Список выбранных элементов в списке.

        internal static void LoadExpandedTreeNode(TreeView treeView)
        {
            treeView.SuspendLayout();
            treeView.BeginUpdate();

            if (!string.IsNullOrEmpty(treeView.Name))
            {
                // Форма, содержащая данные контролы (считывается с первого контрола).
                Form form = treeView.FindForm();
                // Форма и ее имя не нуль.
                if (form != null && !string.IsNullOrEmpty(form.Name))
                {
                    // Корневой элемент конфигурации программы.
                    XmlNode root = programParams[defaultRootNodeName];
                    if (root != null)
                    {
                        // Элемент формы.
                        XmlNode formNode = root[form.Name];
                        if (formNode != null)
                        {
                            // Элемент контрола.
                            XmlNode controlNode = formNode[treeView.Name];
                            if (controlNode != null)
                            {
                                XmlNode expandedItemsXmlNode = controlNode["ExpandedNodes"];
                                if (expandedItemsXmlNode != null)
                                {
                                    foreach (XmlNode item in expandedItemsXmlNode.ChildNodes)
                                    {
                                        string nodeName = item.InnerText;

                                        if (!string.IsNullOrEmpty(nodeName))
                                        {
                                            TreeNode[] searchResult = treeView.Nodes.Find(nodeName, true);
                                            if (searchResult.Length == 1)
                                            {
                                                searchResult[0].Expand();
                                            }
                                        }
                                    }
                                }

                                XmlNode selectedNodeXmlNode = controlNode["SelectedNode"];
                                if (selectedNodeXmlNode != null)
                                {
                                    string nodeName = selectedNodeXmlNode.InnerText;

                                    if (!string.IsNullOrEmpty(nodeName))
                                    {
                                        TreeNode[] searchResult = treeView.Nodes.Find(nodeName, true);
                                        if (searchResult.Length == 1)
                                        {
                                            treeView.SelectedNode = searchResult[0];
                                            treeView.SelectedNode.EnsureVisible();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            treeView.EndUpdate();
            treeView.ResumeLayout(true);
        }

        internal static void SaveExpandedTreeNode(TreeView treeView)
        {
            // Если имя параметра, контрол и его имя не нулевые.
            if (treeView != null && !string.IsNullOrEmpty(treeView.Name))
            {
                // Форма, содержащая данную форму.
                Form form = treeView.FindForm();
                // Форма и его имя не нуль.
                if (form != null && !string.IsNullOrEmpty(form.Name))
                {
                    // Корневой элемент конфиг-файла.
                    XmlNode root = programParams[defaultRootNodeName];
                    if (root == null)
                    {
                        // Создаем корневой элемент, если его нет.
                        root = programParams.CreateElement(defaultRootNodeName);
                        programParams.AppendChild(root);
                    }

                    // Элемент формы.
                    XmlNode formNode = root[form.Name];
                    if (formNode == null)
                    {
                        // Создаем его, если его нет.
                        formNode = programParams.CreateElement(form.Name);
                        root.AppendChild(formNode);
                    }

                    // Элемент контрола.
                    XmlNode treeViewXmlNode = formNode[treeView.Name];
                    if (treeViewXmlNode == null)
                    {
                        // Создаем его, если его нет.
                        treeViewXmlNode = programParams.CreateElement(treeView.Name);
                        formNode.AppendChild(treeViewXmlNode);
                    }

                    XmlNode selectedXmlNode = treeViewXmlNode["SelectedNode"];
                    if (selectedXmlNode == null)
                    {
                        // Создаем его, если его нет.
                        selectedXmlNode = programParams.CreateElement("SelectedNode");
                        treeViewXmlNode.AppendChild(selectedXmlNode);
                    }

                    if (treeView.SelectedNode != null)
                    {
                        selectedXmlNode.InnerText = treeView.SelectedNode.Name;
                    }
                    else
                    {
                        selectedXmlNode.InnerText = string.Empty;
                    }

                    // Элемент Списка выбранных элементов.
                    XmlNode expandedNodesXmlList = treeViewXmlNode["ExpandedNodes"];
                    if (expandedNodesXmlList == null)
                    {
                        // Создаем его, если его нет.
                        expandedNodesXmlList = programParams.CreateElement("ExpandedNodes");
                        treeViewXmlNode.AppendChild(expandedNodesXmlList);
                    }

                    expandedNodesXmlList.InnerText = string.Empty;

                    foreach (TreeNode node in treeView.Nodes)
                    {
                        if (node.IsExpanded && !string.IsNullOrEmpty(node.Name))
                        {
                            XmlNode xmlNode = programParams.CreateElement("Node");
                            expandedNodesXmlList.AppendChild(xmlNode);
                            xmlNode.InnerText = node.Name;
                        }

                        SaveChildNodes(node, expandedNodesXmlList);
                    }
                }
            }
        }

        private static void SaveChildNodes(TreeNode parentNode, XmlNode itemsNode)
        {
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.IsExpanded && !string.IsNullOrEmpty(node.Name))
                {
                    XmlNode xmlNode = programParams.CreateElement("Node");
                    itemsNode.AppendChild(xmlNode);
                    xmlNode.InnerText = node.Name;
                }

                SaveChildNodes(node, itemsNode);
            }
        }

        #endregion Список выбранных элементов в списке.

        #endregion Методы для работы интерфейсом форм.
    }
}

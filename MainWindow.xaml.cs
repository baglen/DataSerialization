using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace DataSerialization
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XmlDocument selectXmlFile;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialogXml = new OpenFileDialog();
            openFileDialogXml.Multiselect = false;
            openFileDialogXml.Filter = "XML|*.xml;";
            openFileDialogXml.Title = "Select XML File";
            if (openFileDialogXml.ShowDialog() == true)
            {
                txtBlockSelected.Text = openFileDialogXml.FileName;
            }
            using (var myStream = openFileDialogXml.OpenFile())
            {
                XmlDocument parsedMyStream = new XmlDocument();
                try
                {

                    parsedMyStream.Load(myStream);

                    selectXmlFile = parsedMyStream;
                }

                catch (XmlException ex)
                {
                    MessageBox.Show("The XML could not be read. " + ex);
                }
            }
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Root));
            Root productOccurence;
            using (XmlReader reader = XmlReader.Create("test.xml"))
            {
                productOccurence = (Root)ser.Deserialize(reader);
            }
            string json = JsonConvert.SerializeObject(productOccurence, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { });
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON|*.json";
            saveFileDialog.ShowDialog();
            File.WriteAllText(saveFileDialog.FileName, json);
        }


        // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class Root
        {

            private RootProductOccurence[] modelFileField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("ProductOccurence", IsNullable = false)]
            public RootProductOccurence[] ModelFile
            {
                get
                {
                    return this.modelFileField;
                }
                set
                {
                    this.modelFileField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class RootProductOccurence
        {

            private RootProductOccurenceAttr[] attributesField;

            private string idField;

            private string nameField;

            

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Attr", IsNullable = false)]
            public RootProductOccurenceAttr[] Attributes
            {
                get
                {
                    return this.attributesField;
                }
                set
                {
                    this.attributesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class RootProductOccurenceAttr
        {

            private string nameField;

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }



    }
}


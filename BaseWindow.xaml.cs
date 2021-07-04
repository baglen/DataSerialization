using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace DataSerialization
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        private string filePath;
        public BaseWindow()
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
                filePath = openFileDialogXml.FileName;
            }
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Root));

            Root productOccurence;
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                productOccurence = (Root)ser.Deserialize(reader);
            }
            
            var json = JsonConvert.SerializeObject(productOccurence, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { });
            json = json.Replace("null", "[]");
            json = json.Replace("{\r\n" + "  \"ModelFile\": ", "");
            json = json.Replace("\r\n" + "}", "");
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON|*.json";
            if(saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, json);
            }           
        }

        /// <remarks/>
        [System.Serializable()]
        [System.ComponentModel.DesignerCategory("code")]
        [XmlType(AnonymousType = true)]
        [XmlRoot(Namespace = "", IsNullable = false)]
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
            [JsonProperty(PropertyName = "Props")]
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


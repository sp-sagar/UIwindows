using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{ 
    class ViewModel : INotifyPropertyChanged
    {
       public const string fileloc = @"C:\Users\sagar.prakash\source\repos\XmlFile";
        public ViewModel()
        {
            if (listOfFiles == null)
                listOfFiles = new ObservableCollection<string>();
            if (listOfForms == null)
                listOfForms = new ObservableCollection<string>();
            if (listOfTags == null)
                listOfTags = new ObservableCollection<string>();
            if (listOfFields == null)
                listOfFields = new ObservableCollection<string>();

                IsFileComboVisible = Visibility.Hidden;
                IsFormComboVisible = Visibility.Hidden;
                IsTabComboVisible = Visibility.Hidden;
                IsFieldComboVisible = Visibility.Hidden;
        }

        private ObservableCollection<string> listOfFiles;
        public ObservableCollection<string> ListOfFiles
        {
            get
            {
                return listOfFiles;
            }
            set
            {
                listOfFiles = value;
                RaisePropertyChanged("ListOfFiles");
            }
        }

     public  static ComboBox FormBox= new ComboBox();

        public ICommand AddForm
        {
            get
            {
                return new RelayCommand(this.OnClickAddForm);
            }
        }

        public Visibility IsFileComboVisible { get; set; }

        public void OnClickAddForm()
        {
            GetFileNames();
            
            IsFileComboVisible = Visibility.Visible;
            RaisePropertyChanged("IsFileComboVisible");
            
        }
        public void GetFileNames()
        {
            listOfFiles.Clear();
           
            string[] xmlfiles = Directory.GetFiles(fileloc, "*.xml");
            foreach (string file in xmlfiles)
                ListOfFiles.Add(System.IO.Path.GetFileNameWithoutExtension(file));

        }

        private string selectedfile;
        public string SelectedFile
        {
            get { return selectedfile; }
            set { selectedfile = value; }
        }


        private ObservableCollection<string> listOfForms;
        public ObservableCollection<string> ListOfForms
        {
            get
            { 
                return listOfForms;
            }
            set
            {
                listOfForms = value;
                RaisePropertyChanged("ListOfForms");
            }
        }
        public ICommand AddTag
        {
            get
            {
                return new RelayCommand(this.OnClickAddTag);
            }
        }

        public Visibility IsFormComboVisible { get; set; }

        public void OnClickAddTag()
        {  
            GetFormNames();
            
            
           
            IsFormComboVisible = Visibility.Visible;
            RaisePropertyChanged("IsFormComboVisible");

        }

        public void GetFormNames()
        {
            listOfForms.Clear();
            string xfile = fileloc + "\\" + selectedfile + ".xml" ;
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xfile);
            XmlNodeList nodes = xdoc.GetElementsByTagName("FormName");
           
            foreach(XmlNode node in nodes)
            {
                ListOfForms.Add(node.Attributes["FormName"].Value.Trim());
            }
           
        }

        private string selectedform;
        public string SelectedForm
        {
            get { return selectedform; }
            set { selectedform = value; }
        }

        private ObservableCollection<string> listOfTags;
        public ObservableCollection<string> ListOfTags
        {
            get
            {
                return listOfTags;
            }
            set
            {
                listOfTags = value;
                RaisePropertyChanged("ListOfTags");
            }
        }
        public ICommand AddTab
        {
            get
            {
                return new RelayCommand(this.OnClickAddTab);
            }
        }
        public Visibility IsTabComboVisible { get; set; }

        public void OnClickAddTab()
        {
            GetTabNames();
            IsTabComboVisible = Visibility.Visible;
            RaisePropertyChanged("IsTabComboVisible");

        }
        public void GetTabNames()
        {
            listOfTags.Clear();
            string xfile = fileloc + "\\" + selectedfile + ".xml";
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xfile);
            XmlNodeList nodes = xdoc.GetElementsByTagName("TabName");

            foreach (XmlNode node in nodes)
            {
                ListOfTags.Add(node.Attributes["Name"].Value);
            }

        }

        private string selectedtab;
        public string SelectedTab
        {
            get { return selectedtab; }
            set { selectedtab = value; }
        }


        private ObservableCollection<string> listOfFields;
        public ObservableCollection<string> ListOfFields
        {
            get
            {
                return listOfFields;
            }
            set
            {
                listOfFields = value;
                RaisePropertyChanged("ListOfFields");
            }
        }
        public ICommand AddField
        {
            get
            {
                return new RelayCommand(this.OnClickAddField);
            }
        }
        public Visibility IsFieldComboVisible { get; set; }

        public void OnClickAddField()
        {
            GetFieldNames();
            IsFieldComboVisible = Visibility.Visible;
            RaisePropertyChanged("IsFieldComboVisible");

        }
        

        public void GetFieldNames()
        {
            listOfFields.Clear();
            string xfile = fileloc + "\\" + selectedfile + ".xml";
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xfile);
            

            XmlNodeList nodes = xdoc.GetElementsByTagName("TabName");

            for (int i = 0; i < nodes.Count; i++)
            {

                if (SelectedTab == nodes[i].Attributes["Name"].Value)
                {

                    foreach (XmlNode ele in nodes[i].ChildNodes)
                        ListOfFields.Add(ele.InnerText);
                }

            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private class RelayCommand : ICommand
        {
            private Action onClick;

            public RelayCommand(Action onClick)
            {
                this.onClick = onClick;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                onClick();
            }
        }
       
        

    }
  }

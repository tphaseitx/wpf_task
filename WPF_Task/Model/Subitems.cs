using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPF_Task.Model
{
    public class Subitems : INotifyPropertyChanged
    {
        private int Index;
        public int _Index
        {
            get { return this.Index; }
            set
            {
                if (this.Index != value)
                {
                    this.Index = value;
                    this.NotifyPropertyChanged("_Index");
                }
            }
        }

        private int IdItem;
        public int _IdItem
        {
            get { return this.IdItem; }
            set
            {
                if (this.IdItem != value)
                {
                    this.IdItem = value;
                    this.NotifyPropertyChanged("_IdItem");
                }
            }
        }

        private string Name;
        public string _Name
        {
            get { return this.Name; }
            set
            {
                if (this.Name != value)
                {
                    this.Name = value;
                    this.NotifyPropertyChanged("_Name");
                }
            }
        }

        private string Description;
        public string _Description
        {
            get { return this.Description; }
            set
            {
                if (this.Description != value)
                {
                    this.Description = value;
                    this.NotifyPropertyChanged("_Description");
                }
            }
        }

        private bool IsMandatory;
        public bool _IsMandatory
        {
            get { return this.IsMandatory; }
            set
            {
                if (this.IsMandatory != value)
                {
                    this.IsMandatory = value;
                    this.NotifyPropertyChanged("_IsMandatory");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}

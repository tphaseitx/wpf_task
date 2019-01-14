using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPF_Task.Model
{
    public class Items : INotifyPropertyChanged
    {
        private int Id;
        public int _Id
        {
            get { return this.Id; }
            set
            {
                if (this.Id != value)
                {
                    this.Id = value;
                    this.NotifyPropertyChanged("_Id");
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

        private string Color;
        public string _Color
        {
            get { return this.Color; }
            set
            {
                if (this.Color != value)
                {
                    this.Color = value;
                    this.NotifyPropertyChanged("_Color");
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

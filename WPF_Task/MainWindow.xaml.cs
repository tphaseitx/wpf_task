using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Model.Items> mItems = new ObservableCollection<Model.Items>();
        public ObservableCollection<Model.Items> _mItems
        {
            get { return mItems; }
        }

        ObservableCollection<Model.Subitems> mSubitems = new ObservableCollection<Model.Subitems>();
        public ObservableCollection<Model.Subitems> _mSubitems
        {
            get { return mSubitems; }
        }

        private int IdItem = -1;

        public MainWindow()
        {
            DataContext = this;

            InitializeComponent();

            for (int i = 0; i < 2; i++)
            {
                mItems.Add(new Model.Items
                {
                    _Id = i,
                    _Name = "Item " + i.ToString(),
                    _Color = "#FFFFFF"
                });
            }

            for (int i = 0; i < 2; i++)
            {
                mSubitems.Add(new Model.Subitems
                {
                    _Index = i,
                    _IdItem = 0,
                    _Name = "Sub item " + i.ToString(),
                    _Description = "Descriere",
                    _IsMandatory = true
                });
            }
        }

        private void button_add_item_Click(object sender, RoutedEventArgs e)
        {
            if (!isTextInTextBox(text_name))
            {
                MessageBox.Show("Introduceti denumirea item-ului", "Adaugare item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!isTextInTextBox(text_color))
            {
                MessageBox.Show("Introduceti culoarea item-ului", "Adaugare item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            mItems.Add(new Model.Items
            {
                _Id = mItems.Count,
                _Name = text_name.Text,
                _Color = text_color.Text
            });

            text_name.Text = "";
            text_color.Text = "";
        }

        private void button_add_subitem_Click(object sender, RoutedEventArgs e)
        {
            if (IdItem == -1)
            {
                MessageBox.Show("Selectati un item", "Adaugare subitem", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }

            if (!isTextInTextBox(text_subitem_name))
            {
                MessageBox.Show("Introduceti denumirea subitem-ului", "Adaugare subitem", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!isTextInTextBox(text_description))
            {
                MessageBox.Show("Introduceti descrierea subitem-ului", "Adaugare subitem", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            IdItem = getItemId(list_items);

            mSubitems.Add(new Model.Subitems
            {
                _Index = mSubitems.Count,
                _IdItem = IdItem,
                _Name = text_subitem_name.Text,
                _Description = text_description.Text,
                _IsMandatory = (bool)check_is_mandatory.IsChecked
            });

            text_subitem_name.Text = "";
            text_description.Text = "";
        }

        private void image_delete_item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Image b = sender as Image;
                    Model.Items items = b.DataContext as Model.Items;

                    if (items != null)
                    {
                        mItems.RemoveAt(items._Id);

                        for (int i = 0; i < mItems.Count; i++)
                        {
                            mItems[i]._Id = i;
                        }
                    }
                }));
            }
            catch (Exception ex) { }
        }

        private void image_delete_subitem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Image b = sender as Image;
                    Model.Subitems subitems = b.DataContext as Model.Subitems;

                    if (subitems != null)
                    {
                        mSubitems.RemoveAt(subitems._Index);

                        for (int i = 0; i < mSubitems.Count; i++)
                        {
                            mSubitems[i]._Index = i;
                        }
                    }
                }));
            }
            catch (Exception ex) { }
        }

        private void move_subitem_up_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Image b = sender as Image;
                    Model.Subitems subitems = b.DataContext as Model.Subitems;

                    if (subitems != null)
                    {
                        list_subitems.SelectedItem = mSubitems;
                        var selectedIndex = list_subitems.SelectedIndex;

                        if (selectedIndex > 0)
                        {
                            var itemToMoveUp = mSubitems[selectedIndex];
                            mSubitems.RemoveAt(selectedIndex);
                            mSubitems.Insert(selectedIndex - 1, itemToMoveUp);
                            list_subitems.SelectedIndex = selectedIndex - 1;
                        }
                    }
                }));
            }
            catch (Exception ex) { }
        }

        private void move_subitem_down_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Image b = sender as Image;
                    Model.Subitems subitems = b.DataContext as Model.Subitems;

                    if (subitems != null)
                    {
                        list_subitems.SelectedItem = mSubitems;
                        var selectedIndex = list_subitems.SelectedIndex;

                        if (selectedIndex + 1 < mSubitems.Count)
                        {
                            var itemToMoveDown = this.mSubitems[selectedIndex];
                            this.mSubitems.RemoveAt(selectedIndex);
                            this.mSubitems.Insert(selectedIndex + 1, itemToMoveDown);
                            this.list_subitems.SelectedIndex = selectedIndex + 1;
                        }
                    }
                }));
            }
            catch (Exception ex) { }
        }

        private bool isTextInTextBox(TextBox text_box)
        {
            if (string.IsNullOrEmpty(text_box.Text) || string.IsNullOrWhiteSpace(text_box.Text))
            {
                return false;
            }

            return true;
        }

        private void list_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IdItem = getItemId(list_items);

            var source = CollectionViewSource.GetDefaultView(mSubitems);
            source.Filter = p => subItemsFilter((Model.Subitems)p);
            source.Refresh();

            list_subitems.ItemsSource = source;
        }

        private bool subItemsFilter(Model.Subitems model)
        {
            return (model._IdItem == IdItem);
        }

        private int getItemId(ListView list_view)
        {
            Type t = list_view.SelectedItem.GetType();
            System.Reflection.PropertyInfo[] props = t.GetProperties();
            return Convert.ToInt32(props[0].GetValue(list_items.SelectedItem, null).ToString());
        }
    }
}

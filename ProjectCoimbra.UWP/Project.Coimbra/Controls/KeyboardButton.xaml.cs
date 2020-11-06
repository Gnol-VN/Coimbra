using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Coimbra.Model;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Coimbra.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class KeyboardButton : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public KeyboardButton() 
        {
            this.InitializeComponent();
            UpdateSelectedKeysText();
            this.SelectedKeys.CollectionChanged += SelectedKeys_CollectionChanged;
        }

        private void SelectedKeys_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateSelectedKeysText();
        }

        /// <summary>
        /// Dependency Property for selected items.
        /// </summary>
        public static readonly DependencyProperty SelectedKeysProperty =
            DependencyProperty.Register("SelectedKeys", typeof(ObservableCollection<OrderedString>), typeof(KeyboardButton), new PropertyMetadata(new ObservableCollection<OrderedString>()));


        /// <summary>
        /// Gets or sets selected items.
        /// </summary>
#pragma warning disable CA2227 // Collection properties should be read only
        public ObservableCollection<OrderedString> SelectedKeys
#pragma warning restore CA2227 // Collection properties should be read only
        {
            get => (ObservableCollection<OrderedString>)this.GetValue(SelectedKeysProperty);
            set => this.SetValue(SelectedKeysProperty, value);
        }

        /// <summary>
        /// Dependency Property for selected items.
        /// </summary>
        public static readonly DependencyProperty AllKeysToDisplayProperty =
            DependencyProperty.Register("AllKeysToDisplay", typeof(ObservableCollection<OrderedString>), typeof(KeyboardButton), new PropertyMetadata(new ObservableCollection<OrderedString>()));


        /// <summary>
        /// Gets or sets selected items.
        /// </summary>
#pragma warning disable CA2227 // Collection properties should be read only
        public ObservableCollection<OrderedString> AllKeysToDisplay
#pragma warning restore CA2227 // Collection properties should be read only
        {
            get => (ObservableCollection<OrderedString>)this.GetValue(AllKeysToDisplayProperty);
            set => this.SetValue(AllKeysToDisplayProperty, value);
        }


        private async void ShowDialog_Click(object sender, RoutedEventArgs e)
        {
            KeyboardDialog keyboardDialog = new KeyboardDialog();
            await keyboardDialog.ShowAsync();
            Debug.WriteLine(keyboardDialog.Result);
            this.SelectedKeys.Add(this.AllKeysToDisplay.First(key => key.Value == keyboardDialog.Result));
            UpdateSelectedKeysText();
        }

        
        private string selectedKeysText;

        /// <summary>
        /// Gets or sets text listing selected keys.
        /// </summary>
        public string SelectedKeysText
        {
            get => this.selectedKeysText;
            set
            {
                this.selectedKeysText = value;
                NotifyPropertyChanged("SelectedKeysText");
            }
        }

        private void UpdateSelectedKeysText()
        {
            if (SelectedKeys.Count == 0)
            {
                this.SelectedKeysText = "No key specified";
            }
            else
            {
                this.SelectedKeysText = string.Join(",", this.SelectedKeys.Select(key => key.Value));
            }
        }

        private void NotifyPropertyChanged(string name) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

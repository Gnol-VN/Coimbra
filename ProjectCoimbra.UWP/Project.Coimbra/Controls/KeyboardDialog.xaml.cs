using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Coimbra.Controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class KeyboardDialog : ContentDialog
    {
        internal class KeyboardLayout
        {

            public string[] Default = new string[]
            {
                 "` 1 2 3 4 5 6 7 8 9 0 - = {backspace}",
                 "{tab} Q W E R T Y U I O P [ ] \\",
                 "CAPS A S D F G H J K L ; ' {enter}",
                 "{shift} Z X C V B N M , . / {shift}",
                 "Ctrl Win Atl {space} Atl Win Menu Ctrl"
            };

        }

        internal class KeyPressed : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public string Key { get; set; }

            public KeyboardDialog KeyboardDialog { get; set; }

            public KeyPressed(string key, KeyboardDialog keyboardDialog)
            {
                Key = key;
                KeyboardDialog = keyboardDialog;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                Debug.WriteLine(Key);
                KeyboardDialog.Result = Key;
                KeyboardDialog.Hide();
                CanExecuteChanged.GetHashCode();
                var element = FocusManager.GetFocusedElement();
                if (element != null)
                {
                    #region MyRegion

                    /*
                    if (element is TextBox)
                    {
                        var tbox = element as TextBox;
                        CanExecuteChanged.GetHashCode();
                        // Input handling
                        switch (Key)
                        {
                            case "{backspace}":
                                {
                                    var idx = tbox.SelectionStart;
                                    if (idx > 0)
                                    {
                                        Debug.WriteLine(idx.ToString());
                                        tbox.Text = tbox.Text.Remove(idx - 1);
                                        tbox.SelectionStart = tbox.Text.Length;
                                        tbox.SelectionLength = 0;
                                    }

                                    break;
                                }
                            case "{delete}":
                                {
                                    tbox.Text = "";
                                    tbox.SelectionStart = tbox.Text.Length;
                                    tbox.SelectionLength = 0;

                                    break;
                                }
                            case "{space}":
                                {
                                    tbox.Text += " ";
                                    tbox.SelectionStart = tbox.Text.Length;
                                    tbox.SelectionLength = 0;

                                    break;
                                }
                            case "{shift}":
                                {
                                    break;
                                }
                            case "{tab}":
                                {
                                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);

                                    break;
                                }
                            case "{enter}":
                                {
                                    break;
                                }
                            default:
                                {
                                    tbox.Text += Key;
                                    tbox.SelectionStart = tbox.Text.Length;
                                    tbox.SelectionLength = 0;
                                    break;
                                }
                        }
                    }
                    */

                    #endregion
                }
            }
        }

        private List<Button> Buttons { get; set; }
        private KeyboardLayout Layout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public KeyboardDialog()
        {
            this.InitializeComponent();
            Layout = new KeyboardLayout();
            Buttons = new List<Button>();

            var rowCount = Layout.Default.Count();

            var grid = new Grid();

            for (int i = 0; i < rowCount; i++)
            {
                var row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);

                grid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < rowCount; i++)
            {
                var rowGrid = new Grid();
                var keys = Layout.Default[i].Split(" ");

                for (int j = 0; j < keys.Count(); j++)
                {
                    var column = new ColumnDefinition();
                    // Make control keys wider
                    var ln = keys[j].Length > 1 ? 2 : 1;
                    column.Width = new GridLength(ln, GridUnitType.Star);
                    rowGrid.ColumnDefinitions.Add(column);
                }

                for (int j = 0; j < keys.Count(); j++)
                {
                    var btn = new Button();
                    btn.Name = keys[j];

                    switch (keys[j])
                    {
                        default:
                            {
                                btn.Content = keys[j];
                                break;
                            }

                    }

                    btn.AllowFocusOnInteraction = false;
                    btn.HorizontalAlignment = HorizontalAlignment.Stretch;
                    btn.VerticalAlignment = VerticalAlignment.Stretch;

                    btn.Command = new KeyPressed(btn.Name, this);

                    Grid.SetColumn(btn, j);
                    rowGrid.Children.Add(btn);

                    //Add buttons to a list so we don't need to search for them after
                    Buttons.Add(btn);
                }

                Grid.SetRow(rowGrid, i);
                grid.Children.Add(rowGrid);

            }

            this.KeyboardGrid.Children.Add(grid);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Result { get; set; }
    }
}

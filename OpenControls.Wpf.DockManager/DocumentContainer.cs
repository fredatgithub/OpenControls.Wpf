using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OpenControls.Wpf.DockManager
{
    internal class DocumentContainer : ViewContainer
    {
        public DocumentContainer()
        {
            double tabHeaderHeight = (double)FindResource("DocumentPaneTabHeaderHeight");
            RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(tabHeaderHeight, GridUnitType.Pixel) });
            RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(1, GridUnitType.Auto) });
            RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(1, GridUnitType.Star) });

            ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(1, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(4, GridUnitType.Pixel) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(1, GridUnitType.Auto) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(2, GridUnitType.Pixel) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(1, GridUnitType.Auto) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(2, GridUnitType.Pixel) });

            CreateTabControl(0, 0);
            TabHeaderControl.ItemContainerStyle = FindResource("DocumentPaneTabItem") as Style;
            TabHeaderControl.ListBox.VerticalAlignment = VerticalAlignment.Bottom;
            TabHeaderControl.VerticalAlignment = VerticalAlignment.Bottom;

            _gap = new Border();
            _gap.SetResourceReference(HeightProperty, "DocumentPaneContentBarHeight");
            _gap.SetResourceReference(Border.BackgroundProperty, "DocumentPaneContentBarBrush");
            Children.Add(_gap);
      SetRow(_gap, 1);
      SetColumn(_gap, 0);
      SetColumnSpan(_gap, 6);

            _commandsButton = new Button();
            Children.Add(_commandsButton);
      SetRow(_commandsButton, 0);
      SetColumn(_commandsButton, 2);
            _commandsButton.Click += delegate { if (DisplayGeneralMenu != null) DisplayGeneralMenu(); };
            _commandsButton.SetResourceReference(StyleProperty, "DocumentPaneCommandsButtonStyle");

            _listButton = new Button();
            Children.Add(_listButton);
      SetRow(_listButton, 0);
      SetColumn(_listButton, 4);
            _listButton.Click += delegate { Helpers.DisplayItemsMenu(_items, TabHeaderControl, _selectedUserControl); };
            _listButton.SetResourceReference(StyleProperty, "DocumentPaneListButtonStyle");

            Style style = TryFindResource("DocumentPaneScrollIndicatorStyle") as Style;
            if (style != null)
            {
                TabHeaderControl.ArrowStyle = style;
            }
            TabHeaderControl.ActiveArrowBrush = FindResource("DocumentPaneActiveScrollIndicatorBrush") as Brush;
            TabHeaderControl.InactiveArrowBrush = FindResource("DocumentPaneInactiveScrollIndicatorBrush") as Brush;

            CloseDocumentsDialogPrompt = (string)FindResource("CloseDocumentsDialogPrompt");
        }

        private readonly string CloseDocumentsDialogPrompt;

        protected override System.Windows.Forms.DialogResult UserConfirmClose(string documentTitle)
        {
            return System.Windows.Forms.MessageBox.Show(CloseDocumentsDialogPrompt, documentTitle, System.Windows.Forms.MessageBoxButtons.YesNoCancel);
        }

        public void HideCommandsButton()
        {
            _commandsButton.Visibility = Visibility.Collapsed;
        }

        public Style CommandsButtonStyle
        {
            set
            {
                if (value != null)
                {
                    _commandsButton.Style = value;
                }
            }
        }

        private Button _commandsButton;

        public Action DisplayGeneralMenu;

        protected override void SetSelectedUserControlGridPosition()
        {
      SetRow(_selectedUserControl, 3);
      SetColumn(_selectedUserControl, 0);
      SetColumnSpan(_selectedUserControl, 99);
      SetZIndex(_selectedUserControl, 2);
        }

        protected override void CheckTabCount()
        {
            // No need to do anything ... 
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OpenControls.Wpf.DockManager
{
    internal class ToolPaneGroup : DockPane
    {
        public ToolPaneGroup() : base(new ToolContainer())
        {
            Border.SetResourceReference(Border.BackgroundProperty, "ToolPaneBackground");
            Border.SetResourceReference(Border.CornerRadiusProperty, "ToolPaneCornerRadius");
            Border.SetResourceReference(Border.BorderBrushProperty, "ToolPaneBorderBrush");
            Border.SetResourceReference(Border.BorderThicknessProperty, "ToolPaneBorderThickness");

            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;

            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(Border.BorderThickness.Left, GridUnitType.Pixel);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1, GridUnitType.Auto);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinitions.Add(columnDefinition);
            
            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(2, GridUnitType.Pixel);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1, GridUnitType.Auto);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(2, GridUnitType.Pixel);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1, GridUnitType.Auto);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(2, GridUnitType.Pixel);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1, GridUnitType.Auto);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(2, GridUnitType.Pixel);
            ColumnDefinitions.Add(columnDefinition);

            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(Border.BorderThickness.Right, GridUnitType.Pixel);
            ColumnDefinitions.Add(columnDefinition);

            RowDefinitions.Add(new RowDefinition());
            RowDefinitions[0].Height = new GridLength(Border.BorderThickness.Top, GridUnitType.Pixel);
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions[1].Height = new GridLength(1, GridUnitType.Auto);
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions[3].Height = new GridLength(Border.BorderThickness.Bottom, GridUnitType.Pixel);

            HeaderBorder = new Border();
            HeaderBorder.VerticalAlignment = VerticalAlignment.Stretch;
            HeaderBorder.HorizontalAlignment = HorizontalAlignment.Stretch;
            HeaderBorder.SetResourceReference(Border.CornerRadiusProperty, "ToolPaneHeaderCornerRadius");
            HeaderBorder.SetResourceReference(Border.BorderBrushProperty, "ToolPaneHeaderBorderBrush");
            HeaderBorder.SetResourceReference(Border.BorderThicknessProperty, "ToolPaneHeaderBorderThickness");
            IsHighlighted = false;

      SetRow(HeaderBorder, 1);
      SetColumn(HeaderBorder, 1);
      SetColumnSpan(HeaderBorder, ColumnDefinitions.Count - 2);
            Children.Add(HeaderBorder);

            _titleLabel = new Label();
            _titleLabel.FontSize = 12;
            _titleLabel.Padding = new Thickness(4, 0, 0, 0);
            _titleLabel.VerticalAlignment = VerticalAlignment.Center;
            _titleLabel.Background = Brushes.Transparent;
            _titleLabel.Foreground = Brushes.White;
      SetRow(_titleLabel, 1);
      SetColumn(_titleLabel, 1);
            Children.Add(_titleLabel);

            _commandsButton = new Button();
            _commandsButton.VerticalAlignment = VerticalAlignment.Center;
            _commandsButton.SetResourceReference(StyleProperty, "ToolPaneCommandsButtonStyle");
            _commandsButton.Click += delegate { DisplayGeneralMenu(); };
      SetRow(_commandsButton, 1);
      SetColumn(_commandsButton, 4);
            Children.Add(_commandsButton);

            _pinButton = new Button();
            _pinButton.VerticalAlignment = VerticalAlignment.Center;
            _pinButton.LayoutTransform = new System.Windows.Media.RotateTransform();
            _pinButton.SetResourceReference(StyleProperty, "ToolPanePinButtonStyle");
            _pinButton.Click += PinButton_Click;
      SetRow(_pinButton, 1);
      SetColumn(_pinButton, 6);
            Children.Add(_pinButton);

            _closeButton = new Button();
            _closeButton.VerticalAlignment = VerticalAlignment.Center;
            _closeButton.SetResourceReference(StyleProperty, "ToolPaneCloseButtonStyle");
      SetRow(_closeButton, 1);
      SetColumn(_closeButton, 8);
      SetZIndex(_closeButton, 99);
            Children.Add(_closeButton);
            _closeButton.Click += delegate { FireCloseRequest(); };

            IViewContainer.SelectionChanged += DocumentContainer_SelectionChanged;
      SetRow(IViewContainer as System.Windows.UIElement, 2);
      SetColumn(IViewContainer as System.Windows.UIElement, 1);
      SetColumnSpan(IViewContainer as System.Windows.UIElement, ColumnDefinitions.Count - 2);

            _titleLabel.SetResourceReference(Control.FontSizeProperty, "ToolPaneHeaderFontSize");
            _titleLabel.SetResourceReference(Control.FontFamilyProperty, "ToolPaneHeaderFontFamily");
            _titleLabel.SetResourceReference(Control.PaddingProperty, "ToolPaneHeaderTitlePadding");
        }

        public void HideCommandsButton()
        {
            _commandsButton.Visibility = Visibility.Collapsed;
        }

        public Border HeaderBorder;

        private bool _isHighlighted;
        public override bool IsHighlighted
        {
            get
            {
                return _isHighlighted;
            }
            set
            {
                _isHighlighted = value;
                if (value)
                {
                    HeaderBorder.SetResourceReference(Border.BackgroundProperty, "SelectedPaneBrush");
                }
                else
                {
                    HeaderBorder.SetResourceReference(Border.BackgroundProperty, "ToolPaneHeaderBackground");
                }
            }
        }

        public void ShowAsUnPinned()
        {
            (_pinButton.LayoutTransform as System.Windows.Media.RotateTransform).Angle = 90.0;
            (_pinButton.LayoutTransform as System.Windows.Media.RotateTransform).CenterX = 0.5;
            (_pinButton.LayoutTransform as System.Windows.Media.RotateTransform).CenterY = 0.5;
        }

        public event EventHandler UnPinClick;

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            UnPinClick?.Invoke(this, null);
        }

        private void DocumentContainer_SelectionChanged(object sender, EventArgs e)
        {
            _titleLabel.Content = IViewContainer.Title;
        }

        protected Label _titleLabel;

        public string Title { get { return IViewContainer.Title; } }

        private Button _pinButton;
        private Button _closeButton;
        private Button _commandsButton;
        private Point _mouseDownPosition;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition(this);
            if (pt.Y <= HeaderBorder.ActualHeight)
            {
                _mouseDownPosition = pt;
        Mouse.Capture(this);
            }
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
      Mouse.Capture(this, CaptureMode.None);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Mouse.Captured == this)
            {
                Point mousePosition = e.GetPosition(this);
                double xdiff = mousePosition.X - _mouseDownPosition.X;
                double ydiff = mousePosition.Y - _mouseDownPosition.Y;
                if ((xdiff * xdiff + ydiff * ydiff) > 200)
                {

                    FireFloat(true);
          Mouse.Capture(this, CaptureMode.None);
                }
            }
        }
    }
}

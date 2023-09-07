using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using System.Runtime.InteropServices;
using WinRT;


// Turn off  "Enable in-app Toolbar" 
// https://github.com/microsoft/microsoft-ui-xaml/issues/8806

namespace WPF_XAML_Islands_WinUI3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {  
        public Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource? m_dwxs = null;

        public IntPtr m_hWnd = IntPtr.Zero;

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; 
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            m_hWnd = new WindowInteropHelper(this).Handle;
            if (m_dwxs is null)
            {
                m_dwxs = new Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource();
                Microsoft.UI.WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(m_hWnd);
                m_dwxs.Initialize(myWndId);
                var sb = m_dwxs.SiteBridge;
                Windows.Graphics.RectInt32 rect = new Windows.Graphics.RectInt32(382, 20, 700, 600);
                sb.MoveAndResize(rect);                
            }
            string sText = @"  <Grid xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                    xmlns:controls='using:Microsoft.UI.Xaml.Controls'
                    Background='Black'>
                    <controls:ColorPicker x:Name='cp1' IsAlphaEnabled='False' IsMoreButtonVisible='True' Margin='0,10,0,0'>
                    </controls:ColorPicker>
            </Grid>";
            var textRange = new TextRange(rtb1.Document.ContentStart, rtb1.Document.ContentEnd) { Text = sText };          
        }  

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.UI.Xaml.Controls.Grid gridRoot = new Microsoft.UI.Xaml.Controls.Grid
            {
                Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.DarkBlue)
            };

            gridRoot.RowDefinitions.Add(new Microsoft.UI.Xaml.Controls.RowDefinition() { Height = new Microsoft.UI.Xaml.GridLength(100, Microsoft.UI.Xaml.GridUnitType.Pixel) });
            //gridRoot.RowDefinitions.Add(new Microsoft.UI.Xaml.Controls.RowDefinition() { Height = new Microsoft.UI.Xaml.GridLength(40, Microsoft.UI.Xaml.GridUnitType.Pixel) });
            gridRoot.RowDefinitions.Add(new Microsoft.UI.Xaml.Controls.RowDefinition() { Height = new Microsoft.UI.Xaml.GridLength(250, Microsoft.UI.Xaml.GridUnitType.Pixel) });
            gridRoot.RowDefinitions.Add(new Microsoft.UI.Xaml.Controls.RowDefinition() { Height = new Microsoft.UI.Xaml.GridLength(1, Microsoft.UI.Xaml.GridUnitType.Star) });
            gridRoot.RowDefinitions.Add(new Microsoft.UI.Xaml.Controls.RowDefinition() { Height = new Microsoft.UI.Xaml.GridLength(100, Microsoft.UI.Xaml.GridUnitType.Pixel) });
            //gridRoot.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            Microsoft.UI.Xaml.Controls.CalendarDatePicker cdp = new Microsoft.UI.Xaml.Controls.CalendarDatePicker()
            {
                PlaceholderText = "Pick a date",
                Header = "Calendar Date Picker",
                VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center,
                HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center
            };
            cdp.SetValue(Microsoft.UI.Xaml.Controls.Grid.RowProperty, 0);
            gridRoot.Children.Add(cdp);

            //string sApplicationPath = "pack://application:,,,";
            string sApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
            List<string> strings = new List<string> {
                sApplicationPath + @"Assets\Beach.jpg",
                sApplicationPath + @"Assets\Beach2.jpg",
                sApplicationPath + @"Assets\Island.jpg",
                sApplicationPath + @"Assets\Sunset.jpg",
                sApplicationPath + @"Assets\Sunset2.jpg",
                sApplicationPath + @"Assets\Sunset3.jpg",
                sApplicationPath + @"Assets\Paradise.jpg"};
            var items = new List<Microsoft.UI.Xaml.FrameworkElement>();
            foreach (string s in strings)
            {
                var img = new Microsoft.UI.Xaml.Controls.Image() { Source = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s)) };
                items.Add(img);
            }

            Microsoft.UI.Xaml.Controls.StackPanel sp11, sp12;
            Microsoft.UI.Xaml.Controls.TextBlock tb1, tb2;
            Microsoft.UI.Xaml.Controls.FlipView fv;
            Microsoft.UI.Xaml.Controls.MediaPlayerElement mpe;

            Microsoft.UI.Xaml.Controls.Grid grid1 = new Microsoft.UI.Xaml.Controls.Grid
            {
                //Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Red),
                Children =
                {
                    (sp11 = new Microsoft.UI.Xaml.Controls.StackPanel
                    {
                        Orientation = Microsoft.UI.Xaml.Controls.Orientation.Vertical,
                        HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                        Children =
                        {
                            (tb1 = new Microsoft.UI.Xaml.Controls.TextBlock
                            {
                                Text = "Test FlipView",
                                Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Yellow),
                                //VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Top,
                                HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                                FontSize = 20,
                            }),
                            (fv = new Microsoft.UI.Xaml.Controls.FlipView
                            {
                                Width = 340,
                                Height = 200,
                                Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Black),
                                //VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center,
                                Margin = new Microsoft.UI.Xaml.Thickness(0, 5, 0, 0),
                                HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                                ItemsSource = items
                            })
                        }
                    }),
                    (sp12 = new Microsoft.UI.Xaml.Controls.StackPanel
                    {
                        Orientation = Microsoft.UI.Xaml.Controls.Orientation.Vertical,
                        HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                        Children =
                        {
                             (tb2 = new Microsoft.UI.Xaml.Controls.TextBlock
                             {                               
                                 Text = "Test MediaPlayer",
                                 Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Yellow),
                                 VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Top,
                                 HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                                 FontSize = 20,
                             }),
                             (mpe = new Microsoft.UI.Xaml.Controls.MediaPlayerElement
                             {
                                Name="mp1",
                                Width = 340,
                                Height = 200,
                                Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Black),
                                //VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center,
                                Margin = new Microsoft.UI.Xaml.Thickness(0, 5, 0, 0),
                                HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                                AreTransportControlsEnabled = true,
                                Source = Windows.Media.Core.MediaSource.CreateFromUri(new Uri("https://storage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4")),
                                //AutoPlay = true
                             })
                        }
                    })
                }
            };
            mpe.TransportControls.IsCompact = true;
            grid1.ColumnDefinitions.Add(new Microsoft.UI.Xaml.Controls.ColumnDefinition() { Width = new Microsoft.UI.Xaml.GridLength(1, Microsoft.UI.Xaml.GridUnitType.Star) });
            grid1.ColumnDefinitions.Add(new Microsoft.UI.Xaml.Controls.ColumnDefinition() { Width = new Microsoft.UI.Xaml.GridLength(1, Microsoft.UI.Xaml.GridUnitType.Star) });

            grid1.SetValue(Microsoft.UI.Xaml.Controls.Grid.RowProperty, 1);

            sp11.SetValue(Microsoft.UI.Xaml.Controls.Grid.ColumnProperty, 0);
            sp12.SetValue(Microsoft.UI.Xaml.Controls.Grid.ColumnProperty, 1);
            gridRoot.Children.Add(grid1);

            // Test CommandBar from MSDN
            // https://learn.microsoft.com/en-us/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.commandbar?view=windows-app-sdk-1.4

            Microsoft.UI.Xaml.Controls.CommandBar cb = new Microsoft.UI.Xaml.Controls.CommandBar
            {
                Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Black),
                VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center,
                Content = 
                    (new Microsoft.UI.Xaml.Controls.TextBlock
                    {
                        Text ="Test CommandBar from MSDN...",
                        Margin = new Microsoft.UI.Xaml.Thickness(12, 8, 0, 0),                       
                    }),
            };

            Microsoft.UI.Xaml.Controls.AppBarToggleButton shuffle = new Microsoft.UI.Xaml.Controls.AppBarToggleButton
            {
                Label = "Shuffle",
                Icon = new Microsoft.UI.Xaml.Controls.SymbolIcon(Microsoft.UI.Xaml.Controls.Symbol.Shuffle)
            };
            shuffle.Click += AppBarButton_Click; 
            cb.PrimaryCommands.Add(shuffle);

            Microsoft.UI.Xaml.Controls.AppBarToggleButton RepeatAll = new Microsoft.UI.Xaml.Controls.AppBarToggleButton
            {
                Label = "Repeat",
                Icon = new Microsoft.UI.Xaml.Controls.SymbolIcon(Microsoft.UI.Xaml.Controls.Symbol.RepeatAll)
            };
            RepeatAll.Click += AppBarButton_Click;
            cb.PrimaryCommands.Add(RepeatAll);

            Microsoft.UI.Xaml.Controls.AppBarSeparator abs = new Microsoft.UI.Xaml.Controls.AppBarSeparator
            {
                
            };
            cb.PrimaryCommands.Add(abs);

            Microsoft.UI.Xaml.Controls.AppBarButton back = new Microsoft.UI.Xaml.Controls.AppBarButton
            {
                Label = "Back",
                Icon = new Microsoft.UI.Xaml.Controls.SymbolIcon(Microsoft.UI.Xaml.Controls.Symbol.Back)
            };
            back.Click += AppBarButton_Click;
            cb.PrimaryCommands.Add(back);

            Microsoft.UI.Xaml.Controls.AppBarButton stop = new Microsoft.UI.Xaml.Controls.AppBarButton
            {
                Label = "Stop",
                Icon = new Microsoft.UI.Xaml.Controls.SymbolIcon(Microsoft.UI.Xaml.Controls.Symbol.Stop)
            };
            stop.Click += AppBarButton_Click;
            cb.PrimaryCommands.Add(stop);

            Microsoft.UI.Xaml.Controls.AppBarButton play = new Microsoft.UI.Xaml.Controls.AppBarButton
            {
                Label = "Play",
                Icon = new Microsoft.UI.Xaml.Controls.SymbolIcon(Microsoft.UI.Xaml.Controls.Symbol.Play)
            };
            play.Click += AppBarButton_Click;
            cb.PrimaryCommands.Add(play);

            Microsoft.UI.Xaml.Controls.AppBarButton forward = new Microsoft.UI.Xaml.Controls.AppBarButton
            {
                Label = "Forward",
                Icon = new Microsoft.UI.Xaml.Controls.SymbolIcon(Microsoft.UI.Xaml.Controls.Symbol.Forward)
            };
            forward.Click += AppBarButton_Click;
            cb.PrimaryCommands.Add(forward);

            Microsoft.UI.Xaml.Controls.AppBarButton like = new Microsoft.UI.Xaml.Controls.AppBarButton
            {
                Label = "Like",
                Icon = new Microsoft.UI.Xaml.Controls.SymbolIcon(Microsoft.UI.Xaml.Controls.Symbol.Like)
            };
            like.Click += AppBarButton_Click;
            cb.SecondaryCommands.Add(like);

            Microsoft.UI.Xaml.Controls.AppBarButton dislike = new Microsoft.UI.Xaml.Controls.AppBarButton
            {
                Label = "Dislike",
                Icon = new Microsoft.UI.Xaml.Controls.SymbolIcon(Microsoft.UI.Xaml.Controls.Symbol.Dislike)
            };
            dislike.Click += AppBarButton_Click;
            cb.SecondaryCommands.Add(dislike);

            cb.SetValue(Microsoft.UI.Xaml.Controls.Grid.RowProperty, 2);
            gridRoot.Children.Add(cb);

            Microsoft.UI.Xaml.Controls.ToggleSwitch ts;
            Microsoft.UI.Xaml.Controls.ProgressRing pg;
            Microsoft.UI.Xaml.Controls.StackPanel sp2 = new Microsoft.UI.Xaml.Controls.StackPanel()
            {
                Orientation = Microsoft.UI.Xaml.Controls.Orientation.Horizontal,
                Children =
                {
                    (ts = new Microsoft.UI.Xaml.Controls.ToggleSwitch
                    {
                        Header = "Toggle work",
                        OffContent = "Do work",
                        OnContent = "Working",
                        VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Top,
                        //HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                        Margin = new Microsoft.UI.Xaml.Thickness(180, 15, 0, 0),
                        IsOn = false
                    }),
                    (pg = new Microsoft.UI.Xaml.Controls.ProgressRing
                    {
                        Width = 50,
                        Height = 50,
                        VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Top,
                        //HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                        Margin = new Microsoft.UI.Xaml.Thickness(100, 15, 0, 0),
                        IsActive = false
                    })
                }
            };
            ts.Toggled += (sender, eargs) =>
            {
                Microsoft.UI.Xaml.Controls.ToggleSwitch? ts = sender as Microsoft.UI.Xaml.Controls.ToggleSwitch;
                pg.IsActive = ts.IsOn ? true : false;
            };
            sp2.SetValue(Microsoft.UI.Xaml.Controls.Grid.RowProperty, 3);
            gridRoot.Children.Add(sp2);

            if (m_dwxs != null)
                m_dwxs.Content = gridRoot;

            // For CommandBar
            //gridRoot.UpdateLayout();           
        }

        private void btn2_Click(object sender, System.Windows.RoutedEventArgs e)
        {  
            string sText = new TextRange(rtb1.Document.ContentStart, rtb1.Document.ContentEnd).Text;
            Microsoft.UI.Xaml.UIElement? uiElement = null;
            try
            {
                uiElement = Microsoft.UI.Xaml.Markup.XamlReader.Load(sText) as Microsoft.UI.Xaml.UIElement;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (m_dwxs != null)
            {
                try
                {
                    m_dwxs.Content = uiElement;
                    var childElement = FindChildElementByName(uiElement, "cp1");
                    if (childElement != null)
                    {
                        try
                        {
                            Microsoft.UI.Xaml.Controls.ColorPicker cp = (Microsoft.UI.Xaml.Controls.ColorPicker)childElement;
                            cp.ColorChanged += (sender, args) =>
                            {
                                this.Background = new SolidColorBrush(Color.FromRgb(args.NewColor.R, args.NewColor.G, args.NewColor.B));
                            };
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void AppBarButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            Console.Beep(8000, 10);
        }

        // Adapted from MS C++ sample
        private Microsoft.UI.Xaml.DependencyObject? FindChildElementByName(Microsoft.UI.Xaml.DependencyObject tree, string sName)
        {
            for (int i = 0; i < Microsoft.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(tree); i++)
            {
                Microsoft.UI.Xaml.DependencyObject child = Microsoft.UI.Xaml.Media.VisualTreeHelper.GetChild(tree, i);
                if (child != null && ((Microsoft.UI.Xaml.FrameworkElement)child).Name == sName)
                    return child;
                else
                {
                    Microsoft.UI.Xaml.DependencyObject? childInSubtree = FindChildElementByName(child, sName);
                    if (childInSubtree != null)
                        return childInSubtree;
                }
            }
            return null;
        }

        ChildType? FindChildElement<ChildType>(Microsoft.UI.Xaml.DependencyObject tree) where ChildType : Microsoft.UI.Xaml.DependencyObject
        {
            for (int i = 0; i < Microsoft.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(tree); i++)
            {
                Microsoft.UI.Xaml.DependencyObject child = Microsoft.UI.Xaml.Media.VisualTreeHelper.GetChild(tree, i);
                if (child != null && child is ChildType)
                {
                    return child as ChildType;
                }
                else
                {
                    ChildType childInSubtree = FindChildElement<ChildType>(child);
                    if (childInSubtree != null)
                    {
                        return childInSubtree;
                    }
                }
            }
            return null;
        }
    }
}


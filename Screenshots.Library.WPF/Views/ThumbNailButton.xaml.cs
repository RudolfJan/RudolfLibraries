using Screenshots.Library.Models;
using System.Windows;

namespace Screenshots.Library.WPF.Views
  {
  /// <summary>
  /// Interaction logic for ThumbNailButton.xaml
  /// </summary>
  ///
  /// https://www.stevefenton.co.uk/2012/09/wpf-bubbling-a-command-from-a-child-view/
  ///  https://stackoverflow.com/questions/3067617/raising-an-event-on-parent-window-from-a-user-control-in-net-c-sharp


  public partial class ThumbNailButton
    {
    public ImageModel Screenshot
      {
      get { return (ImageModel)GetValue(ScreenshotProperty); }
      set { SetValue(ScreenshotProperty, value); }
      }

    public readonly DependencyProperty ScreenshotProperty =
        DependencyProperty.Register("Screenshot", typeof(ImageModel), typeof(ThumbNailButton),
            new PropertyMetadata(null));

    public int ThumbnailHeight
      {
      get { return (int)GetValue(ThumbnailHeightProperty); }
      set { SetValue(ThumbnailHeightProperty, value); }
      }

    public readonly DependencyProperty ThumbnailHeightProperty =
        DependencyProperty.Register("ThumbnailHeight", typeof(int), typeof(ThumbNailButton));

    public int ThumbnailWidth
      {
      get { return (int)GetValue(ThumbnailWidthProperty); }
      set { SetValue(ThumbnailWidthProperty, value); }
      }

    public readonly DependencyProperty ThumbnailWidthProperty =
        DependencyProperty.Register("ThumbnailWidth", typeof(int), typeof(ThumbNailButton));

    public ThumbNailButton()
      {
      InitializeComponent();
      }

    public readonly RoutedEvent ThumbnailClickedEvent = EventManager.RegisterRoutedEvent(
        "Click", // Event name
        RoutingStrategy.Bubble, // Bubble means the event will bubble up through the tree
        typeof(RoutedEventHandler), // The event type
        typeof(ThumbNailButton)); // Belongs to ThumbNailButton

    public event RoutedEventHandler ThumbnailClickedEventHandler
      {
      add { AddHandler(ThumbnailClickedEvent, value); }
      remove { RemoveHandler(ThumbnailClickedEvent, value); }
      }

    private void OnThumbNailPartClicked(object Sender, RoutedEventArgs E)
      {
      var ThumbnailClickedRoutedEventArgs = new RoutedEventArgs(ThumbnailClickedEvent);
      RaiseEvent(ThumbnailClickedRoutedEventArgs);
      }

    public readonly RoutedEvent ThumbnailDoubleClickedEvent = EventManager.RegisterRoutedEvent(
      "DoubleClick", // Event name
      RoutingStrategy.Bubble, // Bubble means the event will bubble up through the tree
      typeof(RoutedEventHandler), // The event type
      typeof(ThumbNailButton)); // Belongs to ThumbNailButton

    public event RoutedEventHandler ThumbnailDoubleClickedEventHandler
      {
      add { AddHandler(ThumbnailDoubleClickedEvent, value); }
      remove { RemoveHandler(ThumbnailDoubleClickedEvent, value); }
      }

    private void OnThumbNailPartDoubleClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
      {
      var ThumbnailDoubleClickedRoutedEventArgs = new RoutedEventArgs(ThumbnailDoubleClickedEvent);
      RaiseEvent(ThumbnailDoubleClickedRoutedEventArgs);
      }
    }
  }

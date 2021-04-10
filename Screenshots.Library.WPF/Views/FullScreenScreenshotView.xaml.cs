using System.Windows;
using System.Windows.Input;

namespace Screenshots.Library.WPF.Views
  {
  public partial class FullScreenScreenshotView : Window
    {
    public string ImagePath { get; set; }

    public FullScreenScreenshotView()
      {
      InitializeComponent();
      DataContext = ImagePath;
      }

    private void OnKeyPressed(object sender, KeyEventArgs e)
      {
      if (e.Key == Key.Escape)
        Close();
      }

    private void OnMouseUp(object sender, MouseButtonEventArgs e)
      {
      Close();
      }
    }
  }

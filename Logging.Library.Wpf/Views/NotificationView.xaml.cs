using System;
using System.Windows;
using System.Windows.Threading;

namespace Logging.Library.Wpf.Views
  {

  public partial class NotificationView
    {

    public NotificationView()
      {
      InitializeComponent();
      var width = SystemParameters.PrimaryScreenWidth;
      Left = width / 2 - Left / 2;

      StartCloseTimer();
      }

    private void StartCloseTimer()
      {
      DispatcherTimer timer = new DispatcherTimer();
      timer.Interval = TimeSpan.FromSeconds(4d);
      timer.Tick += TimerTick;
      timer.Start();
      }

    private void TimerTick(object sender, EventArgs e)
      {
      DispatcherTimer timer = (DispatcherTimer)sender;
      timer.Stop();
      timer.Tick -= TimerTick;
      Close();
      }
    }
  }

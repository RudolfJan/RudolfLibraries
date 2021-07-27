using Caliburn.Micro;

namespace Logging.Library.Wpf.ViewModels
  {
  public class NotificationViewModel : Screen
    {
    public string Message { get; set; }

    protected override void OnViewLoaded(object view)
      {
      base.OnViewLoaded(view);
       }
    }
  }
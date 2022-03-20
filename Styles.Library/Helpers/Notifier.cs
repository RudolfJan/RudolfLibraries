using System;
using System.ComponentModel;

namespace Styles.Library.Helpers
  {
  public class Notifier : INotifyPropertyChanged

    {
    public event PropertyChangedEventHandler
      PropertyChanged;

    protected void NotifyOfPropertyChange(String PropertyName)
      {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
      }
    protected void OnPropertyChanged(String PropertyName)
      {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
      }
    }
  }

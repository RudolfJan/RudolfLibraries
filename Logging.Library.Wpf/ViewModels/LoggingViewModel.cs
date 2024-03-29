﻿using Caliburn.Micro;
using Logging.Library.Wpf.Models;
using Styles.Library.Helpers;
using Styles.Library.Models;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Logging.Library.Wpf.ViewModels
{
  public class LoggingViewModel : Screen
  {
    public LoggingModel Logging { get; set; } = new LoggingModel();
    public bool _debugLogging = true;
    public bool DebugLogging
    {
      get { return _debugLogging; }
      set
      {
        _debugLogging = value;
        NotifyOfPropertyChange(() => DebugLogging);
        Logging.Filter.DebugChecked = DebugLogging;
        ChangeFilter();
      }
    }


    private LogEntryClass _selectedLogEntry;
    public LogEntryClass SelectedLogEntry
    {
      get { return _selectedLogEntry; }
      set
      {
        _selectedLogEntry = value;
        NotifyOfPropertyChange(() => SelectedLogEntry);
      }
    }

    public bool _messageLogging = true;
    public bool MessageLogging
    {
      get { return _messageLogging; }
      set
      {
        _messageLogging = value;
        NotifyOfPropertyChange(() => MessageLogging);
        Logging.Filter.MessageChecked = MessageLogging;
        ChangeFilter();
      }
    }

    public bool _errorLogging = true;
    public bool ErrorLogging
    {
      get { return _errorLogging; }
      set
      {
        _errorLogging = value;
        NotifyOfPropertyChange(() => ErrorLogging);
        Logging.Filter.ErrorChecked = ErrorLogging;
        ChangeFilter();
      }
    }

    public bool _eventLogging = true;
    public bool EventLogging
    {
      get { return _eventLogging; }
      set
      {
        _eventLogging = value;
        NotifyOfPropertyChange(() => EventLogging);
        Logging.Filter.EventChecked = EventLogging;
        ChangeFilter();
      }
    }

    public bool _informUserLogging = true;
    public bool InformUserLogging
    {
      get { return _informUserLogging; }
      set
      {
        _informUserLogging = value;
        NotifyOfPropertyChange(() => InformUserLogging);
        Logging.Filter.InformUserChecked = InformUserLogging;
        ChangeFilter();
      }
    }

    protected override void OnViewLoaded(object view)
    {
      base.OnViewLoaded(view);
      Logging.FilteredLogging = new BindableCollection<LogEntryClass>();
      CreateTestData();
      // Setup initial values for logging filter
      ChangeFilter();
      LogEventHandler.LogEvent += LogEventHandlerOnLogEvent;
    }

    private void LogEventHandlerOnLogEvent(object sender, LogEventArgs e)
    {
      ChangeFilter();
    }

    [Conditional("DEBUG")]
    private static void CreateTestData()
    {
      LogCollectionManager.LogEvents.Add(new LogEntryClass("", "", 1, "Test line Message", null, LogEventType.Message));
      LogCollectionManager.LogEvents.Add(new LogEntryClass("", "", 2, "Test line Message", null, LogEventType.Message));
      LogCollectionManager.LogEvents.Add(new LogEntryClass("", "", 2, "Test line Debug", null, LogEventType.Debug));
      LogCollectionManager.LogEvents.Add(new LogEntryClass("", "", 2, "Test line Error", null, LogEventType.Error));
      LogCollectionManager.LogEvents.Add(new LogEntryClass("", "", 2, "Test line Error", null, LogEventType.Error));
      LogCollectionManager.LogEvents.Add(new LogEntryClass("", "", 2, "Test line Event", null, LogEventType.Event));
      LogCollectionManager.LogEvents.Add(new LogEntryClass("", "", 2, "Test line Error", null, LogEventType.Error));
    }

    private void ChangeFilter()
    {
      Logging.FilteredLogging.Clear();
      foreach (var item in LogCollectionManager.LogEvents)
      {
        if (Logging.Filter.EventTypeFilter(item))
        {
          Logging.FilteredLogging.Add(item);
        }
        Logging.FilteredLogging.Refresh();
      }
    }

    public void ClearLog()
    {
      Logging.FilteredLogging.Clear();
      LogCollectionManager.LogEvents.Clear();
      Logging.FilteredLogging.Refresh();
    }

    public static void SaveLog(string dataPath) //Settings.DataPath
    {
      var fileSaveParams = new SaveFileModel
      {
        Title = "Save log file as csv",
        InitialDirectory = dataPath
      };
      var outputFile = FileIOHelpers.GetSaveFileName(fileSaveParams); if (outputFile.Length > 0)
      {
        var allText = LogEntryClass.WriteCsvHeaderLine();
        foreach (var X in LogCollectionManager.LogEvents)
        {
          allText += X.WriteAsCsv();
        }
        File.WriteAllText(outputFile, allText);
      }
    }

    public Task OKButton()
    {
      return TryCloseAsync();
    }

  }
}

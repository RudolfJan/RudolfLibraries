using System;

namespace Logging.Library
  {
  public class LogFilter
    {
    private Boolean _DebugChecked = true;
    public Boolean DebugChecked
      {
      get => _DebugChecked;
      set
        {
        _DebugChecked = value;
        }
      }

    private Boolean _ErrorChecked = true;
    public Boolean ErrorChecked
      {
      get => _ErrorChecked;
      set
        {
        _ErrorChecked = value;
        }
      }

    private Boolean _MessageChecked = true;
    public Boolean MessageChecked
      {
      get => _MessageChecked;
      set
        {
        _MessageChecked = value;
        }
      }

    private Boolean _EventChecked = true;
    public Boolean EventChecked
      {
      get => _EventChecked;
      set
        {
        _EventChecked = value;
        }
      }

    private Boolean _informUserChecked = true;
    public Boolean InformUserChecked
      {
      get => _informUserChecked;
      set
        {
        _informUserChecked = value;
        }
      }


    public LogFilter(Boolean debugChecked, Boolean errorChecked, Boolean messageChecked, Boolean eventChecked, bool informUserChecked)
      {
      UpdateFilterSettings(debugChecked, errorChecked, messageChecked, eventChecked, informUserChecked);
      }

    public void UpdateFilterSettings(Boolean debugChecked, Boolean errorChecked, Boolean messageChecked, Boolean eventChecked, bool informUserChecked)
      {
      DebugChecked = debugChecked;
      ErrorChecked = errorChecked;
      MessageChecked = messageChecked;
      EventChecked = eventChecked;
      InformUserChecked = informUserChecked;
      }


    public Boolean EventTypeFilter(object Item)
      {
      var MyItem = (LogEntryClass)Item;
      if (MyItem != null)
        {
        switch (MyItem.EventType)
          {
          case LogEventType.Error:
              {
              return ErrorChecked;
              }
          case LogEventType.Debug:
              {
              return DebugChecked;
              }
          case LogEventType.Message:
              {
              return MessageChecked;
              }
          case LogEventType.Event:
              {
              return EventChecked;
              }
          case LogEventType.InformUser:
              {
              return InformUserChecked;
              }
          default:
              {
              return false;
              }
          }
        }
      return false;
      }
    }
  }

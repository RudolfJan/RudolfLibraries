using Logging.Library;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ThumbnailSharp;

namespace Screenshots.Library.Logic
  {
  public class ThumbnailLogic
    {
    public static uint ThumbnailWidth { get; set; } = 180;
    public static void CreateThumbNail(string imagePath, string thumbnailPath)
      {
      Format format;
      var extension = Path.GetExtension(imagePath).ToLower();
      switch (extension)
        {
        case ".jpg":
            {
            format = Format.Jpeg;
            break;
            }
        case ".jpeg":
            {
            format = Format.Jpeg;
            break;
            }
        case ".png":
            {
            format = Format.Png;
            break;
            }
        default:
            {
            throw new NotSupportedException($"File type {extension} not supported for thumbnail");
            }
        }

      try
        {
        using (Stream sourceStream = new ThumbnailCreator().CreateThumbnailStream(ThumbnailWidth, imagePath, format))
          {
          if (sourceStream != null)
            {
            using (FileStream fs = new FileStream(thumbnailPath, FileMode.OpenOrCreate, FileAccess.Write))
              {
              if (sourceStream.Position != 0)
                {
                sourceStream.Position = 0;
                }
              sourceStream.CopyTo(fs);
              }

            }
          }
        }
      catch (Exception ex)
        {
        var result = Log.Trace($"Failed to create thumbnail", ex, LogEventType.Error);
        throw new Exception(result);
        }
      }
    }
  }

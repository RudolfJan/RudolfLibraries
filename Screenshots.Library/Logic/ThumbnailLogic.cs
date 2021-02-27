using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThumbnailSharp;

namespace Screenshots.Library.Logic
  {
  public class ThumbnailLogic
    {
    public static uint ThumbnailWidth { get; set; } = 180;
    public static BitmapImage CreateThumbNail(string imagePath)
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
        byte[] ResultBytes = new ThumbnailCreator().CreateThumbnailBytes(
          thumbnailSize: ThumbnailWidth,
          imageFileLocation: imagePath,
          imageFormat: format);
        return ByteArrayToBitmap(ResultBytes);
        }
      catch (Exception ex)
        {
        throw new InvalidOperationException("Cannot create Thumbnail", ex);
        }
      }

    private static BitmapImage ByteArrayToBitmap(Byte[] ByteArray)
      {
      MemoryStream Stream = new MemoryStream(ByteArray);

      BitmapImage BitmapImage = new BitmapImage();
      BitmapImage.BeginInit();
      BitmapImage.StreamSource = Stream;
      BitmapImage.EndInit();

      return BitmapImage;
      }

    // https://stackoverflow.com/questions/35804375/how-do-i-save-a-bitmapimage-from-memory-into-a-file-in-wpf-c
    public static void SaveThumbNail(BitmapImage image, string filePath)
      {
      BitmapEncoder encoder = new PngBitmapEncoder();
      encoder.Frames.Add(BitmapFrame.Create(image));

      using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
        encoder.Save(fileStream);
        }
      }

    }
  }

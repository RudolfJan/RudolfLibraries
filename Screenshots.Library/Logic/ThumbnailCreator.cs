/*MIT License

Copyright(c) 2017 Mirza Ghulam Rasyid

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

// https://github.com/mirzaevolution/ThumbnailSharp

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;


namespace ThumbnailSharp
  {
  /// <summary>
  /// Image format to use when creating a thumbnail.
  /// </summary>
  public enum Format
    {
    Jpeg,
    Bmp,
    Png,
    Gif,
    Tiff

    }
  /// <summary>
  /// Thumbnail class that holds various methods to create an image thumbnail.
  /// </summary>
  public class ThumbnailCreator
    {
    private Bitmap CreateBitmapThumbnail(uint thumbnailSize, string imageFileLocation)
      {
      Bitmap bitmap = null;
      Image image;
      try
        {
        image = Image.FromFile(imageFileLocation);
        }
      catch
        {
        image = null;
        }
      if (image != null)
        {
        float actualHeight = image.Height;
        float actualWidth = image.Width;
        uint thumbnailHeight;
        uint thumbnailWidth;
        if (actualHeight > actualWidth)
          {
          if ((uint)actualHeight <= thumbnailSize)
            throw new Exception("Thumbnail size must be less than actual height (portrait image)");
          thumbnailHeight = thumbnailSize;
          thumbnailWidth = (uint)((actualWidth / actualHeight) * thumbnailSize);
          }
        else if (actualWidth > actualHeight)
          {
          if ((uint)actualWidth <= thumbnailSize)
            throw new Exception("Thumbnail size must be less than actual width (landscape image)");
          thumbnailWidth = thumbnailSize;
          thumbnailHeight = (uint)((actualHeight / actualWidth) * thumbnailSize);
          }
        else
          {
          if ((uint)actualWidth <= thumbnailSize)
            throw new Exception("Thumbnail size must be less than image's size");
          thumbnailWidth = thumbnailSize;
          thumbnailHeight = thumbnailSize;
          }
        try
          {
          bitmap = new Bitmap((int)thumbnailWidth, (int)thumbnailHeight);
          Graphics resizedImage = Graphics.FromImage(bitmap);
          resizedImage.InterpolationMode = InterpolationMode.HighQualityBicubic;
          resizedImage.CompositingQuality = CompositingQuality.HighQuality;
          resizedImage.SmoothingMode = SmoothingMode.HighQuality;
          resizedImage.DrawImage(image, 0, 0, thumbnailWidth, thumbnailHeight);
          }
        catch
          {
          bitmap = null;
          }
        }
      return bitmap;
      }
 
    private ImageFormat GetImageFormat(Format format)
      {
      return format switch
        {
          Format.Jpeg => ImageFormat.Jpeg,
          Format.Bmp => ImageFormat.Bmp,
          Format.Png => ImageFormat.Png,
          Format.Gif => ImageFormat.Gif,
          _ => ImageFormat.Tiff,
          };
      }
  
    public Stream CreateThumbnailStream(uint thumbnailSize, string imageFileLocation, Format imageFormat)
      {
      if (String.IsNullOrEmpty(imageFileLocation))
        throw new ArgumentNullException(nameof(imageFileLocation), "'imageFileLocation' cannot be null");
      if (!File.Exists(imageFileLocation))
        throw new FileNotFoundException($"'{imageFileLocation}' cannot be found");
      Bitmap bitmap = CreateBitmapThumbnail(thumbnailSize, imageFileLocation);
      if (bitmap != null)
        {
        MemoryStream stream = new MemoryStream();
        bitmap.Save(stream, GetImageFormat(imageFormat));
        stream.Position = 0;
        return stream;
        }
      return null;
      }
    }
  }

using Screenshots.Library.DataAccess;
using Screenshots.Library.Models;
using Styles.Library.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utilities.Library;

namespace Screenshots.Library.Logic
  {

  public class ImageManager : Notifier
    {
    public static string ThumbnailBasePath = "C:\\Temp\\Thumbnails\\";
    private List<ImageModel> _ImageList;
    public List<ImageModel> ImageList
      {
      get { return _ImageList; }
      set
        {
        _ImageList = value;
        OnPropertyChanged("ImageList");
        }
      }

    private ImageModel _SelectedImage;
    public ImageModel SelectedImage
      {
      get { return _SelectedImage; }
      set
        {
        _SelectedImage = value;
        OnPropertyChanged("SelectedImage");
        }
      }
    private string _ImageDescription;
    public string ImageDescription
      {
      get { return _ImageDescription; }
      set
        {
        _ImageDescription = value;
        OnPropertyChanged("ImageDescription");
        }
      }

    public ImageManager()
      {
      ImageList = ImageDataAccess.GetAllImages();
      }

    public static string GetThumbnalPathForCollection(CollectionModel collection)
      {
      return $"{ThumbnailBasePath}\\CollectionId_{collection.Id}\\";
      }

    public static List<ImageModel> LoadNewImagesForAllCollections()
      {
      List<ImageModel> newImages = new List<ImageModel>();

      var collectionList = CollectionDataAccess.GetAllCollections();
      foreach (var collection in collectionList)
        {
        var thumbnailCollectionPath = GetThumbnalPathForCollection(collection);
        Directory.CreateDirectory(thumbnailCollectionPath);
        newImages = newImages.Concat(LoadNewImagesForCollection(collection)).ToList();
        }

      return newImages;
      }

    private static List<ImageModel> LoadNewImagesForCollectionsByType(CollectionModel collection,
      DirectoryInfo dir, string imageType)
      {
      List<ImageModel> newImages = new List<ImageModel>();
      FileInfo[] files = dir.GetFiles($"*.{imageType}", SearchOption.TopDirectoryOnly);
      foreach (var file in files)
        {
        string thumbnailPath =
          $"{ThumbnailBasePath}{Path.GetFileNameWithoutExtension(file.Name)}.png";
        var image = new ImageModel
          {
          ImageDescription = string.Empty,
          ImageThumbnailPath = thumbnailPath,
          ImagePath = file.Name,
          CollectionId = collection.Id
          };
        image.Id = ImageDataAccess.InsertImage(image);
        newImages.Add(image);

        var thumbNail = ThumbnailLogic.CreateThumbNail($"{collection.CollectionPath}{file.Name}");
        ThumbnailLogic.SaveThumbNail(thumbNail, thumbnailPath);
        }
      return newImages;
      }

    private static List<ImageModel> LoadNewImagesForCollection(CollectionModel collection)
      {
      List<ImageModel> newImages = new List<ImageModel>();
      DirectoryInfo dir = new DirectoryInfo(collection.CollectionPath);
      newImages = newImages.Concat(LoadNewImagesForCollectionsByType(collection, dir, "jpg")).ToList();
      newImages = newImages.Concat(LoadNewImagesForCollectionsByType(collection, dir, "png")).ToList();
      newImages = newImages.Concat(LoadNewImagesForCollectionsByType(collection, dir, "jpeg")).ToList();
      return newImages;
      }

    public static void DeleteImage(ImageModel image)
      {
      if (image.ImageThumbnailPath.Length > 0)
        {
        FileHelpers.DeleteSingleFile(image.ImageThumbnailPath);
        }
      var collection = CollectionDataAccess.GetCollectionById(image.CollectionId);
      FileHelpers.DeleteSingleFile($"{collection.CollectionPath}{image.ImagePath}");
      ImageDataAccess.DeleteImage(image.Id);
      }

    public static void DeleteImagesInCollection(CollectionModel collection, bool deleteFiles = false)
      {
      var thumbnailCollectionPath = GetThumbnalPathForCollection(collection);
      FileHelpers.EmptyDirectory(thumbnailCollectionPath);
      Directory.Delete(thumbnailCollectionPath);
      var imageList = ImageDataAccess.GetImagesByCollectionId(collection.Id);
      if (deleteFiles)
        {
        foreach (var image in imageList)
          {
          FileHelpers.DeleteSingleFile($"{collection.CollectionPath}{image.ImagePath}");
          }
        }
      ImageDataAccess.DeleteImagesInCollection(collection.Id);
      }

    public static void DeleteOrphanImagesFromDatabase()
      {
      var collectionList = CollectionDataAccess.GetAllCollections();
      foreach (var collection in collectionList)
        {
        var imageList = ImageDataAccess.GetImagesByCollectionId(collection.Id);
        foreach (var image in imageList)
          {
          if (!File.Exists($"{collection.CollectionPath}{image.ImagePath}"))
            {
            FileHelpers.DeleteSingleFile(image.ImageThumbnailPath);
            ImageDataAccess.DeleteImage(image.Id);
            }
          }
        }
      }

    public void AddTag(int imageId, int tagId)
      {
      var imageTag = new ImageTagsModel
        {
        ImageId = imageId,
        TagId = tagId
        };
      ImageTagsDataAccess.InsertImageTag(imageTag);
      }

    public string GetTagsForImage(int imageId)
      {
      return ImageTagsDataAccess.GetTagsForImage(imageId);
      }
    }
  }
	

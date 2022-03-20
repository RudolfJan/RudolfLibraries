using Screenshots.Library.DataAccess;
using Screenshots.Library.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Library;

namespace Screenshots.Library.Logic
  {
  public class ImageManager
    {
#pragma warning disable CA2211 // Non-constant fields should not be visible
    public static string ThumbnailBasePath = "C:\\Temp\\Thumbnails\\"; //TODO is this correct for production version?
#pragma warning restore CA2211 // Non-constant fields should not be visible

    public static string GetThumbnailPathForCollection(CollectionModel collection)
      {
      return $"{ThumbnailBasePath}CollectionId_{collection.Id}\\";
      }

    public static async Task<List<ImageModel>> LoadNewImagesForAllCollectionsAsync()
      {
      var newImages = new List<ImageModel>();
      var collectionList = CollectionDataAccess.GetAllCollections();
      await Task.Run(async () =>
        {
          foreach (var collection in collectionList)
            {
            var thumbnailCollectionPath = GetThumbnailPathForCollection(collection);
            Directory.CreateDirectory(thumbnailCollectionPath);
            newImages = await LoadNewImagesForCollectionAsync(collection);
            }
        });
      return newImages;
      }

    private static async Task<List<ImageModel>> LoadNewImagesForCollectionAsync(CollectionModel collection)
      {
      var dir = new DirectoryInfo(collection.CollectionPath);
      var newImages1 = await LoadNewImagesForCollectionsByTypeAsync(collection, dir, "jpg");
      var newImages2 = await LoadNewImagesForCollectionsByTypeAsync(collection, dir, "png");
      var newImages3 = await LoadNewImagesForCollectionsByTypeAsync(collection, dir, "jpeg");
      return newImages1.Concat(newImages2).Concat(newImages3).ToList();
      }

    private static async Task<List<ImageModel>> LoadNewImagesForCollectionsByTypeAsync(CollectionModel collection,
      DirectoryInfo dir, string imageType)
      {
      var newImages = new List<ImageModel>();
      var files = dir.GetFiles($"*.{imageType}", SearchOption.TopDirectoryOnly);
      await Task.Run(() =>
        {
          var thumbnailCollectionPath = GetThumbnailPathForCollection(collection);
          Parallel.ForEach<FileInfo>(files, (file) =>
            {
              var thumbnailPath =
              $"{thumbnailCollectionPath}{Path.GetFileNameWithoutExtension(file.Name)}.png";
              if (!File.Exists(thumbnailPath))
                {
                var image = new ImageModel
                  {
                  ImageDescription = string.Empty,
                  ImageThumbnailPath = thumbnailPath,
                  ImagePath = file.Name,
                  CollectionId = collection.Id
                  };
                image.Id = ImageDataAccess.InsertImage(image);
                newImages.Add(image);
                ThumbnailLogic.CreateThumbNail($"{collection.CollectionPath}{file.Name}",
                thumbnailPath);
                }
            });
        });
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
      var thumbnailCollectionPath = GetThumbnailPathForCollection(collection);
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
        var fileList = Directory.GetFiles(collection.CollectionPath).ToList();
        foreach (var image in imageList)
          {
          if (!fileList.Contains($"{collection.CollectionPath}{image.ImagePath}"))
            {
            FileHelpers.DeleteSingleFile(image.ImageThumbnailPath);
            ImageDataAccess.DeleteImage(image.Id);
            }
          }
        }
      }

    public static int AddTag(int imageId, int tagId)
      {
      var imageTag = new ImageTagsModel
        {
        ImageId = imageId,
        TagId = tagId
        };
      return ImageTagsDataAccess.InsertImageTag(imageTag);
      }

    public static string GetTagsForImage(int imageId)
      {
      return ImageTagsDataAccess.GetTagsForImage(imageId);
      }
    }
  }


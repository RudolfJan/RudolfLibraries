using System;
using System.Collections.Generic;
using System.Text;

namespace Screenshots.Library.Models
  {
  public class ImageModel
    {
    public int Id { get; set; }
    public string ImagePath { get; set; }
    public string ImageDescription { get; set; }
    public string ImageThumbnailPath { get; set; }
    public int CollectionId { get; set; }
    }
  }

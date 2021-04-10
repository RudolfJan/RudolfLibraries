using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Library.Filters.Models
  {
 public  class TagCategoriesExtendedModel
    {
    public int TagId { get; set; }
    public string TagName { get; set; }
    public string TagDescription { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public string TagAndCategory
      {
      get
        {
        return $"{CategoryName}-{TagName}";
        }
      }
    }
  }

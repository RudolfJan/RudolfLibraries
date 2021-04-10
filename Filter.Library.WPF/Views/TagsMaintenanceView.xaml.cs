using Filter.Library.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Filter.Library.WPF.Views
  {
 
  public partial class TagsMaintenanceView : UserControl
    {
    public TagAndCategoryViewModel TagAndCategoryData { get; set; }

    public TagsMaintenanceView()
      {
      InitializeComponent();
      SetControlStates();
      }
    private void SetControlStates()
      {
      var categorySelectionCheck = TagAndCategoryData?.SelectedCategory != null;
      EditCategoryButton.IsEnabled = categorySelectionCheck;
      DeleteCategoryButton.IsEnabled = categorySelectionCheck;
      var tagSelectionCheck = TagAndCategoryData?.SelectedTag != null;
      EditTagButton.IsEnabled = tagSelectionCheck;
      DeleteTagButton.IsEnabled = tagSelectionCheck;
      SaveTagButton.IsEnabled = TagAndCategoryData?.TagCategoryName.Length>0;
      }

    private void OnCategoryEditButtonClicked(object sender, RoutedEventArgs e)
      {
      TagAndCategoryData.EditCategory();
      SetControlStates();
      }

    private void OnCategorySaveButtonClicked(object sender, RoutedEventArgs e)
      {
      TagAndCategoryData.SaveCategory();
      CategoryDataGrid.Items.Refresh();
      SetControlStates();
      }

    private void OnCategoryClearButtonClicked(object sender, RoutedEventArgs e)
      {
      TagAndCategoryData.ClearCategory();
      CategoryDataGrid.Items.Refresh();
      SetControlStates();
      }

    private void OnCategoryDeleteButtonClicked(object sender, RoutedEventArgs e)
      {
      TagAndCategoryData.DeleteCategory();
      CategoryDataGrid.Items.Refresh();
      SetControlStates();
      }

    private void OnCategorySelectionChanged(object sender, SelectionChangedEventArgs e)
      {
      
      SetControlStates();
      }

    private void OnTagSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
      SetControlStates();
      }

    private void OnEditTagButtonClicked(object sender, RoutedEventArgs e)
      {
      TagAndCategoryData.EditTag();
      SetControlStates();
      }

    private void OnSaveTagButtonClicked(object sender, RoutedEventArgs e)
      {
      TagAndCategoryData.SaveTag();
      TagDataGrid.Items.Refresh();
      SetControlStates();
      }

    private void OnClearTagButtonClicked(object sender, RoutedEventArgs e)
      {
      TagAndCategoryData.ClearTag();
      TagDataGrid.Items.Refresh();
      SetControlStates();
      }

    private void OnDeleteTagButtonClicked(object sender, RoutedEventArgs e)
      {
      TagAndCategoryData.DeleteTag();
      TagDataGrid.Items.Refresh();
      SetControlStates();
      }

    private void OnTagCategorySelectionChanged(object sender, SelectionChangedEventArgs e)
      {
      SetControlStates();
      }
    }
  }

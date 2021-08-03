using System.Reflection;
using System.Threading.Tasks;
using Caliburn.Micro;
using Utilities.Library.Wpf.Models;

namespace Utilities.Library.Wpf.ViewModels
  {
  public class AboutViewModel: Screen
    {
    public IAboutModel AboutData {get; }

    public AboutViewModel(IAboutModel aboutData)
      {
      AboutData= aboutData;
      }

    public void Initialize(Assembly currentAssembly, string version, string aboutImagePath, string downloadUri)
      {
      AboutData.CurrentAssembly = currentAssembly;
      AboutData.Version = version;
      AboutData.AboutImagePath = aboutImagePath;
      AboutData.DownloadUri = downloadUri;
      }

    public async Task Close()
      {
      await TryCloseAsync();
      }
    }
  }

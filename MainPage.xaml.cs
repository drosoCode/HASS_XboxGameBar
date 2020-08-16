using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HASS_Widget
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Values["apiEndpoint"] != null)
                apiEndpoint.Text = localSettings.Values["apiEndpoint"] as string;

            if (localSettings.Values["apiToken"] != null)
                apiToken.Text = localSettings.Values["apiToken"] as string;

            if (localSettings.Values["apiDevice1"] != null)
                apiDevice1.Text = localSettings.Values["apiDevice1"] as string;

            if (localSettings.Values["apiDevice2"] != null)
                apiDevice2.Text = localSettings.Values["apiDevice2"] as string;

            if (localSettings.Values["apiDevice3"] != null)
                apiDevice3.Text = localSettings.Values["apiDevice3"] as string;

            if (localSettings.Values["apiDevice4"] != null)
                apiDevice4.Text = localSettings.Values["apiDevice4"] as string;

            if (localSettings.Values["apiDevice5"] != null)
                apiDevice5.Text = localSettings.Values["apiDevice5"] as string;


        }
        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["apiEndpoint"] = apiEndpoint.Text;
            localSettings.Values["apiToken"] = apiToken.Text;

            localSettings.Values["apiDevice1"] = apiDevice1.Text;
            localSettings.Values["apiDevice2"] = apiDevice2.Text;
            localSettings.Values["apiDevice3"] = apiDevice3.Text;
            localSettings.Values["apiDevice4"] = apiDevice4.Text;
            localSettings.Values["apiDevice5"] = apiDevice5.Text;

            myButton.Content = "Saved";
        }
    }
}

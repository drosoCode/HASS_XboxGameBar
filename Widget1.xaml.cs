using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.Storage;
using System.Net.Http;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HASS_Widget
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Widget1 : Page
    {
        private String apiEndpoint = "";
        private String apiToken = "";
        private string[] devices = new string[] { "", "", "", "", ""};

        public Widget1()
        {
            this.InitializeComponent();

            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Values["apiEndpoint"] != null)
                apiEndpoint = localSettings.Values["apiEndpoint"] as string;

            if (localSettings.Values["apiToken"] != null)
                apiToken = localSettings.Values["apiToken"] as string;

            if (localSettings.Values["apiDevice1"] != null && localSettings.Values["apiDevice1"] as string != "None")
            {
                devices[0] = localSettings.Values["apiDevice1"] as string;
                string rawData = hassGetRequest("states/" + devices[0]);
                dynamic jsonData = JsonConvert.DeserializeObject(rawData);
                if (jsonData.state == "on")
                    dev_switch_1.IsOn = true;
                else
                    dev_switch_1.IsOn = false;
                dev_txt_1.Text = jsonData.attributes.friendly_name;
                if (rawData.Contains("brightness"))
                {
                    float i = Convert.ToInt16(jsonData.attributes.brightness) / 255 * 100;
                    dev_slider_1.Value = Math.Round(i);
                }
            }

            if (localSettings.Values["apiDevice2"] != null && localSettings.Values["apiDevice2"] as string != "None")
            {
                devices[1] = localSettings.Values["apiDevice2"] as string;
                string rawData = hassGetRequest("states/" + devices[1]);
                dynamic jsonData = JsonConvert.DeserializeObject(rawData);
                if (jsonData.state == "on")
                    dev_switch_2.IsOn = true;
                else
                    dev_switch_2.IsOn = false;
                dev_txt_2.Text = jsonData.attributes.friendly_name;
                if (rawData.Contains("brightness"))
                {
                    float i = Convert.ToInt16(jsonData.attributes.brightness) / 255 * 100;
                    dev_slider_2.Value = Math.Round(i);
                }
            }

            if (localSettings.Values["apiDevice3"] != null && localSettings.Values["apiDevice3"] as string != "None")
            {
                devices[2] = localSettings.Values["apiDevice3"] as string;
                string rawData = hassGetRequest("states/" + devices[2]);
                dynamic jsonData = JsonConvert.DeserializeObject(rawData);
                if (jsonData.state == "on")
                    dev_switch_3.IsOn = true;
                else
                    dev_switch_3.IsOn = false;
                dev_txt_3.Text = jsonData.attributes.friendly_name;
                if (rawData.Contains("brightness"))
                {
                    float i = Convert.ToInt16(jsonData.attributes.brightness) / 255 * 100;
                    dev_slider_3.Value = Math.Round(i);
                }
            }

            if (localSettings.Values["apiDevice4"] != null && localSettings.Values["apiDevice4"] as string != "None")
            {
                devices[3] = localSettings.Values["apiDevice4"] as string;
                string rawData = hassGetRequest("states/" + devices[3]);
                dynamic jsonData = JsonConvert.DeserializeObject(rawData);
                if (jsonData.state == "on")
                    dev_switch_4.IsOn = true;
                else
                    dev_switch_4.IsOn = false;
                dev_txt_4.Text = jsonData.attributes.friendly_name;
                if (rawData.Contains("brightness"))
                {
                    float i = Convert.ToInt16(jsonData.attributes.brightness) / 255 * 100;
                    dev_slider_4.Value = Math.Round(i);
                }
            }

            if (localSettings.Values["apiDevice5"] != null && localSettings.Values["apiDevice5"] as string != "None")
            {
                devices[4] = localSettings.Values["apiDevice5"] as string;
                string rawData = hassGetRequest("states/" + devices[4]);
                dynamic jsonData = JsonConvert.DeserializeObject(rawData);
                if (jsonData.state == "on")
                    dev_switch_5.IsOn = true;
                else
                    dev_switch_5.IsOn = false;
                dev_txt_5.Text = jsonData.attributes.friendly_name;
                if (rawData.Contains("brightness"))
                {
                    float i = Convert.ToInt16(jsonData.attributes.brightness) / 255 * 100;
                    dev_slider_5.Value = Math.Round(i);
                }
            }
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ts = (ToggleSwitch)sender;
            int id = int.Parse(ts.Name.Substring(11))-1;
            String service = devices[id].Substring(0, devices[id].IndexOf("."));
            if (ts.IsOn)
                hassRequest("services/"+service+"/turn_on", "{\"entity_id\":\""+devices[id]+"\"}");
            else
                hassRequest("services/"+service+"/turn_off", "{\"entity_id\":\"" + devices[id] + "\"}");
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider s = (Slider)sender;
            int id = int.Parse(s.Name.Substring(11))-1;
            String service = devices[id].Substring(0, devices[id].IndexOf("."));
            hassRequest("services/" + service + "/turn_on", "{\"entity_id\":\"" + devices[id] + "\", \"brightness\":\""+ Math.Round(s.Value/100*255) + "\"}");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int id = int.Parse(cb.Name.Substring(9))-1;
            String service = devices[id].Substring(0, devices[id].IndexOf("."));
            String value = ((ComboBoxItem)cb.SelectedItem).Content.ToString();
            if (value == "White")
                hassRequest("services/" + service + "/turn_on", "{\"entity_id\":\"" + devices[id] + "\", \"white_value\": 255, \"color_temp\": 288}");
            else
                hassRequest("services/" + service + "/turn_on", "{\"entity_id\":\"" + devices[id] + "\", \"color_name\":\"" + value + "\"}");
        }

        private String hassRequest(String urlAdd, String data)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", apiToken));
            httpClient.DefaultRequestHeaders.Add("Contant-Type", "application/json");
            var request = new HttpRequestMessage(new HttpMethod("POST"), apiEndpoint + "/api/" + urlAdd);
            request.Content = new StringContent(data);
            var response = httpClient.SendAsync(request).Result;
            String resp =  response.Content.ReadAsStringAsync().Result;
            return resp;
        }
        private String hassGetRequest(String urlAdd)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", apiToken));
            httpClient.DefaultRequestHeaders.Add("Contant-Type", "application/json");
            var request = new HttpRequestMessage(new HttpMethod("GET"), apiEndpoint + "/api/" + urlAdd);
            var response = httpClient.SendAsync(request).Result;
            String resp = response.Content.ReadAsStringAsync().Result;
            return resp;
        }
    }
}

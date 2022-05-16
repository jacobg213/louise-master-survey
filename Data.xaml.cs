using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WODIA_Reserach_Guide
{
    /// <summary>
    /// Interaction logic for Data.xaml
    /// </summary>
    public partial class Data : Window
    {
        List<object> DataSource = new List<object>();
        string LatestJSON = "";

        public Data()
        {
            InitializeComponent();
        }

        private void GetDataClick(object sender, RoutedEventArgs e)
        {
			HttpStatusCode status;
			HttpClient client = new HttpClient();

			try
			{
				var couldParse = int.TryParse(Hours.Text, out int hours);
				if (!couldParse)
					hours = 0;

				var Token = "wodia3402u4sijdf02329u4euf0eufsdufoiu394uosdifjoidsfsdfre";
				var ServerURL = @"aalserver.au.dk";
				var ServerPort = @"8088";
				var Citizen = User.Text; //REPLACE WITH THE CITIZEN YOU WANT TO FETCH - YOU NEED TO KNOW THE NAME - NO * 
				var Timetogoback = DateTime.UtcNow.AddHours(-hours).Ticks;

				var url = string.Format("https://{0}:{1}/api/event/wodia?token={2}&citizen={3}&sonographer={4}&site{5}=&timetogoback={6}", ServerURL, ServerPort, Token, Citizen, "", "", Timetogoback);

				HttpResponseMessage response = client.GetAsync(url).Result;

				status = response.StatusCode;

				if (status == HttpStatusCode.OK)
				{

					var result = response.Content.ReadAsStringAsync().Result;
					LatestJSON = result;
					DataSource = JsonConvert.DeserializeObject<List<object>>(result) ?? new List<object> { };
					DataGrid.ItemsSource = DataSource;

					if (DataSource.Count > 0)
					{
						SaveButton.Visibility = Visibility.Visible;
					}
					else
					{
						SaveButton.Visibility = Visibility.Hidden;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex);
			}
		}

		private void SaveJsonClick(object sender, RoutedEventArgs e)
		{
			var name = $"JSON Data - {DateTime.Now.Ticks}.json";
			File.WriteAllText(name, LatestJSON);
		}
	}
}

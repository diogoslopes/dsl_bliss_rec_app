using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Diagnostics;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlissApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //for testing purposes, to force an initial connection failure
        //private bool firstAttempt = true;

        public MainPage()
        {
            this.InitializeComponent();
            
            ShowConnectionFailedResponse(false);
            
            AttemptConnection();

        }

        private async void AttemptConnection() {

            HttpClient httpClient = new HttpClient();

            Uri healthReq = new Uri("https://private-anon-23e1551d40-blissrecruitmentapi.apiary-mock.com/health");


            HttpResponseMessage httpResponse;

            try {
                httpResponse = await httpClient.GetAsync(healthReq);

                if(httpResponse.StatusCode == HttpStatusCode.Ok /*&& ! firstAttempt*/) {
                    Debug.WriteLine("Health OK");
                    ShowConnectionFailedResponse(false);
                    Frame.Navigate(typeof(QuestionsScreen));

                }
                else {
                    ShowConnectionFailedResponse(true);
                }

                //if (firstAttempt) {
                //    firstAttempt = !firstAttempt;
                //}

            }
            catch (Exception e) {
                Debug.WriteLine("Error: " + e.ToString());
            }

        }

        private void retryButton_Click(object sender, RoutedEventArgs e) {

            AttemptConnection();
            
        }


        private void ShowConnectionFailedResponse(bool failed) {
            if( failed ) {
                textMsg.Visibility = Visibility.Visible;
                retryButton.Visibility = Visibility.Visible;
            }
            else {
                textMsg.Visibility = Visibility.Collapsed;
                retryButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BlissApp {

    public sealed partial class ShareScreen : Page {

        private string url { get; set; }

        public ShareScreen() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            url = e.Parameter as string;

        }

        private async void button_Click(object sender, RoutedEventArgs e) {

            System.Diagnostics.Debug.WriteLine("Content: " + emailBox);

            if(emailBox.Text == "") {
                MessageDialog m = new MessageDialog("Please insert a valid email address");
                await m.ShowAsync();
                return;
            }

            if (url != "") {

                HttpClient httpClient = new HttpClient();

                Uri shareUri = new Uri("https://private-anon-8cea0feb9b-blissrecruitmentapi.apiary-mock.com/share?" + emailBox.Text + "&" + url);

                try {

                    HttpResponseMessage httpResponse = await httpClient.PostAsync(shareUri, new HttpStringContent(""));

                    MessageDialog m = null;

                    if (httpResponse.StatusCode == HttpStatusCode.Ok) {
                        m = new MessageDialog("That page was shared with the provided email!");
                        Frame.GoBack();
                    }

                    if (httpResponse.StatusCode == HttpStatusCode.BadRequest) {
                        m = new MessageDialog("Sorry, something went wrong... Please check if the email you provided is a valid email address.");
                    }

                    if (m != null) {
                        await m.ShowAsync();
                    }

                }
                catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine("Error: " + ex.ToString());
                }

            }

        }

        private void backButton_Click(object sender, RoutedEventArgs e) {
            Frame.GoBack();
        }
    }
}

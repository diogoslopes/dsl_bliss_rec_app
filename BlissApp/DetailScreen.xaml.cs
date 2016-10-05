using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
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
    
    public sealed partial class DetailScreen : Page {

        private Question detailedQuestion;

        public string Title {
            get;
            set;
        }

        public ImageSource QuestionImage { get; set; }

        public DetailScreen() {

            this.InitializeComponent();

        }


        protected override void OnNavigatedTo(NavigationEventArgs e) {
            detailedQuestion = e.Parameter as Question;

            if(detailedQuestion != null) {
                Title = detailedQuestion.QuestionText;
                QuestionImage = detailedQuestion.QuestionImage;
                choiceList.ItemsSource = detailedQuestion.choices.ToList<Choice>();

            }


        }

        private void backButton_Click(object sender, RoutedEventArgs e) {
            Frame.GoBack();
        }

        private void choiceList_ItemClick(object sender, ItemClickEventArgs e) {

            CastVote(e.ClickedItem);

        }

        private async void CastVote(object clickedItem) {

            int clickedItemIndex = choiceList.Items.IndexOf(clickedItem);

            HttpClient httpClient = new HttpClient();

            JsonObject jsonStr = Utils.Question2Json(detailedQuestion, clickedItemIndex);

            HttpStringContent httpContent = new HttpStringContent(
                jsonStr.Stringify(),
                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                "application/json"
            );

            Uri putTo = new Uri("https://private-anon-8cea0feb9b-blissrecruitmentapi.apiary-mock.com/questions/" + detailedQuestion.id);

            try {

                HttpResponseMessage httpResponse = await httpClient.PutAsync(putTo, httpContent);

                MessageDialog m = null;

                if (httpResponse.StatusCode == HttpStatusCode.Created) {
                    m = new MessageDialog("Thank you for voting!");
                }

                if (httpResponse.StatusCode == HttpStatusCode.BadRequest) {
                    m = new MessageDialog("Sorry, something went wrong... Your vote wasn't accepted");
                }

                if(m != null) {
                    await m.ShowAsync();
                }

            }
            catch (Exception e) {
                System.Diagnostics.Debug.WriteLine("Error: " + e.ToString());
            }
           
            
        }

        private void shareButton_Click(object sender, RoutedEventArgs e) {
            string currUrl = "https://private-anon-8cea0feb9b-blissrecruitmentapi.apiary-mock.com/questions/" + detailedQuestion.id;

            Frame.Navigate(typeof(ShareScreen), currUrl);
        }
        
    }
}

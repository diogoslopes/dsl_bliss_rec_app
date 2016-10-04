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

using Windows.Web.Http;
using Windows.Data.Json;
using System.Diagnostics;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BlissApp {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 


    

    public sealed partial class QuestionsScreen : Page {

        private int QUESTION_LIMIT = 10;

        private int currentListOffset = 0;

        public QuestionsScreen() {
            this.InitializeComponent();

            GetQuestions(QUESTION_LIMIT, currentListOffset);

        }

        private async void GetQuestions(int limit, int offset, string filter = "") {

            HttpClient httpClient = new HttpClient();

            string listRequestStr = "https://private-anon-e071c9acfd-blissrecruitmentapi.apiary-mock.com/questions?" + limit + "&" + offset;

            if (filter != "") {
                listRequestStr += "&" + filter;
            }

            Uri healthReq = new Uri(listRequestStr);


            HttpResponseMessage httpResponse;

            try {
                httpResponse = await httpClient.GetAsync(healthReq);

                string jsonStr = await httpResponse.Content.ReadAsStringAsync();

                JsonArray jsonArray = JsonValue.Parse(jsonStr).GetArray();


                List<Question> questionListSource = new List<Question>();

                for(uint i = 0; i < jsonArray.Count; i++) {


                    uint id = (uint)jsonArray.GetObjectAt(i).GetNamedNumber("id");
                    string question = jsonArray.GetObjectAt(i).GetNamedString("question");

                    Debug.WriteLine(id);

                    questionListSource.Add(new Question(id, question));

                }

                questionList.ItemsSource = questionListSource;

            }
            catch (Exception e) {
                Debug.WriteLine("Error: " + e.ToString());
            }

        }
    }
}

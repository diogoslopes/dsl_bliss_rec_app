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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BlissApp {
    
    public sealed partial class DetailScreen : Page {

        private Question current;

        public string Title {
            get;
            set;
        }

        public ImageSource QuestionImage { get; set; }

        public DetailScreen() {

            this.InitializeComponent();

        }


        protected override void OnNavigatedTo(NavigationEventArgs e) {
            current = e.Parameter as Question;

            if(current != null) {
                Title = current.QuestionText;
                QuestionImage = current.QuestionImage;
                choiceList.ItemsSource = current.choices.ToList<Choice>();

            }


        }

        private void backButton_Click(object sender, RoutedEventArgs e) {
            Frame.GoBack();
        }
    }
}

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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BlissApp {
    public sealed partial class Question : UserControl {

        public uint id { get; set; }

        public string QuestionText { get; set; }

        public string img_url { get; set; }

        public string thumb_url { get; set; }

        public string date { get; set; }

        public ImageSource ThumbImage { get; set; }

        public ImageSource QuestionImage { get; set; }

        public Choice[] choices { get; set; }


        public Question() {
            this.InitializeComponent();
        }

        public Question(uint id, string text, string img, string thumb, string date, Choice[]choices) : this(){

            this.id = id;

            QuestionText = text;
            img_url = img;
            thumb_url = thumb;
            this.date = date;

            QuestionImage = new BitmapImage(new Uri(img));
            ThumbImage = new BitmapImage(new Uri(thumb));

            this.choices = choices;
        }
        
    }
    
}


using System.Collections.Generic;
using Windows.Data.Json;

namespace BlissApp {

    public class Utils {

        public static Question Json2Question(JsonObject jsonObj) {

            uint id = (uint)jsonObj.GetNamedNumber("id");
            string question = jsonObj.GetNamedString("question");
            string image = jsonObj.GetNamedString("image_url");
            string thumb = jsonObj.GetNamedString("thumb_url");
            string datePublished = jsonObj.GetNamedString("published_at");

            JsonArray jsonChoicesStr = jsonObj.GetNamedArray("choices");
            List<Choice> choices = new List<Choice>();

            for (uint j = 0; j < jsonChoicesStr.Count; j++) {

                Choice c = new Choice(
                    jsonChoicesStr.GetObjectAt(j).GetNamedString("choice"),
                    (uint)jsonChoicesStr.GetObjectAt(j).GetNamedNumber("votes")
                );

                choices.Add(c);

            }

            return new Question(
                id,
                question,
                image,
                thumb,
                datePublished,
                choices.ToArray()
            );
        }

    }


}

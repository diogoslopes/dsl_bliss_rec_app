
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


        public static JsonObject Question2Json(Question q, int votedOption = -1) {

            JsonObject jsonObj = new JsonObject();

            jsonObj.SetNamedValue("id",            JsonValue.CreateNumberValue(q.id));
            jsonObj.SetNamedValue("image_url",     JsonValue.CreateStringValue(q.img_url));
            jsonObj.SetNamedValue("thumb_url",     JsonValue.CreateStringValue(q.thumb_url));
            jsonObj.SetNamedValue("question",      JsonValue.CreateStringValue(q.QuestionText));
            jsonObj.SetNamedValue("published_at",  JsonValue.CreateStringValue(q.date));

            JsonArray choices = new JsonArray();

            for (uint i = 0; i < q.choices.Length; i++) {

                JsonObject choice = new JsonObject();

                choice.SetNamedValue("choice", JsonValue.CreateStringValue(q.choices[i].ChoiceStr));

                if(votedOption == -1) {
                    choice.SetNamedValue("votes", JsonValue.CreateNumberValue(q.choices[i].Votes));
                }
                else {
                    if(votedOption == i) {
                        choice.SetNamedValue("votes", JsonValue.CreateNumberValue(1));
                    }
                    else {
                        choice.SetNamedValue("votes", JsonValue.CreateNumberValue(0));
                    }

                }
                
                choices.Add(choice);
            }

            jsonObj.SetNamedValue("choices", choices);

            return jsonObj;

            
        }

    }


}

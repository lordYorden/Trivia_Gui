﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TriviaGUI
{
    public static class Serializer
    {

        public static string serializeResponse(int code, JObject data)
        {
            string toReturn = "";
            string json = "";
            json = JsonConvert.SerializeObject(data);
            toReturn = helperString(code, json);
            Console.WriteLine(toReturn);
            return toReturn;
        }
        public static string helperString(int code, string str)
        {
            string toReturn = code.ToString();
            if(toReturn.Length == 1)
            {
                toReturn = "0" + toReturn;
            }
            int size = str.Length;
            string sizeStr = fillZeros(size.ToString());
            toReturn += sizeStr;
            toReturn += str;
            return toReturn;
        }
        public static string fillZeros(string toFill)
        {
            string filled = "";
            for (int i = 0; i < 4 - toFill.Length; i++)
            {
                filled += 0;
            }
            return filled + toFill;
        }
    }

    public static class Deserializer
    {
        public static QuestionData desirializeQuestion(JObject json) 
        {
            string question = json["question"].ToString();
            List<string> otherAnswers = json["otherAnswers"].ToString().Split('&').ToList();
            string correctAnswer = json["correctAnswer"].ToString();
            int status = Convert.ToInt32(json["status"].ToString());

            return new QuestionData(question, correctAnswer, otherAnswers);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace VCoinWeb
{

//    To send this kind of notification we simply need to replace data part of our message json (see string variable postData) with new notification json which should be like below:

//{
//    "to" : "yourclientregistrationid...",
//    "notification" : {
//      "body" : "notification body",
//      "title" : "notification title",
//      "icon" : "displayicon"
//    }
//}
    public class FirebaseNotificationManager
    {
        private class NotificationMessage
        {
            public string Title;
            public string Message;
            public long ItemId;
        }

        public FirebaseNotificationManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string SendNotification(List<string> deviceRegIds, string message, string title, long id)
        {

            string SERVER_API_KEY = "";
            var SENDER_ID = "";
            string regIds = string.Join("\",\"", deviceRegIds);

            NotificationMessage nm = new NotificationMessage();
            nm.Title = title;
            nm.Message = message;
            nm.ItemId = id;

            var value = new JavaScriptSerializer().Serialize(nm);

            WebRequest tRequest;
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            string postData = "{\"collapse_key\":\"score_update\",\"time_to_live\":108,\"delay_while_idle\":true,\"data\": { \"message\" : " + value + ",\"time\": " + "\"" + System.DateTime.Now.ToString() + "\"},\"registration_ids\":[\"" + regIds + "\"]}";

            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;
        }
    }
}
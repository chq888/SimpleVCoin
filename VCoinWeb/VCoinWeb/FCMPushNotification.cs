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

    public class FCMPushNotification
    {

        public FCMPushNotification()
        {
            // TODO: Add constructor logic here
        }

        public string Response
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }

        public string RegistrationId;
        public string ServerKey = "AAAAtCPm3kM:APA91bH9X5D_Wu6uoXfRp7vbR8GKC5HCW58_QlXxn3UCTe_KjF_s9jeI7Xu9UzXCnUWrGUdPl9FZ76aSiID_7yjvy8aGaAjCk0o7LJL5UAYpyHskb6PDGLTUNGxYGW_ScBgT8VaGimhW";
        public string SenderId = "773696446019";

        //public FCMPushNotification SendNotification(string _title, string _message, string _topic)
        //{
        //    FCMPushNotification result = new FCMPushNotification();

        //    try
        //    {
        //        result.Error = null;
        //        // var value = message;
        //        var requestUri = "https://fcm.googleapis.com/fcm/send";

        //        WebRequest webRequest = WebRequest.Create(requestUri);
        //        webRequest.Method = "POST";
        //        webRequest.Headers.Add(string.Format("Authorization: key={0}", ServerKey));
        //        webRequest.Headers.Add(string.Format("Sender: id={0}", SenderId));
        //        webRequest.ContentType = "application/json";

        //        var data = new
        //        {
        //            // to = YOUR_FCM_DEVICE_ID, // Uncoment this if you want to test for single device
        //            to = "/topics/" + _topic, // this is for topic 
        //            notification = new
        //            {
        //                title = _title,
        //                body = _message,
        //                //icon="myicon"
        //            }
        //        };

        //        var serializer = new JavaScriptSerializer();
        //        var json = serializer.Serialize(data);

        //        Byte[] byteArray = Encoding.UTF8.GetBytes(json);

        //        webRequest.ContentLength = byteArray.Length;
        //        using (Stream dataStream = webRequest.GetRequestStream())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);

        //            using (WebResponse webResponse = webRequest.GetResponse())
        //            {
        //                using (Stream dataStreamResponse = webResponse.GetResponseStream())
        //                {
        //                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        String sResponseFromServer = tReader.ReadToEnd();
        //                        result.Response = sResponseFromServer;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Response = null;
        //        result.Error = ex;
        //    }

        //    return result;
        //}


        public FCMPushNotification SendNotification(string subject, string message, long itemId, string topic = "LatestRate")
        {
            FCMPushNotification result = new FCMPushNotification();

            try
            {
                string deviceId = "";

                WebRequest request = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                request.Method = "post";
                request.ContentType = "application/json";
                request.Headers.Add(string.Format("Authorization: key={0}", ServerKey));
                request.Headers.Add(string.Format("Sender: id={0}", SenderId));
                // httpWebRequest.Headers.Add("Your_Server_Key");

                var data = new
                {
                    //"condition": " 'Symulti' in topics || 'SymultiLite' in topics",
                    //"priority" : "normal",
                    to = string.IsNullOrEmpty(deviceId) ? $"/topics/{topic}" : deviceId,
                    priority = "high",
                    notification = new
                    {
                        title = subject,
                        body = message,
                        icon = "ic_notification",
                        //tag = itemId,
                        //sound = "Enabled"
                        //click_action : ".MainActivity"
                    },
                    data = new
                    {
                        title = subject,
                        body = message,
                        //tag = itemId
                        //customData = "customData"
                    }
                };

//Body using topics:

//{
//"to": "/topics/my_topic",
//"data": {
//"my_custom_key" : "my_custom_value",
//"other_key" : true
//}
//}

//Or if you want to send it to specific devices:

//{
//"data": {
//"my_custom_key" : "my_custom_value",
//"other_key" : true
//},
//"registration_ids": ["{device-token}","{device2-token}","{device3-token}"]
//}


    //           Body    "1 usd = 200,000 vnd"   string
    //   BodyLocalizationKey(null)  object
    //   ClickAction(null)  object
    //   Color(null)  object
    //Icon    "myicon"    string
    //   Link(null)  object
    //   Sound(null)  object
    //   Tag(null)  object
    //  Title   "www.tuoitre.com"   string
    //   TitleLocalizationKey(null)  object


                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                request.ContentLength = byteArray.Length;

                using (Stream dataStream = request.GetRequestStream())
                {
                    //string json = "{\"to\": \"Your device token\",\"data\": {\"message\": \"This is a Firebase Cloud Messaging Topic Message!\",}}";
                    //string json = "{\"to\": \"/topics/news\",\"notification\": {\"body\": \"New news added in application!\",\"title\":\"" + Your_Notif_Title + "\",}}";
                    //string json = "{\"collapse_key\":\"score_update\",\"time_to_live\":108,\"delay_while_idle\":true,\"data\": { \"message\" : " + value + ",\"time\": " + "\"" + System.DateTime.Now.ToString() + "\"},\"registration_ids\":[\"" + regIds + "\"]}";
                    //streamWriter.Write(json);
                    //streamWriter.Flush();

                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse response = request.GetResponse())
                    {
                        //if (response.StatusCode == HttpStatusCode.Accepted || respuesta.StatusCode == HttpStatusCode.OK || respuesta.StatusCode == HttpStatusCode.Created)
                        //{
                        //}
                        //else
                        //{
                        //    throw new Exception("Ocurrio un error al obtener la respuesta del servidor: " + respuesta.StatusCode);
                        //}
                        using (Stream dataStreamResponse = response.GetResponseStream())
                        {
                            using (StreamReader streamReader = new StreamReader(dataStreamResponse))
                            {
                                string responseFromServer = streamReader.ReadToEnd();
                                result.Response = responseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Response = null;
                result.Error = ex;
            }

            return result;
        }

    }

}
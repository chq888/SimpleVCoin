using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace VCoinWeb
{

//    1) Create a Object "FireBasePush" with your token proyect(from firebase proyect):

//FireBasePush push = new FireBasePush("token_here");

//2) Call the method SendPush:

//push.SendPush(new PushMessage
//{
//	to = "token_target", //for a topic to": "/topics/foo-bar"
//	notification = new PushMessageData
//	{
//		title = "title push",
//		text = "body text",
//		click_action = click_action
//},
//	data = new
//	{
//		example = "ey this is a example" 
//	}
//});

//3) data is optional and only send "extra data" in push:


    public class FireBasePush
    {
        private string FireBase_URL = "https://fcm.googleapis.com/fcm/send";
        private string key_server;

        public FireBasePush(String Key_Server)
        {
            this.key_server = Key_Server;
        }

        public dynamic SendPush(PushMessage message)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FireBase_URL);
            request.Method = "POST";
            request.Headers.Add("Authorization", "key=" + this.key_server);
            request.ContentType = "application/json";
            string json = JsonConvert.SerializeObject(message);
            //json = json.Replace("content_available", "content-available");
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                StreamReader read = new StreamReader(response.GetResponseStream());
                String result = read.ReadToEnd();
                read.Close();
                response.Close();
                dynamic stuff = JsonConvert.DeserializeObject(result);

                return stuff;
            }
            else
            {
                throw new Exception("SendPush: " + response.StatusCode);
            }
        }


    }
    public class PushMessage
    {
        private string _to;
        private PushMessageData _notification;

        private dynamic _data;
        private dynamic _click_action;
        public dynamic data
        {
            get { return _data; }
            set { _data = value; }
        }

        public string to
        {
            get { return _to; }
            set { _to = value; }
        }
        public PushMessageData notification
        {
            get { return _notification; }
            set { _notification = value; }
        }

        public dynamic click_action
        {
            get
            {
                return _click_action;
            }

            set
            {
                _click_action = value;
            }
        }
    }

    public class PushMessageData
    {
        private string _title;
        private string _text;
        private string _sound = "default";
        //private dynamic _content_available;
        private string _click_action;
        public string sound
        {
            get { return _sound; }
            set { _sound = value; }
        }

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string click_action
        {
            get
            {
                return _click_action;
            }

            set
            {
                _click_action = value;
            }
        }

        //public dynamic content_available
        //{
        //    get
        //    {
        //        return _content_available;
        //    }

        //    set
        //    {
        //        _content_available = value;
        //    }
        //}
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WebSocketSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System;
using System.Linq;
using System.Threading;

namespace RosUnity
{
   

    public class RosConnector : MonoBehaviour
    {

        public string RosBridgeIP = "129.94.233.171";
        public string RosBridgePort = "9090";
        private WebSocket webSocket;
        private Dictionary<string, SubStruct> subscribers = new Dictionary<string, SubStruct>();
        public delegate void MessageHandler(Message message);

        void Awake()
        {
            Debug.Log("Connecting to ROS bridge server");

            //var timeOut = new CancellationToken(500).Token;

            webSocket = new WebSocket("ws://" + RosBridgeIP + ":" + RosBridgePort);

            webSocket.OnMessage += (sender, e) => receivedOperation((WebSocket)sender, e);


            webSocket.ConnectAsync();

            while (true)
            {
                if (webSocket.IsAlive == true)
                {

                    break;
                }

            }

  
        }

        public string Subscribe(string topic, string rosMessageType, MessageHandler messageHandler, int throttle_rate = 0, int queue_length = 1, int fragment_size = int.MaxValue, string compression = "none")
        {
            Type messageType = MessageTypes.MessageType(rosMessageType);
            if (messageType == null)
                return null;

            string id = generateId();
            subscribers.Add(id, new SubStruct(topic, messageType, messageHandler));
            sendOperation(new Subscription(id, topic, rosMessageType, throttle_rate, queue_length, fragment_size, compression));
            return id;
        }

        private static string generateId()
        {
            return Guid.NewGuid().GetHashCode().ToString();
        }

        public void Disconnect()
        {
            webSocket.Close();
        }

        private void OnApplicationQuit()
        {
            webSocket.Close();
        }

        private void receivedOperation(object sender, WebSocketSharp.MessageEventArgs e)
        {
            JObject operation = JsonConvert.DeserializeObject<JObject>(e.Data);



            switch (operation.GetOperation())
            {
                case "publish":
                    {
                        receivedPublish(operation, e.RawData);
                        return;
                    }
                case "service_response":
                    {
                        //receivedServiceResponse(operation, e.RawData);
                        return;
                    }
                case "call_service":
                    {
                        //receivedServiceCall(operation, e.RawData);
                        return;
                    }
            }

            //Debug.Log(JsonConvert.SerializeObject(operation, Formatting.Indented));

        }

        private void receivedPublish(JObject publication, byte[] rawData)
        {
            SubStruct subscriber;
          

            bool foundById = subscribers.TryGetValue(publication.GetServiceId(), out subscriber);
            if (!foundById)
             subscriber = subscribers.Values.FirstOrDefault(x => x.topic.Equals(publication.GetTopic()));
 

            subscriber.messageHandler.Invoke((Message)publication.GetMessage().ToObject(subscriber.messageType));
            
        }

        private void sendOperation(Operation operation)
        {

            webSocket.SendAsync(Serialize(operation), null);
        }

        public static byte[] Serialize(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            byte[] buffer = Encoding.ASCII.GetBytes(json);
            int I = json.Length;
            int J = buffer.Length;
            return buffer;
        }


        public static JObject Deserialize(byte[] buffer)
        {
            string ascii = Encoding.ASCII.GetString(buffer, 0, buffer.Length);

            int I = ascii.Length;
            int J = buffer.Length;
            return JsonConvert.DeserializeObject<JObject>(ascii);
        }

        internal struct SubStruct
        {
            internal string topic;
            internal Type messageType;
            internal MessageHandler messageHandler;
            internal SubStruct(string Topic, Type MessageType, MessageHandler MessageHandler)
            {
                topic = Topic;
                messageType = MessageType;
                messageHandler = MessageHandler;
            }
        }

    }
}
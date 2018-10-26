using System.Collections;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;
using Unity3dAzure.WebSockets;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System;
using System.Linq;
using Vuforia;

namespace ROSHololens
{
    public class RosConnector : UnityWebSocket
    {

        public string webSocketIP = "129.94.233.176";
        public string webSocketPort = "9090";

        private Dictionary<string, Subscriber> subscribers = new Dictionary<string, Subscriber>();
        private Dictionary<string, Publisher> publishers = new Dictionary<string, Publisher>();

        private bool socketIsOpen = false;


        // Use this for initialization
        void Start()
        {
            
            WebSocketUri = "ws://" + webSocketIP + ":" + webSocketPort;
            Connect();

        }

          
        protected override void OnWebSocketOpen(object sender, EventArgs e)
        {
           
            foreach (Subscriber subscriber in subscribers.Values)
            {
                
                sendOperation(new Subscription(subscriber.getTopic(), MessageTypes.RosMessageType(subscriber.getType())));
            }

            foreach (Publisher p in publishers.Values)
            {
                sendOperation(new Adverisement(p.getId(), p.getTopic(), MessageTypes.RosMessageType(p.getType())));
            }
            TextToSpeechManager.Instance.websocketConnected();
            socketIsOpen = true;


        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Subscribe(Subscriber subscriber)
        {
            subscribers.Add(subscriber.getTopic(), subscriber);
        }

        private void sendOperation(Operation operation)
        {
            SendText(JsonConvert.SerializeObject(operation));
         
        }

        protected override void OnWebSocketMessage(object sender, WebSocketMessageEventArgs e)
        {
            String json = e.Data;

            if (json.Contains("/scan"))
            {
                json = json.Replace("null", "\"Infinity\"");
            }
            
            JObject operation = JsonConvert.DeserializeObject<JObject>(json);
            //Debug.Log(json);
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
        }

        private void receivedPublish(JObject publication, byte[] rawData)
        {
            if (subscribers.ContainsKey(publication.GetTopic()))
            {
                //Debug.Log((publication.GetTopic()));
                Subscriber s = subscribers[publication.GetTopic()];
                s.receiveMessage((Message)publication.GetMessage().ToObject(s.getType()));
            } else
            {
                Debug.Log("no suscriber found!!!!");
            }
            
            //
        }



        void OnDisable()
        {
            Close();
        }

        public string Advertise(Publisher p)
        {
            string id = generateId();
            publishers.Add(id, p);
            return id;
        }

        public void Publish(Publisher p, Message m)
        {
            if (socketIsOpen)
            {
                sendOperation(new Publication(p.getId(), p.getTopic(), m));
            }
        }

        private static string generateId()
        {
            return Guid.NewGuid().GetHashCode().ToString();
        }

        protected override void OnWebSocketClose(object sender, WebSocketCloseEventArgs e)
        {
            TextToSpeechManager.Instance.websocketDisconnected();
            if (!e.WasClean)
            {
                DisconnectWebSocket();
            }
            DettachHandlers();
        }


        protected override void OnWebSocketError(object sender, WebSocketErrorEventArgs e)
        {
            TextToSpeechManager.Instance.websocketError();
            Debug.LogError("Web socket error: " + e.Message);
            DisconnectWebSocket();
        }


    }
}
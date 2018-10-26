using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using UnityEngine;


namespace ROSHololens
{
    public class Subscriber : MonoBehaviour
    {
        public string messageType = "geometry_msgs/Pose";
        public string topic = "/topi";

        public MessageReceiver r1;
        public MessageReceiver r2;
        public MessageReceiver r3;

        private void Awake()
        {
            GetComponent<RosConnector>().Subscribe(this);
        }

        public Type getType()
        {
            return MessageTypes.MessageType(messageType);
        }

        public string getTopic()
        {
            return topic;
        }

        public void receiveMessage(Message m)
        {
            if (r1 != null)
            {
                r1.receiveMessage(m);
            }
            if (r2 != null)
            {
                r2.receiveMessage(m);
            }
            if (r3 != null)
            {
                r3.receiveMessage(m);
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ROSHololens
{
    public abstract class Publisher : MonoBehaviour
    {

        public abstract Type MessageType { get; }
        public string topic = "/topi";
        private string id;


        private void Awake()
        {
            id = GetComponent<RosConnector>().Advertise(this);
        }

        public Type getType()
        {
            return MessageType;
        }

        public string getId()
        {
            return id;
        }

        public string getTopic()
        {
            return topic;
        }

        public abstract void publishMessage();

    }
}

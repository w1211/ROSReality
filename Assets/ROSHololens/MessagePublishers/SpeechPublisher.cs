/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using UnityEngine;

namespace ROSHololens
{
    public class SpeechPublisher : Publisher
    {
        private StandardString message;
        public override Type MessageType { get { return (typeof(StandardString)); } }

        public override void publishMessage()
        {
            GetComponent<RosConnector>().Publish(this, message);

        }

        void Update()
        {
            try
            {
                message.data = Global.stopMessage.ToString() + ":" + Global.goHome.ToString() + ":" + Global.wallFollow.ToString();
                // Debug.Log("Publisihed: "+message.data);
                publishMessage();
            }
            catch (Exception e)
            {
                Debug.Log("Message not Requested:"+e);
            }

        }

        private void Start()
        {
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new StandardString();
        }
    }
}

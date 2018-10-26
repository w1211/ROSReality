using HoloToolkit.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ROSHololens
{
    class TextToSpeechManager : MonoBehaviour
    {
        private TextToSpeech textToSpeech;
        private AudioSource audioSource;

        public AudioClip websocketDisconnectedSound;
        public AudioClip websocketConnectedSound;
        public AudioClip websocketErrorSound;
        public AudioClip beaconSound;

        private AudioClip soundToPlay;
        private bool mustPlay = false;

        private static TextToSpeechManager _instance;

        public static TextToSpeechManager Instance { get { return _instance; } }

        void Start()
        {
            textToSpeech = GetComponent<TextToSpeech>();
            audioSource = GetComponent<AudioSource>();

        }

        private void Update()
        {
            if (mustPlay)
            {
                audioSource.PlayClip(soundToPlay);
                mustPlay = false;
            }
        }

        private void Awake()
        {
            
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public void websocketConnected()
        {
            Debug.Log("conencted sounnd");
            soundToPlay = websocketConnectedSound;
            mustPlay = true;
        }

        public void websocketDisconnected()
        {

            soundToPlay = websocketDisconnectedSound;
            mustPlay = true;
        }

        public void websocketError()
        {
            soundToPlay = websocketErrorSound;
            mustPlay = true;
        }

        public void beaconDetected()
        {
            soundToPlay = beaconSound;
            mustPlay = true;
        }

        public void speakMessage(string message)
        {
            //var builtMessage = String.Format(message, textToSpeech.Voice.ToString());
            textToSpeech.StartSpeaking(message);
        }
    }
}

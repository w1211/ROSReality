using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.EventSystems;
using HoloToolkit.Unity.InputModule;
using HoloToolkit;


namespace HoloToolkit
{
    public class PopupManager : MonoBehaviour, IInputClickHandler
    {

        public GameObject popup;
        private static GameObject cursor;

        void Start()
        {
            cursor = GameObject.Find("DefaultCursor");
            InputManager.Instance.PushFallbackInputHandler(this.gameObject);
            popup.SetActive(false);

        }

        private void Tap(InteractionSourceKind source, int tapCount, Ray headRay)
        {

            popup.SetActive(!popup.activeInHierarchy);

        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            Debug.Log(eventData.ToString());
            this.transform.position = cursor.transform.position;
            this.transform.rotation = new Quaternion(0, Camera.main.gameObject.transform.rotation.y, 0, 0) * Quaternion.Euler(0, 180f, 0); ;
            popup.SetActive(!popup.activeInHierarchy);



        }


    }
}

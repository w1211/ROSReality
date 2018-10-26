using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Global
{
    public static int stopMessage = 0;
    public static int wallFollow = 0;
    public static int goHome = 0;
}
 
public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        keywords.Add("Run wall follow", () =>
        {
            Global.wallFollow += 1;
            print("Wallf: "+Global.wallFollow+" Gohome: "+Global.goHome+" Stop: "+Global.stopMessage);
            Debug.Log("Wallf: " + Global.wallFollow + " Gohome: " + Global.goHome + " Stop: " + Global.stopMessage);
            // Call the OnReset method on every descendant object.
            //this.BroadcastMessage("OnReset");
        });

        keywords.Add("Navigate home", () =>
        {
            Global.goHome += 1;
            print("Wallf: " + Global.wallFollow + " Gohome: " + Global.goHome + " Stop: " + Global.stopMessage);
            Debug.Log("Wallf: " + Global.wallFollow + " Gohome: " + Global.goHome + " Stop: " + Global.stopMessage);
            //var focusObject = GazeGestureManager.Instance.FocusedObject;
            //if (focusObject != null)
            //{
            //    // Call the OnDrop method on just the focused object.
            //    focusObject.SendMessage("OnDrop", SendMessageOptions.DontRequireReceiver);
            //}
        });

        keywords.Add("Stop movement", () =>
        {
            Global.stopMessage += 1;
            print("Wallf: " + Global.wallFollow + " Gohome: " + Global.goHome + " Stop: " + Global.stopMessage);
            Debug.Log("Wallf: " + Global.wallFollow + " Gohome: " + Global.goHome + " Stop: " + Global.stopMessage);
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
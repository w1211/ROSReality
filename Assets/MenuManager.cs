using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class MenuManager : MonoBehaviour, IInputClickHandler, IInputHandler
{
    public GameObject menuToToggle;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        menuToToggle.SetActive(!menuToToggle.activeInHierarchy);
    }
    public void OnInputDown(InputEventData eventData)
    {

    }
    public void OnInputUp(InputEventData eventData)
    { }
}
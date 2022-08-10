using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    private Text textTutorial;
    private bool pressKey = false;

    private void Start()
    {
        textTutorial = GetComponent<Text>();
        textTutorial.text = "Hold Space key to Rocket fly";
    }

    private void OnGUI()
    {
        ShowText();
    }

    private void ShowText()
    {
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Space.ToString())) && pressKey == false)
        {
            textTutorial.text = "Press A or D to rocket rotate left or right";
            pressKey = true;
        }
        if (CheckPressKeyRotation() && pressKey == true)
        {
            textTutorial.text = "Then go to Plane Green";
        }
    }

    private bool CheckPressKeyRotation()
    {
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.D.ToString()))
        || Event.current.Equals(Event.KeyboardEvent(KeyCode.A.ToString())))
        {
            return true;
        }
        return false;
    }

}

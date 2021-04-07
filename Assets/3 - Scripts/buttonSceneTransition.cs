using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonSceneTransition : MonoBehaviour
{
    public Text text;

    public void Next()
    {
        if (text.text == "START")
            SceneManager.LoadScene("Recycling Warehouse");
        if (text.text == "RESTART")
            SceneManager.LoadScene("Recycling Warehouse");
        if (text.text == "RETURN TO MENU")
            SceneManager.LoadScene("MainMenu");
        if (text.text == "TUTORIAL")
            SceneManager.LoadScene("Tutorial");
    }
}

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
    }
}

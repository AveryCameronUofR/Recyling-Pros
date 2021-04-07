using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideGreenBtnOnStart : MonoBehaviour
{
    public void HideGreenBtn()
    {
        gameObject.SetActive(false);
    }
}

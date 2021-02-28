using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public Text scoreDisplay;

    private int score = 0;

    void Start()
    {
        if (gm == null)
            gm = this.gameObject.GetComponent<GameManager>();

        scoreDisplay.text = "Score: " + score;
    }

    void Update()
    {
        scoreDisplay.text = "Score: " + score;
    }

    public void itemMissed()
    {
        score -= 8;
    }

    public void itemBinned()
    {
        score += 2;
    }
}

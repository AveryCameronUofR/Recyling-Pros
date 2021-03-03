using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Public Members
    public static GameManager gm;
    public Text scoreDisplay;

    // Private Members
    private int score = 0;
    private readonly string waveMapDir = System.IO.Directory.GetCurrentDirectory() + "/Assets/5 - JSON_WaveScripts/";
    private List<WaveMap> waveMaps;
    private WaveMap currWave;
    private enum GameStates { Idle, Priming, Playing, GameOver };
    private GameStates currState = GameStates.Idle;

    [Serializable]
    public class WaveMap
    {
        public int wave_id;
        public string wave_name;
        public int num_recycle;
        public int num_contam;
        public float conv_spd;
    }

    #region Private Methods

    private void Start()
    {
        if (gm == null)
            gm = this.gameObject.GetComponent<GameManager>();

        scoreDisplay.text = "Score: " + score;

        waveMaps = LoadWaves();
        currWave = waveMaps[0];
    }

    private void Update()
    {
        scoreDisplay.text = "Score: " + score;

        switch (currState)
        {
            case GameStates.Idle:
                //Wait for player input to begin playing
                break;
            case GameStates.Priming:
                //Start timer for x amount of seconds, then Playing
                break;
            case GameStates.Playing:
                //Play until wave complete or game over
                break;
            case GameStates.GameOver:
                //Return to menu?
                break;
        }
    }

    private List<WaveMap> LoadWaves()
    {
        List<WaveMap> waveMaps = new List<WaveMap>();
        string[] jsonWaveMaps = System.IO.Directory.GetFiles(waveMapDir, "*.json");

        foreach (string jsonWaveMap in jsonWaveMaps)
        {
            string json = System.IO.File.ReadAllText(jsonWaveMap);
            WaveMap map = JsonUtility.FromJson<WaveMap>(json);

            waveMaps.Add(map);
        }

        return waveMaps;
    }

    #endregion

    #region Public Methods
    public void ItemMissed()
    {
        score -= 8;
    }

    public void ItemBinned()
    {
        score += 2;
    }
    #endregion
}

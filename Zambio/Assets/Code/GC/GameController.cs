using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;
using System.Text;
// Worker GameController
public class GameController : MonoBehaviour
{
    GameData data;
    GameControllerSingleton gc;

    public TextAsset powerUpFile, ammoFile;
    public Sprite[] AmmoSpriteList;
    public Sprite[] PowerUpSpriteList;
    public GameObject[] ammoPrefab;
    public GameObject[] powerUpPrefab;

    private bool UIenabled = true;

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        gc = GameControllerSingleton.get();

        gc.loadTexts(powerUpFile, ammoFile, AmmoSpriteList, PowerUpSpriteList, ammoPrefab, powerUpPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        gc.Update();
        // UI Toggle
        // UI should already be loaded through player controller -Ryan
        //if (Input.GetButtonDown("toggleUI"))
        //{
        //    if (UIenabled)
        //    {
        //        SceneManager.UnloadScene("UI");
        //        UIenabled = false;
        //    }
        //    else
        //    {
        //        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        //        UIenabled = true;
        //    }
        //}
    }
}
[Serializable]
class GameData
{
    public float PlayedTime { get; private set; }
    public float RoundTime { get; private set; }
    public int Round { get; private set; }
    public enum GameState
    {
        STATE_STORY,             // We are doing some story stuff
        STATE_DEMO,              // We are in Demo mode
        STATE_WAVE_IN_PROGRESS,  // There is a wave in progress
        STATE_CHILL              // We are between waves, no enemies to worry about
                                 // More States
    };
    // FSM to handle with game flow
    public GameState State { get; private set; }
}
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
    GameControllerSingleton gc;

    public TextAsset powerUpFile, ammoFile;
    public Sprite[] AmmoSpriteList;
    public Sprite[] PowerUpSpriteList;
    public GameObject[] ammoPrefab;
    public GameObject[] powerUpPrefab;

    SpawnerController spawnController;



    //private bool UIenabled = true;

    void Awake()
    {
        spawnController = GetComponent<SpawnerController>();
    }

    // Use this for initialization
    void Start()
    {
        gc = GameControllerSingleton.get();


        if (gc.powerUpByID.Count == 0) //Zach Edit - Partly solves deathsequence
            gc.loadTexts(powerUpFile, ammoFile, AmmoSpriteList, PowerUpSpriteList, ammoPrefab, powerUpPrefab);
    }

    // Update is called once per frame
    void Update()
    {

        if (!spawnController) {
            spawnController = GetComponent<SpawnerController>();

        }

        gc.Update();

        if (!gc.sc)
        {
            gc.RegisterSpawner(ref spawnController);
        }
        
    }
}

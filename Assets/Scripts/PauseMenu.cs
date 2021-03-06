﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void QuitScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!gameIsPaused)
            {
                Pause();
            }
        }
    }
}

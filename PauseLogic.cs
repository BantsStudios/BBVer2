using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseLogic : MonoBehaviour
{
    public GameObject pauseMenu;
    [SerializeField] private GameObject settings;
    public bool isPause;

    void Start()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))// && !isPause)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPause = true;
        }

        if (Time.timeScale > 0f) 
        {
            isPause = false;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        //isPause = false;
    }

    public void Settings()
    {
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
    }

    public void MainMenu()
    {
        //isPause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

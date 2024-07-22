using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject levelSelect;
    [SerializeField] private GameObject settings;

    private void Start()
    { 
        startMenu.SetActive(true);
        levelSelect.SetActive(false);
        settings.SetActive(false);
    }

    public void StartGame()
    {
        levelSelect.SetActive(true);
        startMenu.SetActive(false);
    }

    public void PlayCredits()
    {
        SceneManager.LoadScene(19);
    }

    public void StopCredits() 
    {
        SceneManager.LoadScene(0);
    }

    public void OpenSettings() 
    {
        startMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void CloseSettings() 
    {
        startMenu.SetActive(true);
        settings.SetActive(false);
    }

    public void BackToStart()
    {
        startMenu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}

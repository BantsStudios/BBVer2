using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    private AudioSource levelCompleteSound;
    [SerializeField] private LevelLogic dooor;
    private bool isLevelComplete = false;
    [SerializeField] private int levelIndex;

    private void Start()
    {
        levelCompleteSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BB") && !isLevelComplete)
        {
            isLevelComplete = true;
            levelCompleteSound.Play();
            Invoke(nameof(LevelComplete), 1f);
        }
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(levelIndex);
    }

    private void LevelComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject resumeBtn;
    public GameObject levelClearTxt;
    private Scene currActiveScene;

    void Start()
    {
        currActiveScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currActiveScene.name);
    }

    public void endGame()
    {
        pausePanel.SetActive(true);
        resumeBtn.SetActive(false);
        levelClearTxt.SetActive(true);
    }
}

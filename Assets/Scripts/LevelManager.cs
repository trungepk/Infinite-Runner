using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private bool isPause;
	void Start () {
		
	}
	
	public void BackToMainMenu()
    {
        SceneManager.LoadScene(Constants.MainMenu);
        Time.timeScale = 1;
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(Constants.Start);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = !isPause ? 0 : 1;
        isPause = !isPause;
    }
}

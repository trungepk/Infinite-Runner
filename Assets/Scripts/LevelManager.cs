using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	void Start () {
		
	}
	
	public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

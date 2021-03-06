﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	public Transform canvas; 
	private string levelToLoad;
	private PlayerStats playerStats;

	private int nextLevel;

	void Start() {
		playerStats = PlayerStats.getInstance();
		levelToLoad = SceneManager.GetActiveScene ().name;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			Pause ();
		} else if (Input.GetKeyDown (KeyCode.O)) {
			Restart ();
		}
	}

	public void Pause(){
		if (canvas.gameObject.activeInHierarchy == false) {
			canvas.gameObject.SetActive (true); 
			Time.timeScale = 0;
		} else {
			canvas.gameObject.SetActive (false);
			Time.timeScale = 1;
		}
	}

	public void Restart(){
		playerStats.reset();
		SceneManager.LoadScene (levelToLoad);
		Time.timeScale = 1;
	}

	public void Quit(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("MainMenu");
	}
}

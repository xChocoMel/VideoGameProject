using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour {

	public PlayerStats playerStats;
	public Transform canvas; 

	// Use this for initialization
	void Start () {
		playerStats = PlayerStats.getInstance();
	}

	public void Restart(){
		playerStats.reset();
		SceneManager.LoadScene (playerStats.Level);
	}

	public void Quit(){
		SceneManager.LoadScene ("MainMenu");
	}
}

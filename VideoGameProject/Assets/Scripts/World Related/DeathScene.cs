using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour {

	public PlayerStats playerStats;
	public Transform canvas; 
	public static bool gameRestart;

	// Use this for initialization
	void Start () {
		playerStats = PlayerStats.getInstance();
		gameRestart = false;
	}

	public void Restart(){
		Clear ();
		SceneManager.LoadScene (playerStats.Level);
	}

	public void Quit(){
		Clear ();
		SceneManager.LoadScene ("MainMenu");
	}

	public void Clear() {
		gameRestart = true;
		playerStats.reset ();
		playerStats.Position = Destroyer.playerPos;
	}
}

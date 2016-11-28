using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Destroyer: MonoBehaviour
{
	public static List<string> objectsCollected = new List<string>();
	public static List<string> enemiesDefeated = new List<string>();
	public static string sceneName;
	private Scene currentScene;
	public static GameObject thePlayer;
	public static Vector3 playerPos;


	void Awake()
	{
		thePlayer = GameObject.Find ("Player");
		playerPos = new Vector3 (thePlayer.transform.position.x, thePlayer.transform.position.y, thePlayer.transform.position.z);
		DontDestroyOnLoad (this.gameObject);
		currentScene = SceneManager.GetActiveScene ();
		sceneName = currentScene.name;
		if (DeathScene.gameRestart) {
			foreach (string item in  Destroyer.objectsCollected) {
				GameObject toActivate = GameObject.Find ("Environment/Objects/" + item);
				if (toActivate.activeInHierarchy == false) {
					toActivate.SetActive (true);
				}
			}

			foreach (string item in  Destroyer.enemiesDefeated) {
				GameObject toActivate = GameObject.Find ("Enemies/" + item);
				if (toActivate.activeInHierarchy == false) {
					toActivate.SetActive (true);
				}
			}
			objectsCollected.Clear ();
			enemiesDefeated.Clear ();
			DeathScene.gameRestart = false;
		}
	}
    // Update is called once per frame
    
	void Start() {

	}

	void Update()
    {
		
    }

	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			foreach (string collected in objectsCollected) {
				GameObject toDestroy = GameObject.Find (collected);
				if (toDestroy != null) {
					toDestroy.gameObject.SetActive (false);
				}
			}
			foreach (string enemy in enemiesDefeated) {
				GameObject toDestroy = GameObject.Find(enemy);
				if (toDestroy != null) {
					toDestroy.gameObject.SetActive (false);
				}
			}
		} 
		else if (level == 2) {
			foreach (string collected in objectsCollected) {
				GameObject toDestroy = GameObject.Find (collected);
				if (toDestroy != null) {
					toDestroy.gameObject.SetActive (false);
				}
			}
			foreach (string enemy in enemiesDefeated) {
				GameObject toDestroy = GameObject.Find(enemy);
				if (toDestroy != null) {
					toDestroy.gameObject.SetActive (false);
				}
			}
		}
	}
}

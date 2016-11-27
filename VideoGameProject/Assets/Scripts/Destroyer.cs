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


	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
		currentScene = SceneManager.GetActiveScene ();
		sceneName = currentScene.name;

	}
    // Update is called once per frame
    void Update()
    {
		
    }

	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			foreach (string collected in objectsCollected) {
				GameObject toDestroy = GameObject.Find ("Environment/Objects/" + collected);
				if (toDestroy != null) {
					toDestroy.gameObject.SetActive (false);
				}
			}
			foreach (string enemy in enemiesDefeated) {
				GameObject toDestroy = GameObject.Find("Enemies/" + enemy);
				if (toDestroy != null) {
					toDestroy.gameObject.SetActive (false);
				}
			}
		} 
		else if (level == 2) {
			foreach (string collected in objectsCollected) {
				GameObject toDestroy = GameObject.Find ("Environment/Objects/" + collected);
				if (toDestroy != null) {
					toDestroy.gameObject.SetActive (false);
				}
			}
			foreach (string enemy in enemiesDefeated) {
				GameObject toDestroy = GameObject.Find("Enemies/" + enemy);
				if (toDestroy != null) {
					toDestroy.gameObject.SetActive (false);
				}
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
	public void changeScene(){
		SceneManager.LoadScene ("Level01");
	}
}

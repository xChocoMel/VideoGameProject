using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIBehaviours : MonoBehaviour {

    public string sceneName;

	public void HeyGuyzButton() {
		SceneManager.LoadScene (sceneName);
	}
}

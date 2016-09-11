using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CylinderEnemy : MonoBehaviour {

	private IEnumerator theCoroutine;

	public GameObject originalProjectile;

	void Start () {
		DontDestroyOnLoad (this.gameObject);
		StartCoroutine (Shoot ());
	}
	void Update () { 
		if(Input.GetKeyUp(KeyCode.S)){
			//StopAllCoroutines ();
			StopCoroutine(theCoroutine);
		}
	}

	IEnumerator Shoot() {
		while (true) {

			Instantiate(originalProjectile,transform.position,Quaternion.identity);
			yield return new WaitForSeconds (5);
		}
	}
}

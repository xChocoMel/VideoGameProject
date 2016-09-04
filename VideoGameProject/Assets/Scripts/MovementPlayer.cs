using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovementPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		transform.Translate (h * 10 * Time.deltaTime, 0, v * 10 * Time.deltaTime, Space.World);

	}

	void OnCollisionEnter(Collision c){
		Debug.Log("Entered Collition with: " + c.transform.name);
		if (c.gameObject.CompareTag ("Enemy")){
			SceneManager.LoadScene ("BattleScene");
		}

		//If you want an enemy to disappear, use this code
		/*if (c.gameObject.CompareTag ("Enemy")){
			c.gameObject.SetActive (false);
		}*/
	}
}
	
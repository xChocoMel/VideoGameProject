using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovementPlayer : MonoBehaviour {

	public int speed;
	private static bool sceneSwitched;

	private GameObject thePlayer;

	// Use this for initialization
	void Start () {
		thePlayer = GameObject.Find("Player");
		if (sceneSwitched) {
			PlayerComingBack ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		transform.Translate (h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime, Space.World);

	}

	void OnCollisionEnter(Collision c){
		Debug.Log("Entered Collision with: " + c.transform.name);
		if (c.gameObject.CompareTag ("Enemy")) {
			sceneSwitched = true;
			PlayerSwitchingScene ();
			SceneManager.LoadScene ("BattleScene");
		}
	}

	void PlayerSwitchingScene () {
		PlayerPrefs.SetFloat ("PlayerX", thePlayer.transform.position.x);
		PlayerPrefs.SetFloat ("PlayerY", thePlayer.transform.position.y);
		PlayerPrefs.SetFloat ("PlayerZ", thePlayer.transform.position.z);
	}

	void PlayerComingBack () {
		float newPlayerX = PlayerPrefs.GetFloat ("PlayerX");
		float newPlayerY = PlayerPrefs.GetFloat ("PlayerY");
		float newPlayerZ = PlayerPrefs.GetFloat ("PlayerZ");
		thePlayer.transform.position = new Vector3 (newPlayerX, newPlayerY, newPlayerZ);
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Hello");

		if (other.gameObject.CompareTag ("Coin")){
			other.gameObject.SetActive (false);
		}
	}
}
	
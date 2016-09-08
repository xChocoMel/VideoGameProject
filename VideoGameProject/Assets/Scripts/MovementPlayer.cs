using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour {

	public int speed;

	private int counter;

	public static bool sceneSwitched;

	private GameObject[] numCoin;
	private GameObject thePlayer;
	public GameObject panel, panel2;

	public Text coinText;
	public Text winText;

	// Use this for initialization
	void Start () {
		counter = 0;
		winText.text = "";
		numCoin = GameObject.FindGameObjectsWithTag ("Coin");
		coinText.text = "Coins Collected: " + counter.ToString()+"/"+numCoin.Length;
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

		else if (c.gameObject.name=="Goal") {
			coinText.gameObject.SetActive (false);
			panel.gameObject.SetActive (false);
			panel2.gameObject.SetActive (true);

			winText.text = "LEVEL CLEARED\n\nCOINS COLLECTED\n---\nRANK\nS";
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
		if (other.gameObject.CompareTag ("Coin")) {
			other.gameObject.SetActive (false);
			this.counter++;
			coinText.text = "Coins Collected: " + counter.ToString () + "/" + numCoin.Length;
		} 
	}
		
}
	
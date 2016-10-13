using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour {

	public float speed;
    public float rotateSpeed;

    private int counter;

	public static bool sceneSwitched;

	private GameObject[] numCoin;
	private GameObject thePlayer;

    private GameObject panel, panel2;
    private Text coinText;
    private Text winText;

    // Use this for initialization
    void Start() {
        if (speed <= 0) {
            speed = 5f;
        }

        if (rotateSpeed <= 0) { 
            rotateSpeed = 50f;
        }

        counter = 0;
        numCoin = GameObject.FindGameObjectsWithTag("Coin");
        thePlayer = GameObject.Find("Player");

        panel = GameObject.Find("Panel1");
        panel2 = GameObject.Find("Panel2");        

        coinText = GameObject.Find("coinText").GetComponent<Text>();
        coinText.text = "Coins Collected: " + counter.ToString() + "/" + numCoin.Length;

        winText = GameObject.Find("winText").GetComponent<Text>();
        winText.text = "";
        panel2.SetActive(false);

        if (sceneSwitched) {
			PlayerComingBack ();
		}
	}
	
	// Update is called once per frame
	void Update () {
        CheckKeyInput();
    }

    private void CheckKeyInput() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Option 1: Vertical > transform
        //transform.Translate(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime, Space.World);

        //Option 2: Vertical > rotation
        transform.Translate(0, 0, v * speed * Time.deltaTime);
        transform.Rotate(0, h * rotateSpeed * Time.deltaTime, 0);
    }

    void OnCollisionEnter(Collision c){
		Debug.Log("Entered Collision with: " + c.transform.name);

        if (c.gameObject.CompareTag ("Enemy")) {
			sceneSwitched = true;
			PlayerSwitchingScene ();
			SceneManager.LoadScene ("BattleScene");
		}

		else if (c.gameObject.name == "Goal") {
			coinText.gameObject.SetActive (false);
			panel.SetActive (false);
			panel2.SetActive (true);

			winText.text = "LEVEL CLEARED\n\nCOINS COLLECTED\n" + counter.ToString() + "/" + numCoin.Length + "\nRANK\nS";
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
		//When the player touch a coin it add one to the counter
		if (other.gameObject.CompareTag ("Coin")) {
			other.gameObject.SetActive (false);
			this.counter++;
			coinText.text = "Coins Collected: " + counter.ToString () + "/" + numCoin.Length;
		} 
	}		
}
	
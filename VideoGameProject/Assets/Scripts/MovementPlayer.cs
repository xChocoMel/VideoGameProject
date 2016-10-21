using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour {

    private PlayerStats playerStats;

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
        playerStats = PlayerStats.getInstance();

        if (speed <= 0) { speed = 5f; }
        if (rotateSpeed <= 0) {  rotateSpeed = 50f; }

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
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Debug.Log(enemies.Length);

        if (c.gameObject.CompareTag ("Enemy")) {
            Debug.Log("ENEMY COLLISON");

            playerStats.EncounteredEnemy = c.gameObject.GetComponent<Enemy>();
            Destroy(c.gameObject);
            PlayerSwitchingScene();
            SceneManager.LoadScene("BattleScene");
        } else if (c.gameObject.name == "Goal") {
			coinText.gameObject.SetActive (false);
			panel.SetActive (false);
			panel2.SetActive (true);

			winText.text = "LEVEL CLEARED\n\nCOINS COLLECTED\n" + counter.ToString() + "/" + numCoin.Length + "\nRANK\nS";
		}
	}

	void PlayerSwitchingScene () {
        playerStats.Position = thePlayer.transform.position;
        sceneSwitched = true;
    }

	void PlayerComingBack () {
		thePlayer.transform.position = playerStats.Position;        
        sceneSwitched = false;
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
	
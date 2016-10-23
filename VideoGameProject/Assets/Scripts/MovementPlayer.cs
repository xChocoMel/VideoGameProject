using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour {

    private PlayerStats playerStats;

    public float speed;
    public float rotateSpeed;

	public static bool sceneSwitched;

    private GameObject[] numCoin;
	private GameObject thePlayer;
	private Text objectText;

    // Use this for initialization
    void Start() {
        playerStats = PlayerStats.getInstance();

		objectText= GameObject.Find("ObjectText").GetComponent<Text>();

		objectText.text =  
			"Accuracy: "+playerStats.ObAccuracy+
			"\nStrength: "+playerStats.ObStrength+
			"\nDefense: "+playerStats.ObDefense+
			"\nHealth: "+playerStats.ObHealth;

        if (speed <= 0) { speed = 5f; }
        if (rotateSpeed <= 0) {  rotateSpeed = 50f; }

        thePlayer = GameObject.Find("Player");

        if (sceneSwitched) {
			PlayerComingBack ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		CheckKeyInput ();
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
		//Testing purpouses
		Debug.Log("Entered Collision with: " + c.transform.name);

		//Change to a fight scene if an enemy is touched
        if (c.gameObject.CompareTag ("Enemy")) {
            Debug.Log("ENEMY COLLISON");

            playerStats.EncounteredEnemy = c.gameObject.GetComponent<Enemy>();
            Destroy(c.gameObject);
            PlayerSwitchingScene();
            SceneManager.LoadScene("BattleScene");
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

	void handleObjects(){
		objectText.text =  
			"Accuracy: "+playerStats.ObAccuracy+
			"\nStrength: "+playerStats.ObStrength+
			"\nDefense: "+playerStats.ObDefense+
			"\nHealth: "+playerStats.ObHealth;
	}



	//When an object is touched it modifies the counters
	void OnTriggerEnter(Collider other) {
		switch (other.gameObject.tag)
		{
		case "Coin":
			other.gameObject.SetActive (false);
			break;
		case "Health":
			other.gameObject.SetActive (false);
			playerStats.ObHealth += 1;
			break;
		case "Power":
			other.gameObject.SetActive (false);
			playerStats.ObStrength += 1;
			break;
		case "Accuracy":
			other.gameObject.SetActive (false);
			playerStats.ObAccuracy += 1;
			break;
		case "Defense":
			other.gameObject.SetActive (false);
			playerStats.ObDefense += 1;
			break;
		default:
			break;
		}
		handleObjects ();
		Debug.Log(playerStats.ObStrength+" "+playerStats.ObHealth+" "+playerStats.ObDefense+" "+playerStats.ObAccuracy);
	}
}
	
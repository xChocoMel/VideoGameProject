﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour {

    private PlayerStats playerStats;
    private Animator playerAnimator;

    public float speed;
	public float rotateSpeed;

	public static bool sceneSwitched;

    private GameObject[] numCoin;
	private GameObject thePlayer;
	private Text objectText;

    private AudioSource audioSource;
    public AudioClip collectableClip;

    // Use this for initialization
    void Start() {
        playerStats = PlayerStats.getInstance();
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();

		playerStats.Level = SceneManager.GetActiveScene().name;

        objectText = GameObject.Find("ObjectText").GetComponent<Text>();
		objectText.text =  
			"Accuracy: " + playerStats.ObAccuracy +
			"\nStrength: " + playerStats.ObStrength +
			"\nDefense: " + playerStats.ObDefense +
			"\nHealth: " + playerStats.ObHealth;

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

        playerAnimator.SetFloat("Speed", v);
    }

    void OnCollisionEnter(Collision c){
		//Testing purpouses
		Debug.Log("Entered Collision with: " + c.transform.name);

		//Change to a fight scene if an enemy is touched
        if (c.gameObject.CompareTag ("Enemy")) {
            Debug.Log("ENEMY COLLISON");

            playerStats.EncounteredEnemy = c.gameObject.GetComponent<Enemy>();
			Destroyer.enemiesDefeated.Add (c.gameObject.name);
            Destroy(c.gameObject);
            PlayerSwitchingScene();
            SceneManager.LoadScene("BattleScene");
        }
		if (c.gameObject.name.Equals("Goal")) {
			Destroyer.enemiesDefeated.Clear ();
			Destroyer.objectsCollected.Clear ();
			if (Destroyer.sceneName.Equals("Level01")) {
				SceneManager.LoadScene ("Level02");
			}
			else if (Destroyer.sceneName.Equals("Level02")) {
				SceneManager.LoadScene ("MainMenu");
			}
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
			"Accuracy: " + playerStats.ObAccuracy +
			"\nStrength: " + playerStats.ObStrength +
			"\nDefense: " + playerStats.ObDefense +
			"\nHealth: " + playerStats.ObHealth;
	}

	//When an object is touched it modifies the counters
	void OnTriggerEnter(Collider other) {
		switch (other.gameObject.tag)
		{
		case "Health":
			audioSource.PlayOneShot (collectableClip);
			playerStats.ObHealth += 1;
			break;
		    case "Power":
            	audioSource.PlayOneShot(collectableClip);
                playerStats.ObStrength += 1;
			    break;
		    case "Accuracy":
                audioSource.PlayOneShot(collectableClip);
                playerStats.ObAccuracy += 1;
			    break;
		    case "Defense":
                audioSource.PlayOneShot(collectableClip);
                playerStats.ObDefense += 1;
			    break;
		    default:
			    break;
		}

		Destroyer.objectsCollected.Add (other.gameObject.name);
		handleObjects ();
		other.gameObject.SetActive (false);
		Debug.Log(playerStats.ObStrength + " " + playerStats.ObHealth + " " + playerStats.ObDefense + " " + playerStats.ObAccuracy);
	}
}
	
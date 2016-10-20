﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour {

	public float speed;
    public float rotateSpeed;

	public static bool sceneSwitched;

	private GameObject thePlayer;

	[SerializeField]
	private PlayerStats health;

	private void Awake(){
		health.Initialize ();
	}

    // Use this for initialization
    void Start() {

        if (speed <= 0) {
            speed = 5f;
        }

        if (rotateSpeed <= 0) { 
            rotateSpeed = 50f;
        }

        thePlayer = GameObject.Find("Player");


        if (sceneSwitched) {
			PlayerComingBack ();
		}
	}
	
	// Update is called once per frame
	void Update () {
        CheckKeyInput();
		if (Input.GetKeyUp (KeyCode.Q)) {
			health.CurrentVal -= 10;
		} else if (Input.GetKeyUp (KeyCode.E)) {
			health.CurrentVal += 10;
		}
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

	//When an object is touched it modifies the counters
	void OnTriggerEnter(Collider other) {
		switch (other.gameObject.tag)
		{
		case "Coin":
			other.gameObject.SetActive (false);
			break;
		case "Health":
			other.gameObject.SetActive (false);
			break;
		case "Power":
			other.gameObject.SetActive (false);
			break;
		case "Accuracy":
			other.gameObject.SetActive (false);
			break;
		case "Defense":
			other.gameObject.SetActive (false);
			break;
		default:
			break;
		}
	}

}
	
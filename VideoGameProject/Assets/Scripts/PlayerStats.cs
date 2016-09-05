using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int playerHP;
	public int playerStrength;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("PlayerHP", playerHP);
		PlayerPrefs.SetInt ("PlayerStrenght", playerStrength);
	}
	
	// Update is called once per frame
	void Update () {
	}
}

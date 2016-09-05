using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

	public int enemyHP;
	public int enemyStrength;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("EnemyHP", enemyHP);
		PlayerPrefs.SetInt ("EnemyStrength", enemyStrength);
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHP <= 0) {
			this.gameObject.SetActive (false);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private int health;
    private int strength;

    public int Health {
        get { return health; }
        set { health = value; }
    }

    public int Strength {
        get { return strength; }
        set { strength = value; }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}

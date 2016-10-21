using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    protected int health;
    protected int strength;
    protected int defence;
    protected bool defending;

    public int Health {
        get { return health; }
        set { health = value; }
    }

    public int Strength {
        get { return strength; }
        set { strength = value; }
    }

    public int Defence {
        get { return defence; }
        set { defence = value; }
    }

    public bool Defending {
        get { return defending; }
        set { defending = value; }
    }

    // Use this for initialization
    void Start () { }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    //Return damage
    //If defending, damage = 0
    public virtual int Fight() { return 0; }
}

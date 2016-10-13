using UnityEngine;
using System.Collections;

public class PlayerStats {

    private static PlayerStats instance = null;

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

    public static PlayerStats getInstance() {
        if (instance == null) {
            instance = new PlayerStats();
        }

        return instance;
    }

    private PlayerStats() {
        reset();
	}

    public void reset() {
        Health = 500;
        Strength = 100;
    }

    public void deleteInstance() {
        instance = null;
    }
}

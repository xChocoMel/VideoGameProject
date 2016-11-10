using UnityEngine;
using System.Collections;
using System;

public class PlayerStats {

    private static PlayerStats instance = null;

    private Vector3 position;
	private int health;
	private int strength;
    protected int defence;
    protected bool defending;

	// Collectables
	private int obAccuracy, obHealth, obDefense, obStrength;

    private static Enemy encounteredEnemy;

    public Vector3 Position {
        get { return position; }
        set { position = value; }
	}

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

    public int ObAccuracy {
        get { return obAccuracy; }
        set { obAccuracy = value; }
    }

    public int ObDefense {
        get { return obDefense; }
        set { obDefense = value; }
    }

    public int ObStrength {
        get { return obStrength; }
        set { obStrength = value; }
    }

    public int ObHealth {
        get { return obHealth; }
        set { obHealth = value; }
    }

    public Enemy EncounteredEnemy {
        get { return encounteredEnemy; }
        set { encounteredEnemy = value; }
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
		Defence = 100;
		obAccuracy=obHealth=obDefense=obStrength=0;
    }

    public void deleteInstance() {
        instance = null;
    }
}

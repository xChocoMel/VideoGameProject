using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlayerStats {

    private static PlayerStats instance = null;

    private Vector3 position;
	private int health;
	private int strength;
    protected int defence;
    protected bool defending;

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
    }

    public void deleteInstance() {
        instance = null;
    }
}

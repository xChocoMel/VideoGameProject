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

	[SerializeField]
	private BarScript bar;

	[SerializeField]
	private float maxVal;

	[SerializeField]
	private float currentVal;

	public void Initialize(){
		this.MaxVal = maxVal;
		this.CurrentVal = currentVal;
	}

	public float MaxVal {
		get { return maxVal; }
		set { 
			this.maxVal = value; 
			bar.MaxValue = maxVal;  
		}
	}

	public float CurrentVal {
		get { return currentVal; }
		set { 
			this.currentVal = Mathf.Clamp(value,0,MaxVal);
			bar.Value = currentVal;
		}
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

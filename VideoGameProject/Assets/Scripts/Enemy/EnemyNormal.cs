using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnemyNormal : Enemy {

    private Text txtEnemyTurn;

    void Awake() {
        Health = 200;
        Strength = 50;
        Defence = 25;
        Defending = false;
    }

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update () { }
    
    public override int Fight() {
        if (txtEnemyTurn == null) {
            try {
                txtEnemyTurn = GameObject.Find("txtEnemyTurn").GetComponent<Text>();
                txtEnemyTurn.text = "";
            } catch (Exception ex) { 
				Debug.Log (ex);
			}
        }

        int damage = 0;
        int r = UnityEngine.Random.Range(0, 2);

        if (r == 0) {
            Defend();
            txtEnemyTurn.text = "Defence activated";
        } else {
            damage = Attack();
        }

        return damage;
    }

    private int Attack() {
        return Strength;
    }

    private void Defend() {
        Defending = true;
    }
}

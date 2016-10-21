using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnemyStrong : Enemy {

    private Text txtEnemyTurn;

    void Awake() {
        Health = 500;
        Strength = 150;
        Defence = 200;
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
            } catch (Exception ex) { }
        }

        int damage = 0;
        int r = UnityEngine.Random.Range(0, 9);

        if (r == 9) {
            damage = SpecialAttack();
            txtEnemyTurn.text = "Special attack succeeded\nDamage: " + damage;
        } else if (r >= 0 && r <= 2) {
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

    private int SpecialAttack() {
        return Strength * 2;
    }
}

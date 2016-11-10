using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnemyNormal : Enemy {

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
        int damage = 0;
        int r = UnityEngine.Random.Range(0, 2);

        if (r == 0) {
            Defend();            
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

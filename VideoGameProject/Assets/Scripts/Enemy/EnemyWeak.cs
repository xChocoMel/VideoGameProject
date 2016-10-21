using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnemyWeak : Enemy {

    void Awake() {
        Health = 100;
        Strength = 20;
        Defence = 0;
        Defending = false;
    }

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update () { }

    public override int Fight() {
        int damage = Attack();
        return damage;
    }

    private int Attack() {
        return Strength;
    }    
}

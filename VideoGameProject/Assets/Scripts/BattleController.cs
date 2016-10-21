using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class BattleController : MonoBehaviour {

    private PlayerStats player;
    private Enemy enemy;

    private bool turn;
    private bool gameEnd;

    private GameObject buttonPanel;
    private Text txtTurn;
    private Text txtPlayerHealth;
    private Text txtEnemyHealth;
    private Text txtPlayerTurn;
    private Text txtEnemyTurn;

    public string sceneName1;

    public EnemyWeak enemyWeak;
    public EnemyNormal enemyNormal;
    public EnemyStrong enemyStrong;

    // Use this for initialization
    void Start () {
        player = PlayerStats.getInstance();

        Enemy encountered = player.EncounteredEnemy;
        Vector3 pos = new Vector3(5, 0, -7.5f);
        Vector3 rot = new Vector3(0, 270, 0);

        //enemy = (Enemy)Instantiate(player.EncounteredEnemy, pos, Quaternion.Euler(rot));

        if (encountered.GetType() == typeof(EnemyWeak)) {
            Debug.Log("Enemy Weak encoutered");
            enemy = (EnemyWeak)Instantiate(enemyWeak, pos, Quaternion.Euler(rot));
        } else if (encountered.GetType() == typeof(EnemyNormal)) {
            Debug.Log("Enemy Normal encoutered");
            enemy = (EnemyNormal)Instantiate(enemyNormal, pos, Quaternion.Euler(rot));
        } else if (encountered.GetType() == typeof(EnemyStrong)) {
            Debug.Log("Enemy Strong encoutered");
            enemy = (EnemyStrong)Instantiate(enemyStrong, pos, Quaternion.Euler(rot));
        }

        turn = true;
        gameEnd = false;

        buttonPanel = GameObject.Find("Panel");
        txtTurn = GameObject.Find("txtTurn").GetComponent<Text>();
        txtPlayerHealth = GameObject.Find("txtPlayerHP").GetComponent<Text>();
        txtEnemyHealth = GameObject.Find("txtEnemyHP").GetComponent<Text>();
        txtPlayerTurn = GameObject.Find("txtPlayerTurn").GetComponent<Text>();
        txtEnemyTurn = GameObject.Find("txtEnemyTurn").GetComponent<Text>();

        txtPlayerHealth.text = "Player Health: " + player.Health;
        txtEnemyHealth.text = "Enemy Health: " + enemy.Health;
        txtPlayerTurn.text = "";
        txtEnemyTurn.text = "";
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void SwitchTurns() {
        ChechWin();

        if (!gameEnd) {
            if (turn) {
                turn = false;
                txtTurn.text = "Enemies turn";
                buttonPanel.gameObject.SetActive(false);
                StartCoroutine(EnemyFight());
            } else {
                turn = true;
                txtTurn.text = "Your turn";
                buttonPanel.gameObject.SetActive(true);
            }
        }
    }

    public void Attack() {
        int damage = player.Strength;

        if (enemy.Defending) {
            damage -= enemy.Defence;
            damage = Math.Max(0, damage);
            enemy.Defending = false;
        }

        enemy.Health -= damage;
        enemy.Health = Math.Max(0, enemy.Health);
        txtPlayerTurn.text = "Damage: " + damage;
        txtEnemyHealth.text = "Enemy Health: " + enemy.Health;
        SwitchTurns();
    }

    public void Defend() {
        player.Defending = true;
        txtPlayerTurn.text = "Defence activated";
        SwitchTurns();
    }

    public void SpecialAttack() {
        int damage = 0;
        int r = UnityEngine.Random.Range(0, 5);

        if (r == 0) {
            damage = player.Strength * 2;

            if (enemy.Defending) {
                damage -= enemy.Defence;
                damage = Math.Max(0, damage);
                enemy.Defending = false;
            }

            enemy.Health -= damage;
            enemy.Health = Math.Max(0, enemy.Health);
            txtEnemyHealth.text = "Enemy Health: " + enemy.Health;            
        }

        if (damage > 0) {
            txtPlayerTurn.text = "Special attack succeeded\nDamage: " + damage;
        } else {
            txtPlayerTurn.text = "Special attack failed";
        }

        SwitchTurns();
    }

    IEnumerator EnemyFight() {
        yield return new WaitForSeconds(2.0f);

        int damage = enemy.Fight();
        bool special = false;

        if (damage > enemy.Strength) {
            special = true;
        }

        if (player.Defending) {
            damage -= player.Defence;
            damage = Math.Max(0, damage);
            player.Defending = false;
        }

        player.Health -= damage;
        player.Health = Math.Max(0, player.Health);

        if (damage > 0) {
            if (special) {
                txtEnemyTurn.text = "Special attack\nDamage: " + damage;
            } else {
                txtEnemyTurn.text = "Damage: " + damage;
            }
        }

        txtPlayerHealth.text = "Player Health: " + player.Health;
        SwitchTurns();
    }

    private void ChechWin() {
        if (player.Health <= 0) {
            txtTurn.text = "You lose";
            StartCoroutine(EndGame());
        } else if (enemy.Health <= 0) {
            txtTurn.text = "You won";
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame() {
        gameEnd = true;
        buttonPanel.gameObject.SetActive(false);

        player.Health = player.Health;
        enemy.Health = enemy.Health;
        player.EncounteredEnemy = null;

        yield return new WaitForSeconds(2.0f);
		//public load scene for testing purpose
        SceneManager.LoadScene(sceneName1);
    }
}

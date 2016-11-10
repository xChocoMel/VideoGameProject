using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class BattleController : MonoBehaviour {

    private PlayerStats player;    
    private Enemy enemy;

    private Animator playerAnimator;
    private Animator enemyAnimator;

    private bool turn;
    private bool gameEnd;

    private GameObject buttonPanel;
	private GameObject objectPanel;
    private Text txtTurn;
    private Text txtPlayerHealth;
    private Text txtEnemyHealth;
    private Text txtPlayerTurn;
    private Text txtEnemyTurn;
    private Text txtObAccuracy;
    private Text txtObStrength;
    private Text txtObDefense;
    private Text txtObHealth;

    public string sceneName1;

    public EnemyWeak enemyWeak;
    public EnemyNormal enemyNormal;
    public EnemyStrong enemyStrong;

    private ParticleSystem psPlayerAttack;
    private ParticleSystem psPlayerDefense;
    private ParticleSystem psPlayerSpecial;
    private ParticleSystem psEnemyAttack;
    private ParticleSystem psEnemyDefense;
    private ParticleSystem psEnemySpecial;

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

        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        enemyAnimator = enemy.GetComponent<Animator>();

        psPlayerAttack = GameObject.Find("psPlayerAttack").GetComponent<ParticleSystem>();
        psPlayerDefense = GameObject.Find("psPlayerDefense").GetComponent<ParticleSystem>();
        psPlayerSpecial = GameObject.Find("psPlayerSpecial").GetComponent<ParticleSystem>();
        psEnemyAttack = GameObject.Find("psEnemyAttack").GetComponent<ParticleSystem>();
        psEnemyDefense = GameObject.Find("psEnemyDefense").GetComponent<ParticleSystem>();
        psEnemySpecial = GameObject.Find("psEnemySpecial").GetComponent<ParticleSystem>();

        turn = true;
        gameEnd = false;

        //UI
        buttonPanel = GameObject.Find("Panel");
		objectPanel = GameObject.Find ("ObjectPanel");
        txtTurn = GameObject.Find("txtTurn").GetComponent<Text>();
        txtPlayerHealth = GameObject.Find("txtPlayerHP").GetComponent<Text>();
        txtEnemyHealth = GameObject.Find("txtEnemyHP").GetComponent<Text>();
        txtPlayerTurn = GameObject.Find("txtPlayerTurn").GetComponent<Text>();
        txtEnemyTurn = GameObject.Find("txtEnemyTurn").GetComponent<Text>();
        txtObAccuracy = GameObject.Find("ButtonAcc").GetComponentInChildren<Text>();
        txtObStrength = GameObject.Find("ButtonStr").GetComponentInChildren<Text>();
        txtObDefense = GameObject.Find("ButtonDef").GetComponentInChildren<Text>();
        txtObHealth = GameObject.Find("ButtonHp").GetComponentInChildren<Text>();

        txtPlayerHealth.text = "Player Health: " + player.Health;
        txtEnemyHealth.text = "Enemy Health: " + enemy.Health;
        txtPlayerTurn.text = "";
        txtEnemyTurn.text = "";
        txtObAccuracy.text = "Accuracy: " + player.ObAccuracy;
        txtObStrength.text = "Strength: " + player.ObStrength;
        txtObDefense.text = "Defence: " + player.ObDefense;
        txtObHealth.text = "Health: " + player.ObHealth;
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
				objectPanel.gameObject.SetActive (false);
                StartCoroutine(EnemyFight());
            } else {
                turn = true;
                txtTurn.text = "Your turn";
                buttonPanel.gameObject.SetActive(true);
				objectPanel.gameObject.SetActive (true);
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

        psPlayerAttack.Play();
        StartCoroutine(AnimatePlayerAttack());

        SwitchTurns();
    }

    public void Defend() {
        player.Defending = true;
        psPlayerDefense.Play();
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
            psPlayerSpecial.Play();
            StartCoroutine(AnimatePlayerAttack());
        } else {
            txtPlayerTurn.text = "Special attack failed";
        }

        SwitchTurns();
    }

    IEnumerator EnemyFight() {
        yield return new WaitForSeconds(1.0f);

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

        if (enemy.Defending) {
            psEnemyDefense.Play();
            txtEnemyTurn.text = "Defence activated";
        }

        if (damage >= 0) {
            if (damage > 0) {
                StartCoroutine(AnimatePlayerDamage());
            }

            if (special) {
                psEnemySpecial.Play();
                txtEnemyTurn.text = "Special attack\nDamage: " + damage;
            } else {
                psEnemyAttack.Play();
                txtEnemyTurn.text = "Damage: " + damage;
            }
        }

        txtPlayerHealth.text = "Player Health: " + player.Health;
        yield return new WaitForSeconds(1.0f);

        SwitchTurns();
    }

    IEnumerator AnimatePlayerAttack() {
        playerAnimator.SetBool("Attack", true);
        yield return new WaitForSeconds(1.33f);
        playerAnimator.SetBool("Attack", false);
    }

    IEnumerator AnimatePlayerDamage() {
        yield return new WaitForSeconds(1.0f);
        playerAnimator.SetBool("Damage", true);
        yield return new WaitForSeconds(1.0f);
        playerAnimator.SetBool("Damage", false);
    }

    IEnumerator AnimateEnemyDamage() {
        yield return new WaitForSeconds(1.0f);
        enemyAnimator.SetBool("Death", true);
    }

    IEnumerator AnimateEnemySpecialAttack() {        
        yield return new WaitForSeconds(1.0f);
        
    }

    IEnumerator AnimateEnemyDead() {
        yield return new WaitForSeconds(1.0f);
        enemyAnimator.SetBool("Death", true);
    }

    //----This functions are used when the button in the battle scene is clicked----
    public void UseHealth() {
        if (player.ObHealth > 0) {
            txtPlayerTurn.text = "HEALTH + 100";
            player.Health += 100;
            txtPlayerHealth.text = "Player Health: " + player.Health;
            player.ObHealth -= 1;
            SwitchTurns();
        } else {
            txtPlayerTurn.text = "No Health objects left";
        }

        StartCoroutine(ChangeText());
    }

    public void UseStrength() {
        if (player.ObStrength > 0) {
            txtPlayerTurn.text = "Strength rasised to " + player.Strength;
            player.ObStrength -= 1;
            SwitchTurns();
        } else {
            txtPlayerTurn.text = "No Strength objects left";
        }

        StartCoroutine(ChangeText());
    }

    public void UseAccuracy() {
        if (player.ObAccuracy > 0) {
            txtPlayerTurn.text = "Accuracy raised 20%";
            player.ObAccuracy -= 1;
            SwitchTurns();
        } else {
            txtPlayerTurn.text = "No Accuracy objects left";
        }

        StartCoroutine(ChangeText());
    }

    public void UseDefense() {
        if (player.ObDefense > 0) {
            player.Defence += 50;
            txtPlayerTurn.text = "Defense raised to " + player.Defence;
            player.ObDefense -= 1;
            SwitchTurns();
        } else {
            txtPlayerTurn.text = "No Defense objects left";
        }

        StartCoroutine(ChangeText());
    }
    //---------------------------------------------------------------

    IEnumerator ChangeText() {
        txtObAccuracy.text = "Accuracy: " + player.ObAccuracy;
        txtObStrength.text = "Strength: " + player.ObStrength;
        txtObDefense.text = "Defence: " + player.ObDefense;
        txtObHealth.text = "Health: " + player.ObHealth;

        yield return new WaitForSeconds(1.0f);
    }

    private void ChechWin() {
        if (player.Health <= 0) {
			SceneManager.LoadScene("Death");
			txtTurn.text = "You lose";
            StartCoroutine(AnimatePlayerDamage());
            StartCoroutine(EndGame());
        } else if (enemy.Health <= 0) {
            txtTurn.text = "You won";
            StartCoroutine(AnimateEnemyDead());
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame() {
        gameEnd = true;
        buttonPanel.gameObject.SetActive(false);
		objectPanel.gameObject.SetActive (false);

		player.Defence = 100;

        player.Health = player.Health;
        enemy.Health = enemy.Health;
        player.EncounteredEnemy = null;

        yield return new WaitForSeconds(2.0f);
		//public load scene for testing purpose
		SceneManager.LoadScene(sceneName1);

    }
}

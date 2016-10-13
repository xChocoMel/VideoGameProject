using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleController : MonoBehaviour {

    private PlayerStats playerStats;
    private Enemy enemy;

    private int playerHealth;
    private int playerStrength;

    private int enemyHealth;
    private int enemyStrength;

    private bool turn;
    private bool gameEnd;

    private Button btnFight;
    private Text txtTurn;
    private Text txtPlayerHealth;
    private Text txtEnemyHealth;

	public string sceneName1;

    // Use this for initialization
    void Start () {
        playerStats = PlayerStats.getInstance();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();

        playerHealth = playerStats.Health;
        playerStrength = playerStats.Strength;

        enemyHealth = enemy.Health;
        enemyStrength = enemy.Strength;
        
        turn = true;
        gameEnd = false;

        btnFight = GameObject.Find("btnFight").GetComponent<Button>();
        txtTurn = GameObject.Find("txtTurn").GetComponent<Text>();
        txtPlayerHealth = GameObject.Find("txtPlayerHP").GetComponent<Text>();
        txtEnemyHealth = GameObject.Find("txtEnemyHP").GetComponent<Text>();

        txtPlayerHealth.text = "Player Health: " + playerHealth;
        txtEnemyHealth.text = "Enemy Health: " + enemyHealth;
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
                btnFight.gameObject.SetActive(false);
                StartCoroutine(EnemyFight());
            } else {
                turn = true;
                txtTurn.text = "Your turn";
                btnFight.gameObject.SetActive(true);
            }
        }
    }

    public void Fight() {
        enemyHealth -= playerStrength;

        if (enemyHealth < 0) {
            enemyHealth = 0;
        }

        txtEnemyHealth.text = "Enemy Health: " + enemyHealth;
        SwitchTurns();
    }

    IEnumerator EnemyFight() {
        yield return new WaitForSeconds(2.0f);

        playerHealth -= enemyStrength;

        if (playerHealth < 0) {
            playerHealth = 0;
        }

        txtPlayerHealth.text = "Player Health: " + playerHealth;
        SwitchTurns();
    }

    private void ChechWin() {
        if (playerHealth <= 0) {
            txtTurn.text = "You lose";
            StartCoroutine(EndGame());
        } else if (enemyHealth <= 0) {
            txtTurn.text = "You won";
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame() {
        gameEnd = true;
        btnFight.gameObject.SetActive(false);

        playerStats.Health = playerHealth;
        enemy.Health = enemyHealth;

        yield return new WaitForSeconds(2.0f);
		//public load scene for testing purpose
        SceneManager.LoadScene(sceneName1);

    }
}

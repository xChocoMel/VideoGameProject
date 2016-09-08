using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleController : MonoBehaviour {

    private int playerHP;
    private int enemyStrength;

    private int enemyHP;
    private int playerStrength;

    private bool turn;
    private bool gameEnd;

    private Button btnFight;
    private Text txtTurn;
    private Text txtPlayerHP;
    private Text txtEnemyHP;

	public string sceneName1;

    // Use this for initialization
    void Start () {

        playerHP = 500;
        playerStrength = 100;

        enemyHP = 200;
        enemyStrength = 50;
        
        turn = true;
        gameEnd = false;

        btnFight = GameObject.Find("btnFight").GetComponent<Button>();
        txtTurn = GameObject.Find("txtTurn").GetComponent<Text>();
        txtPlayerHP = GameObject.Find("txtPlayerHP").GetComponent<Text>();
        txtEnemyHP = GameObject.Find("txtEnemyHP").GetComponent<Text>();

        txtPlayerHP.text = "Player HP: " + playerHP;
        txtEnemyHP.text = "Enemy HP: " + enemyHP;
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
        enemyHP -= playerStrength;

        if (enemyHP < 0) {
            enemyHP = 0;
        }

        txtEnemyHP.text = "Enemy HP: " + enemyHP;
        SwitchTurns();
    }

    IEnumerator EnemyFight() {
        yield return new WaitForSeconds(2.0f);

        playerHP -= enemyStrength;

        if (playerHP < 0) {
            playerHP = 0;
        }

        txtPlayerHP.text = "Player HP: " + playerHP;
        SwitchTurns();
    }

    private void ChechWin() {
        if (playerHP <= 0) {
            txtTurn.text = "You lose";
            StartCoroutine(EndGame());
        } else if (enemyHP <= 0) {
            txtTurn.text = "You won";
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame() {
        gameEnd = true;
        btnFight.gameObject.SetActive(false);

        yield return new WaitForSeconds(2.0f);
		//public load scene for testing purpouse
        SceneManager.LoadScene(sceneName1);

    }
}

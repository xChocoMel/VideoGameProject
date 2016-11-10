using UnityEngine;
using System.Collections;

public class EnemyPatrolling : MonoBehaviour {

	public Transform[] checkpointsArr;
	private int currentCheckpoint;
	public float treshold;
	public int speed;

    private Animator animator;

    // Use this for initialization
    void Start () {
		this.currentCheckpoint = 0;
        animator = GetComponent<Animator>();
        animator.SetBool("Move", true);
    }
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (this.checkpointsArr[this.currentCheckpoint]);
		transform.Translate (transform.forward * Time.deltaTime * this.speed, Space.World);
		float distance = Vector3.Distance (transform.position, this.checkpointsArr[this.currentCheckpoint].position);

        if (distance < this.treshold) {
			this.currentCheckpoint++;

			if (this.currentCheckpoint == this.checkpointsArr.Length) {
				this.currentCheckpoint = 0;
			}
		}
	}
}

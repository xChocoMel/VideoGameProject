using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	//change to private
	public float fillAmount;
	public Image content;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();
	
	}

	private void HandleBar(){
		content.fillAmount = fillAmount;
	}

}

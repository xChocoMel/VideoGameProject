using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    public static List<GameObject> objectsList = new List<GameObject>();
    public static List<GameObject> objectsCollected = new List<GameObject>();

    public static List<GameObject> enemiesList = new List<GameObject>();
    public static List<GameObject> enemiesDefeated = new List<GameObject>();
	public static List<GameObject> enemiesToDefeat = new List<GameObject>();

    void Start()
    {			
		foreach (GameObject itemCollectted in objectsCollected)
		{
			foreach (GameObject objectItem in objectsList)
			{
				if (itemCollectted.gameObject.name.Equals(objectItem.gameObject.name)) {
					Destroy(objectItem.gameObject);
				}
			}
		}
    }
}
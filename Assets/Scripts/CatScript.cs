using UnityEngine;
using System.Collections;

public class CatScript : MonoBehaviour {
	private CatSpawnerScript spawnerScript;
	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (14, 14);	// ignore enemy-enemy collision
		spawnerScript = GameObject.Find("CatSpawner").GetComponent<CatSpawnerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.rigidbody2D.IsAwake() && transform.position.y < -30)
		{
			gameObject.rigidbody2D.isKinematic = true;
			spawnerScript.QueueCatOrDog(gameObject);
		}
	}
}

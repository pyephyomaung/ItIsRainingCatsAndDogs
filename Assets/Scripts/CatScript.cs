using UnityEngine;
using System.Collections;

public class CatScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (14, 14);	// ignore enemy-enemy collision
		hasSpawn = false;
	}

	private bool hasSpawn;
	// Update is called once per frame
	void Update () {
		if (!hasSpawn && renderer.IsVisibleFrom(Camera.main))
		{
			Spawn();
		}

		else if (hasSpawn && !renderer.IsVisibleFrom(Camera.main) && transform.position.y < 0)
		{
			Destroy(gameObject);
		}
	}

	void Spawn ()
	{
		hasSpawn = true;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Ground") {
			Destroy(gameObject);
		}
	}
}

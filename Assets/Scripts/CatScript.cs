using UnityEngine;
using System.Collections;

public class CatScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		hasSpawn = false;
	}

	private bool hasSpawn;
	// Update is called once per frame
	void Update () {
		if (!hasSpawn && renderer.IsVisibleFrom(Camera.main))
		{
			Spawn();
		}

		else if (hasSpawn && !renderer.IsVisibleFrom(Camera.main))
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

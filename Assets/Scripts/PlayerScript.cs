using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed = 0.1f;
	public GUIText debugText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0)
		{
			switch (Input.GetTouch(0).phase) {
			case TouchPhase.Began:
				rigidbody2D.gravityScale = 0;
				break;
			case TouchPhase.Moved:
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				if (debugText != null) debugText.text = string.Format("deltaposition x:{0} y:{1}", touchDeltaPosition.x, touchDeltaPosition.y);
				transform.Translate(touchDeltaPosition.x * speed, touchDeltaPosition.y * speed, 0);
				break;
			case TouchPhase.Ended:
				rigidbody2D.gravityScale = 1;
				break;
			}

		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			Destroy(gameObject);
		}
	}

	void OnDestroy()
	{
		// Game Over
		// Add it to the parent, as this game object is likely to be destroyed immediately
		transform.parent.gameObject.AddComponent<GameOverScript>();
	}
}

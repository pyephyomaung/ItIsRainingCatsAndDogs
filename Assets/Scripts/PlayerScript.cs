using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed = 0.035f;
	public GUIText debugText;
	public float northBound = 11f;
	public float southBound = -11f;
	public float eastBound = 6.4f;
	public float westBound = -6.4f;

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
				float xShift = touchDeltaPosition.x * speed;
				float yShift = touchDeltaPosition.y * speed;
				float newX = transform.position.x + xShift;
				float newY = transform.position.y + yShift;
				xShift = (newX >= westBound && newX <= eastBound) ? xShift : 0; 
				yShift = (newY >= southBound && newY <= northBound) ? yShift : 0; 
				transform.Translate(xShift, yShift, 0);
				break;
			case TouchPhase.Ended:
				rigidbody2D.gravityScale = 1;
				break;
			}

		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			renderer.enabled = false;
			audio.Play();
			particleSystem.Play();

			Destroy(gameObject, 0.3f);
		}
	}

	void OnDestroy()
	{

		// Game Over
		// Add it to the parent, as this game object is likely to be destroyed immediately
		transform.parent.gameObject.AddComponent<GameOverScript>();
	}
}

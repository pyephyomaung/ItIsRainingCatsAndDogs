using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed = 0.035f;
	public GUIText debugText;
	public float northBound = 11f;
	public float southBound = -11f;
	public float eastBound = 6.4f;
	public float westBound = -6.4f;
	public bool rainOnStart = false;
	public bool deathIsNotTheEnd = false;
	public GUIText scoreGUIText;

	private CatSpawnerScript spawnScript;
	private GameObject bgMusicGameObject;
	private bool isRaining = false;
	private bool isAlive = true;
	private float startTime = 0;
	public int score = 0;


	// Use this for initialization
	void Start () {
		scoreGUIText.fontSize = AppConstants.GetDefaultFontSize ();
		spawnScript = GameObject.Find("CatSpawner").GetComponent<CatSpawnerScript>();
		bgMusicGameObject = GameObject.Find("Music");

		score = 0;

		if (rainOnStart && !isRaining) {
			StartRaining();
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Set the score 
		if (isRaining && isAlive && !spawnScript.IsGameOver()) score = (int)(Time.time - startTime) * 10;

		if (Input.touchCount > 0)
		{
			switch (Input.GetTouch(0).phase) {
//			case TouchPhase.Began:
//				rigidbody2D.gravityScale = 0;
//				break;
			case TouchPhase.Moved:

				if (!isRaining) {
					StartRaining();
				}

				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				//if (debugText != null) debugText.text = string.Format("deltaposition x:{0} y:{1}", touchDeltaPosition.x, touchDeltaPosition.y);
				float xShift = touchDeltaPosition.x * speed;
				float yShift = touchDeltaPosition.y * speed;
				float newX = transform.position.x + xShift;
				float newY = transform.position.y + yShift;
				xShift = (newX >= westBound && newX <= eastBound) ? xShift : 0; 
				yShift = (newY >= southBound && newY <= northBound) ? yShift : 0; 
				transform.Translate(xShift, yShift, 0);
				break;
//			case TouchPhase.Ended:
//				rigidbody2D.gravityScale = 1;
//				break;
			}

		}
	}

	void OnGUI() {
		// Set the score text.
		scoreGUIText.text = "Score: " + score;
	}

	public int GetScore() {
		return this.score;
	}

	void StartRaining() {
		spawnScript.RepeatSpawn();
		isRaining = true;
		startTime = Time.time;
		bgMusicGameObject.GetComponent<AudioSource>().Play ();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			GetComponent<Renderer>().enabled = deathIsNotTheEnd ? true : false;
			GetComponent<AudioSource>().Play();
			GetComponent<ParticleSystem>().Play();
			if (!deathIsNotTheEnd) {
				Destroy(gameObject,0.2f);
			}
		}
	}

	// custom method on stage cleared
	public void OnStageCleared() {
		spawnScript.GameOver ();
		GameOverScript.CreateComponent (transform.parent.gameObject, score, true);
	}

	void OnDestroy()
	{
		isAlive = false;
		// Game Over
		spawnScript.GameOver ();
		// Add it to the parent, as this game object is likely to be destroyed immediately
		GameOverScript.CreateComponent (transform.parent.gameObject, score, false);
		if (bgMusicGameObject != null) bgMusicGameObject.GetComponent<AudioSource>().Stop ();
	}
}

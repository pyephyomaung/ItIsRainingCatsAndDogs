using UnityEngine;
using System.Collections;

public class CatSpawnerScript : MonoBehaviour
{
	public GameObject cat;

	private float spawnTime = 0.7f;		// The amount of time between each spawn.
	private float spawnDelay = 2f;		// The amount of time before spawning starts.
	private float[] spawnPosXs = new float[] { -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6 };
	private int patternIndex = 0;
	
	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}
	
	
	void Spawn ()
	{
		var spawnPos = transform.position;
		string pattern = getPattern();
		for (int i = 0; i < spawnPosXs.Length; i++) {
			if (pattern[i] == '1') {
				var spawnPosX = spawnPosXs[i];
				spawnPos.x = spawnPosX;
				spawnPos.y = spawnPos.y + ((i % 2 == 0)? 0f : 3f);
				Instantiate(cat, spawnPos, transform.rotation);
			}
		}

		// Play the spawning effect from all of the particle systems.
//		foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
//		{
//			p.Play();
//		}

		//	update pattern pattern for next spawn

	}

	string getPattern() {
		string[] patterns = new string[] { 
			"11111   11111",
			"11111   11111",
			"11111   11111",
			"1111  1  1111",
			"1111  11  111",
			"111  1111  11",
			"111  11111  1",
			"111        11",
			"111111   1111",
			"1111111  1111",
			"111111  11111",
			"111111  11111",
			"11      11111",
			"1  1111111111",
			"1      111111",
			"1111   111111",
			"11111   11111",
		};

		var currentPattern = patterns [patternIndex];
		patternIndex = (patternIndex + 1) % patterns.Length;
		return currentPattern;
	}
}
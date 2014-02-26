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
		// instantiate cats according to the pattern
		string pattern = getPattern();
		for (int i = 0; i < spawnPosXs.Length; i++) {
			if (pattern[i] == '1') {
				var spawnPosX = spawnPosXs[i];
				var spawnPos = transform.position;
				spawnPos.x = spawnPosX;
				spawnPos.y = spawnPos.y;
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
			"1 1 1   1 1 1",
			"1 1 1   1 1 1",
			"1 1 1   1 1 1",
			"11 1  1  1  1",
			"11 1  11  1 1",
			"1 1  1111  11",
			"1 1  11111   ",
			"1 1 1  11  11",
			"1 1 11   1 11",
			"1 1 1 1  11 1",
			"1 11 1  11 11",
			"1 11 1  11 11",
			"11      1 1 1",
			"1 1    1 1 11",
			"11 11  11 1 1",
			"11 1   1 11 1",
			"1 1 1   111 1",
		};

		var currentPattern = patterns [patternIndex];
		patternIndex = (patternIndex + 1) % patterns.Length;
		return currentPattern;
	}
}
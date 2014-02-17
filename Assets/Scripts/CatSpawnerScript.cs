using UnityEngine;
using System.Collections;

public class CatSpawnerScript : MonoBehaviour
{
	public GameObject cat;

	private float spawnTime = 0.5f;		// The amount of time between each spawn.
	private float spawnDelay = 3f;		// The amount of time before spawning starts.
	private float[] spawnPosXs = new float[] { -10, -8, -6, -4, -2, 0, 2, 4, 6, 8, 10 };
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
				Instantiate(cat, spawnPos, transform.rotation);
			}
		}

		// Play the spawning effect from all of the particle systems.
		foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}

		//	update pattern pattern for next spawn

	}

	string getPattern() {
		string[] patterns = new string[] { 
			"10100000000",
			"10101000000",
			"10101010000",
			"10101010100",
			"10101010101",
			"10101001010",
			"10100101010",
			"10010101010",
			"01010101010",
			"10101010101",
		};

		var currentPattern = patterns [patternIndex];
		patternIndex = (patternIndex + 1) % patterns.Length;
		return currentPattern;
	}
}
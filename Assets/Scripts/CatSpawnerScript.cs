﻿using UnityEngine;
using System.Collections;

public class CatSpawnerScript : MonoBehaviour
{
	public GameObject[] catsAndDogs;

	private float spawnTime = 0.3f;		// The amount of time between each spawn.
	private float spawnDelay = 0.5f;		// The amount of time before spawning starts.
	private float[] spawnPosXs = new float[] { -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6 };
	private int patternIndex = 0;

	// to be called the first time touch started by playerScript
	public void RepeatSpawn() {
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void  Spawn ()
	{   
		Vector3 spawnPos = transform.position;
		float gap = 4.0f;

		string pattern = getPattern();
		string msg = "";
		for (int i = 0; i < spawnPosXs.Length; i++) {
			if (pattern[i] == '1' || pattern[i] == 'x') {
				var spawnPosX = spawnPosXs[i];
				spawnPos.x = spawnPosX + Random.Range(-0.2f, 0.2f);
				spawnPos.y = spawnPos.y + (pattern[i] == 'x' ? 0 : Random.Range(-0.6f, 0.6f));	// less variation if pavement
				var randIndex = Random.Range (0, catsAndDogs.Length);
				Instantiate(catsAndDogs[randIndex], spawnPos, transform.rotation);
				msg += spawnPos.y  + " ";
			}
		}	
		print (msg);
	

		// Play the spawning effect from all of the particle systems.
//		foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
//		{
//			p.Play();
//		}

		//	update pattern pattern for next spawn

	}


	string getPattern() {
		string[] patterns = new string[] { 
			"1 1 1 x   x 1",
			" 1 1 x   x 1 ",
			"1 1 x   x 1 1",
			" 1 x   x 1 1 ",
			"1 x   x 1 1 1",
			" x   x 1 1 1 ",
			"1 x   x 1 1 1",
			" 1 x   x 1 1 ",
			"1 1 x   x 1 1",
			" 1 1 x   x 1 ",
			"1 1 x   x 1 1",
			" 1 x   x 1,1 ",
			"1 x   x 1 1 1",
			" x   x 1 1 1 ",
			"x   x 1 1 1 1",
			" x   x 1 1 1 ",
			"1 x   x 1 1 1",
			" 1 x   x 1 1 ",
			"1 1 x   x 1 1",
			" 1 1 x   x 1 ",
			"1 1 1 x   x 1",
			" 1 1 1 x   x ",
			"1 1 1 x   x 1",
			" 1 1 x   x 1 ",
			"1 1 x   x 1 1",
			" 1 x   x 1 1 ",
			"1 x   x 1 1 1",
			" x   x 1 1 1 ",
			"x   x 1 1 1 1",
			" x   x 1 1 1 ",
			"1 x   x 1 1 1",
			" 1 x   x 1 1 ",
			"1 1 x   x 1 1",
			" 1 1 x   x 1 ",
			"1 1 1 x   x 1",
			" 1 1 1 x   x ",
			"1 1 1 x   x 1",
			" 1 1 x   x 1 ",
			"1 1 x   x 1 1",
			" 1 x   x 1 1 ",
			"1 x   x 1 1 1",
			" x   x 1 1 1 ",
			"x   x 1 1 1 1",
			" x   x 1 1 1 ",
			"1 x   x 1 1 1",
			" 1 x   x 1 1 ",
			"1 1 x   x 1 1",
			" 1 1 x   x 1 ",
			"1 1 1 x   x 1",
			" 1 1 1 x   x ",
			"1 1 1 1 x   x",
			" 1 1 1 x   x ",
			"1 1 1 x   x 1",
			" 1 1 x   x 1 ",
			"             ",
			"             ",
			"    1 1 1 1 1",
			" 1   1 1 1 1 ",
			"1 1   1 1 1 1",
			" 1 1   1 1 1 ",
			"1 1 1   1 1 1",
			" 1 1 1       ",
			"1 1 1 1 1   1",
			" 1 1 1 1 1   ",
			"1 1 1 1 1   1",
			" 1 1 1 1   1 ",
			"1 1 1 1   1 1",
			"         1 1 ",
			"    1 1 1 1 1",
			" 1   1 1 1 1 ",
			"1 1   1 1 1 1",
			" 1 1   1 1 1 ",
			"1 1 1   1 1 1",
			" 1 1 1       ",
			"1 1 1 1 1   1",
			" 1 1 1 1 1   ",
			"1 1 1 1 1   1",
			" 1 1 1 1   1 ",
			"1 1 1 1   1 1",
			"         1 1 ",
			"    1 1 1 1 1",
			" 1   1 1 1 1 ",
			"1 1   1 1 1 1",
			" 1 1   1 1 1 ",
			"1 1 1   1 1 1",
			" 1 1 1       ",
			"1 1 1 1 1   1",
			" 1 1 1 1 1   ",
			"1 1 1 1 1   1",
			" 1 1 1 1   1 ",
			"1 1 1 1   1 1",
			"         1 1 ",
			"    1 1 1 1 1",
			" 1   1 1 1 1 ",
			"1 1   1 1 1 1",
			" 1 1   1 1 1 ",
			"1 1 1   1 1 1",
			" 1 1 1       ",
			"1 1 1 1 1   1",
			" 1 1 1 1 1   ",
			"1 1 1 1 1   1",
			" 1 1 1 1   1 ",
			"1 1 1 1   1 1",
			"         1 1 ",
			"    1 1 1 1 1",
			" 1   1 1 1 1 ",
			"1 1   1 1 1 1",
			" 1 1   1 1 1 ",
			"1 1 1   1 1 1",
			" 1 1 1       ",
			"1 1 1 1 1   1",
			" 1 1 1 1 1   ",
			"1 1 1 1 1   1",
			" 1 1 1 1   1 ",
			"1 1 1 1   1 1",
			"         1 1 ",
		};

		var currentPattern = patterns [patternIndex];
		patternIndex = (patternIndex + 1) % patterns.Length;
		return currentPattern;
	}
}
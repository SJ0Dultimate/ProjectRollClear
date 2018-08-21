using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPoolSur : MonoBehaviour {

	public int blockPoolSize = 5;
	public GameObject blockPrefab;
	public float spawnRate = 4f;
	public float blockMin;
	public float blockMax;

	private GameObject[] blocks;
	private Vector2 objectPoolPosition = new Vector2 (-15f, -25f);
	private float timeSinceLastSpawned;
	private float spawnXPosition = 20f;
	private int currentblock = 0;

	// Use this for initialization
	void Start () {
		blocks = new GameObject[blockPoolSize];
		for (int i = 0; i < blockPoolSize; i++) {
			blocks [i] = (GameObject)Instantiate (blockPrefab, objectPoolPosition, Quaternion.identity);
		}
	}

	// Update is called once per frame
	void Update () {
		timeSinceLastSpawned += Time.deltaTime;

		if (GameController.instance.gameOver == false && timeSinceLastSpawned >= spawnRate) {
			timeSinceLastSpawned = 0;
			float spawnYPosition = Random.Range (blockMin, blockMax);
			blocks [currentblock].transform.position = new Vector2 (spawnXPosition, spawnYPosition);
			currentblock++;
			if (currentblock >= blockPoolSize) {
				currentblock = 0;
			}
		}
	}
}

using NaughtyAttributes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MapGenerator : MonoBehaviour
{
	[SerializeField, BoxGroup("Generator Settings")] private int seed = 0;
	[SerializeField, BoxGroup("Generator Settings")] private bool generateRandomSeed = false;
	[Space]
	[SerializeField, BoxGroup("Generator Settings")] private int mapWidth = 10;
	[SerializeField, BoxGroup("Generator Settings")] private int mapHeight = 10;
	[Space]
	[SerializeField, BoxGroup("Generator Settings")] private int chunkWidth = 25;
	[SerializeField, BoxGroup("Generator Settings")] private int chunkHeight = 25;
	[Space]
	[SerializeField, BoxGroup("Generator Settings")] private float noiseScale = 1.0f;
	[Space]
	[SerializeField, BoxGroup("Generator Settings")] private Vector2 noiseOffset;

	[SerializeField, BoxGroup("Tile Prefabs")] private Transform chunksParent = default;
	[SerializeField, BoxGroup("Tile Prefabs")] private List<TerrainLevel> terrains = new List<TerrainLevel>();
	[Space]
	[SerializeField, BoxGroup("Tile Prefabs")] private Transform resourceChunksParent = default;
	[SerializeField, BoxGroup("Tile Prefabs")] private List<Resource> resources = new List<Resource>();

	[SerializeField, BoxGroup("Runtime References")] private List<GameObject> tilesInScene = new List<GameObject>();
	[Space]
	[SerializeField, BoxGroup("Runtime References")] private List<GameObject> resourcesInScene = new List<GameObject>();

	void Start()
	{
		Generate();
	}

	[Button]
	private void Generate()
	{
		CleanUp();

		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();

		if (generateRandomSeed) seed = Random.Range(0, int.MaxValue);
		Random.InitState(seed);


		Vector2 startNoiseOffset = new Vector2(Random.Range(0, 1000), Random.Range(0, 1000));
		// Generate the map
		for (int x = 0; x < mapWidth; x++)
		{
			for (int y = 0; y < mapHeight; y++)
			{
				GameObject chunkParent = new GameObject($"Chunk[{x}][{y}]");
				chunkParent.transform.position = new Vector3(x * chunkWidth, y * chunkHeight, 0);
				chunkParent.transform.parent = chunksParent.transform;

				GameObject resourceChunkParent = new GameObject($"Resource Chunk[{x}][{y}]");
				resourceChunkParent.transform.position = new Vector3(x * chunkWidth, y * chunkHeight, 0);
				resourceChunkParent.transform.parent = resourceChunksParent.transform;

				noiseOffset = new Vector2(startNoiseOffset.x + x, startNoiseOffset.y + y);
				float[,] mainLevelMap = GeneratePerlinNoiseMap();
				GenerateChunkFromPerlinNoiseMap(mainLevelMap, chunkParent.transform, resourceChunkParent.transform);
			}
		}

		stopwatch.Stop();
		Debug.Log($"<color=lime>Level Generation took {stopwatch.ElapsedMilliseconds}ms</color>");
	}

	// Generates a map using Perlin noise
	private float[,] GeneratePerlinNoiseMap()
	{
		// Create an empty map
		float[,] map = new float[chunkWidth, chunkHeight];

		// Loop through each point on the map
		for (int x = 0; x < chunkWidth; x++)
		{
			for (int y = 0; y < chunkHeight; y++)
			{
				// Calculate the x and y coordinates of this point in the noise space
				float xCoord = (float)x / chunkWidth * noiseScale + noiseOffset.x;
				float yCoord = (float)y / chunkHeight * noiseScale + noiseOffset.y;

				// Generate the Perlin noise value at this point
				map[x, y] = Mathf.Clamp(Mathf.PerlinNoise(xCoord, yCoord), 0, 1);
			}
		}

		// Return the generated map
		return map;
	}

	// Instantiates the map tiles based on the perlin noise map
	private void GenerateChunkFromPerlinNoiseMap(float[,] noiseMap, Transform terrainParent, Transform resourcesParent)
	{
		for (int x = 0; x < noiseMap.GetLength(0); x++)
		{
			for (int y = 0; y < noiseMap.GetLength(1); y++)
			{
				// Spawn map Terrain Tiles
				float height = noiseMap[x, y];
				Vector2 newTilePosition = new Vector2(terrainParent.position.x + x, terrainParent.position.y + y);
				foreach (TerrainLevel terrain in terrains)
				{
					if (height < terrain.heightMin || height > terrain.heightMax) continue;

					tilesInScene.Add(Instantiate(terrain.prefabs.GetRandom(), newTilePosition, Quaternion.identity, terrainParent));
				}

				if (x % 2 == 1 || y % 2 == 1) continue;

				// Spawn map Resources
				int spawnChance = Random.Range(0, 100);
				Vector2 spawnOffset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

				if (spawnChance > 75) continue;

				foreach (Resource resource in resources)
				{
					if (height < resource.heightMin || height > resource.heightMax) continue;

					resourcesInScene.Add(Instantiate(resource.prefabs.GetRandom(), newTilePosition + spawnOffset, Quaternion.identity, resourcesParent));
				}
			}
		}
	}

	// Cleans the scene
	[Button]
	private void CleanUp()
	{
		ClearLog();
		foreach (Transform tileInScene in chunksParent.GetComponentsInChildren<Transform>())
		{
			if (tileInScene != chunksParent && tileInScene != null)
			{
				DestroyImmediate(tileInScene.gameObject);
			}
		}

		foreach (Transform resourceInScene in resourceChunksParent.GetComponentsInChildren<Transform>())
		{
			if (resourceInScene != resourceChunksParent && resourceInScene != null)
			{
				DestroyImmediate(resourceInScene.gameObject);
			}
		}

		tilesInScene.Clear();
		resourcesInScene.Clear();
	}

	// Clears the Unity Editor Console
	public void ClearLog()
	{
#if UNITY_EDITOR
		var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
		var type = assembly.GetType("UnityEditor.LogEntries");
		var method = type.GetMethod("Clear");
		method.Invoke(new object(), null);
#endif
	}
}

[System.Serializable]
public struct TerrainLevel
{
	public string name;
	public float heightMin;
	public float heightMax;
	public WeightedRandomList<GameObject> prefabs;
}
[System.Serializable]
public struct Resource
{
	public string name;
	public float heightMin;
	public float heightMax;
	public WeightedRandomList<GameObject> prefabs;
}
using NaughtyAttributes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MapGenerator : MonoBehaviour
{
	[SerializeField, Foldout("Generator Settings")] private int seed = 0;
	[SerializeField, Foldout("Generator Settings")] private bool generateRandomSeed = false;
	[Space]
	[SerializeField, Foldout("Generator Settings")] private int mapWidth = 100;
	[SerializeField, Foldout("Generator Settings")] private int mapHeight = 100;
	[Space]
	[SerializeField, Foldout("Generator Settings")] private float noiseScale = 1.0f;
	[Space]
	[SerializeField, Foldout("Generator Settings")] private Vector2 noiseOffset;

	[SerializeField, Foldout("Tile Prefabs")] private Transform tilesParent = default;
	[SerializeField, Foldout("Tile Prefabs")] private List<TerrainLevel> terrains = new List<TerrainLevel>();
	[Space]
	[SerializeField, Foldout("Tile Prefabs")] private Transform resourcesParent = default;
	[SerializeField, Foldout("Tile Prefabs")] private List<Resource> resources = new List<Resource>();

	[SerializeField, Foldout("Runtime References")] private List<GameObject> tilesInScene = new List<GameObject>();
	[Space]
	[SerializeField, Foldout("Runtime References")] private List<GameObject> resourcesInScene = new List<GameObject>();


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

		// Generate the map
		float[,] mainLevelMap = GeneratePerlinNoiseMap();
		GenerateLevelFromPerlinNoiseMap(mainLevelMap);

		stopwatch.Stop();
		Debug.Log($"<color=lime>Level Generation took {stopwatch.ElapsedMilliseconds}ms</color>");
	}

	// Generates a map using Perlin noise
	private float[,] GeneratePerlinNoiseMap()
	{
		// Create an empty map
		float[,] map = new float[mapWidth, mapHeight];

		// Loop through each point on the map
		for (int x = 0; x < mapWidth; x++)
		{
			for (int y = 0; y < mapHeight; y++)
			{
				// Calculate the x and y coordinates of this point in the noise space
				float xCoord = (float)x / mapWidth * noiseScale + noiseOffset.x;
				float yCoord = (float)y / mapHeight * noiseScale + noiseOffset.y;

				// Generate the Perlin noise value at this point
				map[x, y] = Mathf.Clamp(Mathf.PerlinNoise(xCoord, yCoord), 0, 1);
			}
		}

		// Return the generated map
		return map;
	}

	// Instantiates the map tiles based on the perlin noise map
	private void GenerateLevelFromPerlinNoiseMap(float[,] noiseMap)
	{
		for (int x = 0; x < noiseMap.GetLength(0); x++)
		{
			for (int y = 0; y < noiseMap.GetLength(1); y++)
			{
				// Spawn map Terrain Tiles
				float height = noiseMap[x, y];
				Vector2 newTilePosition = new Vector2(x, y);
				foreach (TerrainLevel terrain in terrains)
				{
					if (height >= terrain.heightMin && height <= terrain.heightMax)
					{
						tilesInScene.Add(Instantiate(terrain.prefabs.GetRandom(), newTilePosition, Quaternion.identity, tilesParent));
					}
				}

				if (x % 2 == 1 || y % 2 == 1) continue;

				// Spawn map Resources
				int spawnChance = Random.Range(0, 100);
				Vector2 spawnOffset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

				if (spawnChance > 75) continue;

				foreach (Resource resource in resources)
				{
					if (height >= resource.heightMin && height <= resource.heightMax)
					{
						resourcesInScene.Add(Instantiate(resource.prefabs.GetRandom(), newTilePosition + spawnOffset, Quaternion.identity, resourcesParent));
					}
				}
			}
		}
	}

	// Cleans the scene
	[Button]
	private void CleanUp()
	{
		ClearLog();
		foreach (Transform tileInScene in tilesParent.GetComponentsInChildren<Transform>())
		{
			if (tileInScene != tilesParent && tileInScene != null)
			{
				DestroyImmediate(tileInScene.gameObject);
			}
		}

		foreach (Transform resourceInScene in resourcesParent.GetComponentsInChildren<Transform>())
		{
			if (resourceInScene != resourcesParent && resourceInScene != null)
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
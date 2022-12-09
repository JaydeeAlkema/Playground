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
	[SerializeField, BoxGroup("Generator Settings")] private int mapWidth = 100;
	[SerializeField, BoxGroup("Generator Settings")] private int mapHeight = 100;
	[Space]
	[SerializeField, BoxGroup("Generator Settings")] private float noiseScale = 1.0f;
	[Space]
	[SerializeField, BoxGroup("Generator Settings")] private Vector2 noiseOffset;

	[Space(10)]

	[SerializeField, BoxGroup("Tile Prefabs")] private Transform tilesParent = default;
	[SerializeField, BoxGroup("Tile Prefabs")] private WeightedRandomList<GameObject> waterTilePrefabs = new WeightedRandomList<GameObject>();
	[SerializeField, BoxGroup("Tile Prefabs")] private WeightedRandomList<GameObject> lightGrassTilePrefabs = new WeightedRandomList<GameObject>();
	[SerializeField, BoxGroup("Tile Prefabs")] private WeightedRandomList<GameObject> darkGrassTilePrefabs = new WeightedRandomList<GameObject>();

	[Space(10)]

	[SerializeField, BoxGroup("Tile Prefabs")] private Transform resourcesParent = default;
	[SerializeField, BoxGroup("Resources")] private WeightedRandomList<GameObject> largeTreePrefabs = new WeightedRandomList<GameObject>();
	[SerializeField, BoxGroup("Resources")] private WeightedRandomList<GameObject> smallTreePrefabs = new WeightedRandomList<GameObject>();
	[Space]
	[SerializeField, BoxGroup("Resources")] private WeightedRandomList<GameObject> largeBushPrefabs = new WeightedRandomList<GameObject>();
	[SerializeField, BoxGroup("Resources")] private WeightedRandomList<GameObject> smallBushPrefabs = new WeightedRandomList<GameObject>();

	[Space(10)]

	[SerializeField, BoxGroup("Runtime References")] private List<GameObject> walkableTiles = new List<GameObject>();
	[SerializeField, BoxGroup("Runtime References")] private List<GameObject> unwalkableTiles = new List<GameObject>();
	[Space]
	[SerializeField, BoxGroup("Runtime References")] private List<GameObject> resources = new List<GameObject>();


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

		// Generate the Resources
		float[,] resourcesMap = GeneratePerlinNoiseMap();
		GenerateLevelResources(mainLevelMap);

		stopwatch.Stop();
		Debug.Log($"<color=lime>Level Generation took {stopwatch.ElapsedMilliseconds}ms</color>");
	}

	// Generates a map using Perlin noise
	private float[,] GeneratePerlinNoiseMap()
	{
		// Create an empty map
		float[,] map = new float[mapWidth, mapHeight];
		float randX = Random.Range(-1000, 1000);
		float randY = Random.Range(-1000, 1000);
		noiseOffset = new Vector2(randX, randY);

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
				float height = noiseMap[x, y];
				Vector2 newTilePosition = new Vector2(x, y);


				if (height >= 0.0f && height <= 0.3f)
				{
					unwalkableTiles.Add(Instantiate(waterTilePrefabs.GetRandom(), newTilePosition, Quaternion.identity, tilesParent));
				}
				else if (height >= 0.3f && height <= 0.7f)
				{
					walkableTiles.Add(Instantiate(lightGrassTilePrefabs.GetRandom(), newTilePosition, Quaternion.identity, tilesParent));
				}
				else if (height >= 0.7f && height <= 1.0f)
				{
					walkableTiles.Add(Instantiate(darkGrassTilePrefabs.GetRandom(), newTilePosition, Quaternion.identity, tilesParent));
				}
			}
		}
	}

	private void GenerateLevelResources(float[,] noiseMap)
	{
		for (int x = 0; x < noiseMap.GetLength(0); x++)
		{
			for (int y = 0; y < noiseMap.GetLength(1); y++)
			{
				float height = noiseMap[x, y];
				Vector2 newTilePosition = new Vector2(x, y);

				int spawnChance = Random.Range(0, 100);
				Vector2 spawnOffset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

				if (spawnChance > 15) continue;

				if (height >= 0.4f && height <= 0.6f)
				{
					int rand = Random.Range(0, 2);
					if (rand == 0)
						resources.Add(Instantiate(smallTreePrefabs.GetRandom(), newTilePosition + spawnOffset, Quaternion.identity, resourcesParent));
					else
						resources.Add(Instantiate(smallBushPrefabs.GetRandom(), newTilePosition + spawnOffset, Quaternion.identity, resourcesParent));
				}
				else if (height >= 0.7f && height <= 1f)
				{
					int rand = Random.Range(0, 2);
					if (rand == 0)
						resources.Add(Instantiate(largeTreePrefabs.GetRandom(), newTilePosition + spawnOffset, Quaternion.identity, resourcesParent));
					else
						resources.Add(Instantiate(largeBushPrefabs.GetRandom(), newTilePosition + spawnOffset, Quaternion.identity, resourcesParent));
				}
			}
		}
	}

	// Cleans the scene
	[Button]
	private void CleanUp()
	{
		ClearLog();
		Transform[] tilesInScene = tilesParent.GetComponentsInChildren<Transform>();
		foreach (Transform tileInScene in tilesInScene)
		{
			if (tileInScene != tilesParent && tileInScene != null)
			{
				DestroyImmediate(tileInScene.gameObject);
			}
		}

		Transform[] resourcesInScene = resourcesParent.GetComponentsInChildren<Transform>();
		foreach (Transform resourceInScene in resourcesInScene)
		{
			if (resourceInScene != resourcesParent && resourceInScene != null)
			{
				DestroyImmediate(resourceInScene.gameObject);
			}
		}

		unwalkableTiles.Clear();
		walkableTiles.Clear();
		resources.Clear();
	}

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
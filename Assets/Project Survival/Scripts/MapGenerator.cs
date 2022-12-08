using NaughtyAttributes;
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

	// The generated map
	private float[,] map;

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
		map = GeneratePerlinNoiseMap();
		GenerateLevelFromPerlinNoiseMap();

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
				map[x, y] = Mathf.PerlinNoise(xCoord, yCoord);
			}
		}

		// Return the generated map
		return map;
	}

	// Instantiates the map tiles based on the perlin noise map
	private void GenerateLevelFromPerlinNoiseMap()
	{
		for (int x = 0; x < map.GetLength(0); x++)
		{
			for (int y = 0; y < map.GetLength(1); y++)
			{
				float height = map[x, y];
				Vector2 newTilePosition = new Vector2(x, y);


				if (height > 0.0f && height < 0.3f)
				{
					Instantiate(waterTilePrefabs.GetRandom(), newTilePosition, Quaternion.identity, tilesParent);
				}
				else if (height > 0.3f && height < 0.7f)
				{
					Instantiate(lightGrassTilePrefabs.GetRandom(), newTilePosition, Quaternion.identity, tilesParent);
				}
				else if (height > 0.7f && height < 1.3f)
				{
					Instantiate(darkGrassTilePrefabs.GetRandom(), newTilePosition, Quaternion.identity, tilesParent);
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
			if (tileInScene != tilesParent)
			{
				DestroyImmediate(tileInScene.gameObject);
			}
		}
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

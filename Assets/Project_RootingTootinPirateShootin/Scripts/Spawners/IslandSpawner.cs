using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTPS
{
	public class IslandSpawner : MonoBehaviour
	{
		[SerializeField] private int seed;
		[SerializeField] private GameObject[] islands;
		[SerializeField] private Vector3 spawnArea = new Vector3();
		[SerializeField] private int spawnAmount = 10;
		[SerializeField] private int collisionRadius = 25;

		private int tries = 0;
		private int maxTries = 100;
		private List<GameObject> spawnedIslands = new List<GameObject>();

		private void Start()
		{
			Random.InitState( seed );
			SpawnIslands();
		}

		void SpawnIslands()
		{
			for( int i = 0; i < spawnAmount; i++ )
			{
				Vector3 spawnPos = new Vector3( Random.Range( -spawnArea.x, spawnArea.x ), 0f, Random.Range( -spawnArea.z, spawnArea.z ) );

				foreach( GameObject island in spawnedIslands )
				{
					tries = 0;
					while( Vector3.Distance( island.transform.position, spawnPos ) <= collisionRadius && tries < maxTries )
					{
						spawnPos = new Vector3( Random.Range( -spawnArea.x, spawnArea.x ), 0f, Random.Range( -spawnArea.z, spawnArea.z ) );
						tries++;
					}
				}

				Vector3 spawnRot = new Vector3( 0, Random.Range( 0, 360 ), 0 );
				int islandIndex = Random.Range( 0, islands.Length );

				GameObject newIsland = Instantiate( islands[islandIndex], spawnPos, Quaternion.Euler( spawnRot ), this.transform );

				spawnedIslands.Add( newIsland );
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube( transform.position, spawnArea * 2f );
		}
	}
}

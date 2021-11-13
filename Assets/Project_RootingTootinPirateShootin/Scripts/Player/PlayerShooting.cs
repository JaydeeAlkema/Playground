using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTPS
{
	public class PlayerShooting : MonoBehaviour
	{
		[SerializeField] private GameObject _projectilePrefab;
		[Space]
		[SerializeField] private KeyCode _fireLeftSideKeycode = KeyCode.Mouse0;
		[SerializeField] private KeyCode _fireRightSideKeycode = KeyCode.Mouse1;
		[Space]
		[SerializeField] private Transform _leftSideFirePoint = default;
		[SerializeField] private Transform _rightSideFirePoint = default;
		[SerializeField] private Vector3 firingPoint = new Vector3();
		[Space]
		[SerializeField] private ScriptablePlayerStats _stats;

		private float _cooldownTime;

		private bool _leftSideCooldown = false;
		private bool _rightSideCooldown = false;

		private void Update()
		{
			GetInput();
		}

		private void GetInput()
		{
			if( Input.GetKey( _fireLeftSideKeycode ) && !_leftSideCooldown )
			{
				FiringPointDetector.Instance.Move( -_stats.ProjectileFireForce, 25f );
			}
			else if( Input.GetKey( _fireRightSideKeycode ) && !_rightSideCooldown )
			{
				FiringPointDetector.Instance.Move( _stats.ProjectileFireForce, 25f );
			}

			if( Input.GetKeyUp( _fireLeftSideKeycode ) )
			{
				firingPoint = FiringPointDetector.Instance.GetFiringPointHitPoint();
				FiringPointDetector.Instance.ResetPos();

				Shoot( _leftSideFirePoint );
			}
			if( Input.GetKeyUp( _fireRightSideKeycode ) )
			{
				firingPoint = FiringPointDetector.Instance.GetFiringPointHitPoint();
				FiringPointDetector.Instance.ResetPos();

				Shoot( _rightSideFirePoint );
			}
		}

		private void Shoot( Transform projectileSpawnPoint )
		{
			GameObject newProjectileGO = Instantiate( _projectilePrefab, projectileSpawnPoint.position, Quaternion.identity );

			//Quaternion q = Quaternion.FromToRotation( Vector3.up, transform.forward );
			//newProjectileGO.transform.rotation = q * projectileSpawnPoint.transform.rotation;

			newProjectileGO.GetComponent<Projectile>().SetDestination( firingPoint );

			Destroy( newProjectileGO, 15f );
		}
	}
}
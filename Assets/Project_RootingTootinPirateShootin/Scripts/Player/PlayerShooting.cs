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
		[Space]
		[SerializeField] private ScriptablePlayerStats _stats;

		private float _cooldownTime;

		private bool _leftSideCooldown = false;
		private bool _rightSideCooldown = false;

		private float _leftSideFireForce = 0f;
		private float _rightSideFireForce = 0f;

		private void Update()
		{
			GetInput();
		}

		private void GetInput()
		{
			if( Input.GetKey( _fireLeftSideKeycode ) && !_leftSideCooldown )
			{
				_leftSideFireForce += _stats.ProjectileFireForce * Time.deltaTime;
				float _totalLeftSideFireForce = Mathf.Clamp( _leftSideFireForce, 0, _stats.ProjectileMaxFireForce );

				Debug.Log( _totalLeftSideFireForce );
			}
			else if( Input.GetKey( _fireRightSideKeycode ) && !_rightSideCooldown )
			{
				_rightSideFireForce += _stats.ProjectileFireForce * Time.deltaTime;
				float _totalRightSideFireForce = Mathf.Clamp( _rightSideFireForce, 0, _stats.ProjectileMaxFireForce );

				Debug.Log( _totalRightSideFireForce );
			}

			if( Input.GetKeyUp( _fireLeftSideKeycode ) )
			{
				Shoot( _leftSideFireForce, _leftSideFirePoint );
				_leftSideFireForce = 0f;
			}
			if( Input.GetKeyUp( _fireRightSideKeycode ) )
			{
				Shoot( _rightSideFireForce, _rightSideFirePoint );
				_rightSideFireForce = 0f;
			}
		}

		private void Shoot( float force, Transform projectileSpawnPoint )
		{
			GameObject newProjectileGO = Instantiate( _projectilePrefab, projectileSpawnPoint.position, Quaternion.identity );

			Quaternion q = Quaternion.FromToRotation( Vector3.up, transform.forward );
			newProjectileGO.transform.rotation = q * projectileSpawnPoint.transform.rotation;
			newProjectileGO.GetComponent<Projectile>().Initialize( force );

			Destroy( newProjectileGO, 15f );
		}
	}
}
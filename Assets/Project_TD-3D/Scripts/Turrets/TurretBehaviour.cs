using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor;

public enum DETECTIONMODE
{
	Nearest = 1,
	Farthest = 2
}

public class TurretBehaviour : MonoBehaviour
{
	public DETECTIONMODE detectionMode = DETECTIONMODE.Nearest;
	public LayerMask detectionLayer = default;
	[Space]
	public GameObject projectilePrefab = null;
	[Required] public Transform turretHeadPivot = null;
	[Required] public Transform muzzleTransform = null;
	[Space]
	public float turretHeadRotationSpeed = 10f;
	[Range( 5, 15 )] public int detectionRange = 5;
	[Range( 0.1f, 0.5f )] public float detectionInterval = 0.1f;
	public float shootInterval = 1f;
	[Space]
	[ReadOnly] public GameObject currentTarget = default;
	[ReadOnly] public GameObject previousTarget = default;
	[Space]
	public bool showDetectionRange = false;

	public void LookAtTarget()
	{
		if( !currentTarget ) return;

		Vector3 dir = currentTarget.transform.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation( dir );
		Vector3 rotation = Quaternion.Lerp( turretHeadPivot.rotation, lookRotation, Time.deltaTime * turretHeadRotationSpeed ).eulerAngles;
		turretHeadPivot.rotation = Quaternion.Euler( 0f, rotation.y, 0f );
	}

	public void GetTarget()
	{
		StartCoroutine( GetTargetCoroutine() );
	}

	private IEnumerator GetTargetCoroutine()
	{
		while( true )
		{
			yield return new WaitForSeconds( detectionInterval );
			switch( detectionMode )
			{
				case DETECTIONMODE.Nearest:
					currentTarget = DetectNearestTarget();
					break;

				case DETECTIONMODE.Farthest:
					currentTarget = DetectFarthestTarget();
					break;

				default:
					break;
			}
			yield return null;
		}
	}

	/// <summary>
	/// Detect the Nearest Target.
	/// </summary>
	/// <returns> Nearest Target GameObject. </returns>
	private GameObject DetectNearestTarget()
	{
		GameObject nearestTarget = null;
		float dist = Mathf.Infinity;
		Collider[] collidersInRange = Physics.OverlapSphere( transform.position, detectionRange, detectionLayer );

		foreach( Collider collider in collidersInRange )
		{
			float diff = Vector3.Distance( transform.position, collider.transform.position );
			if( diff < dist )
			{
				nearestTarget = collider.gameObject;
				diff = dist;
			}
		}

		return nearestTarget;
	}

	/// <summary>
	/// Detect the Farthest Target.
	/// </summary>
	/// <returns> Farthest Target GameObject. </returns>
	private GameObject DetectFarthestTarget()
	{
		GameObject farthestTarget = null;
		float dist = 0f;
		Collider[] collidersInRange = Physics.OverlapSphere( transform.position, detectionRange, detectionLayer );

		foreach( Collider collider in collidersInRange )
		{
			float diff = Vector3.Distance( transform.position, collider.transform.position );
			if( diff > dist )
			{
				farthestTarget = collider.gameObject;
				diff = dist;
			}
		}

		return farthestTarget;
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		if( showDetectionRange )
		{
			Handles.color = Color.red;
			Handles.DrawWireDisc( transform.position, Vector3.up, detectionRange );
		}
	}
#endif
}

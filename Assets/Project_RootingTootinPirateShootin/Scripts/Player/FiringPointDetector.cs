using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPointDetector : MonoBehaviour
{
	private static FiringPointDetector instance = null;

	private Ray ray = default;
	private RaycastHit hit = default;

	[SerializeField] private bool drawGizmos = false;

	public static FiringPointDetector Instance { get => instance; set => instance = value; }

	private void Awake()
	{
		if( instance != null || instance != this )
		{
			instance = this;
		}
	}

	public Vector3 GetFiringPointHitPoint()
	{
		ray = new Ray( transform.position, Vector3.down * 1000f );

		if( Physics.Raycast( ray, out hit ) )
		{
			Debug.Log( hit.point );
			return hit.point;
		}

		return Vector3.zero;
	}

	public void Move( float value, float maxDist )
	{
		Vector3 pos = transform.localPosition;

		pos.x += value * Time.deltaTime;
		pos.x = Mathf.Clamp( pos.x, -maxDist, maxDist );

		transform.localPosition = pos;
	}

	public void ResetPos()
	{
		transform.localPosition = new Vector3( 0, 10f, 0 );
	}

	private void OnDrawGizmos()
	{
		if( drawGizmos )
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube( transform.position, Vector3.one );

			Gizmos.color = Color.red;
			Gizmos.DrawRay( transform.position, Vector3.down * 1000f );

			if( hit.point != Vector3.zero )
			{
				Gizmos.color = Color.blue;
				Gizmos.DrawSphere( hit.point, 0.25f );
			}
		}
	}
}

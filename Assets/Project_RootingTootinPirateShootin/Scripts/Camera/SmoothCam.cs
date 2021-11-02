using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTPS
{
	public class SmoothCam : MonoBehaviour
	{
		[SerializeField] private Transform target = default;
		[SerializeField] private Vector3 offset = new Vector3();
		[SerializeField] private float smoothing = 5f;

		private Vector3 desiredPos = new Vector3();
		private Vector3 smoothedPos = new Vector3();
		private Vector3 vel = new Vector3();

		private void FixedUpdate()
		{
			desiredPos = new Vector3( target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z );
			smoothedPos = Vector3.SmoothDamp( transform.position, desiredPos, ref vel, smoothing * Time.deltaTime );

			transform.position = smoothedPos;
		}
	}
}

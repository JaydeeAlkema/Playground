using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTPS
{
	[RequireComponent( typeof( Rigidbody ) )]
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] private string _horizontalInput = "Horizontal";
		[SerializeField] private string _verticalInput = "Vertical";
		[Space]
		[SerializeField] private ScriptablePlayerStats _stats = default;

		private Rigidbody _rb = default;
		private Vector2 _movementInput = new Vector2();

		float movementSpeed = 0f;
		float clampedMovementSpeed = 0f;

		float rotateForce = 0f;
		float clampedRotateForce = 0f;

		private void Start()
		{
			_rb = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			Move();
		}

		private void Move()
		{
			_movementInput = new Vector2( Input.GetAxisRaw( _horizontalInput ), Input.GetAxisRaw( _verticalInput ) );

			movementSpeed = _movementInput.y * _stats.MoveAcceleration * Time.deltaTime;
			clampedMovementSpeed = Mathf.Clamp( movementSpeed, -_stats.MaxMoveSpeed, _stats.MaxMoveSpeed );

			rotateForce = _movementInput.x * _stats.RotateAcceleration * Time.deltaTime;
			clampedRotateForce = Mathf.Clamp( rotateForce, -_stats.MaxRotateSpeed, _stats.MaxRotateSpeed );

			_rb.AddForce( transform.forward * clampedMovementSpeed );

			if( _rb.velocity.magnitude > 0.01f )
			{
				Vector3 deltaRotation = new Vector3( 0f, clampedRotateForce, 0f );
				_rb.AddRelativeTorque( deltaRotation );
			}
		}

	}
}
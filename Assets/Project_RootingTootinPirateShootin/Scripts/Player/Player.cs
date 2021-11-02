using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTPS
{
	[RequireComponent( typeof( Rigidbody ) )]
	public class Player : MonoBehaviour
	{
		[SerializeField] private string horizontalInput = "Horizontal";
		[SerializeField] private string verticalInput = "Vertical";
		[SerializeField] private string fireInput = "Fire";
		[Space]
		[SerializeField] private Stats stats = default;

		private Rigidbody rb = default;
		private Vector2 movementInput = new Vector2();
		private float actualMovementSpeed = 0f;
		private float actualRotationSpeed = 0f;

		private void Start()
		{
			rb = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			GetInputs();
		}

		private void FixedUpdate()
		{
			Move();
			Rotate();
		}

		private void GetInputs()
		{
			movementInput = new Vector2( Input.GetAxisRaw( horizontalInput ), Input.GetAxisRaw( verticalInput ) );

			// Forward Movement
			if( movementInput.y >= 1 )
			{
				actualMovementSpeed += stats.moveAcceleration * Time.deltaTime;
			}
			else
			{
				if( actualMovementSpeed > 0.01f )
				{
					actualMovementSpeed -= stats.moveAcceleration * Time.deltaTime;
				}
				else
				{
					actualMovementSpeed = 0f;
				}
			}

			// Rotation
			actualRotationSpeed += movementInput.x * stats.rotateAcceleration * Time.deltaTime;

			actualMovementSpeed = Mathf.Clamp( actualMovementSpeed, 0, stats.maxMoveSpeed );
			actualRotationSpeed = Mathf.Clamp( actualRotationSpeed, -stats.maxRotateSpeed, stats.maxRotateSpeed );
		}

		private void Move()
		{
			rb.AddForce( transform.forward * actualMovementSpeed );
		}

		private void Rotate()
		{
			rb.MoveRotation( Quaternion.Euler( 0f, actualRotationSpeed, 0f ) );
		}
	}

	[System.Serializable]
	public struct Stats
	{
		public int health;

		public float fireRate;

		public int projectileInventory;
		public int projectileRestockTime;

		public float rotateAcceleration;
		public float moveAcceleration;

		public float maxMoveSpeed;
		public float maxRotateSpeed;
	}
}
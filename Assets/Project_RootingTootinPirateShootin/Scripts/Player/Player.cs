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
		private float movementSpeed = 0f;
		private float rotation = 0f;

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
				movementSpeed += stats.moveAcceleration * Time.deltaTime;
			}
			else
			{
				if( movementSpeed > 0.01f )
				{
					movementSpeed -= stats.moveAcceleration * Time.deltaTime;
				}
				else
				{
					movementSpeed = 0f;
				}
			}

			// Rotation
			rotation += movementInput.x * stats.rotateAcceleration * Time.deltaTime;

			movementSpeed = Mathf.Clamp( movementSpeed, 0, stats.maxMoveSpeed );
		}

		private void Move()
		{
			rb.AddForce( transform.forward * movementSpeed );
		}

		private void Rotate()
		{
			rb.MoveRotation( Quaternion.Euler( 0f, transform.rotation.y + rotation, 0f ) );
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
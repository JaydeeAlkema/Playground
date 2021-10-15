using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace project_starvation
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private float moveSpeed;

		private float horizontalInput;
		private float verticalInput;

		private Vector2 movementDir;

		private Rigidbody2D rb2d;

		private void Awake()
		{
			rb2d = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			movementDir = new Vector2( horizontalInput, verticalInput );
		}

		private void FixedUpdate()
		{
			rb2d.MovePosition( rb2d.position + movementDir * moveSpeed * Time.fixedDeltaTime);
		}

		public void OnMoveInput( float horizontal, float vertical )
		{
			horizontalInput = horizontal;
			verticalInput = vertical;
			Debug.Log( $"Player Controller: Move Input: {vertical}, {horizontal}" );
		}
	}
}

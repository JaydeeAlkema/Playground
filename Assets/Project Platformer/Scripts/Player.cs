using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project_Platformer
{
	public class Player : MonoBehaviour
	{
		[SerializeField, BoxGroup("Movement Variables")] private float _moveSpeed = 5f;
		[SerializeField, BoxGroup("Movement Variables")] private float _jumpForce = 8f;
		[Space]
		[SerializeField, BoxGroup("Movement Variables")] private LayerMask _groundedMask = default;
		[SerializeField, BoxGroup("Movement Variables")] private float _groundedRayLength = 0.25f;

		[SerializeField, BoxGroup("References")] private SpriteRenderer _spriteRenderer = default;
		[SerializeField, BoxGroup("References")] private Transform _groundedCheckTransform = default;

		[SerializeField, BoxGroup("References")] private Rigidbody2D rb2d = default;
		[SerializeField, BoxGroup("References")] private Animator animator;

		private ProjectPlatformerControls projectPlatformerInputActions = default;

		#region Unity Callbacks
		private void Awake()
		{
			projectPlatformerInputActions = new ProjectPlatformerControls();
		}
		private void OnEnable()
		{
			projectPlatformerInputActions.PlayerMovement.GroundedMove.performed += Move;
			projectPlatformerInputActions.PlayerMovement.GroundedMove.Enable();

			projectPlatformerInputActions.PlayerMovement.Jump.started += Jump;
			projectPlatformerInputActions.PlayerMovement.Jump.Enable();

			projectPlatformerInputActions.PlayerMovement.Attack.started += Attack;
			projectPlatformerInputActions.PlayerMovement.Attack.Enable();
		}
		private void OnDisable()
		{
			projectPlatformerInputActions.PlayerMovement.GroundedMove.Disable();
			projectPlatformerInputActions.PlayerMovement.Jump.Disable();
		}
		private void Update()
		{
			SetAnimatorStates();
		}
		#endregion

		#region Input Action Callbacks
		private void Move(InputAction.CallbackContext context)
		{
			Vector2 inputAxis = context.ReadValue<Vector2>();
			rb2d.velocity = new Vector2(inputAxis.x * _moveSpeed, rb2d.velocity.y);

			FlipSprite(inputAxis);
		}
		private void Jump(InputAction.CallbackContext context)
		{
			if (!IsGrounded()) return;
			rb2d.velocity = new Vector2(rb2d.velocity.x, _jumpForce);
		}
		private void Attack(InputAction.CallbackContext context)
		{
			animator.SetTrigger("Attack");
		}
		#endregion

		private void FlipSprite(Vector2 inputAxis)
		{
			float inputX = inputAxis.x;

			if (inputX >= 0.1f) _spriteRenderer.flipX = false;
			else if (inputX <= -0.1f) _spriteRenderer.flipX = true;
		}
		private bool IsGrounded()
		{
			return Physics2D.Raycast(_groundedCheckTransform.position, Vector2.down, _groundedRayLength, _groundedMask);
		}
		private bool IsMoving()
		{
			if (rb2d.velocity.x != 0) return true;
			else return false;
		}
		private bool IsJumping()
		{
			if (rb2d.velocity.y != 0) return true;
			else return false;
		}
		private void SetAnimatorStates()
		{
			animator.SetBool("Moving", IsMoving());
			animator.SetBool("Jumping", IsJumping());
			animator.SetBool("Grounded", IsGrounded());
			animator.SetFloat("Magnitude X", rb2d.velocity.magnitude / _moveSpeed);
			animator.SetFloat("Velocity Y", rb2d.velocity.y);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawRay(_groundedCheckTransform.position, Vector3.down * _groundedRayLength);
		}
	}
}
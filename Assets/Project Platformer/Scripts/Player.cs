using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project_Platformer
{
	public class Player : MonoBehaviour
	{
		[SerializeField, Foldout("Movement Variables")] private float moveSpeed = 2f;
		[SerializeField, Foldout("Movement Variables")] private float jumpForce = 8f;
		[Space]
		[SerializeField, Foldout("Movement Variables")] private LayerMask groundedMask = default;
		[SerializeField, Foldout("Movement Variables")] private float groundedRadius = 0.25f;

		[SerializeField, Foldout("References")] private SpriteRenderer spriteRenderer = default;
		[SerializeField, Foldout("References")] private Transform groundedCheckTransform = default;

		private Rigidbody2D rb2d = default;
		private ProjectPlatformerControls projectPlatformerInputActions = default;
		private Animator animator;

		#region Unity Callbacks
		private void Awake()
		{
			rb2d = GetComponent<Rigidbody2D>();
			animator = GetComponentInChildren<Animator>();
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
			rb2d.velocity = new Vector2(inputAxis.x * moveSpeed, rb2d.velocity.y);

			FlipSprite(inputAxis);
		}
		private void Jump(InputAction.CallbackContext context)
		{
			if (!IsGrounded()) return;
			rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
		}
		private void Attack(InputAction.CallbackContext context)
		{
			animator.SetTrigger("Attack");
		}
		#endregion

		private void FlipSprite(Vector2 inputAxis)
		{
			switch (inputAxis.x)
			{
				case 1:
					spriteRenderer.flipX = false;
					break;
				case -1:
					spriteRenderer.flipX = true;
					break;
			}
		}
		private bool IsGrounded()
		{
			Collider2D[] collidersUnderCharacter = Physics2D.OverlapCircleAll(groundedCheckTransform.position, groundedRadius, groundedMask);

			switch (collidersUnderCharacter.Length)
			{
				case 0:
					return false;
				default:
					return true;
			}
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
			animator.SetFloat("Velocity Y", rb2d.velocity.y);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(groundedCheckTransform.position, groundedRadius);
		}
	}
}
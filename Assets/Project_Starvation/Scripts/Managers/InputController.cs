using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class MoveInputEvent : UnityEvent<float, float> { }

namespace project_starvation
{
	public class InputController : MonoBehaviour
	{
		[SerializeField] private ProjectStarvationInputActions inputActions;
		[SerializeField] private MoveInputEvent moveInputEvent;

		private void Awake()
		{
			inputActions = new ProjectStarvationInputActions();
		}

		private void OnEnable()
		{
			inputActions.Gameplay.Enable();
			inputActions.Gameplay.Movement.performed += OnMovePerformed;
			inputActions.Gameplay.Movement.canceled += OnMovePerformed;
		}

		private void OnMovePerformed( InputAction.CallbackContext context )
		{
			Vector2 moveInput = context.ReadValue<Vector2>();
			moveInputEvent.Invoke( moveInput.x, moveInput.y );
		}
	}
}

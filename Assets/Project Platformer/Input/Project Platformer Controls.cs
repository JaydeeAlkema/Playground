//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Project Platformer/Input/Project Platformer Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Project_Platformer
{
    public partial class @ProjectPlatformerControls : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @ProjectPlatformerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Project Platformer Controls"",
    ""maps"": [
        {
            ""name"": ""Player Movement"",
            ""id"": ""ed51051f-9757-463b-8ad9-0586ef426568"",
            ""actions"": [
                {
                    ""name"": ""Grounded Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ac528a95-c5ee-4b30-bc24-c2a40dcbdecd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""2da3d4a2-629e-4324-901c-4427254443e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""1a3d1567-5d2a-49e7-9779-ba4f585ce37a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""KBM"",
                    ""id"": ""9292eadd-6e9f-4a34-a634-b1efc8e39ae2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grounded Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""fab5701f-ab0f-43e6-9b0e-ac4e9c0ae4c7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Grounded Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""a97e0f05-500c-4be0-887e-086441aeb322"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Grounded Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""e9e68266-6efb-4f24-836a-ed5cf8172208"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grounded Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6dd806b1-da81-4724-b2c3-ee21e66e37b6"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Grounded Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3a2eae3e-4295-496f-bd7c-a23a796aeb5f"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Grounded Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""81ce7a8e-7122-4bfb-b574-def40e5b5e19"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0461b535-4587-47d3-b705-7741e0836f99"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57672307-414a-4a68-847a-5daaf7112a11"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""101f26f7-1914-4077-904a-d3c420a4d8c7"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<VirtualMouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Player Movement
            m_PlayerMovement = asset.FindActionMap("Player Movement", throwIfNotFound: true);
            m_PlayerMovement_GroundedMove = m_PlayerMovement.FindAction("Grounded Move", throwIfNotFound: true);
            m_PlayerMovement_Jump = m_PlayerMovement.FindAction("Jump", throwIfNotFound: true);
            m_PlayerMovement_Attack = m_PlayerMovement.FindAction("Attack", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Player Movement
        private readonly InputActionMap m_PlayerMovement;
        private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
        private readonly InputAction m_PlayerMovement_GroundedMove;
        private readonly InputAction m_PlayerMovement_Jump;
        private readonly InputAction m_PlayerMovement_Attack;
        public struct PlayerMovementActions
        {
            private @ProjectPlatformerControls m_Wrapper;
            public PlayerMovementActions(@ProjectPlatformerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @GroundedMove => m_Wrapper.m_PlayerMovement_GroundedMove;
            public InputAction @Jump => m_Wrapper.m_PlayerMovement_Jump;
            public InputAction @Attack => m_Wrapper.m_PlayerMovement_Attack;
            public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerMovementActions instance)
            {
                if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
                {
                    @GroundedMove.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnGroundedMove;
                    @GroundedMove.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnGroundedMove;
                    @GroundedMove.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnGroundedMove;
                    @Jump.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                    @Attack.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAttack;
                    @Attack.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAttack;
                    @Attack.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAttack;
                }
                m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @GroundedMove.started += instance.OnGroundedMove;
                    @GroundedMove.performed += instance.OnGroundedMove;
                    @GroundedMove.canceled += instance.OnGroundedMove;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Attack.started += instance.OnAttack;
                    @Attack.performed += instance.OnAttack;
                    @Attack.canceled += instance.OnAttack;
                }
            }
        }
        public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        public interface IPlayerMovementActions
        {
            void OnGroundedMove(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnAttack(InputAction.CallbackContext context);
        }
    }
}

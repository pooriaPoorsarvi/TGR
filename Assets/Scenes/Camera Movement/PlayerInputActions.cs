// GENERATED AUTOMATICALLY FROM 'Assets/Scenes/Camera Movement/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerAction"",
            ""id"": ""c7c08b07-5cba-4b50-abb5-5c01afc3d36b"",
            ""actions"": [
                {
                    ""name"": ""RS"",
                    ""type"": ""PassThrough"",
                    ""id"": ""64714f2c-f5c8-482b-b966-62bddcae8836"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LB"",
                    ""type"": ""Button"",
                    ""id"": ""e63b093f-9726-4b85-bb74-3de509865ba9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RB"",
                    ""type"": ""Button"",
                    ""id"": ""6813115e-cb58-447f-b5be-f4fdd08e7e01"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LT"",
                    ""type"": ""Button"",
                    ""id"": ""b3a198d6-6078-4cff-84b1-2771c1500724"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RT"",
                    ""type"": ""Button"",
                    ""id"": ""98fb1fef-d7ee-4668-8473-f775b070582d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LS"",
                    ""type"": ""PassThrough"",
                    ""id"": ""10c2b811-a958-4c75-9fd8-b0e4d9a4b98d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UpButton"",
                    ""type"": ""Button"",
                    ""id"": ""c653716d-f9be-4951-a012-f7bcbb634aa7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DownButton"",
                    ""type"": ""Button"",
                    ""id"": ""a5521bbb-8480-4c96-97cb-90b398f50e83"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""KillSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""2264b1a1-ca38-4bc0-9aa0-a0fed590ea4d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""593ce459-c488-4dfe-b843-1de4daa19427"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7045cf9-4e42-4fa6-a235-40e10f7579db"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1806688f-bd52-49b7-8f7a-5e3e9b127cfd"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cb15f41-2fff-4b71-9ce2-834c8f6bece7"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62fa33ff-5bca-47a5-a1ce-b00846525775"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54051cfd-8f0b-4bc9-a7d4-c07b2c0d76d6"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74a73afe-392f-4a95-b1a1-13886fe11b13"",
                    ""path"": ""<XInputController>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f344573-6fd9-4ef0-8b18-bf66f418fe82"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DownButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3f26c49-69e7-4a1a-b59e-d99ffa23ea30"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KillSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerAction
        m_PlayerAction = asset.FindActionMap("PlayerAction", throwIfNotFound: true);
        m_PlayerAction_RS = m_PlayerAction.FindAction("RS", throwIfNotFound: true);
        m_PlayerAction_LB = m_PlayerAction.FindAction("LB", throwIfNotFound: true);
        m_PlayerAction_RB = m_PlayerAction.FindAction("RB", throwIfNotFound: true);
        m_PlayerAction_LT = m_PlayerAction.FindAction("LT", throwIfNotFound: true);
        m_PlayerAction_RT = m_PlayerAction.FindAction("RT", throwIfNotFound: true);
        m_PlayerAction_LS = m_PlayerAction.FindAction("LS", throwIfNotFound: true);
        m_PlayerAction_UpButton = m_PlayerAction.FindAction("UpButton", throwIfNotFound: true);
        m_PlayerAction_DownButton = m_PlayerAction.FindAction("DownButton", throwIfNotFound: true);
        m_PlayerAction_KillSwitch = m_PlayerAction.FindAction("KillSwitch", throwIfNotFound: true);
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

    // PlayerAction
    private readonly InputActionMap m_PlayerAction;
    private IPlayerActionActions m_PlayerActionActionsCallbackInterface;
    private readonly InputAction m_PlayerAction_RS;
    private readonly InputAction m_PlayerAction_LB;
    private readonly InputAction m_PlayerAction_RB;
    private readonly InputAction m_PlayerAction_LT;
    private readonly InputAction m_PlayerAction_RT;
    private readonly InputAction m_PlayerAction_LS;
    private readonly InputAction m_PlayerAction_UpButton;
    private readonly InputAction m_PlayerAction_DownButton;
    private readonly InputAction m_PlayerAction_KillSwitch;
    public struct PlayerActionActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActionActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @RS => m_Wrapper.m_PlayerAction_RS;
        public InputAction @LB => m_Wrapper.m_PlayerAction_LB;
        public InputAction @RB => m_Wrapper.m_PlayerAction_RB;
        public InputAction @LT => m_Wrapper.m_PlayerAction_LT;
        public InputAction @RT => m_Wrapper.m_PlayerAction_RT;
        public InputAction @LS => m_Wrapper.m_PlayerAction_LS;
        public InputAction @UpButton => m_Wrapper.m_PlayerAction_UpButton;
        public InputAction @DownButton => m_Wrapper.m_PlayerAction_DownButton;
        public InputAction @KillSwitch => m_Wrapper.m_PlayerAction_KillSwitch;
        public InputActionMap Get() { return m_Wrapper.m_PlayerAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionActions instance)
        {
            if (m_Wrapper.m_PlayerActionActionsCallbackInterface != null)
            {
                @RS.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRS;
                @RS.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRS;
                @RS.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRS;
                @LB.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLB;
                @LB.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLB;
                @LB.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLB;
                @RB.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRB;
                @RB.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRB;
                @RB.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRB;
                @LT.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLT;
                @LT.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLT;
                @LT.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLT;
                @RT.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRT;
                @RT.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRT;
                @RT.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRT;
                @LS.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLS;
                @LS.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLS;
                @LS.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLS;
                @UpButton.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnUpButton;
                @UpButton.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnUpButton;
                @UpButton.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnUpButton;
                @DownButton.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnDownButton;
                @DownButton.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnDownButton;
                @DownButton.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnDownButton;
                @KillSwitch.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnKillSwitch;
                @KillSwitch.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnKillSwitch;
                @KillSwitch.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnKillSwitch;
            }
            m_Wrapper.m_PlayerActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RS.started += instance.OnRS;
                @RS.performed += instance.OnRS;
                @RS.canceled += instance.OnRS;
                @LB.started += instance.OnLB;
                @LB.performed += instance.OnLB;
                @LB.canceled += instance.OnLB;
                @RB.started += instance.OnRB;
                @RB.performed += instance.OnRB;
                @RB.canceled += instance.OnRB;
                @LT.started += instance.OnLT;
                @LT.performed += instance.OnLT;
                @LT.canceled += instance.OnLT;
                @RT.started += instance.OnRT;
                @RT.performed += instance.OnRT;
                @RT.canceled += instance.OnRT;
                @LS.started += instance.OnLS;
                @LS.performed += instance.OnLS;
                @LS.canceled += instance.OnLS;
                @UpButton.started += instance.OnUpButton;
                @UpButton.performed += instance.OnUpButton;
                @UpButton.canceled += instance.OnUpButton;
                @DownButton.started += instance.OnDownButton;
                @DownButton.performed += instance.OnDownButton;
                @DownButton.canceled += instance.OnDownButton;
                @KillSwitch.started += instance.OnKillSwitch;
                @KillSwitch.performed += instance.OnKillSwitch;
                @KillSwitch.canceled += instance.OnKillSwitch;
            }
        }
    }
    public PlayerActionActions @PlayerAction => new PlayerActionActions(this);
    public interface IPlayerActionActions
    {
        void OnRS(InputAction.CallbackContext context);
        void OnLB(InputAction.CallbackContext context);
        void OnRB(InputAction.CallbackContext context);
        void OnLT(InputAction.CallbackContext context);
        void OnRT(InputAction.CallbackContext context);
        void OnLS(InputAction.CallbackContext context);
        void OnUpButton(InputAction.CallbackContext context);
        void OnDownButton(InputAction.CallbackContext context);
        void OnKillSwitch(InputAction.CallbackContext context);
    }
}

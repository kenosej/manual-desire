//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/InputScripts/PlayerInputActions.inputactions
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

namespace InputScripts
{
    public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""24034119-96d2-49e7-9a95-a8d654e060b4"",
            ""actions"": [
                {
                    ""name"": ""ThrottleAct"",
                    ""type"": ""Button"",
                    ""id"": ""61ee47d0-7a85-4101-8b0d-7b85a2faa9b7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightAct"",
                    ""type"": ""Button"",
                    ""id"": ""4ccd9063-aa18-4704-ab7a-7bb102ffa1fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftAct"",
                    ""type"": ""Button"",
                    ""id"": ""4b2b60b7-96e4-4666-ba94-8dc48db1230f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BrakeAct"",
                    ""type"": ""Button"",
                    ""id"": ""27656699-eed7-4e76-a48b-b14bcfe86e96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Gear1Act"",
                    ""type"": ""Button"",
                    ""id"": ""09621bf6-2580-4987-aea8-e1dfa9b3d75a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Gear2Act"",
                    ""type"": ""Button"",
                    ""id"": ""80701ad1-7416-4bee-bba6-f5bf5da4d175"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Gear3Act"",
                    ""type"": ""Button"",
                    ""id"": ""034eb365-097b-4c1c-a82c-0ce9875b54dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Gear4Act"",
                    ""type"": ""Button"",
                    ""id"": ""03980660-8e8b-40d6-9e9b-33a3e4b5a3e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Gear5Act"",
                    ""type"": ""Button"",
                    ""id"": ""e9c5775f-c5d7-4ca2-83bb-cd6862958a99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Gear6Act"",
                    ""type"": ""Button"",
                    ""id"": ""956e6795-6479-4688-899f-48f9e0363f25"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Gear7Act"",
                    ""type"": ""Button"",
                    ""id"": ""4d6e8a76-c6c8-4508-a302-5239f9923c5d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Gear8Act"",
                    ""type"": ""Button"",
                    ""id"": ""7ba71f0e-fd86-4dff-95eb-15d40a2cc2b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GearReverseAct"",
                    ""type"": ""Button"",
                    ""id"": ""32376031-b7f6-411f-ae17-e4c91ff0da5a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GearNeutralAct"",
                    ""type"": ""Button"",
                    ""id"": ""e9e3f54f-5fae-4873-a910-f983c9fa95a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ClutchAct"",
                    ""type"": ""Button"",
                    ""id"": ""7e42bf07-76fa-4927-8ca9-142941227111"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.2)"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6df1c95e-f0c3-4538-85ee-228a337b7bd5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Hold(duration=0.7)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrottleAct"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c60dd46-49af-4c05-82d0-54a1fbe258d1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Hold(duration=0.2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftAct"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a7d8e75-139d-4413-8eb6-07b2a59ad229"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""Hold(duration=0.2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightAct"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d5fe298-0a57-40d9-b38c-d91e22f15b73"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BrakeAct"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7734308-00de-42cc-9c83-76c6b8295024"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Hold(duration=0.5)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClutchAct"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3429641e-72ea-4e47-830d-ad44d6378bfe"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gear1Act"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6e5990f-c821-4573-9a6d-44b782a1142e"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gear3Act"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d442be24-e701-4e6f-823c-dc4ac12b5205"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gear4Act"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6bed043-3182-41af-98be-b937f69f34b9"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gear5Act"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9431f8b7-9a21-457a-aed0-4144e2846616"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gear6Act"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59a4079f-7787-48f5-b8b6-733afcc312bf"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gear7Act"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1c3c075-8239-4fa0-b3d8-b6b6384fb6e4"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gear8Act"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db86ebe6-4625-4ebf-b2b6-4507a9001ed0"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GearReverseAct"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf312440-0062-4f0a-a1f7-3d11fd29471b"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GearNeutralAct"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2eec4814-6c39-4ae8-ade5-3e6a75002f1b"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gear2Act"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_ThrottleAct = m_Player.FindAction("ThrottleAct", throwIfNotFound: true);
            m_Player_RightAct = m_Player.FindAction("RightAct", throwIfNotFound: true);
            m_Player_LeftAct = m_Player.FindAction("LeftAct", throwIfNotFound: true);
            m_Player_BrakeAct = m_Player.FindAction("BrakeAct", throwIfNotFound: true);
            m_Player_Gear1Act = m_Player.FindAction("Gear1Act", throwIfNotFound: true);
            m_Player_Gear2Act = m_Player.FindAction("Gear2Act", throwIfNotFound: true);
            m_Player_Gear3Act = m_Player.FindAction("Gear3Act", throwIfNotFound: true);
            m_Player_Gear4Act = m_Player.FindAction("Gear4Act", throwIfNotFound: true);
            m_Player_Gear5Act = m_Player.FindAction("Gear5Act", throwIfNotFound: true);
            m_Player_Gear6Act = m_Player.FindAction("Gear6Act", throwIfNotFound: true);
            m_Player_Gear7Act = m_Player.FindAction("Gear7Act", throwIfNotFound: true);
            m_Player_Gear8Act = m_Player.FindAction("Gear8Act", throwIfNotFound: true);
            m_Player_GearReverseAct = m_Player.FindAction("GearReverseAct", throwIfNotFound: true);
            m_Player_GearNeutralAct = m_Player.FindAction("GearNeutralAct", throwIfNotFound: true);
            m_Player_ClutchAct = m_Player.FindAction("ClutchAct", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_ThrottleAct;
        private readonly InputAction m_Player_RightAct;
        private readonly InputAction m_Player_LeftAct;
        private readonly InputAction m_Player_BrakeAct;
        private readonly InputAction m_Player_Gear1Act;
        private readonly InputAction m_Player_Gear2Act;
        private readonly InputAction m_Player_Gear3Act;
        private readonly InputAction m_Player_Gear4Act;
        private readonly InputAction m_Player_Gear5Act;
        private readonly InputAction m_Player_Gear6Act;
        private readonly InputAction m_Player_Gear7Act;
        private readonly InputAction m_Player_Gear8Act;
        private readonly InputAction m_Player_GearReverseAct;
        private readonly InputAction m_Player_GearNeutralAct;
        private readonly InputAction m_Player_ClutchAct;
        public struct PlayerActions
        {
            private @PlayerInputActions m_Wrapper;
            public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @ThrottleAct => m_Wrapper.m_Player_ThrottleAct;
            public InputAction @RightAct => m_Wrapper.m_Player_RightAct;
            public InputAction @LeftAct => m_Wrapper.m_Player_LeftAct;
            public InputAction @BrakeAct => m_Wrapper.m_Player_BrakeAct;
            public InputAction @Gear1Act => m_Wrapper.m_Player_Gear1Act;
            public InputAction @Gear2Act => m_Wrapper.m_Player_Gear2Act;
            public InputAction @Gear3Act => m_Wrapper.m_Player_Gear3Act;
            public InputAction @Gear4Act => m_Wrapper.m_Player_Gear4Act;
            public InputAction @Gear5Act => m_Wrapper.m_Player_Gear5Act;
            public InputAction @Gear6Act => m_Wrapper.m_Player_Gear6Act;
            public InputAction @Gear7Act => m_Wrapper.m_Player_Gear7Act;
            public InputAction @Gear8Act => m_Wrapper.m_Player_Gear8Act;
            public InputAction @GearReverseAct => m_Wrapper.m_Player_GearReverseAct;
            public InputAction @GearNeutralAct => m_Wrapper.m_Player_GearNeutralAct;
            public InputAction @ClutchAct => m_Wrapper.m_Player_ClutchAct;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @ThrottleAct.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrottleAct;
                    @ThrottleAct.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrottleAct;
                    @ThrottleAct.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrottleAct;
                    @RightAct.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightAct;
                    @RightAct.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightAct;
                    @RightAct.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightAct;
                    @LeftAct.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftAct;
                    @LeftAct.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftAct;
                    @LeftAct.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftAct;
                    @BrakeAct.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBrakeAct;
                    @BrakeAct.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBrakeAct;
                    @BrakeAct.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBrakeAct;
                    @Gear1Act.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear1Act;
                    @Gear1Act.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear1Act;
                    @Gear1Act.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear1Act;
                    @Gear2Act.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear2Act;
                    @Gear2Act.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear2Act;
                    @Gear2Act.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear2Act;
                    @Gear3Act.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear3Act;
                    @Gear3Act.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear3Act;
                    @Gear3Act.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear3Act;
                    @Gear4Act.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear4Act;
                    @Gear4Act.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear4Act;
                    @Gear4Act.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear4Act;
                    @Gear5Act.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear5Act;
                    @Gear5Act.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear5Act;
                    @Gear5Act.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear5Act;
                    @Gear6Act.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear6Act;
                    @Gear6Act.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear6Act;
                    @Gear6Act.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear6Act;
                    @Gear7Act.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear7Act;
                    @Gear7Act.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear7Act;
                    @Gear7Act.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear7Act;
                    @Gear8Act.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear8Act;
                    @Gear8Act.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear8Act;
                    @Gear8Act.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGear8Act;
                    @GearReverseAct.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGearReverseAct;
                    @GearReverseAct.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGearReverseAct;
                    @GearReverseAct.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGearReverseAct;
                    @GearNeutralAct.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGearNeutralAct;
                    @GearNeutralAct.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGearNeutralAct;
                    @GearNeutralAct.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGearNeutralAct;
                    @ClutchAct.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClutchAct;
                    @ClutchAct.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClutchAct;
                    @ClutchAct.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClutchAct;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ThrottleAct.started += instance.OnThrottleAct;
                    @ThrottleAct.performed += instance.OnThrottleAct;
                    @ThrottleAct.canceled += instance.OnThrottleAct;
                    @RightAct.started += instance.OnRightAct;
                    @RightAct.performed += instance.OnRightAct;
                    @RightAct.canceled += instance.OnRightAct;
                    @LeftAct.started += instance.OnLeftAct;
                    @LeftAct.performed += instance.OnLeftAct;
                    @LeftAct.canceled += instance.OnLeftAct;
                    @BrakeAct.started += instance.OnBrakeAct;
                    @BrakeAct.performed += instance.OnBrakeAct;
                    @BrakeAct.canceled += instance.OnBrakeAct;
                    @Gear1Act.started += instance.OnGear1Act;
                    @Gear1Act.performed += instance.OnGear1Act;
                    @Gear1Act.canceled += instance.OnGear1Act;
                    @Gear2Act.started += instance.OnGear2Act;
                    @Gear2Act.performed += instance.OnGear2Act;
                    @Gear2Act.canceled += instance.OnGear2Act;
                    @Gear3Act.started += instance.OnGear3Act;
                    @Gear3Act.performed += instance.OnGear3Act;
                    @Gear3Act.canceled += instance.OnGear3Act;
                    @Gear4Act.started += instance.OnGear4Act;
                    @Gear4Act.performed += instance.OnGear4Act;
                    @Gear4Act.canceled += instance.OnGear4Act;
                    @Gear5Act.started += instance.OnGear5Act;
                    @Gear5Act.performed += instance.OnGear5Act;
                    @Gear5Act.canceled += instance.OnGear5Act;
                    @Gear6Act.started += instance.OnGear6Act;
                    @Gear6Act.performed += instance.OnGear6Act;
                    @Gear6Act.canceled += instance.OnGear6Act;
                    @Gear7Act.started += instance.OnGear7Act;
                    @Gear7Act.performed += instance.OnGear7Act;
                    @Gear7Act.canceled += instance.OnGear7Act;
                    @Gear8Act.started += instance.OnGear8Act;
                    @Gear8Act.performed += instance.OnGear8Act;
                    @Gear8Act.canceled += instance.OnGear8Act;
                    @GearReverseAct.started += instance.OnGearReverseAct;
                    @GearReverseAct.performed += instance.OnGearReverseAct;
                    @GearReverseAct.canceled += instance.OnGearReverseAct;
                    @GearNeutralAct.started += instance.OnGearNeutralAct;
                    @GearNeutralAct.performed += instance.OnGearNeutralAct;
                    @GearNeutralAct.canceled += instance.OnGearNeutralAct;
                    @ClutchAct.started += instance.OnClutchAct;
                    @ClutchAct.performed += instance.OnClutchAct;
                    @ClutchAct.canceled += instance.OnClutchAct;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        public interface IPlayerActions
        {
            void OnThrottleAct(InputAction.CallbackContext context);
            void OnRightAct(InputAction.CallbackContext context);
            void OnLeftAct(InputAction.CallbackContext context);
            void OnBrakeAct(InputAction.CallbackContext context);
            void OnGear1Act(InputAction.CallbackContext context);
            void OnGear2Act(InputAction.CallbackContext context);
            void OnGear3Act(InputAction.CallbackContext context);
            void OnGear4Act(InputAction.CallbackContext context);
            void OnGear5Act(InputAction.CallbackContext context);
            void OnGear6Act(InputAction.CallbackContext context);
            void OnGear7Act(InputAction.CallbackContext context);
            void OnGear8Act(InputAction.CallbackContext context);
            void OnGearReverseAct(InputAction.CallbackContext context);
            void OnGearNeutralAct(InputAction.CallbackContext context);
            void OnClutchAct(InputAction.CallbackContext context);
        }
    }
}

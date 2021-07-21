// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/PlayerInputAsset.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputAsset : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAsset()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAsset"",
    ""maps"": [
        {
            ""name"": ""PlayerControlls"",
            ""id"": ""6b6b708b-77ea-469f-aec3-62e6b551ed6a"",
            ""actions"": [
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""PassThrough"",
                    ""id"": ""56505741-3c3e-4661-bdd9-7f3fccbe55ae"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c59a294f-4d10-46ee-ae8b-92824e1d1441"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Boost"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bc09ef68-8d34-489d-9bac-e89f2bde9324"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accelerometer"",
                    ""type"": ""Value"",
                    ""id"": ""4829b735-dcab-4df7-abb1-76cffc3ece88"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ResetVel"",
                    ""type"": ""Button"",
                    ""id"": ""27c3c7b9-f6ad-443d-89f9-5c9e17e1bc06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Delta"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ea004aae-5b64-4e34-a753-3227d4ddf506"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone(min=0.5,max=0.925)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Delta sec touch"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3b7ce22e-af96-4bcd-9ab6-fe67d3ac6892"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone(min=0.5,max=0.925)"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""A/D"",
                    ""id"": ""edf8a00b-c208-49d5-8dca-a06ab40f3d47"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d610813d-7730-46ae-80a4-1f9964609ab7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f95e3007-e485-4bfe-af9d-15553864579d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a920feff-1c7b-4e63-9fae-8f81d1dd2802"",
                    ""path"": ""<Gyroscope>/angularVelocity/x"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=-1,max=1),Invert,AxisDeadzone(min=0.8)"",
                    ""groups"": ""Mobile"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b850c983-694b-4aa9-a0f6-d36494371013"",
                    ""path"": ""<Touchscreen>/position/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""81e5dd58-51c7-4467-a8b2-8e9a1958c3d3"",
                    ""path"": ""<Touchscreen>/touch1/position/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""W/S"",
                    ""id"": ""dcf6d010-4f0b-40aa-95bf-1df2a98d3a08"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7418f9d1-68c3-43b7-a1aa-11470b2fa839"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""27547980-62b6-4ca0-ab9b-a2e697d44b72"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f2b9245e-81bc-4642-9b3e-ba31cfb3011d"",
                    ""path"": ""<Gyroscope>/angularVelocity/y"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=-1,max=1),Invert,AxisDeadzone(min=0.8)"",
                    ""groups"": ""Mobile"",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a62bec52-557b-4572-b225-74a8284d9c40"",
                    ""path"": ""<Touchscreen>/position/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""606b582a-e720-44a1-9ca3-28dd670cdeb2"",
                    ""path"": ""<Touchscreen>/touch1/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b91f292-831b-436f-a8c6-df6fe74f09f8"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aab727b1-d146-497b-9caf-5019d69bbb28"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4acc5cf-721e-460b-8077-2c785bc03f49"",
                    ""path"": ""<AttitudeSensor>/attitude"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Accelerometer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""843d41f6-c18e-4bf8-b837-8535850790b5"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""ResetVel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5929140-ad25-4329-a62b-df0fe5ad420f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""ResetVel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2474135-6fd6-4184-8614-1ad97fa10505"",
                    ""path"": ""<Touchscreen>/touch0/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Delta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0f38ba3-3249-4eea-a5a7-c92f836f25c3"",
                    ""path"": ""<Touchscreen>/touch1/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Delta sec touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""0465b81c-5ea6-4fe5-a6b0-06389e480b5d"",
            ""actions"": [
                {
                    ""name"": ""Left Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ac39fe07-7bc2-4e17-9a24-23088fb2f59a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""077220e8-d65d-4da7-a3be-c97ccf258254"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""PassThrough"",
                    ""id"": ""89d5f80e-f4ac-4631-96a5-6a0f4c35e683"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ec7b9b72-484e-424b-8ccc-4d364bd51b24"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bce59cd8-3115-4607-868d-25bd1d6e2139"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Left Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb7d582e-c507-4541-9c53-4a486a957a2b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Left Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e72a38c3-9659-44f9-93f9-fbaa9bf09af2"",
                    ""path"": ""<Touchscreen>/touch*/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67b2961f-00e8-42ae-b152-913981dd3aca"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a3bdf49-903e-481f-af82-d218bfed11d5"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard;Mobile"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71ea087b-8703-4494-a34c-3bac0634929a"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard;Mobile"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse and Keyboard"",
            ""bindingGroup"": ""Mouse and Keyboard"",
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
                }
            ]
        },
        {
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gyroscope>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerControlls
        m_PlayerControlls = asset.FindActionMap("PlayerControlls", throwIfNotFound: true);
        m_PlayerControlls_Horizontal = m_PlayerControlls.FindAction("Horizontal", throwIfNotFound: true);
        m_PlayerControlls_Vertical = m_PlayerControlls.FindAction("Vertical", throwIfNotFound: true);
        m_PlayerControlls_Boost = m_PlayerControlls.FindAction("Boost", throwIfNotFound: true);
        m_PlayerControlls_Accelerometer = m_PlayerControlls.FindAction("Accelerometer", throwIfNotFound: true);
        m_PlayerControlls_ResetVel = m_PlayerControlls.FindAction("ResetVel", throwIfNotFound: true);
        m_PlayerControlls_Delta = m_PlayerControlls.FindAction("Delta", throwIfNotFound: true);
        m_PlayerControlls_Deltasectouch = m_PlayerControlls.FindAction("Delta sec touch", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_LeftClick = m_UI.FindAction("Left Click", throwIfNotFound: true);
        m_UI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
        m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
        m_UI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
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

    // PlayerControlls
    private readonly InputActionMap m_PlayerControlls;
    private IPlayerControllsActions m_PlayerControllsActionsCallbackInterface;
    private readonly InputAction m_PlayerControlls_Horizontal;
    private readonly InputAction m_PlayerControlls_Vertical;
    private readonly InputAction m_PlayerControlls_Boost;
    private readonly InputAction m_PlayerControlls_Accelerometer;
    private readonly InputAction m_PlayerControlls_ResetVel;
    private readonly InputAction m_PlayerControlls_Delta;
    private readonly InputAction m_PlayerControlls_Deltasectouch;
    public struct PlayerControllsActions
    {
        private @PlayerInputAsset m_Wrapper;
        public PlayerControllsActions(@PlayerInputAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @Horizontal => m_Wrapper.m_PlayerControlls_Horizontal;
        public InputAction @Vertical => m_Wrapper.m_PlayerControlls_Vertical;
        public InputAction @Boost => m_Wrapper.m_PlayerControlls_Boost;
        public InputAction @Accelerometer => m_Wrapper.m_PlayerControlls_Accelerometer;
        public InputAction @ResetVel => m_Wrapper.m_PlayerControlls_ResetVel;
        public InputAction @Delta => m_Wrapper.m_PlayerControlls_Delta;
        public InputAction @Deltasectouch => m_Wrapper.m_PlayerControlls_Deltasectouch;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControlls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControllsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControllsActions instance)
        {
            if (m_Wrapper.m_PlayerControllsActionsCallbackInterface != null)
            {
                @Horizontal.started -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnHorizontal;
                @Horizontal.performed -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnHorizontal;
                @Horizontal.canceled -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnHorizontal;
                @Vertical.started -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnVertical;
                @Vertical.performed -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnVertical;
                @Vertical.canceled -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnVertical;
                @Boost.started -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnBoost;
                @Boost.performed -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnBoost;
                @Boost.canceled -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnBoost;
                @Accelerometer.started -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnAccelerometer;
                @Accelerometer.performed -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnAccelerometer;
                @Accelerometer.canceled -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnAccelerometer;
                @ResetVel.started -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnResetVel;
                @ResetVel.performed -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnResetVel;
                @ResetVel.canceled -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnResetVel;
                @Delta.started -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnDelta;
                @Delta.performed -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnDelta;
                @Delta.canceled -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnDelta;
                @Deltasectouch.started -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnDeltasectouch;
                @Deltasectouch.performed -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnDeltasectouch;
                @Deltasectouch.canceled -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnDeltasectouch;
            }
            m_Wrapper.m_PlayerControllsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Horizontal.started += instance.OnHorizontal;
                @Horizontal.performed += instance.OnHorizontal;
                @Horizontal.canceled += instance.OnHorizontal;
                @Vertical.started += instance.OnVertical;
                @Vertical.performed += instance.OnVertical;
                @Vertical.canceled += instance.OnVertical;
                @Boost.started += instance.OnBoost;
                @Boost.performed += instance.OnBoost;
                @Boost.canceled += instance.OnBoost;
                @Accelerometer.started += instance.OnAccelerometer;
                @Accelerometer.performed += instance.OnAccelerometer;
                @Accelerometer.canceled += instance.OnAccelerometer;
                @ResetVel.started += instance.OnResetVel;
                @ResetVel.performed += instance.OnResetVel;
                @ResetVel.canceled += instance.OnResetVel;
                @Delta.started += instance.OnDelta;
                @Delta.performed += instance.OnDelta;
                @Delta.canceled += instance.OnDelta;
                @Deltasectouch.started += instance.OnDeltasectouch;
                @Deltasectouch.performed += instance.OnDeltasectouch;
                @Deltasectouch.canceled += instance.OnDeltasectouch;
            }
        }
    }
    public PlayerControllsActions @PlayerControlls => new PlayerControllsActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_LeftClick;
    private readonly InputAction m_UI_Point;
    private readonly InputAction m_UI_Submit;
    private readonly InputAction m_UI_Cancel;
    public struct UIActions
    {
        private @PlayerInputAsset m_Wrapper;
        public UIActions(@PlayerInputAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftClick => m_Wrapper.m_UI_LeftClick;
        public InputAction @Point => m_Wrapper.m_UI_Point;
        public InputAction @Submit => m_Wrapper.m_UI_Submit;
        public InputAction @Cancel => m_Wrapper.m_UI_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @LeftClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnLeftClick;
                @Point.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Submit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Cancel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_MouseandKeyboardSchemeIndex = -1;
    public InputControlScheme MouseandKeyboardScheme
    {
        get
        {
            if (m_MouseandKeyboardSchemeIndex == -1) m_MouseandKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse and Keyboard");
            return asset.controlSchemes[m_MouseandKeyboardSchemeIndex];
        }
    }
    private int m_MobileSchemeIndex = -1;
    public InputControlScheme MobileScheme
    {
        get
        {
            if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
            return asset.controlSchemes[m_MobileSchemeIndex];
        }
    }
    public interface IPlayerControllsActions
    {
        void OnHorizontal(InputAction.CallbackContext context);
        void OnVertical(InputAction.CallbackContext context);
        void OnBoost(InputAction.CallbackContext context);
        void OnAccelerometer(InputAction.CallbackContext context);
        void OnResetVel(InputAction.CallbackContext context);
        void OnDelta(InputAction.CallbackContext context);
        void OnDeltasectouch(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnLeftClick(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
}

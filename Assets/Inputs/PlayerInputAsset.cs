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
                    ""type"": ""Value"",
                    ""id"": ""56505741-3c3e-4661-bdd9-7f3fccbe55ae"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical"",
                    ""type"": ""Value"",
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
                    ""id"": ""1b91f292-831b-436f-a8c6-df6fe74f09f8"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": ""Hold(duration=9999999)"",
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
                    ""interactions"": ""Hold(duration=9999999)"",
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
    public struct PlayerControllsActions
    {
        private @PlayerInputAsset m_Wrapper;
        public PlayerControllsActions(@PlayerInputAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @Horizontal => m_Wrapper.m_PlayerControlls_Horizontal;
        public InputAction @Vertical => m_Wrapper.m_PlayerControlls_Vertical;
        public InputAction @Boost => m_Wrapper.m_PlayerControlls_Boost;
        public InputAction @Accelerometer => m_Wrapper.m_PlayerControlls_Accelerometer;
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
            }
        }
    }
    public PlayerControllsActions @PlayerControlls => new PlayerControllsActions(this);
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
    }
}

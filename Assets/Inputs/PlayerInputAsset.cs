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
                    ""name"": ""Horizontal Acc"",
                    ""type"": ""Value"",
                    ""id"": ""bc363fec-1288-4852-b49d-547a4ce6b7b5"",
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
                    ""name"": ""Vertical Acc"",
                    ""type"": ""Value"",
                    ""id"": ""de3e8382-87c5-4705-a13d-05d264beb81a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""a8ae4786-cdee-4ad1-b283-2d2226b9c213"",
                    ""expectedControlType"": ""Vector2"",
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
                    ""name"": ""WASD"",
                    ""id"": ""d5ae65ae-9cca-4862-ad1f-4ea85764a5b8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""03b0e5fb-1ed1-455f-ac76-f4fbe3f6f05d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""35339d0d-5786-4149-b363-468c84f8f588"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b96ae6af-6708-4071-96b0-683154f965b3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""da682ca6-f048-4d25-9cdc-d0b80aa9b272"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Movement"",
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
                    ""name"": ""A/D"",
                    ""id"": ""d0cdcd0b-5ea6-463c-8157-bba7ce9474b0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal Acc"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""db6b9990-4155-4f17-bf0c-4b54c065b2ce"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Horizontal Acc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d2223a5b-0139-42a5-9180-3ef983089d8a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Horizontal Acc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""265e452c-8aab-4f46-84fb-6c6d4c6ef2b0"",
                    ""path"": ""<Accelerometer>/acceleration/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Horizontal Acc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""W/S"",
                    ""id"": ""9979ef19-3db1-4f7e-8886-bf7fd6edcf5b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Vertical Acc"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""bcb92726-5106-4150-95bc-b071de00f895"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Vertical Acc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""775fadc0-ff62-479b-ac0f-ac3378e216a8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Vertical Acc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fbce4ce4-6d80-4e55-a742-80cccd354df4"",
                    ""path"": ""<Accelerometer>/acceleration/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Vertical Acc"",
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
        m_PlayerControlls_HorizontalAcc = m_PlayerControlls.FindAction("Horizontal Acc", throwIfNotFound: true);
        m_PlayerControlls_Vertical = m_PlayerControlls.FindAction("Vertical", throwIfNotFound: true);
        m_PlayerControlls_VerticalAcc = m_PlayerControlls.FindAction("Vertical Acc", throwIfNotFound: true);
        m_PlayerControlls_Movement = m_PlayerControlls.FindAction("Movement", throwIfNotFound: true);
        m_PlayerControlls_Boost = m_PlayerControlls.FindAction("Boost", throwIfNotFound: true);
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
    private readonly InputAction m_PlayerControlls_HorizontalAcc;
    private readonly InputAction m_PlayerControlls_Vertical;
    private readonly InputAction m_PlayerControlls_VerticalAcc;
    private readonly InputAction m_PlayerControlls_Movement;
    private readonly InputAction m_PlayerControlls_Boost;
    public struct PlayerControllsActions
    {
        private @PlayerInputAsset m_Wrapper;
        public PlayerControllsActions(@PlayerInputAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @Horizontal => m_Wrapper.m_PlayerControlls_Horizontal;
        public InputAction @HorizontalAcc => m_Wrapper.m_PlayerControlls_HorizontalAcc;
        public InputAction @Vertical => m_Wrapper.m_PlayerControlls_Vertical;
        public InputAction @VerticalAcc => m_Wrapper.m_PlayerControlls_VerticalAcc;
        public InputAction @Movement => m_Wrapper.m_PlayerControlls_Movement;
        public InputAction @Boost => m_Wrapper.m_PlayerControlls_Boost;
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
                @HorizontalAcc.started -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnHorizontalAcc;
                @HorizontalAcc.performed -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnHorizontalAcc;
                @HorizontalAcc.canceled -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnHorizontalAcc;
                @Vertical.started -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnVertical;
                @Vertical.performed -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnVertical;
                @Vertical.canceled -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnVertical;
                @VerticalAcc.started -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnVerticalAcc;
                @VerticalAcc.performed -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnVerticalAcc;
                @VerticalAcc.canceled -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnVerticalAcc;
                @Movement.started -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnMovement;
                @Boost.started -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnBoost;
                @Boost.performed -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnBoost;
                @Boost.canceled -= m_Wrapper.m_PlayerControllsActionsCallbackInterface.OnBoost;
            }
            m_Wrapper.m_PlayerControllsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Horizontal.started += instance.OnHorizontal;
                @Horizontal.performed += instance.OnHorizontal;
                @Horizontal.canceled += instance.OnHorizontal;
                @HorizontalAcc.started += instance.OnHorizontalAcc;
                @HorizontalAcc.performed += instance.OnHorizontalAcc;
                @HorizontalAcc.canceled += instance.OnHorizontalAcc;
                @Vertical.started += instance.OnVertical;
                @Vertical.performed += instance.OnVertical;
                @Vertical.canceled += instance.OnVertical;
                @VerticalAcc.started += instance.OnVerticalAcc;
                @VerticalAcc.performed += instance.OnVerticalAcc;
                @VerticalAcc.canceled += instance.OnVerticalAcc;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Boost.started += instance.OnBoost;
                @Boost.performed += instance.OnBoost;
                @Boost.canceled += instance.OnBoost;
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
        void OnHorizontalAcc(InputAction.CallbackContext context);
        void OnVertical(InputAction.CallbackContext context);
        void OnVerticalAcc(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnBoost(InputAction.CallbackContext context);
    }
}

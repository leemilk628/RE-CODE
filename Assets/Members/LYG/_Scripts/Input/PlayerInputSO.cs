using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Members.LYG._Scripts.Input
{
        [CreateAssetMenu(fileName = "Player Input SO", menuName = "Player Input SO", order = 0)]
        public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
        {
                #region private region
                private Controls _controls;
                private Camera _mainCamera;
                
                private Vector2 _inputDirection;
                private Vector2 _mousePosition;
                private bool _isSprint;
                #endregion

                #region public region
                public Vector2 InputDirection
                {
                        get => _inputDirection;
                        private set
                        {
                                if (_inputDirection == value)
                                {
                                        return;
                                }
                                
                                OnMoveChanged?.Invoke(value);
                                _inputDirection = value;
                        }
                }
                public Vector2 MousePosition
                {
                        get => _mousePosition;
                        private set
                        {
                                _mousePosition = value;
                        }

                }
                public bool IsSprint
                {
                        get => _isSprint;
                        set => _isSprint = value;
                }

                public event Action<Vector2> OnMoveChanged;
                public event Action OnDashHandled;
                public event Action<bool> OnAttackHandled;
                public event Action OnInteractHandled;
                public event Action<bool> OnUseHandled;
                public event Action<float> OnSwapHandled;
                public event Action<Vector2> OnPingHandled;
                public event Action OnHelperWindowHandled;
                public event Action OnOpenMemoHandled;
                public event Action OnSaveHandled;
                public event Action OnMapHandled;
                public event Action OnPauseHandled;
                #endregion

                public void SetEnable()
                {
                        ClearSubscriptions();
                        
                        if (_controls == null)
                        {
                                _controls = new Controls();
                                _controls.Player.SetCallbacks(this);
                        }
                        _controls.Enable();
                }

                public void SetDisable()
                {
                        if (_controls != null)
                        {
                                _controls.Player.Disable();
                        }
                }

                private void ClearSubscriptions()
                {
                        OnMoveChanged = null;
                        OnAttackHandled = null;
                        OnInteractHandled = null;
                        OnUseHandled = null;
                        OnSwapHandled = null;
                        OnPingHandled = null;
                        OnHelperWindowHandled = null;
                        OnOpenMemoHandled = null;
                        OnSaveHandled = null;
                        OnMapHandled = null;
                        OnPauseHandled = null;
                }

                public void OnMove(InputAction.CallbackContext context)
                {
                        InputDirection = context.ReadValue<Vector2>();
                }

                public void OnDash(InputAction.CallbackContext context)
                {
                        if (context.started)
                                OnDashHandled?.Invoke();
                }

                public void OnAttack(InputAction.CallbackContext context)
                {
                        if(context.performed)
                                OnAttackHandled?.Invoke(true);
                        if(context.canceled)
                                OnAttackHandled?.Invoke(false);
                }

                public void OnInteract(InputAction.CallbackContext context)
                {
                        if (context.started)
                                OnInteractHandled?.Invoke();
                }

                public void OnSprint(InputAction.CallbackContext context)
                        => IsSprint = context.performed;

                public void OnUse(InputAction.CallbackContext context) 
                {
                        if(context.performed)
                                OnUseHandled?.Invoke(true);
                        if(context.canceled)
                                OnUseHandled?.Invoke(false);
                }

                public void OnSwap(InputAction.CallbackContext context)
                {
                        if (context.performed)
                                OnSwapHandled?.Invoke(context.ReadValue<float>());
                }

                public void OnPing(InputAction.CallbackContext context)
                {
                        if (context.started)
                                OnPingHandled?.Invoke(MousePosition);
                }

                public void OnHelperWindow(InputAction.CallbackContext context)
                {
                        if (context.started)
                                OnHelperWindowHandled?.Invoke();
                }

                public void OnOpenMemo(InputAction.CallbackContext context)
                {
                        if (context.started)
                                OnOpenMemoHandled?.Invoke();
                }

                public void OnSave(InputAction.CallbackContext context)
                {
                        if (context.started)
                                OnSaveHandled?.Invoke();
                }

                public void OnMap(InputAction.CallbackContext context)
                {
                        if (context.started)
                                OnMapHandled?.Invoke();
                }

                public void OnPause(InputAction.CallbackContext context)
                {
                        if (context.started)
                                OnPauseHandled?.Invoke();
                }
                
                public void OnPointer(InputAction.CallbackContext context) 
                        => MousePosition = GetMouseWorldPosition(context.ReadValue<Vector2>());

                private Vector2 GetMouseWorldPosition(Vector2 mousePosition)
                {
                        if(_mainCamera == null)
                                _mainCamera = Camera.main;
            
                        Vector3 worldPosition = _mainCamera!.ScreenToWorldPoint(mousePosition);
                        worldPosition.z = 0;
                        return worldPosition;
                }
        }
}
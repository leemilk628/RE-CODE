using System;
using System.Diagnostics;
using DevLib.ModuleSystem;
using Members.LYG._Scripts.Agents.Player;
using UnityEngine;

namespace Members.LYG._Scripts.Agents
{
        public class AbstractMover : MonoModule, IMover
        {
                public event Action<Vector2> OnMoveChanged;
                public event Action OnDashEnd;
                
                public Rigidbody2D Rigidbody { get; private set; }
                public Vector2 Direction { get; private set; }

                [Tooltip("목표 속도에 도달하는 속도"), Min(0f)] 
                [field: SerializeField] public float Acceleration { get; private set; } = 40f;
                [Tooltip("입력이 없을 때 감속하는 속도"), Min(0f)] 
                [field: SerializeField] public float Deceleration { get; private set; } = 15f;
                [Tooltip("일반 이동 속도"), Min(0f)] 
                [field: SerializeField] public float MoveSpeed { get; private set; }
                [Tooltip("달리기 이동 속도"), Min(0f)] 
                [field: SerializeField] public float SprintSpeed { get; private set; }
                [Tooltip("대시 속도"), Min(0f)]
                [field: SerializeField] public float DashSpeed { get; private set; }
                [Tooltip("대시 유지 시간")]
                [field: SerializeField] public float DashDuration { get; private set; } = 0.15f;
                public bool CanMove { get; private set; } = true;

                private float _currentSpeed;
                private float _remainingDashTime;
                private Vector2 _dashDirection;
                private bool _isMove;

                public bool IsDashing { get; private set; }


                private const float DirectionEpsilon = 0.1f;

                public override void Initialize(ModuleOwner owner)
                {
                        base.Initialize(owner);
                        Rigidbody = Owner.GetComponent<Rigidbody2D>();
                        Rigidbody.linearDamping = 0f;
                        _remainingDashTime = DashDuration;
                }

                public virtual void SetDirection(Vector2 direction)
                {
                        Direction = direction.sqrMagnitude <= DirectionEpsilon ? Vector2.zero : direction.normalized;
                        _dashDirection =  direction == Vector2.zero ? _dashDirection : direction.normalized;
                }

                public void SetSpeed(SpeedType speedType)
                {
                        _currentSpeed = speedType switch
                        {
                                SpeedType.Move => _currentSpeed = MoveSpeed,
                                SpeedType.Sprint => _currentSpeed = SprintSpeed,
                                _ => throw new Exception("Unexpected speed type: " + speedType)
                        };
                }

                public void SetMove(Vector2 direction)
                {
                        SetDirection(direction);
                }

                public virtual void Move()
                {
                        if (IsDashing) UpdateDash();
                        if (Rigidbody == null || !CanMove||IsDashing) return;

                        OnMoveChanged?.Invoke(Direction);
                        Vector2 targetVelocity = Direction * _currentSpeed;

                        float movementRate = Direction.sqrMagnitude > DirectionEpsilon ? Acceleration : Deceleration;

                        _isMove = Direction != Vector2.zero;
                        
                        Rigidbody.linearVelocity = Vector2.MoveTowards(
                                Rigidbody.linearVelocity,
                                targetVelocity,
                                movementRate * Time.fixedDeltaTime);
                }

                protected virtual void UpdateDash()
                {
                        if (IsDashing)
                                _remainingDashTime -= Time.fixedDeltaTime;
                        
                        if (_remainingDashTime > 0f) return;

                        IsDashing = false;
                        
                        OnDashEnd?.Invoke();
                        _remainingDashTime = DashDuration;
                        Rigidbody.linearVelocity = Vector2.zero;
                }

                public virtual void SetCanMove(bool canMove) 
                        => CanMove = canMove;

                public virtual void Dash()
                {
                        if (Rigidbody == null )
                                return;

                        if (!_isMove)
                        {
                                _dashDirection = (Owner.GetComponent<PlayerController>().PlayerInput.MousePosition -
                                                  (Vector2)transform.position).normalized ;
                        }
                        
                        IsDashing = true;
                        _remainingDashTime = DashDuration;
                        Rigidbody.linearVelocity = Vector2.zero;
                        Rigidbody.AddForce(_dashDirection * DashSpeed, ForceMode2D.Impulse);
                }

                public virtual void Stop()
                {
                        SetDirection(Vector2.zero);
                }

                public virtual void StopImmediately()
                {
                        _remainingDashTime = 0f;
                        SetDirection(Vector2.zero);
                        Rigidbody.linearVelocity = Vector2.zero;
                }
        }
}
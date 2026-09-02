using System;
using DevLib.ModuleSystem;
using UnityEngine;

namespace Members.LYG._Scripts.Agents
{
        public class AbstractMover : MonoModule, IMover
        {
                public event Action<Vector2> OnMoveChanged;
                
                public Rigidbody2D Rigidbody { get; private set; }
                public Vector2 Direction { get; private set; }

                [Tooltip("목표 속도에 도달하는 속도"), Min(0f)] 
                [field: SerializeField] public float Acceleration { get; private set; } = 40f;
                [Tooltip("입력이 없을 때 감속하는 속도"), Min(0f)] 
                [field: SerializeField] public float Deceleration { get; private set; } = 15f;
                [Tooltip("일반 이동 속도"), Min(0f)] 
                [field: SerializeField] public float Speed { get; private set; }
                [Tooltip("대시 속도"), Min(0f)]
                [field: SerializeField] public float DashSpeed { get; private set; }
                public bool CanMove { get; private set; } = true;


                private const float DirectionEpsilon = 0.1f;

                public override void Initialize(ModuleOwner owner)
                {
                        base.Initialize(owner);
                        Rigidbody = Owner.GetComponent<Rigidbody2D>();
                        Rigidbody.linearDamping = 0f;
                }

                public virtual void SetDirection(Vector2 direction)
                {
                        Direction = direction.sqrMagnitude <= DirectionEpsilon ? Vector2.zero : direction.normalized;
                }

                public void SetMove(Vector2 direction)
                {
                        SetDirection(direction);
                }

                public virtual void Move()
                {
                        if (Rigidbody == null || !CanMove) return;

                        OnMoveChanged?.Invoke(Direction);
                        Vector2 targetVelocity = Direction * Speed;

                        float movementRate = Direction.sqrMagnitude > DirectionEpsilon ? Acceleration : Deceleration;

                        Rigidbody.linearVelocity = Vector2.MoveTowards(
                                Rigidbody.linearVelocity,
                                targetVelocity,
                                movementRate * Time.fixedDeltaTime);
                }
                
                public virtual void SetCanMove(bool canMove) 
                        => CanMove = canMove;

                public virtual void Dash()
                {
                        if (Rigidbody == null || Direction == Vector2.zero)
                                return;

                        Rigidbody.linearVelocity = Vector2.zero;
                        Rigidbody.AddForce(Direction * DashSpeed, ForceMode2D.Impulse);
                }

                public virtual void Stop()
                {
                        SetDirection(Vector2.zero);
                }

                public virtual void StopImmediately()
                {
                        SetDirection(Vector2.zero);
                        Rigidbody.linearVelocity = Vector2.zero;
                }
        }
}
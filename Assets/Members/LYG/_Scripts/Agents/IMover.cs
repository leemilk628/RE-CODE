using System;
using UnityEngine;

namespace Members.LYG._Scripts.Agents
{
        public enum SpeedType
        {
                Move,
                Sprint,
        }
        public interface IMover
        {
                event Action<Vector2> OnMoveChanged;
                event Action OnDashEnd;
                
                Rigidbody2D Rigidbody { get; }
                Vector2 Direction { get; }
                float Acceleration { get; }
                float Deceleration { get; }
                float MoveSpeed { get; }
                float SprintSpeed { get; }
                float DashSpeed { get; }
                bool CanMove { get; }

                void SetDirection(Vector2 direction);
                void SetSpeed(SpeedType speedType);
                void Dash();
                void SetMove(Vector2 direction);
                void SetCanMove(bool canMove);
                void Move();
                void Stop();
                void StopImmediately();
        }
}
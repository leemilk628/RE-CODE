using System;
using UnityEngine;

namespace Members.LYG._Scripts.Agents
{
        public interface IMover
        {
                event Action<Vector2> OnMoveChanged;
                
                Rigidbody2D Rigidbody { get; }
                Vector2 Direction { get; }
                float Acceleration { get; }
                float Deceleration { get; }
                float Speed { get; }
                float DashSpeed { get; }
                bool CanMove { get; }

                void SetDirection(Vector2 direction);
                void Dash();
                void SetMove(Vector2 direction);
                void SetCanMove(bool canMove);
                void Move();
                void Stop();
                void StopImmediately();
        }
}
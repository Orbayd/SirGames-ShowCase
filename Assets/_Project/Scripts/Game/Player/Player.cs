using System.Collections.Generic;
using System.Linq;
using SirGames.Showcase.Events;
using SirGames.Showcase.Helpers;
using UnityEngine;

namespace SirGames.Showcase.GamePlay
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        private CharacterController _controller;
        private Quaternion _lookRotation;
        private Dictionary<MoveDirection, IMoveCommand> _moveCommands = new Dictionary<MoveDirection, IMoveCommand>();

        [SerializeField]
        private float _speed;
        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _moveCommands.Add(MoveDirection.Forward, new MoveCommand(() => _controller.Move(transform.forward * _speed)));
        }

        private void FixedUpdate()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            var touches = Input.touches;
            if (!touches.Any())
            {
                return;
            }

            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    if (touch.deltaPosition.magnitude > 5.0f)
                    {
                        var normalized = touch.deltaPosition.normalized;
                        _lookRotation = Quaternion.LookRotation(new Vector3(normalized.x, 0, normalized.y));
                        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 2);
                    }
                    _moveCommands[MoveDirection.Forward].Execute();
                    break;
                case TouchPhase.Stationary:
                    _lookRotation = Quaternion.identity;
                    _moveCommands[MoveDirection.Forward].Execute();
                    break;
                case TouchPhase.Ended:
                    _lookRotation = Quaternion.identity;
                    break;
                case TouchPhase.Began:
                    break;
                default: break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            MessageBus.Publish<PrepareRewardEvent>(new PrepareRewardEvent(other.gameObject));
        }
    }
}

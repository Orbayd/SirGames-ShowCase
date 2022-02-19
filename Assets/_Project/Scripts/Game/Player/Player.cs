using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace SirGames.Showcase.Managers
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        private CharacterController _controller;

        private Dictionary<MoveDirection, IMoveCommand> moveCommands = new Dictionary<MoveDirection, IMoveCommand>();

        [SerializeField]
        private float _speed;
        void Start()
        {
            _controller = GetComponent<CharacterController>();

            moveCommands.Add(MoveDirection.Forward, new MoveCommand(() => _controller.Move(transform.forward * _speed)));
            moveCommands.Add(MoveDirection.Backward, new MoveCommand(() => _controller.Move(transform.forward * -1 * _speed)));
            moveCommands.Add(MoveDirection.Left, new MoveCommand(() => _controller.Move(transform.right * -1 * _speed)));
            moveCommands.Add(MoveDirection.Right, new MoveCommand(() => _controller.Move(transform.right * _speed)));
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }
        private Quaternion _lookRotation;
        private void HandleInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveCommands[MoveDirection.Forward].Execute();
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveCommands[MoveDirection.Backward].Execute();
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveCommands[MoveDirection.Left].Execute();
            }

            if (Input.GetKey(KeyCode.D))
            {
                moveCommands[MoveDirection.Right].Execute();
            }

            var touches = Input.touches;
            if (!touches.Any())
            {
                return;
            }

            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("[INFO] Touch Phase Began");
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("[INFO] Touch Phase Moved");
                
                var angle =Vector2.Angle(touch.deltaPosition.normalized, new Vector2(0,1));

                Debug.Log($"[INFO] DeltaPosition {touch.deltaPosition.normalized}");
                Debug.Log($"[INFO] Angle {angle}");
                
                 var normalized = touch.deltaPosition.normalized;
                // float singleStep = 10 * Time.deltaTime;
                // var newDirection = Vector3.RotateTowards(transform.forward, new Vector3(normalized.x,0,normalized.y), singleStep, 0.0f);
    
                //create the rotation we need to be in to look at the target
                _lookRotation = Quaternion.LookRotation(new Vector3(normalized.x ,0, normalized.y));
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 2);
                
                
                moveCommands[MoveDirection.Forward].Execute();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("[INFO] Touch Phase Ended");                
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            GameManager.Singleton.GiveReward(other.gameObject);
        }
    }
}

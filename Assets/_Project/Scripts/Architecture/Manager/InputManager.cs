/*
namespace SirGames.Showcase.Managers
{
    public class InputManager : MonoBehaviour
    {
        private Dictionary<MoveDirection, IMoveCommand> moveCommands = new Dictionary<MoveDirection, IMoveCommand>();

        void Start()
        {
            Init();
        }
        public void Init()
        {
            moveCommands.Add(MoveDirection.Forward, new MoveCommand(() => _controller.Move(transform.forward * _speed)));
            moveCommands.Add(MoveDirection.Backward, new MoveCommand(() => _controller.Move(transform.forward * -1 * _speed)));
            moveCommands.Add(MoveDirection.Left, new MoveCommand(() => _controller.Move(transform.right * -1 * _speed)));
            moveCommands.Add(MoveDirection.Right, new MoveCommand(() => _controller.Move(transform.right * _speed)));
        }

        void Update()
        {
            HandleInput();
        }

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

        }
    }
}
*/
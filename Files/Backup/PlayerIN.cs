// class PlayerInput : MonoBehaviour
// {
//     public static PlayerInput current;

//     bool player1Shoot = false;
//     bool player2Shoot = false;
//     bool player1Shoot2 = false;
//     bool player2Shoot2 = false;
//     bool player1Block = false;
//     bool player2Block = false;

//     Vector2 dir1 = Vector2.zero;
//     Vector2 dir2 = Vector2.zero;

//     void Awake()
//     {
//         current = this;
//     }

//     public event Action<int> onLightShootInput;
//     public event Action<int> onHeavyShootInput;
//     public event Action<int> onBlockInput;
//     public event Action<int, Vector2> onMoveInput;

//     void Update()
//     {
//         if(Input.GetButtonDown($"{1}shoot"))
//         {
//             FunctionTimer.current.removeTimer("Player1Shoot:false");
//             player1Shoot = true;
//             FunctionTimer.current.AddTimer(()=> {player1Shoot = false;}, inputValidTime, "Player1Shoot:false");
//         }
//         if(Input.GetButtonDown($"{2}shoot"))
//         {
//             FunctionTimer.current.removeTimer("Player2Shoot:false");
//             player2Shoot = true;
//             FunctionTimer.current.AddTimer(()=> {player2Shoot = false;}, inputValidTime, "Player2Shoot:false");
//         }
//         if(Input.GetButtonDown($"{1}block"))
//         {
//             FunctionTimer.current.removeTimer("Player1Block:false");
//             player1Block = true;
//             FunctionTimer.current.AddTimer(()=> {player1Block = false;}, inputValidTime, "Player1Block:false");
//         }
//         if(Input.GetButtonDown($"{2}block"))
//         {
//             FunctionTimer.current.removeTimer("Player2Block:false");
//             player2Block = true;
//             FunctionTimer.current.AddTimer(()=> {player2Block = false;}, inputValidTime, "Player2Block:false");
//         }
//         if(Input.GetButtonDown($"{1}shoot2"))
//         {
//             FunctionTimer.current.removeTimer("Player1Shoot2:false");
//             player1Shoot2 = true;
//             FunctionTimer.current.AddTimer(()=> {player1Shoot2 = false;}, inputValidTime, "Player1Shoot2:false");
//         }
//         if(Input.GetButtonDown($"{2}shoot2"))
//         {
//             FunctionTimer.current.removeTimer("Player2Shoot2:false");
//             player2Shoot2 = true;
//             FunctionTimer.current.AddTimer(()=> {player2Shoot2 = false;}, inputValidTime, "Player2Shoot2:false");
//         }
//         if (Input.GetButtonDown($"{1}shoot") && onLightShootInput != null)
//         {
//             onLightShootInput(1);
//         }
//         if (Input.GetButtonDown($"{2}shoot") && onLightShootInput != null)
//         {
//             onLightShootInput(2);
//         }
//         if (Input.GetButtonDown($"{1}block") && onBlockInput != null)
//         {
//             onBlockInput(1);
//         }
//         if (Input.GetButtonDown($"{2}block") && onBlockInput != null)
//         {
//             onBlockInput(2);
//         }
//         if (Input.GetButtonDown($"{1}shoot2") && onHeavyShootInput != null)
//         {
//             onHeavyShootInput(1);
//         }
//         if (Input.GetButtonDown($"{2}shoot2") && onHeavyShootInput != null)
//         {
//             onHeavyShootInput(2);
//         }

//         Vector2 newDir1 = getMoveInput(1);
//         if (newDir1 != dir1 && onMoveInput != null)
//         {
//             onMoveInput(1, newDir1);
//             dir1 = newDir1;
//         }

//         Vector2 newDir2 = getMoveInput(2);
//         if (newDir2 != dir2 && onMoveInput != null)
//         {
//             onMoveInput(2, newDir2);
//             dir2 = newDir2;
//         }
//     }

//     public Vector2 getMoveInput(int player)
//     {
//         Vector2 dir = new Vector2
//         (
//                 Mathf.Round(Input.GetAxisRaw($"{player}right") * 1.25f), // make corners easyer to get to
//                 Mathf.Round(Input.GetAxisRaw($"{player}up") * 1.25f)
//             );

//         return dir;
//     }

//     public bool isMoving(int player)
//     {
//         Vector2 dir = new Vector2
//         (
//                 Mathf.Round(Input.GetAxisRaw($"{player}right") * 1.25f), // make corners easyer to get to
//                 Mathf.Round(Input.GetAxisRaw($"{player}up") * 1.25f)
//         );

//         return dir != Vector2.zero;
//     }

//     public bool Shoot(int player)
//     {
//         switch (player)
//         {
//             case 1:
//                 return player1Shoot;
//             case 2:
//                 return player2Shoot;
//             default:
//                 return false;
//         }
//     }

//     public bool Block(int player)
//     {
//         switch (player)
//         {
//             case 1:
//                 return player1Block;
//             case 2:
//                 return player2Block;
//             default:
//                 return false;
//         }
//     }

//     public bool Shoot2(int player)
//     {
//         switch (player)
//         {
//             case 1:
//                 return player1Shoot2;
//             case 2:
//                 return player2Shoot2;
//             default:
//                 return false;
//         }
//     }
// }

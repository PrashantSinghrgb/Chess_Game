using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using PrashantSingh.Utilities.Serialization;
using PrashantSingh.Utilities.ExtensionMethods;
using PrashantSingh.Utilities;
using Utilities;


namespace Game
{
    public class GameManager : MonoBehaviour
    {
        #region Properties

        // A reference to the gameBoard
        [SerializeField]
        private GameBoard _gameBoard;
        // A reference to player 1
        [SerializeField]
        private Player _player_1;
        // A reference to player 2
        [SerializeField]
        private Player _player_2;

        // The current player depending on the current game state
        private Player player
        {
            get
            {
                return (_gameState == GameState.WaitingOnPlayer1 || _gameState == GameState.Player1Move ? _player_1 : _player_2);
            }
        }

        // A reference to the computer AI
        [SerializeField]
        private ComputerAI _computerAI;

        // an enum denoting the various game state
        private enum GameState
        {
            // Waiting on Player 1 - This is when it is player 1st turn and they haven't selected a piece to move
            WaitingOnPlayer1,
            // Player 1 Move - This is when player 1 has selected a piece to move, waiting on board cell to move to
            Player1Move,
            // Waiting on Player 2 - This is when it is player 2nd turn and they haven't selected a piece to move
            WaitingOnPlayer2,
            // Player 2 Move - This is when player 2 has selected a piece to move, waiting on board cell to move to
            Player2Move
        }
        // The game state
        private GameState _gameState = GameState.WaitingOnPlayer1;
        // Whether player 1 is humand and the game is waiting on them to select a piece
        private bool WaitingOnHumanPlayer1
        {
            get
            {
                return _gameState == GameState.WaitingOnPlayer1 && _player_1.type == Player.Type.Human;
            }
        }
        // Whether player 1 is human and the game is waiting on them to move a selected piece
        private bool HumanPlayer1Move
        {
            get
            {
                return _gameState == GameState.Player1Move && _player_1.type == Player.Type.Human;
            }
        }
        // Whether player 2 is human and the game is waiting on them to select a piece
        private bool WaitingOnHumanPlayer2
        {
            get
            {
                return _gameState == GameState.WaitingOnPlayer2 && _player_2.type == Player.Type.Human;
            }
        }
        // Whether player 2 is human and the game is waiting on them to move a selected piece
        private bool HumanPlayer2Move
        {
            get
            {
                return _gameState == GameState.Player2Move && _player_2.type == Player.Type.Human;
            }
        }
        // Whether player 1 is the Computer and it is their turn
        private bool ComputerPlayer1Move
        {
            get
            {
                return _gameState == GameState.WaitingOnPlayer1 && _player_1.type == Player.Type.Computer;
            }
        }
        // Whether player 2 is the Computer and it is their turn
        private bool ComputerPlayer2Move
        {
            get
            {
                return _gameState == GameState.WaitingOnPlayer2 && _player_2.type == Player.Type.Computer;
            }
        }

        // Whether the computer is thinking
        private bool _computerIsThinking = false;
        // The board cell layer mask
        public LayerMask boardCellLayerMask;
        // The board Piece layer mask
        public LayerMask boardPieceLayerMask;
        #endregion

        #region Initialization

        // EDITOR ONLY : Callback when the script is loaded or a value is changed in the inspector
        private void OnValidate()
        {
            Assert.IsNotNull(_gameBoard);
            Assert.IsNotNull(_player_1);
            Assert.IsNotNull(_player_2);
            Assert.IsNotNull(_computerAI);
        }
        // Callback when the instance is started
        private void Start()
        {
            // setup the player
            _player_1.SetType(PlayerPreferences.GetInt(PlayerPreferencesKeys.player1));
            _player_2.SetType(PlayerPreferences.GetInt(PlayerPreferencesKeys.player2));
        }

        #endregion

        #region Callbacks

        // Callback when the instance is updating
        private void Update()
        {
            // if it is a human turn to choose a piece
            if (WaitingOnHumanPlayer1 || WaitingOnHumanPlayer2)
            {
                // there is a touch event
                if (Input.GetMouseButton(0))
                {
                    // cast a ray and determine if the player is touching a board piece
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, boardPieceLayerMask);
                    if (hit.collider != null)
                    {
                        // save a reference to the piece
                        player.selectedPiece = hit.collider.gameObject.GetComponent<BoardPiece>();
                        // ignore when the player has selected as opponents piece
                        if (player.selectedPiece.color != player.color)
                        {
                            return;
                        }
                        //highlight cells for the piece and update game state
                        _gameBoard.HighlightBoardCellsForPlayer(player);
                        _gameState = WaitingOnHumanPlayer1 ? GameState.Player1Move : GameState.Player2Move;
                    }
                }
            }
            // else if it is a human turn to make a move with a selected piece
            else if (HumanPlayer1Move || HumanPlayer2Move)
            {
                // there is a touch event
                if (Input.GetMouseButtonDown(0))
                {
                    //cast a ray and determine if the player is touching a board cell
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, boardCellLayerMask);
                    if (hit.collider != null)
                    {
                        // first check if the player is touching one of their own piece - if so, use that as the new selected piece
                        BoardCell cell = hit.collider.gameObject.GetComponent<BoardCell>();
                        BoardPiece piece = _gameBoard.pieces[cell.x, cell.y];

                        if (piece != null && piece.color == player.color && piece.gameObject != player.selectedPiece)
                        {
                            // save a reference to the piece
                            player.selectedPiece = piece;
                            // Reset cells for selected piece
                            _gameBoard.ResetBoardCellsHighlight();
                            // highlight cells for selected piece
                            _gameBoard.HighlightBoardCellsForPlayer(player);
                        }
                        // otherwise try to move the player piece to the selected cell
                        else
                        {
                            player.selectedCell = cell;

                            // if the move is possible, change state
                            if (_gameBoard.TryPlayerMove(player))
                            {
                                _gameState = HumanPlayer1Move ? GameState.WaitingOnPlayer2 : GameState.WaitingOnPlayer1;
                            }
                        }
                    }
                }
            }
            else if (ComputerPlayer1Move || ComputerPlayer2Move) //else if it is a computer move
            {
                if (!_computerIsThinking)
                {
                    _computerIsThinking = true; //computer starts to 'think'
                                               //wait one second and make a random move, then update game state
                    this.Invoke(()=>{
                        _computerAI.MakeMoveForPlayer(player);
                        _gameState = ComputerPlayer1Move ? GameState.WaitingOnPlayer2 : GameState.WaitingOnPlayer1;
                        _computerIsThinking = false;
                    }, Duration.ONE_SECOND);
                }
            }
        }
        /// <summary>Callback when the ResetButton is pressed.</summary>
        public void ResetButtonPressed()
        {
            _gameBoard.Reset();
            _player_1.Reset(); 
            _player_2.Reset();
            StopAllCoroutines();
            _gameState = GameState.WaitingOnPlayer1; 
            _computerIsThinking = false;
        }
        #endregion
    }
}
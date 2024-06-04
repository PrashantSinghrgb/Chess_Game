using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

namespace Game
{
    public class GameBoard : MonoBehaviour
    {
        #region Properties

        // The number of rows
        public const int ROWS = 8;
        // The number of colums
        public const int COLUMS = 8;
        // The BoardCell prefab
        public BoardCell boardCellPrefab;
        // An array of board cells
        private BoardCell[,] _cells;
        // The BoardPieces prefab
        public BoardPiece boardPiecesPrefab;
        // An Array of board pieces
        public BoardPiece[,] pieces
        {
            get;
            private set;
        }

        #endregion

        #region Initialization

        // EDITOR ONLY : Callback when the script is loaded or a value is changed in the inspector
        private void OnValidate()
        {
            Assert.IsNotNull(boardCellPrefab);
            Assert.IsNotNull(boardPiecesPrefab);
        }
        // Callback when the instance awakes
        private void Awake()
        {
            _cells = new BoardCell[COLUMS, ROWS];
            pieces = new BoardPiece[COLUMS, ROWS];
        }
        // Callback when the instance start
        private void Start()
        {
            CreateBoardCells();
            CreateBoardPieces();
        }

        #endregion

        #region Board Initialization

        // Resets the gameboard
        public void Reset()
        {
            // delete any piece on the board
            for (int x = 0; x < COLUMS; x++)
            {
                for (int y = 0; y < ROWS; y++)
                {
                    if (pieces[x, y] != null)
                    {
                        Destroy(pieces[x, y].gameObject);
                    }
                }
            }

            // re-create the board pieces
            CreateBoardPieces();
            // reset any cell highlight
            ResetBoardCellsHighlight();
        }

        // Create the board Cells
        private void CreateBoardCells()
        {
            for (int x = 0; x < COLUMS; x++)
            {
                for (int y = 0; y < ROWS; y++)
                {
                    BoardCell cell = Instantiate(boardCellPrefab, this.transform, false) as BoardCell;
                    cell.SetXY(x, y);
                    cell.color = (x % 2 == y % 2 ? GameColors.cellDark : GameColors.cellLight);
                    _cells[x, y] = cell;
                }
            }
        }
        // Create the board pieces
        private void CreateBoardPieces()
        {
            // Initial 2-row piece layout
            BoardPiece.Type[] initialPieceTypes =
            {
                BoardPiece.Type.Rook,
                BoardPiece.Type.Knight,
                BoardPiece.Type.Bishop,
                BoardPiece.Type.Queen,
                BoardPiece.Type.King,
                BoardPiece.Type.Bishop,
                BoardPiece.Type.Knight,
                BoardPiece.Type.Rook,
                BoardPiece.Type.Pawn,
                BoardPiece.Type.Pawn,
                BoardPiece.Type.Pawn,
                BoardPiece.Type.Pawn,
                BoardPiece.Type.Pawn,
                BoardPiece.Type.Pawn,
                BoardPiece.Type.Pawn,
                BoardPiece.Type.Pawn,
            };

            int pieceCount = 0;
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < COLUMS; x++)
                {
                    BoardPiece piece = Instantiate(boardPiecesPrefab, this.transform, false) as BoardPiece;
                    piece.SetUp(initialPieceTypes[pieceCount], Player.Color.White, x, y);
                    pieces[x, y] = piece;
                    pieceCount++;
                }
            }

            pieceCount = 0;
            for (int y = ROWS-1; y >= ROWS-2; y--)
            {
                for (int x = COLUMS-1; x >= 0; x--)
                {
                    BoardPiece piece = Instantiate(boardPiecesPrefab, this.transform, false) as BoardPiece;
                    piece.gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
                    piece.SetUp(initialPieceTypes[pieceCount], Player.Color.Black, x, y);
                    pieces[x, y] = piece;
                    pieceCount++;
                }
            }
        }

        #endregion

        #region Move

        // Tries to move the player selected piece to a selected board cell
        ///<return><c>true</c>, if player move was possible, <c>false</c> otherwise </return>
        ///<param name="player">The player</param>
        ///<param name="cellX">The x-value of the cell to move to</param>
        ///<param name="cellY">The y-value of the cell to move to</param>
        public bool TryPlayerMove(Player player, int cellX, int cellY)
        {
            player.selectedCell = _cells[cellX, cellY];
            return TryPlayerMove(player);
        }

        // Tries to move the player selected piece to a selected board cell
        ///<return><c>true</c>, if player move was possible, <c>false</c> otherwise </return>
        ///<param name="player">The player</param>
        public bool TryPlayerMove(Player player)
        {
            if (player.selectedCell == null)
            {
                Debug.LogError("Player's selected cell is null!");
                return false;
            }

            int targetX = player.selectedCell.x;
            int targetY = player.selectedCell.y;
            int pieceX = player.selectedPiece.x;
            int pieceY = player.selectedPiece.y;

            // no piece at the selected cell
            if (pieces[targetX, targetY] == null)
            {
                if (player.IsValidMove(targetX, targetY))
                {
                    MovePieceFromTo(pieceX, pieceY, targetX, targetY);
                    ResetBoardCellsHighlight();
                    return true;
                }
                else
                {
                    Debug.Log("Invalid Move");
                }
            }
            // piece at selected cell
            else
            {
                if (player.IsValidMove(targetX, targetY))
                {
                    if (pieces[targetX,targetY].canBeCaptured)
                    {
                        PlayerRemovesPiece(player, pieces[targetX, targetY]);
                        MovePieceFromTo(pieceX, pieceY, targetX, targetY);
                        ResetBoardCellsHighlight();
                        return true;
                    }
                    else
                    {
                        Debug.LogFormat($"{pieces[targetX, targetY].name} at ({targetX}, {targetY}) cannot be captured");
                    }
                }
                else
                {
                    Debug.Log("Invalid Move");
                }
            }
            return false;
        }

        // Removes a given piece from the game board by a given player
        ///<param name="player">The Player</param>
        ///<param name="piece">The piece</param>
        private void PlayerRemovesPiece(Player player, BoardPiece piece)
        {
            player.AddCapturedPiece(piece);
            piece.RemovedFromBoard();
            pieces[piece.x, piece.y] = null;
        }

        // Moves a board piece from (srcX, srcY) to (targetX, targetY)
        ///<param name="srcX">Source x</param>
        ///<param name="srcY">Source y</param>
        ///<param name="targetX">Target x</param>
        ///<param name="targetY">Target y</param>
        private void MovePieceFromTo(int srcX, int srcY, int targetX, int targetY)
        {
            // Debug logs
            Debug.Log($"Moving piece from ({srcX}, {srcY}) to ({targetX}, {targetY})");

            // Ensure pieces array and elements are not null
            if (pieces == null)
            {
                Debug.LogError("Pieces array is null!");
                return;
            }
            if (pieces[srcX, srcY] == null)
            {
                Debug.LogError($"Source piece at ({srcX}, {srcY}) is null!");
                return;
            }
            if (pieces[targetX, targetY] != null)
            {
                Debug.Log($"Target piece at ({targetX}, {targetY}) will be replaced.");
            }

            //move the piece
            pieces[targetX, targetY] = pieces[srcX, srcY];
            pieces[srcX, srcY] = null;
            pieces[targetX, targetY].SetXY(targetX, targetY); 
            pieces[targetX, targetY].Moved();
            //check if it can be promoted
            if (pieces[targetX, targetY].canBePromoted && ((pieces[targetX, targetY].isWhite && targetY == ROWS - 1) || (pieces[targetX, targetY].isBlack && targetY == 0)))
            {
                //show promotion option
                pieces[targetX, targetY].PromoteTo(BoardPiece.Type.Queen);
            }
            //reset cells highlight
            ResetBoardCellsHighlight();
        }

        #endregion

        #region Cell Highlight
        
        // Highlight the board  cells of possible move for a player selected piece
        public void HighlightBoardCellsForPlayer(Player player)
        {
            BoardPiece boardPiece = player.selectedPiece.GetComponent<BoardPiece>();
            player.validMoves = boardPiece.GetPlayerMovesForGameBoardPieces(player, pieces);

            foreach (int[] move in player.validMoves)
            {
                _cells[move[0], move[1]].color = GameColors.cellHightLight;
            }

            if (player.validMoves.Count == 0)
            {
                _cells[boardPiece.x, boardPiece.y].color = GameColors.cellNoMoves;
            }
        }

        // Reset the highlight of board cells
        public void ResetBoardCellsHighlight()
        {
            for (int x = 0; x < COLUMS; x++)
            {
                for (int y = 0; y < ROWS; y++)
                {
                    if (_cells[x, y].color == GameColors.cellHightLight || _cells[x, y].color == GameColors.cellNoMoves)
                    {
                        _cells[x, y].color = (x % 2 == y % 2 ? GameColors.cellDark : GameColors.cellLight);
                    }
                }
            }
        }
        #endregion

        #region Player

        // Gets a list of board pieces for a given player
        ///<return>The player board pieces</return>
        ///<param name="player">The player</param>
        public List<BoardPiece> GetBoardPiecesForPlayer(Player player)
        {
            List<BoardPiece> playerPieces = new List<BoardPiece>();
            for (int x = 0; x < GameBoard.COLUMS; x++)
            {
                for (int y = 0; y < GameBoard.ROWS; y++)
                {
                    if (pieces[x, y] != null && pieces[x, y].color == player.color)
                    {
                        playerPieces.Add(pieces[x, y]);
                    }
                }
            }
            return playerPieces;
        }
        #endregion
    }
}
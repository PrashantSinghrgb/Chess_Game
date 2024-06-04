using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

namespace Game
{
    public class BoardPiece : BoardComponent
    {
        #region Properties

        // An Enum denoting the types of board pieces
        public enum Type
        {
            // The king board piece type
            King,
            // The Queen board piece type
            Queen,
            // The Rook board piece type
            Rook,
            // The Bishop board piece type
            Bishop,
            // The knight board piece type
            Knight,
            // The Pawn board piece type
            Pawn

        }

        // The piece type
        public Type type
        {
            get;
            private set;
        }
        // Whether the piece is a king
        public bool isKing
        {
            get
            {
                return type == Type.King;
            }
        }
        // Whether the piece is a queen
        public bool isQueen
        {
            get
            {
                return type == Type.Queen;
            }
        }
        // Whether the piece is a Rook
        public bool isRook
        {
            get
            {
                return type == Type.Rook;
            }
        }
        // Whether the piece is a Bishop
        public bool isBishop
        {
            get
            {
                return type == Type.Bishop;
            }
        }
        // Whether the piece is a Knight
        public bool isKnight
        {
            get
            {
                return type == Type.Knight;
            }
        }
        // Whether the piece is a Pawn
        public bool isPawn
        {
            get
            {
                return type == Type.Pawn;
            }
        }


        // The Pieces color
        public Player.Color color
        {
            get;
            private set;
        }
        // Determines if the player is playing as white
        public bool isWhite
        {
            get
            {
                return color == Player.Color.White;
            }
        }
        // Determines if the player is playing as black
        public bool isBlack
        {
            get
            {
                return color == Player.Color.Black;
            }
        }


        // Whether the piece can be captured
        public bool canBeCaptured
        {
            get
            {
                return !isKing;
            }
        }
        // Whether the piece has already been promoted
        private bool _hasBeenPromoted = false;
        // Whether the piece can be promoted
        public bool canBePromoted
        {
            get
            {
                return type == Type.Pawn && !_hasBeenPromoted;
            }
        }
        // whether the piece can jump over the piece
        private bool _canJump
        {
            get
            {
                return isKnight;
            }
        }

        // The number of times the piece has been moved
        private int _timesMoved = 0;
        // Whether the piece has already been moved
        private bool _hasBeenAlreadyMoved
        {
            get
            {
                return _timesMoved != 0;
            }
        }

        // An Array of piece sprites
        [Tooltip("Organize as Black the White, King, Queen, Rook, Bishop, Knight, Pawn")]
        [SerializeField]
        private Sprite[] _sprites;
        // The sprite renderer
        private SpriteRenderer _spriteRenderer;

        #endregion

        #region Initialization

        // EDITOR ONLY : Callback when the script is loaded or a value is changed in the inspector
        private void OnValidate()
        {
            Assert.IsNotNull(_sprites);

            for (int i = 0; i < _sprites.Length; i++)
            {
                Assert.IsNotNull(_sprites[i]);
            }

            Assert.IsTrue(_sprites.Length == Constants.NUMBER_TYPE_PIECES * 2);
        }

        // Sets the piece up
        /// <param name="type">The Pieces type</param>
        /// <param name="color">The Pieces color</param>
        /// <param name="x">The x-value</param
        /// <param name="y">The y-value</param>
        /// <param name="automaticallyUpdateTransform">Whether the transform position should be updated. Defaults to true</param>
        public void SetUp(BoardPiece.Type type, Player.Color color, int x, int y, bool automaticallyUpdateTransform = true)
        {
            // set the type, color, (x,y) and name
            this.type = type;
            this.color = color;
            SetXY(x, y, automaticallyUpdateTransform);
            name = string.Format("{0} {1}", type.ToString(), color.ToString());

            //Update the sprite
            UpdateSprite();
        }

        #endregion

        #region Methods

        // Updates the pieces sprite
        private void UpdateSprite()
        {
            // get a reference to the sprite renderer if necessary
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }

            // determine array index
            int index = (isWhite ? 0 : Constants.NUMBER_TYPE_PIECES) + (int)type;
            _spriteRenderer.sprite = _sprites[index];
        }

        // An Array of move directions for the pieces
        private List<int[]> MoveDirections
        {
            get
            {
                List<int[]> list = new List<int[]>();
                // king can move up, down, left, right and diagonally by one unity
                if (isKing)
                {
                    for (int i =- 1; i <= 1; i++)
                    {
                        for (int j =- 1; j < 1; j++)
                        {
                            if (1 != 0 && j != 0)
                            {
                                list.Add(new int[] { i, j });
                            }
                        }
                    }
                }
                // queen can move horizontally, left or right, vertically up or down, or diagonally leftup, leftdown, rightup, rightdown by any value
                else if (isQueen)
                {
                    for (int i = 1; i < GameBoard.ROWS; i++)
                    {
                        list.Add(new int[] { i, 0 });
                        list.Add(new int[] { 0, i });
                        list.Add(new int[] { -i, 0 });
                        list.Add(new int[] { 0, -i });
                    }
                    for (int i = 1; i < GameBoard.ROWS; i++)
                    {
                        list.Add(new int[] { i, i });
                        list.Add(new int[] { -i, -i });
                        list.Add(new int[] { -i, i });
                        list.Add(new int[] { i, -i });
                    }
                }
                // rook can move horizontally left or right, veritcally up or down by any value
                else if (isRook)
                {
                    for (int i = 1; i < GameBoard.ROWS; i++)
                    {
                        list.Add(new int[] { i, 0 });
                        list.Add(new int[] { 0, i });
                        list.Add(new int[] { -i, 0 });
                        list.Add(new int[] { 0, -i });
                    }
                }
                // bishop can move diagonally leftup, leftdown, rightup, rightdown by any value
                else if (isBishop)
                {
                    for (int i = 1; i < GameBoard.ROWS; i++)
                    {
                        list.Add(new int[] { i, i });
                        list.Add(new int[] { -i, -i });
                        list.Add(new int[] { -i, i });
                        list.Add(new int[] { i, -i });
                    }
                }
                else if (isKnight)
                {
                    list.Add(new int[] { -1, -2 });
                    list.Add(new int[] { -2, -1 });
                    list.Add(new int[] { -2, 1 });
                    list.Add(new int[] { -1, 2 });
                    list.Add(new int[] { 1, -2 });
                    list.Add(new int[] { 2, -1 });
                    list.Add(new int[] { 2, 1 });
                    list.Add(new int[] { 1, 2 });
                }
                // pawn can generally just move one square forward, two on the first move
                else if (isPawn)
                {
                    // one sqaure forwards
                    list.Add(new int[] { 0, (isWhite ? 1 : -1) });

                    // two square forward on first move
                    if (!_hasBeenAlreadyMoved)
                    {
                        list.Add(new int[] { 0, (isWhite ? 2 : -2) });
                    }
                    // Diagonal capture left
                    list.Add(new int[] { -1, (isWhite ? 1 : -1) });
                    // Diagonal capture right
                    list.Add(new int[] { 1, (isWhite ? 1 : -1) });
                }
                return list;
            }
        }


        // Determiones a list of valid moves for a given player with a given board setup
        /// <return>A list of the valid moves for the piece</return>
        /// <param name="player">The player</param>
        /// <param name="pieces">The gameboard pieces</param>
        
        public List<int[]> GetPlayerMovesForGameBoardPieces(Player player, BoardPiece[,] pieces)
        {
            List<int[]> validMoves = new List<int[]>();

            // loop throught the possible movement directions for the piece
            List<int[]> movementDirections = MoveDirections;

            for (int k = 0; k < movementDirections.Count; k++)
            {
                Assert.IsFalse(movementDirections[k][0] == 0 && movementDirections[k][1] == 0);
                int testX = x + movementDirections[k][0];
                int testY = y + movementDirections[k][1];

                // test for out of bounds
                if (testX < 0 || testX >= GameBoard.COLUMS || testY < 0 || testY >= GameBoard.ROWS)
                {
                    continue;
                }
                // test that it isn't a player piece
                if (pieces[ testX, testY ] != null && pieces[ testX, testY ].color == player.color)
                {
                    continue;
                }
                // Test for pawn specific conditions
                if (isPawn)
                {
                    // no pieces to capture diagonally forwards
                    if (testX != x && pieces[ testX, testY] == null)
                    {
                        continue;
                    }
                    // cannot move vertically forwards as opponents piece is blocking
                    if (testX == x && testY != y && pieces[testX, testY] != null)
                    {
                        continue;
                    }
                }
                // test that the opponent piece can be captured
                if (pieces[testX, testY] != null && pieces[testX, testY].color != player.color && !pieces[testX, testY].canBeCaptured)
                {
                    continue;
                }

                // if the piece cannot jump, test if there is a piece on the path between (x,y) and (testX, testY)
                if (!_canJump)
                {
                    if (!PieceBlocksPathBetween(x, y, testX, testY, pieces))
                    {
                        continue;
                    }
                }
                validMoves.Add(new int[] { testX, testY });
            }
            return validMoves;
        }


        // Determines if there are any pieces which block a piece moving from (x1, y1) to (x2, y2)
        ///<return> Return true if no piece are located are the path between (x1, y1) and (x2, y2) </return>
        ///<param name="x1">The first x value</param>
        ///<param name="y1">The first y value</param>
        ///<param name="x2">The second x value</param>
        ///<param name="y2">The second y value</param>
        ///<param name="pieces">The gameboard pieces</param>
        
        private bool PieceBlocksPathBetween(int x1, int y1, int x2, int y2, BoardPiece[,] pieces)
        {
            Assert.IsFalse(x1 == x2 && y1 == y2);

            // determine the change in position for x and y. Calculate the maximum search direction
            int distanceX = x2 - x1;
            int distanceY = y2 - y1;
            int max = Mathf.Max(Mathf.Abs(distanceX), Mathf.Abs(distanceY));

            // loop form 1 to max -1
            for (int inBetweenMove = 1; inBetweenMove <= max-1; inBetweenMove++)
            {
                // determine the current step(tX, tY) position
                int tX = distanceX == 0 ? x1 : x1 + inBetweenMove * (distanceX > 0 ? 1 : -1);
                int tY = distanceY == 0 ? y1 : y1 + inBetweenMove * (distanceY > 0 ? 1 : -1);

                // if ther is a piece at this position - return false
                if (pieces[tX, tY] != null)
                {
                    return false;
                }
            }

            // no piece block the path - return true
            return true;
        }

        // The pieces was moved. Update Variables
        public void Moved()
        {
            _timesMoved++;
        }

        // The piece was removed from the board. Update Variables
        public void RemovedFromBoard()
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }

        // Promotes the pieces to a given type
        ///<param name="type">The type to promote to</param>
        
        public void PromoteTo(BoardPiece.Type type)
        {
            Assert.IsTrue(type != Type.King && type != Type.Pawn, string.Format("{0} is an invalid type to promote to", type.ToString()));

            this.type = type;
            name = string.Format("{0} (Promoted Pawn) {1}", type.ToString(), color.ToString());
            UpdateSprite();
        }

        // Get the text character representation of the piece
        public string text
        {
            get
            {
                if (color==Player.Color.Black)
                {
                    switch (type)
                    {
                        case Type.King:
                            return "♚";
                        case Type.Queen:
                            return "♛";
                        case Type.Rook:
                            return "♜";
                        case Type.Bishop:
                            return "♝";
                        case Type.Knight:
                            return "♞";
                        case Type.Pawn:
                            return "♟";
                    }
                }
                else
                {
                    switch (type)
                    {
                        case Type.King:
                            return "♚";
                        case Type.Queen:
                            return "♛";
                        case Type.Rook:
                            return "♜";
                        case Type.Bishop:
                            return "♝";
                        case Type.Knight:
                            return "♞";
                        case Type.Pawn:
                            return "♟";
                    }
                }
                return " ";
            }
        }
        #endregion
    }
}
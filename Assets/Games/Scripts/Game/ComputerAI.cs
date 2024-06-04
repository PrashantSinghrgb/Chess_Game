using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public class ComputerAI : MonoBehaviour
    {

        #region Properties

        [SerializeField]
        private GameBoard _gameBoard;

        #endregion

        #region Initialization

        // EDITOR ONLY : callback when the script is loaded or a value is changed in the inspector
        private void OnValidate()
        {
            Assert.IsNotNull(_gameBoard);
        }

        #endregion

        #region Methods

        // Makes a move for a given player
        ///<param name="player">The player</param>
        public void MakeMoveForPlayer(Player player)
        {
            MakeRandomMoveForPlayer(player);
        }

        // Makes a random move for a given player
        /// <param name="player">The player</param>
        
        private void MakeRandomMoveForPlayer(Player player)
        {
            // get a list of the players pieces and choose a random piece
            List<BoardPiece> playerPieces = _gameBoard.GetBoardPiecesForPlayer(player);
            // choose a random piece that has at least one valid move
            BoardPiece randomPiece;
            do
            {
                randomPiece = playerPieces[Random.Range(0, playerPieces.Count)];
                player.validMoves = randomPiece.GetPlayerMovesForGameBoardPieces(player, _gameBoard.pieces);
            } while (player.validMoves.Count == 0);

            // choose a random move and determine its [dX, dY]
            int[] randomMove = player.validMoves[Random.Range(0, player.validMoves.Count)];

            Debug.Log($" {randomPiece.name} ( {randomPiece.x} , {randomPiece.y} ) -> ( {randomMove[0]}, {randomMove[1]} )");

            player.selectedPiece = randomPiece;
            _gameBoard.TryPlayerMove(player, randomMove[0], randomMove[1]);
        }
        #endregion
    }
}
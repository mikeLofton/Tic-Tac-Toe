using System;
using System.Collections.Generic;
using System.Text;

namespace Tic_Tac_Toe
{
    class Board
    {
        private char _player1Token;
        private char _player2Token;
        private char _currentToken;
        private char[,] _board;
        private int _currentScene = 0;
        private int _turnCount = 0;

        /// <summary>
        /// Initializes player tokens and the game board
        /// </summary>
        public void Start()
        {
            _player1Token = 'x';
            _player2Token = 'o';
            _currentToken = _player1Token;
            _board = new char[3, 3] { { '1', '2', '3'}, { '4', '5', '6'}, { '7', '8', '9'} };
        }

        /// <summary>
        /// Gets the input from the player.
        /// Sets the player token at the desired spot in the 2D array.
        /// Checks if there is a winner.
        /// Changes the current token in play.
        /// </summary>
        public void Update()
        {
           switch (_currentScene)
            {
                case 0:
                    CheckWinner(_player1Token);
                    CheckWinner(_player2Token);
                    Console.WriteLine($"Current Turn: {_currentToken}");
                    Console.WriteLine($"Turn Count: {_turnCount}");
                    SetToken(_currentToken, 1, 1);

                    if (_currentToken == _player1Token)
                        _currentToken = _player2Token;
                    else
                        _currentToken = _player1Token;
                    break;

                case 1:
                    ClearBoard();
                    break;
            }
           
        }

        /// <summary>
        /// Draws the board and let's the players know whose turn it is
        /// </summary>
        public void Draw()
        {
            Console.WriteLine($" {_board[0, 0]} | {_board[0, 1]} | {_board[0, 2]} \n " +
                $"__________\n" +
                $" {_board[1, 0]} | {_board[1, 1]} | {_board[1, 2]} \n " +
                $"__________\n" +
                $" {_board[2, 0]} | {_board[2, 1]} | {_board[2, 2]}");
        }

        public void End()
        {

        }

        /// <summary>
        /// Assigns the spot at the given indices in the board array to be the given token.
        /// </summary>
        /// <param name="token">The token to set the array index to.</param>
        /// <param name="posX">The x position of the token.</param>
        /// <param name="posY">The y position of the token.</param>
        /// <returns>Return false if the indicies are out of range.</returns>
        public bool SetToken(char token, int posX, int posY)
        {
            int mark = Game.GetInput();

            if (mark >= 0 && mark <= 9)
            {
                posX = mark / 3;
                posY = mark % 3;

                _board[posX, posY] = token;
                _turnCount++;
                return true;
            }   
            else
            {
                Console.WriteLine("Invalid Input");
                Console.ReadKey(true);
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the token apears three times consecutively vertically, horizontally, or diagonally.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool CheckWinner(char token)
        {
            //Win Vertically
            for (int i = 0; i < _board.GetLength(0); i++)
            {
              if (_board[i, 0] == token && _board[i, 1] == token && _board[i, 2] == token)
              {
                    Console.WriteLine($"{token} WINS!");
                    _currentScene = 1;
                    return true;
              }
            }

            //Win Horizontally
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                if (_board[0, j] == token && _board[1, j] == token && _board[2, j] == token)
                {
                    Console.WriteLine($"{token} WINS!");
                    _currentScene = 1;
                    return true;
                }
            }

            //Win Diagonally
            if (_board[0, 0] == token && _board[1, 1] == token && _board[2, 2] == token)
            {
                Console.WriteLine($"{token} WINS!");
                _currentScene = 1;
                return true;
            }
            else if (_board[0, 2] == token && _board[1, 1] == token && _board[2, 0] == token)
            {
                Console.WriteLine($"{token} WINS!");
                _currentScene = 1;
                return true;
            }

            if (_turnCount >= 9)
            {
                Console.WriteLine("Draw");
                _currentScene = 1;
                return true;
            }
         

            return false;
        }

        /// <summary>
        /// Resets the board to it's default state.
        /// </summary>
        public void ClearBoard()
        {
            Console.WriteLine("Do you want to play again? \n 1. Yes \n 2. No");
            int input = Game.GetInput();
            if (input == 0)
            {
                Console.Clear();
                _currentToken = _player1Token;
                _board = new char[3, 3] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
                _turnCount = 0;
                _currentScene = 0;
            }
            else if (input == 1)
            {
                Game._gameOver = true;
            }
        }
    }
}

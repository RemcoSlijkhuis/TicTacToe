﻿using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using TicTacToeShared;

namespace TicTacToeServer.Controllers
{
    /// <summary>
    /// HTTP Controller for the game server.
    /// Assumptions (are the mother of all f*ckups ... ):
    /// 
    /// - The server is always player 1
    /// - The server is responsible for the game state, the player has no influence over that
    /// - The player will need to do its own validation on the moves
    /// 
    /// </summary>
    [RoutePrefix("game")]
    public class GameController : ApiController
    {
        /// <summary>
        /// To keeps stats we needs to know who's playing who. To do this all communication will be done using a GUID that's generated by the server.
        /// This GUID needs to be in the header of each method. If the name of a player already exists, the player will be overwritten
        /// </summary>
        [HttpGet]
        [Route("register")]
        public HttpResponseMessage Register([FromUri]string name)
        {
            if(string.IsNullOrEmpty(name))
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Name cannot be empty", Encoding.UTF8, "text/plain")
                };

            GameStateHelper.AddRequest();

            var existingPlayer = GameStateHelper.GetPlayerId(name);

            if (existingPlayer != Guid.Empty)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(existingPlayer.ToString(), Encoding.UTF8, "text/plain")
                };
            }

            var playerId = Guid.NewGuid();

            while (GameStateHelper.Games.ContainsKey(playerId))
                playerId = Guid.NewGuid();

            GameStateHelper.RegisterNewPlayer(playerId, name);
            
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(playerId.ToString(), Encoding.UTF8, "text/plain")
            };
        }

        /// <summary>
        /// If the client has no running game, it should initiate one by using a GET.
        /// This method will randomly choose a starting player. If the starting player is the server, it will make a move directly
        /// </summary>
        [HttpGet]
        [Route("startGame")]
        public HttpResponseMessage StartGame([FromUri]string playerId)
        {
            try
            {
                GameStateHelper.AddRequest();

                var playerIdGuid = Guid.Parse(playerId);
                var playerState = GameStateHelper.Games[playerIdGuid];

                playerState.StartNewGame();

                var returnState = "";

                if (playerState.CurrentGame.CurrentPlayerIndex == 1)
                {
                    var move = playerState.CurrentGame.PlayOneMove();

                    returnState = playerState.CurrentGame.ToString();
                    returnState += "," + move;
                }
                else
                {
                    returnState = playerState.CurrentGame.ToString();
                }
                    

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(returnState, Encoding.UTF8, "text/plain")
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("makeMove")]
        public HttpResponseMessage MakeMove([FromUri]int boardColumn, [FromUri]int boardRow,
                                            [FromUri]int cellColumn, [FromUri]int cellRow, 
                                            [FromUri]string playerId)
        {
            try
            {
                GameStateHelper.AddRequest();

                var playerIdGuid = Guid.Parse(playerId);
                var playerState = GameStateHelper.Games[playerIdGuid];

                if (playerState.CurrentGame.GameResult != 0)
                    throw new Exception("Game is already finished, start a new one!");

                if (playerState.CurrentGame.CurrentPlayerIndex == 1)
                    throw new Exception("DEBUG -- Corruption in turns!");

                var move = new Move(boardColumn, boardRow, cellColumn, cellRow);
                if (!move.ConstraintsAreValid())
                    throw new Exception("This is not a valid move");

                playerState.Player2.PrepareNextMove(move);
                playerState.CurrentGame.PlayOneMove();

                var returnState = "";
                Move serverMove = null;

                if (playerState.CurrentGame.GameResult == 0)
                    serverMove = playerState.CurrentGame.PlayOneMove();

                if (playerState.CurrentGame.GameResult == 0)
                {
                    returnState = playerState.CurrentGame.ToString();
                    returnState += "," + serverMove;
                }
                else
                {
                    playerState.Stats.AddResult(playerState.CurrentGame.GameResult);

                    returnState = playerState.CurrentGame.ToString();
                    returnState += playerState.CurrentGame.GameResult.ToString(CultureInfo.InvariantCulture);

                    if (serverMove != null)
                        returnState += "," + serverMove;
                }

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(returnState, Encoding.UTF8, "text/plain")
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("getAvailableMoves")]
        public HttpResponseMessage GetAvailableMoves([FromUri]string playerId)
        {
            try
            {
                GameStateHelper.AddRequest();

                var playerIdGuid = Guid.Parse(playerId);
                var playerState = GameStateHelper.Games[playerIdGuid];

                if (playerState.CurrentGame.GameResult != 0)
                    throw new Exception("Game is already finished, start a new one!");

                if (playerState.CurrentGame.CurrentPlayerIndex == 1)
                    throw new Exception("DEBUG -- Corruption in turns!");

                var moves = playerState.CurrentGame.Board.GetAvailableMoves();

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(moves.ToMovesString(), Encoding.UTF8, "text/plain")
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}

﻿using System.Net.Sockets;

namespace ServerProject
{
    /// <summary>
    /// the model iterface of the MVC.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IModel<T>
    {
        /// <summary>
        /// set the controller of the MVC.
        /// </summary>
        /// <param name="cont">the controller of the game</param>
        void SetController(IController cont);

        /// <summary>
        /// generate annew game.
        /// </summary>
        /// <param name="name">the name of the game</param>
        /// <param name="x">number of colloms</param>
        /// <param name="y">number of rows</param>
        /// <returns>the new maze</returns>
        T Generate(string name, int x, int y);

        /// <summary>
        /// solve a maze with a search algorithm.
        /// </summary>
        /// <param name="name">the name of the maze</param>
        /// <param name="searcher">the seaech algorithm for solving the maze</param>
        /// <returns>the solution for the maze</returns>
        string Solve(string name, int searcher);

        /// <summary>
        /// start a multi player game.
        /// </summary>
        /// <param name="client">the client that will be player1</param>
        /// <param name="name">the name of the game</param>
        /// <param name="x">number of rows.</param>
        /// <param name="y">number off cols</param>
        /// <returns>returns the game maze</returns>
        T Start(TcpClient client, string name, int x, int y);

        /// <summary>
        /// join a game.
        /// </summary>
        /// <param name="client">player2</param>
        /// <param name="name">name of game to join.</param>
        /// <returns>the game.</returns>
        T Join(TcpClient client, string name);

        /// <summary>
        /// list of games that are ready to join.
        /// </summary>
        /// <returns>the list</returns>
        string List();

        /// <summary>
        /// get a multiplayer game.
        /// </summary>
        /// <param name="player">the player</param>
        /// <returns>the game</returns>
        MultiPlayerGame GetGame(TcpClient player);
    }
}

using System.Collections.Generic;
using System.Linq;
using MazeLib;
using SearchAlgorithmsLib;
using MazeGeneratorLib;
using Main;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;

namespace ServerProject
{
    class MazeModel : IModel<Maze>
    {
        private IController controller;
        private Dictionary<string, Maze> singleMazes;
        private Dictionary<string, string> solvedMazes;
        private Dictionary<string, Maze> toJoinMazes;
        private Dictionary<string, MultiPlayerGame> multiGames;
        private Dictionary<string, Maze> playingMazes;

        public MazeModel()
        {
            singleMazes = new Dictionary<string, Maze>();
            solvedMazes = new Dictionary<string, string>();
            toJoinMazes = new Dictionary<string, Maze>();
            multiGames = new Dictionary<string, MultiPlayerGame>();
            playingMazes = new Dictionary<string, Maze>();
        }

        public void SetController(IController cont)
        {
            controller = cont;
        }

        public Maze Generate(string name, int x, int y)
        {
             // override the old maze if exists.
            if (singleMazes.ContainsKey(name))
            {
                singleMazes.Remove(name);
                if (solvedMazes.ContainsKey(name))
                {
                    solvedMazes.Remove(name);
                }
            }
            IMazeGenerator gen = new DFSMazeGenerator();
            Maze maze = gen.Generate(x, y);
            maze.Name = name;
            singleMazes.Add(name, maze);
            return singleMazes[name];
        }

        public string Solve(string name, int searcher)
        {
            if (!singleMazes.ContainsKey(name))
            {
                return "maze not found";
            }
            if (searcher != 0 && searcher != 1)
            {
                return "no algorithm";
            }
            if (!solvedMazes.ContainsKey(name))
            {
                AddSolution(name, searcher);
            }
            return solvedMazes[name];
        }

        private void AddSolution(string name, int searcher)
        {
            Solution<State<Position>> solution = new Solution<State<Position>>();
            int nodesEv = 0;
            ISearchable<Position> srMaze = new MazeSearchable(singleMazes[name]);
             if (searcher == 0)
            {
                ISearcher<Position> bfs = new Bfs<Position>();
                solution = bfs.Search(srMaze);
                nodesEv = bfs.GetNumberOfNodesEvaluated();
            }
            else
            {
                ISearcher<Position> dfs = new Dfs<Position>();
                solution = dfs.Search(srMaze);
                nodesEv = dfs.GetNumberOfNodesEvaluated();
            }
            string strSol = " ";
            for (int i = solution.SolLst.Count - 1; i > 0; i--)
            {
                if (solution.SolLst[i].state.Row > solution.SolLst[i - 1].state.Row)
                {
                    strSol += "2";
                }
                else if (solution.SolLst[i].state.Row < solution.SolLst[i - 1].state.Row)
                {
                    strSol += "3";
                }
                else if (solution.SolLst[i].state.Col > solution.SolLst[i - 1].state.Col)
                {
                    strSol += "0";
                }
                else if (solution.SolLst[i].state.Col < solution.SolLst[i - 1].state.Col)
                {
                    strSol += "1";
                }
            }
            JObject sol = new JObject();
            sol.Add("Name", name);
            sol.Add("Solution", strSol);
            sol.Add("NodesEvaluated", nodesEv);
            string Jsol = JsonConvert.SerializeObject(sol);

             JsonConvert.DeserializeObject(Jsol);

            solvedMazes.Add(name, Jsol);
        }

        public Maze Start(TcpClient client, string name, int x, int y)
        {
            if (toJoinMazes.ContainsKey(name))
            {
                // error.
                return null;
            }
            IMazeGenerator gen = new DFSMazeGenerator();
            Maze maze = gen.Generate(x, y);
            maze.Name = name;
            toJoinMazes.Add(name, maze);

            MultiPlayerGame game = new MultiPlayerGame(client, name);
            multiGames.Add(name, game);
            game.WaitForJoin();
            // we got another player.
            return playingMazes[name];
        }

        public Maze Join(TcpClient client, string name)
        {
            if (!toJoinMazes.ContainsKey(name))
            {
                // error. we dont have such game available.
                return null;
            }
            playingMazes.Add(name, toJoinMazes[name]);
            toJoinMazes.Remove(name);

            multiGames[name].SetSecondPlayer(client);
            
            return playingMazes[name];
        }

        public string List()
        {
            JArray Jsol = new JArray(toJoinMazes.Keys);
            return Jsol.ToString();
        }

        public MultiPlayerGame GetGame(TcpClient player)
        {
            MultiPlayerGame game = null;
            for (int i = 0; i < multiGames.Count; i++)
            {
                game = multiGames.ElementAt(i).Value;
                if (game.IsPlayer(player))
                {
                    return game;
                }
            }
            return null;
        }
    }
}

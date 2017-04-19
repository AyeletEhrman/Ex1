using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;
using MazeGeneratorLib;
using Main;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServerProject
{
    class MazeModel : IModel<Maze>
    {
        private Dictionary<string, Maze> mazes;
        private Dictionary<string, string> solvedMazes;
        private IController controller;
        public MazeModel()
        {
            mazes = new Dictionary<string, Maze>();
            solvedMazes = new Dictionary<string, string>();
        }
        public Maze Generate(string name, int x, int y)
        {
            if (!mazes.ContainsKey(name))
            {
                IMazeGenerator gen = new DFSMazeGenerator();
                Maze maze = gen.Generate(x, y);
                maze.Name = name;
                mazes.Add(name, maze);
            }
            return mazes[name];
           // controller.NotifyGenerated(name);
        }
       /* public Maze GetGeneratedObj(string name)
        {
            return mazes[name];
        }*/
        public string Solve(string name, int searcher)
        {
            if (!mazes.ContainsKey(name))
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
            ISearchable<Position> srMaze = new MazeSearchable(mazes[name]);
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
            solvedMazes.Add(name, Jsol);
        }
        public void SetController(IController cont)
        {
            controller = cont;
        }
    
    }
}

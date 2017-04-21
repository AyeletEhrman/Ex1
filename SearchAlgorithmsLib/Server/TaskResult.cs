
namespace ServerProject
{
    /// <summary>
    /// resutr from a given task
    /// </summary>
    class TaskResult
    {
        /// <summary>
        /// solution in json format.
        /// </summary>
        public string JsonSol { get; set; }
        /// <summary>
        /// boolean that tell if to stay connected or not.
        /// </summary>
        public bool StayConnected { get; set; }

        /// <summary>
        /// constroctor of task result
        /// </summary>
        /// <param name="Jsol">solution in jsol format</param>
        /// <param name="sc">boolea for staying connected.</param>
        public TaskResult(string Jsol, bool sc)
        {
            JsonSol = Jsol;
            StayConnected = sc;
        }
    }
}

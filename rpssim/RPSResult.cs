using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpssim
{
    public class RPSResult
    {
        public long TotalTieGames { get; set; }

        public long TotalPlayerOneWins { get; set; }

        public long TotalPlayerOneLosses { get; set; }

        public long TotalPlayerTwoWins { get; set; }

        public long TotalPlayerTwoLosses { get; set; }

        public long TotalSimulationsRun { get; set; }

        public long TimeElapsedDuringSimulation { get; set; }

        public long RockWinCount { get; set; }

        public long PaperWinCount { get; set; }

        public long ScissorWinCount { get; set; }
    }
}

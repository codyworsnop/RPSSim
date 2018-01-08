using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace rpssim
{
    public class RPSEngine
    {
        private Dictionary<Tuple<RPSEnum, RPSEnum>, bool> WinStateDictionary = new Dictionary<Tuple<RPSEnum, RPSEnum>, bool>();

        private int playerOne = 1;

        private int playerTwo = 2;

        private Stopwatch simTimer;

        public RPSResult RunSimulation(long NumberOfSimsToRun)
        {
            InitializeWinStates();
            var result = new RPSResult();
            simTimer = new Stopwatch();
            var rand = new Random();

            simTimer.Start();

            for (int simNumber = 0; simNumber < NumberOfSimsToRun; simNumber++)
            {
                result.TotalSimulationsRun++;

                //randomize player hands
                var p1 = rand.Next(0, 3);
                var p2 = rand.Next(0, 3);

                var winner = DetermineWinner((RPSEnum)p1, (RPSEnum)p2, result);

                UpdateResults(result, winner);
            }

            simTimer.Stop();
            result.TimeElapsedDuringSimulation = simTimer.ElapsedMilliseconds;

            return result;
        }

        private void UpdateResults(RPSResult result, int roundWinner)
        {
            if (roundWinner == playerOne)
            {
                result.TotalPlayerOneWins++;
                result.TotalPlayerTwoLosses++;
            }
            else if (roundWinner == playerTwo)
            {
                result.TotalPlayerTwoWins++;
                result.TotalPlayerOneLosses++;
            }
            else
            {
                result.TotalTieGames++;
            }
        }

        private int DetermineWinner(RPSEnum playerOneChoice, RPSEnum playerTwoChoice, RPSResult result)
        {
            bool output;

            if (WinStateDictionary.TryGetValue(Tuple.Create(playerOneChoice, playerTwoChoice), out output))
            {
                if (playerOneChoice == RPSEnum.Rock)
                {
                    result.RockWinCount++;
                }
                else if (playerOneChoice == RPSEnum.Paper)
                {
                    result.PaperWinCount++;
                }
                else
                {
                    result.ScissorWinCount++;
                }

                return playerOne;
            }
            else if (WinStateDictionary.TryGetValue(Tuple.Create(playerTwoChoice, playerOneChoice), out output))
            {
                if (playerTwoChoice == RPSEnum.Rock)
                {
                    result.RockWinCount++;
                }
                else if (playerTwoChoice == RPSEnum.Paper)
                {
                    result.PaperWinCount++;
                }
                else
                {
                    result.ScissorWinCount++;
                }

                return playerTwo;
            }

            return 0;
        }

        private void InitializeWinStates()
        {
            WinStateDictionary.Add(Tuple.Create(RPSEnum.Rock, RPSEnum.Scissors), true);
            WinStateDictionary.Add(Tuple.Create(RPSEnum.Paper, RPSEnum.Rock), true);
            WinStateDictionary.Add(Tuple.Create(RPSEnum.Scissors, RPSEnum.Paper), true);
        }
    }
}

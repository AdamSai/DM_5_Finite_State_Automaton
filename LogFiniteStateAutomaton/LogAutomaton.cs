using System;
using System.Collections.Generic;
using System.Text;

namespace LogFiniteStateAutomaton
{
    class LogAutomaton
    {
        private State _startState;

        public LogAutomaton(State startState)
        {
            _startState = startState;
        }

        // TODO: Maybe continue running if we find illegal state, and see if it's last state is in a final state
        // TODO: Could check recursively from the starting state and try to find the state to continue from
        public void ValidateStates(List<LogEntry> logEntries)
        {
            Console.WriteLine($"======================");
            Console.WriteLine($"Validating log entries for instance \"{logEntries[0].InstanceId}\"");

            var currState = _startState;
            for (var i = 0; i < logEntries.Count; i++)
            {
                var currEntry = logEntries[i];
                currState = currState.MoveNext(currEntry.ActionId);
                if (currState == null)
                {
                    Console.WriteLine("Action failed. Illegal state change.");

                    if (i == 0)
                    {
                        Console.WriteLine($"Illegal state occurred as first action was: \"{currEntry.ActionId}\"");
                    }
                    else
                    {
                        Console.WriteLine($"Illegal state occurred when trying to go from action \"{logEntries[i - 1].ActionId}\" to action \"{currEntry.ActionId}\"");
                    }
                    break;
                }
                if (currState.IsFinalState && i == logEntries.Count - 1)
                {
                    Console.WriteLine($"No issues found with instance id: \"{currEntry.InstanceId}\"");
                }
                else if (i == logEntries.Count - 1)
                {
                    Console.WriteLine($"Instance \"{currEntry.InstanceId}\" still running at action \"{currEntry.ActionId}\"");
                }
            }
        }
    }
}

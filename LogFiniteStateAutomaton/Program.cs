using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace LogFiniteStateAutomaton
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<List<LogEntry>> logEntriesList = new List<List<LogEntry>>();
            string line;
            // Change this to match your filepath
            var path = @"C:\Users\Adam\source\repos\LogFiniteStateAutomaton\LogFiniteStateAutomaton\log.txt";
            var file = new System.IO.StreamReader(path);

            // The current line the Stream reader is on
            int currLine = 0;

            // Used to track when we read a new instance.
            int currInstanceId = 0;
            // List of log entries with the same instance id
            List<LogEntry> logEntries = new List<LogEntry>();

            // Go through the log file, line by line, and combine lines with the same InstanceId into their own files.
            while ((line = file.ReadLine()) != null)
            {
                // Skip first line of the CSV file
                if (currLine == 0)
                {
                    currLine++;
                    continue;
                }
                var csv = line.Split(',');
                var level = csv[0];
                var systemId = int.Parse(csv[1]);
                var instanceId = int.Parse(csv[2]);
                var actionId = int.Parse(csv[3]);
                var timeStamp = long.Parse(csv[4]);
                var entry = new LogEntry(level, systemId, instanceId, actionId, timeStamp);

                // If a new instance has appeared. Add the current list of entries to the list of lists.
                if (instanceId != currInstanceId)
                {
                    currInstanceId = instanceId;
                    logEntriesList.Add(logEntries);
                    logEntries = new List<LogEntry>();
                }
                logEntries.Add(entry);
                currLine++;
            }
            // Because I read the file line by line, I couldn't add the final log entry in the while loop
            // TODO: Change the while loop to also look at the next line, so I can check if it is null, and then add the final log entry there
            logEntriesList.Add(logEntries);

            // Create states
            var initial = new State(-1, false);
            var login = new State(0, false);
            var viewProfile = new State(1, false);
            var changePic = new State(2, false);
            var logout = new State(3, true);

            // Add edges to the states
            initial.NextStates.Add(login);
            login.NextStates.Add(viewProfile);
            viewProfile.NextStates.Add(logout);
            viewProfile.NextStates.Add(changePic);
            changePic.NextStates.Add(viewProfile);
            changePic.NextStates.Add(logout);

            var automaton = new LogAutomaton(initial);

            // Validate the states
            foreach (var entry in logEntriesList)
            {
                automaton.ValidateStates(entry);
            }

        }
    }
}

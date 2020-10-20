using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace LogFiniteStateAutomaton
{
    class State
    {
        private readonly int _id;

        public List<State> NextStates { get; }
        public bool IsFinalState { get; }

        public State(int id, bool isFinalState)
        {
            this._id = id;
            IsFinalState = isFinalState;
            NextStates = new List<State>();
        }

        /// <summary>
        /// Try to move to a valid state
        /// </summary>
        /// <param name="nextStateId">The id of the next state to move to.</param>
        /// <returns>Returns a valid state if possible, otherwise return null.</returns>
        public State MoveNext(int nextStateId)
        {
            var nextState = NextStates.Find(x => x._id == nextStateId);
            return nextState;
        }

        public override bool Equals(object? obj)
        {
            if (obj is State state)
            {
                return state._id == _id && state.NextStates == NextStates;
            }
            return base.Equals(obj);
        }
    }
}

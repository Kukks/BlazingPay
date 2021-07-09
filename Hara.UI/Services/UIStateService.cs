using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hara.Abstractions.Contracts;

namespace Hara.UI.Services
{
    public class UIStateService : IUIStateService
    {
        private string _title;
        private Stack<Func<Task>> _stackedStates = new Stack<Func<Task>>();
      
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                StateChanged?.Invoke();
            }
        }

        public bool AnyStackState => _stackedStates.Any();

        public Task InvokeStackState()
        {
            if (AnyStackState)
            {
                return _stackedStates.Pop().Invoke().ContinueWith(task => StateChanged?.Invoke());
            }

            return Task.CompletedTask;
        }

        public Task PushStackState(Func<Task> act)
        {
            _stackedStates.Push(act);
            StateChanged?.Invoke();
            return Task.CompletedTask;
        }

        public Task ClearStack()
        {
            if (AnyStackState)
            {
                _stackedStates.Clear();
                StateChanged?.Invoke();
            }
            
            return Task.CompletedTask;
        }

        public event Action StateChanged;
    }
}
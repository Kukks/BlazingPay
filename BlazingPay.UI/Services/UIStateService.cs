using System;
using System.Collections.Generic;
using BlazingPay.Abstractions.Contracts;
using Microsoft.Extensions.Primitives;

namespace BlazingPay.UI.Services
{
    public class UIStateService : IUIStateService
    {
        private string _title;
      
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                StateChanged?.Invoke();
            }
        }

      

        public string EntryData { get; set; }
        public Dictionary<string, StringValues> Form { get; set; }

        public event Action StateChanged;
    }
}
using System;
using BlazingPay.Abstractions.Contracts;
using Microsoft.AspNetCore.Http;

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
        public IFormCollection Form { get; set; }

        public event Action StateChanged;
    }
}
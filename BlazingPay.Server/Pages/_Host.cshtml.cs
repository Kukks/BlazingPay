using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazingPay.Abstractions.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazingPay.Server.Pages
{
    [IgnoreAntiforgeryToken]
    public class HostPage :PageModel
    {
        private readonly IUIStateService _uiStateService;

        public HostPage(IUIStateService uiStateService)
        {
            _uiStateService = uiStateService;
        }
        public void OnGet()
        {

        }
        
        public async Task OnPostAsync()
        {
            if (Request.HasFormContentType)
            {
                _uiStateService.Form = Request.Form.ToDictionary(pair => pair.Key, pair => pair.Value);
            }
            if (Request.ContentType == "application/json")
            {
                using var reader = new StreamReader(Request.Body);
                _uiStateService.EntryData = await reader.ReadToEndAsync();
            }
        }
    }
}
namespace BlazingPay.UI.Services
{


    public class UIPreferences
    {
        public virtual bool? DarkMode { get; set; } = true;
        public virtual string DefaultInstanceId { get; set; }
        
    }
}
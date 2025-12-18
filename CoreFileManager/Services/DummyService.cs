namespace CoreFileManager.Services
{
    public class DummyService : IDummyService
    {
        public string GetInfo()
        {
            return "Service çalışıyor - " + DateTime.Now;
        }
    }
}

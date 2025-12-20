namespace DrinkBuddy.Application.IntegrationInterfaces.SemKer
{
    public interface ISemKerClient
    {
        Task<string> SendAsync(SemKerRequest request);
    }
}

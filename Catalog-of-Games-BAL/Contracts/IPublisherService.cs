namespace Catalog_of_Games_BAL.Contracts
{
    public interface IPublisherService
    {
        Task DeleteByNameAsync(string name);
    }
}
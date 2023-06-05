using Catalog_of_Games_BAL.Contracts;
using Catalog_of_Games_DAL.Repositories.Contracts;

namespace Catalog_of_Games_BAL.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork unitOfWork;

        public PublisherService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task DeleteByNameAsync(string name)
        {
            await unitOfWork.PublisherRepository.DeleteByNameAsync(name);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
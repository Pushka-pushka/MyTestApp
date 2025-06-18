using MyTestApp.Domain.Repositories.Abstract;

namespace MyTestApp.Domain
{
    public class DataManager
    {
        public IServiceCategoriesRepository ServiceCategories { get; set; } 

        public IServicesRepository Services { get; set; }

        public DataManager(IServiceCategoriesRepository ServiceCategoriesRepository,
            IServicesRepository ServicesRepository)
        {
            ServiceCategories = ServiceCategoriesRepository;
            Services= ServicesRepository;


        }
    }
}

namespace MyTestApp.Domain.Entities
{
    public class ServiceCategory: EntitiBase
    {

       public ICollection<Service>? Services { get; set; }
    }
}

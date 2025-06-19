using MyTestApp.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace MyTestApp.Domain.Entities
{
    public class Service: EntitiBase
    {

        [Display(Name = "Выбери категорию, к которой относится услуга!")]
        public int? ServicesCateforyId { get; set; }
        public ServiceCategory? ServiceCategory { get; set; }

        [Display (Name ="краткое пояснение че каво")]
        [MaxLength(3000)]
        public string? DescriptionShort { get; set; }


        [Display(Name = "Описание")]
        [MaxLength(100000)]
        public string? Description { get; set; }

        [Display(Name ="картинОЧКА")]
        [MaxLength (300)]
        public string? Photo {  get; set; }

        [Display(Name = "Тип услуги")]
        public ServiceTypeEnum Type { get; set; }

    }
}

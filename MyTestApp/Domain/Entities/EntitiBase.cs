using System.ComponentModel.DataAnnotations;

namespace MyTestApp.Domain.Entities
{
    public  abstract class EntitiBase
    {

        public int Id { get; set; }


        [Required(ErrorMessage ="Заполни название!")]
        [Display(Name ="Название")]
        [MaxLength(200)]
        public string? Title {  get; set; }

        public DateTime DataCreated { get; set; } = DateTime.UtcNow; //+3
    }
}

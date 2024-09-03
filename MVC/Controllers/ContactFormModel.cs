using System.ComponentModel.DataAnnotations;

namespace ROCNSBE.MVC.Controllers
{

  public partial class FormsController
  {
    public class ContactFormModel
    {
      [MaxLength(55)]
      [Required]
      public string? Name { get; set; }
      [Required]
      [EmailAddress]
      public string? Email { get; set; }
      [Required]
      [MaxLength(1000)]
      public string? Message { get; set; }

      [MaxLength(55)]
      public string? Company { get; set; }
      [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "The PhoneNumber field is not a valid phone number")]
      public string? Phone { get; set; }

    }

  }

}

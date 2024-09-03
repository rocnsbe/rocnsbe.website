using System.ComponentModel.DataAnnotations;

namespace ROCNSBE.MVC.Controllers
{

  public partial class FormsController
  {
    public class MailingListFormModel
    {

      [EmailAddress]
      [Required]
      public string? Email { get; set; }
    }

  }

}

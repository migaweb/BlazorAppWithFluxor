using BlazorAppWithFluxor.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppWithFluxor.Server.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class FeedbackController : ControllerBase
  {
    [HttpPost]
    public async Task Post(UserFeedbackModel model)
    {
      var email = model.EmailAddress;
      var rating = model.Rating;
      var comment = model.Comment;

      await Task.Delay(2000);

      Console.WriteLine($"Received rating {rating} from {email} with comment '{comment}'");
    }
  }
}

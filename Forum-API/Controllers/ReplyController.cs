using Microsoft.AspNetCore.Mvc;

namespace Forum_API.Controllers
{

    [Route("api/posts")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly ILogger<ReplyController> logger;

        public ReplyController(ILogger<ReplyController> logger)
        {
            this.logger = logger;
        }

        // Додання коментаря до поста

    }
}

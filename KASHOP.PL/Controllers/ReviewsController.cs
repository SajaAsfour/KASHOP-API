using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Request;
using KASHOP.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ReviewsController(IReviewService reviewService ,IStringLocalizer<SharedResources> localizer)
        {
            _reviewService = reviewService;
            _localizer = localizer;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddReview([FromBody] AddReviewRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _reviewService.AddReviewAsync(userId, request);
            if (!result) return BadRequest();

            return Ok(new
            {
                message = _localizer["Success"].Value,
            });
        }
    }
}

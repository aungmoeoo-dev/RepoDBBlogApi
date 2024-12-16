using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoDBBlogApi.Shared;

namespace RepoDBBlogApi.RestApi.Features.Blog;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{

	private BlogService _blogService;

	public BlogController()
	{
		_blogService = new BlogService();
	}

	[HttpGet]
	public async Task<IActionResult> GetBlogs()
	{
		BlogListResponseModel responseModel = new();
		try
		{
			responseModel = await _blogService.GetBlogs();
			return Ok(responseModel);
		}
		catch (Exception ex)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = ex.Message;
			return StatusCode(500, responseModel);
		}
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetBlog(string id)
	{
		BlogResponseModel responseModel = new();
		try
		{
			responseModel = await _blogService.GetBlog(id);
			if (!responseModel.IsSuccess) return BadRequest(responseModel);

			return Ok(responseModel);
		}
		catch (Exception ex)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = ex.Message;
			return StatusCode(500, responseModel);
		}
	}
}

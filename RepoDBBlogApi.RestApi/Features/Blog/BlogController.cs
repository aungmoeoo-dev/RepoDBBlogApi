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
	public IActionResult GetBlogs()
	{
		BlogListResponseModel responseModel = new();
		try
		{
			responseModel = _blogService.GetBlogs();
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
	public IActionResult GetBlog(string id)
	{
		BlogResponseModel responseModel = new();
		try
		{
			responseModel = _blogService.GetBlog(id);
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

	[HttpPost]
	public IActionResult CreateBlog([FromBody]TBL_Blog requestModel)
	{
		BlogResponseModel responseModel = new();
		try
		{
			responseModel = _blogService.CreateBlog(requestModel);
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

	[HttpPatch("{id}")]
	public IActionResult UpdateBlog(string id, [FromBody] TBL_Blog requestModel)
	{
		BlogResponseModel responseModel = new();
		try
		{
			requestModel.BlogId = id;
			responseModel = _blogService.UpdateBlog(requestModel);
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

	[HttpDelete("{id}")]
	public IActionResult DeleteBlog(string id)
	{
		BlogResponseModel responseModel = new();
		try
		{
			responseModel = _blogService.DeleteBlog(id);
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

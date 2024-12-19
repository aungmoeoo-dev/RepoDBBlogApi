using Microsoft.AspNetCore.Mvc;
using RepoDBBlogApi.Shared;

namespace RepoDBBlogApi.MinimalApi.Endpoints.Blog;

public static class BlogEndpoints
{
	public static IEndpointRouteBuilder UseBlogEndpoint(this IEndpointRouteBuilder app)
	{
		app.MapGet("/api/Blog", () =>
		{
			BlogListResponseModel responseModel = new();
			try
			{
				IBlogService blogService = new BlogService();
				responseModel = blogService.GetBlogs();
				return Results.Ok(responseModel);
			}
			catch (Exception ex)
			{
				responseModel.IsSuccess = false;
				responseModel.Message = ex.Message;
				return Results.Json(responseModel, statusCode: 500);
			}
		})
		.WithName("GetBlogs")
		.WithOpenApi();

		app.MapGet("/api/Blog/{id}", (string id) =>
		{
			BlogResponseModel responseModel = new();
			try
			{
				IBlogService blogService = new BlogService();
				responseModel = blogService.GetBlog(id);
				if (!responseModel.IsSuccess) return Results.BadRequest(responseModel);

				return Results.Ok(responseModel);
			}
			catch (Exception ex)
			{
				responseModel.IsSuccess = false;
				responseModel.Message = ex.Message;
				return Results.Json(responseModel, statusCode: 500);
			}
		})
		.WithName("GetBlog")
		.WithOpenApi();

		app.MapPost("/api/Blog", (TBL_Blog requestModel) =>
		{
			BlogResponseModel responseModel = new();
			try
			{
				IBlogService blogService = new BlogService();
				responseModel = blogService.CreateBlog(requestModel);
				if (!responseModel.IsSuccess) return Results.BadRequest(responseModel);

				return Results.Ok(responseModel);
			}
			catch (Exception ex)
			{
				responseModel.IsSuccess = false;
				responseModel.Message = ex.Message;
				return Results.Json(responseModel, statusCode: 500);
			}
		})
		.WithName("CreateBlog")
		.WithOpenApi();

		app.MapPatch("/api/Blog/{id}", (string id, TBL_Blog requestModel) =>
		{
			BlogResponseModel responseModel = new();
			try
			{
				IBlogService blogService = new BlogService();

				requestModel.BlogId = id;
				responseModel = blogService.UpdateBlog(requestModel);
				if (!responseModel.IsSuccess) return Results.BadRequest(responseModel);

				return Results.Ok(responseModel);
			}
			catch (Exception ex)
			{
				responseModel.IsSuccess = false;
				responseModel.Message = ex.Message;
				return Results.Json(responseModel, statusCode: 500);
			}
		})
		.WithName("UpdateBlog")
		.WithOpenApi();

		app.MapDelete("/api/Blog/{id}", (string id) =>
		{
			BlogResponseModel responseModel = new();
			try
			{
				IBlogService blogService = new BlogService();
				responseModel = blogService.DeleteBlog(id);
				if (!responseModel.IsSuccess) return Results.BadRequest(responseModel);

				return Results.Ok(responseModel);
			}
			catch (Exception ex)
			{
				responseModel.IsSuccess = false;
				responseModel.Message = ex.Message;
				return Results.Json(responseModel, statusCode: 500);
			}
		})
		.WithName("DeleteBlog")
		.WithOpenApi();

		return app;
	}
}

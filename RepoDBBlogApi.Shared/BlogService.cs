using Microsoft.Data.SqlClient;
using RepoDb;
using System.Data;

namespace RepoDBBlogApi.Shared;

public class BlogService
{
	private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new()
	{
		DataSource = ".",
		InitialCatalog = "TestDB",
		UserID = "sa",
		Password = "Aa145156167!",
		TrustServerCertificate = true
	};

	private IDbConnection _dbConnection;

	public BlogService()
	{
		_dbConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

		RepoDb.SqlServerBootstrap.Initialize();
	}
	public async Task<BlogListResponseModel> GetBlogs()
	{
		BlogListResponseModel responseModel = new();

		var list =  await _dbConnection.QueryAllAsync<BlogModel>("TBL_Blog");

		responseModel.IsSuccess = true;
		responseModel.Message = "Success";
		responseModel.Data = list.ToList();
		return responseModel;
	}

	public async Task<BlogResponseModel> GetBlog(string id)
	{
		BlogResponseModel responseModel = new();

		var list = await _dbConnection.QueryAsync<BlogModel>("TBL_Blog", x => x.BlogId == id);
		var blog = list.FirstOrDefault();

		if (blog is null)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "No data found.";
			return responseModel;
		}

		responseModel.IsSuccess = true;
		responseModel.Message = "Success";
		responseModel.Data = blog;
		return responseModel;
	}

	public async Task<BlogResponseModel> CreateBlog(BlogModel requestModel)
	{
		BlogResponseModel responseModel = new();

		int result = await _dbConnection.InsertAsync<BlogModel, int>(requestModel);

		responseModel.IsSuccess = result > 0;
		responseModel.Message = result > 0 ? "Saving successful." : "Saving failed.";
		responseModel.Data = result > 0 ? requestModel : null;
		return responseModel;
	}

	public async Task<BlogResponseModel> UpdateBlog(BlogModel requestModel)
	{
		BlogResponseModel responseModel = new();

		int result = await _dbConnection.UpdateAsync<BlogModel>(requestModel);

		responseModel.IsSuccess = result > 0;
		responseModel.Message = result > 0 ? "Updating successful." : "Updating failed.";
		responseModel.Data = result > 0 ? requestModel : null;
		return responseModel;
	}

	public async Task<BlogResponseModel> DeleteBlog(string id)
	{
		BlogResponseModel responseModel = new();

		int result = await _dbConnection.DeleteAsync(id);

		responseModel.IsSuccess = result > 0;
		responseModel.Message = result > 0 ? "Deleting successful." : "Deleting failed.";
		return responseModel;
	}
}

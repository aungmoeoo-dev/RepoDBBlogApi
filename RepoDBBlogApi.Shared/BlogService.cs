using Microsoft.Data.SqlClient;
using RepoDb;
using System.Data;

namespace RepoDBBlogApi.Shared;

public class BlogService
{
	private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new()
	{
		DataSource = ".",
		InitialCatalog = "BlogDB",
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
	public BlogListResponseModel GetBlogs()
	{
		BlogListResponseModel responseModel = new();

		var list =  _dbConnection.QueryAll<TBL_Blog>().ToList();

		responseModel.IsSuccess = true;
		responseModel.Message = "Success";
		responseModel.Data = list;
		return responseModel;
	}

	public BlogResponseModel GetBlog(string id)
	{
		BlogResponseModel responseModel = new();

		var blog = _dbConnection.Query<TBL_Blog>(x => x.BlogId == id).FirstOrDefault();

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

	public BlogResponseModel CreateBlog(TBL_Blog requestModel)
	{
		BlogResponseModel responseModel = new();

		string blogId = Guid.NewGuid().ToString(); ;
		requestModel.BlogId = blogId;
		string result = _dbConnection.Insert<TBL_Blog, string>(requestModel);

		responseModel.IsSuccess = result != "";
		responseModel.Message = result != "" ? "Saving successful." : "Saving failed.";
		responseModel.Data = result != "" ? requestModel : null;
		return responseModel;
	}

	public BlogResponseModel UpdateBlog(TBL_Blog requestModel)
	{
		BlogResponseModel responseModel = new();

		int result = _dbConnection.Update<TBL_Blog>(requestModel);

		responseModel.IsSuccess = result > 0;
		responseModel.Message = result > 0 ? "Updating successful." : "Updating failed.";
		responseModel.Data = result > 0 ? requestModel : null;
		return responseModel;
	}

	public BlogResponseModel DeleteBlog(string id)
	{
		BlogResponseModel responseModel = new();

		int result = _dbConnection.Delete<TBL_Blog>(id);

		responseModel.IsSuccess = result > 0;
		responseModel.Message = result > 0 ? "Deleting successful." : "Deleting failed.";
		return responseModel;
	}
}

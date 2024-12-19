namespace RepoDBBlogApi.Shared
{
	public interface IBlogService
	{
		BlogResponseModel CreateBlog(TBL_Blog requestModel);
		BlogResponseModel DeleteBlog(string id);
		BlogResponseModel GetBlog(string id);
		BlogListResponseModel GetBlogs();
		BlogResponseModel UpdateBlog(TBL_Blog requestModel);
	}
}
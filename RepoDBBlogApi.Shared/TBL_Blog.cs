namespace RepoDBBlogApi.Shared;

public class TBL_Blog
{
	public string? BlogId { get; set; }
	public string? BlogTitle { get; set; }
	public string? BlogAuthor { get; set; }
	public string? BlogContent { get; set; }
}

public class BlogResponseModel
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; }
	public TBL_Blog Data { get; set; }
}

public class BlogListResponseModel
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; }
	public List<TBL_Blog> Data { get; set; }
}
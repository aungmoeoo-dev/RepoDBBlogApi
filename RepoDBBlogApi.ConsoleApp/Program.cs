// See https://aka.ms/new-console-template for more information

using RepoDBBlogApi.ConsoleApp.Features.Blog;
using System.Reflection.Metadata;

BlogService blogService = new BlogService();

var getBlogsResponse = await blogService.GetBlogs();
foreach(var blog in getBlogsResponse.Data)
{
	Console.WriteLine(blog.BlogTitle);
}

Console.ReadLine();

var getBlogResponse = await blogService.GetBlog("18d350f8-ca8f-48ac-a13f-49f0c47b2717");
Console.WriteLine(getBlogResponse.Data.BlogTitle);

Console.ReadLine();


BlogModel newBlog = new()
{
	BlogTitle = "Test title",
	BlogAuthor = "Test author",
	BlogContent = "Test content"
};
var createBlogResponse = await blogService.CreateBlog(newBlog);
Console.WriteLine(createBlogResponse.Data.BlogTitle);

Console.ReadLine();

BlogModel updateBlog = new()
{
	BlogId = "96de3635-a999-4260-936a-b4f7cb383e2a",
	BlogTitle = "Test title",
	BlogAuthor = "Test author",
	BlogContent = "Test content"
};
var updateBlogResponse = await blogService.CreateBlog(updateBlog);
Console.WriteLine(updateBlogResponse.Data.BlogTitle);

Console.ReadLine();

var deleteBlogResponse = await blogService.DeleteBlog("96de3635-a999-4260-936a-b4f7cb383e2a");
Console.WriteLine(deleteBlogResponse.Message);

Console.ReadLine();
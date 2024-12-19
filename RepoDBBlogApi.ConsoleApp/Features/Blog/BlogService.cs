using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoDBBlogApi.ConsoleApp.Features.Blog;

public class BlogService
{
	private readonly string endpoint = "https://localhost:7082/api/blog";
	private readonly RestClient _restClient;

	public BlogService()
	{
		_restClient = new RestClient();
	}

	public async Task<BlogListResponseModel> GetBlogs()
	{
		RestRequest request = new RestRequest(endpoint);
		RestResponse response = await _restClient.ExecuteAsync(request);

		string content = response.Content;
		//Console.WriteLine(content);
		return JsonConvert.DeserializeObject<BlogListResponseModel>(content);
	}

	public async Task<BlogResponseModel> GetBlog(string id)
	{
		RestRequest request = new($"{endpoint}/{id}");
		RestResponse response = await _restClient.ExecuteAsync(request);
		string content = response.Content;
		//Console.WriteLine(content);
		return JsonConvert.DeserializeObject<BlogResponseModel>(content);
	}

	public async Task<BlogResponseModel> CreateBlog(BlogModel requestModel)
	{
		RestRequest request = new(endpoint, Method.Post);
		request.AddJsonBody(requestModel);
		RestResponse response = await _restClient.ExecuteAsync(request);
		string content = response.Content;
		//Console.WriteLine(content);
		return JsonConvert.DeserializeObject<BlogResponseModel>(content);
	}

	public async Task<BlogResponseModel> UpdateBlog(BlogModel requestModel)
	{
		RestRequest request = new($"{endpoint}/{requestModel.BlogId}", Method.Patch);
		request.AddJsonBody(requestModel);

		RestResponse response = await _restClient.ExecuteAsync(request);
		string content = response.Content;
		//Console.WriteLine(content);
		return JsonConvert.DeserializeObject<BlogResponseModel>(content);
	}

	public async Task<BlogResponseModel> DeleteBlog(string id)
	{
		RestRequest request = new($"{endpoint}/{id}", Method.Delete);
		RestResponse response = await _restClient.ExecuteAsync(request);

		string content = response.Content;
		//Console.WriteLine(content);
		return JsonConvert.DeserializeObject<BlogResponseModel>(content);
	}
}

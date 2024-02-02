using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using TheMovieDB.Model;

namespace TheMovieDB;

public class Client
{
    private const string CURL = @"https://api.themoviedb.org/3/";
    private string _token;

    public Client(string token)
    {
        _token = token;
    }

    public async Task<Movie?> MovieAsync(int id)
    {
        var a = await RequestBase<object, Movie>(null, "movie/" + id.ToString(), HttpMethod.Get);
        return a;
    }

    public Movie? Movie(int id)
    {
        return MovieAsync(id).Result;
    }

    public async Task<ListBase<Movie>> PopularMovieAsync(int page)
    {
        var a = await RequestBase<object, ListBase<Movie>>(null, "movie/popular", HttpMethod.Get,
            new Dictionary<string, string>() { { "page", page.ToString() } });
        return a;
    }

    public ListBase<Movie> PopularMovie(int page = 1)
    {
        return PopularMovieAsync(page).Result;
    }

    private async Task<R> RequestBase<T, R>(T model, string endPoint, HttpMethod method,
        Dictionary<string, string> parameters = null)
    {
        if (parameters != null)
            endPoint = parameters.Aggregate(endPoint, (current, item) => current + ("?" + item.Key + "=" + item.Value));
        var httpRequest = new HttpRequestMessage(method, CURL + endPoint);
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = new HttpResponseMessage();

        if (method == HttpMethod.Get)
        {
            response = new HttpClient().SendAsync(httpRequest).Result;
        }

        try
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ToString());
            }

            return JsonSerializer.Deserialize<R>(response.Content.ReadAsStringAsync().Result.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
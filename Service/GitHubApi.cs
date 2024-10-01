using System.Net.Http.Headers;
using System.Text;

namespace OnlineVerilog.Service
{
    public static class GitHubApi
    {
        public static string GitHubApiUrl = "https://api.github.com/repos";
        public static string GitHubToken = "";
        public static string RepoOwner = "ArsicAntonijo";
        public static string RepoName = "VeronDumpRepo";

        public static async void PushToGit(string FilePath, string FileContent, string CommitMessage)
        {
            var url = $"{GitHubApiUrl}/{RepoOwner}/{RepoName}/contents/{FilePath}";
            try
            {
                var content = new
                {
                    message = CommitMessage,
                    content = Convert.ToBase64String(Encoding.UTF8.GetBytes(FileContent))
                };

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MyApp/1.0)");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", GitHubToken);

                    var requestBody = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync(url, requestBody);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("File pushed successfully.");
                    }
                    else
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error: {responseBody}");
                    }
                }
            }
            catch(Exception ex) { Console.WriteLine($"Error uploading to git: {ex.Message}"); }            
        }
    }
}

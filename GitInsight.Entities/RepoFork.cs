namespace GitInsight.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

public class RepoFork
{  
    private GitInsightContext _context;

    public record RepoForkObj(string repoName, string ownerName, string repo_url);

    public static async Task<IEnumerable<RepoForkObj>> getRepoForks(string gitPath, string kee){
        
        var rawData = await Task.WhenAll(ConnectGithubAPI(gitPath, kee)).ConfigureAwait(false);
        var data = rawData.SelectMany(x=>x.Select(y=>new RepoForkObj(y.name!,y.owner!.login!, y.html_url!)));
        
        return data;
    }

    public static async Task<IEnumerable<Fork>> ConnectGithubAPI(string gitPath, string kee){   
      
        //Sets up the connection to Github API
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://api.github.com");
        
        //to access the fork info you must generate a github token under your github account settings: Developer Settings:
        // Personal access tokens : tokens (classic): Generate new token: 
        // Make sure to give it access to repo and admin:public_key

        //Token is Very SECRET use the following commands to not leak access to you github xDDD
        //dotnet user-secrets set "Git:HubToken" "INSERT TOKEN HERE"
       
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();
       
        var token = builder.Configuration["Git:HubToken"];
        
        client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        //For tesing
        if(kee!= null){
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", kee);
        }else{
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);
        }
        
        //takes the "/repos/:Organization name:/:Repo Name:/forks" as an argument
        try{
        var gitForks = await client.GetAsync("/repos/" + gitPath + "/forks").ConfigureAwait(false);
        gitForks.EnsureSuccessStatusCode();

        string responseBody = await gitForks.Content.ReadAsStringAsync().ConfigureAwait(false);
        var list = new List<Fork>();

        var forkList = JsonConvert.DeserializeObject<List<Fork>>(responseBody);

        return forkList!;
        
        }catch(Exception){
            return new List<Fork>();
        }
        
        
    }


   
   
}


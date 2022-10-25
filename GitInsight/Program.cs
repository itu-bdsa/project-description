// See https://aka.ms/new-console-template for more information

using LibGit2Sharp;

    var path = Repository.Init(".");
    Repository repo = new Repository(path);
 //creates repository through libGit2Sharp
        //creates a repository object from the path above
        repo.Commit("Inital commit", new Signature("Monica Hardt", "monha@itu.dk", new System.DateTimeOffset()),new Signature("Monica Hardt", "monha@itu.dk", new System.DateTimeOffset()), new CommitOptions() {AllowEmptyCommit = true});
        Console.WriteLine(repo.Commits.First().Message);

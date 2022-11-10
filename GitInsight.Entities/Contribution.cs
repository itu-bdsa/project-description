using System;
using System.Globalization;
namespace GitInsight.Entities;

public class Contribution
{
    public int Id { get; set; }

    /*[Required]
    public string repoPath { get; set; }*/

    public string? author { get; set; }

    public DateTime date { get; set; }

    public int commitsCount { get; set; }
    
    //[Required]
    //public RepoCheck repCheck { get; set; }
}

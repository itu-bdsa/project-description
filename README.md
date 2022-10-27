# Project Description


The goal of the project is to build an application called `GitInsight` that allows users to get insights over development in Git/GitHub repositories.

For example, on GitHub you get some of the following visualizations for your repositories when selecting the _Insights_ tab on top of a repository view: 

<table>
    <tr>
        <td><a href="https://github.com/apache/airflow/graphs/contributors"><img src="images/contributors_airflow.png" width="100%"></a></td>
        <td><a href="https://github.com/apache/airflow/graphs/commit-activity"><img src="images/commits_airflow.png" width="100%"></a></td>
    </tr>
    <tr>
        <td><a href="https://github.com/apache/airflow/graphs/code-frequency"><img src="images/code_freq_airflow.png" width="100%"></a></td>
        <td><a href="https://github.com/apache/airflow/network/members"><img src="images/forks_airflow.png" width="100%"></a></td>
    </tr>
</table>

The project will be conducted in an agile and iterative fashion.
That is, from week to week you will extend your application and refactor it so that its design reflects the most current requirements.

## Week Zero (Week 42)

Find five other group members and register your group by sending a pull-request to the file [./PROJECT_GROUPS.md](./PROJECT_GROUPS.md) in which you add your group members to the respective column in the table.
Also, assign your group a name.
Remember, such a name has to be appropriate in a professional setting.

Please send in the message to the pull-request the IDs of your group members too so that TAs can more easily resolve merge conflicts.


## Week One (Week 43)

Build a small C#/.Net Core application that can be run from the command-line.
As a parameter, it should receive the path to a Git repository that resides in a local directory, i.e., a directory on your computer.

Given that path to a repository, your application should collect all commits with respective author names and author dates.
The data can be collected with the library [`libgit2sharp`](https://github.com/libgit2/libgit2sharp), which can be installed from [NuGet](https://www.nuget.org/packages/LibGit2Sharp).

Your program should be able to run in two modes, which may be indicated via command-line switches.

When running `GitInsight` in _commit frequency_ mode, it should produce textual output on stdout that lists the number of commits per day.
For example, the output might look like in the following:

```
      1 2017-12-08
      6 2017-12-26
     12 2018-01-01
     13 2018-01-02
     10 2018-01-14
      7 2018-01-17
      5 2018-01-18 
```

When running `GitInsight` in _commit author_ mode, it should produce textual output on stdout that lists the number of commits per day per author.
For example, the output might look like in the following:

```
Marie Beaumin
      1 2017-12-08
      6 2017-12-26
     12 2018-01-01
     13 2018-01-02
     10 2018-01-14
      7 2018-01-17
      5 2018-01-18 

Maxime Kauta
      5 2017-12-06
      3 2017-12-07
      1 2018-01-01
     10 2018-01-02
     21 2018-01-03
      1 2018-01-04
      5 2018-01-05 
```

Of course, as described by R. Martin, you try to keep your application as simple as possible to solve the task.
Remember that as good agile software engineers, you develop your project in a test-driven manner, i.e., start writing unit tests before implementing your solution.

Create one or more GitHub Action workflows that run your unit tests and that build your `GitInsight` application on every push to the main branch or on any pull-request that is merged into the main branch.

Next to the code, create a directory called `docs` in the root of your repository.
In it  use markdown documents to describe your project.
For the first week, provide a list of functional requirements that you extract from the description above.
When necessary, create describe interactions with your `GitInsight` application via use cases.

You have to develop your `GitInsight` application in a public GitHub repository (on github.com).
Before Friday 28/10 10:00, you have to send a pull-request to the file [./PROJECT_GROUPS.md](./PROJECT_GROUPS.md) in which you add the link to your repository.

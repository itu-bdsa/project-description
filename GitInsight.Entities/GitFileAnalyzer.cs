namespace GitInsight.Entities;
using LibGit2Sharp;

public class GitFileAnalyzer{

    public record FileAndNrChanges(string fileName, int changes);

    public List<FileAndNrChanges> getFilesAndNrChanges(Repository repo){
        //sort commits by date
        var filter = new CommitFilter {
        SortBy = CommitSortStrategies.Time | CommitSortStrategies.Reverse,
        };
        var commits = repo.Commits.QueryBy(filter);

        //Find changes between trees, save changes to list
        var tempList = new List<TreeEntryChanges>();
        for (int i = 0; i < commits.Count(); i++){
            if(i+1 < commits.Count()){
                var temp = repo.Diff.Compare<TreeChanges>(commits.ElementAt(i).Tree, commits.ElementAt(i + 1).Tree);

                foreach(var change in temp){
                    tempList.Add(change);
                }
            }
        }

        var returnList = new List<FileAndNrChanges>();
        //select path, Count(path) ... group by path
        var r = tempList.GroupBy(c => c.Path).Select(s => new { path = s.Key, count = s.Count()});
        foreach(var p in r){
            returnList.Add(new FileAndNrChanges(p.path, p.count));
        }

        return returnList;

    }

    public FileAndNrChanges getMostFreqChangedFile(List<FileAndNrChanges> fileList){
        //find path/file that has been changed most frequently
        //there can be multiple, but we only take the first one found
        var mostFreqChangedFile = fileList.Where(c => c.changes == 
                                (fileList.Select(k => k.changes).Max()))
                                .First();

        return mostFreqChangedFile;
    }
}
namespace GitInsight.Core;
public interface ICommitSignatureRepository
{
    Response Update (CommitSignatureUpdateDTO sign);
    
}

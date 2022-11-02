namespace GitInsight.Entities;

public class CommitSignatureRepository: ICommitSignatureRepository, IDisposable{

private GitInsightContext _context;
    public CommitSignatureRepository(GitInsightContext context){
        _context = context;
    }

    //creating a SignatureUpdateDTO from a signature
    private static CommitSignatureUpdateDTO SignatureUpdateDTOFromSignature(CommmitSignature sign){
        return new CommitSignatureUpdateDTO (
        name: sign.Name,
        email: sign.Email,
        date: sign.Date
    );
    }
    
    public Response Update(CommitSignatureUpdateDTO sign){

        //find the date of the mathcing signature
        //var toUpdate = _context.Signatures.Find(sign.date);

        _context.SaveChanges();
        return Response.Updated;
    }
    public void Dispose(){
        _context.Dispose();
    }
}
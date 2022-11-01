namespace GitInsight.Entities;

public class SignatureRepository: ISignatureRepository{

private GitInsightContext _context;
    private SignatureRepository(GitInsightContext context){
        _context = context;
    }

    //creating a SignatureUpdateDTO from a signature
    private static SignatureUpdateDTO SignatureUpdateDTOFromSignature(Signature sign){
        return new SignatureUpdateDTO (
        name: sign.Name,
        email: sign.Email,
        date: sign.Date
    );
    }
    
    public Response Update(SignatureUpdateDTO sign){

        //find the date of the mathcing signature
        //var toUpdate = _context.Signatures.Find(sign.date);

        _context.SaveChanges();
        return Response.Updated;
    }
}
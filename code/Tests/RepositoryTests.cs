namespace Tests;

public class RepositoryTests : IDisposable
{
    private readonly CommitTreeContext _context;
    private readonly CommitDataRepository _repository;

    public RepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<CommitTreeContext>();
        builder.UseSqlite(connection);
        var context = new CommitTreeContext(builder.Options);
        context.Database.EnsureCreated();
        context.SaveChanges();

        _context = context;
        _repository = new CommitDataRepository(_context);
    }

#pragma warning disable

    [Fact]
    public void Create_Should_Create_CommitData_and_add_to_context()
    {
        // Arrange
        var testAuthorString = "bemi";

        // Act
        _repository.Create(new CommitDataCreateDTO("21", "bemillant/bdsa-project", "bemi", DateTime.Now));

        // Assert
        Assert.Equal(_context.CommitData.FirstOrDefault().AuthorName, testAuthorString);
    }


    [Fact]
    public void Create_should_not_be_able_to_create_the_same_CommitData_twice()
    {
        // Arrange
        int countAfterCreate = 1;

        // Act
        _repository.Create(new CommitDataCreateDTO("21", "bemillant/bdsa-project", "bemi", DateTime.Now));
        _repository.Create(new CommitDataCreateDTO("21", "bemillant/bdsa-project", "bemi", DateTime.Now));

        // Assert
        Assert.Equal(countAfterCreate, _context.CommitData.Count());
    }





    [Fact]
    public void Delete_should_delete_all_Data_from_given_repo()
    {
        //Arrange
        _repository.Create(new CommitDataCreateDTO("21", "bemillant/bdsa-project", "bemi", DateTime.Now));
        _repository.Create(new CommitDataCreateDTO("1", "bemillant/bdsa-project", "bemi", DateTime.Now));
        int count = 0;
        //Act
        _repository.DeleteAll("bemillant/bdsa-project");

        //Assert

        Assert.Equal(count, _context.CommitData.Count());
    }

    // [Fact]
    // public void Delete_all_existing_commits()
    // {
    //     _freqRepo.DeleteAll();

    //     Assert.Equal(0, _context.allFrequencyData.Count());
    // }

    [Fact]
    public void ReadAllCommitsFromRepo_should_return_list_of_Tuples()
    {
        //Arrange
        _repository.Create(new CommitDataCreateDTO("1", "bemillant/bdsa-project", "bemi", DateTime.Parse("12/01/2016")));
        _repository.Create(new CommitDataCreateDTO("21", "bemillant/bdsa-project", "bemi", DateTime.Parse("12/01/2021")));
        _repository.Create(new CommitDataCreateDTO("211", "bemillant/bdsa-project", "raoo", DateTime.Parse("12/01/2021")));
        var listShouldBe = new List<Tuple<DateTime, int>>();
        listShouldBe.Add(new Tuple<DateTime, int>(DateTime.Parse("12/01/2016"), 1));
        listShouldBe.Add(new Tuple<DateTime, int>(DateTime.Parse("12/01/2021"), 2));


        //Act
        var listToCheck = _repository.ReadAllCommitsFromRepo("bemillant/bdsa-project");

        //Assert
        Assert.Equal(listShouldBe, listToCheck);
    }
    [Fact]
    public void ReadAllCommitsFromRepo_increase_count()
    {
        //Arrange
        _repository.Create(new CommitDataCreateDTO("1", "bemillant/bdsa-project", "bemi", DateTime.Parse("12/01/2016")));
        _repository.Create(new CommitDataCreateDTO("21", "bemillant/bdsa-project", "bemi", DateTime.Parse("12/01/2021")));
        _repository.Create(new CommitDataCreateDTO("211", "bemillant/bdsa-project", "bemi", DateTime.Parse("12/01/2021")));
        _repository.Create(new CommitDataCreateDTO("2111", "bemillant/bdsa-project", "bemi", DateTime.Parse("12/01/2021")));
        var listShouldBe = new List<Tuple<DateTime, int>>();
        listShouldBe.Add(new Tuple<DateTime, int>(DateTime.Parse("12/01/2016"), 1));
        listShouldBe.Add(new Tuple<DateTime, int>(DateTime.Parse("12/01/2021"), 3));


        //Act
        var listToCheck = _repository.ReadAllCommitsFromRepo("bemillant/bdsa-project");

        //Assert
        Assert.Equal(listShouldBe, listToCheck);
        Assert.Equal(3, listToCheck.Where(d => d.Item1 == DateTime.Parse("12/01/2021")).Select(d => d.Item2).FirstOrDefault());
        Assert.Equal(1, listToCheck.Where(d => d.Item1 == DateTime.Parse("12/01/2016")).Select(d => d.Item2).FirstOrDefault());
    }



    [Fact]
    public void GetAllAuthorsCommitsFromRepository_should_give_map_of_all_commits()
    {
        //Arrange
        _repository.Create(new CommitDataCreateDTO("1", "bemillant/bdsa-project", "bemi", DateTime.Parse("12/01/2016")));
        _repository.Create(new CommitDataCreateDTO("21", "bemillant/bdsa-project", "bemi", DateTime.Parse("12/01/2021")));
        _repository.Create(new CommitDataCreateDTO("212", "bemillant/bdsa-project", "bemi", DateTime.Parse("12/01/2021")));
        _repository.Create(new CommitDataCreateDTO("211", "bemillant/bdsa-project", "raoo", DateTime.Parse("12/01/2021")));

        Dictionary<string, List<Tuple<DateTime, int>>> dict1 = new Dictionary<string, List<Tuple<DateTime, int>>> {
             { "bemi",
                new List<Tuple<DateTime, int>>
                    {
                        new Tuple<DateTime, int>(DateTime.Parse("12/01/2016"), 1),
                        new Tuple<DateTime, int>(DateTime.Parse("12/01/2021"), 2)
                    }
                },
             { "raoo",
                new List<Tuple<DateTime, int>>
                    {
                        new Tuple<DateTime, int>(DateTime.Parse("12/01/2021"), 1)
                    }
             }};

        //Act
        Dictionary<string, List<Tuple<DateTime, int>>> mapToCheck = (Dictionary<string, List<Tuple<DateTime, int>>>)_repository.GetAllAuthorsCommitsFromRepository("bemillant/bdsa-project");

        //Assert
        Assert.Equal(dict1, mapToCheck);
    }




    public void Dispose()
    {
        _context.Dispose();
    }
}



//     [Fact]
//     public void Create_Should_Create_Author_and_Add_to_Context()
//     {
//         // Arrange
//         var testAuthorString = "Stephen King";

//         // Act
//         _authorRepository.Create(new AuthorDataCreateDTO("Stephen King"));

//         // Assert
//         Assert.Equal(_context.allAuthorData.FirstOrDefault().Name, testAuthorString);
//     }

//     [Fact]
//     public void ReadAll_Should_Return_List_Size_3()
//     {
//         // Arrange
//         var testAuthorString0 = "JK Rowling";
//         var testAuthorString1 = "JRR Tolkien";
//         var testAuthorString2 = "GRR Martin";
//         var listOfTestAuthorStrings = new List<string>() { testAuthorString0, testAuthorString1, testAuthorString2 };

//         // Act
//         _authorRepository.Create(new AuthorDataCreateDTO(testAuthorString0));
//         _authorRepository.Create(new AuthorDataCreateDTO(testAuthorString1));
//         _authorRepository.Create(new AuthorDataCreateDTO(testAuthorString2));

//         var jens = _authorRepository.ReadAll().ToList();
//         var hans = new List<string>();
//         foreach (AuthorDataDTO a in jens)
//         {
//             hans.Add(a.Name);
//         }

//         // Assert
//         Assert.Equal(hans, listOfTestAuthorStrings);
//     }



//     public void Dispose()
//     {
//         _context.Dispose();
//     }
// }

// namespace Tests;

// public class FrequencyDataRepositoryTests : IDisposable
// {

//     private readonly CommitTreeContext _context;
//     private readonly FrequencyDataRepository _freqRepo;


//     public FrequencyDataRepositoryTests()
//     {
//         var connection = new SqliteConnection("Filename=:memory:");
//         connection.Open();
//         var builder = new DbContextOptionsBuilder<CommitTreeContext>();
//         builder.UseSqlite(connection);
//         var context = new CommitTreeContext(builder.Options);
//         context.Database.EnsureCreated();

//         //24/08/2008, 3
//         context.allFrequencyData.AddRange(
//             new FrequencyData(System.DateTime.ParseExact("24/08/2008", "dd/MM/yyyy", null)),
//             new FrequencyData(System.DateTime.ParseExact("20/10/2009", "dd/MM/yyyy", null)),
//             new FrequencyData(System.DateTime.ParseExact("24/08/2010", "dd/MM/yyyy", null)),
//             new FrequencyData(System.DateTime.ParseExact("04/03/2011", "dd/MM/yyyy", null))
//         );
//         context.SaveChanges();

//         _context = context;
//         _freqRepo = new FrequencyDataRepository(_context);

//     }


//     public void Dispose()
//     {
//         _context.Dispose();
//     }

//     [Fact]
//     public void Create_Frequency_Should_Create_Frequency_And_Add_To_DB()
//     {
//         _freqRepo.Create(new FrequencyDataCreateDTO(DateTime.Parse("01/01/2023"), 1));

//         Assert.Equal(5, _context.allFrequencyData.Count());
//     }

//     [Fact(Skip = "System.FormatException : String '20/10/2009' was not recognized as a valid DateTime.")]
//     public void Create_Freq_Should_not_change_count_of_freq_since_freq_exists()
//     {
//         _freqRepo.Create(new FrequencyDataCreateDTO(DateTime.Parse("20/10/2009"), 1));
//         Assert.Equal(4, _context.allFrequencyData.Count());
//     }

//     [Fact(Skip = "System.FormatException : String '20/10/2009' was not recognized as a valid DateTime.")]
//     public void Create_Freq_Should_change_freq_count_freq_exists()
//     {
//         var newFreq = new FrequencyDataCreateDTO(DateTime.Parse("20/10/2009"), 1);
//         _freqRepo.Create(newFreq);

//         Assert.Equal(2, _context.allFrequencyData.Find(newFreq.Date)!.Count);
//     }

//     [Fact(Skip = "System.FormatException : String '20/10/2009' was not recognized as a valid DateTime.")]
//     public void Delete_existing_commit()
//     {
//         _freqRepo.DeleteSpecificFreq(DateTime.Parse("20/10/2009"));

//         Assert.Equal(3, _context.allFrequencyData.Count());
//     }

//     [Fact]
//     public void Delete_all_existing_commits()
//     {
//         _freqRepo.DeleteAll();

//         Assert.Equal(0, _context.allFrequencyData.Count());
//     }

//     [Fact(Skip = "Not currently working")]
//     public void ReadAll_return_all_frequencies()
//     {
//         var listToCheck = _freqRepo.ReadAll();

//         Assert.Equivalent(listToCheck, _context.allFrequencyData);
//     }
// }
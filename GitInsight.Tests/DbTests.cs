using System.Collections;
using LibGit2Sharp;
using Microsoft.Data.Sqlite;
using GitInsight.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

// Placeholder
public class DbTests{
    private readonly SqliteConnection _connection;
    private readonly GitInsightContext _context;
    private readonly ContributionRepository _repository;

        public DbTests(){
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<GitInsightContext>().UseSqlite(_connection);
        _context = new GitInsightContext(builder.Options);
        _context.Database.EnsureCreated();

        _context.SaveChanges();

        _repository = new ContributionRepository(_context);
    }

     public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
    }

}
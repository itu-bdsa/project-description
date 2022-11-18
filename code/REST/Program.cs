namespace REST;
using Models;

class Program
{
    static void Main(string[] args)
    {
        var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy  => { policy.WithOrigins("https://localhost:7251").AllowAnyHeader().AllowAnyMethod();});
            });

        // Add services to the container.

        builder.Services.AddControllers();
        // builder.Services.AddDbContext<CommitTreeContext>();
        builder.Services.AddDbContext<CommitTreeContext>(opt =>
            opt.UseInMemoryDatabase("inMemDB"));
            
        // builder.Services.AddCors(options => {
        //     options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        // });
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();


        app.UseCors();

        app.UseAuthorization();

        app.MapControllers();

        app.Run(); 
    }
}
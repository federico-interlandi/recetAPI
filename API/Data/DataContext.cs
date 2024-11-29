using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options){
    public required DbSet<User> Users {get; set;}

    public required DbSet<Recipe> Recipes {get; set;}

    public required DbSet<Ingredient> Ingredients {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .HasOne<User>() 
                .WithMany()
                .HasForeignKey(r => r.UserEmail) 
                .HasPrincipalKey(u => u.Email); 
        }
}
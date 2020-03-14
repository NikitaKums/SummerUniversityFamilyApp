using System;
using System.Linq;
using Contracts.DAL.Base;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App
{
    public class AppDbContext : DbContext, IDataContext
    {

        public DbSet<Person> Persons { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<PersonInRelationship> PersonInRelationships { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder
                .Entity<Relationship>()
                .Property(p => p.Relation)
                .HasConversion(
                    v => v.ToString(),
                    v => (Relation)Enum.Parse(typeof(Relation), v));

            builder.Entity<PersonInRelationship>()
                .HasOne(pt => pt.Person)
                .WithMany(p => p.RelatedFrom) // <--
                .HasForeignKey(pt => pt.PersonId)
                .OnDelete(DeleteBehavior.Restrict); // see the note at the end

            builder.Entity<PersonInRelationship>()
                .HasOne(pt => pt.Person1)
                .WithMany(t => t.RelatedTo)
                .HasForeignKey(pt => pt.Person1Id);
            
            // disable cascade delete
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}
using System;
using System.Linq;
using DAL.App;
using Domain;
using Microsoft.AspNetCore.Builder;

namespace WebApp.Extensions
{
    public static class RelationDataSeeder
    {
        public static void SeedRelationData(this IApplicationBuilder app, AppDbContext appDbContext)
        {
            var service = appDbContext.Relationships;
            var relations = Enum.GetValues(typeof(Relation)).Cast<Relation>().ToList();
            var availableRelations = service.Select(e => e.Relation).ToList();

            foreach (var relation in relations)
            {
                if (!availableRelations.Any(r => r.Equals(relation)))
                {
                    service.AddAsync(new Relationship()
                    {
                        Relation = relation
                    });
                }
            }
            appDbContext.SaveChanges();
        }
    }
}


using api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext : IdentityDbContext<User>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DataContext(DbContextOptions options) :base (options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            
        }

        public DbSet<Engineere> Engineeres { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<AgeSegments> AgeSegments  { get; set; }
        public DbSet<AnnualData> AnnualDatas { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Claims> Claims { get; set; }
        public DbSet<EngineeringUnits> EngineeringUnits { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<PayMethod> PayMethods { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Status> Statuses { get; set; }
        
        public DbSet<WorkPlace> WorkPlaces { get; set; }

         public DbSet<RelationType> RelationTypes { get; set; }

         public DbSet<AnnualDataDetail> AnnualDataDetails{get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

           // builder.Entity<TaskTypeGroup>().HasMany(tg=>tg.TaskTypeCheckLists).WithOne(tg=>tg.TaskTypeGroup).OnDelete(DeleteBehavior.NoAction);




             
              builder.Entity<Person>()
            .HasOne(e => e.Engineere)
            .WithOne(ed => ed.Person)
            .HasForeignKey<Engineere>(ed => ed.Id);

            builder.Entity<EngineeringUnits>().HasMany(tg=>tg.AnnualDatas).WithOne(tg=>tg.EngineeringUnits).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<WorkPlace>().HasMany(tg=>tg.AnnualDatas).WithOne(tg=>tg.WorkPlace).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Person>()
            .HasOne(r => r.Engineere)
            .WithOne(e => e.Person)
            .OnDelete(DeleteBehavior.NoAction);

            
             builder.Entity<WorkPlace>().HasMany(tg=>tg.Engineeres).WithOne(tg=>tg.WorkPlace).OnDelete(DeleteBehavior.NoAction);



             builder.Entity<Person>().HasMany(tg=>tg.Relations).WithOne(tg=>tg.Person).OnDelete(DeleteBehavior.NoAction);
             builder.Entity<Engineere>().HasMany(tg=>tg.Relations).WithOne(tg=>tg.Engineere).OnDelete(DeleteBehavior.NoAction);



             builder.Entity<Person>().HasMany(tg=>tg.AnnualDataDetails).WithOne(tg=>tg.Person).OnDelete(DeleteBehavior.NoAction);
        }



    }
}
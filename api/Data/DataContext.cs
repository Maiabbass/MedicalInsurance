

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
         public DbSet<SurgicalProcedures> SurgicalProcedures { get; set; }
         public DbSet<EngineeringeDepar> EngineeringeDepars { get; set;}

         public DbSet<YearConfiguration> YearConfigurations { get; set;}

         public DbSet<Recovered> Recovereds { get; set; }

         public DbSet<PasswordEng> passwordEngs {get ; set;}

         public DbSet<Limits> limits { get ; set;}
         public DbSet<EnduranceRatio> EnduranceRatios { get ; set;}

         public DbSet<Images>Images{ get ; set; }
         public DbSet<Words> Words {get;set;}

         public DbSet<BlockList> blockLists{ get ; set;}

        public DbSet<Note>Notes{ get ; set ;}

        public DbSet<Subscribers2024> subscribers2024s{ get ; set;}

         
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           // builder.Entity<TaskTypeGroup>().HasMany(tg=>tg.TaskTypeCheckLists).WithOne(tg=>tg.TaskTypeGroup).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<EngineeringUnits>().HasMany(tg=>tg.AnnualDatas).WithOne(tg=>tg.EngineeringUnits).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<WorkPlace>().HasMany(tg=>tg.AnnualDatas).WithOne(tg=>tg.WorkPlace).OnDelete(DeleteBehavior.NoAction);
            
             builder.Entity<WorkPlace>().HasMany(tg=>tg.Engineeres).WithOne(tg=>tg.WorkPlace).OnDelete(DeleteBehavior.NoAction);



             builder.Entity<Person>().HasMany(tg=>tg.Relations).WithOne(tg=>tg.Person).OnDelete(DeleteBehavior.NoAction);
             builder.Entity<Engineere>().HasMany(tg=>tg.Relations).WithOne(tg=>tg.Engineere).OnDelete(DeleteBehavior.NoAction);
             



             builder.Entity<Person>().HasMany(tg=>tg.AnnualDataDetails).WithOne(tg=>tg.Person).OnDelete(DeleteBehavior.NoAction);
            // builder.Entity<Person>()
           // .HasIndex(u => u.NationalId)
            //.IsUnique();
           // builder.Entity<Person>()
           // .HasIndex(u => u.EnsuranceNumber)
           // .IsUnique();
           // builder.Entity<Engineere>().HasIndex(u => u.EngNumber).IsUnique();
           // builder.Entity<City>().HasIndex(u => u.Name).IsUnique();
           // builder.Entity<Claims>().HasIndex(u => u.EnsuranceNumber).IsUnique();
            builder.Entity<EngineeringeDepar>().HasIndex(u => u.Name).IsUnique();
            builder.Entity<EngineeringUnits>().HasIndex(u => u.Name).IsUnique();
            builder.Entity<PayMethod>().HasIndex(u => u.NameMethod).IsUnique();
            builder.Entity<Specialization>().HasIndex(u => u.Name).IsUnique();
            builder.Entity<WorkPlace>().HasIndex(u => u.Name).IsUnique();
             builder.Entity<EngineeringeDepar>()
            .HasMany(e => e.Specializations)
            .WithOne(s => s.EngineeringeDepar)
            .HasForeignKey(s => s.EngineeringeDeparId)
            .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Person>()
            .HasOne(p => p.Engineere)
            .WithOne(e => e.Person)
            .HasForeignKey<Engineere>(e => e.Id)
            .OnDelete(DeleteBehavior.Cascade);
           
           // builder.Entity<AnnualData>().HasIndex(u => u.Year).IsUnique();
            /*
            builder.Entity<AnnualData>().HasIndex(u => u.EngineereId).IsUnique();
            builder.Entity<AnnualData>()
            .HasIndex(a => new { a.Year, a.EngineereId })
            .IsUnique();
*/
            builder.Entity<YearConfiguration>().HasIndex(u => u.Year).IsUnique();
            builder.Entity<PasswordEng>().HasIndex(u => u.EngineerNumber).IsUnique();

         builder.Entity<User>()
        .HasOne(u => u.Engineer)
        .WithOne()
        .HasForeignKey<User>(u => u.EngineerId)
        .OnDelete(DeleteBehavior.Restrict);


        builder.Entity<User>()
        .HasOne(u => u.Person)
        .WithOne(p => p.User)
        .HasForeignKey<User>(u => u.PersonId)
        .OnDelete(DeleteBehavior.Restrict);

         builder.Entity<Note>()
        .HasOne(n => n.SurgicalProcedures)
        .WithMany(s => s.Notes)
        .HasForeignKey(n => n.SurgicalProcedureId)
        .OnDelete(DeleteBehavior.Cascade);



        }

    }
}
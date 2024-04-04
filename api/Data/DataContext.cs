

using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext : DbContext
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

        

    }
}
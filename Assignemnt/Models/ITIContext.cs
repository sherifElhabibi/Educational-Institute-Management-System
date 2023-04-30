using Microsoft.EntityFrameworkCore;
using Assignemnt.Models;

namespace Assignemnt.Models
{
    public class ITIContext:DbContext
    {
        public ITIContext(DbContextOptions options):base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=ITI_Project;Trusted_Connection=True");
        //    optionsBuilder.UseLazyLoadingProxies();
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasKey(a => a.CrsId);
            modelBuilder.Entity<Course>().Property(a => a.CrsId)
            .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<Department>().HasKey(a => a.DeptId);
            modelBuilder.Entity<Department>().Property(a => a.DeptId)
            .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<Student>().HasKey(a => a.StdId);
            modelBuilder.Entity<Student>().Property(a => a.StdId)
            .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<CourseStudent>().HasKey(a => new {a.StdId , a.CrsId});
            modelBuilder.Entity<CourseStudent>()
                .HasOne(pt => pt.Course)
                .WithMany(p => p.CourseStudents)
                .HasForeignKey(pt => pt.CrsId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(pt => pt.Student)
                .WithMany(t => t.CourseStudents)
                .HasForeignKey(pt => pt.StdId);
        }
        public DbSet<Assignemnt.Models.Course>? Course { get; set; }
        public DbSet<Assignemnt.Models.Department>? Department { get; set; }
        public DbSet<Assignemnt.Models.Student>? Student { get; set; }
        public DbSet<Assignemnt.Models.CourseStudent>? CourseStudents { get; set; }
        public DbSet<Assignemnt.Models.Roles>? Roles { get; set; }
    }
}

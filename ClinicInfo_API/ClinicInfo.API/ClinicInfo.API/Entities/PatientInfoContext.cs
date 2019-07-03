using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClinicInfo.API.Entities
{
    public class PatientInfoContext : DbContext
    {
        public PatientInfoContext(DbContextOptions<PatientInfoContext> options)
           : base(options)
        {
            Database.Migrate();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity("ClinicInfo.API.Entities.ServiceType", b =>
        //    {
        //        b.Property<int>("ServiceTypeId")
        //            .ValueGeneratedOnAdd()
        //            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        //        b.Property<double>("Fee");

        //        b.Property<string>("Type")
        //            .IsRequired()
        //            .HasMaxLength(50);

        //        b.HasKey("ServiceTypeId");

        //        b.ToTable("ServiceTypes");
        //    });
        //}

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PatientService> PatientServices { get; set; }

    }
}

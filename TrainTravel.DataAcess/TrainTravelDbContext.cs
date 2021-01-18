using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrainTravel.ApplicationLogic.DataModel;

namespace TrainTravel.DataAcess
{
    public class TrainTravelDbContext : DbContext
    {

        public TrainTravelDbContext(DbContextOptions<TrainTravelDbContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<TrainType> TrainTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TicketHolder> TicketHolders { get; set; }
        public DbSet<TrainRoute> TrainRoutes { get; set; }
        public DbSet<TrainSchedule> TrainSchedules { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Admin>()
                .HasIndex(u => u.Code).IsUnique();

            builder.Entity<TrainRoute>()
                .Property(u => u.Class1Price).HasColumnType("decimal(18,2)");
            builder.Entity<TrainRoute>()
                .Property(u => u.Class2Price).HasColumnType("decimal(18,2)");


        }

    }
}

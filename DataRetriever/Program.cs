using DataServer.RabbitMQ;
using System.Net.Http;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataRetriever
{
    class Program
    {
        static void Main(string[] args)
        {
            CompanyHandler.GenerateNewCompanyData("aapl");
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }
    }

    public class StockContext : DbContext
    {

        public DbSet<Company> Companies
        { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=asdf1234;Host=localhost;Port=5432;Database=StockData;Pooling=true;");
        }
    }
}

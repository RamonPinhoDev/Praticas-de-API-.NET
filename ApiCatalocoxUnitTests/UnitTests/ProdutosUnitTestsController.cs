using APICatologo.Data;
using APICatologo.DTOs.Mappins;
using APICatologo.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalocoxUnitTests.UnitTests
{
    public class ProdutosUnitTestsController
    {
        public IUnitOfWork repository;
        public IMapper mapper;
        static DbContextOptions<AppDbContext> DbContextOptions { get; }

        public static string connectionString = "server=ramon;database=APICatalogo;user id=sa;password=Fisio2021#; TrustServerCertificate=true;";
        static ProdutosUnitTestsController()
        {
            DbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }


        public ProdutosUnitTestsController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProdutoDTOMappingProfile());
            });


            mapper = config.CreateMapper();
            var context = new AppDbContext(DbContextOptions);
            repository = new UnitOfWork(context);

        }
    }
}

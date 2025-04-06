using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Domain.Data
{
    //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    //{
    //    //ApplicationDbContext 代表的是你的创建失败的那个类型
    //    public ApplicationDbContext CreateDbContext(string[] args)
    //    {
    //        var connectionString = "Server=localhost;Port=3306;User Id=root;Password=123456;Database=demo6api";

    //        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    //        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

    //        return new ApplicationDbContext(optionsBuilder.Options);
    //    }
    //}
}

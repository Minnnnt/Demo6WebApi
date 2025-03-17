using FirstDemo6DbModels;
using System.Reflection.Metadata;

//数据库连接字符串
string connectionString = "";
using (ApplicationDbContext context=new ApplicationDbContext(connectionString))
{
    //根据数据库连接字符串的配置删除数据库，如果不存在就不操作
    context.Database.EnsureDeleted();

    //根据数据库连接字符串的配置创建数据库，如果存在就不创建
    context.Database.EnsureCreated();

}

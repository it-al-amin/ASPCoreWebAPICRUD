# ASPCoreWebAPICRUD
1. I have done it database first approach
Below see this .

2.Create database and tables
3.In->Tools-> Nuget Packet Manager -> open console->write below code: At first Create A Models folder this your project.Then run below code in command:
Scaffold-DbContext "server=serverName; database=DatabaseName; trusted-coonection=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
4.protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { remove inside things} in DbContext file
5.Add 3 line in Program.cs file:
// to register sql server for connectionString 

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<MyDbContext>(item=>item.UseSqlServer(config.GetConnectionString("dbcs")));
6. In apsetting.json add this below code:
  "ConnectionStrings": {
    "dbcs": "Server=(local);Database=MyDB;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"
  },
  Now Ok ?  Perform CRUD operation Using Http verbs

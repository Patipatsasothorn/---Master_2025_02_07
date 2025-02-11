using BPI_BillOfLading.context;
using BPI_BillOfLading.Models;
using BPI_BillOfLading.Models.ApproveModel;
using BPI_BillOfLading.Models.ReportModel;
using BPI_BillOfLading.Models.ReturnStockModel;
using BPI_BillOfLading.Models.StockOutModel;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BpiLiveContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PickUpConnection")));
builder.Services.AddDbContext<BpigContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<STRContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<TableBillContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<ApproveContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));

builder.Services.AddDbContext<WHContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));  // Connecton Store
builder.Services.AddDbContext<allWHContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));  // Connecton Store
builder.Services.AddDbContext<STRContextStockOut>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));  // Connecton Store
builder.Services.AddDbContext<TableBillContextStockOut>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<BillOfLoadingContextStockOut>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<STRPagetwContextStockOut>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<RSTRPagetwContextStockOut>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<ReportContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<ReportStockOutContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<convertfcontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<statuscontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<RTableBillContextStockOut>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<ReportReturnStockOutContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<lotContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<WHbyColumnContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<binbycolumnContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));
builder.Services.AddDbContext<onhandQtycontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SetUsersConnection")));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // ตั้งค่าเวลาหมดอายุของ session nนาที
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

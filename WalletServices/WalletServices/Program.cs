using WalletServices.DAL.interfaces;
using WalletServices.DTO;
using WalletServices.DAL;
using WalletServices.Models;
using WalletServices.Services;
using WalletServices.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IProduct, ProductServices>();

builder.Services.AddScoped<IWallet, WalletDAL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/wallets", (IWallet WalletDAL) =>
{
    List<WalletDTO> walletsDto = new List<WalletDTO>();
    var walletsDtos = WalletDAL.GetAll();
    foreach (var wallet in walletsDtos)
    {
        walletsDto.Add(new WalletDTO
        {
            WalletId = wallet.WalletId,
            CustomerId = wallet.CustomerId,
            Saldo = wallet.Saldo,
        });
    }
    return Results.Ok(walletsDto);
});
app.MapPost("/api/orderdetail", async (IWallet walletDAL, WalletDTO walletDTO, IProduct ProductServices) =>
{
    try
    {
        var product = await ProductServices.GetByProductId(walletDTO.CustomerId);
        if (product == null)
        {
            return Results.BadRequest("Costumers not Found");
        }
        Wallet wallets = new Wallet()
        {
            WalletId = walletDTO.WalletId,
            CustomerId = walletDTO.CustomerId,
            Saldo = walletDTO.Saldo,
        };
        walletDAL.Insert(wallets);
        //return 201 Created
        return Results.Created($"/api/categories/{walletDTO.WalletId}", walletDTO);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

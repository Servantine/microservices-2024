using OrderServices;
using OrderServices.DAL;
using OrderServices.DAL.interfaces;
using Microsoft.OpenApi.Models;
using OrderServices.DTO;
using OrderServices.services;
using OrderServices.DAL.Interfaces;
using OrderServices.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomer, CustomerDAL>();
builder.Services.AddScoped<IOrderHeader, OrderHeaderDAL>(); 
builder.Services.AddScoped<IOrderDetail, OrderDetailDAL>();
builder.Services.AddHttpClient<IProduct, ProductServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/customer", (ICustomer CustomerDAL) =>
{
    List<CostumerDTO> CostumerDTO = new List<CostumerDTO>();
    var customer = CustomerDAL.GetAll();
    foreach (var customers in customer)
    {
        CostumerDTO.Add(new CostumerDTO
        {
            CustomerId = customers.CustomerId,
            CustomerName = customers.CustomerName
        });
    }
    return Results.Ok(CostumerDTO);
});

app.MapPost("/api/customer", (ICustomer costumerDAL, CostumerCreateDTO costumerCreateDTO) =>
{
    try
    {
        Customer costumer = new Customer
        {
            CustomerName = costumerCreateDTO.CustomerName
        };
        costumerDAL.Insert(costumer);

        object? customer = null;
        //return 201 Created
        return Results.Created($"/api/categories/{costumer.CustomerId}", customer);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapGet("/api/orderheader", (IOrderHeader OrderHeaderDAL) =>
{
    List<OrderHeaderDTO> OrderHeaderDTO = new List<OrderHeaderDTO>();
    var orderheader = OrderHeaderDAL.GetAll();
    foreach (var orderheaders in orderheader)
    {
        OrderHeaderDTO.Add(new OrderHeaderDTO
        {
            CustomerId = orderheaders.CustomerId,
            OrderHeaderId = orderheaders.OrderHeaderId,
        });
    }
    return Results.Ok(OrderHeaderDTO);
});
app.MapPost("/api/orderheader", (IOrderHeader orderheaderDAL, OrderHeaderCreateDTO orderHeaderCreateDTO) =>
{
    try
    {
        OrderHeader orderheader = new OrderHeader
        {
            CustomerId = orderHeaderCreateDTO.CustomerId
        };
        orderheaderDAL.Insert(orderheader);
        //return 201 Created
        return Results.Created($"/api/categories/{orderheader.CustomerId}", orderheader);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});
app.MapGet("/api/orderdetail", (IOrderHeader OrderDetailDAL) =>
{
    List<OrderDetailDTO> OrderDetailDTO = new List<OrderDetailDTO>();
    var orderdetail = OrderDetailDAL.GetAll();
    foreach (var orderdetails in orderdetail)
    {
        OrderDetailDTO.Add(new OrderDetailDTO
        {
            ProductId = orderdetails.CustomerId,
            OrderHeaderId = orderdetails.OrderHeaderId,
        });
    }
    return Results.Ok(OrderDetailDTO);
});
app.MapPost("/api/orderdetail", (IOrderDetail orderdetailDAL, OrderDetailCreateDTO orderDetailCreateDTO) =>
{
    try
    {
        OrderDetail orderdetail = new OrderDetail
        {
            ProductId = orderDetailCreateDTO.ProductId
        };
        orderdetailDAL.Insert(orderdetail);
        //return 201 Created
        return Results.Created($"/api/categories/{orderdetail.ProductId}", orderdetail);
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

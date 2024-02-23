using CatalogServices.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var products = new List<Product>
{
    new Product { ProductID = 1, Name = "Banana", Description = "Yellow Banana", Price = 0.99M, Category = "Fruit", Quantity = 50 },
    new Product { ProductID = 2, Name = "Orange", Description = "Sweet Orange", Price = 1.49M, Category = "Fruit", Quantity = 75 },
    new Product { ProductID = 3, Name = "Grapes", Description = "Juicy Grapes", Price = 2.99M, Category = "Fruit", Quantity = 30 },
    new Product { ProductID = 4, Name = "Watermelon", Description = "Fresh Watermelon", Price = 5.99M, Category = "Fruit", Quantity = 20 },
    new Product { ProductID = 5, Name = "Mango", Description = "Ripe Mango", Price = 3.99M, Category = "Fruit", Quantity = 40 }
};

app.MapGet("/api/products", () =>
{
    return Results.Ok(products);
});

app.MapGet("/api/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.ProductID == id);
    return Results.Ok(product);
});

// Contoh penggunaan Querry String

// app.MapGet("/api/products/getbyname", (HttpRequest request) =>
// {
//     var name = request.Query["name"].ToString();
//     var results = products.Where(p=> p.Name.ToLower().Contains(name.ToLower()));
//     return Results.Ok(results);
// });


// Contoh penggunaan String 2
app.MapGet("/api/products/getbyname", (string name) =>
{

    var results = products.Where(p=> p.Name.ToLower().Contains(name.ToLower()));
    return Results.Ok(results);
});

app.Run();


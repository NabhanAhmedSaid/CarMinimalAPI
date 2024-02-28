var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var cars = new List<Car>
{
    new Car()
    {
        Id = 1,
        Brand = "Toyota",
        Name = "Yaris"
    },
    new Car()
    {
        Id = 2,
        Brand = "Nissan",
        Name = "Altima"
    },
    new Car()
    {
        Id = 3,
        Brand ="BMW",
        Name = "M2"
    }
};

app.MapGet("/cars", () =>
{
    return Results.Ok(cars);
});

app.MapGet("/cars/{id}", (int id) => { 
var car = cars.FirstOrDefault(x => x.Id == id);

    if (car == null)
        return Results.NotFound("car does not exist");
    return Results.Ok(car);

});
app.MapPost("/cars",(Car newCar) =>
{
    cars.Add(newCar);
    return Results.Created($"/car/{newCar.Id}", newCar);

});

app.MapDelete("/cars/{id}", (int id) =>
{
    var car = cars.FirstOrDefault(c => c.Id == id);
    if (car == null)
        return Results.NotFound("car does not exist");
    cars.Remove(car);
    return Results.NoContent();
});
app.MapPut("/cars/{id}", (Car updateCar, int id) => {
    var car = cars.FirstOrDefault(c => c.Id == id);

    if (car == null)
        return Results.NotFound("car does not exist");
    car.Brand = updateCar.Brand;
    car.Name = updateCar.Name;
    return Results.Ok(car);
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();


class Car
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
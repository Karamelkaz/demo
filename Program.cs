using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddRazorPages();
var app = builder.Build();
string message = "";
app.UseCors(o => o.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

List<Order> repo = new ()
{
    new (1, DateTime.Now, "техника", "комп", "сломался", "Витя Белый Абрамович", "78422352222", "В работе", "Сергей")
};

app.MapGet("/{number}", ())
app.MapGet("/orders", (int param = 0) =>
{
    string buff = message;
    message = "";
    if (param != 0)
        return new {repo = repo.FindAll(x => x.Number == param), message = buff};
    return new {repo = repo, message = buff};
});
app.MapGet("/create", ([AsParameters] Order dto) => repo.Add(dto));
app.Run();


repo = [];
class Order
{
    int number;
    DateTime startdate;
    string orgteh;
    string fio;
    string model;
    string description;
    string phone;
    string? status;
    string? master;

    public Order(int number, DateTime startdate, string orgteh, string model, string description, string fio, string phone, string? status, string? master)
    {
        this.Number = number;
        this.Startdate = startdate;
        this.Orgteh = orgteh;
        this.Model = model;
        this.Description = description;
        this.Fio = fio;
        this.Phone = phone;
        this.Status = status;
        this.Master = master;
    }

    public int Number { get => number; set => number = value; }
    public DateTime Startdate { get => startdate; set => startdate = value; }
    public string Orgteh { get => orgteh; set => orgteh = value; }
    public string Model { get => model; set => model = value; }
    public string Description { get  => description; set => description = value; }
    public string Fio { get => fio; set => fio = value; }
    public string Phone { get => phone; set => phone = value; }
    public string? Status { get => status; set => status = value; }
    public string? Master { get => master; set => master = value; }
}
record OrderDto(int number, string? status, string? master);
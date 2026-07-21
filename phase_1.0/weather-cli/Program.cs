using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

// Crear la variable con el nombre del archivo

const string fileName = "weather-data.json";


// A partir de aquí comienza el programa
Dictionary<string, List<DailyReading>> cities;

if (File.Exists(fileName))
{
    Console.WriteLine("Cargando datos desde weather-data.json...");

    string json = File.ReadAllText(fileName);

    cities = JsonConvert.DeserializeObject<Dictionary<string, List<DailyReading>>>(json)!;
}
else
{
    cities = new Dictionary<string, List<DailyReading>>
    {
        ["San Jose"] = new()
    {
        new DailyReading(new DateOnly(2026, 7, 1), "Sunny", 31),
        new DailyReading(new DateOnly(2026, 7, 2), "Rainy", 25),
        new DailyReading(new DateOnly(2026, 7, 3), "Cloudy", 28)
    },

    ["Cartago"] = new()
    {
        new DailyReading(new DateOnly(2026, 7, 1), "Cloudy", 22),
        new DailyReading(new DateOnly(2026, 7, 2), "Rainy", 20),
        new DailyReading(new DateOnly(2026, 7, 3), "Rainy", 19)
    },

    ["Heredia"] = new()
    {
        new DailyReading(new DateOnly(2026, 7, 1), "Sunny", 29),
        new DailyReading(new DateOnly(2026, 7, 2), "Windy", 27),
        new DailyReading(new DateOnly(2026, 7, 3), "Cloudy", 26)
        
    }
};

    string json = JsonConvert.SerializeObject(cities, Formatting.Indented);

    File.WriteAllText(fileName, json);
}


Console.WriteLine("El diccionario fue creado correctamente.");

Console.WriteLine("=== Ciudades disponibles ===");

foreach (var city in cities)
{
    Console.WriteLine(city.Key);
}

foreach (var city in cities)
{
    Console.WriteLine($"\nCiudad: {city.Key}");

    foreach (DailyReading reading in city.Value)
    {
        Console.WriteLine($"{reading.Date} | {reading.Condition} | {reading.TempC}°C");
    }
}

Console.WriteLine("Ingrese el nombre de una ciudad: ");
string cityName = Console.ReadLine()!;


if (cities.TryGetValue(cityName, out List<DailyReading>? readings))
{
    Console.WriteLine($"\nLecturas para {cityName}:\n");

    foreach (DailyReading reading in readings)
    {
        Console.WriteLine($"{reading.Date} | {reading.Condition} | {reading.TempC}°C");
    }
}
else
{
    Console.WriteLine("La ciudad no existe.");
}

HashSet<string> conditions = new();
foreach (var city in cities)
{
    foreach (DailyReading reading in city.Value)
    {
        conditions.Add(reading.Condition);

    }
}

Console.WriteLine ("\n=== Condicioes climáticas únicas ===");

foreach (string condition in conditions)
{
    Console.WriteLine(condition);
}

Console.WriteLine("ingrese la temperatura: ");
double limite = double.Parse(Console.ReadLine()!);

var HotDays = readings!.Where(r => r.TempC > limite);

Console.WriteLine($"\nDías con temperatura mayor a {limite}°C:");

foreach (DailyReading reading in HotDays)
{
    Console.WriteLine($"{reading.Date} | {reading.Condition} | {reading.TempC}°C");
}

// Dias mas calurosos
Console.WriteLine("\n=== Días más calurosos ===");
var hottesDays = readings!.OrderByDescending (r => r.TempC).Take(3);

foreach (DailyReading day in hottesDays)
{
    Console.WriteLine($"{day.Date} | {day.Condition} | {day.TempC}°C");
}

// Estadisticas por Condicion
Console.WriteLine("\n=== Estadísticas por Condición ===");
var groups = readings!.GroupBy(r => r.Condition);

foreach (var group in groups)
{
    Console.WriteLine($"\nCondición: {group.Key}");
    Console.WriteLine($"Cantidad de días: {group.Count()}");
    Console.WriteLine($"Temperatura promedio: {group.Average(r => r.TempC):F2}°C");
}


// Definición del tipo
record DailyReading(DateOnly Date, string Condition, double TempC);
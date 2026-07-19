using System;
using System.Collections.Generic;

// A partir de aquí comienza el programa
List<DailyReading> readings = new()
{
    new DailyReading(new DateOnly(2026, 7, 1), "Sunny", 31),
    new DailyReading(new DateOnly(2026, 7, 2), "Rainy", 25),
    new DailyReading(new DateOnly(2026, 7, 3), "Cloudy", 28),
    new DailyReading(new DateOnly(2026, 7, 4), "Sunny", 33),
    new DailyReading(new DateOnly(2026, 7, 5), "Windy", 26),
    new DailyReading(new DateOnly(2026, 7, 6), "Rainy", 24),
    new DailyReading(new DateOnly(2026, 7, 7), "Sunny", 32),
    new DailyReading(new DateOnly(2026, 7, 8), "Cloudy", 27),
    new DailyReading(new DateOnly(2026, 7, 9), "Sunny", 34),
    new DailyReading(new DateOnly(2026, 7, 10), "Rainy", 23)
};

Console.WriteLine("=== Weather CLI ===\n");

foreach (DailyReading reading in readings)
{
    Console.WriteLine($"{reading.Date} | {reading.Condition} | {reading.TempC}°C");
}

Console.WriteLine();

Console.WriteLine("ingrese la temperatura: ");
double limite = double.Parse(Console.ReadLine()!);

var HotDays = readings.Where(r => r.TempC > limite);

Console.WriteLine($"\nDías con temperatura mayor a {limite}°C:");

foreach (DailyReading reading in HotDays)
{
    Console.WriteLine($"{reading.Date} | {reading.Condition} | {reading.TempC}°C");
}

// Dias mas calurosos
Console.WriteLine("\n=== Días más calurosos ===");
var hottesDays = readings.OrderByDescending (r => r.TempC).Take(3);

foreach (DailyReading day in hottesDays)
{
    Console.WriteLine($"{day.Date} | {day.Condition} | {day.TempC}°C");
}

// Estadisticas por Condicion
Console.WriteLine("\n=== Estadísticas por Condición ===");
var groups = readings.GroupBy(r => r.Condition);

foreach (var group in groups)
{
    Console.WriteLine($"\nCondición: {group.Key}");
    Console.WriteLine($"Cantidad de días: {group.Count()}");
    Console.WriteLine($"Temperatura promedio: {group.Average(r => r.TempC):F2}°C");
}

// Definición del tipo
record DailyReading(DateOnly Date, string Condition, double TempC);
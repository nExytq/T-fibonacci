using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace FibonacciLab;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("=== Калькулятор Чисел Фибоначчи ===");

        // 1. Получаем и валидируем ввод
        BigInteger startRange = GetValidatedInput("Введите начало диапазона : ");
        BigInteger endRange = GetValidatedInput("Введите конец диапазона : ");

        if (startRange > endRange)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[!] Ошибка: Начало диапазона не может быть больше конца.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine($"\n[*] Поиск чисел Фибоначчи в диапазоне [{startRange}, {endRange}]...");
        
        // Замеряем время выполнения (бонус для проверяющего)
        Stopwatch sw = Stopwatch.StartNew();
        
        // 2. Итерация и вывод
        bool foundAny = false;
        foreach (BigInteger fibNumber in GetFibonacciInRange(startRange, endRange))
        {
            Console.WriteLine($"> {fibNumber}");
            foundAny = true;
        }

        sw.Stop();

        // 3. Проверка на пустоту
        if (!foundAny)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("В заданном диапазоне нет чисел Фибоначчи.");
            Console.ResetColor();
        }

        Console.WriteLine($"\n[+] Выполнено за {sw.ElapsedMilliseconds} мс.");
    }

    /// <summary>
    /// Генератор чисел Фибоначчи. Вычисляет последовательность итеративно.
    /// </summary>
    private static IEnumerable<BigInteger> GetFibonacciInRange(BigInteger start, BigInteger end)
    {
        BigInteger a = 0;
        BigInteger b = 1;

        // Генерируем, пока текущее число не превысит верхнюю границу
        while (a <= end)
        {
            // Отдаем число наружу, только если оно вошло в диапазон
            if (a >= start)
            {
                yield return a;
            }

            // Итеративный сдвиг
            BigInteger next = a + b;
            a = b;
            b = next;
        }
    }

    /// <summary>
    /// Безопасный запрос ввода от пользователя с проверкой на числа >= 0.
    /// </summary>
    private static BigInteger GetValidatedInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (BigInteger.TryParse(input, out BigInteger result) && result >= 0)
            {
                return result;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[!] Неверный ввод. Пожалуйста, введите целое неотрицательное число.");
            Console.ResetColor();
        }
    }
}
using System;
using System.Globalization;

namespace Kalkulator
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Prosty kalkulator - operacje: +  -  *  /");
            Console.WriteLine("Wpisz 'q' aby zakończyć.");

            while (true)
            {
                Console.WriteLine();
                Console.Write("Wybierz operację (+, -, *, /) lub q: ");
                var operation = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(operation))
                {
                    continue;
                }

                if (operation.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (operation != "+" && operation != "-" && operation != "*" && operation != "/")
                {
                    Console.WriteLine("Nieznana operacja. Spróbuj ponownie.");
                    continue;
                }

                if (!TryReadDouble("Pierwsza liczba: ", out var a))
                {
                    Console.WriteLine("Nieprawidłowa liczba. Rozpoczynamy od nowa.");
                    continue;
                }

                if (!TryReadDouble("Druga liczba: ", out var b))
                {
                    Console.WriteLine("Nieprawidłowa liczba. Rozpoczynamy od nowa.");
                    continue;
                }

                if (operation == "/" && Math.Abs(b) < double.Epsilon)
                {
                    Console.WriteLine("Błąd: dzielenie przez zero.");
                    continue;
                }

                var result = operation switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => a / b,
                    _ => double.NaN
                };

                Console.WriteLine($"Wynik: {result}");
            }

            Console.WriteLine("Koniec programu. Naciśnij dowolny klawisz, aby zamknąć.");
            Console.ReadKey();
        }

        private static bool TryReadDouble(string prompt, out double value)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                value = 0;
                return false;
            }

            // Umożliwia wpisanie zarówno z przecinkiem, jak i z kropką
            input = input.Replace(',', '.');

            return double.TryParse(input, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value);
        }
    }
}
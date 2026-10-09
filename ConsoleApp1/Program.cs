using System;
using System.Collections.Generic;

class TextStatistics
{
    public string Text;
    public int WordCount;
    public string ShortestWord;
    public int Sentences;
    public int Vowels;
    public int Consonants;
    public string LongestWord;
    public int[] LetterCounts;
}

class Program
{
    static void Main()
    {
        List<TextStatistics> history = new List<TextStatistics>();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Новый текст");
            Console.WriteLine("2 - Посмотреть прошлую статистику");
            Console.WriteLine("0 - Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        Console.WriteLine("Введите текст минимум на 100 символов:");
                        string text = Console.ReadLine();

                        while (text == null || text.Length < 100)
                        {
                            Console.WriteLine("Текст должен содержать минимум 100 символов.");
                            text = Console.ReadLine();
                        }

                        // Разделяем текст на слова
                        string[] words = text.Split(
                            new char[] { ' ', '.', ',', '!', '?', ';', ':', '"', '(', ')', '\t' },
                            StringSplitOptions.RemoveEmptyEntries);

                        if (words.Length == 0)
                        {
                            Console.WriteLine("В тексте нет слов.");
                            break;
                        }

                        int wordCount = words.Length;

                        // Самое короткое слово
                        string shortest = words[0];

                        for (int i = 1; i < words.Length; i++)
                        {
                            if (words[i].Length < shortest.Length)
                            {
                                shortest = words[i];
                            }
                        }

                        // Количество предложений
                        int sentences = 0;

                        for (int i = 0; i < text.Length; i++)
                        {
                            char c = text[i];

                            if (c == '.' || c == '!' || c == '?')
                            {
                                bool nextIsEnd = i + 1 < text.Length &&
                                    (text[i + 1] == '.' || text[i + 1] == '!' || text[i + 1] == '?');

                                if (!nextIsEnd)
                                {
                                    sentences++;
                                }
                            }
                        }

                        // Гласные и согласные
                        int vowelsCount = 0;
                        int consonantsCount = 0;

                        string vowels = "аеёиоуыэюяАЕЁИОУЫЭЮЯ";
                        string consonants = "бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ";

                        for (int i = 0; i < text.Length; i++)
                        {
                            char c = text[i];

                            if (vowels.IndexOf(c) >= 0)
                            {
                                vowelsCount++;
                            }

                            if (consonants.IndexOf(c) >= 0)
                            {
                                consonantsCount++;
                            }
                        }

                        // Самое длинное слово
                        string longest = words[0];

                        for (int i = 1; i < words.Length; i++)
                        {
                            if (words[i].Length > longest.Length)
                            {
                                longest = words[i];
                            }
                        }

                        // Частота букв
                        int[] letterCounts = new int[65536];

                        for (int i = 0; i < text.Length; i++)
                        {
                            char c = text[i];

                            if (char.IsLetter(c))
                            {
                                letterCounts[char.ToLower(c)]++;
                            }
                        }

                        Console.WriteLine();
                        Console.WriteLine("Частота букв:");

                        for (int i = 0; i < letterCounts.Length; i++)
                        {
                            if (letterCounts[i] > 0)
                            {
                                Console.WriteLine($"{(char)i} - {letterCounts[i]}");
                            }
                        }

                        // Создаём объект статистики
                        TextStatistics statistics = new TextStatistics();

                        statistics.Text = text;
                        statistics.WordCount = wordCount;
                        statistics.ShortestWord = shortest;
                        statistics.Sentences = sentences;
                        statistics.Vowels = vowelsCount;
                        statistics.Consonants = consonantsCount;
                        statistics.LongestWord = longest;
                        statistics.LetterCounts = letterCounts;

                        // Сохраняем статистику в историю
                        history.Add(statistics);

                        Console.WriteLine();
                        Console.WriteLine("Статистика:");
                        Console.WriteLine("Количество слов: " + wordCount);
                        Console.WriteLine("Самое короткое слово: " + shortest);
                        Console.WriteLine("Количество предложений: " + sentences);
                        Console.WriteLine("Гласных: " + vowelsCount);
                        Console.WriteLine("Согласных: " + consonantsCount);
                        Console.WriteLine("Самое длинное слово: " + longest);

                        break;
                    }

                case "2":
                    {
                        if (history.Count == 0)
                        {
                            Console.WriteLine("История пока пустая.");
                        }
                        else
                        {
                            for (int i = 0; i < history.Count; i++)
                            {
                                TextStatistics statistics = history[i];

                                Console.WriteLine();
                                Console.WriteLine("Текст №" + (i + 1));
                                Console.WriteLine("Количество слов: " + statistics.WordCount);
                                Console.WriteLine("Самое короткое слово: " + statistics.ShortestWord);
                                Console.WriteLine("Количество предложений: " + statistics.Sentences);
                                Console.WriteLine("Гласных: " + statistics.Vowels);
                                Console.WriteLine("Согласных: " + statistics.Consonants);
                                Console.WriteLine("Самое длинное слово: " + statistics.LongestWord);

                                Console.WriteLine("Частота букв:");

                                for (int j = 0; j < statistics.LetterCounts.Length; j++)
                                {
                                    if (statistics.LetterCounts[j] > 0)
                                    {
                                        Console.WriteLine(
                                            $"{(char)j} - {statistics.LetterCounts[j]}");
                                    }
                                }
                            }
                        }

                        break;
                    }

                case "0":
                    return;

                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
}

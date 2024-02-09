using System.Text;
using System.Collections.Generic;

namespace HammerAndNails
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string answer;
            // Задаем силу удара - 50 миллиметров за один удар
            int hitRange = 50;
            // создаем переменную для подсчета количества ударов в цикле
            int hitCount = 0;
            // создаем переменную, которой зададим длину гвоздя из массива
            int hitPoint;
            // общая сумма ударов
            int iterationSum;
            // список для сохранения количества ударов по каждому гвоздю
            List<int> iterationCount = new List<int>();
            Console.OutputEncoding = Encoding.UTF8;
            string condition = "Нам нужно забить несколько гвоздей разных размеров и посчитать количество ударов. За каждый удар вы забиваете гвоздь на глубину 50 миллиметров Начать работу? (Нажмите y/n)";
            foreach (char i in condition) 
            {
                Console.Write(i);
                Thread.Sleep(50);
            }
            Console.WriteLine();
            do
            {
                answer = Console.ReadLine();
                if (answer != "y")
                {
                    Console.WriteLine("Не ленись!");
                }
                else
                {
                    Console.WriteLine("начинаем забивать говозди");
                }
            } while (answer != "y");
            // создаем список гвоздей. Значение в списке равно длине гвоздя.
            List<int> nailsBox = new List<int>() {200, 300, 100, 350, 50, 400};
            // создаем цикл для работы с гвоздями по очереди
            foreach (int i in nailsBox) 
            {
                // переменной hitPoint присваиваем длину гвоздя
                hitPoint = i;
                Console.WriteLine($"Берем гвоздь размера {i} миллиметров");
                Thread.Sleep(1500);
                Console.WriteLine("Забиваем");
                // создаем цикл, повторяющийся до тех пор, пока hitPoint не станет равным 0. То есть гвоздь забит
                do
                {
                    // учитываем каждую итерацию
                    hitCount++;
                    Console.WriteLine($"Удар-{hitCount}");
                    Console.Beep();
                    // подсчитываем остаток длины гвоздя
                    hitPoint = hitPoint - hitRange;
                    Thread.Sleep(400);

                } while (hitPoint > 0);
                // добавляем получившееся количество итераций цикла
                iterationCount.Add(hitCount);
                Console.WriteLine($"Гвоздь размера {i} миллиметров забит. Вам потребовалось {hitCount} ударов. ");
                // обнуляем счетчик итераций после завершения цикла, так как далее будем забивать новый гвоздь
                hitCount = 0;
            }
            // суммируем итерации цикла do
            iterationSum = iterationCount.Sum();
            Thread.Sleep(1000);
            Console.WriteLine($"Все {nailsBox.Count} гвоздей забиты.");
            Thread.Sleep(2000);
            Console.WriteLine($"На выполнение всей работы вам понадобилось {iterationSum} ударов");





        }
    }
}

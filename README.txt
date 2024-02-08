using System.Reflection;
using System.Text;
using System.IO;

namespace Taxes
{
    internal class Program
    {
        public class Employee
        {
            public string firstName;
            public string lastName;
            public string position;
            public string setPath;
            bool checkChar;
            string hourSalaryString;
            string totalHoursThisMonthString;
            
            public void EmployeeData()
            {
                Console.WriteLine("Введите имя сотрудника.");
                do
                {
                    firstName = (Console.ReadLine());
                    if (string.IsNullOrEmpty(firstName))
                    {
                        Console.WriteLine("Это обязательное поле. Введите имя.");
                    }
                    foreach (char c in firstName)
                    {
                        if (!char.IsLetter(c))
                        {
                            checkChar = false;
                            Console.WriteLine($"Строка содержит недопустимый символ-'{c}'. Имя может состоять только из букв. Попробуйте еще раз.");
                            break;
                        }
                        else checkChar = true;
                    }
                } while (string.IsNullOrEmpty(firstName) || !checkChar);
                Console.WriteLine("Спасибо. Теперь введите фамилию");
                do
                {
                    lastName = (Console.ReadLine());
                    if (string.IsNullOrEmpty(lastName))
                    {
                        Console.WriteLine("Это обязательное поле. Введите фамилию.");
                    }
                    foreach (char c in lastName)
                    {
                        if (!char.IsLetter(c))
                        {
                            checkChar = false;
                            Console.WriteLine($"Строка содержит недопустимый символ-'{c}'. Фамилия может состоять только из букв. Попробуйте еще раз.");
                            break;
                        }
                        else checkChar = true;
                    }
                } while (string.IsNullOrEmpty(lastName) || !checkChar);
                Console.WriteLine("Спасибо. Теперь введите должность");
                do
                {
                    position = (Console.ReadLine());
                    if (string.IsNullOrEmpty(position))
                    {
                        Console.WriteLine("Это обязательное поле. Введите должность сотрудника.");
                    }
                } while (string.IsNullOrEmpty(position));
            }
            public void Salarydata(double incomeTaxRate, double retirementContributionRate, double unionDuesRate, ref bool flag, out double hourSalary, out double totalHoursThisMonth, out double incomeTax, out double retirementContribution, out double unionDues, out double accured, out double subtracted, out double toPay)
            {
                Console.WriteLine("Введите часовую ставку");
                hourSalaryString = Console.ReadLine();
                if (double.TryParse(hourSalaryString, out hourSalary))
                {
                    Console.WriteLine("Принято.");
                    Thread.Sleep(1500);
                }
                else
                {
                    do
                    {
                        Console.WriteLine("Введен неверный формат данных либо пустая строка. Попробуйте еще раз. Допустимы только целые и дробные числа.");
                        hourSalaryString = Console.ReadLine();
                    } while (!double.TryParse(hourSalaryString, out hourSalary)); 
                }
                Console.WriteLine("Введите общее количество рабочих часов за месяц.");

                totalHoursThisMonthString = Console.ReadLine();
                if (double.TryParse(totalHoursThisMonthString, out totalHoursThisMonth))
                {
                    Console.WriteLine("Принято.");
                    Thread.Sleep(1500);
                }
                else
                {
                    do
                    {
                        Console.WriteLine("Введен неверный формат данных либо пустая строка. Попробуйте еще раз. Допустимы только целые и дробные числа.");
                        totalHoursThisMonthString = Console.ReadLine();
                    } while (!double.TryParse(totalHoursThisMonthString, out totalHoursThisMonth));
                }
                Console.WriteLine("Расчет заработной платы");
                accured = hourSalary * totalHoursThisMonth;
                incomeTax = accured / 100 * incomeTaxRate;
                retirementContribution = accured / 100 * retirementContributionRate;
                unionDues = accured / 100 * unionDuesRate;
                toPay = accured - incomeTax - retirementContribution - unionDues;
                subtracted = accured - toPay;
                if (accured is double & incomeTax is double & retirementContribution is double & unionDues is double & toPay is double & subtracted is double) flag = true;
                Thread.Sleep (2000);
            }
            public void SaveToFile( double hourSalary, double totalHoursThisMonth, double accured, double incomeTaxRate, double incomeTax, double retirementContributionRate, double retirementContribution, double unionDuesRate, double unionDues, double subtracted, double toPay) 
            {
                do
                {
                    setPath = (Console.ReadLine());
                    if (string.IsNullOrEmpty(setPath))
                    {
                        Console.WriteLine("Это обязательное поле. Заполните корректно.");
                    }
                } while (string.IsNullOrEmpty(setPath));
                Console.WriteLine();
                string path = @$"c:\{setPath}\Расчетный_лист_сотрудника_{firstName}{lastName}.txt";
                if (!File.Exists(path))
                {
                    using (StreamWriter save = File.CreateText(path))
                    {
                        save.Write("Сотрудник - ");
                        save.Write(firstName + " ");
                        save.Write(lastName);
                        save.WriteLine();
                        save.Write("Должность - ");
                        save.Write(position);
                        save.WriteLine();
                        save.Write($"Ставка за час/всего часов");
                        save.Write("                  ");
                        save.Write($"{hourSalary}/{totalHoursThisMonth}");
                        save.WriteLine();
                        save.Write("Начислено:");
                        save.Write("                                 ");
                        save.Write(accured + " рублей");
                        save.WriteLine();
                        save.Write($"Подоходный налог {incomeTaxRate}%:");
                        save.Write("                      ");
                        save.Write(incomeTax + " рублей");
                        save.WriteLine();
                        save.Write($"Пенсионный взнос {retirementContributionRate}%:");
                        save.Write("                       ");
                        save.Write(retirementContribution + " рублей");
                        save.WriteLine();
                        save.Write($"Профсоюзный взнос {unionDuesRate}%:");
                        save.Write("                      ");
                        save.Write(unionDues + " рублей");
                        save.WriteLine();
                        save.Write("Всего удержано:");
                        save.Write("                            ");
                        save.Write(subtracted + " рублей");
                        save.WriteLine();
                        save.Write("К оплате:");
                        save.Write("                                  ");
                        save.Write(toPay + " рублей");
                        Console.WriteLine("Сохранение...");
                        Thread.Sleep(2000);
                        
                    }
                }
            }
            static void Main(string[] args)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;
                string intro = "Расчетный лист с вычетом налогов.";
                double hourSalary;
                double totalHoursThisMonth;
                const double incomeTaxRate = 17;
                const double retirementContributionRate = 1;
                const double unionDuesRate = 1;
                double incomeTax;
                double retirementContribution;
                double unionDues;
                double accured;
                double subtracted;
                double toPay;
                string doSave = "Сохранить данные? Y/N";
                string answerToSave;
                bool flag = false;
                
                var emp = new Employee();
                foreach (char c in intro)
                {
                    Console.Write(c);
                    Thread.Sleep(20);
                }
                Console.WriteLine();
                emp.EmployeeData();
                emp.Salarydata(17, 1, 1, ref flag, out hourSalary, out totalHoursThisMonth, out incomeTax, out retirementContribution, out unionDues, out accured, out subtracted, out toPay);
                Console.Write("Результат расчетов:");
                Console.Write("                        ");
                Console.Write(flag); 
                Console.WriteLine();
                Console.Write("Сотрудник - ");
                Thread.Sleep(100);
                Console.Write(emp.firstName + " ");
                Thread.Sleep(100);
                Console.Write(emp.lastName);
                Console.WriteLine();
                Console.Write("Должность - ");
                Console.Write(emp.position);
                Console.WriteLine();
                Console.Write($"Ставка за час/всего часов");
                Console.Write("                  ");
                Console.Write($"{hourSalary}/{totalHoursThisMonth}");
                Console.WriteLine();
                Console.Write("Начислено:");
                Console.Write("                                 ");
                Console.Write(accured + " рублей");
                Console.WriteLine();
                Console.Write($"Подоходный налог {incomeTaxRate}%:");
                Console.Write("                      ");
                Console.Write(incomeTax + " рублей");
                Console.WriteLine();
                Console.Write($"Пенсионный взнос {retirementContributionRate}%:");
                Console.Write("                       ");
                Console.Write(retirementContribution + " рублей");
                Console.WriteLine();
                Console.Write($"Профсоюзный взнос {unionDuesRate}%:");
                Console.Write("                      ");
                Console.Write(unionDues + " рублей");
                Console.WriteLine();
                Console.Write("Всего удержано:");
                Console.Write("                            ");
                Console.Write(subtracted + " рублей");
                Console.WriteLine();
                Console.Write("К оплате:");
                Console.Write("                                  ");
                Console.Write(toPay + " рублей");
                Console.WriteLine();
                Thread.Sleep(5000);
                foreach (char c in doSave) 
                { 
                    Console.Write(c);
                    Thread.Sleep(50);
                }
                Console.WriteLine();
                do
                {
                    answerToSave = Console.ReadLine();
                    if (answerToSave == "y" || answerToSave == "Y" || answerToSave == "у" || answerToSave == "У") 
                    {
                        Console.WriteLine($"Укажите путь к СУЩЕСТВУЮЩЕЙ папке, в которую хотите сохранить файл. (Заполните только промежуток вместо многоточий). c:\\.......\\Расчетный_лист_сотрудника_{emp.firstName}{emp.lastName}.txt");
                        Console.WriteLine("Пример: Users\\Lenovo\\Downloads");
                    }
                    else if (answerToSave == "n" || answerToSave == "N" || answerToSave == "н" || answerToSave == "Н") 
                    {
                        Console.WriteLine("Завершение работы программы");
                        Thread.Sleep (2000);
                        return;
                    }
                    else Console.WriteLine("Пожалуйста, ответьте корректно");
                } while (answerToSave != "Y" && answerToSave != "y" && answerToSave != "у" && answerToSave != "У");
                emp.SaveToFile(hourSalary, totalHoursThisMonth, accured, incomeTaxRate, incomeTax, retirementContributionRate, retirementContribution, unionDuesRate, unionDues, subtracted, toPay);
                Console.WriteLine("Работа программы завершена. Нажмите 'Enter' для выхода из консоли.");
                Console.ReadKey();
            }
        }
    }
}


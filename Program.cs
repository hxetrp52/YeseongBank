using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text.Json;

public class Program
{
    static int haveMoney = 0;

    enum TransactionType
    {
        Deposit,
        Withdraw
    }

    struct BankRecord
    {
        public string date;
        public TransactionType type;
        public string amount;
    }

    static List<BankRecord> records = new List<BankRecord>();


    public static void Main(string[] args)
    {

        Console.WriteLine("예성 뱅크에 입장했습니다.");
        Console.WriteLine("================================");
        Console.WriteLine("원하시는 작업을 선택하세요:");
        Console.WriteLine("1. 입금");
        Console.WriteLine("2. 출금");
        Console.WriteLine("3. 잔액 조회");
        Console.WriteLine("4. 거래 내역 조회");
        Console.WriteLine("5. 종료");
        Console.WriteLine("================================");


        while (true)
        {
            string choice = Console.ReadLine();
            ResetConsole();
            switch (choice)
            {
                case "1":
                    Deposit();
                    break;
                case "2":
                    Withdraw();
                    break;
                case "3":
                    CheckMoney();
                    break;
                case "4":
                    CheckRecords();
                    break;
                case "44":
                    ClearRecords();
                    break; 
                case "5":
                    Console.WriteLine("예성 뱅크를 이용해 주셔서 감사합니다.");
                    return;
                default:
                    Console.WriteLine("잘못된 선택입니다. 다시 시도해주세요.");
                    Console.ReadLine();
                    ResetConsole();
                    break;
            }
        }
    }

    static void Deposit()
    {
        Console.WriteLine("입금할 금액을 입력하세요:");
        string input = Console.ReadLine();
        if(int.TryParse(input, out int value) && value > 0) 
        {
            haveMoney += value;
            WriteColorText(ChangeCommaNumber(value), ConsoleColor.Yellow);
            Console.WriteLine("원이 입금되었습니다.");
            records.Add(SaveRecord(TransactionType.Deposit, value));
        }
        else
        {
            Console.WriteLine("잘못된 금액입니다. 다시 시도해주세요.");
        }
        Console.ReadLine();
        ResetConsole();
    }

    static void Withdraw()
    {
        Console.WriteLine("출금할 금액을 입력하세요:");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int value) && value > 0)
        {
            if (haveMoney >= value)
            {
                haveMoney -= value;
                WriteColorText(ChangeCommaNumber(value), ConsoleColor.Yellow);
                Console.WriteLine("원이 출금되었습니다.");
                records.Add(SaveRecord(TransactionType.Withdraw, value));
            }
            else
            {
                Console.WriteLine("잔액이 부족합니다.");
            }
        }
        else
        {
            Console.WriteLine("잘못된 금액입니다. 다시 시도해주세요.");
        }
        Console.ReadLine();
        ResetConsole();
    }

    static void CheckMoney()
    {
        Console.WriteLine($"현재 잔액");
        WriteColorText(ChangeCommaNumber(haveMoney), ConsoleColor.Yellow);
        Console.WriteLine("원");
        Console.ReadLine();
        ResetConsole();
    }

    static void ResetConsole()
    {
        Console.Clear();
        Console.WriteLine("================================");
        Console.WriteLine("원하시는 작업을 선택하세요:");
        Console.WriteLine("1. 입금");
        Console.WriteLine("2. 출금");
        Console.WriteLine("3. 잔액 조회");
        Console.WriteLine("4. 거래 내역 조회 / 44. 거래 내역 초기화");
        Console.WriteLine("5. 종료");
        Console.WriteLine("================================");
    }

    static string ChangeCommaNumber(int number)
    {
        if(number == 0) return "0";
        return number.ToString("#,#", new CultureInfo("ko-KR"));
    }

    static void WriteColorText(string text, ConsoleColor color, bool newline = false)
    {
        Console.ForegroundColor = color;
        if (newline)
        {
            Console.WriteLine(text);
        }
        else
        {
            Console.Write(text);
        }
        Console.ResetColor();
    }


    static BankRecord SaveRecord(TransactionType type, int value) 
    {
        return new BankRecord
        {
            date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            type = type,
            amount = ChangeCommaNumber(value)
        };
    }

    static void CheckRecords()
    {
        Console.WriteLine();
        if(records.Count == 0)
        {
            Console.WriteLine("거래 내역이 없습니다.");
            Console.ReadLine();
            ResetConsole();
            return;
        }
        Console.WriteLine("거래 내역:");
        for (int i = 0; i < records.Count; i++)
        {
            Console.WriteLine($"[{records[i].date}]");
            if (records[i].type == TransactionType.Deposit)
            {
                WriteColorText("입금", ConsoleColor.Green, true);
            }
            else
            {
                WriteColorText("출금", ConsoleColor.Red, true);
            }
            WriteColorText(records[i].amount, ConsoleColor.Yellow);
            Console.WriteLine("원");
            Console.WriteLine("--------------------");
        }
        Console.ReadLine();
        ResetConsole();
    }

    static void ClearRecords()
    {
        records.Clear();
        Console.WriteLine("거래 내역이 초기화되었습니다.");
        Console.ReadLine();
        ResetConsole();
    }
}
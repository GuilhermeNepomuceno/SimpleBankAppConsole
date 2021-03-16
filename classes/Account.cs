using System;

namespace BankConsoleApp
{
    
public class Account
{
    private string holderName { get; set; }
    private double balance { get; set; }
    private double credit { get; set; }
    private AccountType accountType { get; set; }
    
    public Account (AccountType accountType, string userName, double balance, double credit)
    {
        this.accountType = accountType;
        this.holderName = userName;
        this.balance = balance;
        this.credit = credit;
    }

    public bool Withdraw(double value)
    {
        if(this.balance + this.credit >= value)
        {
            var oldBalance = this.balance;
            this.balance-= value;
            Console.WriteLine("\n\t\t\t\tSaque bem-sucedido!");
            Console.WriteLine("\n\tSaldo anterior: " + oldBalance + "\tSaldo atual:" + this.balance + "\tValor retirado: " + value);
            Console.WriteLine("X------------------------------------------------------------------------------------X");
            return true;
        }else
        {    
            Console.WriteLine("\n\t\t\t\tSaldo insuficiente: " + this.balance);
            Console.WriteLine("X------------------------------------------------------------------------------------X");
            return false;
        }
    }

    public void Deposit(double value)
    {
        Console.WriteLine("\n\t\t\t\tDepósito bem-sucedido!" +
        "\n\tSaldo anterior: {0}\tSaldo atual: {1}",this.balance, this.balance += value);
        Console.WriteLine("X------------------------------------------------------------------------------------X");
    }

    private void Deposit(double value, Account destinyAccount)
    {
        destinyAccount.balance += value;
        Console.WriteLine("\n\t\t\t\tTransferência bem-sucedida!");
        Console.WriteLine("X------------------------------------------------------------------------------------X");
    }

    public void Transfer(double value, Account destinyAccount)
    {
        if(this.Withdraw(value))
        {
         destinyAccount.Deposit(value, destinyAccount);
        }
    }

    public override string ToString()
    {
        string accountInformation = "\nTitular da conta: " +  this.holderName + "\nTipo de conta: " + this.accountType + "\t\nSaldo: " + this.balance + "\t\nCrédito: " + this.credit;
        return accountInformation;
               
    }

    public string getHolderName()
    {
        return this.holderName;
    }
    
}
}
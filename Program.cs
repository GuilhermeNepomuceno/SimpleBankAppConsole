using System;
using System.Collections.Generic;

namespace BankConsoleApp
{
    class Program
    {
        static List<Account> accountList = new List<Account>();
        static void Main(string[] args)
        {
            string choosedOption;
            do{
                choosedOption  = getOption();
                switch (choosedOption.ToUpper())
                {
                    case "1":
                        //listar contas
                        ListAccounts();
                    break;
                    case "2":
                        //nova conta
                        try
                        {
                            CreateNewAccount();
                        }catch(Exception e)
                        {
                            Console.WriteLine(e.Message + "\n Pressione qualquer tecla para continuar.");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    break;
                    case "3":
                        //transferencia
                        Transfer();
                    break;
                    case "4":
                        //sacar
                        Withdraw();
                    break;
                    case "5":
                        //depositar
                        Deposit();
                    break;
                    case "L":
                        Console.Clear();
                    break;
                    case "X":

                    break;
                    default:

                    break;
                }
            }while(choosedOption.ToUpper() != "X");

        }

        private static void Transfer()
        {
            Console.WriteLine("\t\t\t\tInstruções para Transferência:");
            try
            {
                ListAccountIndex();
                Console.Write("\nDigite o número da conta de origem: ");
                var originAccountIndex = byte.Parse(Console.ReadLine());
                if((originAccountIndex > accountList.Count) || (originAccountIndex < 1))
                {
                    throw new Exception("Conta inválida.");
                }
                ListTransferAccount(originAccountIndex);
                Console.Write("\nDigite o número da conta de destino: ");
                var destinyAccountIndex = int.Parse(Console.ReadLine());
                Console.Write("\nDigite o valor para transferência:");
                if((destinyAccountIndex > accountList.Count) ||
                 (destinyAccountIndex < 1)||
                 (destinyAccountIndex == originAccountIndex))
                {
                    throw new Exception("Conta inválida.");
                }
                var transferValue = double.Parse(Console.ReadLine());
                if (transferValue < 0)
                {
                    throw new Exception("Valor inválido.");
                }
                accountList[--originAccountIndex].Transfer(transferValue, accountList[--destinyAccountIndex]);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ListTransferAccount(int originIndex)
        {
            for(int i = 1; i <= (accountList.Count); i++)
            {
                if(i != originIndex)
                {
                    Console.WriteLine($"\n\t#{i} - Titular: {accountList[i-1].getHolderName()}");
                }
            }
        }

        private static void Deposit()
        {
            Console.WriteLine("\t\t\t\tInstruções para Depósito:");
            try
            {
                ListAccountIndex();
                Console.Write("\nDigite o número da conta: ");
                var accountIndex = int.Parse(Console.ReadLine());
                Console.Write("\nDigite o valor para depósito: ");
                var depositValue = double.Parse(Console.ReadLine());
                if (depositValue < 0)
                {
                    throw new Exception("Valor inválido.");
                }
                accountList[--accountIndex].Deposit(depositValue);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Withdraw()
        {
            Console.WriteLine("\t\t\t\tInstruções para Saque:");
            try
            {
                ListAccountIndex();
                Console.Write("\nDigite o número da conta: ");
                var accountIndex = int.Parse(Console.ReadLine());
                Console.Write("\nDigite o valor a ser sacado: ");
                var withdrawValue = double.Parse(Console.ReadLine());
                if (withdrawValue < 0)
                {
                    throw new Exception("Valor inválido.");
                }
                accountList[--accountIndex].Withdraw(withdrawValue);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ListAccounts()
        {
            if(accountList.Count != 0)
            {
                int accountNumber = 1;
                Console.WriteLine("\t\t\t\tLista de contas");
                foreach(var account in accountList)
                {
                    Console.WriteLine($"\n{accountNumber++}# {account}");
                }
            }else
            {
                Console.WriteLine("Não existem contas cadastradas no sistema.");
            }
                
        }

        private static void ListAccountIndex()
        {
            if(accountList.Count == 0)
            {
                throw new Exception("Não existem contas neste sistema.");
            }else
            {
                int index = 1;
                foreach (var item in accountList)
                {
                    Console.WriteLine($"\n\t#{index++} - Titular: {item.getHolderName()}");
                }
            }
        }

        private static void CreateNewAccount()
        {
            byte accountType = 0;
            string holderName = "";
            double balance = 0.0;
            double credit = 0.0;
            Console.WriteLine("\n\t\t\t\tNova Conta");
            Console.WriteLine("\nDigite 1 para Conta Física ou 0 para Conta Jurídica ");
            try
            {
                accountType = byte.Parse(Console.ReadLine());
                if(accountType >= 2)
                    throw new Exception("Tipo de conta inválido");
            }catch(Exception)
            {
                throw new Exception("Tipo de conta inválido");
            }

            Console.WriteLine("\nDigite o nome do titular da conta:");
            holderName = Console.ReadLine();
            if (String.IsNullOrEmpty(holderName)||(int.TryParse(holderName, out int x)))
            {
                throw new ArgumentException("Nome inválido");
            }

            Console.WriteLine("\nDigite o saldo inicial:");
            try
            {
                balance = double.Parse(Console.ReadLine());
                if(balance < 0)
                    throw new Exception("Valor de saldo inválido!");
            }catch(Exception)
            {
                throw new Exception("Valor de saldo inválido!");
            }

            Console.WriteLine("\nDigite o crédito:");
            try
            {
                credit = double.Parse(Console.ReadLine());
                if(credit < 0)
                    throw new Exception("Valor de crédito inválido!");
            }catch(Exception)
            {
                throw new Exception("Valor de crédito inválido!");
            }

            Account newAccount = new Account((AccountType)accountType, holderName, balance, credit);
            accountList.Add(newAccount);
        }

        static string getOption(){
            Console.WriteLine("\n\t\t\t\tOpções");
            Console.WriteLine("1 - Listar Contas");
            Console.WriteLine("2 - Inserir nova conta");
            Console.WriteLine("3 - Transferência");
            Console.WriteLine("4 - Sacar");
            Console.WriteLine("5 - Depositar");
            Console.WriteLine("L - Limpar tela");
            Console.WriteLine("X - Sair\n");

            return Console.ReadLine();
        }
    }
}

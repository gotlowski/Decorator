using System;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            var account = new Account("123123123123");

            account.Deposit(100);
            account.Charge(500);
            account.Deposit(1000);
            account.Charge(500);
            account.ShowBalance();

            var selverDecorator = new SilverDecorator(account);

            account.ShowBalance();
            selverDecorator.Deposit(1000);
            account.ShowBalance();

            var goldDecorator = new GoldDecorator(account);

            goldDecorator.Deposit(2000);
            account.ShowBalance();
        }
    }

    abstract class BaseAccount
    {
        public abstract decimal Balance { get; set; }
        public abstract string AccountNumber { get; set; }

        public abstract void Deposit(decimal amount);

        public abstract void Charge(decimal amount);

        public abstract void ShowBalance();
    }

    class Account : BaseAccount
    {
        public override decimal Balance { get; set; }
        public override string AccountNumber { get; set; }
        public Account(string number)
        {
            AccountNumber = number;
        }

        public override void Deposit(decimal amount)
        {
            Balance = Balance + amount;

            Console.WriteLine("On account " + AccountNumber + " payed:" + amount.ToString());
        }

        public override void Charge(decimal amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Account balance too low");
                return;
            }

            Balance = Balance - amount;

            Console.WriteLine("On account " + AccountNumber + " charged:" + amount.ToString());
        }

        public override void ShowBalance()
        {
            Console.WriteLine("On account " + AccountNumber + " Balance: " + Balance.ToString());
        }
    }

    class AccountDecorator : BaseAccount
    {
        protected BaseAccount _account;
        public AccountDecorator(BaseAccount account)
        {
            _account = account;
        }

        public override decimal Balance
        {
            get
            {
                return _account.Balance;
            }
            set
            {
                _account.Balance = value;
            }
        }

        public override string AccountNumber { get; set; }

        public override void Deposit(decimal amount)
        {
            _account.Deposit(amount);
        }
        public override void Charge(decimal amount)
        {
            _account.Charge(amount);
        }

        public override void ShowBalance()
        {
            _account.ShowBalance();
        }
    }

    class SilverDecorator : AccountDecorator
    {
        public SilverDecorator(BaseAccount account) : base(account)
        {

        }

        public override void Deposit(decimal amount)
        {
            base.Deposit(amount);

            if (amount >= 1000)
            {
                Balance = Balance + 10;
            }
        }
    }

    class UsualDecorator : AccountDecorator
    {
        public UsualDecorator(Account account) : base(account)
        {
        }
        public override void ShowBalance()
        {
            base.ShowBalance();
            Console.WriteLine("Usual account");
        }
    }

    class GoldDecorator : AccountDecorator
    {

        public GoldDecorator(Account number) : base(number)
        {

        }

        public override void Deposit(decimal amount)
        {
            base.Deposit(amount);

            if (amount >= 2000)
            {
                Balance = Balance + (amount * 1.1m);
            }
        }
    }
}

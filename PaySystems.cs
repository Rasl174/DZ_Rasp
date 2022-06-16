using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CS_DZ_Rasp_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = new Order(2, 2000);
            IPaymentSystem system1 = new PaySystem1(order.Id, order.Amount);
            IPaymentSystem system2 = new PaySystem2(order.Id, order.Amount);
            IPaymentSystem system3 = new PaySystem3(order.Id, order.Amount);
            system1.GetPayingLink(order);
            system2.GetPayingLink(order);
            system3.GetPayingLink(order);
        }
    }

    public class Order
    {
        public readonly int Id;
        public readonly int Amount;

        public Order(int id, int amount) => (Id, Amount) = (id, amount);
    }

    interface IPaymentSystem
    {
        string GetPayingLink(Order order);
    }

    public class PaySystem1 : Order, IPaymentSystem
    {
        public PaySystem1(int id, int amount) : base(id, amount)
        {
        }

        public string GetPayingLink(Order order)
        {
            var md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(order.Id.ToString());
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder stringBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("X2"));
            }
            Console.WriteLine($"pay.system1.ru/order?amount={order.Amount}RUB&hash={stringBuilder}");
            return stringBuilder.ToString();
        }
    }

    public class PaySystem2 : Order, IPaymentSystem
    {
        public PaySystem2(int id, int amount) : base(id, amount)
        {
        }

        public string GetPayingLink(Order order)
        {
            var md5 = MD5.Create();

            byte[] idBytes = System.Text.Encoding.ASCII.GetBytes(order.Id.ToString());
            byte[] amountBytes = System.Text.Encoding.ASCII.GetBytes(order.Amount.ToString());
            byte[] inputBytes = idBytes.Concat(amountBytes).ToArray();
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder stringBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("X2"));
            }
            Console.WriteLine($"order.system2.ru/pay?hash={stringBuilder}");
            return stringBuilder.ToString();
        }
    }

    public class PaySystem3 : Order, IPaymentSystem
    {
        public PaySystem3(int id, int amount) : base(id, amount)
        {
        }

        public string GetPayingLink(Order order)
        {
            var sha1 = SHA1.Create();
            var des = DES.Create();

            byte[] amountBytes = System.Text.Encoding.ASCII.GetBytes(order.Amount.ToString());
            byte[] idBytes = System.Text.Encoding.ASCII.GetBytes(order.Id.ToString());
            byte[] secretKey = System.Text.Encoding.ASCII.GetBytes(des.Key.ToString());
            byte[] inputBytes = amountBytes.Concat(idBytes).Concat(secretKey).ToArray();
            byte[] hashBytes = sha1.ComputeHash(inputBytes);

            StringBuilder stringBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("X2"));
            }
            Console.WriteLine($"system3.com/pay?amount={order.Amount}&curency=RUB&hash={stringBuilder}");
            return stringBuilder.ToString();
        }
    }
}
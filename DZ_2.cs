using System;
using System.Collections.Generic;
using System.Linq;

namespace CS_DZ_Rasp_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            warehouse.ShowAllGoods();

            Cart cart = shop.CreateCart();
            cart.AddToCart(iPhone12, 4);
            cart.AddToCart(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

            cart.ShowGoodsInCart();

            //Console.WriteLine(cart.Order().Paylink);

            cart.AddToCart(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары
        }
    }

    class Good
    {
        private readonly string _name;

        public string Name => _name;

        public Good(string name)
        {
            _name = name;
        }
    }

    class Warehouse
    {
        private Dictionary<Good, int> _stock = new Dictionary<Good, int>();

        public IReadOnlyDictionary<Good, int> Stock => _stock;

        public void Delive(Good good, int count)
        {
            if(count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            else
            {
                _stock.Add(good, count);
            }
        }

        public void ShowAllGoods()
        {
            Console.WriteLine("На складе есть:");
            foreach (var good in _stock)
            {
                Console.WriteLine("Товар с наименованием - " + good.Key.Name + " в количестве - " + good.Value);
            }
            Console.WriteLine();
        }

        public void GetGoodFromStock(Good good, int count)
        {
            if(_stock[good] - count == 0)
            {
                _stock.Remove(good);
            }
            else
            {
                _stock[good] = _stock[good] - count;
            }
        }
    }

    class Shop
    {
        private Warehouse _warehouse;

        public Shop(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }
        public Cart CreateCart() => new Cart(this);

        public void RemoveGood(Good good, int count)
        {
            _warehouse.GetGoodFromStock(good, count);
            Console.WriteLine("Покупатель добавил в корзину товар - " + good.Name + " в количестве: " + count);
        }

        public bool ChechCountInStock(Good good, int count)
        {
            if(_warehouse.Stock[good] < count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Cart
    {
        private Dictionary<Good, int> _cart = new Dictionary<Good, int>();
        private Shop _shop;

        public Cart(Shop shop)
        {
            _shop = shop;
        }

        public void AddToCart(Good good, int count)
        {
            if(_cart.ContainsKey(good))
            {
                throw new Exception("В корзину пытаются добавить уже отправленные со склада товары - " + good.Name);
            }
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if (_shop.ChechCountInStock(good, count))
            {
                throw new Exception("На складе нет столько товара!");
            }
            else
            {
                _shop.RemoveGood(good, count);
                _cart.Add(good, count);
            }
        }

        public void ShowGoodsInCart()
        {
            Console.WriteLine();
            Console.WriteLine("У покупателя в корзине: ");
            foreach (var good in _cart)
            {
                Console.WriteLine(good.Key.Name + " в количестве: " + good.Value);
            }
        }
    }
}

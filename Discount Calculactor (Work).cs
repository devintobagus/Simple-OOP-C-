using System;

namespace ShopeeFood
{
    public abstract class Shopee
    {
        public abstract void Calculate();
        private double total_food_price; // Total Harga makanan
        private double delivery_cost; // Harga pengiriman per kilometer
        public double distance; 
        public double adm_fee_customer = 4000;
        public double adm_fee_saler = 0.2;
        public double saler_food;
        public double discount;
        public double max_discount; // nominal yang diberikan diskon
        public double min_discount; // nominal minimal untuk menggunakan promo
        public double delivery_discount = 10000;
        public double FoodPrice
        {
            get { return total_food_price; }
            set { total_food_price = value; }
        }
        public double DeliveryCost
        {
            get { return delivery_cost; }
            set { delivery_cost = value; }
        }
    }
    class Customer : Shopee
    {
        public double TotalOrder()
        {
            if (FoodPrice >= min_discount)
            {
                if (FoodPrice * discount <= max_discount)
                {
                    double total_order = FoodPrice * (1 - discount) + adm_fee_customer + DeliveryCost * distance - delivery_discount;
                    return total_order;
                }
                else
                {
                    double total_order = FoodPrice - max_discount + adm_fee_customer + DeliveryCost * distance - delivery_discount;
                    return total_order;
                }
            }
            else
            {
                double total_order = FoodPrice + adm_fee_customer + DeliveryCost * distance - delivery_discount;
                return total_order;
            }
        }
        public double[] SplitBill(int person, double total_order)
        {
            double[] person_arr = new double[person];
            for (int i = 0; i < person_arr.Length; i++)
            {
                Console.WriteLine("Person " + (i+1));
                person_arr[i] = double.Parse(Console.ReadLine());
            }
            double adm_after = adm_fee_customer + DeliveryCost * distance - delivery_discount;
            double sum = person_arr.Sum();
            double[] person_con = new double[person];
            for (int i = 0; i < person_con.Length; i++)
            {
                person_con[i] = (person_arr[i] / (sum)) * (total_order - adm_after) + (adm_after / person_arr.Length);
            }
            return person_con;
        }
        public override void Calculate()
        {
            this.TotalOrder();
            Console.WriteLine(TotalOrder());
        }
        public void Calculate(int person)
        {
            double[] person_ = SplitBill(person, TotalOrder());
            for (int i = 0; i < person_.Length; i++)
            {
                Console.Write("Person " + (i+1) + " must pay : " + Convert.ToInt32(person_[i]) + "\n");
            }

        }
    }
    class Saler : Shopee
    {
        public override void Calculate()
        {
            double saler_price = saler_food / (1-adm_fee_saler);
            Console.WriteLine("Price on Shopee : " + saler_price);
        }
    }
    class Program
    {
       
        public static void Main()
        {
            Console.WriteLine("Who are you? (1) Customer (2) Saler?");
            int input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                Customer customer = new Customer();
                customer.max_discount = 25000;
                customer.min_discount = 40000;
                customer.FoodPrice = 45000;
                customer.discount = 0.6;
                customer.DeliveryCost = 5000;
                customer.delivery_discount = 10000;
                customer.distance = 1.4;
                customer.Calculate(4);
            }
            else if(input == 2)
            {
                Saler saler = new Saler();
                saler.saler_food = 13000;
                saler.Calculate();
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }

}

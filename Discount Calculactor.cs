using System;

namespace ShopeeFood
{
    public abstract class Shopee 
    {
        public abstract void Calculate();
        private double total_food_price;
        private double delivery_cost=5000;
        public double distance;
        public double adm_fee_customer = 4000;
        public double adm_fee_saler = 0.2;
        public double saler_food;
        public double discount = 0.6;
        public double max_discount=25000; // nominal yang diberikan diskon
        public double min_discount=40000; // nominal minimal untuk menggunakan promo
        public double delivery_discount=10000;
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
            {   if (FoodPrice*discount <= max_discount)
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
        public double[] SplitBill(int person,double total_order)
        {
            double[] person_arr = new double[person];
            for (int i = 0; i < person_arr.Length; i++)
            {
                person_arr[i] = double.Parse(Console.ReadLine());
            }
            double adm_after = adm_fee_customer+DeliveryCost * distance - delivery_discount;
            double sum = person_arr.Sum();
            double[] person_con = new double[person];
            for (int i = 0; i < person_con.Length; i++)
            {
                person_con[i] = (person_arr[i] / (sum))*(total_order-adm_after)+(adm_after/person_arr.Length);
            }
            return person_con;
        }
        public override void Calculate()
        {
            Console.WriteLine("Total that you should pay "+ TotalOrder());
        }
        public void Calculate(int person)
        {
            double[] person_ =  SplitBill(person,TotalOrder());
            for (int i= 0; i < person_.Length;i++)
            {
                Console.Write("Person " + i + " must pay : " + Convert.ToInt32(person_[i])+"\n");
            }

        }
   }
   class Saler : Shopee
    {   
        public override void Calculate()
        {
            double saler_price = saler_food / adm_fee_saler;
            Console.WriteLine("Price on Shopee : " + saler_price);
        }
    }
    class Program
    {   
        public void Cust()
        {   Customer customer = new Customer();

            Console.WriteLine("Total food price : \n");
            customer.FoodPrice = double.Parse(Console.ReadLine());
            Console.WriteLine("Distance (km) : \n");
            customer.distance = double.Parse(Console.ReadLine());
            Console.WriteLine("Are you splitting bill with someone? if yes, tell us how much, but if no, please type [0]");
            int num_person = int.Parse(Console.ReadLine());
            if (num_person == 0)
            {
                customer.Calculate();
            }
            else
            {
                customer.Calculate(num_person);
            }
        }
        public void Salr()
        {
            Saler saler = new Saler();
            Console.WriteLine("Food Price : \n");
            saler.saler_food = double.Parse(Console.ReadLine());
            saler.Calculate();

        }
        public static void Main()
        {
            Console.WriteLine("\n\nWho are you? Press (1) for Customer and (2) for Saler\n");
            int id = int.Parse(Console.ReadLine());
            Program program = new Program();
            if (id == 1)
            {
                
                program.Cust();
            }
            else if(id == 2)
            {
                program.Salr();
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
            
        }
    }

}

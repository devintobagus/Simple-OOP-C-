using System;

namespace Application
{
	//Aplikasi untuk menghitung biaya atau revenue yang didapat jika menjadi salah satu bagian dari suatu OnlineShop
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("\n\nWho are you?(1) Customer (2) Saler (3) Shipping"); 
            int input = Convert.ToInt32(Console.ReadLine()); // Untuk mengidentifikasi pengguna
            if (input == 1)
            {   Customer customer = new Customer();
                Console.WriteLine("Name : ");
                string cust_name = Console.ReadLine();
                Console.WriteLine("Current Money : ");
                double curr_money = double.Parse(Console.ReadLine());
                Console.WriteLine("Total Order : ");
                double order_ = double.Parse(Console.ReadLine());
                customer.customer_name = cust_name;
                customer.CurrentMoney = curr_money;
                customer.TotalOrder = order_;
                customer.Calculate();

            }
            else if(input == 2)
            {
                Saler saler = new Saler();
                Console.WriteLine("Name : ");
                string saler_name = Console.ReadLine();
                Console.WriteLine("Total order : ");
                double order_total = double.Parse(Console.ReadLine());
                Console.WriteLine("Total Revenue: ");
                double total_revenue = double.Parse(Console.ReadLine());
                saler.salers_name = saler_name;
                saler.TotalOrder = order_total;
                saler.SalersRevenue = total_revenue;
                saler.Calculate();

            }
            else if(input == 3)
            {
                Shipping shipment = new Shipping(); 
                Console.WriteLine("Name : ");
                string shipping_name = Console.ReadLine();
                Console.WriteLine("Total shipping : ");
                double shipping_total = double.Parse(Console.ReadLine());
                shipment.shipping_name = shipping_name; 
                shipment.shipping_cost = shipping_total;
                shipment.Calculate();

            }
            else // Error jika menginput diluar index
            {
                throw new IndexOutOfRangeException();
            }

        }
    }

    abstract class OnlineShop // Abstract Class
    {
        public string customer_name; // nama customer
        private double last_customer_money; //nominal uang diakun customer (USD)

        public string salers_name; // nama penjual
        private double salers_revenue; // keuntungan penjual(USD)
        private double total_order; // harga barang (USD)

        public string shipping_name; // nama jasa pengiriman
        public double shipping_cost; // biaya pengiriman 
        public double adm = 0.2; //persentase administrasi fee
        public abstract void Calculate(); //abstract untuk kalkulasi
		
        //Getter and Setter
        public double CurrentMoney 
        {
            get { return last_customer_money; }
            set { last_customer_money = value; }
        }
        public double SalersRevenue 
        {

            get { return salers_revenue; }
            set { salers_revenue = value; }

        }
        public double TotalOrder
        {
            get { return total_order; }
            set { total_order= value; }
        }
    }
    class Customer : OnlineShop // turunan kelas
    {
        public override void Calculate() //Menghitung total biaya yang harus dibayar customer
        {

            double payment_after = TotalOrder * (1+adm) + shipping_cost;
            Console.WriteLine(customer_name + ", Total payment : " + payment_after + ". There is " + (CurrentMoney-payment_after)+" left on your account.");
        }

    }
    class Saler : OnlineShop // turunan kelas
    {
        public override void Calculate() // menghitung revenue yang didapat oleh saler
        {
            double revenue_after = (TotalOrder+SalersRevenue) * (1 - adm);
            Console.WriteLine(salers_name + ", Total order revenue after administraion : " + revenue_after);
        }

    }
    class Shipping : OnlineShop // turunan kelas
    {
        public override void Calculate() // menghitung revenue yang didapat oleh jasa pengiriman
        {
            double shipping_revenueafter = shipping_cost * (1 - adm);
            Console.WriteLine(shipping_name + ", Total shipping revenue after administration : " + shipping_revenueafter);
        }
    }
}


        
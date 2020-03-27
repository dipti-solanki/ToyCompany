using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompany
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome!");
            Console.WriteLine("1. Create your account");
            Console.WriteLine("2. Alreday have an account? Sign In.");
            Console.WriteLine("Enter your choice");
            var db = new ToyCompanyDbEntities();
            int choice;
            choice = Convert.ToInt32(Console.ReadLine());
            switch(choice)
            {
                case 1:Add();
                    Console.WriteLine("Now Please Sign in to Use Your Account");
                    break;
                case 2:login();
                   
                    break;
                default:break;
            }
             void Add()
            {
                using (db)
                {
                    var user = new User();
                    user.getDetails();
                    var check = db.Users.SingleOrDefault(t => t.EmailAddress == user.EmailAddress);
                    if (check == null)
                    {
                        try
                        {
                            db.Users.Add(user);
                            db.SaveChanges();
                            Console.WriteLine("Your Account is successfully created");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                    }
                    else
                    {
                        Console.WriteLine("Your account already exists");
                    }
                }
            }

             void login()
            {
                using (db)
                {
                    var user = new User();
                    Console.WriteLine("Enter your email address");
                    user.EmailAddress = Console.ReadLine();
                    var check = db.Users.SingleOrDefault(t => t.EmailAddress == user.EmailAddress);
                    
                    
                    if (check != null)
                    {
                        Console.WriteLine("You have successfully logged in!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Credentials");
                    }
                   
                    var userId = check.UserId;
                    AddOrder(userId);
                   
                }
               
            }
            void AddOrder(int userId)
            {
                using(db)
                {
                    var order = new Order();
                    var toy = new Toy();
                    toy.ToyList();
                    Console.WriteLine("Choose the options from the toys list");
                    order.UserId = userId;
                    ShowAddresses(userId);
                    Console.WriteLine("Enter the id of the address for which you want delivery");
                    var addressId = Convert.ToInt32(Console.ReadLine());
                    order.AddressId = addressId;
                    try
                    {
                        db.Orders.Add(order);
                        db.SaveChanges();
                        var orders= db.Orders.Where(t => t.UserId == userId).FirstOrDefault();
                        var orderId=orders.OrderId;
                        
                        Console.WriteLine("Enter toy Id which you want to buy");
                        var toyId = Convert.ToInt32(Console.ReadLine());
                        AddOrderDetail(orderId, toyId);
                        
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
            }
            void AddOrderDetail(int orderId,int toyId)
            {
               using(db)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderId = orderId;
                    orderDetail.ToyId = toyId;
                    try
                    {
                        db.OrderDetails.Add(orderDetail);
                        db.SaveChanges();
                        Console.WriteLine("Toy Added Successfully");
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
            }
            void ShowAddresses(int userId)
            {
                var Address = new Address();
                var dbcontext = new ToyCompanyDbEntities();
                var addressList = dbcontext.Addresses.Where(t => t.UserId == userId);
                foreach(var address in addressList)
                {
                    Console.WriteLine("Address Id"+address.AddressId+"Locality" + address.Locality + "City:" + address.City + "Pincode:" + address.Pincode);
                }
                

            }

         
           
        }
    }
}

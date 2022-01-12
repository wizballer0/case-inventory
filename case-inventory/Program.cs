using System;
using BusinessLayer;
using case_inventory;

namespace case_inventory
{
    class Program
    {
        public static void Main(string[] args)
        {
            IInventory inventory = new Inventory();
            InputHandler inputHandler = new InputHandler();

            Console.WriteLine
                ("Welcome to this simluated inventory application. " +
                "Use commands IX(to increase inventory with X), " +
                "SX(to decrease inventory with X) and L to show inventory status.");

            Console.WriteLine("Enter action:");

            while (true)
            {
                (char action, int quantity) = inputHandler.ParseInput(Console.ReadLine());

                string message = inputHandler.HandleInventory(inventory, action, quantity);
                Console.WriteLine(message);
            }
        }
    }
}



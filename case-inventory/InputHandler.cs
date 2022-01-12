using System;
using BusinessLayer;
namespace case_inventory
{
	public class InputHandler
	{
        const char DECREASE = 'S';
        const char INCREASE = 'I';
        const char GET = 'L';
        const char UNKNOWN_ACTION = 'X';
        const int NO_ARGUMENT = -1;

        public InputHandler()
		{
		}

        public (char, int) ParseInput(string input)
        {
            input = input.Trim();
            // Read first non white-space character as action if there is one
            char action = input.Length > 0 ? input.ToCharArray().ElementAt(0) : UNKNOWN_ACTION;

            switch (action)
            {
                case DECREASE:
                case INCREASE:
                    try
                    {
                        int quantity = Int32.Parse(input.Substring(1));
                        return (action, quantity);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return (UNKNOWN_ACTION, NO_ARGUMENT);
                    }

                case GET:
                    return (action, NO_ARGUMENT);

                default:
                    return (UNKNOWN_ACTION, NO_ARGUMENT);
            }
        }

        public string HandleInventory(IInventory inventory, char action, int quantity)
        {

            switch(action)
            {
                case DECREASE:
                    try
                    {
                        inventory.Decrease(quantity);
                        return ("Sold " + quantity + " items from inventory.");
                    }
                    catch (Exception e)
                    {
                        return (e.Message);
                    }
                case INCREASE:
                    try
                    {
                        inventory.Increase(quantity);
                        return ("Added " + quantity + " items to inventory.");
                    }
                    catch (Exception e)
                    {
                        return (e.Message);
                    }
                case GET:
                    return ("There are " + inventory.Get() + " items in the inventory.");
                default:
                    return ("Unknown action.");
            }
        }
    }
}


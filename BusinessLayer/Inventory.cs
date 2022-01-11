using System;
namespace BusinessLayer
{
    public class Inventory : IInventory
    {
        private int numberOfItems;

        public Inventory()
        {
            numberOfItems = 0;
        }

        public void Increase(int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException("quantity");
            }

            if((Int32.MaxValue - quantity) < numberOfItems)
            {
                throw new ArgumentOutOfRangeException("quantity");
            }

            numberOfItems += quantity;
        }

        public void Decrease(int quantity)
        {
            if (quantity > numberOfItems)
            {
                throw new ArgumentOutOfRangeException("quantity");
            }

            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException("quantity");
            }

            numberOfItems -= quantity;
        }

        public int Get()
        {
            return numberOfItems;
        }
    }
}


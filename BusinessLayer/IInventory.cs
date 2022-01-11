using System;
namespace BusinessLayer
{
	public interface IInventory
	{
		void Increase(int quantity);
		void Decrease(int quantity);
        int Get();
	}
}


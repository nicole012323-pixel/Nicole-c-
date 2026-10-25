namespace PrintingShopBusinessLogic
{
    public class PrintingShopLogic
    {
        public bool IsCustomerExisting(List<string> customers, string name)
        {
            int index = 0;

            while (index < customers.Count)
            {
                if (customers[index] == name)
                {
                    return true;
                }
                index += 1;
            }

            return false;
        }

        public int ComputePrice(string paperSize, int copies)
        {
            int pricePerPage = 0;

            if (paperSize == "Short")
            {
                pricePerPage = 2;
            }
            else if (paperSize == "Long/legal")
            {
                pricePerPage = 3;
            }
            else if (paperSize == "A4")
            {
                pricePerPage = 2;
            }

            return pricePerPage * copies;
        }

        public bool IsOrderNumberValid(int totalOrders, int orderNumber)
        {
            if (orderNumber >= 1 && orderNumber <= totalOrders)
            {
                return true;
            }

            return false;
        }
    }
}

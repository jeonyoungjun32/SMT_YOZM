namespace MobileViewer.View
{
	internal class SalesData
	{
		private string billToName;
		private string Qty;
		private string TotalPrice;

		public SalesData(string totalPrice)
		{
			TotalPrice = totalPrice;
		}

		public string BillToName { get => billToName; set => billToName = value; }
	}
}
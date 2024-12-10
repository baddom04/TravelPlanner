namespace TravelPlanner.Persistor
{
    public class Item(string name, bool isChecked, int quantity)
    {
        public string Name { get; set; } = name;
        public bool IsChecked { get; set; } = isChecked;
        public int Quantity { get; set; } = quantity;
    }
}

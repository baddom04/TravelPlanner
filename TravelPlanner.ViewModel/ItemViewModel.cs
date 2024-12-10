namespace TravelPlanner.ViewModel
{
    public class ItemViewModel
    {
        public int Count { get; set; } = 1;

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnNameChanged();
            }
        }
        public event EventHandler? NameChanged;
        private void OnNameChanged()
        {
            NameChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

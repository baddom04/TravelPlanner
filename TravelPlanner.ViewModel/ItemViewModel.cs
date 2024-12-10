using TravelPlanner.Persistor;

namespace TravelPlanner.ViewModel
{
    public class ItemViewModel
    {
        private readonly Item _item;
        public string Quantity { get; set; } = "1";

        private string _name = string.Empty;

        private bool can = false;
        public string Name
        {
            get { return _name; }
            set
            {
                if (!can)
                {
                    can = true;
                    return;
                }
                _name = value;
                OnNameChanged();
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set 
            { 
                _isChecked = value;
                SetItemValue(value);
            }
        }


        public event EventHandler? NameChanged;
       
        public ItemViewModel()
        {
            _item = new(string.Empty, false, 1);
            Quantity = "1";
            _name = string.Empty;
        }
        public ItemViewModel(Item item)
        {
            _item = item;
            Quantity = item.Quantity.ToString();
            _name = item.Name;
            IsChecked = item.IsChecked;
        }
        private void SetItemValue(bool value)
        {
            _item.IsChecked = value;
        }
        private void OnNameChanged()
        {
            NameChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

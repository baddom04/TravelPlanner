using System.Windows.Input;

namespace TravelPlanner.ViewModel
{
    public class DelegateCommand(Action<object?> execute, Predicate<object?>? canExecute) : ICommand
    {
        #region Private Fields

        private readonly Predicate<object?>? _canExecute = canExecute;
        private readonly Action<object?> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

        #endregion

        #region Events

        public event EventHandler? CanExecuteChanged;

        #endregion

        #region Constructors

        public DelegateCommand(Action<object?> execute) : this(execute, null) { }

        #endregion

        #region Public Methods

        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object? parameter) => _execute(parameter);
        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        #endregion
    }
}

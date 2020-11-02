using System;

namespace DataBinding
{
    public class Observable<T>
    {
        public T Value 
        {
            get { return _currentValue; }
            set { SetValue(value); }
        }

        private event Action<T> _onValueChanged;
        private T _currentValue;

        public void Subscribe(Action<T> onChangedHandler)
        {
            if (onChangedHandler != null)
            {
                _onValueChanged += onChangedHandler;
                onChangedHandler(_currentValue);
            }
        }

        public void SetValue(T value)
        {
            _currentValue = value;
            _onValueChanged?.Invoke(value);
        }

        public void ClearSubscribtions()
        {
            _onValueChanged = null;
        }
    }
}
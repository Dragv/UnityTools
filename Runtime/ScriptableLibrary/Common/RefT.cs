namespace Vocario.Scriptable
{
    public class RefT<T> : UnityEngine.ScriptableObject
    {
        public event System.Action<T> OnValueChange = null;
        [UnityEngine.SerializeField]
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChange?.Invoke(value);
            }
        }
    }
}

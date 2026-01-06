using Prism.Mvvm;

namespace SDTaskRunner.Models
{
    public class RequestField<T> : BindableBase
    {
        private bool isDefined;
        private T value;

        private RequestField(bool isDefined, T value)
        {
            this.isDefined = isDefined;
            this.value = value;
        }

        public bool IsDefined
        {
            get => isDefined;
            set => SetProperty(ref isDefined, value);
        }

        public T Value
        {
            get => value;
            set => SetProperty(ref this.value, value);
        }

        public static RequestField<T> Undefined()
            => new(false, default);

        public static RequestField<T> Defined(T value)
            => new(true, value);
    }
}
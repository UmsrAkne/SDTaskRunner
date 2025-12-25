namespace SDTaskRunner.Models
{
    public sealed class RequestField<T>
    {
        private RequestField(bool isDefined, T value)
        {
            IsDefined = isDefined;
            Value = value;
        }

        public bool IsDefined { get; }

        public T Value { get; }

        public static RequestField<T> Undefined() => new(false, default);

        public static RequestField<T> Defined(T value) => new(true, value);
    }
}
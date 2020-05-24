namespace Puffix.IoC
{
    public class IoCNamedParameter
    {
        public string Name { get; }

        public object Value { get; }

        private IoCNamedParameter(string name, object @value)
        {
            Name = name;
            Value = @value;
        }

        public static IoCNamedParameter CreateNew(string name, object @value)
        {
            return new IoCNamedParameter(name, @value);
        }
    }
}

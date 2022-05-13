namespace Puffix.IoC
{
    public class IoCNamedParameter
    {
        public string Name { get; } = string.Empty;

        public object Value { get; }

        public IoCNamedParameter(string name, object @value)
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

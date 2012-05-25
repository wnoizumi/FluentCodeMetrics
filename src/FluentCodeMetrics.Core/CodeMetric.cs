namespace FluentCodeMetrics.Core
{
    public abstract class CodeMetric
    {
        public abstract int Value { get; }

        public static implicit operator int(CodeMetric source)
        {
            return source.Value;
        }
    }
}

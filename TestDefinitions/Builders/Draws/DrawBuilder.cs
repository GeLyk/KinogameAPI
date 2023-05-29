namespace TestDefinitions.Builders.Draws
{
    public class DrawBuilder
    {
        public Draw Build()
        {
            var draw = Draw.Create();
            return draw;
        }

        public static implicit operator Draw(DrawBuilder instance)
        {
            return instance.Build();
        }
    }
}

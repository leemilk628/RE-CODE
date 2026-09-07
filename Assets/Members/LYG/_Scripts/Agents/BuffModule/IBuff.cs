namespace Members.LYG._Scripts.Agents.BuffModule
{
        public interface IBuff
        {
                public BuffType Type { get; }
                public string Name { get; }
                public float Value { get; }
        }
}
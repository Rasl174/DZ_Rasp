namespace Clean_code_11
{
    class Program
    {
        public void Enable()
        {
            _effects.StartEnableAnimation();
        }

        public void Disable()
        {
            _pool.Free(this);
        }
    }
}

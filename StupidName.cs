namespace CS_DZ_Rasp_5
{
    class Program
    {
        public static int FindCorrectNumber(int a, int b, int c)
        {
            if (a < b)
                return b;
            else if (a > c)
                return c;
            else
                return a;
        }
    }
}

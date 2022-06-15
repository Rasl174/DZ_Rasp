namespace Clean_code_7
{
    class Program
    {
        public static void CreateObject()
        {
            //Создание объекта на карте
        }

        public static void GenerateChance()
        {
            _chance = Random.Range(0, 100);
        }

        public static int SetSalary(int hoursWorked)
        {
            return _hourlyRate * hoursWorked;
        }
    }
}

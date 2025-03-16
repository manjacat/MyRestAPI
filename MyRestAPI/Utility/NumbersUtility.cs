namespace MyRestAPI.Utility
{
    public static class NumbersUtility
    {
        public static bool IsPrimeNumber(long number)
        {
            bool prime = true;
            for (long i = 2; i < Math.Sqrt(number); i++) 
            {
                if (number % i == 0) {  prime = false; break; }
            }
            return prime;
        }
    }
}

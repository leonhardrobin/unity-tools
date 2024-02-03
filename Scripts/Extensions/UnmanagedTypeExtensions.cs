using System;

namespace LRS.Utils
{
    public static class UnmanagedTypeExtensions
    {
        private class U
        {
        }

        public static bool IsUnManaged(this Type t)
        {
            try
            {
                Type makeGenericType = typeof(U).MakeGenericType(t);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
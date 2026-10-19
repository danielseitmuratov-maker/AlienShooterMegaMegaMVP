
namespace _Project.CodeBase.Runtime.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNotNull(this object obj)
        {
            if (obj != null)
                return true;
            return false;
        }

        public static bool IsNull(this object obj)
        {
            if (obj == null)
                return true;
            return false;
        }
    }
}
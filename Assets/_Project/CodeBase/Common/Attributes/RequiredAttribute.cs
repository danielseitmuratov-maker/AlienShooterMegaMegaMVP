using System;

namespace _Project.CodeBase.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class RequiredAttribute : Attribute
    {
        public string Message { get; }

        public RequiredAttribute(string message = null)
        {
            Message = message;
        }
    }
}
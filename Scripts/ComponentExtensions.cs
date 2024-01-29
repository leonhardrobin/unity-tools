using UnityEngine;

namespace LRS
{
    public static class ComponentExtensions
    {
        public static T OrNull<T>(this T component) where T : Component
        {
            return component == null ? null : component;
        }
    }
}
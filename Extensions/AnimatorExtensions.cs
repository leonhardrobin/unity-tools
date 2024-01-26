using System.Linq;
using UnityEngine;

namespace LRS
{
    public static class AnimatorExtensions
    {
        /// <summary>
        /// Checks if an animator has a given parameter
        /// </summary>
        /// <param name="animator">The animator to check</param>
        /// <param name="parameterName">The parameter to check for</param>
        /// <returns>true or false whether the parameter has been found</returns>
        public static bool HasParameter(this Animator animator, string parameterName)
        {
            return animator.parameters.Any(parameter => parameter.name == parameterName);
        }
    }
}
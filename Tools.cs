using UnityEngine;
using UnityEngine.Pool;
using System.Threading.Tasks;
using System;
using System.Linq;

public static class Tools
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    public static float RangeTo01(float value, float min, float max) => (value - min) / (max - min);
    
    //https://stackoverflow.com/questions/929103/convert-a-number-range-to-another-range-maintaining-ratio
    /// <summary>
    /// Remaps a value from one range to another
    /// </summary>
    /// <param name="value">The value to be remapped</param>
    /// <param name="from1">Range start of the old value</param>
    /// <param name="to1">Range end of the old value</param>
    /// <param name="from2">New range start</param>
    /// <param name="to2">New range end</param>
    public static float RemapRange(float value, float from1, float to1, float from2, float to2) => (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    
    /// <summary>
    /// Waits for a given time and then releases the element back into the pool
    /// </summary>
    /// <param name="pool">The Unity ObjectPool</param>
    /// <param name="element">The element to release</param>
    /// <param name="time">Time to release after</param>
    /// <typeparam name="T">Type of Pool</typeparam>
    public static async void WaitForRelease<T>(IObjectPool<T> pool, T element, float time) where T : class
    {
        await Task.Delay(TimeSpan.FromSeconds(time));
        pool.Release(element);
    }
    
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

    public static class PhysicCalculations
    {
        /// <summary>
        /// Calculates the velocity needed to hit a target with a given angle in 3D space
        /// </summary>
        /// <param name="alpha">The desired angle towards the target</param>
        /// <param name="startPos">The starting position</param>
        /// <param name="destPos">The destination position</param>
        /// <returns>The velocity as a Vector3</returns>
        public static Vector3 VelocityTowardsTargetWithAngle(float alpha, Vector3 startPos, Vector3 destPos)
        {
            // calculate Force to apply
            float gravity = Physics.gravity.magnitude;
            float angle = alpha * Mathf.Deg2Rad;

            Vector3 planarTarget = new(destPos.x, 0, destPos.z);

            Vector3 planarPosition = new(startPos.x, 0, startPos.z);

            float distance = Vector3.Distance(planarTarget, planarPosition);
            float yOffset = startPos.y - destPos.y;

            float initialVelocity = (1 / Mathf.Cos(angle)) *
                                    Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) /
                                               (distance * Mathf.Tan(angle) + yOffset));

            Vector3 velocity = new(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

            float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) *
                                        (planarTarget.x > planarPosition.x ? 1 : -1);

            Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

            return finalVelocity;
        }
    }
    
    public static class DetectHitDirection
    {
        /// <summary>
        /// Calculates the horizontal and vertical hit direction based on the hit point and the transform's position
        /// </summary>
        /// <param name="t">transform where the hit came from</param>
        /// <param name="hitPoint">hit position of the target</param>
        /// <param name="horizontal">horizontal amount of hit direction</param>
        /// <param name="vertical">vertical amount of hit direction</param>
        /// <param name="intensity">intensity multiplier</param>
        public static void HitDirection(Transform t, Vector3 hitPoint, out float horizontal, out float vertical, float intensity = 3f)
        {
            Vector3 playerPosition = t.position;
            Vector3 playerToHitPoint = hitPoint - playerPosition;
            Vector3 playerForward = t.forward;
            Vector3 playerRight = t.right;

            float dotForward = Vector3.Dot(playerToHitPoint, playerForward);
            float dotRight = Vector3.Dot(playerToHitPoint, playerRight);

            horizontal = dotRight * -1 * intensity;
            vertical = dotForward * -1 * intensity;
        }
    }
}

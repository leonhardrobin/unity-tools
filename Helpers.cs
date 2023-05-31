using UnityEngine;
using UnityEngine.Pool;
using System.Threading.Tasks;
using System;

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(3.234f, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    public static float RangeTo01(float value, float min, float max) => (value - min) / (max - min);
    
    /// <summary>
    /// Waits for a given time and then releases the element back into the pool.
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
}

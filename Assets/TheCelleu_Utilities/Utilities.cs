using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thecelleu
{

    static class Utilities
    {
        private static System.Random randomGen = new System.Random();

        /// <summary>
        /// Damping towards zero. This function is not time boxed, there is not a fixed timespan to get to 0.
        /// </summary>
        /// <param name="source">Source value. Usualy the current Value of an object</param>
        /// <param name="smoothing">Smoothing rate dictates the proportion of source remaining after one second</param>
        /// <param name="dt">Deltatime</param>
        /// <returns></returns>
        public static float Damp(float source, float smoothing, float dt)
        {
            return source * Mathf.Pow(smoothing, dt);
        }

        /// <summary>
        /// Framerate aware Damp function. Use instead of a = Mathf.Lerp(a, b, r);.
        /// See http://www.rorydriscoll.com/2016/03/07/frame-rate-independent-damping-using-lerp/ for an extensive explanation
        /// </summary>
        /// <param name="a">Source value. Usualy the current Value of an object</param>
        /// <param name="b">Target value</param>
        /// <param name="lambda">Smoothing rate dictates the proportion of source remaining after one second</param>
        /// <param name="dt">Deltatime</param>
        /// <returns></returns>
        public static float Damp(float a, float b, float lambda, float dt)
        {
            return Mathf.Lerp(a, b, 1 - Mathf.Exp(-lambda * dt));
        }

        /// <summary>
        /// Returns a random enumeration value from all the enums of this type.
        /// </summary>
        /// <typeparam name="T">Enumeration Type</typeparam>
        /// <returns>Random value of this enumeration type</returns>
        public static T RandomEnumValue<T>()
        {
            var enumvaluesAsArray = System.Enum.GetValues(typeof(T));
            return (T)enumvaluesAsArray.GetValue(randomGen.Next(enumvaluesAsArray.Length));
        }

        // sound
        // maybe to do https://johnleonardfrench.com/articles/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best/

        /// <summary>
        /// returns true uf point is on the left side and false if point is on the right side of the plane
        /// thanks to Harald Hanche-Olsen https://math.stackexchange.com/a/214194
        /// </summary>
        /// <param name="planeA">plane point A</param>
        /// <param name="planeB">plane point B</param>
        /// <param name="planeC">plane point C</param>
        /// <param name="point">Point to compare</param>
        /// <returns></returns>
        public static bool GetSide(Vector3 planeA, Vector3 planeB, Vector3 planeC, Vector3 point)
        {
            bool result;
            Vector3 BA = planeB - planeA;
            Vector3 CA = planeC - planeA;
            Vector3 PA = point - planeA;

            float determinant3x3 = BA.x * CA.y * PA.z + BA.y * CA.z * PA.x + BA.z * CA.x * PA.y
                - BA.z * CA.y * PA.x - BA.y * CA.x * PA.z - BA.x * CA.z * PA.y;
            if (determinant3x3 > 0f)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
    }

    /// <summary>
    /// The casts to object in the below code are an unfortunate necessity due to
    /// C#'s restriction against a where T : Enum constraint. (There are ways around
    /// this, but they're outside the scope of this simple illustration.)
    /// Thanks to Dan Tao (https://stackoverflow.com/questions/3261451/using-a-bitmask-in-c-sharp)
    /// </summary>
    public static class FlagsHelper
    {
        /// <summary>
        /// Check if a specific bit is set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flags">The Bitstring you want to check</param>
        /// <param name="flag">The Bit you want to be checked</param>
        /// <returns></returns>
        public static bool IsSet<T>(T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            return (flagsValue & flagValue) != 0;
        }

        /// <summary>
        /// Set a specific bit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flags">The Bitstring you want to set the bit</param>
        /// <param name="flag">The Bit you want to be set</param>
        public static void Set<T>(ref T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            flags = (T)(object)(flagsValue | flagValue);
        }

        /// <summary>
        /// Unset a specific bit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flags">The Bitstring you want to unset the bit</param>
        /// <param name="flag">The Bit you want to be unset</param>
        public static void Unset<T>(ref T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            flags = (T)(object)(flagsValue & (~flagValue));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiToolExtensions
{
    /// <summary>
    /// Contains Type extensions for many standard objects to allow for "fuzzier" null tests. Some examples of tests it performs are
    /// default(object), object_value = null, object_value.GetType() = null, (int)0, (String)"", and most importantly, typeof(System.DBNull).
    /// Each object type extension is tailored to work specifically for the quirks of that type; for instance, standard testing of DateTime
    /// can throw an exception if done incorrectly. These tests are all safe, and allows the same method to be used across all types.
    /// </summary>
    public static class NullTests
    {
        /// <summary>
        /// Returns true if the object has a value of 0. Checks for other nulls that it can never be as well...just because.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static bool isNull(this int object_value)
        {
            if (object_value == default(int) || object_value == 0 || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns true if the object is null, the objects' default value, or System.DBNull.
        /// The original object value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static bool isNull(this object object_value)
        {
            if (object_value == default(object) || object_value == null || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Returns true if the string object is null, the string default value, an empty string, or System.DBNull.
        /// The original object value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static bool isNull(this string object_value)
        {
            if (object_value == default(string) || object_value == "" || object_value == null || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns true if the DateTime object is default(DateTime).
        /// The original object value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static bool isNull(this DateTime object_value)
        {
            if (object_value == default(DateTime))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns true_value parameter object if the calling object is null, the objects' default value, or System.DBNull.
        /// The object value will be retained if its value is not null. If true, the original object value is overwritten by the given parameter value.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="true_value">[Optional] The object to return if the calling object is null.</param>
        public static object isNull(this object object_value, object true_value)
        {
            if (object_value == default(object) || object_value == null || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true_value;
            }
            return object_value;
        }
        /// <summary>
        /// Returns true_value parameter string if the calling object is null, the string default value, an empty string or DBNull,
        /// The object value will be retained if its value is not null. If true, the original object value is overwritten by the given parameter value.
        /// </summary>
        /// <param name="object_value">[Required] The calling string object.</param>
        /// <param name="true_value">[Optional] The string object to return if the calling string object is null.</param>
        public static string isNull(this string object_value, string true_value)
        {
            if (object_value == default(string) || object_value == "" || object_value == null || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true_value;
            }
            return object_value;
        }
        /// <summary>
        /// Returns true if the object has a value of 0.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="true_value">[Optional] The string object to return if the calling string object is null.</param>
        public static int isNull(this int object_value, int true_value)
        {
            if (object_value == default(int) || object_value == 0 || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true_value;
            }
            return object_value;
        }
        public static DateTime isNull(this DateTime object_value, DateTime true_value)
        {
            if (object_value == default(DateTime))
            {
                return true_value;
            }
            return object_value;
        }
        /// <summary>
        /// Returns the true_value parameter object if the object is null, the objects' default value, an empty string or System.DBNull, 
        /// or the false_value if it is not any of these. The original object value is overwritten by the given parameter value.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="true_value">[Optional] The object to return if the calling object is null.</param>
        /// <param name="false_value">[Optional] The object to return if the calling object is not null.</param>
        public static object isNull(this object object_value, object true_value, object false_value)
        {
            if (object_value == default(object) || object_value == null || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true_value;
            }
            return false_value;
        }
        /// <summary>
        /// Returns the true_value parameter string if the string object is null, the string default value, an empty string or DBNull, 
        /// or the false_value if it is not any of these. The original object value is overwritten by the given parameter value.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="true_value">[Optional] The object to return if the calling object is null.</param>
        /// <param name="false_value">[Optional] The object to return if the calling object is not null.</param>
        public static string isNull(this string object_value, string true_value, string false_value)
        {
            if (object_value == default(string) || object_value == "" || object_value == null || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true_value;
            }
            return false_value;

        }
        /// <summary>
        /// Returns true if the object has a value of 0, or the false_value if it is not 0.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="true_value">[Optional] The string object to return if the calling string object is null.</param>
        /// <param name="false_value">[Optional] The object to return if the calling object is not null.</param>
        public static int isNull(this int object_value, int true_value, int false_value)
        {
            if (object_value == default(int) || object_value == 0 || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true_value;
            }
            return false_value;

        }
        /// <summary>
        /// Returns the true_value parameter string if the DateTime object is default(DateTime), 
        /// or the false_value if it is not. The original object value is overwritten by the given parameter value.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="true_value">[Optional] The object to return if the calling object is default(DateTime).</param>
        /// <param name="false_value">[Optional] The object to return if the calling object is not default(DateTime).</param>
        public static DateTime isNull(this DateTime object_value, DateTime true_value, DateTime false_value)
        {
            if (object_value == default(DateTime))
            {
                return true_value;
            }
            return false_value;
        }
    }
}

using MultiToolExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiToolExtensions
{
    /// <summary>
    /// A basic tuple specifically for handling integer ranges.
    /// </summary>
    public struct RangeTuple
    {
        public int Start { get; set; }
        private int _end { get; set; }
        public int End { get { return _end; } set { _end = _validate(Start, End); } }
        public int Length { get { return (End - Start); } }
        private static int _validate(int start, int end)
        {
            if (start < end)
            {
                return end;
            }
            else
            {
                throw new ArgumentException("RangeTuple.End cannot be less than RangeTuple.Start", nameof(end));
            }
        }
    }
    /// <summary>
    /// RangeTuple Type extensions.
    /// </summary>
    public static class RangeTupleExtensions
    {
        /// <summary>
        /// Parses a RangeTuple out of the given string.
        /// </summary>
        /// <param name="point_str">The point_str.</param>
        /// <returns>A RangeTuple.</returns>
        public static RangeTuple Parse(string point_str)
        {
            RangeTuple point = new RangeTuple();
            point.Start = int.Parse(point_str.Split(',')[0].Replace("(", ""));
            point.End = int.Parse(point_str.Split(',')[1].Replace(")", ""));
            return point;
        }
        /// <summary>
        /// Returns the string representation of the RangeTuple.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static string Print(this RangeTuple object_value)
        {
            return string.Format("({0},{1})", object_value.Start, object_value.End);
        }
        /// <summary>
        /// Returns a List<int> of the RangeTuple object.
        /// </summary>
        /// <param name="object_value">The object_value.</param>
        /// <returns>A list of int.</returns>
        public static List<int> ToList(this RangeTuple object_value)
        {
            return new List<int>() { object_value.Start, object_value.End };
        }
        /// <summary>
        /// Slices the given string matching the RangeTuple Start and End values. If string is shorter than the length
        /// of the RangeTuple, slice will be from RangeTuple.Start to the end of the given string.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="string_value">[Required] The string to slice.</param>
        public static string Substring(this RangeTuple object_value, string string_value)
        {
            if (object_value.Length > 0 && string_value.Length > 0)
            {
                if (object_value.Start < string_value.Length)
                {
                    if (string_value.Length >= object_value.Length)
                    {
                        return string_value.Substring(object_value.Start, object_value.Length);
                    }
                    else
                    {
                        return string_value.Substring(object_value.Start, string_value.Length - 1);
                    }
                }
                else
                {
                    throw new ArgumentException("Start index is larger than given string", nameof(object_value.Start));
                }

            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Slices the given array matching the RangeTuple Start and End values. If array is shorter than the length
        /// of the RangeTuple, slice will be from RangeTuple.Start to the end of the given array.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="array_value">[Required] The array to slice.</param>         
        public static string[] ArraySlice<String>(this RangeTuple object_value, string[] array_value)
        {
            if (object_value.Length > 0 && array_value.Length > 0)
            {
                if (object_value.Start < array_value.Length)
                {
                    if (array_value.Length >= object_value.Length)
                    {
                        return new ArraySegment<string>(array_value, object_value.Start, object_value.Length).ToArray();
                    }
                    else
                    {
                        return new ArraySegment<string>(array_value, object_value.Start, array_value.Length - 1).ToArray();
                    }
                }
                else
                {
                    throw new ArgumentException("Start index is larger than given array", nameof(object_value.Start));
                }
            }
            else
            {
                return new string[0];
            }
        }
        /// <summary>
        /// Slices the given list matching the RangeTuple Start and End values. If list is shorter than the length
        /// of the RangeTuple, slice will be from RangeTuple.Start to the end of the given list.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="list_value">[Required] The list to slice.</param>
        public static List<dynamic> ListSlice(this RangeTuple object_value, List<dynamic> list_value)
        {
            if (object_value.Length > 0 && list_value.Count > 0)
            {
                if (object_value.Start < list_value.Count)
                {
                    if (list_value.Count >= object_value.Length)
                    {
                        return list_value.GetRange(object_value.Start, object_value.Length);
                    }
                    else
                    {
                        return list_value.GetRange(object_value.Start, list_value.Count - 1);
                    }
                }
                else
                {
                    throw new ArgumentException("Start index is larger than given List", nameof(object_value.Start));
                }
            }
            else
            {
                return new List<dynamic>();
            }
        }
    }
}

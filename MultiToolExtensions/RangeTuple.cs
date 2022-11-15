using System;
using System.Collections.Generic;
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
        public static RangeTuple Parse(string point_str)
        {
            RangeTuple point = new RangeTuple();
            point.Start = int.Parse(point_str.Split(',')[0].Replace("(", ""));
            point.End = int.Parse(point_str.Split(',')[1].Replace(")", ""));
            return point;
        }

    }
}

using System;

namespace engine.framework
{
    public struct Range : IEquatable<Range>
    {
        #region Fields

        public float LowerBound;
        public float UpperBound;

        #endregion

        #region Properties

        public static Range Min
        {
            get { return new Range() { LowerBound = float.MinValue }; }
        }

        public static Range Max 
        {
            get { return new Range() { UpperBound = float.MaxValue }; }
        }

        #endregion

        #region Constructors

        public Range(float lower, float upper) 
        {
            LowerBound = 0;
            UpperBound = 0;
        }

        #endregion

        #region Methods

        public float GetRandomValue() 
        {
            return (float)new Random().NextDouble() * (UpperBound - LowerBound);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is Range)
                return Equals((Range)obj);
            else
                return false;
        }

        public bool Equals(Range other) 
        {
            return LowerBound == other.LowerBound && UpperBound == other.UpperBound;
        }

/*
        #region Operator

        public static Rate operator +(Rate value, float value2) 
        {
            float newValue = value.Value + value2;


            return new Rate()
            {
                Value = newValue > value.MaxValue ? value.Value : newValue,
                MaxValue = value.MaxValue,
                MinValue = value.MinValue
            };
        }

        public static Rate operator -(Rate value, float value2) 
        {
            float newValue = value.Value - value2;

            return new Rate()
            {
                Value = newValue < value.MinValue ? value.Value : newValue,
                MaxValue = value.MaxValue,
                MinValue = value.MinValue
            };
        }

        public static Rate operator ++(Rate value)              
        {
            float newValue = value.Value++;


            return new Rate() { Value = newValue > value.MaxValue ? value.Value : newValue, 
                                MaxValue = value.MaxValue, 
                                MinValue = value.MinValue };
        }

        public static Rate operator --(Rate value)              
        {
            float newValue = value.Value--;

            return new Rate()
            {
                Value = newValue < value.MinValue ? value.Value : newValue,
                MaxValue = value.MaxValue,
                MinValue = value.MinValue
            };
        }

        #endregion
 */
    }
}

using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) : base("Value should be between " + i_MinValue + " and " + i_MaxValue)
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }
    }
}

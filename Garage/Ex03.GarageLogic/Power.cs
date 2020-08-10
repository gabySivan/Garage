using System;

namespace Ex03.GarageLogic
{
    public enum eEngineType
    {
        Octan95,
        Octan96,
        Octan98,
        Soler,
        Electric,
    }


  

    internal class Power
    {
        private readonly float r_MaxPower;
        private float m_CurrentPower;


   

        private eEngineType m_eEnginePowerType;

        internal Power(float i_MaxPower, eEngineType i_eEngine)
        {
            m_eEnginePowerType = i_eEngine;
            m_CurrentPower = 0;
            r_MaxPower = i_MaxPower;
        }

       internal void AddPower(float i_PowerToAdd)
        {
            if (m_CurrentPower + i_PowerToAdd > r_MaxPower || i_PowerToAdd < 0)
            {
                throw new ValueOutOfRangeException(0, r_MaxPower - m_CurrentPower);
            }

            m_CurrentPower += i_PowerToAdd;
        }

       internal eEngineType GetEngineType
        {
            get
            {
                return m_eEnginePowerType;
            }
        }

       public float CurrentPower
        {
            get
            {
                return m_CurrentPower;
            }

            set
            {
                m_CurrentPower = value;
            }     
       }

        public float GetMaxPower
        {
            get
            {
                return r_MaxPower;
            }
        }
    }
}

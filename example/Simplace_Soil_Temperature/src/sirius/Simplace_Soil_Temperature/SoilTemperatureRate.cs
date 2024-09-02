
using System;
using System.Collections.Generic;
using CRA.ModelLayer.Core;
using System.Reflection;
using CRA.ModelLayer.ParametersManagement;   

namespace SiriusQualitySoilTemperature.DomainClass
{
    public class SoilTemperatureRate : ICloneable, IDomainClass
    {
        private double _rSnowWaterContentRate;
        private double _rSoilSurfaceTemperatureRate;
        private int _rAgeOfSnowRate;
        private ParametersIO _parametersIO;

        public SoilTemperatureRate()
        {
            _parametersIO = new ParametersIO(this);
        }

        public SoilTemperatureRate(SoilTemperatureRate toCopy, bool copyAll) // copy constructor 
        {
            if (copyAll)
            {
                rSnowWaterContentRate = toCopy.rSnowWaterContentRate;
                rSoilSurfaceTemperatureRate = toCopy.rSoilSurfaceTemperatureRate;
                rAgeOfSnowRate = toCopy.rAgeOfSnowRate;
            }
        }

        public double rSnowWaterContentRate
        {
            get { return this._rSnowWaterContentRate; }
            set { this._rSnowWaterContentRate= value; } 
        }
        public double rSoilSurfaceTemperatureRate
        {
            get { return this._rSoilSurfaceTemperatureRate; }
            set { this._rSoilSurfaceTemperatureRate= value; } 
        }
        public int rAgeOfSnowRate
        {
            get { return this._rAgeOfSnowRate; }
            set { this._rAgeOfSnowRate= value; } 
        }

        public string Description
        {
            get { return "SoilTemperatureRate of the component";}
        }

        public string URL
        {
            get { return "http://" ;}
        }

        public virtual IDictionary<string, PropertyInfo> PropertiesDescription
        {
            get { return _parametersIO.GetCachedProperties(typeof(IDomainClass));}
        }

        public virtual Boolean ClearValues()
        {
             _rSnowWaterContentRate = default(double);
             _rSoilSurfaceTemperatureRate = default(double);
             _rAgeOfSnowRate = default(int);
            return true;
        }

        public virtual Object Clone()
        {
            IDomainClass myclass = (IDomainClass) this.MemberwiseClone();
            _parametersIO.PopulateClonedCopy(myclass);
            return myclass;
        }
    }
}

using System;
using System.Collections.Generic;
using CRA.ModelLayer.Core;
using System.Reflection;
using CRA.ModelLayer.ParametersManagement;   

namespace SoilTemperature.DomainClass
{
    public class SoilTemperatureRateVarInfo : IVarInfoClass
    {
        static VarInfo _rSnowWaterContentRate = new VarInfo();
        static VarInfo _rSoilSurfaceTemperatureRate = new VarInfo();
        static VarInfo _rAgeOfSnowRate = new VarInfo();

        static SoilTemperatureRateVarInfo()
        {
            SoilTemperatureRateVarInfo.DescribeVariables();
        }

        public virtual string Description
        {
            get { return "SoilTemperatureRate Domain class of the component";}
        }

        public string URL
        {
            get { return "http://" ;}
        }

        public string DomainClassOfReference
        {
            get { return "SoilTemperatureRate";}
        }

        public static  VarInfo rSnowWaterContentRate
        {
            get { return _rSnowWaterContentRate;}
        }

        public static  VarInfo rSoilSurfaceTemperatureRate
        {
            get { return _rSoilSurfaceTemperatureRate;}
        }

        public static  VarInfo rAgeOfSnowRate
        {
            get { return _rAgeOfSnowRate;}
        }

        static void DescribeVariables()
        {
            _rSnowWaterContentRate.Name = "rSnowWaterContentRate";
            _rSnowWaterContentRate.Description = "daily snow water content change rate";
            _rSnowWaterContentRate.MaxValue = 1500.0;
            _rSnowWaterContentRate.MinValue = -1500.0;
            _rSnowWaterContentRate.DefaultValue = -1D;
            _rSnowWaterContentRate.Units = "http://www.wurvoc.org/vocabularies/om-1.8/millimetre_per_day";
            _rSnowWaterContentRate.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            _rSoilSurfaceTemperatureRate.Name = "rSoilSurfaceTemperatureRate";
            _rSoilSurfaceTemperatureRate.Description = "daily soil surface temperature change rate";
            _rSoilSurfaceTemperatureRate.MaxValue = 70.0;
            _rSoilSurfaceTemperatureRate.MinValue = -40.0;
            _rSoilSurfaceTemperatureRate.DefaultValue = -1D;
            _rSoilSurfaceTemperatureRate.Units = "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_per_day";
            _rSoilSurfaceTemperatureRate.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            _rAgeOfSnowRate.Name = "rAgeOfSnowRate";
            _rAgeOfSnowRate.Description = "daily age of snow change rate";
            _rAgeOfSnowRate.MaxValue = -1D;
            _rAgeOfSnowRate.MinValue = -1D;
            _rAgeOfSnowRate.DefaultValue = -1D;
            _rAgeOfSnowRate.Units = "http://www.wurvoc.org/vocabularies/om-1.8/one";
            _rAgeOfSnowRate.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");

        }

    }
}
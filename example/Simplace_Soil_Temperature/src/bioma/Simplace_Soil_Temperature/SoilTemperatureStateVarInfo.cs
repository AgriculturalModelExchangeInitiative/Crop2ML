
using System;
using System.Collections.Generic;
using CRA.ModelLayer.Core;
using System.Reflection;
using CRA.ModelLayer.ParametersManagement;   

namespace SoilTemperature.DomainClass
{
    public class SoilTemperatureStateVarInfo : IVarInfoClass
    {
        static VarInfo _pInternalAlbedo = new VarInfo();
        static VarInfo _SnowWaterContent = new VarInfo();
        static VarInfo _SoilSurfaceTemperature = new VarInfo();
        static VarInfo _AgeOfSnow = new VarInfo();
        static VarInfo _rSoilTempArrayRate = new VarInfo();
        static VarInfo _pSoilLayerDepth = new VarInfo();
        static VarInfo _SoilTempArray = new VarInfo();

        static SoilTemperatureStateVarInfo()
        {
            SoilTemperatureStateVarInfo.DescribeVariables();
        }

        public virtual string Description
        {
            get { return "SoilTemperatureState Domain class of the component";}
        }

        public string URL
        {
            get { return "http://" ;}
        }

        public string DomainClassOfReference
        {
            get { return "SoilTemperatureState";}
        }

        public static  VarInfo pInternalAlbedo
        {
            get { return _pInternalAlbedo;}
        }

        public static  VarInfo SnowWaterContent
        {
            get { return _SnowWaterContent;}
        }

        public static  VarInfo SoilSurfaceTemperature
        {
            get { return _SoilSurfaceTemperature;}
        }

        public static  VarInfo AgeOfSnow
        {
            get { return _AgeOfSnow;}
        }

        public static  VarInfo rSoilTempArrayRate
        {
            get { return _rSoilTempArrayRate;}
        }

        public static  VarInfo pSoilLayerDepth
        {
            get { return _pSoilLayerDepth;}
        }

        public static  VarInfo SoilTempArray
        {
            get { return _SoilTempArray;}
        }

        static void DescribeVariables()
        {
            _pInternalAlbedo.Name = "pInternalAlbedo";
            _pInternalAlbedo.Description = "Albedo privat";
            _pInternalAlbedo.MaxValue = 1.0;
            _pInternalAlbedo.MinValue = 0.0;
            _pInternalAlbedo.DefaultValue = -1D;
            _pInternalAlbedo.Units = "http://www.wurvoc.org/vocabularies/om-1.8/one";
            _pInternalAlbedo.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            _SnowWaterContent.Name = "SnowWaterContent";
            _SnowWaterContent.Description = "Snow water content";
            _SnowWaterContent.MaxValue = 1500.0;
            _SnowWaterContent.MinValue = 0.0;
            _SnowWaterContent.DefaultValue = 0.0;
            _SnowWaterContent.Units = "http://www.wurvoc.org/vocabularies/om-1.8/millimetre";
            _SnowWaterContent.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            _SoilSurfaceTemperature.Name = "SoilSurfaceTemperature";
            _SoilSurfaceTemperature.Description = "Soil surface temperature";
            _SoilSurfaceTemperature.MaxValue = 70.0;
            _SoilSurfaceTemperature.MinValue = -40.0;
            _SoilSurfaceTemperature.DefaultValue = 0.0;
            _SoilSurfaceTemperature.Units = "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius";
            _SoilSurfaceTemperature.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            _AgeOfSnow.Name = "AgeOfSnow";
            _AgeOfSnow.Description = "Age of snow";
            _AgeOfSnow.MaxValue = -1D;
            _AgeOfSnow.MinValue = 0;
            _AgeOfSnow.DefaultValue = 0;
            _AgeOfSnow.Units = "http://www.wurvoc.org/vocabularies/om-1.8/day";
            _AgeOfSnow.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");

            _rSoilTempArrayRate.Name = "rSoilTempArrayRate";
            _rSoilTempArrayRate.Description = "Array of daily temperature change";
            _rSoilTempArrayRate.MaxValue = -1D;
            _rSoilTempArrayRate.MinValue = -1D;
            _rSoilTempArrayRate.DefaultValue = -1D;
            _rSoilTempArrayRate.Units = "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_per_day";
            _rSoilTempArrayRate.ValueType = VarInfoValueTypes.GetInstanceForName("ArrayDouble");

            _pSoilLayerDepth.Name = "pSoilLayerDepth";
            _pSoilLayerDepth.Description = "Depth of soil layer plus additional depth";
            _pSoilLayerDepth.MaxValue = -1D;
            _pSoilLayerDepth.MinValue = -1D;
            _pSoilLayerDepth.DefaultValue = -1D;
            _pSoilLayerDepth.Units = "http://www.wurvoc.org/vocabularies/om-1.8/metre";
            _pSoilLayerDepth.ValueType = VarInfoValueTypes.GetInstanceForName("ArrayDouble");

            _SoilTempArray.Name = "SoilTempArray";
            _SoilTempArray.Description = "Array of soil temperatures in layers ";
            _SoilTempArray.MaxValue = -1D;
            _SoilTempArray.MinValue = -1D;
            _SoilTempArray.DefaultValue = -1D;
            _SoilTempArray.Units = "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius";
            _SoilTempArray.ValueType = VarInfoValueTypes.GetInstanceForName("ArrayDouble");

        }

    }
}
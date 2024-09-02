
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using CRA.ModelLayer.MetadataTypes;
using CRA.ModelLayer.Core;
using CRA.ModelLayer.Strategy;
using System.Reflection;
using VarInfo=CRA.ModelLayer.Core.VarInfo;
using Preconditions=CRA.ModelLayer.Core.Preconditions;
using CRA.AgroManagement;       
using SiriusQualitySoilTemperature.DomainClass;
namespace SiriusQualitySoilTemperature.Strategies
{
    public class SnowCoverCalculator : IStrategySiriusQualitySoilTemperature
    {
        public SnowCoverCalculator()
        {
            ModellingOptions mo0_0 = new ModellingOptions();
            //Parameters
            List<VarInfo> _parameters0_0 = new List<VarInfo>();
            VarInfo v1 = new VarInfo();
            v1.DefaultValue = 0.5;
            v1.Description = "Carbon content of upper soil layer";
            v1.Id = 0;
            v1.MaxValue = 20.0;
            v1.MinValue = 0.5;
            v1.Name = "cCarbonContent";
            v1.Size = 1;
            v1.Units = "http://www.wurvoc.org/vocabularies/om-1.8/percent";
            v1.URL = "";
            v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
            _parameters0_0.Add(v1);
            VarInfo v2 = new VarInfo();
            v2.DefaultValue = 0;
            v2.Description = "Initial age of snow";
            v2.Id = 0;
            v2.MaxValue = -1D;
            v2.MinValue = 0;
            v2.Name = "cInitialAgeOfSnow";
            v2.Size = 1;
            v2.Units = "http://www.wurvoc.org/vocabularies/om-1.8/percent";
            v2.URL = "";
            v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v2.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");
            _parameters0_0.Add(v2);
            VarInfo v3 = new VarInfo();
            v3.DefaultValue = 0.0;
            v3.Description = "Initial snow water content";
            v3.Id = 0;
            v3.MaxValue = 1500.0;
            v3.MinValue = 0.0;
            v3.Name = "cInitialSnowWaterContent";
            v3.Size = 1;
            v3.Units = "http://www.wurvoc.org/vocabularies/om-1.8/percent";
            v3.URL = "";
            v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
            _parameters0_0.Add(v3);
            VarInfo v4 = new VarInfo();
            v4.DefaultValue = -1D;
            v4.Description = "Albedo";
            v4.Id = 0;
            v4.MaxValue = 1.0;
            v4.MinValue = 0.0;
            v4.Name = "Albedo";
            v4.Size = 1;
            v4.Units = "http://www.wurvoc.org/vocabularies/om-1.8/one";
            v4.URL = "";
            v4.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v4.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
            _parameters0_0.Add(v4);
            VarInfo v5 = new VarInfo();
            v5.DefaultValue = 2.3;
            v5.Description = "Static part of the snow isolation index calculation";
            v5.Id = 0;
            v5.MaxValue = 10.0;
            v5.MinValue = 0.0;
            v5.Name = "cSnowIsolationFactorA";
            v5.Size = 1;
            v5.Units = "http://www.wurvoc.org/vocabularies/om-1.8/one";
            v5.URL = "";
            v5.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v5.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
            _parameters0_0.Add(v5);
            VarInfo v6 = new VarInfo();
            v6.DefaultValue = 0.22;
            v6.Description = "Dynamic part of the snow isolation index calculation";
            v6.Id = 0;
            v6.MaxValue = 1.0;
            v6.MinValue = 0.0;
            v6.Name = "cSnowIsolationFactorB";
            v6.Size = 1;
            v6.Units = "http://www.wurvoc.org/vocabularies/om-1.8/one";
            v6.URL = "";
            v6.VarType = CRA.ModelLayer.Core.VarInfo.Type.PARAMETER;
            v6.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
            _parameters0_0.Add(v6);
            mo0_0.Parameters=_parameters0_0;

            //Inputs
            List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
            PropertyDescription pd1 = new PropertyDescription();
            pd1.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState);
            pd1.PropertyName = "pInternalAlbedo";
            pd1.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.pInternalAlbedo).ValueType.TypeForCurrentValue;
            pd1.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.pInternalAlbedo);
            _inputs0_0.Add(pd1);
            PropertyDescription pd2 = new PropertyDescription();
            pd2.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous);
            pd2.PropertyName = "iTempMax";
            pd2.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iTempMax).ValueType.TypeForCurrentValue;
            pd2.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iTempMax);
            _inputs0_0.Add(pd2);
            PropertyDescription pd3 = new PropertyDescription();
            pd3.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous);
            pd3.PropertyName = "iTempMin";
            pd3.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iTempMin).ValueType.TypeForCurrentValue;
            pd3.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iTempMin);
            _inputs0_0.Add(pd3);
            PropertyDescription pd4 = new PropertyDescription();
            pd4.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous);
            pd4.PropertyName = "iRadiation";
            pd4.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iRadiation).ValueType.TypeForCurrentValue;
            pd4.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iRadiation);
            _inputs0_0.Add(pd4);
            PropertyDescription pd5 = new PropertyDescription();
            pd5.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous);
            pd5.PropertyName = "iRAIN";
            pd5.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iRAIN).ValueType.TypeForCurrentValue;
            pd5.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iRAIN);
            _inputs0_0.Add(pd5);
            PropertyDescription pd6 = new PropertyDescription();
            pd6.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous);
            pd6.PropertyName = "iCropResidues";
            pd6.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iCropResidues).ValueType.TypeForCurrentValue;
            pd6.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iCropResidues);
            _inputs0_0.Add(pd6);
            PropertyDescription pd7 = new PropertyDescription();
            pd7.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous);
            pd7.PropertyName = "iPotentialSoilEvaporation";
            pd7.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iPotentialSoilEvaporation).ValueType.TypeForCurrentValue;
            pd7.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iPotentialSoilEvaporation);
            _inputs0_0.Add(pd7);
            PropertyDescription pd8 = new PropertyDescription();
            pd8.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous);
            pd8.PropertyName = "iLeafAreaIndex";
            pd8.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iLeafAreaIndex).ValueType.TypeForCurrentValue;
            pd8.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iLeafAreaIndex);
            _inputs0_0.Add(pd8);
            PropertyDescription pd9 = new PropertyDescription();
            pd9.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous);
            pd9.PropertyName = "iSoilTempArray";
            pd9.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iSoilTempArray).ValueType.TypeForCurrentValue;
            pd9.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iSoilTempArray);
            _inputs0_0.Add(pd9);
            PropertyDescription pd10 = new PropertyDescription();
            pd10.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState);
            pd10.PropertyName = "SnowWaterContent";
            pd10.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SnowWaterContent).ValueType.TypeForCurrentValue;
            pd10.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SnowWaterContent);
            _inputs0_0.Add(pd10);
            PropertyDescription pd11 = new PropertyDescription();
            pd11.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState);
            pd11.PropertyName = "SoilSurfaceTemperature";
            pd11.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SoilSurfaceTemperature).ValueType.TypeForCurrentValue;
            pd11.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SoilSurfaceTemperature);
            _inputs0_0.Add(pd11);
            PropertyDescription pd12 = new PropertyDescription();
            pd12.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState);
            pd12.PropertyName = "AgeOfSnow";
            pd12.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.AgeOfSnow).ValueType.TypeForCurrentValue;
            pd12.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.AgeOfSnow);
            _inputs0_0.Add(pd12);
            mo0_0.Inputs=_inputs0_0;

            //Outputs
            List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
            PropertyDescription pd13 = new PropertyDescription();
            pd13.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState);
            pd13.PropertyName = "SnowWaterContent";
            pd13.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SnowWaterContent).ValueType.TypeForCurrentValue;
            pd13.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SnowWaterContent);
            _outputs0_0.Add(pd13);
            mo0_0.Outputs=_outputs0_0;PropertyDescription pd14 = new PropertyDescription();
            pd14.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState);
            pd14.PropertyName = "SoilSurfaceTemperature";
            pd14.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SoilSurfaceTemperature).ValueType.TypeForCurrentValue;
            pd14.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SoilSurfaceTemperature);
            _outputs0_0.Add(pd14);
            mo0_0.Outputs=_outputs0_0;PropertyDescription pd15 = new PropertyDescription();
            pd15.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState);
            pd15.PropertyName = "AgeOfSnow";
            pd15.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.AgeOfSnow).ValueType.TypeForCurrentValue;
            pd15.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.AgeOfSnow);
            _outputs0_0.Add(pd15);
            mo0_0.Outputs=_outputs0_0;PropertyDescription pd16 = new PropertyDescription();
            pd16.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRate);
            pd16.PropertyName = "rSnowWaterContentRate";
            pd16.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rSnowWaterContentRate).ValueType.TypeForCurrentValue;
            pd16.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rSnowWaterContentRate);
            _outputs0_0.Add(pd16);
            mo0_0.Outputs=_outputs0_0;PropertyDescription pd17 = new PropertyDescription();
            pd17.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRate);
            pd17.PropertyName = "rSoilSurfaceTemperatureRate";
            pd17.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rSoilSurfaceTemperatureRate).ValueType.TypeForCurrentValue;
            pd17.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rSoilSurfaceTemperatureRate);
            _outputs0_0.Add(pd17);
            mo0_0.Outputs=_outputs0_0;PropertyDescription pd18 = new PropertyDescription();
            pd18.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRate);
            pd18.PropertyName = "rAgeOfSnowRate";
            pd18.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rAgeOfSnowRate).ValueType.TypeForCurrentValue;
            pd18.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rAgeOfSnowRate);
            _outputs0_0.Add(pd18);
            mo0_0.Outputs=_outputs0_0;PropertyDescription pd19 = new PropertyDescription();
            pd19.DomainClassType = typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureAuxiliary);
            pd19.PropertyName = "SnowIsolationIndex";
            pd19.PropertyType = (SiriusQualitySoilTemperature.DomainClass.SoilTemperatureAuxiliaryVarInfo.SnowIsolationIndex).ValueType.TypeForCurrentValue;
            pd19.PropertyVarInfo =(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureAuxiliaryVarInfo.SnowIsolationIndex);
            _outputs0_0.Add(pd19);
            mo0_0.Outputs=_outputs0_0;
            //Associated strategies
            List<string> lAssStrat0_0 = new List<string>();
            mo0_0.AssociatedStrategies = lAssStrat0_0;
            //Adding the modeling options to the modeling options manager
            _modellingOptionsManager = new ModellingOptionsManager(mo0_0);
            SetStaticParametersVarInfoDefinitions();
            SetPublisherData();

        }

        public string Description
        {
            get { return "as given in the documentation" ;}
        }

        public string URL
        {
            get { return "" ;}
        }

        public string Domain
        {
            get { return "";}
        }

        public string ModelType
        {
            get { return "";}
        }

        public bool IsContext
        {
            get { return false;}
        }

        public IList<int> TimeStep
        {
            get
            {
                IList<int> ts = new List<int>();
                return ts;
            }
        }

        private  PublisherData _pd;
        public PublisherData PublisherData
        {
            get { return _pd;} 
        }

        private  void SetPublisherData()
        {
            _pd = new CRA.ModelLayer.MetadataTypes.PublisherData();
            _pd.Add("Creator", "Gunther Krauss");
            _pd.Add("Date", "");
            _pd.Add("Publisher", "INRES Pflanzenbau, Uni Bonn "); 
        }

        private ModellingOptionsManager _modellingOptionsManager;
        public ModellingOptionsManager ModellingOptionsManager
        {
            get { return _modellingOptionsManager; } 
        }

        public IEnumerable<Type> GetStrategyDomainClassesTypes()
        {
            return new List<Type>() {  typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState),  typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState), typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRate), typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureAuxiliary), typeof(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous)};
        }

        // Getter and setters for the value of the parameters of the strategy. The actual parameters are stored into the ModelingOptionsManager of the strategy.

        public double cCarbonContent
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("cCarbonContent");
                if (vi != null && vi.CurrentValue!=null) return (double)vi.CurrentValue ;
                else throw new Exception("Parameter 'cCarbonContent' not found (or found null) in strategy 'SnowCoverCalculator'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("cCarbonContent");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'cCarbonContent' not found in strategy 'SnowCoverCalculator'");
            }
        }
        public int cInitialAgeOfSnow
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("cInitialAgeOfSnow");
                if (vi != null && vi.CurrentValue!=null) return (int)vi.CurrentValue ;
                else throw new Exception("Parameter 'cInitialAgeOfSnow' not found (or found null) in strategy 'SnowCoverCalculator'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("cInitialAgeOfSnow");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'cInitialAgeOfSnow' not found in strategy 'SnowCoverCalculator'");
            }
        }
        public double cInitialSnowWaterContent
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("cInitialSnowWaterContent");
                if (vi != null && vi.CurrentValue!=null) return (double)vi.CurrentValue ;
                else throw new Exception("Parameter 'cInitialSnowWaterContent' not found (or found null) in strategy 'SnowCoverCalculator'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("cInitialSnowWaterContent");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'cInitialSnowWaterContent' not found in strategy 'SnowCoverCalculator'");
            }
        }
        public double Albedo
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("Albedo");
                if (vi != null && vi.CurrentValue!=null) return (double)vi.CurrentValue ;
                else throw new Exception("Parameter 'Albedo' not found (or found null) in strategy 'SnowCoverCalculator'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("Albedo");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'Albedo' not found in strategy 'SnowCoverCalculator'");
            }
        }
        public double cSnowIsolationFactorA
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("cSnowIsolationFactorA");
                if (vi != null && vi.CurrentValue!=null) return (double)vi.CurrentValue ;
                else throw new Exception("Parameter 'cSnowIsolationFactorA' not found (or found null) in strategy 'SnowCoverCalculator'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("cSnowIsolationFactorA");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'cSnowIsolationFactorA' not found in strategy 'SnowCoverCalculator'");
            }
        }
        public double cSnowIsolationFactorB
        {
            get { 
                VarInfo vi= _modellingOptionsManager.GetParameterByName("cSnowIsolationFactorB");
                if (vi != null && vi.CurrentValue!=null) return (double)vi.CurrentValue ;
                else throw new Exception("Parameter 'cSnowIsolationFactorB' not found (or found null) in strategy 'SnowCoverCalculator'");
            } set {
                VarInfo vi = _modellingOptionsManager.GetParameterByName("cSnowIsolationFactorB");
                if (vi != null)  vi.CurrentValue=value;
                else throw new Exception("Parameter 'cSnowIsolationFactorB' not found in strategy 'SnowCoverCalculator'");
            }
        }

        public void SetParametersDefaultValue()
        {
            _modellingOptionsManager.SetParametersDefaultValue();
        }

        private static void SetStaticParametersVarInfoDefinitions()
        {

            cCarbonContentVarInfo.Name = "cCarbonContent";
            cCarbonContentVarInfo.Description = "Carbon content of upper soil layer";
            cCarbonContentVarInfo.MaxValue = 20.0;
            cCarbonContentVarInfo.MinValue = 0.5;
            cCarbonContentVarInfo.DefaultValue = 0.5;
            cCarbonContentVarInfo.Units = "http://www.wurvoc.org/vocabularies/om-1.8/percent";
            cCarbonContentVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            cInitialAgeOfSnowVarInfo.Name = "cInitialAgeOfSnow";
            cInitialAgeOfSnowVarInfo.Description = "Initial age of snow";
            cInitialAgeOfSnowVarInfo.MaxValue = -1D;
            cInitialAgeOfSnowVarInfo.MinValue = 0;
            cInitialAgeOfSnowVarInfo.DefaultValue = 0;
            cInitialAgeOfSnowVarInfo.Units = "http://www.wurvoc.org/vocabularies/om-1.8/percent";
            cInitialAgeOfSnowVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");

            cInitialSnowWaterContentVarInfo.Name = "cInitialSnowWaterContent";
            cInitialSnowWaterContentVarInfo.Description = "Initial snow water content";
            cInitialSnowWaterContentVarInfo.MaxValue = 1500.0;
            cInitialSnowWaterContentVarInfo.MinValue = 0.0;
            cInitialSnowWaterContentVarInfo.DefaultValue = 0.0;
            cInitialSnowWaterContentVarInfo.Units = "http://www.wurvoc.org/vocabularies/om-1.8/percent";
            cInitialSnowWaterContentVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            AlbedoVarInfo.Name = "Albedo";
            AlbedoVarInfo.Description = "Albedo";
            AlbedoVarInfo.MaxValue = 1.0;
            AlbedoVarInfo.MinValue = 0.0;
            AlbedoVarInfo.DefaultValue = -1D;
            AlbedoVarInfo.Units = "http://www.wurvoc.org/vocabularies/om-1.8/one";
            AlbedoVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            cSnowIsolationFactorAVarInfo.Name = "cSnowIsolationFactorA";
            cSnowIsolationFactorAVarInfo.Description = "Static part of the snow isolation index calculation";
            cSnowIsolationFactorAVarInfo.MaxValue = 10.0;
            cSnowIsolationFactorAVarInfo.MinValue = 0.0;
            cSnowIsolationFactorAVarInfo.DefaultValue = 2.3;
            cSnowIsolationFactorAVarInfo.Units = "http://www.wurvoc.org/vocabularies/om-1.8/one";
            cSnowIsolationFactorAVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Double");

            cSnowIsolationFactorBVarInfo.Name = "cSnowIsolationFactorB";
            cSnowIsolationFactorBVarInfo.Description = "Dynamic part of the snow isolation index calculation";
            cSnowIsolationFactorBVarInfo.MaxValue = 1.0;
            cSnowIsolationFactorBVarInfo.MinValue = 0.0;
            cSnowIsolationFactorBVarInfo.DefaultValue = 0.22;
            cSnowIsolationFactorBVarInfo.Units = "http://www.wurvoc.org/vocabularies/om-1.8/one";
            cSnowIsolationFactorBVarInfo.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
        }

        private static VarInfo _cCarbonContentVarInfo = new VarInfo();
        public static VarInfo cCarbonContentVarInfo
        {
            get { return _cCarbonContentVarInfo;} 
        }

        private static VarInfo _cInitialAgeOfSnowVarInfo = new VarInfo();
        public static VarInfo cInitialAgeOfSnowVarInfo
        {
            get { return _cInitialAgeOfSnowVarInfo;} 
        }

        private static VarInfo _cInitialSnowWaterContentVarInfo = new VarInfo();
        public static VarInfo cInitialSnowWaterContentVarInfo
        {
            get { return _cInitialSnowWaterContentVarInfo;} 
        }

        private static VarInfo _AlbedoVarInfo = new VarInfo();
        public static VarInfo AlbedoVarInfo
        {
            get { return _AlbedoVarInfo;} 
        }

        private static VarInfo _cSnowIsolationFactorAVarInfo = new VarInfo();
        public static VarInfo cSnowIsolationFactorAVarInfo
        {
            get { return _cSnowIsolationFactorAVarInfo;} 
        }

        private static VarInfo _cSnowIsolationFactorBVarInfo = new VarInfo();
        public static VarInfo cSnowIsolationFactorBVarInfo
        {
            get { return _cSnowIsolationFactorBVarInfo;} 
        }

        public string TestPostConditions(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState s,SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState s1,SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRate r,SiriusQualitySoilTemperature.DomainClass.SoilTemperatureAuxiliary a,SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous ex,string callID)
        {
            try
            {
                //Set current values of the outputs to the static VarInfo representing the output properties of the domain classes
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SnowWaterContent.CurrentValue=s.SnowWaterContent;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SoilSurfaceTemperature.CurrentValue=s.SoilSurfaceTemperature;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.AgeOfSnow.CurrentValue=s.AgeOfSnow;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rSnowWaterContentRate.CurrentValue=r.rSnowWaterContentRate;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rSoilSurfaceTemperatureRate.CurrentValue=r.rSoilSurfaceTemperatureRate;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rAgeOfSnowRate.CurrentValue=r.rAgeOfSnowRate;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureAuxiliaryVarInfo.SnowIsolationIndex.CurrentValue=a.SnowIsolationIndex;
                ConditionsCollection prc = new ConditionsCollection();
                Preconditions pre = new Preconditions(); 
                RangeBasedCondition r19 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SnowWaterContent);
                if(r19.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SnowWaterContent.ValueType)){prc.AddCondition(r19);}
                RangeBasedCondition r20 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SoilSurfaceTemperature);
                if(r20.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SoilSurfaceTemperature.ValueType)){prc.AddCondition(r20);}
                RangeBasedCondition r21 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.AgeOfSnow);
                if(r21.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.AgeOfSnow.ValueType)){prc.AddCondition(r21);}
                RangeBasedCondition r22 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rSnowWaterContentRate);
                if(r22.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rSnowWaterContentRate.ValueType)){prc.AddCondition(r22);}
                RangeBasedCondition r23 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rSoilSurfaceTemperatureRate);
                if(r23.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rSoilSurfaceTemperatureRate.ValueType)){prc.AddCondition(r23);}
                RangeBasedCondition r24 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rAgeOfSnowRate);
                if(r24.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRateVarInfo.rAgeOfSnowRate.ValueType)){prc.AddCondition(r24);}
                RangeBasedCondition r25 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureAuxiliaryVarInfo.SnowIsolationIndex);
                if(r25.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureAuxiliaryVarInfo.SnowIsolationIndex.ValueType)){prc.AddCondition(r25);}
                string postConditionsResult = pre.VerifyPostconditions(prc, callID); if (!string.IsNullOrEmpty(postConditionsResult)) { pre.TestsOut(postConditionsResult, true, "PostConditions errors in strategy " + this.GetType().Name); } return postConditionsResult;
            }
            catch (Exception exception)
            {
                string msg = "SiriusQuality.SoilTemperature, " + this.GetType().Name + ": Unhandled exception running post-condition test. ";
                throw new Exception(msg, exception);
            }
        }

        public string TestPreConditions(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState s,SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState s1,SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRate r,SiriusQualitySoilTemperature.DomainClass.SoilTemperatureAuxiliary a,SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous ex,string callID)
        {
            try
            {
                //Set current values of the inputs to the static VarInfo representing the inputs properties of the domain classes
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.pInternalAlbedo.CurrentValue=s.pInternalAlbedo;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iTempMax.CurrentValue=ex.iTempMax;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iTempMin.CurrentValue=ex.iTempMin;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iRadiation.CurrentValue=ex.iRadiation;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iRAIN.CurrentValue=ex.iRAIN;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iCropResidues.CurrentValue=ex.iCropResidues;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iPotentialSoilEvaporation.CurrentValue=ex.iPotentialSoilEvaporation;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iLeafAreaIndex.CurrentValue=ex.iLeafAreaIndex;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iSoilTempArray.CurrentValue=ex.iSoilTempArray;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SnowWaterContent.CurrentValue=s.SnowWaterContent;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SoilSurfaceTemperature.CurrentValue=s.SoilSurfaceTemperature;
                SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.AgeOfSnow.CurrentValue=s.AgeOfSnow;
                ConditionsCollection prc = new ConditionsCollection();
                Preconditions pre = new Preconditions(); 
                RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.pInternalAlbedo);
                if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.pInternalAlbedo.ValueType)){prc.AddCondition(r1);}
                RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iTempMax);
                if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iTempMax.ValueType)){prc.AddCondition(r2);}
                RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iTempMin);
                if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iTempMin.ValueType)){prc.AddCondition(r3);}
                RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iRadiation);
                if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iRadiation.ValueType)){prc.AddCondition(r4);}
                RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iRAIN);
                if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iRAIN.ValueType)){prc.AddCondition(r5);}
                RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iCropResidues);
                if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iCropResidues.ValueType)){prc.AddCondition(r6);}
                RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iPotentialSoilEvaporation);
                if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iPotentialSoilEvaporation.ValueType)){prc.AddCondition(r7);}
                RangeBasedCondition r8 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iLeafAreaIndex);
                if(r8.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iLeafAreaIndex.ValueType)){prc.AddCondition(r8);}
                RangeBasedCondition r9 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iSoilTempArray);
                if(r9.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenousVarInfo.iSoilTempArray.ValueType)){prc.AddCondition(r9);}
                RangeBasedCondition r10 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SnowWaterContent);
                if(r10.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SnowWaterContent.ValueType)){prc.AddCondition(r10);}
                RangeBasedCondition r11 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SoilSurfaceTemperature);
                if(r11.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.SoilSurfaceTemperature.ValueType)){prc.AddCondition(r11);}
                RangeBasedCondition r12 = new RangeBasedCondition(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.AgeOfSnow);
                if(r12.ApplicableVarInfoValueTypes.Contains( SiriusQualitySoilTemperature.DomainClass.SoilTemperatureStateVarInfo.AgeOfSnow.ValueType)){prc.AddCondition(r12);}
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("cCarbonContent")));
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("cInitialAgeOfSnow")));
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("cInitialSnowWaterContent")));
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Albedo")));
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("cSnowIsolationFactorA")));
                prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("cSnowIsolationFactorB")));
                string preConditionsResult = pre.VerifyPreconditions(prc, callID); if (!string.IsNullOrEmpty(preConditionsResult)) { pre.TestsOut(preConditionsResult, true, "PreConditions errors in strategy " + this.GetType().Name); } return preConditionsResult;
            }
            catch (Exception exception)
            {
                string msg = "SiriusQuality.SoilTemperature, " + this.GetType().Name + ": Unhandled exception running pre-condition test. ";
                throw new Exception(msg, exception);
            }
        }

        public void Estimate(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState s,SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState s1,SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRate r,SiriusQualitySoilTemperature.DomainClass.SoilTemperatureAuxiliary a,SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous ex)
        {
            try
            {
                CalculateModel(s, s1, r, a, ex);
            }
            catch (Exception exception)
            {
                string msg = "Error in component SiriusQualitySoilTemperature, strategy: " + this.GetType().Name + ": Unhandled exception running model. "+exception.GetType().FullName+" - "+exception.Message;
                throw new Exception(msg, exception);
            }
        }

        public void Init(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState s, SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState s1, SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRate r, SiriusQualitySoilTemperature.DomainClass.SoilTemperatureAuxiliary a, SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous ex)
        {
            double iTempMax;
            double iTempMin;
            double iRadiation;
            double iRAIN;
            double iCropResidues;
            double iPotentialSoilEvaporation;
            double iLeafAreaIndex;
            double[] iSoilTempArray;
            double pInternalAlbedo;
            double SnowWaterContent = 0.0;
            double SoilSurfaceTemperature = 0.0;
            int AgeOfSnow = 0;
            pInternalAlbedo = 0.00d;
            double TMEAN;
            double TAMPL;
            double DST;
            if (Albedo == (double)(0))
            {
                pInternalAlbedo = 0.02260d * Math.Log(cCarbonContent, 10) + 0.15020d;
            }
            else
            {
                pInternalAlbedo = Albedo;
            }
            TMEAN = 0.50d * (iTempMax + iTempMin);
            TAMPL = 0.50d * (iTempMax - iTempMin);
            DST = TMEAN + (TAMPL * (iRadiation * (1 - pInternalAlbedo) - 14) / 20);
            SoilSurfaceTemperature = DST;
            AgeOfSnow = cInitialAgeOfSnow;
            SnowWaterContent = cInitialSnowWaterContent;
            s.pInternalAlbedo= pInternalAlbedo;
            s.SnowWaterContent= SnowWaterContent;
            s.SoilSurfaceTemperature= SoilSurfaceTemperature;
            s.AgeOfSnow= AgeOfSnow;
        }

        private void CalculateModel(SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState s, SiriusQualitySoilTemperature.DomainClass.SoilTemperatureState s1, SiriusQualitySoilTemperature.DomainClass.SoilTemperatureRate r, SiriusQualitySoilTemperature.DomainClass.SoilTemperatureAuxiliary a, SiriusQualitySoilTemperature.DomainClass.SoilTemperatureExogenous ex)
        {
            double pInternalAlbedo = s.pInternalAlbedo;
            double iTempMax = ex.iTempMax;
            double iTempMin = ex.iTempMin;
            double iRadiation = ex.iRadiation;
            double iRAIN = ex.iRAIN;
            double iCropResidues = ex.iCropResidues;
            double iPotentialSoilEvaporation = ex.iPotentialSoilEvaporation;
            double iLeafAreaIndex = ex.iLeafAreaIndex;
            double[] iSoilTempArray = ex.iSoilTempArray;
            double SnowWaterContent = s.SnowWaterContent;
            double SoilSurfaceTemperature = s.SoilSurfaceTemperature;
            int AgeOfSnow = s.AgeOfSnow;
            double rSnowWaterContentRate;
            double rSoilSurfaceTemperatureRate;
            int rAgeOfSnowRate;
            double SnowIsolationIndex;
            double tiCropResidues;
            double tiSoilTempArray;
            double TMEAN;
            double TAMPL;
            double DST;
            double tSoilSurfaceTemperature;
            double tSnowIsolationIndex;
            double SNOWEVAPORATION;
            double SNOWMELT;
            double EAJ;
            double ageOfSnowFactor;
            double SNPKT;
            tiCropResidues = iCropResidues * 10.00d;
            tiSoilTempArray = iSoilTempArray[0];
            TMEAN = 0.50d * (iTempMax + iTempMin);
            TAMPL = 0.50d * (iTempMax - iTempMin);
            DST = TMEAN + (TAMPL * (iRadiation * (1 - pInternalAlbedo) - 14) / 20);
            if (iRAIN > (double)(0) && (tiSoilTempArray < (double)(1) || (SnowWaterContent > (double)(3) || SoilSurfaceTemperature < (double)(0))))
            {
                SnowWaterContent = SnowWaterContent + iRAIN;
            }
            tSnowIsolationIndex = 1.00d;
            if (tiCropResidues < (double)(10))
            {
                tSnowIsolationIndex = tiCropResidues / (tiCropResidues + Math.Exp(5.340d - (2.40d * tiCropResidues)));
            }
            if (SnowWaterContent < 1E-10)
            {
                tSnowIsolationIndex = tSnowIsolationIndex * 0.850d;
                tSoilSurfaceTemperature = 0.50d * (DST + ((1 - tSnowIsolationIndex) * DST) + (tSnowIsolationIndex * tiSoilTempArray));
            }
            else
            {
                tSnowIsolationIndex = Math.Max(SnowWaterContent / (SnowWaterContent + Math.Exp(cSnowIsolationFactorA - (cSnowIsolationFactorB * SnowWaterContent))), tSnowIsolationIndex);
                tSoilSurfaceTemperature = (1 - tSnowIsolationIndex) * DST + (tSnowIsolationIndex * tiSoilTempArray);
            }
            if (SnowWaterContent == (double)(0) && !(iRAIN > (double)(0) && tiSoilTempArray < (double)(1)))
            {
                SnowWaterContent = (double)(0);
            }
            else
            {
                EAJ = .50d;
                if (SnowWaterContent < (double)(5))
                {
                    EAJ = Math.Exp(-Math.Max((0.40d * iLeafAreaIndex), (0.10d * (tiCropResidues + 0.10d))));
                }
                SNOWEVAPORATION = iPotentialSoilEvaporation * EAJ;
                ageOfSnowFactor = AgeOfSnow / (AgeOfSnow + Math.Exp(5.340d - (2.3950d * AgeOfSnow)));
                SNPKT = .33330d * (2 * Math.Min(tSoilSurfaceTemperature, tiSoilTempArray) + iTempMax);
                if (TMEAN > (double)(0))
                {
                    SNOWMELT = Math.Max(0, Math.Sqrt(iTempMax * iRadiation) * (1.520d + (.540d * ageOfSnowFactor * SNPKT)));
                }
                else
                {
                    SNOWMELT = (double)(0);
                }
                if (SNOWMELT + SNOWEVAPORATION > SnowWaterContent)
                {
                    SNOWMELT = SNOWMELT / (SNOWMELT + SNOWEVAPORATION) * SnowWaterContent;
                    SNOWEVAPORATION = SNOWEVAPORATION / (SNOWMELT + SNOWEVAPORATION) * SnowWaterContent;
                }
                SnowWaterContent = SnowWaterContent - (SNOWMELT + SNOWEVAPORATION);
                if (SnowWaterContent < (double)(0))
                {
                    SnowWaterContent = (double)(0);
                }
                if (SnowWaterContent < (double)(5))
                {
                    AgeOfSnow = 0;
                }
                else
                {
                    AgeOfSnow = AgeOfSnow + 1;
                }
            }
            rSnowWaterContentRate = SnowWaterContent - SnowWaterContent;
            rSoilSurfaceTemperatureRate = tSoilSurfaceTemperature - SoilSurfaceTemperature;
            rAgeOfSnowRate = AgeOfSnow - AgeOfSnow;
            SoilSurfaceTemperature = tSoilSurfaceTemperature;
            SnowIsolationIndex = tSnowIsolationIndex;
            s.SnowWaterContent= SnowWaterContent;
            s.SoilSurfaceTemperature= SoilSurfaceTemperature;
            s.AgeOfSnow= AgeOfSnow;
            r.rSnowWaterContentRate = rSnowWaterContentRate;
            r.rSoilSurfaceTemperatureRate = rSoilSurfaceTemperatureRate;
            r.rAgeOfSnowRate = rAgeOfSnowRate;
            a.SnowIsolationIndex= SnowIsolationIndex;
        }

    }
}
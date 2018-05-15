

 //Author:Loic Manceau loic.manceau@inra.fr
 //Institution:INRA
 //Author of revision: 
 //Date first release:
 //Date of revision:

using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CRA.ModelLayer.MetadataTypes;
using CRA.ModelLayer.Core;
using CRA.ModelLayer.Strategy;
using System.Reflection;
using VarInfo=CRA.ModelLayer.Core.VarInfo;
using Preconditions=CRA.ModelLayer.Core.Preconditions;


using SiriusQualityPhenology;
using CRA.AgroManagement;


//To make this project compile please add the reference to assembly: SiriusQuality-PhenologyComponent, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: CRA.ModelLayer, Version=1.0.5212.29139, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: CRA.AgroManagement2014, Version=0.8.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;

namespace SiriusQualityPhenology.Strategies
{

	/// <summary>
	///Class CalculatePhyllochronWithPTQ
    /// Calculate the phyllochron  with Photothermal Quotien
    /// </summary>
	public class CalculatePhyllochronWithPTQ : IStrategySiriusQualityPhenology
	{

	#region Constructor

			public CalculatePhyllochronWithPTQ()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 0.45;
				 v1.Description = "Exctinction Coefficient";
				 v1.Id = 0;
				 v1.MaxValue = 50;
				 v1.MinValue = 0;
				 v1.Name = "Kl";
				 v1.Size = 1;
				 v1.Units = "dimesionless";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 0.00439;
				 v2.Description = "Slope for Phyllochron  parametrization";
				 v2.Id = 0;
				 v2.MaxValue = 100;
				 v2.MinValue = 0;
				 v2.Name = "slopePhylPTQ";
				 v2.Size = 1;
				 v2.Units = "m²°Cd/MJ";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 0.005208;
				 v3.Description = "Intercept for Phyllochron parametrization";
				 v3.Id = 0;
				 v3.MaxValue = 100;
				 v3.MinValue = 0;
				 v3.Name = "interceptPhylPTQ";
				 v3.Size = 1;
				 v3.Units = "dimensionless";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new VarInfo();
				 v4.DefaultValue = 2;
				 v4.Description = "Potential surface area of the leaves produced before floral initiation";
				 v4.Id = 0;
				 v4.MaxValue = 100;
				 v4.MinValue = 0;
				 v4.Name = "AreaSL";
				 v4.Size = 1;
				 v4.Units = "cm²/lamina";
				 v4.URL = "";
				 v4.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v4.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new VarInfo();
				 v5.DefaultValue = 2;
				 v5.Description = "Potential surface area of the sheath of the leaves produced before floral initiation";
				 v5.Id = 0;
				 v5.MaxValue = 100;
				 v5.MinValue = 0;
				 v5.Name = "AreaSS";
				 v5.Size = 1;
				 v5.Units = "cm²/sheath";
				 v5.URL = "";
				 v5.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v5.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v5);
				VarInfo v6 = new VarInfo();
				 v6.DefaultValue = 250;
				 v6.Description = "Sowing density";
				 v6.Id = 0;
				 v6.MaxValue = 1000;
				 v6.MinValue = 0;
				 v6.Name = "SowingDensity";
				 v6.Size = 1;
				 v6.Units = "m-²";
				 v6.URL = "";
				 v6.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v6.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v6);
				VarInfo v7 = new VarInfo();
				 v7.DefaultValue = 20;
				 v7.Description = "Phyllochron at PTQ=1";
				 v7.Id = 0;
				 v7.MaxValue = 1000;
				 v7.MinValue = 0;
				 v7.Name = "PhylPTQ1";
				 v7.Size = 1;
				 v7.Units = "°Cd/leaf";
				 v7.URL = "";
				 v7.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v7.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v7);
				VarInfo v8 = new VarInfo();
				 v8.DefaultValue = 0.842934;
				 v8.Description = "Slope to intercept ratio for Phyllochron  parametrization with PhotoThermal Quotient";
				 v8.Id = 0;
				 v8.MaxValue = 1000;
				 v8.MinValue = 0;
				 v8.Name = "aPTQ";
				 v8.Size = 1;
				 v8.Units = "°Cd";
				 v8.URL = "";
				 v8.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v8.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v8);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd1.PropertyName = "pastMaxAI";
				pd1.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.pastMaxAI)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.pastMaxAI);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd2.PropertyName = "PTQ";
				pd2.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.PTQ)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.PTQ);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd3.PropertyName = "GAI";
				pd3.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.GAI)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.GAI);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd4.PropertyName = "LeafNumber";
				pd4.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
				_inputs0_0.Add(pd4);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd5.PropertyName = "pastMaxAI";
				pd5.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.pastMaxAI)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.pastMaxAI);
				_outputs0_0.Add(pd5);
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd6.PropertyName = "Phyllochron";
				pd6.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron);
				_outputs0_0.Add(pd6);
				mo0_0.Outputs=_outputs0_0;
				//Associated strategies
				List<string> lAssStrat0_0 = new List<string>();
				mo0_0.AssociatedStrategies = lAssStrat0_0;
				//Adding the modeling options to the modeling options manager
				_modellingOptionsManager = new ModellingOptionsManager(mo0_0);
			
				SetStaticParametersVarInfoDefinitions();
				SetPublisherData();
					
			}

	#endregion

	#region Implementation of IAnnotatable

			/// <summary>
			/// Description of the model
			/// </summary>
			public string Description
			{
				get { return "Calculate the phyllochron  with Photothermal Quotien"; }
			}
			
			/// <summary>
			/// URL to access the description of the model
			/// </summary>
			public string URL
			{
				get { return "http://biomamodelling.org"; }
			}
		

	#endregion
	
	#region Implementation of IStrategy

			/// <summary>
			/// Domain of the model.
			/// </summary>
			public string Domain
			{
				get {  return "Crop"; }
			}

			/// <summary>
			/// Type of the model.
			/// </summary>
			public string ModelType
			{
				get { return "Development"; }
			}

			/// <summary>
			/// Declare if the strategy is a ContextStrategy, that is, it contains logic to select a strategy at run time. 
			/// </summary>
			public bool IsContext
			{
					get { return  false; }
			}

			/// <summary>
			/// Timestep to be used with this strategy
			/// </summary>
			public IList<int> TimeStep
			{
				get
				{
					IList<int> ts = new List<int>();
					
					return ts;
				}
			}
	
	
	#region Publisher Data

			private PublisherData _pd;
			private  void SetPublisherData()
			{
					// Set publishers' data
					
				_pd = new CRA.ModelLayer.MetadataTypes.PublisherData();
				_pd.Add("Creator", "loic.manceau@inra.fr");
				_pd.Add("Date", "");
				_pd.Add("Publisher", "INRA");
			}

			public PublisherData PublisherData
			{
				get { return _pd; }
			}

	#endregion

	#region ModellingOptionsManager

			private ModellingOptionsManager _modellingOptionsManager;
			
			public ModellingOptionsManager ModellingOptionsManager
			{
				get { return _modellingOptionsManager; }            
			}

	#endregion

			/// <summary>
			/// Return the types of the domain classes used by the strategy
			/// </summary>
			/// <returns></returns>
			public IEnumerable<Type> GetStrategyDomainClassesTypes()
			{
				return new List<Type>() {  typeof(SiriusQualityPhenology.PhenologyState),typeof(SiriusQualityPhenology.PhenologyState),typeof(CRA.AgroManagement.ActEvents) };
			}

	#endregion

    #region Instances of the parameters
			
			// Getter and setters for the value of the parameters of the strategy. The actual parameters are stored into the ModelingOptionsManager of the strategy.

			
			public Double Kl
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Kl");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Kl' not found (or found null) in strategy 'CalculatePhyllochronWithPTQ'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Kl");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Kl' not found in strategy 'CalculatePhyllochronWithPTQ'");
				}
			}
			public Double slopePhylPTQ
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("slopePhylPTQ");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'slopePhylPTQ' not found (or found null) in strategy 'CalculatePhyllochronWithPTQ'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("slopePhylPTQ");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'slopePhylPTQ' not found in strategy 'CalculatePhyllochronWithPTQ'");
				}
			}
			public Double interceptPhylPTQ
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("interceptPhylPTQ");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'interceptPhylPTQ' not found (or found null) in strategy 'CalculatePhyllochronWithPTQ'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("interceptPhylPTQ");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'interceptPhylPTQ' not found in strategy 'CalculatePhyllochronWithPTQ'");
				}
			}
			public Double AreaSL
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("AreaSL");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'AreaSL' not found (or found null) in strategy 'CalculatePhyllochronWithPTQ'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("AreaSL");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'AreaSL' not found in strategy 'CalculatePhyllochronWithPTQ'");
				}
			}
			public Double AreaSS
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("AreaSS");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'AreaSS' not found (or found null) in strategy 'CalculatePhyllochronWithPTQ'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("AreaSS");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'AreaSS' not found in strategy 'CalculatePhyllochronWithPTQ'");
				}
			}
			public Double SowingDensity
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SowingDensity");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'SowingDensity' not found (or found null) in strategy 'CalculatePhyllochronWithPTQ'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SowingDensity");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SowingDensity' not found in strategy 'CalculatePhyllochronWithPTQ'");
				}
			}
			public Double PhylPTQ1
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PhylPTQ1");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PhylPTQ1' not found (or found null) in strategy 'CalculatePhyllochronWithPTQ'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PhylPTQ1");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PhylPTQ1' not found in strategy 'CalculatePhyllochronWithPTQ'");
				}
			}
			public Double aPTQ
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("aPTQ");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'aPTQ' not found (or found null) in strategy 'CalculatePhyllochronWithPTQ'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("aPTQ");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'aPTQ' not found in strategy 'CalculatePhyllochronWithPTQ'");
				}
			}

			// Getter and setters for the value of the parameters of a composite strategy
			

	#endregion		

	
	#region Parameters initialization method
			
            /// <summary>
            /// Set parameter(s) current values to the default value
            /// </summary>
            public void SetParametersDefaultValue()
            {
				_modellingOptionsManager.SetParametersDefaultValue();
				 

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section5
					//Code written below will not be overwritten by a future code generation

					//Custom initialization of the parameter. E.g. initialization of the array dimensions of array parameters

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section5 
            }

	#endregion		

	#region Static parameters VarInfo definition

			// Define the properties of the static VarInfo of the parameters
			private static void SetStaticParametersVarInfoDefinitions()
			{                                
                KlVarInfo.Name = "Kl";
				KlVarInfo.Description =" Exctinction Coefficient";
				KlVarInfo.MaxValue = 50;
				KlVarInfo.MinValue = 0;
				KlVarInfo.DefaultValue = 0.45;
				KlVarInfo.Units = "dimesionless";
				KlVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				slopePhylPTQVarInfo.Name = "slopePhylPTQ";
				slopePhylPTQVarInfo.Description =" Slope for Phyllochron  parametrization";
				slopePhylPTQVarInfo.MaxValue = 100;
				slopePhylPTQVarInfo.MinValue = 0;
				slopePhylPTQVarInfo.DefaultValue = 0.00439;
				slopePhylPTQVarInfo.Units = "m²°Cd/MJ";
				slopePhylPTQVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				interceptPhylPTQVarInfo.Name = "interceptPhylPTQ";
				interceptPhylPTQVarInfo.Description =" Intercept for Phyllochron parametrization";
				interceptPhylPTQVarInfo.MaxValue = 100;
				interceptPhylPTQVarInfo.MinValue = 0;
				interceptPhylPTQVarInfo.DefaultValue = 0.005208;
				interceptPhylPTQVarInfo.Units = "dimensionless";
				interceptPhylPTQVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				AreaSLVarInfo.Name = "AreaSL";
				AreaSLVarInfo.Description =" Potential surface area of the leaves produced before floral initiation";
				AreaSLVarInfo.MaxValue = 100;
				AreaSLVarInfo.MinValue = 0;
				AreaSLVarInfo.DefaultValue = 2;
				AreaSLVarInfo.Units = "cm²/lamina";
				AreaSLVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				AreaSSVarInfo.Name = "AreaSS";
				AreaSSVarInfo.Description =" Potential surface area of the sheath of the leaves produced before floral initiation";
				AreaSSVarInfo.MaxValue = 100;
				AreaSSVarInfo.MinValue = 0;
				AreaSSVarInfo.DefaultValue = 2;
				AreaSSVarInfo.Units = "cm²/sheath";
				AreaSSVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				SowingDensityVarInfo.Name = "SowingDensity";
				SowingDensityVarInfo.Description =" Sowing density";
				SowingDensityVarInfo.MaxValue = 1000;
				SowingDensityVarInfo.MinValue = 0;
				SowingDensityVarInfo.DefaultValue = 250;
				SowingDensityVarInfo.Units = "m-²";
				SowingDensityVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PhylPTQ1VarInfo.Name = "PhylPTQ1";
				PhylPTQ1VarInfo.Description =" Phyllochron at PTQ=1";
				PhylPTQ1VarInfo.MaxValue = 1000;
				PhylPTQ1VarInfo.MinValue = 0;
				PhylPTQ1VarInfo.DefaultValue = 20;
				PhylPTQ1VarInfo.Units = "°Cd/leaf";
				PhylPTQ1VarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				aPTQVarInfo.Name = "aPTQ";
				aPTQVarInfo.Description =" Slope to intercept ratio for Phyllochron  parametrization with PhotoThermal Quotient";
				aPTQVarInfo.MaxValue = 1000;
				aPTQVarInfo.MinValue = 0;
				aPTQVarInfo.DefaultValue = 0.842934;
				aPTQVarInfo.Units = "°Cd";
				aPTQVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _KlVarInfo= new VarInfo();
				/// <summary> 
				///Kl VarInfo definition
				/// </summary>
				public static VarInfo KlVarInfo
				{
					get { return _KlVarInfo; }
				}
				private static VarInfo _slopePhylPTQVarInfo= new VarInfo();
				/// <summary> 
				///slopePhylPTQ VarInfo definition
				/// </summary>
				public static VarInfo slopePhylPTQVarInfo
				{
					get { return _slopePhylPTQVarInfo; }
				}
				private static VarInfo _interceptPhylPTQVarInfo= new VarInfo();
				/// <summary> 
				///interceptPhylPTQ VarInfo definition
				/// </summary>
				public static VarInfo interceptPhylPTQVarInfo
				{
					get { return _interceptPhylPTQVarInfo; }
				}
				private static VarInfo _AreaSLVarInfo= new VarInfo();
				/// <summary> 
				///AreaSL VarInfo definition
				/// </summary>
				public static VarInfo AreaSLVarInfo
				{
					get { return _AreaSLVarInfo; }
				}
				private static VarInfo _AreaSSVarInfo= new VarInfo();
				/// <summary> 
				///AreaSS VarInfo definition
				/// </summary>
				public static VarInfo AreaSSVarInfo
				{
					get { return _AreaSSVarInfo; }
				}
				private static VarInfo _SowingDensityVarInfo= new VarInfo();
				/// <summary> 
				///SowingDensity VarInfo definition
				/// </summary>
				public static VarInfo SowingDensityVarInfo
				{
					get { return _SowingDensityVarInfo; }
				}
				private static VarInfo _PhylPTQ1VarInfo= new VarInfo();
				/// <summary> 
				///PhylPTQ1 VarInfo definition
				/// </summary>
				public static VarInfo PhylPTQ1VarInfo
				{
					get { return _PhylPTQ1VarInfo; }
				}
				private static VarInfo _aPTQVarInfo= new VarInfo();
				/// <summary> 
				///aPTQ VarInfo definition
				/// </summary>
				public static VarInfo aPTQVarInfo
				{
					get { return _aPTQVarInfo; }
				}					
			
			//Parameters static VarInfo list of the composite class
						

	#endregion
	
	#region pre/post conditions management		

		    /// <summary>
			/// Test to verify the postconditions
			/// </summary>
			public string TestPostConditions(SiriusQualityPhenology.PhenologyState phenologystate,SiriusQualityPhenology.PhenologyState phenologystate1, string callID)
			{
				try
				{
					//Set current values of the outputs to the static VarInfo representing the output properties of the domain classes				
					
					SiriusQualityPhenology.PhenologyStateVarInfo.pastMaxAI.CurrentValue=phenologystate.pastMaxAI;
					SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron.CurrentValue=phenologystate.Phyllochron;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.pastMaxAI);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.pastMaxAI.ValueType)){prc.AddCondition(r5);}
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron.ValueType)){prc.AddCondition(r6);}

					

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section4
					//Code written below will not be overwritten by a future code generation

        

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section4 

					//Get the evaluation of postconditions
					string postConditionsResult =pre.VerifyPostconditions(prc, callID);
					//if we have errors, send it to the configured output 
					if(!string.IsNullOrEmpty(postConditionsResult)) { pre.TestsOut(postConditionsResult, true, "PostConditions errors in component SiriusQualityPhenology.Strategies, strategy " + this.GetType().Name ); }
					return postConditionsResult;
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1001,	"Strategy: " + this.GetType().Name + " - Unhandled exception running post-conditions");

					string msg = "Component SiriusQualityPhenology.Strategies, " + this.GetType().Name + ": Unhandled exception running post-condition test. ";
					throw new Exception(msg, exception);
				}
			}

			/// <summary>
			/// Test to verify the preconditions
			/// </summary>
			public string TestPreConditions(SiriusQualityPhenology.PhenologyState phenologystate,SiriusQualityPhenology.PhenologyState phenologystate1, string callID)
			{
				try
				{
					//Set current values of the inputs to the static VarInfo representing the input properties of the domain classes				
					
					SiriusQualityPhenology.PhenologyStateVarInfo.pastMaxAI.CurrentValue=phenologystate.pastMaxAI;
					SiriusQualityPhenology.PhenologyStateVarInfo.PTQ.CurrentValue=phenologystate.PTQ;
					SiriusQualityPhenology.PhenologyStateVarInfo.GAI.CurrentValue=phenologystate.GAI;
					SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.CurrentValue=phenologystate.LeafNumber;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.pastMaxAI);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.pastMaxAI.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.PTQ);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.PTQ.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.GAI);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.GAI.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.ValueType)){prc.AddCondition(r4);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Kl")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("slopePhylPTQ")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("interceptPhylPTQ")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("AreaSL")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("AreaSS")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SowingDensity")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PhylPTQ1")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("aPTQ")));

					

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section3
					//Code written below will not be overwritten by a future code generation

        

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section3 
								
					//Get the evaluation of preconditions;					
					string preConditionsResult =pre.VerifyPreconditions(prc, callID);
					//if we have errors, send it to the configured output 
					if(!string.IsNullOrEmpty(preConditionsResult)) { pre.TestsOut(preConditionsResult, true, "PreConditions errors in component SiriusQualityPhenology.Strategies, strategy " + this.GetType().Name ); }
					return preConditionsResult;
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//	TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1002,"Strategy: " + this.GetType().Name + " - Unhandled exception running pre-conditions");

					string msg = "Component SiriusQualityPhenology.Strategies, " + this.GetType().Name + ": Unhandled exception running pre-condition test. ";
					throw new Exception(msg, exception);
				}
			}

		
	#endregion
		


	#region Model

		 	/// <summary>
			/// Run the strategy to calculate the outputs. In case of error during the execution, the preconditions tests are executed.
			/// </summary>
			public void Estimate(SiriusQualityPhenology.PhenologyState phenologystate,SiriusQualityPhenology.PhenologyState phenologystate1,CRA.AgroManagement.ActEvents actevents)
			{
				try
				{
					CalculateModel(phenologystate,phenologystate1,actevents);

					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Verbose, 1005,"Strategy: " + this.GetType().Name + " - Model executed");
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1003,		"Strategy: " + this.GetType().Name + " - Unhandled exception running model");

					string msg = "Error in component SiriusQualityPhenology.Strategies, strategy: " + this.GetType().Name + ": Unhandled exception running model. "+exception.GetType().FullName+" - "+exception.Message;				
					throw new Exception(msg, exception);
				}
			}

		

			private void CalculateModel(SiriusQualityPhenology.PhenologyState phenologystate,SiriusQualityPhenology.PhenologyState phenologystate1,CRA.AgroManagement.ActEvents actevents)
			{				
				

				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section1
				//Code written below will not be overwritten by a future code generation

                double PTQ = phenologystate.PTQ;
                double GAI = Math.Max(phenologystate1.pastMaxAI, phenologystate.GAI); //Use GAI
                //phenologystate.pastMaxAI = phenologystate1.pastMaxAI = GAI;
                phenologystate.pastMaxAI = GAI;


                if (GAI > 0.0) phenologystate.Phyllochron = PhylPTQ1 * ((GAI * Kl) / (1 - Math.Exp(-Kl * GAI))) / (PTQ + aPTQ);
                else phenologystate.Phyllochron = PhylPTQ1;
        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}

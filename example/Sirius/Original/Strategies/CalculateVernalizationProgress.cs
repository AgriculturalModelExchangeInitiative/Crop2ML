

 //Author:Pierre Martre pierre.martre@supagro.inra.fr
 //Institution:Inra
 //Author of revision: 
 //Date first release:3/29/2018
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
//To make this project compile please add the reference to assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;

namespace SiriusQualityPhenology.Strategies
{

	/// <summary>
	///Class CalculateVernalizationProgress
    /// Calculate progress (VernaProg) towards vernalization, but there is no vernalization below minTvern (default = 0°C) and above maxTvern (default = 17°C). The maximum value of VernaProg is 1. Progress towards full vernalization is a linear function of shoot temperature (soil temperature until leaf # reach MaxLeafSoil and then canopy temperature)
    /// </summary>
	public class CalculateVernalizationProgress : IStrategySiriusQualityPhenology
	{

	#region Constructor

			public CalculateVernalizationProgress()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 0;
				 v1.Description = "initial minimal final leaf number";
				 v1.Id = 0;
				 v1.MaxValue = 25;
				 v1.MinValue = 0;
				 v1.Name = "AMNFLNO";
				 v1.Size = 1;
				 v1.Units = "leaf";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 0;
				 v2.Description = "true if the plant is vernalizable";
				 v2.Id = 0;
				 v2.MaxValue = 1;
				 v2.MinValue = 0;
				 v2.Name = "IsVernalizable";
				 v2.Size = 1;
				 v2.Units = "NA";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 0;
				 v3.Description = "Minimum temperature for vernalization to occur";
				 v3.Id = 0;
				 v3.MaxValue = 60;
				 v3.MinValue = -20;
				 v3.Name = "MinTvern";
				 v3.Size = 1;
				 v3.Units = "°C";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new VarInfo();
				 v4.DefaultValue = 10;
				 v4.Description = "Intermediate temperature for vernalization to occur";
				 v4.Id = 0;
				 v4.MaxValue = 60;
				 v4.MinValue = -20;
				 v4.Name = "IntTvern";
				 v4.Size = 1;
				 v4.Units = "°C";
				 v4.URL = "";
				 v4.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v4.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new VarInfo();
				 v5.DefaultValue = 0;
				 v5.Description = "Response of vernalization rate to temperature";
				 v5.Id = 0;
				 v5.MaxValue = 1;
				 v5.MinValue = 0;
				 v5.Name = "VAI";
				 v5.Size = 1;
				 v5.Units = "1/(d.°C)";
				 v5.URL = "";
				 v5.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v5.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v5);
				VarInfo v6 = new VarInfo();
				 v6.DefaultValue = 0;
				 v6.Description = "Vernalization rate at 0°C";
				 v6.Id = 0;
				 v6.MaxValue = 1;
				 v6.MinValue = 0;
				 v6.Name = "VBEE";
				 v6.Size = 1;
				 v6.Units = "1/d";
				 v6.URL = "";
				 v6.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v6.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v6);
				VarInfo v7 = new VarInfo();
				 v7.DefaultValue = 12;
				 v7.Description = "Threshold daylength below which it does influence vernalization rate";
				 v7.Id = 0;
				 v7.MaxValue = 24;
				 v7.MinValue = 0;
				 v7.Name = "MinDL";
				 v7.Size = 1;
				 v7.Units = "h";
				 v7.URL = "";
				 v7.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v7.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v7);
				VarInfo v8 = new VarInfo();
				 v8.DefaultValue = 12;
				 v8.Description = "Saturating photoperiod above which final leaf number is not influenced by daylength";
				 v8.Id = 0;
				 v8.MaxValue = 24;
				 v8.MinValue = 0;
				 v8.Name = "MaxDL";
				 v8.Size = 1;
				 v8.Units = "h";
				 v8.URL = "";
				 v8.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v8.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v8);
				VarInfo v9 = new VarInfo();
				 v9.DefaultValue = 17;
				 v9.Description = "Maximum temperature for vernalization to occur";
				 v9.Id = 0;
				 v9.MaxValue = 60;
				 v9.MinValue = -20;
				 v9.Name = "MaxTvern";
				 v9.Size = 1;
				 v9.Units = "°C";
				 v9.URL = "";
				 v9.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v9.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v9);
				VarInfo v10 = new VarInfo();
				 v10.DefaultValue = 10;
				 v10.Description = "Number of primorida in the apex at emergence";
				 v10.Id = 0;
				 v10.MaxValue = 24;
				 v10.MinValue = 0;
				 v10.Name = "PNini";
				 v10.Size = 1;
				 v10.Units = "primordia";
				 v10.URL = "";
				 v10.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v10.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v10);
				VarInfo v11 = new VarInfo();
				 v11.DefaultValue = 10;
				 v11.Description = "Absolute maximum leaf number";
				 v11.Id = 0;
				 v11.MaxValue = 25;
				 v11.MinValue = 0;
				 v11.Name = "AMXLFNO";
				 v11.Size = 1;
				 v11.Units = "leaf";
				 v11.URL = "";
				 v11.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v11.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v11);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd1.PropertyName = "DayLength";
				pd1.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.DayLength)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.DayLength);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd2.PropertyName = "DeltaTT";
				pd2.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.DeltaTT)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.DeltaTT);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd3.PropertyName = "cumulTT";
				pd3.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd4.PropertyName = "LeafNumber";
				pd4.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
				_inputs0_0.Add(pd4);
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd5.PropertyName = "Calendar";
				pd5.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.Calendar)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.Calendar);
				_inputs0_0.Add(pd5);
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd6.PropertyName = "Vernaprog";
				pd6.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog);
				_inputs0_0.Add(pd6);
				PropertyDescription pd7 = new PropertyDescription();
				pd7.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd7.PropertyName = "MinFinalNumber";
				pd7.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber)).ValueType.TypeForCurrentValue;
				pd7.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber);
				_inputs0_0.Add(pd7);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd8 = new PropertyDescription();
				pd8.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd8.PropertyName = "Vernaprog";
				pd8.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog)).ValueType.TypeForCurrentValue;
				pd8.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog);
				_outputs0_0.Add(pd8);
				PropertyDescription pd9 = new PropertyDescription();
				pd9.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd9.PropertyName = "MinFinalNumber";
				pd9.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber)).ValueType.TypeForCurrentValue;
				pd9.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber);
				_outputs0_0.Add(pd9);
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
				get { return "Calculate progress (VernaProg) towards vernalization, but there is no vernalization below minTvern (default = 0°C) and above maxTvern (default = 17°C). The maximum value of VernaProg is 1. Progress towards full vernalization is a linear function of shoot temperature (soil temperature until leaf # reach MaxLeafSoil and then canopy temperature)"; }
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
				_pd.Add("Creator", "pierre.martre@supagro.inra.fr");
				_pd.Add("Date", "3/29/2018");
				_pd.Add("Publisher", "Inra");
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

			
			public Double AMNFLNO
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("AMNFLNO");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'AMNFLNO' not found (or found null) in strategy 'CalculateVernalizationProgress'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("AMNFLNO");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'AMNFLNO' not found in strategy 'CalculateVernalizationProgress'");
				}
			}
			public Int32 IsVernalizable
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("IsVernalizable");
						if (vi != null && vi.CurrentValue!=null) return (Int32)vi.CurrentValue ;
						else throw new Exception("Parameter 'IsVernalizable' not found (or found null) in strategy 'CalculateVernalizationProgress'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("IsVernalizable");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'IsVernalizable' not found in strategy 'CalculateVernalizationProgress'");
				}
			}
			public Double MinTvern
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("MinTvern");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'MinTvern' not found (or found null) in strategy 'CalculateVernalizationProgress'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("MinTvern");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'MinTvern' not found in strategy 'CalculateVernalizationProgress'");
				}
			}
			public Double IntTvern
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("IntTvern");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'IntTvern' not found (or found null) in strategy 'CalculateVernalizationProgress'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("IntTvern");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'IntTvern' not found in strategy 'CalculateVernalizationProgress'");
				}
			}
			public Double VAI
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("VAI");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'VAI' not found (or found null) in strategy 'CalculateVernalizationProgress'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("VAI");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'VAI' not found in strategy 'CalculateVernalizationProgress'");
				}
			}
			public Double VBEE
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("VBEE");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'VBEE' not found (or found null) in strategy 'CalculateVernalizationProgress'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("VBEE");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'VBEE' not found in strategy 'CalculateVernalizationProgress'");
				}
			}
			public Double MinDL
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("MinDL");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'MinDL' not found (or found null) in strategy 'CalculateVernalizationProgress'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("MinDL");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'MinDL' not found in strategy 'CalculateVernalizationProgress'");
				}
			}
			public Double MaxDL
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("MaxDL");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'MaxDL' not found (or found null) in strategy 'CalculateVernalizationProgress'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("MaxDL");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'MaxDL' not found in strategy 'CalculateVernalizationProgress'");
				}
			}
			public Double MaxTvern
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("MaxTvern");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'MaxTvern' not found (or found null) in strategy 'CalculateVernalizationProgress'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("MaxTvern");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'MaxTvern' not found in strategy 'CalculateVernalizationProgress'");
				}
			}
			public Double PNini
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PNini");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PNini' not found (or found null) in strategy 'CalculateVernalizationProgress'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PNini");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PNini' not found in strategy 'CalculateVernalizationProgress'");
				}
			}
			public Double AMXLFNO
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("AMXLFNO");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'AMXLFNO' not found (or found null) in strategy 'CalculateVernalizationProgress'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("AMXLFNO");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'AMXLFNO' not found in strategy 'CalculateVernalizationProgress'");
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
                AMNFLNOVarInfo.Name = "AMNFLNO";
				AMNFLNOVarInfo.Description =" initial minimal final leaf number";
				AMNFLNOVarInfo.MaxValue = 25;
				AMNFLNOVarInfo.MinValue = 0;
				AMNFLNOVarInfo.DefaultValue = 0;
				AMNFLNOVarInfo.Units = "leaf";
				AMNFLNOVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				IsVernalizableVarInfo.Name = "IsVernalizable";
				IsVernalizableVarInfo.Description =" true if the plant is vernalizable";
				IsVernalizableVarInfo.MaxValue = 1;
				IsVernalizableVarInfo.MinValue = 0;
				IsVernalizableVarInfo.DefaultValue = 0;
				IsVernalizableVarInfo.Units = "NA";
				IsVernalizableVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Integer");

				MinTvernVarInfo.Name = "MinTvern";
				MinTvernVarInfo.Description =" Minimum temperature for vernalization to occur";
				MinTvernVarInfo.MaxValue = 60;
				MinTvernVarInfo.MinValue = -20;
				MinTvernVarInfo.DefaultValue = 0;
				MinTvernVarInfo.Units = "°C";
				MinTvernVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				IntTvernVarInfo.Name = "IntTvern";
				IntTvernVarInfo.Description =" Intermediate temperature for vernalization to occur";
				IntTvernVarInfo.MaxValue = 60;
				IntTvernVarInfo.MinValue = -20;
				IntTvernVarInfo.DefaultValue = 10;
				IntTvernVarInfo.Units = "°C";
				IntTvernVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				VAIVarInfo.Name = "VAI";
				VAIVarInfo.Description =" Response of vernalization rate to temperature";
				VAIVarInfo.MaxValue = 1;
				VAIVarInfo.MinValue = 0;
				VAIVarInfo.DefaultValue = 0;
				VAIVarInfo.Units = "1/(d.°C)";
				VAIVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				VBEEVarInfo.Name = "VBEE";
				VBEEVarInfo.Description =" Vernalization rate at 0°C";
				VBEEVarInfo.MaxValue = 1;
				VBEEVarInfo.MinValue = 0;
				VBEEVarInfo.DefaultValue = 0;
				VBEEVarInfo.Units = "1/d";
				VBEEVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				MinDLVarInfo.Name = "MinDL";
				MinDLVarInfo.Description =" Threshold daylength below which it does influence vernalization rate";
				MinDLVarInfo.MaxValue = 24;
				MinDLVarInfo.MinValue = 0;
				MinDLVarInfo.DefaultValue = 12;
				MinDLVarInfo.Units = "h";
				MinDLVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				MaxDLVarInfo.Name = "MaxDL";
				MaxDLVarInfo.Description =" Saturating photoperiod above which final leaf number is not influenced by daylength";
				MaxDLVarInfo.MaxValue = 24;
				MaxDLVarInfo.MinValue = 0;
				MaxDLVarInfo.DefaultValue = 12;
				MaxDLVarInfo.Units = "h";
				MaxDLVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				MaxTvernVarInfo.Name = "MaxTvern";
				MaxTvernVarInfo.Description =" Maximum temperature for vernalization to occur";
				MaxTvernVarInfo.MaxValue = 60;
				MaxTvernVarInfo.MinValue = -20;
				MaxTvernVarInfo.DefaultValue = 17;
				MaxTvernVarInfo.Units = "°C";
				MaxTvernVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PNiniVarInfo.Name = "PNini";
				PNiniVarInfo.Description =" Number of primorida in the apex at emergence";
				PNiniVarInfo.MaxValue = 24;
				PNiniVarInfo.MinValue = 0;
				PNiniVarInfo.DefaultValue = 10;
				PNiniVarInfo.Units = "primordia";
				PNiniVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				AMXLFNOVarInfo.Name = "AMXLFNO";
				AMXLFNOVarInfo.Description =" Absolute maximum leaf number";
				AMXLFNOVarInfo.MaxValue = 25;
				AMXLFNOVarInfo.MinValue = 0;
				AMXLFNOVarInfo.DefaultValue = 10;
				AMXLFNOVarInfo.Units = "leaf";
				AMXLFNOVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _AMNFLNOVarInfo= new VarInfo();
				/// <summary> 
				///AMNFLNO VarInfo definition
				/// </summary>
				public static VarInfo AMNFLNOVarInfo
				{
					get { return _AMNFLNOVarInfo; }
				}
				private static VarInfo _IsVernalizableVarInfo= new VarInfo();
				/// <summary> 
				///IsVernalizable VarInfo definition
				/// </summary>
				public static VarInfo IsVernalizableVarInfo
				{
					get { return _IsVernalizableVarInfo; }
				}
				private static VarInfo _MinTvernVarInfo= new VarInfo();
				/// <summary> 
				///MinTvern VarInfo definition
				/// </summary>
				public static VarInfo MinTvernVarInfo
				{
					get { return _MinTvernVarInfo; }
				}
				private static VarInfo _IntTvernVarInfo= new VarInfo();
				/// <summary> 
				///IntTvern VarInfo definition
				/// </summary>
				public static VarInfo IntTvernVarInfo
				{
					get { return _IntTvernVarInfo; }
				}
				private static VarInfo _VAIVarInfo= new VarInfo();
				/// <summary> 
				///VAI VarInfo definition
				/// </summary>
				public static VarInfo VAIVarInfo
				{
					get { return _VAIVarInfo; }
				}
				private static VarInfo _VBEEVarInfo= new VarInfo();
				/// <summary> 
				///VBEE VarInfo definition
				/// </summary>
				public static VarInfo VBEEVarInfo
				{
					get { return _VBEEVarInfo; }
				}
				private static VarInfo _MinDLVarInfo= new VarInfo();
				/// <summary> 
				///MinDL VarInfo definition
				/// </summary>
				public static VarInfo MinDLVarInfo
				{
					get { return _MinDLVarInfo; }
				}
				private static VarInfo _MaxDLVarInfo= new VarInfo();
				/// <summary> 
				///MaxDL VarInfo definition
				/// </summary>
				public static VarInfo MaxDLVarInfo
				{
					get { return _MaxDLVarInfo; }
				}
				private static VarInfo _MaxTvernVarInfo= new VarInfo();
				/// <summary> 
				///MaxTvern VarInfo definition
				/// </summary>
				public static VarInfo MaxTvernVarInfo
				{
					get { return _MaxTvernVarInfo; }
				}
				private static VarInfo _PNiniVarInfo= new VarInfo();
				/// <summary> 
				///PNini VarInfo definition
				/// </summary>
				public static VarInfo PNiniVarInfo
				{
					get { return _PNiniVarInfo; }
				}
				private static VarInfo _AMXLFNOVarInfo= new VarInfo();
				/// <summary> 
				///AMXLFNO VarInfo definition
				/// </summary>
				public static VarInfo AMXLFNOVarInfo
				{
					get { return _AMXLFNOVarInfo; }
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
					
					SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog.CurrentValue=phenologystate.Vernaprog;
					SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber.CurrentValue=phenologystate.MinFinalNumber;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r8 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog);
					if(r8.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog.ValueType)){prc.AddCondition(r8);}
					RangeBasedCondition r9 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber);
					if(r9.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber.ValueType)){prc.AddCondition(r9);}

					

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
					
					SiriusQualityPhenology.PhenologyStateVarInfo.DayLength.CurrentValue=phenologystate.DayLength;
					SiriusQualityPhenology.PhenologyStateVarInfo.DeltaTT.CurrentValue=phenologystate.DeltaTT;
					SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT.CurrentValue=phenologystate.cumulTT;
					SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.CurrentValue=phenologystate.LeafNumber;
					SiriusQualityPhenology.PhenologyStateVarInfo.Calendar.CurrentValue=phenologystate.Calendar;
					SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog.CurrentValue=phenologystate.Vernaprog;
					SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber.CurrentValue=phenologystate.MinFinalNumber;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.DayLength);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.DayLength.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.DeltaTT);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.DeltaTT.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.ValueType)){prc.AddCondition(r4);}
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.Calendar);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.Calendar.ValueType)){prc.AddCondition(r5);}
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog.ValueType)){prc.AddCondition(r6);}
					RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber);
					if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber.ValueType)){prc.AddCondition(r7);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("AMNFLNO")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("IsVernalizable")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("MinTvern")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("IntTvern")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("VAI")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("VBEE")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("MinDL")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("MaxDL")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("MaxTvern")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PNini")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("AMXLFNO")));

					

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
                if (IsVernalizable==1 && phenologystate1.Vernaprog < 1)
                {
                    double TT = phenologystate.DeltaTT; // other sirius versions use previous temperature value

                    if (TT >= MinTvern && TT <= IntTvern)
                    {
                        phenologystate.Vernaprog = phenologystate1.Vernaprog + VAI * TT + VBEE;
                    }
                    else
                    {
                        phenologystate.Vernaprog = phenologystate1.Vernaprog;
                    }
                    //  temperature response modified to allow for constant rate of vernalisation above IntermTvern under short days,
                    //  based on Brooking et al., (2002) FCR 79:21-38 and Allard et al., (2012) JXB 63:847-857
                    //  Here we make the assumption that the vernalising effect of shordays decreases linearly from 1 at minDL (set a 8h) to 0 at maxDL (set at 16h) 
                    if (TT > IntTvern)
                    {
                        double maxVernaProg = VAI * IntTvern + VBEE; // maximum vernalisation rate, for temperature = IntermTvern

                        double DLverna = Math.Max(MinDL, Math.Min(MaxDL, phenologystate.DayLength)); // limits the daylength between maxDL and minDL
                        phenologystate.Vernaprog += Math.Max(0, maxVernaProg * (1 + ((IntTvern - TT) / (MaxTvern - IntTvern)) * ((DLverna - MinDL) / (MaxDL - MinDL))));
                    }


                    double primordno = 2.0 * phenologystate.LeafNumber + PNini;
                    double minLeafNumber = phenologystate1.MinFinalNumber;

                    // First stopping rule.  Vernalisation is completed when VPROG reaches 1
                    // or the number of primordium exeeds the absolute maximal leaf number
                    if (phenologystate.Vernaprog >= 1.0 || primordno >= AMXLFNO)
                    {
                        //EndVernalization(primordno, phenologystate.cumulTT, phenologystate.calendar);
                        phenologystate.MinFinalNumber = Math.Max(primordno, phenologystate1.MinFinalNumber); ;
                        phenologystate.Calendar.Set(GrowthStage.EndVernalisation, phenologystate.currentdate,phenologystate.cumulTT);
                        phenologystate.Vernaprog = Math.Max(1, phenologystate.Vernaprog);
                    }
                    else
                    {
                        double potlfno = AMXLFNO - (AMXLFNO - minLeafNumber) * phenologystate.Vernaprog;

                        // Second stopping rule.  Vernalization is completed when primodia number
                        // exceeds potential number of leaves
                        if (primordno >= potlfno)
                        {
                            //EndVernalization((potlfno + primordno) / 2.0, phenologystate.cumulTT, phenologystate.calendar);
                            phenologystate.MinFinalNumber = Math.Max((potlfno + primordno) / 2.0, phenologystate1.MinFinalNumber); ;
                            phenologystate.Calendar.Set(GrowthStage.EndVernalisation, phenologystate.currentdate, phenologystate.cumulTT);
                            phenologystate.Vernaprog = Math.Max(1, phenologystate.Vernaprog);
                        }
                        else { 
                            phenologystate.MinFinalNumber = phenologystate1.MinFinalNumber; 
                        }
                    }
                }
                else
                {
                    phenologystate.Vernaprog=phenologystate1.Vernaprog;
                    phenologystate.MinFinalNumber = phenologystate1.MinFinalNumber;
                }

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

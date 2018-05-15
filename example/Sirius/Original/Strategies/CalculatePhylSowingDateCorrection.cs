

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
//To make this project compile please add the reference to assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;

namespace SiriusQualityPhenology.Strategies
{

	/// <summary>
	///Class CalculatePhylSowingDateCorrection
    /// Correction of the Phyllochron Varietal parameter according to sowing date
    /// </summary>
	public class CalculatePhylSowingDateCorrection : IStrategySiriusQualityPhenology
	{

	#region Constructor

			public CalculatePhylSowingDateCorrection()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 1;
				 v1.Description = "Day of Year at sowing";
				 v1.Id = 0;
				 v1.MaxValue = 366;
				 v1.MinValue = 1;
				 v1.Name = "SowingDay";
				 v1.Size = 1;
				 v1.Units = "dimensionless";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 0;
				 v2.Description = "Latitude";
				 v2.Id = 0;
				 v2.MaxValue = 90;
				 v2.MinValue = -90;
				 v2.Name = "Latitude";
				 v2.Size = 1;
				 v2.Units = "°";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 1;
				 v3.Description = "Sowing date at which Phyllochrone is maximum in southern hemispher";
				 v3.Id = 0;
				 v3.MaxValue = 366;
				 v3.MinValue = 1;
				 v3.Name = "SDsa_sh";
				 v3.Size = 1;
				 v3.Units = "dimensionless";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new VarInfo();
				 v4.DefaultValue = 120;
				 v4.Description = "Phyllochron (Varietal parameter)";
				 v4.Id = 0;
				 v4.MaxValue = 1000;
				 v4.MinValue = 0;
				 v4.Name = "P";
				 v4.Size = 1;
				 v4.Units = "°Cd/leaf";
				 v4.URL = "";
				 v4.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v4.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new VarInfo();
				 v5.DefaultValue = 1;
				 v5.Description = "Sowing date at which Phyllochrone is minimum";
				 v5.Id = 0;
				 v5.MaxValue = 366;
				 v5.MinValue = 1;
				 v5.Name = "SDws";
				 v5.Size = 1;
				 v5.Units = "dimensionless";
				 v5.URL = "";
				 v5.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v5.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v5);
				VarInfo v6 = new VarInfo();
				 v6.DefaultValue = 1;
				 v6.Description = "Sowing date at which Phyllochrone is maximum in northern hemispher";
				 v6.Id = 0;
				 v6.MaxValue = 366;
				 v6.MinValue = 1;
				 v6.Name = "SDsa_nh";
				 v6.Size = 1;
				 v6.Units = "dimensionless";
				 v6.URL = "";
				 v6.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v6.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v6);
				VarInfo v7 = new VarInfo();
				 v7.DefaultValue = 0;
				 v7.Description = "Rate of change of Phyllochrone with sowing date";
				 v7.Id = 0;
				 v7.MaxValue = 366;
				 v7.MinValue = 0;
				 v7.Name = "Rp";
				 v7.Size = 1;
				 v7.Units = "1/day";
				 v7.URL = "";
				 v7.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v7.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v7);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd1.PropertyName = "Fixphyll";
				pd1.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.Fixphyll)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.Fixphyll);
				_outputs0_0.Add(pd1);
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
				get { return "Correction of the Phyllochron Varietal parameter according to sowing date"; }
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

			
			public Int32 SowingDay
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SowingDay");
						if (vi != null && vi.CurrentValue!=null) return (Int32)vi.CurrentValue ;
						else throw new Exception("Parameter 'SowingDay' not found (or found null) in strategy 'CalculatePhylSowingDateCorrection'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SowingDay");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SowingDay' not found in strategy 'CalculatePhylSowingDateCorrection'");
				}
			}
			public Double Latitude
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Latitude");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Latitude' not found (or found null) in strategy 'CalculatePhylSowingDateCorrection'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Latitude");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Latitude' not found in strategy 'CalculatePhylSowingDateCorrection'");
				}
			}
			public Double SDsa_sh
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SDsa_sh");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'SDsa_sh' not found (or found null) in strategy 'CalculatePhylSowingDateCorrection'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SDsa_sh");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SDsa_sh' not found in strategy 'CalculatePhylSowingDateCorrection'");
				}
			}
			public Double P
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("P");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'P' not found (or found null) in strategy 'CalculatePhylSowingDateCorrection'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("P");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'P' not found in strategy 'CalculatePhylSowingDateCorrection'");
				}
			}
			public Double SDws
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SDws");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'SDws' not found (or found null) in strategy 'CalculatePhylSowingDateCorrection'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SDws");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SDws' not found in strategy 'CalculatePhylSowingDateCorrection'");
				}
			}
			public Double SDsa_nh
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SDsa_nh");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'SDsa_nh' not found (or found null) in strategy 'CalculatePhylSowingDateCorrection'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SDsa_nh");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SDsa_nh' not found in strategy 'CalculatePhylSowingDateCorrection'");
				}
			}
			public Double Rp
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Rp");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Rp' not found (or found null) in strategy 'CalculatePhylSowingDateCorrection'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Rp");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Rp' not found in strategy 'CalculatePhylSowingDateCorrection'");
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
                SowingDayVarInfo.Name = "SowingDay";
				SowingDayVarInfo.Description =" Day of Year at sowing";
				SowingDayVarInfo.MaxValue = 366;
				SowingDayVarInfo.MinValue = 1;
				SowingDayVarInfo.DefaultValue = 1;
				SowingDayVarInfo.Units = "dimensionless";
				SowingDayVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Integer");

				LatitudeVarInfo.Name = "Latitude";
				LatitudeVarInfo.Description =" Latitude";
				LatitudeVarInfo.MaxValue = 90;
				LatitudeVarInfo.MinValue = -90;
				LatitudeVarInfo.DefaultValue = 0;
				LatitudeVarInfo.Units = "°";
				LatitudeVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				SDsa_shVarInfo.Name = "SDsa_sh";
				SDsa_shVarInfo.Description =" Sowing date at which Phyllochrone is maximum in southern hemispher";
				SDsa_shVarInfo.MaxValue = 366;
				SDsa_shVarInfo.MinValue = 1;
				SDsa_shVarInfo.DefaultValue = 1;
				SDsa_shVarInfo.Units = "dimensionless";
				SDsa_shVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PVarInfo.Name = "P";
				PVarInfo.Description =" Phyllochron (Varietal parameter)";
				PVarInfo.MaxValue = 1000;
				PVarInfo.MinValue = 0;
				PVarInfo.DefaultValue = 120;
				PVarInfo.Units = "°Cd/leaf";
				PVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				SDwsVarInfo.Name = "SDws";
				SDwsVarInfo.Description =" Sowing date at which Phyllochrone is minimum";
				SDwsVarInfo.MaxValue = 366;
				SDwsVarInfo.MinValue = 1;
				SDwsVarInfo.DefaultValue = 1;
				SDwsVarInfo.Units = "dimensionless";
				SDwsVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				SDsa_nhVarInfo.Name = "SDsa_nh";
				SDsa_nhVarInfo.Description =" Sowing date at which Phyllochrone is maximum in northern hemispher";
				SDsa_nhVarInfo.MaxValue = 366;
				SDsa_nhVarInfo.MinValue = 1;
				SDsa_nhVarInfo.DefaultValue = 1;
				SDsa_nhVarInfo.Units = "dimensionless";
				SDsa_nhVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				RpVarInfo.Name = "Rp";
				RpVarInfo.Description =" Rate of change of Phyllochrone with sowing date";
				RpVarInfo.MaxValue = 366;
				RpVarInfo.MinValue = 0;
				RpVarInfo.DefaultValue = 0;
				RpVarInfo.Units = "1/day";
				RpVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _SowingDayVarInfo= new VarInfo();
				/// <summary> 
				///SowingDay VarInfo definition
				/// </summary>
				public static VarInfo SowingDayVarInfo
				{
					get { return _SowingDayVarInfo; }
				}
				private static VarInfo _LatitudeVarInfo= new VarInfo();
				/// <summary> 
				///Latitude VarInfo definition
				/// </summary>
				public static VarInfo LatitudeVarInfo
				{
					get { return _LatitudeVarInfo; }
				}
				private static VarInfo _SDsa_shVarInfo= new VarInfo();
				/// <summary> 
				///SDsa_sh VarInfo definition
				/// </summary>
				public static VarInfo SDsa_shVarInfo
				{
					get { return _SDsa_shVarInfo; }
				}
				private static VarInfo _PVarInfo= new VarInfo();
				/// <summary> 
				///P VarInfo definition
				/// </summary>
				public static VarInfo PVarInfo
				{
					get { return _PVarInfo; }
				}
				private static VarInfo _SDwsVarInfo= new VarInfo();
				/// <summary> 
				///SDws VarInfo definition
				/// </summary>
				public static VarInfo SDwsVarInfo
				{
					get { return _SDwsVarInfo; }
				}
				private static VarInfo _SDsa_nhVarInfo= new VarInfo();
				/// <summary> 
				///SDsa_nh VarInfo definition
				/// </summary>
				public static VarInfo SDsa_nhVarInfo
				{
					get { return _SDsa_nhVarInfo; }
				}
				private static VarInfo _RpVarInfo= new VarInfo();
				/// <summary> 
				///Rp VarInfo definition
				/// </summary>
				public static VarInfo RpVarInfo
				{
					get { return _RpVarInfo; }
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
					
					SiriusQualityPhenology.PhenologyStateVarInfo.Fixphyll.CurrentValue=phenologystate.Fixphyll;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.Fixphyll);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.Fixphyll.ValueType)){prc.AddCondition(r1);}

					

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
					

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SowingDay")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Latitude")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SDsa_sh")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("P")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SDws")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SDsa_nh")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Rp")));

					

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

                phenologystate.Fixphyll = CalculateFixphyll();

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation

            public double CalculateFixphyll()
            {
                if (Latitude < 0)
                {
                    if (SowingDay > SDsa_sh)
                    {
                        return P * (1 - Rp * Math.Min(SowingDay - SDsa_sh, SDws));
                    }
                    else return P;
                }
                else
                {
                    if (SowingDay < SDsa_nh)
                    {
                        return P * (1 - Rp * Math.Min(SowingDay, SDws));
                    }
                    else return P;
                }
            }


				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}

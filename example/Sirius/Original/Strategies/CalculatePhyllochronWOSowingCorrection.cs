

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
	///Class CalculatePhyllochronWOSowingCorrection
    /// Calculate Phyllochron without sowing date correction
    /// </summary>
	public class CalculatePhyllochronWOSowingCorrection : IStrategySiriusQualityPhenology
	{

	#region Constructor

			public CalculatePhyllochronWOSowingCorrection()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 3;
				 v1.Description = "Leaf number up to which the phyllochron is decreased by Pdecr";
				 v1.Id = 0;
				 v1.MaxValue = 30;
				 v1.MinValue = 0;
				 v1.Name = "Ldecr";
				 v1.Size = 1;
				 v1.Units = "leaf";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 8;
				 v2.Description = "Leaf number above which the phyllochron is increased by Pincr";
				 v2.Id = 0;
				 v2.MaxValue = 30;
				 v2.MinValue = 0;
				 v2.Name = "Lincr";
				 v2.Size = 1;
				 v2.Units = "leaf";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 0.4;
				 v3.Description = "Factor decreasing the phyllochron for leaf number less than Ldecr";
				 v3.Id = 0;
				 v3.MaxValue = 10;
				 v3.MinValue = 0;
				 v3.Name = "Pdecr";
				 v3.Size = 1;
				 v3.Units = "dimensionless";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new VarInfo();
				 v4.DefaultValue = 1.5;
				 v4.Description = "Factor increasing the phyllochron for leaf number higher than Lincr";
				 v4.Id = 0;
				 v4.MaxValue = 10;
				 v4.MinValue = 0;
				 v4.Name = "Pincr";
				 v4.Size = 1;
				 v4.Units = "dimensionless";
				 v4.URL = "";
				 v4.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v4.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new VarInfo();
				 v5.DefaultValue = 120;
				 v5.Description = "Phyllochron (Varietal parameter)";
				 v5.Id = 0;
				 v5.MaxValue = 1000;
				 v5.MinValue = 0;
				 v5.Name = "P";
				 v5.Size = 1;
				 v5.Units = "°Cd/leaf";
				 v5.URL = "";
				 v5.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v5.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v5);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd1.PropertyName = "LeafNumber";
				pd1.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
				_inputs0_0.Add(pd1);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd2.PropertyName = "Phyllochron";
				pd2.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron);
				_outputs0_0.Add(pd2);
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
				get { return "Calculate Phyllochron without sowing date correction"; }
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

			
			public Double Ldecr
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Ldecr");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Ldecr' not found (or found null) in strategy 'CalculatePhyllochronWOSowingCorrection'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Ldecr");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Ldecr' not found in strategy 'CalculatePhyllochronWOSowingCorrection'");
				}
			}
			public Double Lincr
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Lincr");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Lincr' not found (or found null) in strategy 'CalculatePhyllochronWOSowingCorrection'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Lincr");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Lincr' not found in strategy 'CalculatePhyllochronWOSowingCorrection'");
				}
			}
			public Double Pdecr
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Pdecr");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Pdecr' not found (or found null) in strategy 'CalculatePhyllochronWOSowingCorrection'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Pdecr");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Pdecr' not found in strategy 'CalculatePhyllochronWOSowingCorrection'");
				}
			}
			public Double Pincr
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Pincr");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Pincr' not found (or found null) in strategy 'CalculatePhyllochronWOSowingCorrection'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Pincr");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Pincr' not found in strategy 'CalculatePhyllochronWOSowingCorrection'");
				}
			}
			public Double P
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("P");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'P' not found (or found null) in strategy 'CalculatePhyllochronWOSowingCorrection'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("P");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'P' not found in strategy 'CalculatePhyllochronWOSowingCorrection'");
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
                LdecrVarInfo.Name = "Ldecr";
				LdecrVarInfo.Description =" Leaf number up to which the phyllochron is decreased by Pdecr";
				LdecrVarInfo.MaxValue = 30;
				LdecrVarInfo.MinValue = 0;
				LdecrVarInfo.DefaultValue = 3;
				LdecrVarInfo.Units = "leaf";
				LdecrVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				LincrVarInfo.Name = "Lincr";
				LincrVarInfo.Description =" Leaf number above which the phyllochron is increased by Pincr";
				LincrVarInfo.MaxValue = 30;
				LincrVarInfo.MinValue = 0;
				LincrVarInfo.DefaultValue = 8;
				LincrVarInfo.Units = "leaf";
				LincrVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PdecrVarInfo.Name = "Pdecr";
				PdecrVarInfo.Description =" Factor decreasing the phyllochron for leaf number less than Ldecr";
				PdecrVarInfo.MaxValue = 10;
				PdecrVarInfo.MinValue = 0;
				PdecrVarInfo.DefaultValue = 0.4;
				PdecrVarInfo.Units = "dimensionless";
				PdecrVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PincrVarInfo.Name = "Pincr";
				PincrVarInfo.Description =" Factor increasing the phyllochron for leaf number higher than Lincr";
				PincrVarInfo.MaxValue = 10;
				PincrVarInfo.MinValue = 0;
				PincrVarInfo.DefaultValue = 1.5;
				PincrVarInfo.Units = "dimensionless";
				PincrVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PVarInfo.Name = "P";
				PVarInfo.Description =" Phyllochron (Varietal parameter)";
				PVarInfo.MaxValue = 1000;
				PVarInfo.MinValue = 0;
				PVarInfo.DefaultValue = 120;
				PVarInfo.Units = "°Cd/leaf";
				PVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _LdecrVarInfo= new VarInfo();
				/// <summary> 
				///Ldecr VarInfo definition
				/// </summary>
				public static VarInfo LdecrVarInfo
				{
					get { return _LdecrVarInfo; }
				}
				private static VarInfo _LincrVarInfo= new VarInfo();
				/// <summary> 
				///Lincr VarInfo definition
				/// </summary>
				public static VarInfo LincrVarInfo
				{
					get { return _LincrVarInfo; }
				}
				private static VarInfo _PdecrVarInfo= new VarInfo();
				/// <summary> 
				///Pdecr VarInfo definition
				/// </summary>
				public static VarInfo PdecrVarInfo
				{
					get { return _PdecrVarInfo; }
				}
				private static VarInfo _PincrVarInfo= new VarInfo();
				/// <summary> 
				///Pincr VarInfo definition
				/// </summary>
				public static VarInfo PincrVarInfo
				{
					get { return _PincrVarInfo; }
				}
				private static VarInfo _PVarInfo= new VarInfo();
				/// <summary> 
				///P VarInfo definition
				/// </summary>
				public static VarInfo PVarInfo
				{
					get { return _PVarInfo; }
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
					
					SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron.CurrentValue=phenologystate.Phyllochron;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron.ValueType)){prc.AddCondition(r2);}

					

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
					
					SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.CurrentValue=phenologystate.LeafNumber;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.ValueType)){prc.AddCondition(r1);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Ldecr")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Lincr")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Pdecr")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Pincr")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("P")));

					

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

                if (phenologystate.LeafNumber < Ldecr) phenologystate.Phyllochron = P * Pdecr;
                else if (phenologystate.LeafNumber >= Ldecr && phenologystate.LeafNumber < Lincr) phenologystate.Phyllochron = P;
                else phenologystate.Phyllochron = P * Pincr;
        

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

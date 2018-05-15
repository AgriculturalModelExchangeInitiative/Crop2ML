

 //Author:pierre Martre pierre.martre@supagro.inra.fr
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
	///Class RegisterZadok
    /// record the zadok stage in the calendar
    /// </summary>
	public class RegisterZadok : IStrategySiriusQualityPhenology
	{

	#region Constructor

			public RegisterZadok()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 0;
				 v1.Description = "Duration of the endosperm endoreduplication phase";
				 v1.Id = 0;
				 v1.MaxValue = 10000;
				 v1.MinValue = 0;
				 v1.Name = "Der";
				 v1.Size = 1;
				 v1.Units = "°Cd";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 0;
				 v2.Description = "used for the calcul of Terminal spikelet";
				 v2.Id = 0;
				 v2.MaxValue = 10000;
				 v2.MinValue = 0;
				 v2.Name = "slopeTSFLN";
				 v2.Size = 1;
				 v2.Units = "-";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 0;
				 v3.Description = "used for the calcul of Terminal spikelet";
				 v3.Id = 0;
				 v3.MaxValue = 10000;
				 v3.MinValue = 0;
				 v3.Name = "intTSFLN";
				 v3.Size = 1;
				 v3.Units = "-";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd1.PropertyName = "cumulTT";
				pd1.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd2.PropertyName = "phase_";
				pd2.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.phase_)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.phase_);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd3.PropertyName = "LeafNumber";
				pd3.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd4.PropertyName = "cumulTTFromZC_65";
				pd4.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_65)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_65);
				_inputs0_0.Add(pd4);
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd5.PropertyName = "currentdate";
				pd5.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.currentdate)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.currentdate);
				_inputs0_0.Add(pd5);
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd6.PropertyName = "Calendar";
				pd6.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.Calendar)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.Calendar);
				_inputs0_0.Add(pd6);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd7 = new PropertyDescription();
				pd7.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd7.PropertyName = "hasZadokStageChanged";
				pd7.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.hasZadokStageChanged)).ValueType.TypeForCurrentValue;
				pd7.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.hasZadokStageChanged);
				_outputs0_0.Add(pd7);
				PropertyDescription pd8 = new PropertyDescription();
				pd8.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd8.PropertyName = "currentZadokStage";
				pd8.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.currentZadokStage)).ValueType.TypeForCurrentValue;
				pd8.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.currentZadokStage);
				_outputs0_0.Add(pd8);
				PropertyDescription pd9 = new PropertyDescription();
				pd9.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd9.PropertyName = "Calendar";
				pd9.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.Calendar)).ValueType.TypeForCurrentValue;
				pd9.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.Calendar);
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
				get { return "record the zadok stage in the calendar"; }
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

			
			public Double Der
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Der");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Der' not found (or found null) in strategy 'RegisterZadok'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Der");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Der' not found in strategy 'RegisterZadok'");
				}
			}
			public Double slopeTSFLN
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("slopeTSFLN");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'slopeTSFLN' not found (or found null) in strategy 'RegisterZadok'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("slopeTSFLN");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'slopeTSFLN' not found in strategy 'RegisterZadok'");
				}
			}
			public Double intTSFLN
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("intTSFLN");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'intTSFLN' not found (or found null) in strategy 'RegisterZadok'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("intTSFLN");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'intTSFLN' not found in strategy 'RegisterZadok'");
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
                DerVarInfo.Name = "Der";
				DerVarInfo.Description =" Duration of the endosperm endoreduplication phase";
				DerVarInfo.MaxValue = 10000;
				DerVarInfo.MinValue = 0;
				DerVarInfo.DefaultValue = 0;
				DerVarInfo.Units = "°Cd";
				DerVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				slopeTSFLNVarInfo.Name = "slopeTSFLN";
				slopeTSFLNVarInfo.Description =" used for the calcul of Terminal spikelet";
				slopeTSFLNVarInfo.MaxValue = 10000;
				slopeTSFLNVarInfo.MinValue = 0;
				slopeTSFLNVarInfo.DefaultValue = 0;
				slopeTSFLNVarInfo.Units = "-";
				slopeTSFLNVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				intTSFLNVarInfo.Name = "intTSFLN";
				intTSFLNVarInfo.Description =" used for the calcul of Terminal spikelet";
				intTSFLNVarInfo.MaxValue = 10000;
				intTSFLNVarInfo.MinValue = 0;
				intTSFLNVarInfo.DefaultValue = 0;
				intTSFLNVarInfo.Units = "-";
				intTSFLNVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _DerVarInfo= new VarInfo();
				/// <summary> 
				///Der VarInfo definition
				/// </summary>
				public static VarInfo DerVarInfo
				{
					get { return _DerVarInfo; }
				}
				private static VarInfo _slopeTSFLNVarInfo= new VarInfo();
				/// <summary> 
				///slopeTSFLN VarInfo definition
				/// </summary>
				public static VarInfo slopeTSFLNVarInfo
				{
					get { return _slopeTSFLNVarInfo; }
				}
				private static VarInfo _intTSFLNVarInfo= new VarInfo();
				/// <summary> 
				///intTSFLN VarInfo definition
				/// </summary>
				public static VarInfo intTSFLNVarInfo
				{
					get { return _intTSFLNVarInfo; }
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
					
					SiriusQualityPhenology.PhenologyStateVarInfo.hasZadokStageChanged.CurrentValue=phenologystate.hasZadokStageChanged;
					SiriusQualityPhenology.PhenologyStateVarInfo.currentZadokStage.CurrentValue=phenologystate.currentZadokStage;
					SiriusQualityPhenology.PhenologyStateVarInfo.Calendar.CurrentValue=phenologystate.Calendar;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.hasZadokStageChanged);
					if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.hasZadokStageChanged.ValueType)){prc.AddCondition(r7);}
					RangeBasedCondition r8 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.currentZadokStage);
					if(r8.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.currentZadokStage.ValueType)){prc.AddCondition(r8);}
					RangeBasedCondition r9 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.Calendar);
					if(r9.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.Calendar.ValueType)){prc.AddCondition(r9);}

					

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
					
					SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT.CurrentValue=phenologystate.cumulTT;
					SiriusQualityPhenology.PhenologyStateVarInfo.phase_.CurrentValue=phenologystate.phase_;
					SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.CurrentValue=phenologystate.LeafNumber;
					SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_65.CurrentValue=phenologystate.cumulTTFromZC_65;
					SiriusQualityPhenology.PhenologyStateVarInfo.currentdate.CurrentValue=phenologystate.currentdate;
					SiriusQualityPhenology.PhenologyStateVarInfo.Calendar.CurrentValue=phenologystate.Calendar;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.phase_);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.phase_.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_65);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_65.ValueType)){prc.AddCondition(r4);}
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.currentdate);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.currentdate.ValueType)){prc.AddCondition(r5);}
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.Calendar);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.Calendar.ValueType)){prc.AddCondition(r6);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Der")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("slopeTSFLN")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("intTSFLN")));

					

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

                int roundedFinalLeafNumber = (int)(phenologystate.FinalLeafNumber + 0.5);

                if (HasReachedHaun(4, phenologystate.LeafNumber) && !(phenologystate1.Calendar[GrowthStage.ZC_21_MainShootPlus1Tiller].HasValue))
                {
                    phenologystate.Calendar.Set(GrowthStage.ZC_21_MainShootPlus1Tiller, phenologystate.currentdate, phenologystate.cumulTT);
                    phenologystate.currentZadokStage = GrowthStage.ZC_21_MainShootPlus1Tiller;
                    phenologystate.hasZadokStageChanged = 1;
                }
                else if (HasReachedHaun(5, phenologystate.LeafNumber) && !(phenologystate1.Calendar[GrowthStage.ZC_22_MainShootPlus2Tiller].HasValue))
                {
                    phenologystate.Calendar.Set(GrowthStage.ZC_22_MainShootPlus2Tiller, phenologystate.currentdate, phenologystate.cumulTT);
                    phenologystate.currentZadokStage = GrowthStage.ZC_22_MainShootPlus2Tiller;
                    phenologystate.hasZadokStageChanged = 1;
                }
                else if (HasReachedHaun(6, phenologystate.LeafNumber) && !(phenologystate1.Calendar[GrowthStage.ZC_23_MainShootPlus3Tiller].HasValue))
                {
                    phenologystate.Calendar.Set(GrowthStage.ZC_23_MainShootPlus3Tiller, phenologystate.currentdate, phenologystate.cumulTT);
                    phenologystate.currentZadokStage = GrowthStage.ZC_23_MainShootPlus3Tiller;
                    phenologystate.hasZadokStageChanged = 1;
                }
                else if (phenologystate.FinalLeafNumber > 0 && HasReachedHaun(slopeTSFLN * phenologystate.FinalLeafNumber - intTSFLN, phenologystate.LeafNumber) && !(phenologystate1.Calendar[GrowthStage.TerminalSpikelet].HasValue))
                {
                    phenologystate.Calendar.Set(GrowthStage.TerminalSpikelet, phenologystate.currentdate, phenologystate.cumulTT);
                    phenologystate.currentZadokStage = GrowthStage.TerminalSpikelet;
                    phenologystate.hasZadokStageChanged = 1;
                }
                else if (HasReachedFlagLeaf(4, roundedFinalLeafNumber, phenologystate.LeafNumber) && !(phenologystate1.Calendar[GrowthStage.ZC_30_PseudoStemErection].HasValue))
                {
                    phenologystate.Calendar.Set(GrowthStage.ZC_30_PseudoStemErection, phenologystate.currentdate, phenologystate.cumulTT);
                    phenologystate.currentZadokStage = GrowthStage.ZC_30_PseudoStemErection;
                    phenologystate.hasZadokStageChanged = 1;
                }
                else if (HasReachedFlagLeaf(3, roundedFinalLeafNumber, phenologystate.LeafNumber) && !(phenologystate1.Calendar[GrowthStage.ZC_31_1stNodeDetectable].HasValue))
                {
                    phenologystate.Calendar.Set(GrowthStage.ZC_31_1stNodeDetectable, phenologystate.currentdate, phenologystate.cumulTT);
                    phenologystate.currentZadokStage = GrowthStage.ZC_31_1stNodeDetectable;
                    phenologystate.hasZadokStageChanged = 1;
                }
                else if (HasReachedFlagLeaf(2, roundedFinalLeafNumber, phenologystate.LeafNumber) && !(phenologystate1.Calendar[GrowthStage.ZC_32_2ndNodeDetectable].HasValue))
                {
                    phenologystate.Calendar.Set(GrowthStage.ZC_32_2ndNodeDetectable, phenologystate.currentdate, phenologystate.cumulTT);
                    phenologystate.currentZadokStage = GrowthStage.ZC_32_2ndNodeDetectable;
                    phenologystate.hasZadokStageChanged = 1;
                }
                else if (HasReachedFlagLeaf(1, roundedFinalLeafNumber, phenologystate.LeafNumber) && !(phenologystate1.Calendar[GrowthStage.ZC_37_FlagLeafJustVisible].HasValue))
                {
                    phenologystate.Calendar.Set(GrowthStage.ZC_37_FlagLeafJustVisible, phenologystate.currentdate, phenologystate.cumulTT);
                    phenologystate.currentZadokStage = GrowthStage.ZC_37_FlagLeafJustVisible;
                    phenologystate.hasZadokStageChanged = 1;
                }
                else if (HasReachedFlagLeaf(0, roundedFinalLeafNumber, phenologystate.LeafNumber) && !(phenologystate1.Calendar[GrowthStage.ZC_39_FlagLeafLiguleJustVisible].HasValue))
                {
                }
                else if ((!(phenologystate1.Calendar[GrowthStage.ZC_85_MidGrainFilling].HasValue))
                    && phenologystate.phase_.phaseValue == 4.5//EndCellDivisionToEndGrainFill
                    && phenologystate.cumulTTFromZC_65 >= Der)
                {
                    phenologystate.Calendar.Set(GrowthStage.ZC_85_MidGrainFilling, phenologystate.currentdate, phenologystate.cumulTT);
                    phenologystate.currentZadokStage = GrowthStage.ZC_85_MidGrainFilling;
                    phenologystate.hasZadokStageChanged = 1;
                }
                else
                {
                    phenologystate.hasZadokStageChanged = 0;
                    phenologystate.Calendar = phenologystate1.Calendar;
                }
        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
                ///<summary>Check if the crop has reached the stage "flag leaf ligule just visible"</summary>
                ///<param name="stg"> the flag leaf</param>
                ///<returns>return true if the crop has reached the stage "flag leaf ligule just visible", false if not</returns>
                public bool HasReachedFlagLeaf(int stg, int roundedFinalLeafNumber, double leafNumber)
                {
                    double trgt = roundedFinalLeafNumber - (double)stg;
                    return (leafNumber >= trgt && trgt > 0);
                }

                ///<summary>Check if the crop has reached a specified Haun stage</summary>
                ///<param name="stg">Haun stage to check for</param>
                ///<returns>return true if the crop has reached the specified Haun stage, false if not</returns>
                private bool HasReachedHaun(double stg, double leafNumber)
                {
                    return (leafNumber >= stg);
                }
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}

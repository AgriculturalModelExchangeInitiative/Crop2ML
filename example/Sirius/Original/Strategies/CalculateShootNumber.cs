

 //Author:pierre martre pierre.martre@supagro.inra.fr
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
//To make this project compile please add the reference to assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;

namespace SiriusQualityPhenology.Strategies
{

	/// <summary>
	///Class CalculateShootNumber
    /// calculate the shoot number and update the related variables if needed
    /// </summary>
	public class CalculateShootNumber : IStrategySiriusQualityPhenology
	{

	#region Constructor

			public CalculateShootNumber()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 280;
				 v1.Description = "number of plant /m²";
				 v1.Id = 0;
				 v1.MaxValue = 500;
				 v1.MinValue = 0;
				 v1.Name = "SowingDensity";
				 v1.Size = 1;
				 v1.Units = "plant/m²";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 600;
				 v2.Description = "max value of shoot number for the canopy";
				 v2.Id = 0;
				 v2.MaxValue = 1000;
				 v2.MinValue = 280;
				 v2.Name = "TargetFertileShoot";
				 v2.Size = 1;
				 v2.Units = "shoot";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd1.PropertyName = "LeafNumber";
				pd1.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd2.PropertyName = "CanopyShootNumber";
				pd2.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.CanopyShootNumber)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.CanopyShootNumber);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd3.PropertyName = "tilleringProfile";
				pd3.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.tilleringProfile)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.tilleringProfile);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd4.PropertyName = "leafTillerNumberArray";
				pd4.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.leafTillerNumberArray)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.leafTillerNumberArray);
				_inputs0_0.Add(pd4);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd5.PropertyName = "AverageShootNumberPerPlant";
				pd5.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.AverageShootNumberPerPlant)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.AverageShootNumberPerPlant);
				_outputs0_0.Add(pd5);
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd6.PropertyName = "CanopyShootNumber";
				pd6.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.CanopyShootNumber)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.CanopyShootNumber);
				_outputs0_0.Add(pd6);
				PropertyDescription pd7 = new PropertyDescription();
				pd7.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd7.PropertyName = "leafTillerNumberArray";
				pd7.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.leafTillerNumberArray)).ValueType.TypeForCurrentValue;
				pd7.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.leafTillerNumberArray);
				_outputs0_0.Add(pd7);
				PropertyDescription pd8 = new PropertyDescription();
				pd8.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd8.PropertyName = "tilleringProfile";
				pd8.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.tilleringProfile)).ValueType.TypeForCurrentValue;
				pd8.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.tilleringProfile);
				_outputs0_0.Add(pd8);
				PropertyDescription pd9 = new PropertyDescription();
				pd9.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd9.PropertyName = "TillerNumber";
				pd9.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.TillerNumber)).ValueType.TypeForCurrentValue;
				pd9.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.TillerNumber);
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
				get { return "calculate the shoot number and update the related variables if needed"; }
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

			
			public Double SowingDensity
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SowingDensity");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'SowingDensity' not found (or found null) in strategy 'CalculateShootNumber'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SowingDensity");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SowingDensity' not found in strategy 'CalculateShootNumber'");
				}
			}
			public Double TargetFertileShoot
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("TargetFertileShoot");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'TargetFertileShoot' not found (or found null) in strategy 'CalculateShootNumber'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("TargetFertileShoot");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'TargetFertileShoot' not found in strategy 'CalculateShootNumber'");
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
                SowingDensityVarInfo.Name = "SowingDensity";
				SowingDensityVarInfo.Description =" number of plant /m²";
				SowingDensityVarInfo.MaxValue = 500;
				SowingDensityVarInfo.MinValue = 0;
				SowingDensityVarInfo.DefaultValue = 280;
				SowingDensityVarInfo.Units = "plant/m²";
				SowingDensityVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				TargetFertileShootVarInfo.Name = "TargetFertileShoot";
				TargetFertileShootVarInfo.Description =" max value of shoot number for the canopy";
				TargetFertileShootVarInfo.MaxValue = 1000;
				TargetFertileShootVarInfo.MinValue = 280;
				TargetFertileShootVarInfo.DefaultValue = 600;
				TargetFertileShootVarInfo.Units = "shoot";
				TargetFertileShootVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _SowingDensityVarInfo= new VarInfo();
				/// <summary> 
				///SowingDensity VarInfo definition
				/// </summary>
				public static VarInfo SowingDensityVarInfo
				{
					get { return _SowingDensityVarInfo; }
				}
				private static VarInfo _TargetFertileShootVarInfo= new VarInfo();
				/// <summary> 
				///TargetFertileShoot VarInfo definition
				/// </summary>
				public static VarInfo TargetFertileShootVarInfo
				{
					get { return _TargetFertileShootVarInfo; }
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
					
					SiriusQualityPhenology.PhenologyStateVarInfo.AverageShootNumberPerPlant.CurrentValue=phenologystate.AverageShootNumberPerPlant;
					SiriusQualityPhenology.PhenologyStateVarInfo.CanopyShootNumber.CurrentValue=phenologystate.CanopyShootNumber;
					SiriusQualityPhenology.PhenologyStateVarInfo.leafTillerNumberArray.CurrentValue=phenologystate.leafTillerNumberArray;
					SiriusQualityPhenology.PhenologyStateVarInfo.tilleringProfile.CurrentValue=phenologystate.tilleringProfile;
					SiriusQualityPhenology.PhenologyStateVarInfo.TillerNumber.CurrentValue=phenologystate.TillerNumber;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.AverageShootNumberPerPlant);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.AverageShootNumberPerPlant.ValueType)){prc.AddCondition(r5);}
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.CanopyShootNumber);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.CanopyShootNumber.ValueType)){prc.AddCondition(r6);}
					RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.leafTillerNumberArray);
					if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.leafTillerNumberArray.ValueType)){prc.AddCondition(r7);}
					RangeBasedCondition r8 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.tilleringProfile);
					if(r8.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.tilleringProfile.ValueType)){prc.AddCondition(r8);}
					RangeBasedCondition r9 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.TillerNumber);
					if(r9.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.TillerNumber.ValueType)){prc.AddCondition(r9);}

					

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
					SiriusQualityPhenology.PhenologyStateVarInfo.CanopyShootNumber.CurrentValue=phenologystate.CanopyShootNumber;
					SiriusQualityPhenology.PhenologyStateVarInfo.tilleringProfile.CurrentValue=phenologystate.tilleringProfile;
					SiriusQualityPhenology.PhenologyStateVarInfo.leafTillerNumberArray.CurrentValue=phenologystate.leafTillerNumberArray;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.CanopyShootNumber);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.CanopyShootNumber.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.tilleringProfile);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.tilleringProfile.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.leafTillerNumberArray);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.leafTillerNumberArray.ValueType)){prc.AddCondition(r4);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SowingDensity")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("TargetFertileShoot")));

					

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
                var OldCanopyShootNumber = phenologystate1.CanopyShootNumber;
                CalcShootNumber(phenologystate);

                if (phenologystate.CanopyShootNumber != OldCanopyShootNumber)
                {
                    // New tiller has emerged.
                    phenologystate1.tilleringProfile.Add(phenologystate.CanopyShootNumber - OldCanopyShootNumber);

                }

                phenologystate.tilleringProfile = new List<double>();
                for (int i = 0; i < phenologystate1.tilleringProfile.Count;i++ ) phenologystate.tilleringProfile.Add(phenologystate1.tilleringProfile[i]);

                phenologystate.TillerNumber = phenologystate.tilleringProfile.Count;


                for (int i = phenologystate1.leafTillerNumberArray.Count; i < Math.Ceiling(phenologystate.LeafNumber); i++)
                {
                    phenologystate1.leafTillerNumberArray.Add(phenologystate.TillerNumber);
                    
                }


                phenologystate.leafTillerNumberArray = new List<double>();
                for (int i = 0; i < phenologystate1.leafTillerNumberArray.Count; i++) phenologystate.leafTillerNumberArray.Add(phenologystate1.leafTillerNumberArray[i]);
        
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
                ///<summary>Init this phytomers for a new simulation</summary>
            public void Init(SiriusQualityPhenology.PhenologyState phenologystate1)
                {

                    phenologystate1.CanopyShootNumber = SowingDensity;
                    phenologystate1.AverageShootNumberPerPlant = 1;
                    phenologystate1.tilleringProfile.Add(SowingDensity);
                    phenologystate1.TillerNumber = 1;
      
                }

                private void CalcShootNumber(SiriusQualityPhenology.PhenologyState phenologystate)
                {
                    int EmergedLeaves = (int)Math.Max(1, Math.Ceiling(phenologystate.LeafNumber - 1));
                    int Shoots = Fibonacci(EmergedLeaves);
                    phenologystate.CanopyShootNumber = Math.Min(Shoots * SowingDensity, TargetFertileShoot);
                    phenologystate.AverageShootNumberPerPlant = phenologystate.CanopyShootNumber / SowingDensity;
                }

                private int Fibonacci(int N)
                {
                    int a = 0;
                    int b = 1;
                    for (int i = 0; i < N; i++)
                    {
                        int temp = a;
                        a = b;
                        b = temp + b;
                    }
                    return a;
                }

                public static double ShiftTiller(int tillerIndex)
                {
                    // Behnam (2016.02.12): Following SQ manual.
                    // Because it must be based on tiller order, not tiller index which starts at 0;
                    switch (tillerIndex + 1)
                    {

                        case 1: return 0;
                        case 2: return 0.35;
                        case 3: return 0.75;
                        case 4: return 0.75;
                        case 5: return 0.75;
                        default: return 1.0;
                    }
                }
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}



 //Author:pierre martre pierre.martre@supagro.inra.fr
 //Institution:INRA
 //Author of revision: 
 //Date first release:12/8/2016
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
	///Class CalculateLeafNumber
    /// calculate leaf number. LeafNumber increase is caped at one more leaf per day
    /// </summary>
	public class CalculateLeafNumber : IStrategySiriusQualityPhenology
	{

	#region Constructor

			public CalculateLeafNumber()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 0;
				 v1.Description = "true if maize";
				 v1.Id = 0;
				 v1.MaxValue = 1;
				 v1.MinValue = 0;
				 v1.Name = "SwitchMaize";
				 v1.Size = 1;
				 v1.Units = "-";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 10;
				 v2.Description = "slope of leaf initiation";
				 v2.Id = 0;
				 v2.MaxValue = 1000;
				 v2.MinValue = 0;
				 v2.Name = "atip";
				 v2.Size = 1;
				 v2.Units = "leaf/°Cday²";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 10;
				 v3.Description = "parameter for maize number of tip emerged";
				 v3.Id = 0;
				 v3.MaxValue = 1000;
				 v3.MinValue = 0;
				 v3.Name = "Leaf_tip_emerg";
				 v3.Size = 1;
				 v3.Units = "-";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new VarInfo();
				 v4.DefaultValue = 1.412;
				 v4.Description = "-";
				 v4.Id = 0;
				 v4.MaxValue = 1000;
				 v4.MinValue = 0;
				 v4.Name = "k_bl";
				 v4.Size = 1;
				 v4.Units = "-";
				 v4.URL = "";
				 v4.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v4.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new VarInfo();
				 v5.DefaultValue = 6.617;
				 v5.Description = "-";
				 v5.Id = 0;
				 v5.MaxValue = 1000;
				 v5.MinValue = 0;
				 v5.Name = "Nlim";
				 v5.Size = 1;
				 v5.Units = "-";
				 v5.URL = "";
				 v5.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v5.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v5);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd1.PropertyName = "DeltaTT";
				pd1.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.DeltaTT)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.DeltaTT);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd2.PropertyName = "Phyllochron";
				pd2.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd3.PropertyName = "HasFlagLeafLiguleAppeared";
				pd3.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.HasFlagLeafLiguleAppeared)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.HasFlagLeafLiguleAppeared);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd4.PropertyName = "cumulTTPhenoMaizeAtEmergence";
				pd4.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTPhenoMaizeAtEmergence)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTPhenoMaizeAtEmergence);
				_inputs0_0.Add(pd4);
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd5.PropertyName = "LeafNumber";
				pd5.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
				_inputs0_0.Add(pd5);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd6.PropertyName = "LeafNumber";
				pd6.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
				_outputs0_0.Add(pd6);
				PropertyDescription pd7 = new PropertyDescription();
				pd7.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd7.PropertyName = "Ntip";
				pd7.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.Ntip)).ValueType.TypeForCurrentValue;
				pd7.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.Ntip);
				_outputs0_0.Add(pd7);
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
				get { return "calculate leaf number. LeafNumber increase is caped at one more leaf per day"; }
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
				_pd.Add("Date", "12/8/2016");
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

			
			public Int32 SwitchMaize
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SwitchMaize");
						if (vi != null && vi.CurrentValue!=null) return (Int32)vi.CurrentValue ;
						else throw new Exception("Parameter 'SwitchMaize' not found (or found null) in strategy 'CalculateLeafNumber'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SwitchMaize");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SwitchMaize' not found in strategy 'CalculateLeafNumber'");
				}
			}
			public Double atip
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("atip");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'atip' not found (or found null) in strategy 'CalculateLeafNumber'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("atip");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'atip' not found in strategy 'CalculateLeafNumber'");
				}
			}
			public Double Leaf_tip_emerg
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Leaf_tip_emerg");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Leaf_tip_emerg' not found (or found null) in strategy 'CalculateLeafNumber'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Leaf_tip_emerg");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Leaf_tip_emerg' not found in strategy 'CalculateLeafNumber'");
				}
			}
			public Double k_bl
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("k_bl");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'k_bl' not found (or found null) in strategy 'CalculateLeafNumber'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("k_bl");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'k_bl' not found in strategy 'CalculateLeafNumber'");
				}
			}
			public Double Nlim
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Nlim");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Nlim' not found (or found null) in strategy 'CalculateLeafNumber'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Nlim");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Nlim' not found in strategy 'CalculateLeafNumber'");
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
                SwitchMaizeVarInfo.Name = "SwitchMaize";
				SwitchMaizeVarInfo.Description =" true if maize";
				SwitchMaizeVarInfo.MaxValue = 1;
				SwitchMaizeVarInfo.MinValue = 0;
				SwitchMaizeVarInfo.DefaultValue = 0;
				SwitchMaizeVarInfo.Units = "-";
				SwitchMaizeVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Integer");

				atipVarInfo.Name = "atip";
				atipVarInfo.Description =" slope of leaf initiation";
				atipVarInfo.MaxValue = 1000;
				atipVarInfo.MinValue = 0;
				atipVarInfo.DefaultValue = 10;
				atipVarInfo.Units = "leaf/°Cday²";
				atipVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				Leaf_tip_emergVarInfo.Name = "Leaf_tip_emerg";
				Leaf_tip_emergVarInfo.Description =" parameter for maize number of tip emerged";
				Leaf_tip_emergVarInfo.MaxValue = 1000;
				Leaf_tip_emergVarInfo.MinValue = 0;
				Leaf_tip_emergVarInfo.DefaultValue = 10;
				Leaf_tip_emergVarInfo.Units = "-";
				Leaf_tip_emergVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				k_blVarInfo.Name = "k_bl";
				k_blVarInfo.Description =" -";
				k_blVarInfo.MaxValue = 1000;
				k_blVarInfo.MinValue = 0;
				k_blVarInfo.DefaultValue = 1.412;
				k_blVarInfo.Units = "-";
				k_blVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				NlimVarInfo.Name = "Nlim";
				NlimVarInfo.Description =" -";
				NlimVarInfo.MaxValue = 1000;
				NlimVarInfo.MinValue = 0;
				NlimVarInfo.DefaultValue = 6.617;
				NlimVarInfo.Units = "-";
				NlimVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _SwitchMaizeVarInfo= new VarInfo();
				/// <summary> 
				///SwitchMaize VarInfo definition
				/// </summary>
				public static VarInfo SwitchMaizeVarInfo
				{
					get { return _SwitchMaizeVarInfo; }
				}
				private static VarInfo _atipVarInfo= new VarInfo();
				/// <summary> 
				///atip VarInfo definition
				/// </summary>
				public static VarInfo atipVarInfo
				{
					get { return _atipVarInfo; }
				}
				private static VarInfo _Leaf_tip_emergVarInfo= new VarInfo();
				/// <summary> 
				///Leaf_tip_emerg VarInfo definition
				/// </summary>
				public static VarInfo Leaf_tip_emergVarInfo
				{
					get { return _Leaf_tip_emergVarInfo; }
				}
				private static VarInfo _k_blVarInfo= new VarInfo();
				/// <summary> 
				///k_bl VarInfo definition
				/// </summary>
				public static VarInfo k_blVarInfo
				{
					get { return _k_blVarInfo; }
				}
				private static VarInfo _NlimVarInfo= new VarInfo();
				/// <summary> 
				///Nlim VarInfo definition
				/// </summary>
				public static VarInfo NlimVarInfo
				{
					get { return _NlimVarInfo; }
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
					
					SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.CurrentValue=phenologystate.LeafNumber;
					SiriusQualityPhenology.PhenologyStateVarInfo.Ntip.CurrentValue=phenologystate.Ntip;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.ValueType)){prc.AddCondition(r6);}
					RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.Ntip);
					if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.Ntip.ValueType)){prc.AddCondition(r7);}

					

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
					
					SiriusQualityPhenology.PhenologyStateVarInfo.DeltaTT.CurrentValue=phenologystate.DeltaTT;
					SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron.CurrentValue=phenologystate.Phyllochron;
					SiriusQualityPhenology.PhenologyStateVarInfo.HasFlagLeafLiguleAppeared.CurrentValue=phenologystate.HasFlagLeafLiguleAppeared;
					SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTPhenoMaizeAtEmergence.CurrentValue=phenologystate.cumulTTPhenoMaizeAtEmergence;
					SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.CurrentValue=phenologystate.LeafNumber;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.DeltaTT);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.DeltaTT.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.Phyllochron.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.HasFlagLeafLiguleAppeared);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.HasFlagLeafLiguleAppeared.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTPhenoMaizeAtEmergence);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTPhenoMaizeAtEmergence.ValueType)){prc.AddCondition(r4);}
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.ValueType)){prc.AddCondition(r5);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SwitchMaize")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("atip")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Leaf_tip_emerg")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("k_bl")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Nlim")));

					

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
                if (phenologystate.HasFlagLeafLiguleAppeared==0)//sowingToAnthesis
                {
					if (SwitchMaize==0) 
                    {
                        if (phenologystate.Phyllochron == 0.0)
                        {
                            phenologystate.Phyllochron = 0.0000001;
                            //phenologyState_.TagPhenoWarnOut = "Suspicious phenology phase transition occured";
                        }
                        phenologystate.LeafNumber = phenologystate1.LeafNumber + Math.Min(phenologystate.DeltaTT / phenologystate.Phyllochron, 0.999);
                    }
                    else
                    {
                        //non continuous version of LN based on startExp
                        if (phenologystate1.LeafNumber < Leaf_tip_emerg)
                        {
                            phenologystate.LeafNumber = Leaf_tip_emerg;
                        }
                        else
                        {
                            //calcul startExp of next leaf
                            double nextstartExpTT = 0;

                            double nexttipTT = ((phenologystate1.LeafNumber + 1) - Leaf_tip_emerg) / atip + phenologystate.cumulTTPhenoMaizeAtEmergence;

                            double abl = k_bl * atip;
                            double tt_lim_lip = ((Nlim - Leaf_tip_emerg) / atip) + phenologystate.cumulTTPhenoMaizeAtEmergence;
                            double bbl = Nlim - (abl * tt_lim_lip);

                            double tt_bl = ((phenologystate1.LeafNumber + 1) - bbl) / abl;
                            if (tt_bl > nexttipTT)
                            {
                                nextstartExpTT = nexttipTT;
                            }
                            else
                            {
                                nextstartExpTT = tt_bl;
                            }
                            if (phenologystate.cumulTT[6] >= nextstartExpTT) 
                            {
                                phenologystate.LeafNumber = phenologystate1.LeafNumber + 1;
                            }
                            else
                            {
                                phenologystate.LeafNumber = phenologystate1.LeafNumber;
                            }
                        }
                        phenologystate.Ntip = atip * (phenologystate.cumulTT[6] - phenologystate.cumulTTPhenoMaizeAtEmergence) + Leaf_tip_emerg;
					}				
				}
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
                public void Init(SiriusQualityPhenology.PhenologyState phenologyState_)
                {
                    phenologyState_.LeafNumber = 0;//we don't need to call updateflag because leafNumber =0;
                    //TagPhenoWarnOut = "";
                }

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}

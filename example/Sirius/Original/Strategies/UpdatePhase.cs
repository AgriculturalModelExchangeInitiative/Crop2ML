

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
//To make this project compile please add the reference to assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;

namespace SiriusQualityPhenology.Strategies
{

	/// <summary>
	///Class UpdatePhase
    /// This strategy advances the phase and calculate the final leaf number
    /// </summary>
	public class UpdatePhase : IStrategySiriusQualityPhenology
	{

	#region Constructor

			public UpdatePhase()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 0;
				 v1.Description = "true if the plant is vernalizable";
				 v1.Id = 0;
				 v1.MaxValue = 1;
				 v1.MinValue = 0;
				 v1.Name = "IsVernalizable";
				 v1.Size = 1;
				 v1.Units = "NA";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 150;
				 v2.Description = "Thermal time from sowing to emergence";
				 v2.Id = 0;
				 v2.MaxValue = 1000;
				 v2.MinValue = 0;
				 v2.Name = "Dse";
				 v2.Size = 1;
				 v2.Units = "°Cd";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 0;
				 v3.Description = "Phyllochronic duration of the period between flag leaf ligule appearance and anthesis";
				 v3.Id = 0;
				 v3.MaxValue = 1000;
				 v3.MinValue = 0;
				 v3.Name = "PFLLAnth";
				 v3.Size = 1;
				 v3.Units = "dimensionless";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new VarInfo();
				 v4.DefaultValue = 0;
				 v4.Description = "Duration of the endosperm cell division phase";
				 v4.Id = 0;
				 v4.MaxValue = 10000;
				 v4.MinValue = 0;
				 v4.Name = "Dcd";
				 v4.Size = 1;
				 v4.Units = "°Cd";
				 v4.URL = "";
				 v4.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v4.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new VarInfo();
				 v5.DefaultValue = 0;
				 v5.Description = "Grain filling duration (from anthesis to physiological maturity)";
				 v5.Id = 0;
				 v5.MaxValue = 10000;
				 v5.MinValue = 0;
				 v5.Name = "Dgf";
				 v5.Size = 1;
				 v5.Units = "°Cd";
				 v5.URL = "";
				 v5.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v5.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v5);
				VarInfo v6 = new VarInfo();
				 v6.DefaultValue = 0;
				 v6.Description = "Grain maturation duration (from physiological maturity to harvest ripeness)";
				 v6.Id = 0;
				 v6.MaxValue = 10000;
				 v6.MinValue = 0;
				 v6.Name = "Degfm";
				 v6.Size = 1;
				 v6.Units = "°Cd";
				 v6.URL = "";
				 v6.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v6.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v6);
				VarInfo v7 = new VarInfo();
				 v7.DefaultValue = 0;
				 v7.Description = "Saturating photoperiod above which final leaf number is not influenced by daylength";
				 v7.Id = 0;
				 v7.MaxValue = 24;
				 v7.MinValue = 0;
				 v7.Name = "MaxDL";
				 v7.Size = 1;
				 v7.Units = "h";
				 v7.URL = "";
				 v7.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v7.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v7);
				VarInfo v8 = new VarInfo();
				 v8.DefaultValue = 0;
				 v8.Description = "Daylength response of leaf production";
				 v8.Id = 0;
				 v8.MaxValue = 1;
				 v8.MinValue = 0;
				 v8.Name = "SLDL";
				 v8.Size = 1;
				 v8.Units = "leaf/h";
				 v8.URL = "";
				 v8.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v8.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v8);
				VarInfo v9 = new VarInfo();
				 v9.DefaultValue = 0;
				 v9.Description = "true to ignore grain maturation";
				 v9.Id = 0;
				 v9.MaxValue = 1;
				 v9.MinValue = 0;
				 v9.Name = "IgnoreGrainMaturation";
				 v9.Size = 1;
				 v9.Units = "-";
				 v9.URL = "";
				 v9.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v9.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");
				 _parameters0_0.Add(v9);
				VarInfo v10 = new VarInfo();
				 v10.DefaultValue = 1;
				 v10.Description = "Number of phyllochron between heading and anthesis";
				 v10.Id = 0;
				 v10.MaxValue = 3;
				 v10.MinValue = 0;
				 v10.Name = "PHEADANTH";
				 v10.Size = 1;
				 v10.Units = "-";
				 v10.URL = "";
				 v10.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v10.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v10);
				VarInfo v11 = new VarInfo();
				 v11.DefaultValue = 0;
				 v11.Description = "true if maize";
				 v11.Id = 0;
				 v11.MaxValue = 1;
				 v11.MinValue = 0;
				 v11.Name = "SwitchMaize";
				 v11.Size = 1;
				 v11.Units = "-";
				 v11.URL = "";
				 v11.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v11.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");
				 _parameters0_0.Add(v11);
				VarInfo v12 = new VarInfo();
				 v12.DefaultValue = 0;
				 v12.Description = "Switch to choose the type of phyllochron calculation to be used";
				 v12.Id = 0;
				 v12.MaxValue = 0;
				 v12.MinValue = 0;
				 v12.Name = "choosePhyllUse";
				 v12.Size = 0;
				 v12.Units = "-";
				 v12.URL = "";
				 v12.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v12.ValueType = VarInfoValueTypes.GetInstanceForName("String");
				 _parameters0_0.Add(v12);
				VarInfo v13 = new VarInfo();
				 v13.DefaultValue = 120;
				 v13.Description = "Phyllochron (Varietal parameter)";
				 v13.Id = 0;
				 v13.MaxValue = 1000;
				 v13.MinValue = 0;
				 v13.Name = "P";
				 v13.Size = 1;
				 v13.Units = "°Cd/leaf";
				 v13.URL = "";
				 v13.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v13.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v13);
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
				pd2.PropertyName = "cumulTT";
				pd2.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd3.PropertyName = "Vernaprog";
				pd3.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd4.PropertyName = "MinFinalNumber";
				pd4.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber);
				_inputs0_0.Add(pd4);
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd5.PropertyName = "LeafNumber";
				pd5.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
				_inputs0_0.Add(pd5);
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd6.PropertyName = "GrainCumulTT";
				pd6.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.GrainCumulTT)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.GrainCumulTT);
				_inputs0_0.Add(pd6);
				PropertyDescription pd7 = new PropertyDescription();
				pd7.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd7.PropertyName = "GAI";
				pd7.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.GAI)).ValueType.TypeForCurrentValue;
				pd7.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.GAI);
				_inputs0_0.Add(pd7);
				PropertyDescription pd8 = new PropertyDescription();
				pd8.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd8.PropertyName = "isMomentRegistredZC_39";
				pd8.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.isMomentRegistredZC_39)).ValueType.TypeForCurrentValue;
				pd8.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.isMomentRegistredZC_39);
				_inputs0_0.Add(pd8);
				PropertyDescription pd9 = new PropertyDescription();
				pd9.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd9.PropertyName = "cumulTTFromZC_39";
				pd9.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_39)).ValueType.TypeForCurrentValue;
				pd9.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_39);
				_inputs0_0.Add(pd9);
				PropertyDescription pd10 = new PropertyDescription();
				pd10.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd10.PropertyName = "phase_";
				pd10.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.phase_)).ValueType.TypeForCurrentValue;
				pd10.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.phase_);
				_inputs0_0.Add(pd10);
				PropertyDescription pd11 = new PropertyDescription();
				pd11.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd11.PropertyName = "cumulTTFromZC_91";
				pd11.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_91)).ValueType.TypeForCurrentValue;
				pd11.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_91);
				_inputs0_0.Add(pd11);
				PropertyDescription pd12 = new PropertyDescription();
				pd12.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd12.PropertyName = "Fixphyll";
				pd12.PropertyType = (( SiriusQualityPhenology.PhenologyStateVarInfo.Fixphyll)).ValueType.TypeForCurrentValue;
				pd12.PropertyVarInfo =( SiriusQualityPhenology.PhenologyStateVarInfo.Fixphyll);
				_inputs0_0.Add(pd12);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd13 = new PropertyDescription();
				pd13.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd13.PropertyName = "FinalLeafNumber";
				pd13.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.FinalLeafNumber)).ValueType.TypeForCurrentValue;
				pd13.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.FinalLeafNumber);
				_outputs0_0.Add(pd13);
				PropertyDescription pd14 = new PropertyDescription();
				pd14.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd14.PropertyName = "phase_";
				pd14.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.phase_)).ValueType.TypeForCurrentValue;
				pd14.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.phase_);
				_outputs0_0.Add(pd14);
				PropertyDescription pd15 = new PropertyDescription();
				pd15.DomainClassType = typeof(SiriusQualityPhenology.PhenologyState);
				pd15.PropertyName = "hasLastPrimordiumAppeared";
				pd15.PropertyType =  (( SiriusQualityPhenology.PhenologyStateVarInfo.hasLastPrimordiumAppeared)).ValueType.TypeForCurrentValue;
				pd15.PropertyVarInfo =(  SiriusQualityPhenology.PhenologyStateVarInfo.hasLastPrimordiumAppeared);
				_outputs0_0.Add(pd15);
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
				get { return "This strategy advances the phase and calculate the final leaf number"; }
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

			
			public Int32 IsVernalizable
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("IsVernalizable");
						if (vi != null && vi.CurrentValue!=null) return (Int32)vi.CurrentValue ;
						else throw new Exception("Parameter 'IsVernalizable' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("IsVernalizable");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'IsVernalizable' not found in strategy 'UpdatePhase'");
				}
			}
			public Double Dse
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Dse");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Dse' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Dse");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Dse' not found in strategy 'UpdatePhase'");
				}
			}
			public Double PFLLAnth
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PFLLAnth");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PFLLAnth' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PFLLAnth");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PFLLAnth' not found in strategy 'UpdatePhase'");
				}
			}
			public Double Dcd
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Dcd");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Dcd' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Dcd");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Dcd' not found in strategy 'UpdatePhase'");
				}
			}
			public Double Dgf
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Dgf");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Dgf' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Dgf");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Dgf' not found in strategy 'UpdatePhase'");
				}
			}
			public Double Degfm
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Degfm");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Degfm' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Degfm");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Degfm' not found in strategy 'UpdatePhase'");
				}
			}
			public Double MaxDL
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("MaxDL");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'MaxDL' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("MaxDL");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'MaxDL' not found in strategy 'UpdatePhase'");
				}
			}
			public Double SLDL
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SLDL");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'SLDL' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SLDL");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SLDL' not found in strategy 'UpdatePhase'");
				}
			}
			public Int32 IgnoreGrainMaturation
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("IgnoreGrainMaturation");
						if (vi != null && vi.CurrentValue!=null) return (Int32)vi.CurrentValue ;
						else throw new Exception("Parameter 'IgnoreGrainMaturation' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("IgnoreGrainMaturation");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'IgnoreGrainMaturation' not found in strategy 'UpdatePhase'");
				}
			}
			public Double PHEADANTH
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PHEADANTH");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PHEADANTH' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PHEADANTH");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PHEADANTH' not found in strategy 'UpdatePhase'");
				}
			}
			public Int32 SwitchMaize
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SwitchMaize");
						if (vi != null && vi.CurrentValue!=null) return (Int32)vi.CurrentValue ;
						else throw new Exception("Parameter 'SwitchMaize' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SwitchMaize");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SwitchMaize' not found in strategy 'UpdatePhase'");
				}
			}
			public String choosePhyllUse
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("choosePhyllUse");
						if (vi != null && vi.CurrentValue!=null) return (String)vi.CurrentValue ;
						else throw new Exception("Parameter 'choosePhyllUse' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("choosePhyllUse");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'choosePhyllUse' not found in strategy 'UpdatePhase'");
				}
			}
			public Double P
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("P");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'P' not found (or found null) in strategy 'UpdatePhase'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("P");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'P' not found in strategy 'UpdatePhase'");
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
                IsVernalizableVarInfo.Name = "IsVernalizable";
				IsVernalizableVarInfo.Description =" true if the plant is vernalizable";
				IsVernalizableVarInfo.MaxValue = 1;
				IsVernalizableVarInfo.MinValue = 0;
				IsVernalizableVarInfo.DefaultValue = 0;
				IsVernalizableVarInfo.Units = "NA";
				IsVernalizableVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Integer");

				DseVarInfo.Name = "Dse";
				DseVarInfo.Description =" Thermal time from sowing to emergence";
				DseVarInfo.MaxValue = 1000;
				DseVarInfo.MinValue = 0;
				DseVarInfo.DefaultValue = 150;
				DseVarInfo.Units = "°Cd";
				DseVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PFLLAnthVarInfo.Name = "PFLLAnth";
				PFLLAnthVarInfo.Description =" Phyllochronic duration of the period between flag leaf ligule appearance and anthesis";
				PFLLAnthVarInfo.MaxValue = 1000;
				PFLLAnthVarInfo.MinValue = 0;
				PFLLAnthVarInfo.DefaultValue = 0;
				PFLLAnthVarInfo.Units = "dimensionless";
				PFLLAnthVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				DcdVarInfo.Name = "Dcd";
				DcdVarInfo.Description =" Duration of the endosperm cell division phase";
				DcdVarInfo.MaxValue = 10000;
				DcdVarInfo.MinValue = 0;
				DcdVarInfo.DefaultValue = 0;
				DcdVarInfo.Units = "°Cd";
				DcdVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				DgfVarInfo.Name = "Dgf";
				DgfVarInfo.Description =" Grain filling duration (from anthesis to physiological maturity)";
				DgfVarInfo.MaxValue = 10000;
				DgfVarInfo.MinValue = 0;
				DgfVarInfo.DefaultValue = 0;
				DgfVarInfo.Units = "°Cd";
				DgfVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				DegfmVarInfo.Name = "Degfm";
				DegfmVarInfo.Description =" Grain maturation duration (from physiological maturity to harvest ripeness)";
				DegfmVarInfo.MaxValue = 10000;
				DegfmVarInfo.MinValue = 0;
				DegfmVarInfo.DefaultValue = 0;
				DegfmVarInfo.Units = "°Cd";
				DegfmVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				MaxDLVarInfo.Name = "MaxDL";
				MaxDLVarInfo.Description =" Saturating photoperiod above which final leaf number is not influenced by daylength";
				MaxDLVarInfo.MaxValue = 24;
				MaxDLVarInfo.MinValue = 0;
				MaxDLVarInfo.DefaultValue = 0;
				MaxDLVarInfo.Units = "h";
				MaxDLVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				SLDLVarInfo.Name = "SLDL";
				SLDLVarInfo.Description =" Daylength response of leaf production";
				SLDLVarInfo.MaxValue = 1;
				SLDLVarInfo.MinValue = 0;
				SLDLVarInfo.DefaultValue = 0;
				SLDLVarInfo.Units = "leaf/h";
				SLDLVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				IgnoreGrainMaturationVarInfo.Name = "IgnoreGrainMaturation";
				IgnoreGrainMaturationVarInfo.Description =" true to ignore grain maturation";
				IgnoreGrainMaturationVarInfo.MaxValue = 1;
				IgnoreGrainMaturationVarInfo.MinValue = 0;
				IgnoreGrainMaturationVarInfo.DefaultValue = 0;
				IgnoreGrainMaturationVarInfo.Units = "-";
				IgnoreGrainMaturationVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Integer");

				PHEADANTHVarInfo.Name = "PHEADANTH";
				PHEADANTHVarInfo.Description =" Number of phyllochron between heading and anthesis";
				PHEADANTHVarInfo.MaxValue = 3;
				PHEADANTHVarInfo.MinValue = 0;
				PHEADANTHVarInfo.DefaultValue = 1;
				PHEADANTHVarInfo.Units = "-";
				PHEADANTHVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				SwitchMaizeVarInfo.Name = "SwitchMaize";
				SwitchMaizeVarInfo.Description =" true if maize";
				SwitchMaizeVarInfo.MaxValue = 1;
				SwitchMaizeVarInfo.MinValue = 0;
				SwitchMaizeVarInfo.DefaultValue = 0;
				SwitchMaizeVarInfo.Units = "-";
				SwitchMaizeVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Integer");

				choosePhyllUseVarInfo.Name = "choosePhyllUse";
				choosePhyllUseVarInfo.Description =" Switch to choose the type of phyllochron calculation to be used";
				choosePhyllUseVarInfo.MaxValue = 0;
				choosePhyllUseVarInfo.MinValue = 0;
				choosePhyllUseVarInfo.DefaultValue = 0;
				choosePhyllUseVarInfo.Units = "-";
				choosePhyllUseVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("String");

				PVarInfo.Name = "P";
				PVarInfo.Description =" Phyllochron (Varietal parameter)";
				PVarInfo.MaxValue = 1000;
				PVarInfo.MinValue = 0;
				PVarInfo.DefaultValue = 120;
				PVarInfo.Units = "°Cd/leaf";
				PVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _IsVernalizableVarInfo= new VarInfo();
				/// <summary> 
				///IsVernalizable VarInfo definition
				/// </summary>
				public static VarInfo IsVernalizableVarInfo
				{
					get { return _IsVernalizableVarInfo; }
				}
				private static VarInfo _DseVarInfo= new VarInfo();
				/// <summary> 
				///Dse VarInfo definition
				/// </summary>
				public static VarInfo DseVarInfo
				{
					get { return _DseVarInfo; }
				}
				private static VarInfo _PFLLAnthVarInfo= new VarInfo();
				/// <summary> 
				///PFLLAnth VarInfo definition
				/// </summary>
				public static VarInfo PFLLAnthVarInfo
				{
					get { return _PFLLAnthVarInfo; }
				}
				private static VarInfo _DcdVarInfo= new VarInfo();
				/// <summary> 
				///Dcd VarInfo definition
				/// </summary>
				public static VarInfo DcdVarInfo
				{
					get { return _DcdVarInfo; }
				}
				private static VarInfo _DgfVarInfo= new VarInfo();
				/// <summary> 
				///Dgf VarInfo definition
				/// </summary>
				public static VarInfo DgfVarInfo
				{
					get { return _DgfVarInfo; }
				}
				private static VarInfo _DegfmVarInfo= new VarInfo();
				/// <summary> 
				///Degfm VarInfo definition
				/// </summary>
				public static VarInfo DegfmVarInfo
				{
					get { return _DegfmVarInfo; }
				}
				private static VarInfo _MaxDLVarInfo= new VarInfo();
				/// <summary> 
				///MaxDL VarInfo definition
				/// </summary>
				public static VarInfo MaxDLVarInfo
				{
					get { return _MaxDLVarInfo; }
				}
				private static VarInfo _SLDLVarInfo= new VarInfo();
				/// <summary> 
				///SLDL VarInfo definition
				/// </summary>
				public static VarInfo SLDLVarInfo
				{
					get { return _SLDLVarInfo; }
				}
				private static VarInfo _IgnoreGrainMaturationVarInfo= new VarInfo();
				/// <summary> 
				///IgnoreGrainMaturation VarInfo definition
				/// </summary>
				public static VarInfo IgnoreGrainMaturationVarInfo
				{
					get { return _IgnoreGrainMaturationVarInfo; }
				}
				private static VarInfo _PHEADANTHVarInfo= new VarInfo();
				/// <summary> 
				///PHEADANTH VarInfo definition
				/// </summary>
				public static VarInfo PHEADANTHVarInfo
				{
					get { return _PHEADANTHVarInfo; }
				}
				private static VarInfo _SwitchMaizeVarInfo= new VarInfo();
				/// <summary> 
				///SwitchMaize VarInfo definition
				/// </summary>
				public static VarInfo SwitchMaizeVarInfo
				{
					get { return _SwitchMaizeVarInfo; }
				}
				private static VarInfo _choosePhyllUseVarInfo= new VarInfo();
				/// <summary> 
				///choosePhyllUse VarInfo definition
				/// </summary>
				public static VarInfo choosePhyllUseVarInfo
				{
					get { return _choosePhyllUseVarInfo; }
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
					
					SiriusQualityPhenology.PhenologyStateVarInfo.FinalLeafNumber.CurrentValue=phenologystate.FinalLeafNumber;
					SiriusQualityPhenology.PhenologyStateVarInfo.phase_.CurrentValue=phenologystate.phase_;
					SiriusQualityPhenology.PhenologyStateVarInfo.hasLastPrimordiumAppeared.CurrentValue=phenologystate.hasLastPrimordiumAppeared;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r13 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.FinalLeafNumber);
					if(r13.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.FinalLeafNumber.ValueType)){prc.AddCondition(r13);}
					RangeBasedCondition r14 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.phase_);
					if(r14.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.phase_.ValueType)){prc.AddCondition(r14);}
					RangeBasedCondition r15 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.hasLastPrimordiumAppeared);
					if(r15.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.hasLastPrimordiumAppeared.ValueType)){prc.AddCondition(r15);}

					

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
					SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT.CurrentValue=phenologystate.cumulTT;
					SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog.CurrentValue=phenologystate.Vernaprog;
					SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber.CurrentValue=phenologystate.MinFinalNumber;
					SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.CurrentValue=phenologystate.LeafNumber;
					SiriusQualityPhenology.PhenologyStateVarInfo.GrainCumulTT.CurrentValue=phenologystate.GrainCumulTT;
					SiriusQualityPhenology.PhenologyStateVarInfo.GAI.CurrentValue=phenologystate.GAI;
					SiriusQualityPhenology.PhenologyStateVarInfo.isMomentRegistredZC_39.CurrentValue=phenologystate.isMomentRegistredZC_39;
					SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_39.CurrentValue=phenologystate.cumulTTFromZC_39;
					SiriusQualityPhenology.PhenologyStateVarInfo.phase_.CurrentValue=phenologystate.phase_;
					SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_91.CurrentValue=phenologystate.cumulTTFromZC_91;
					SiriusQualityPhenology.PhenologyStateVarInfo.Fixphyll.CurrentValue=phenologystate.Fixphyll;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.DayLength);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.DayLength.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTT.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.Vernaprog.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.MinFinalNumber.ValueType)){prc.AddCondition(r4);}
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.LeafNumber.ValueType)){prc.AddCondition(r5);}
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.GrainCumulTT);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.GrainCumulTT.ValueType)){prc.AddCondition(r6);}
					RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.GAI);
					if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.GAI.ValueType)){prc.AddCondition(r7);}
					RangeBasedCondition r8 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.isMomentRegistredZC_39);
					if(r8.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.isMomentRegistredZC_39.ValueType)){prc.AddCondition(r8);}
					RangeBasedCondition r9 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_39);
					if(r9.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_39.ValueType)){prc.AddCondition(r9);}
					RangeBasedCondition r10 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.phase_);
					if(r10.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.phase_.ValueType)){prc.AddCondition(r10);}
					RangeBasedCondition r11 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_91);
					if(r11.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.cumulTTFromZC_91.ValueType)){prc.AddCondition(r11);}
					RangeBasedCondition r12 = new RangeBasedCondition(SiriusQualityPhenology.PhenologyStateVarInfo.Fixphyll);
					if(r12.ApplicableVarInfoValueTypes.Contains( SiriusQualityPhenology.PhenologyStateVarInfo.Fixphyll.ValueType)){prc.AddCondition(r12);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("IsVernalizable")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Dse")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PFLLAnth")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Dcd")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Dgf")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Degfm")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("MaxDL")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SLDL")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("IgnoreGrainMaturation")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PHEADANTH")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SwitchMaize")));
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

                //for Maize phenologystate.FinalLeafNumber= Nfinal

                if (phenologystate1.phase_.phaseValue >= 0 && phenologystate1.phase_.phaseValue < 1)//SowingToEmergence
                {
                    //CheckEmergence
					if (SwitchMaize==0)
					{
                        if (phenologystate.cumulTT[0] >= Dse)
						{
                            phenologystate.phase_.phaseValue = 1;//Emergence 
						}
                        else
                        {
                            phenologystate.phase_.phaseValue = phenologystate1.phase_.phaseValue;
                        }
					}
					else
					{
                        if (phenologystate.cumulTT[6] >= Dse)
						{
							phenologystate.phase_.phaseValue = 1;//Emergence
						}
                        else
                        {
                            phenologystate.phase_.phaseValue = phenologystate1.phase_.phaseValue;
                        }
					}
                }
                else if (phenologystate1.phase_.phaseValue >= 1 && phenologystate1.phase_.phaseValue < 2)//EmergenceToFloralInitiation
                {
                    if (IsAfterVernalizationEnd(phenologystate.Vernaprog) == 1)
                    {
                        //CalculateFinalLeafNumber

                        // Pete's version of the method found in Sirius 2005x
                        // leaf number associated with the approximate primordium number for
                        // last leaf  primordium.  Final leaf number is calculated from the
                        // daylength of the day this primordium occurs.  If daylength is more thant 15
                        // then set final leaf number to minimum leaf number
                        if (SwitchMaize == 0)
                        {

                            if (phenologystate.DayLength > MaxDL)
                            {
                                phenologystate.FinalLeafNumber = phenologystate.MinFinalNumber;
                                phenologystate.hasLastPrimordiumAppeared = 1;

                            }
                            else
                            {
                                var appFLN = phenologystate.MinFinalNumber + SLDL * (MaxDL - phenologystate.DayLength);
                                // calculation of final leaf number from daylength at inflexion plus 2 leaves
                                if (appFLN / 2.0 <= phenologystate.LeafNumber)
                                {
                                    phenologystate.FinalLeafNumber = appFLN;
                                    phenologystate.hasLastPrimordiumAppeared = 1;
                                }
                                else
                                {
                                    phenologystate.phase_.phaseValue = phenologystate1.phase_.phaseValue;
                                }
                            }
                            //for Maize phenologystate.hasLastPrimordiumAppeared = true;
                        }
                        else
                        {
                            phenologystate.hasLastPrimordiumAppeared = 1;
                        }

                        //CheckFloralInitiation
                        if (phenologystate.hasLastPrimordiumAppeared == 1)
                        {
                            phenologystate.phase_.phaseValue = 2;//Floralinitiation  
                        }

                    }
                    else
                    {
                        phenologystate.phase_.phaseValue = phenologystate1.phase_.phaseValue;
                    }
                }
                else if (phenologystate1.phase_.phaseValue >= 2 && phenologystate1.phase_.phaseValue < 4)//FloralInitiationToAnthesis
                {
                    if (phenologystate.isMomentRegistredZC_39==1)
                    {
                        //calculate the heading date
                        if (phenologystate1.phase_.phaseValue < 3)
                        {
                            //double ttFromLastLeafToHeading = (PFLLAnth - PHEADANTH) * FixPhyll;
                            double ttFromLastLeafToHeading = 0.0;
                            if (choosePhyllUse == "Default") ttFromLastLeafToHeading = (PFLLAnth - PHEADANTH) * phenologystate.Fixphyll;
                            else if (choosePhyllUse == "PTQ") ttFromLastLeafToHeading = (PFLLAnth - PHEADANTH) * phenologystate.Phyllochron;
                            else if (choosePhyllUse == "Test") ttFromLastLeafToHeading = (PFLLAnth - PHEADANTH) * P;
                            if (phenologystate.cumulTTFromZC_39 >= ttFromLastLeafToHeading)
                            {
                                phenologystate.phase_.phaseValue = 3;
                            }
                            else
                            {
                                phenologystate.phase_.phaseValue = phenologystate1.phase_.phaseValue;
                            }
                        }
                        else
                        {
                            phenologystate.phase_.phaseValue = phenologystate1.phase_.phaseValue;
                        }

                        //CheckAnthesis;
                        //double ttFromLastLeafToAnthesis = PFLLAnth * FixPhyll;
                        double ttFromLastLeafToAnthesis = 0.0;
                        if (choosePhyllUse == "Default") ttFromLastLeafToAnthesis = PFLLAnth * phenologystate.Fixphyll;
                        else if (choosePhyllUse == "PTQ") ttFromLastLeafToAnthesis = PFLLAnth * phenologystate.Phyllochron;
                        else if (choosePhyllUse == "Test") ttFromLastLeafToAnthesis = PFLLAnth * P;
                        if (phenologystate.cumulTTFromZC_39 >= ttFromLastLeafToAnthesis)
                        {
                            phenologystate.phase_.phaseValue = 4;//Anthesis
                        }
                   
                    }
                    else
                    {
                        phenologystate.phase_.phaseValue = phenologystate1.phase_.phaseValue;
                    }
                }
                else if (phenologystate1.phase_.phaseValue == 4)//AnthesisToEndCellDivision
                {
                    //CheckEndCellDivision
                    if (phenologystate.GrainCumulTT >= Dcd)
                    {
                        phenologystate.phase_.phaseValue = 4.5;//EndCellDivision
                    }
                    else
                    {
                        phenologystate.phase_.phaseValue = phenologystate1.phase_.phaseValue;
                    }
                }
                else if (phenologystate1.phase_.phaseValue == 4.5)//EndCellDivisionToEndGrainFill
                {
                   // CheckEndGrainFilling
                    if (phenologystate.GrainCumulTT >= Dgf || phenologystate.GAI <= 0)
                    {
                        phenologystate.phase_.phaseValue = 5;//End of grain filling
                    }
                    else
                    {
                        phenologystate.phase_.phaseValue = phenologystate1.phase_.phaseValue;
                    }
                }
                else if (phenologystate1.phase_.phaseValue >= 5 && phenologystate1.phase_.phaseValue < 6)//EndGrainFillToMaturity
                {
                    //CheckMaturity
                    ///<Comment>To enable ignoring grain maturation duration</Comment>
                    double LocalDegfm = Degfm;
                    if (IgnoreGrainMaturation==1) LocalDegfm = -1;

                    if (phenologystate.cumulTTFromZC_91 >= LocalDegfm)
                    {
                        phenologystate.phase_.phaseValue = 6; //maturity
                    }
                    else
                    {
                        phenologystate.phase_.phaseValue = phenologystate1.phase_.phaseValue;
                    }
                }
                else if (phenologystate1.phase_.phaseValue >= 6 && phenologystate1.phase_.phaseValue < 7)
                {
                    phenologystate.phase_.phaseValue=phenologystate1.phase_.phaseValue;

                }
                else
                {
                    throw new Exception("current phase is not between 0 and 7");
                }

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
                ///<summary>Returns true if winter type cv. and vernalisation is completed or if spring type cv.</summary>
                private int IsAfterVernalizationEnd(double vernaProg)
                {
                    return (IsVernalizable==1 && vernaProg >= 1) || (IsVernalizable==0)?1:0;
                }

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}

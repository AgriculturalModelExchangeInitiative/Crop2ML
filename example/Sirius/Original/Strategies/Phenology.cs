

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
	///Class Phenology
    /// composite strategy which manage the phenology of Sirius Quality
    /// </summary>
	public class Phenology : IStrategySiriusQualityPhenology
	{

	#region Constructor

			public Phenology()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new CompositeStrategyVarInfo(_calculateleafnumber,"SwitchMaize");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new CompositeStrategyVarInfo(_calculateleafnumber,"atip");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new CompositeStrategyVarInfo(_calculateleafnumber,"Leaf_tip_emerg");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new CompositeStrategyVarInfo(_calculateleafnumber,"k_bl");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new CompositeStrategyVarInfo(_calculateleafnumber,"Nlim");
				 _parameters0_0.Add(v5);
				VarInfo v6 = new CompositeStrategyVarInfo(_calculatephyllochron,"Ldecr");
				 _parameters0_0.Add(v6);
				VarInfo v7 = new CompositeStrategyVarInfo(_calculatephyllochron,"Lincr");
				 _parameters0_0.Add(v7);
				VarInfo v8 = new CompositeStrategyVarInfo(_calculatephyllochron,"Pdecr");
				 _parameters0_0.Add(v8);
				VarInfo v9 = new CompositeStrategyVarInfo(_calculatephyllochron,"Pincr");
				 _parameters0_0.Add(v9);
				VarInfo v10 = new CompositeStrategyVarInfo(_calculatephyllochronwithptq,"Kl");
				 _parameters0_0.Add(v10);
				VarInfo v11 = new CompositeStrategyVarInfo(_calculatephyllochronwithptq,"slopePhylPTQ");
				 _parameters0_0.Add(v11);
				VarInfo v12 = new CompositeStrategyVarInfo(_calculatephyllochronwithptq,"interceptPhylPTQ");
				 _parameters0_0.Add(v12);
				VarInfo v13 = new CompositeStrategyVarInfo(_calculatephyllochronwithptq,"AreaSL");
				 _parameters0_0.Add(v13);
				VarInfo v14 = new CompositeStrategyVarInfo(_calculatephyllochronwithptq,"AreaSS");
				 _parameters0_0.Add(v14);
				VarInfo v15 = new CompositeStrategyVarInfo(_calculatephyllochronwithptq,"SowingDensity");
				 _parameters0_0.Add(v15);
				VarInfo v16 = new CompositeStrategyVarInfo(_calculatephyllochronwithptq,"PhylPTQ1");
				 _parameters0_0.Add(v16);
				VarInfo v17 = new CompositeStrategyVarInfo(_calculatephyllochronwithptq,"aPTQ");
				 _parameters0_0.Add(v17);
				VarInfo v18 = new CompositeStrategyVarInfo(_calculatephyllochronwosowingcorrection,"Ldecr");
				 _parameters0_0.Add(v18);
				VarInfo v19 = new CompositeStrategyVarInfo(_calculatephyllochronwosowingcorrection,"Lincr");
				 _parameters0_0.Add(v19);
				VarInfo v20 = new CompositeStrategyVarInfo(_calculatephyllochronwosowingcorrection,"Pdecr");
				 _parameters0_0.Add(v20);
				VarInfo v21 = new CompositeStrategyVarInfo(_calculatephyllochronwosowingcorrection,"Pincr");
				 _parameters0_0.Add(v21);
				VarInfo v22 = new CompositeStrategyVarInfo(_calculatephyllochronwosowingcorrection,"P");
				 _parameters0_0.Add(v22);
				VarInfo v23 = new CompositeStrategyVarInfo(_calculatephylsowingdatecorrection,"SowingDay");
				 _parameters0_0.Add(v23);
				VarInfo v24 = new CompositeStrategyVarInfo(_calculatephylsowingdatecorrection,"Latitude");
				 _parameters0_0.Add(v24);
				VarInfo v25 = new CompositeStrategyVarInfo(_calculatephylsowingdatecorrection,"SDsa_sh");
				 _parameters0_0.Add(v25);
				VarInfo v26 = new CompositeStrategyVarInfo(_calculatephylsowingdatecorrection,"P");
				 _parameters0_0.Add(v26);
				VarInfo v27 = new CompositeStrategyVarInfo(_calculatephylsowingdatecorrection,"SDws");
				 _parameters0_0.Add(v27);
				VarInfo v28 = new CompositeStrategyVarInfo(_calculatephylsowingdatecorrection,"SDsa_nh");
				 _parameters0_0.Add(v28);
				VarInfo v29 = new CompositeStrategyVarInfo(_calculatephylsowingdatecorrection,"Rp");
				 _parameters0_0.Add(v29);
				VarInfo v30 = new CompositeStrategyVarInfo(_calculateshootnumber,"SowingDensity");
				 _parameters0_0.Add(v30);
				VarInfo v31 = new CompositeStrategyVarInfo(_calculateshootnumber,"TargetFertileShoot");
				 _parameters0_0.Add(v31);
				VarInfo v32 = new CompositeStrategyVarInfo(_calculatevernalizationprogress,"AMNFLNO");
				 _parameters0_0.Add(v32);
				VarInfo v33 = new CompositeStrategyVarInfo(_calculatevernalizationprogress,"IsVernalizable");
				 _parameters0_0.Add(v33);
				VarInfo v34 = new CompositeStrategyVarInfo(_calculatevernalizationprogress,"MinTvern");
				 _parameters0_0.Add(v34);
				VarInfo v35 = new CompositeStrategyVarInfo(_calculatevernalizationprogress,"IntTvern");
				 _parameters0_0.Add(v35);
				VarInfo v36 = new CompositeStrategyVarInfo(_calculatevernalizationprogress,"VAI");
				 _parameters0_0.Add(v36);
				VarInfo v37 = new CompositeStrategyVarInfo(_calculatevernalizationprogress,"VBEE");
				 _parameters0_0.Add(v37);
				VarInfo v38 = new CompositeStrategyVarInfo(_calculatevernalizationprogress,"MinDL");
				 _parameters0_0.Add(v38);
				VarInfo v39 = new CompositeStrategyVarInfo(_calculatevernalizationprogress,"MaxDL");
				 _parameters0_0.Add(v39);
				VarInfo v40 = new CompositeStrategyVarInfo(_calculatevernalizationprogress,"MaxTvern");
				 _parameters0_0.Add(v40);
				VarInfo v41 = new CompositeStrategyVarInfo(_calculatevernalizationprogress,"PNini");
				 _parameters0_0.Add(v41);
				VarInfo v42 = new CompositeStrategyVarInfo(_calculatevernalizationprogress,"AMXLFNO");
				 _parameters0_0.Add(v42);
				VarInfo v43 = new CompositeStrategyVarInfo(_registerzadok,"Der");
				 _parameters0_0.Add(v43);
				VarInfo v44 = new CompositeStrategyVarInfo(_registerzadok,"slopeTSFLN");
				 _parameters0_0.Add(v44);
				VarInfo v45 = new CompositeStrategyVarInfo(_registerzadok,"intTSFLN");
				 _parameters0_0.Add(v45);
				VarInfo v46 = new CompositeStrategyVarInfo(_updatephase,"IsVernalizable");
				 _parameters0_0.Add(v46);
				VarInfo v47 = new CompositeStrategyVarInfo(_updatephase,"Dse");
				 _parameters0_0.Add(v47);
				VarInfo v48 = new CompositeStrategyVarInfo(_updatephase,"PFLLAnth");
				 _parameters0_0.Add(v48);
				VarInfo v49 = new CompositeStrategyVarInfo(_updatephase,"Dcd");
				 _parameters0_0.Add(v49);
				VarInfo v50 = new CompositeStrategyVarInfo(_updatephase,"Dgf");
				 _parameters0_0.Add(v50);
				VarInfo v51 = new CompositeStrategyVarInfo(_updatephase,"Degfm");
				 _parameters0_0.Add(v51);
				VarInfo v52 = new CompositeStrategyVarInfo(_updatephase,"MaxDL");
				 _parameters0_0.Add(v52);
				VarInfo v53 = new CompositeStrategyVarInfo(_updatephase,"SLDL");
				 _parameters0_0.Add(v53);
				VarInfo v54 = new CompositeStrategyVarInfo(_updatephase,"IgnoreGrainMaturation");
				 _parameters0_0.Add(v54);
				VarInfo v55 = new CompositeStrategyVarInfo(_updatephase,"PHEADANTH");
				 _parameters0_0.Add(v55);
				VarInfo v56 = new CompositeStrategyVarInfo(_updatephase,"SwitchMaize");
				 _parameters0_0.Add(v56);
				VarInfo v57 = new CompositeStrategyVarInfo(_updatephase,"choosePhyllUse");
				 _parameters0_0.Add(v57);
				VarInfo v58 = new CompositeStrategyVarInfo(_updatephase,"P");
				 _parameters0_0.Add(v58);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				mo0_0.Outputs=_outputs0_0;
				//Associated strategies
				List<string> lAssStrat0_0 = new List<string>();
				lAssStrat0_0.Add(typeof(SiriusQualityPhenology.Strategies.CalculateLeafNumber).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityPhenology.Strategies.CalculatePhyllochron).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityPhenology.Strategies.CalculatePhyllochronWOSowingCorrection).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityPhenology.Strategies.CalculatePhylSowingDateCorrection).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityPhenology.Strategies.CalculateShootNumber).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityPhenology.Strategies.CalculateVernalizationProgress).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityPhenology.Strategies.RegisterZadok).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityPhenology.Strategies.UpdateLeafFlag).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityPhenology.Strategies.UpdatePhase).FullName);
				mo0_0.AssociatedStrategies = lAssStrat0_0;
				//Adding the modeling options to the modeling options manager

				//Creating the modeling options manager of the strategy
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
				get { return "composite strategy which manage the phenology of Sirius Quality"; }
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

			

			// Getter and setters for the value of the parameters of a composite strategy
			
			public Int32 SwitchMaize
			{ 
				get {
						return _calculateleafnumber.SwitchMaize ;
				}
				set {
						_calculateleafnumber.SwitchMaize=value;
						_updatephase.SwitchMaize=value;
				}
			}
			public Double atip
			{ 
				get {
						return _calculateleafnumber.atip ;
				}
				set {
						_calculateleafnumber.atip=value;
				}
			}
			public Double Leaf_tip_emerg
			{ 
				get {
						return _calculateleafnumber.Leaf_tip_emerg ;
				}
				set {
						_calculateleafnumber.Leaf_tip_emerg=value;
				}
			}
			public Double k_bl
			{ 
				get {
						return _calculateleafnumber.k_bl ;
				}
				set {
						_calculateleafnumber.k_bl=value;
				}
			}
			public Double Nlim
			{ 
				get {
						return _calculateleafnumber.Nlim ;
				}
				set {
						_calculateleafnumber.Nlim=value;
				}
			}
			public Double Ldecr
			{ 
				get {
						return _calculatephyllochron.Ldecr ;
				}
				set {
						_calculatephyllochron.Ldecr=value;
						_calculatephyllochronwosowingcorrection.Ldecr=value;
				}
			}
			public Double Lincr
			{ 
				get {
						return _calculatephyllochron.Lincr ;
				}
				set {
						_calculatephyllochron.Lincr=value;
						_calculatephyllochronwosowingcorrection.Lincr=value;
				}
			}
			public Double Pdecr
			{ 
				get {
						return _calculatephyllochron.Pdecr ;
				}
				set {
						_calculatephyllochron.Pdecr=value;
						_calculatephyllochronwosowingcorrection.Pdecr=value;
				}
			}
			public Double Pincr
			{ 
				get {
						return _calculatephyllochron.Pincr ;
				}
				set {
						_calculatephyllochron.Pincr=value;
						_calculatephyllochronwosowingcorrection.Pincr=value;
				}
			}
			public Double Kl
			{ 
				get {
						return _calculatephyllochronwithptq.Kl ;
				}
				set {
						_calculatephyllochronwithptq.Kl=value;
				}
			}
			public Double slopePhylPTQ
			{ 
				get {
						return _calculatephyllochronwithptq.slopePhylPTQ ;
				}
				set {
						_calculatephyllochronwithptq.slopePhylPTQ=value;
				}
			}
			public Double interceptPhylPTQ
			{ 
				get {
						return _calculatephyllochronwithptq.interceptPhylPTQ ;
				}
				set {
						_calculatephyllochronwithptq.interceptPhylPTQ=value;
				}
			}
			public Double AreaSL
			{ 
				get {
						return _calculatephyllochronwithptq.AreaSL ;
				}
				set {
						_calculatephyllochronwithptq.AreaSL=value;
				}
			}
			public Double AreaSS
			{ 
				get {
						return _calculatephyllochronwithptq.AreaSS ;
				}
				set {
						_calculatephyllochronwithptq.AreaSS=value;
				}
			}
			public Double SowingDensity
			{ 
				get {
						return _calculatephyllochronwithptq.SowingDensity ;
				}
				set {
						_calculatephyllochronwithptq.SowingDensity=value;
						_calculateshootnumber.SowingDensity=value;
				}
			}
			public Double PhylPTQ1
			{ 
				get {
						return _calculatephyllochronwithptq.PhylPTQ1 ;
				}
				set {
						_calculatephyllochronwithptq.PhylPTQ1=value;
				}
			}
			public Double aPTQ
			{ 
				get {
						return _calculatephyllochronwithptq.aPTQ ;
				}
				set {
						_calculatephyllochronwithptq.aPTQ=value;
				}
			}
			public Double P
			{ 
				get {
						return _calculatephyllochronwosowingcorrection.P ;
				}
				set {
						_calculatephyllochronwosowingcorrection.P=value;
						_calculatephylsowingdatecorrection.P=value;
						_updatephase.P=value;
				}
			}
			public Int32 SowingDay
			{ 
				get {
						return _calculatephylsowingdatecorrection.SowingDay ;
				}
				set {
						_calculatephylsowingdatecorrection.SowingDay=value;
				}
			}
			public Double Latitude
			{ 
				get {
						return _calculatephylsowingdatecorrection.Latitude ;
				}
				set {
						_calculatephylsowingdatecorrection.Latitude=value;
				}
			}
			public Double SDsa_sh
			{ 
				get {
						return _calculatephylsowingdatecorrection.SDsa_sh ;
				}
				set {
						_calculatephylsowingdatecorrection.SDsa_sh=value;
				}
			}
			public Double SDws
			{ 
				get {
						return _calculatephylsowingdatecorrection.SDws ;
				}
				set {
						_calculatephylsowingdatecorrection.SDws=value;
				}
			}
			public Double SDsa_nh
			{ 
				get {
						return _calculatephylsowingdatecorrection.SDsa_nh ;
				}
				set {
						_calculatephylsowingdatecorrection.SDsa_nh=value;
				}
			}
			public Double Rp
			{ 
				get {
						return _calculatephylsowingdatecorrection.Rp ;
				}
				set {
						_calculatephylsowingdatecorrection.Rp=value;
				}
			}
			public Double TargetFertileShoot
			{ 
				get {
						return _calculateshootnumber.TargetFertileShoot ;
				}
				set {
						_calculateshootnumber.TargetFertileShoot=value;
				}
			}
			public Double AMNFLNO
			{ 
				get {
						return _calculatevernalizationprogress.AMNFLNO ;
				}
				set {
						_calculatevernalizationprogress.AMNFLNO=value;
				}
			}
			public Int32 IsVernalizable
			{ 
				get {
						return _calculatevernalizationprogress.IsVernalizable ;
				}
				set {
						_calculatevernalizationprogress.IsVernalizable=value;
						_updatephase.IsVernalizable=value;
				}
			}
			public Double MinTvern
			{ 
				get {
						return _calculatevernalizationprogress.MinTvern ;
				}
				set {
						_calculatevernalizationprogress.MinTvern=value;
				}
			}
			public Double IntTvern
			{ 
				get {
						return _calculatevernalizationprogress.IntTvern ;
				}
				set {
						_calculatevernalizationprogress.IntTvern=value;
				}
			}
			public Double VAI
			{ 
				get {
						return _calculatevernalizationprogress.VAI ;
				}
				set {
						_calculatevernalizationprogress.VAI=value;
				}
			}
			public Double VBEE
			{ 
				get {
						return _calculatevernalizationprogress.VBEE ;
				}
				set {
						_calculatevernalizationprogress.VBEE=value;
				}
			}
			public Double MinDL
			{ 
				get {
						return _calculatevernalizationprogress.MinDL ;
				}
				set {
						_calculatevernalizationprogress.MinDL=value;
				}
			}
			public Double MaxDL
			{ 
				get {
						return _calculatevernalizationprogress.MaxDL ;
				}
				set {
						_calculatevernalizationprogress.MaxDL=value;
						_updatephase.MaxDL=value;
				}
			}
			public Double MaxTvern
			{ 
				get {
						return _calculatevernalizationprogress.MaxTvern ;
				}
				set {
						_calculatevernalizationprogress.MaxTvern=value;
				}
			}
			public Double PNini
			{ 
				get {
						return _calculatevernalizationprogress.PNini ;
				}
				set {
						_calculatevernalizationprogress.PNini=value;
				}
			}
			public Double AMXLFNO
			{ 
				get {
						return _calculatevernalizationprogress.AMXLFNO ;
				}
				set {
						_calculatevernalizationprogress.AMXLFNO=value;
				}
			}
			public Double Der
			{ 
				get {
						return _registerzadok.Der ;
				}
				set {
						_registerzadok.Der=value;
				}
			}
			public Double slopeTSFLN
			{ 
				get {
						return _registerzadok.slopeTSFLN ;
				}
				set {
						_registerzadok.slopeTSFLN=value;
				}
			}
			public Double intTSFLN
			{ 
				get {
						return _registerzadok.intTSFLN ;
				}
				set {
						_registerzadok.intTSFLN=value;
				}
			}
			public Double Dse
			{ 
				get {
						return _updatephase.Dse ;
				}
				set {
						_updatephase.Dse=value;
				}
			}
			public Double PFLLAnth
			{ 
				get {
						return _updatephase.PFLLAnth ;
				}
				set {
						_updatephase.PFLLAnth=value;
				}
			}
			public Double Dcd
			{ 
				get {
						return _updatephase.Dcd ;
				}
				set {
						_updatephase.Dcd=value;
				}
			}
			public Double Dgf
			{ 
				get {
						return _updatephase.Dgf ;
				}
				set {
						_updatephase.Dgf=value;
				}
			}
			public Double Degfm
			{ 
				get {
						return _updatephase.Degfm ;
				}
				set {
						_updatephase.Degfm=value;
				}
			}
			public Double SLDL
			{ 
				get {
						return _updatephase.SLDL ;
				}
				set {
						_updatephase.SLDL=value;
				}
			}
			public Int32 IgnoreGrainMaturation
			{ 
				get {
						return _updatephase.IgnoreGrainMaturation ;
				}
				set {
						_updatephase.IgnoreGrainMaturation=value;
				}
			}
			public Double PHEADANTH
			{ 
				get {
						return _updatephase.PHEADANTH ;
				}
				set {
						_updatephase.PHEADANTH=value;
				}
			}
			public String choosePhyllUse
			{ 
				get {
						return _updatephase.choosePhyllUse ;
				}
				set {
						_updatephase.choosePhyllUse=value;
				}
			}

	#endregion		

	
	#region Parameters initialization method
			
            /// <summary>
            /// Set parameter(s) current values to the default value
            /// </summary>
            public void SetParametersDefaultValue()
            {
				_modellingOptionsManager.SetParametersDefaultValue();
				
					_calculateleafnumber.SetParametersDefaultValue();
					_calculatephyllochron.SetParametersDefaultValue();
					_calculatephyllochronwithptq.SetParametersDefaultValue();
					_calculatephyllochronwosowingcorrection.SetParametersDefaultValue();
					_calculatephylsowingdatecorrection.SetParametersDefaultValue();
					_calculateshootnumber.SetParametersDefaultValue();
					_calculatevernalizationprogress.SetParametersDefaultValue();
					_registerzadok.SetParametersDefaultValue();
					_updateleafflag.SetParametersDefaultValue();
					_updatephase.SetParametersDefaultValue(); 

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
                
       
			}

			//Parameters static VarInfo list 
								
			
			//Parameters static VarInfo list of the composite class
			
				/// <summary> 
				///SwitchMaize VarInfo definition
				/// </summary>
				public static VarInfo SwitchMaizeVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateLeafNumber.SwitchMaizeVarInfo; }
				}
				/// <summary> 
				///atip VarInfo definition
				/// </summary>
				public static VarInfo atipVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateLeafNumber.atipVarInfo; }
				}
				/// <summary> 
				///Leaf_tip_emerg VarInfo definition
				/// </summary>
				public static VarInfo Leaf_tip_emergVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateLeafNumber.Leaf_tip_emergVarInfo; }
				}
				/// <summary> 
				///k_bl VarInfo definition
				/// </summary>
				public static VarInfo k_blVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateLeafNumber.k_blVarInfo; }
				}
				/// <summary> 
				///Nlim VarInfo definition
				/// </summary>
				public static VarInfo NlimVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateLeafNumber.NlimVarInfo; }
				}
				/// <summary> 
				///Ldecr VarInfo definition
				/// </summary>
				public static VarInfo LdecrVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochron.LdecrVarInfo; }
				}
				/// <summary> 
				///Lincr VarInfo definition
				/// </summary>
				public static VarInfo LincrVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochron.LincrVarInfo; }
				}
				/// <summary> 
				///Pdecr VarInfo definition
				/// </summary>
				public static VarInfo PdecrVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochron.PdecrVarInfo; }
				}
				/// <summary> 
				///Pincr VarInfo definition
				/// </summary>
				public static VarInfo PincrVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochron.PincrVarInfo; }
				}
				/// <summary> 
				///Kl VarInfo definition
				/// </summary>
				public static VarInfo KlVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ.KlVarInfo; }
				}
				/// <summary> 
				///slopePhylPTQ VarInfo definition
				/// </summary>
				public static VarInfo slopePhylPTQVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ.slopePhylPTQVarInfo; }
				}
				/// <summary> 
				///interceptPhylPTQ VarInfo definition
				/// </summary>
				public static VarInfo interceptPhylPTQVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ.interceptPhylPTQVarInfo; }
				}
				/// <summary> 
				///AreaSL VarInfo definition
				/// </summary>
				public static VarInfo AreaSLVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ.AreaSLVarInfo; }
				}
				/// <summary> 
				///AreaSS VarInfo definition
				/// </summary>
				public static VarInfo AreaSSVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ.AreaSSVarInfo; }
				}
				/// <summary> 
				///SowingDensity VarInfo definition
				/// </summary>
				public static VarInfo SowingDensityVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ.SowingDensityVarInfo; }
				}
				/// <summary> 
				///PhylPTQ1 VarInfo definition
				/// </summary>
				public static VarInfo PhylPTQ1VarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ.PhylPTQ1VarInfo; }
				}
				/// <summary> 
				///aPTQ VarInfo definition
				/// </summary>
				public static VarInfo aPTQVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ.aPTQVarInfo; }
				}
				/// <summary> 
				///P VarInfo definition
				/// </summary>
				public static VarInfo PVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhyllochronWOSowingCorrection.PVarInfo; }
				}
				/// <summary> 
				///SowingDay VarInfo definition
				/// </summary>
				public static VarInfo SowingDayVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhylSowingDateCorrection.SowingDayVarInfo; }
				}
				/// <summary> 
				///Latitude VarInfo definition
				/// </summary>
				public static VarInfo LatitudeVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhylSowingDateCorrection.LatitudeVarInfo; }
				}
				/// <summary> 
				///SDsa_sh VarInfo definition
				/// </summary>
				public static VarInfo SDsa_shVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhylSowingDateCorrection.SDsa_shVarInfo; }
				}
				/// <summary> 
				///SDws VarInfo definition
				/// </summary>
				public static VarInfo SDwsVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhylSowingDateCorrection.SDwsVarInfo; }
				}
				/// <summary> 
				///SDsa_nh VarInfo definition
				/// </summary>
				public static VarInfo SDsa_nhVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhylSowingDateCorrection.SDsa_nhVarInfo; }
				}
				/// <summary> 
				///Rp VarInfo definition
				/// </summary>
				public static VarInfo RpVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculatePhylSowingDateCorrection.RpVarInfo; }
				}
				/// <summary> 
				///TargetFertileShoot VarInfo definition
				/// </summary>
				public static VarInfo TargetFertileShootVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateShootNumber.TargetFertileShootVarInfo; }
				}
				/// <summary> 
				///AMNFLNO VarInfo definition
				/// </summary>
				public static VarInfo AMNFLNOVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateVernalizationProgress.AMNFLNOVarInfo; }
				}
				/// <summary> 
				///IsVernalizable VarInfo definition
				/// </summary>
				public static VarInfo IsVernalizableVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateVernalizationProgress.IsVernalizableVarInfo; }
				}
				/// <summary> 
				///MinTvern VarInfo definition
				/// </summary>
				public static VarInfo MinTvernVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateVernalizationProgress.MinTvernVarInfo; }
				}
				/// <summary> 
				///IntTvern VarInfo definition
				/// </summary>
				public static VarInfo IntTvernVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateVernalizationProgress.IntTvernVarInfo; }
				}
				/// <summary> 
				///VAI VarInfo definition
				/// </summary>
				public static VarInfo VAIVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateVernalizationProgress.VAIVarInfo; }
				}
				/// <summary> 
				///VBEE VarInfo definition
				/// </summary>
				public static VarInfo VBEEVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateVernalizationProgress.VBEEVarInfo; }
				}
				/// <summary> 
				///MinDL VarInfo definition
				/// </summary>
				public static VarInfo MinDLVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateVernalizationProgress.MinDLVarInfo; }
				}
				/// <summary> 
				///MaxDL VarInfo definition
				/// </summary>
				public static VarInfo MaxDLVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateVernalizationProgress.MaxDLVarInfo; }
				}
				/// <summary> 
				///MaxTvern VarInfo definition
				/// </summary>
				public static VarInfo MaxTvernVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateVernalizationProgress.MaxTvernVarInfo; }
				}
				/// <summary> 
				///PNini VarInfo definition
				/// </summary>
				public static VarInfo PNiniVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateVernalizationProgress.PNiniVarInfo; }
				}
				/// <summary> 
				///AMXLFNO VarInfo definition
				/// </summary>
				public static VarInfo AMXLFNOVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.CalculateVernalizationProgress.AMXLFNOVarInfo; }
				}
				/// <summary> 
				///Der VarInfo definition
				/// </summary>
				public static VarInfo DerVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.RegisterZadok.DerVarInfo; }
				}
				/// <summary> 
				///slopeTSFLN VarInfo definition
				/// </summary>
				public static VarInfo slopeTSFLNVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.RegisterZadok.slopeTSFLNVarInfo; }
				}
				/// <summary> 
				///intTSFLN VarInfo definition
				/// </summary>
				public static VarInfo intTSFLNVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.RegisterZadok.intTSFLNVarInfo; }
				}
				/// <summary> 
				///Dse VarInfo definition
				/// </summary>
				public static VarInfo DseVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.UpdatePhase.DseVarInfo; }
				}
				/// <summary> 
				///PFLLAnth VarInfo definition
				/// </summary>
				public static VarInfo PFLLAnthVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.UpdatePhase.PFLLAnthVarInfo; }
				}
				/// <summary> 
				///Dcd VarInfo definition
				/// </summary>
				public static VarInfo DcdVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.UpdatePhase.DcdVarInfo; }
				}
				/// <summary> 
				///Dgf VarInfo definition
				/// </summary>
				public static VarInfo DgfVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.UpdatePhase.DgfVarInfo; }
				}
				/// <summary> 
				///Degfm VarInfo definition
				/// </summary>
				public static VarInfo DegfmVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.UpdatePhase.DegfmVarInfo; }
				}
				/// <summary> 
				///SLDL VarInfo definition
				/// </summary>
				public static VarInfo SLDLVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.UpdatePhase.SLDLVarInfo; }
				}
				/// <summary> 
				///IgnoreGrainMaturation VarInfo definition
				/// </summary>
				public static VarInfo IgnoreGrainMaturationVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.UpdatePhase.IgnoreGrainMaturationVarInfo; }
				}
				/// <summary> 
				///PHEADANTH VarInfo definition
				/// </summary>
				public static VarInfo PHEADANTHVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.UpdatePhase.PHEADANTHVarInfo; }
				}
				/// <summary> 
				///choosePhyllUse VarInfo definition
				/// </summary>
				public static VarInfo choosePhyllUseVarInfo
				{
					get { return SiriusQualityPhenology.Strategies.UpdatePhase.choosePhyllUseVarInfo; }
				}			

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
					
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					

					
					string ret = "";
					 ret += _calculateleafnumber.TestPostConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculateLeafNumber");
					 ret += _calculatephyllochron.TestPostConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculatePhyllochron");
					 ret += _calculatephyllochronwithptq.TestPostConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ");
					 ret += _calculatephyllochronwosowingcorrection.TestPostConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculatePhyllochronWOSowingCorrection");
					 ret += _calculatephylsowingdatecorrection.TestPostConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculatePhylSowingDateCorrection");
					 ret += _calculateshootnumber.TestPostConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculateShootNumber");
					 ret += _calculatevernalizationprogress.TestPostConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculateVernalizationProgress");
					 ret += _registerzadok.TestPostConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.RegisterZadok");
					 ret += _updateleafflag.TestPostConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.UpdateLeafFlag");
					 ret += _updatephase.TestPostConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.UpdatePhase");
					if (ret != "") { pre.TestsOut(ret, true, "   postconditions tests of associated classes"); }

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
            
					

					
					string ret = "";
					 ret += _calculateleafnumber.TestPreConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculateLeafNumber");
					 ret += _calculatephyllochron.TestPreConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculatePhyllochron");
					 ret += _calculatephyllochronwithptq.TestPreConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ");
					 ret += _calculatephyllochronwosowingcorrection.TestPreConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculatePhyllochronWOSowingCorrection");
					 ret += _calculatephylsowingdatecorrection.TestPreConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculatePhylSowingDateCorrection");
					 ret += _calculateshootnumber.TestPreConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculateShootNumber");
					 ret += _calculatevernalizationprogress.TestPreConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.CalculateVernalizationProgress");
					 ret += _registerzadok.TestPreConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.RegisterZadok");
					 ret += _updateleafflag.TestPreConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.UpdateLeafFlag");
					 ret += _updatephase.TestPreConditions(phenologystate,phenologystate1, "strategy SiriusQualityPhenology.Strategies.UpdatePhase");
					if (ret != "") { pre.TestsOut(ret, true, "   preconditions tests of associated classes"); }

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
				
					EstimateOfAssociatedClasses(phenologystate,phenologystate1,actevents);

				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section1
				//Code written below will not be overwritten by a future code generation

        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}


            #region Composite class: associations

            //Declaration of the associated strategies
            SiriusQualityPhenology.Strategies.CalculateLeafNumber _calculateleafnumber = new SiriusQualityPhenology.Strategies.CalculateLeafNumber();
            SiriusQualityPhenology.Strategies.CalculatePhyllochron _calculatephyllochron = new SiriusQualityPhenology.Strategies.CalculatePhyllochron();
            SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ _calculatephyllochronwithptq = new SiriusQualityPhenology.Strategies.CalculatePhyllochronWithPTQ();
            SiriusQualityPhenology.Strategies.CalculatePhyllochronWOSowingCorrection _calculatephyllochronwosowingcorrection = new SiriusQualityPhenology.Strategies.CalculatePhyllochronWOSowingCorrection();
            SiriusQualityPhenology.Strategies.CalculatePhylSowingDateCorrection _calculatephylsowingdatecorrection = new SiriusQualityPhenology.Strategies.CalculatePhylSowingDateCorrection();
            SiriusQualityPhenology.Strategies.CalculateShootNumber _calculateshootnumber = new SiriusQualityPhenology.Strategies.CalculateShootNumber();
            SiriusQualityPhenology.Strategies.RegisterZadok _registerzadok = new SiriusQualityPhenology.Strategies.RegisterZadok();
            SiriusQualityPhenology.Strategies.CalculateVernalizationProgress _calculatevernalizationprogress = new SiriusQualityPhenology.Strategies.CalculateVernalizationProgress();
            SiriusQualityPhenology.Strategies.UpdateLeafFlag _updateleafflag = new SiriusQualityPhenology.Strategies.UpdateLeafFlag();
            SiriusQualityPhenology.Strategies.UpdatePhase _updatephase = new SiriusQualityPhenology.Strategies.UpdatePhase();

            //Call of the associated strategies
            private void EstimateOfAssociatedClasses(SiriusQualityPhenology.PhenologyState phenologystate, SiriusQualityPhenology.PhenologyState phenologystate1, CRA.AgroManagement.ActEvents actevents)
            {

                if (phenologystate.cumulTT.Count < 1) { throw new ArgumentException("cumulTT must have at least one element"); }

                //previousphenologystate.Calendar = phenologystate.Calendar;
                //previousphenologystate.LeafNumber = phenologystate.LeafNumber;

                phenologystate.LeafNumber = phenologystate1.LeafNumber;

                phenologystate.isMomentRegistredZC_39 = phenologystate1.Calendar.IsMomentRegistred(GrowthStage.ZC_39_FlagLeafLiguleJustVisible);

                _calculatephylsowingdatecorrection.Estimate(phenologystate, phenologystate1, actevents);

                if (phenologystate1.Calendar.IsMomentRegistred(GrowthStage.ZC_65_Anthesis) == 1)
                {
                    if (SwitchMaize == 0)
                    {
                        phenologystate.cumulTTFromZC_65 = phenologystate1.Calendar.cumulTTFrom(0, GrowthStage.ZC_65_Anthesis, phenologystate.cumulTT[0]);
                    }
                    else
                    {
                        phenologystate.cumulTTFromZC_65 = phenologystate1.Calendar.cumulTTFrom(6, GrowthStage.ZC_65_Anthesis, phenologystate.cumulTT[6]);
                    }
                }
                if (phenologystate1.Calendar.IsMomentRegistred(GrowthStage.ZC_39_FlagLeafLiguleJustVisible) == 1)
                {
                    if (SwitchMaize == 0)
                    {
                        phenologystate.cumulTTFromZC_39 = phenologystate1.Calendar.cumulTTFrom(0, GrowthStage.ZC_39_FlagLeafLiguleJustVisible, phenologystate.cumulTT[0]);
                    }
                    else
                    {
                        phenologystate.cumulTTFromZC_39 = phenologystate1.Calendar.cumulTTFrom(6, GrowthStage.ZC_39_FlagLeafLiguleJustVisible, phenologystate.cumulTT[6]);
                    }
                }
                if (phenologystate1.Calendar.IsMomentRegistred(GrowthStage.ZC_91_EndGrainFilling) == 1)
                {
                    if (SwitchMaize == 0)
                    {
                        phenologystate.cumulTTFromZC_91 = phenologystate1.Calendar.cumulTTFrom(0, GrowthStage.ZC_91_EndGrainFilling, phenologystate.cumulTT[0]);
                    }
                    else
                    {
                        phenologystate.cumulTTFromZC_91 = phenologystate1.Calendar.cumulTTFrom(6, GrowthStage.ZC_91_EndGrainFilling, phenologystate.cumulTT[6]);
                    }
                }

                if (choosePhyllUse == "PTQ") _calculatephyllochronwithptq.Estimate(phenologystate, phenologystate1, actevents);

                phenologystate.Calendar = phenologystate1.Calendar;
                _calculatevernalizationprogress.Estimate(phenologystate, phenologystate1, actevents);
                phenologystate1.Calendar = phenologystate.Calendar;
                _updatephase.Estimate(phenologystate, phenologystate1, actevents);


                if (phenologystate.phase_.phaseValue == 1 && phenologystate.cumulTTPhenoMaizeAtEmergence == 0)
                {
                    phenologystate.cumulTTPhenoMaizeAtEmergence = phenologystate.cumulTT[6];
                }


                if (phenologystate.phase_.phaseValue >= 1 && phenologystate.phase_.phaseValue < 4)
                {

                    _calculateleafnumber.Estimate(phenologystate, phenologystate1, actevents);
                    _updateleafflag.Estimate(phenologystate, phenologystate1, actevents);//need to be called after the update of the LeafNumber
                    phenologystate1.Calendar = phenologystate.Calendar;
                }


                // _calculatephyllochron.Estimate(phenologystate, phenologystate1, actevents);

                if (choosePhyllUse == "Default") _calculatephyllochron.Estimate(phenologystate, phenologystate1, actevents);
                else if (choosePhyllUse == "Test") _calculatephyllochronwosowingcorrection.Estimate(phenologystate, phenologystate1, actevents);

                _registerzadok.Estimate(phenologystate, phenologystate1, actevents);

                //need to update the calendar after the update of the phase
                if (phenologystate.Calendar.IsMomentRegistred(phenologystate.phase_.PreviousMoment()) == 0)
                {
                    GrowthStage stage = phenologystate.Calendar.LastGrowthStageSet;
                    phenologystate.Calendar.Set(phenologystate.phase_.PreviousMoment(), phenologystate.currentdate, phenologystate.cumulTT);
                    if (phenologystate.hasZadokStageChanged == 1)//if a new zadok stage and  a new phase stage happen the same day, we set the lastGrowthStage to the zadock one.
                    {
                        phenologystate.Calendar.LastGrowthStageSet = stage;
                    }
                }

                //testBeginningStemExtension;
                if (phenologystate.Calendar[GrowthStage.BeginningStemExtension] == null)
                {
                    if (phenologystate.IsLatestLeafInternodeLengthPotPositive == 1)
                    {
                        GrowthStage growthStage = phenologystate.Calendar.LastGrowthStageSet;
                        phenologystate.Calendar.Set(GrowthStage.BeginningStemExtension, phenologystate.currentdate, phenologystate.cumulTT);
                        if (phenologystate.hasZadokStageChanged == 1)
                        {
                            phenologystate.Calendar.LastGrowthStageSet = growthStage;
                        }
                    }
                }

                _calculateshootnumber.Estimate(phenologystate, phenologystate1, actevents);

            }
            #endregion


	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation

                ///<param name="cumulTTatSowing"> array of the different types of cumulTT at sowing date which will be saved in the calendar</param>
                public void Init(List<double> cumulTTatSowing, DateTime sowingDate, SiriusQualityPhenology.PhenologyState phenologystate1)
                    {
                        //recordSowing
                        //Debug
                        phenologystate1.Calendar.Set(GrowthStage.ZC_00_Sowing, sowingDate, cumulTTatSowing);
                        phenologystate1.MinFinalNumber = AMNFLNO;
                        //Debug

                        _calculateshootnumber.Init(phenologystate1);


                    }

                //public void EstimateFixphyll(SiriusQualityPhenology.PhenologyState phenologystate, SiriusQualityPhenology.PhenologyState phenologystate1, CRA.AgroManagement.ActEvents actevents)
                //{
                //    _calculatephylsowingdatecorrection.Estimate(phenologystate, phenologystate1, actevents);
                //}

                /// <summary>
                /// copy constructor
                /// </summary> 
                public Phenology( Phenology toCopy): this()
                {
                    //we only need to copy the parameters (the strategies being stateless)
                    AMNFLNO =toCopy.AMNFLNO;
                    IsVernalizable =toCopy.IsVernalizable;
                    MinTvern =toCopy.MinTvern;
                    IntTvern =toCopy.IntTvern;
                    VAI =toCopy.VAI;
                    VBEE =toCopy.VBEE;
                    MinDL =toCopy.MinDL;
                    MaxDL =toCopy.MaxDL;
                    MaxTvern =toCopy.MaxTvern;
                    PNini =toCopy.PNini;
                    AMXLFNO =toCopy.AMXLFNO;
                    Dse =toCopy.Dse;
                    PFLLAnth =toCopy.PFLLAnth;
                    PHEADANTH = toCopy.PHEADANTH;
                    //FixPhyll =toCopy.FixPhyll;
                    Dcd =toCopy.Dcd;
                    Dgf =toCopy.Dgf;
                    Degfm =toCopy.Degfm;
                    SLDL = toCopy.SLDL;
                    IgnoreGrainMaturation = toCopy.IgnoreGrainMaturation;
                    Ldecr = toCopy.Ldecr;
                    Lincr = toCopy.Lincr;
                    Pdecr = toCopy.Pdecr;
                    Pincr = toCopy.Pincr;
                    Der = toCopy.Der;
                    SwitchMaize = toCopy.SwitchMaize;
                    SowingDensity = toCopy.SowingDensity;
                    TargetFertileShoot = toCopy.TargetFertileShoot;
                    slopeTSFLN = toCopy.slopeTSFLN;
                    intTSFLN = toCopy.intTSFLN;
                    choosePhyllUse = toCopy.choosePhyllUse;
                    //interceptPhylPTQ = toCopy.interceptPhylPTQ;
                    //slopePhylPTQ = toCopy.slopePhylPTQ;
                    aPTQ = toCopy.aPTQ;
                    Kl = toCopy.Kl;
                    //LphylPTQ = toCopy.LphylPTQ;
                    PhylPTQ1 = toCopy.PhylPTQ1;
                    AreaSL = toCopy.AreaSL;
                    AreaSS = toCopy.AreaSS;
                    SowingDay = toCopy.SowingDay;
                    Latitude = toCopy.Latitude;
                    Latitude = toCopy.SDsa_sh;
                    P = toCopy.P;
                    Rp = toCopy.Rp;
                    SDsa_sh = toCopy.SDsa_sh;
                    SDws = toCopy.SDws;
                    SDsa_nh = toCopy.SDsa_nh;
                    //TagPhenoWarnOut = toCopy.TagPhenoWarnOut;

                    if (toCopy.SwitchMaize==1)
                    {

                        atip = toCopy.atip;
                        Leaf_tip_emerg = toCopy.Leaf_tip_emerg;
                        k_bl = toCopy.k_bl;
                        Nlim = toCopy.Nlim;
                    }

                }
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

/// 
/// This class was created from file C:\Users\mancealo\Documents\Sirius Quality\branches\Development2\Code\SiriusQuality-PhenologyComponent\XML\SiriusQualityPhenology_PhenologyState.xml
/// The tool used was: DCC - Domain Class Coder, http://components.biomamodelling.org/, DCC
/// 
/// Loic Manceau
/// loic.manceau@inra.fr
/// INRA
/// 
/// 
/// 4/26/2018 3:03:27 PM
/// 
namespace SiriusQualityPhenology
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using CRA.ModelLayer.Core;
    using CRA.ModelLayer.ParametersManagement;
    
    
    /// <summary>PhenologyState Domain class contains the accessors to values</summary>
    [Serializable()]
    public class PhenologyState : ICloneable, IDomainClass
    {
        
        #region Private fields
        private System.DateTime _currentdate = new DateTime();
        
        private System.Collections.Generic.List<double> _cumulTT = new List<double>();
        
        private double _DayLength;
        
        private double _DeltaTT;
        
        private double _GrainCumulTT;
        
        private double _GAI;
        
        private int _IsLatestLeafInternodeLengthPotPositive;
        
        private Calendar _Calendar;
        
        private double _LeafNumber;
        
        private double _FinalLeafNumber;
        
        private Phase _phase_;
        
        private double _Phyllochron;
        
        private double _Vernaprog;
        
        private int _HasFlagLeafLiguleAppeared;
        
        private double _MinFinalNumber;
        
        private int _hasLastPrimordiumAppeared;
        
        private int _isMomentRegistredZC_39;
        
        private double _cumulTTFromZC_39;
        
        private double _cumulTTFromZC_91;
        
        private double _cumulTTFromZC_65;
        
        private GrowthStage _currentZadokStage;
        
        private int _hasZadokStageChanged;
        
        private System.Collections.Generic.List<double> _tilleringProfile = new List<double>();
        
        private System.Collections.Generic.List<double> _leafTillerNumberArray = new List<double>();
        
        private double _CanopyShootNumber;
        
        private double _AverageShootNumberPerPlant;
        
        private int _TillerNumber;
        
        private double _Ntip;
        
        private double _cumulTTPhenoMaizeAtEmergence;
        
        private double _PTQ;
        
        private double _Fixphyll;
        
        private double _pastMaxAI;
        #endregion
        
        #region Private field for properties
        private ParametersIO _parametersIO;
        #endregion
        
        #region Constructor

        /// <summary>No parameters constructor</summary>
        public PhenologyState()
        {
            _parametersIO = new ParametersIO(this);
            _phase_ = new Phase();
            _Calendar = new Calendar();
        }

        /// <summary>copy constructor</summary>
        public PhenologyState(PhenologyState toCopy, bool copyAll)
        {
            _parametersIO = new ParametersIO(this);
            _phase_ = (toCopy._phase_ != null) ? new Phase(toCopy._phase_) : null;
            _Calendar = (toCopy._Calendar != null) ? new Calendar(toCopy._Calendar) : null;
            _currentdate = toCopy._currentdate;
            _cumulTT = toCopy._cumulTT;
            _DayLength = toCopy._DayLength;
            _DeltaTT = toCopy._DeltaTT;
            _GrainCumulTT = toCopy._GrainCumulTT;
            _GAI = toCopy._GAI;
            _PTQ = toCopy._PTQ;
            _pastMaxAI = toCopy._pastMaxAI;
            _Fixphyll = toCopy._Fixphyll;
            _LeafNumber = toCopy._LeafNumber;
            //_TagPhenoWarnOut = toCopy._TagPhenoWarnOut;
            _FinalLeafNumber = toCopy._FinalLeafNumber;
            _Phyllochron = toCopy._Phyllochron;
            _Vernaprog = toCopy._Vernaprog;
            _currentZadokStage = toCopy._currentZadokStage;
            _CanopyShootNumber = toCopy._CanopyShootNumber;
            _TillerNumber = toCopy._TillerNumber;
            _AverageShootNumberPerPlant = toCopy._AverageShootNumberPerPlant;
            tilleringProfile = new List<double>();
            for (int i = 0; i < toCopy.tilleringProfile.Count; i++)
            {
                tilleringProfile.Add(toCopy.tilleringProfile[i]);
            }
            leafTillerNumberArray = new List<double>();
            for (int i = 0; i < toCopy.leafTillerNumberArray.Count; i++)
            {
                leafTillerNumberArray.Add(toCopy.leafTillerNumberArray[i]);
            }
            if (copyAll)
            {
                _Ntip = toCopy._Ntip;
                _hasLastPrimordiumAppeared = toCopy._hasLastPrimordiumAppeared;
                _isMomentRegistredZC_39 = toCopy._isMomentRegistredZC_39;
                _cumulTTFromZC_39 = toCopy._cumulTTFromZC_39;
                _cumulTTFromZC_91 = toCopy._cumulTTFromZC_91;
                _cumulTTFromZC_65 = toCopy._cumulTTFromZC_65;
                _IsLatestLeafInternodeLengthPotPositive = toCopy._IsLatestLeafInternodeLengthPotPositive;
                _HasFlagLeafLiguleAppeared = toCopy._HasFlagLeafLiguleAppeared;
                _MinFinalNumber = toCopy._MinFinalNumber;
                _cumulTTPhenoMaizeAtEmergence = toCopy._cumulTTPhenoMaizeAtEmergence;
                _hasZadokStageChanged = toCopy._hasZadokStageChanged;
            }
        }

        #endregion
        
        #region Public properties
        /// <summary>current date</summary>
        public System.DateTime currentdate
        {
            get
            {
                return this._currentdate;
            }
            set
            {
                this._currentdate = value;
            }
        }
        
        /// <summary>array of the different cumulTT to be saved in the calendar. The first element is the one used to advance the phenology</summary>
        public System.Collections.Generic.List<double> cumulTT
        {
            get
            {
                return this._cumulTT;
            }
            set
            {
                this._cumulTT = value;
            }
        }
        
        /// <summary>length of the day</summary>
        public double DayLength
        {
            get
            {
                return this._DayLength;
            }
            set
            {
                this._DayLength = value;
            }
        }
        
        /// <summary>daily delta TT . It is the same kind of TT as cumulTT[0]</summary>
        public double DeltaTT
        {
            get
            {
                return this._DeltaTT;
            }
            set
            {
                this._DeltaTT = value;
            }
        }
        
        /// <summary>cumulTT used for the grain developpment</summary>
        public double GrainCumulTT
        {
            get
            {
                return this._GrainCumulTT;
            }
            set
            {
                this._GrainCumulTT = value;
            }
        }
        
        /// <summary>green area index</summary>
        public double GAI
        {
            get
            {
                return this._GAI;
            }
            set
            {
                this._GAI = value;
            }
        }
        
        /// <summary>true if the potential length of the latest leaf's internode is positive. Used to test for the beginning stem expension</summary>
        public int IsLatestLeafInternodeLengthPotPositive
        {
            get
            {
                return this._IsLatestLeafInternodeLengthPotPositive;
            }
            set
            {
                this._IsLatestLeafInternodeLengthPotPositive = value;
            }
        }
        
        /// <summary>Dictionnary containing for each stage the date it occurs as well as a copy of all types of cumulated thermal times</summary>
        public Calendar Calendar
        {
            get
            {
                return this._Calendar;
            }
            set
            {
                this._Calendar = value;
            }
        }
        
        /// <summary>Actual number of phytomers</summary>
        public double LeafNumber
        {
            get
            {
                return this._LeafNumber;
            }
            set
            {
                this._LeafNumber = value;
            }
        }
        
        /// <summary>final leaf number</summary>
        public double FinalLeafNumber
        {
            get
            {
                return this._FinalLeafNumber;
            }
            set
            {
                this._FinalLeafNumber = value;
            }
        }
        
        /// <summary>instance of the phase class . You can get the name of the phase using phase.getPhaseAsString(PhaseValue)</summary>
        public Phase phase_
        {
            get
            {
                return this._phase_;
            }
            set
            {
                this._phase_ = value;
            }
        }
        
        /// <summary>Phyllochron</summary>
        public double Phyllochron
        {
            get
            {
                return this._Phyllochron;
            }
            set
            {
                this._Phyllochron = value;
            }
        }
        
        /// <summary>progression on a 0  to 1 scale of the vernalization</summary>
        public double Vernaprog
        {
            get
            {
                return this._Vernaprog;
            }
            set
            {
                this._Vernaprog = value;
            }
        }
        
        /// <summary>true if flag leaf has appeared (leafnumber reached finalLeafNumber)</summary>
        public int HasFlagLeafLiguleAppeared
        {
            get
            {
                return this._HasFlagLeafLiguleAppeared;
            }
            set
            {
                this._HasFlagLeafLiguleAppeared = value;
            }
        }
        
        /// <summary>minimum final leaf number</summary>
        public double MinFinalNumber
        {
            get
            {
                return this._MinFinalNumber;
            }
            set
            {
                this._MinFinalNumber = value;
            }
        }
        
        /// <summary>true if the last primordium has appeared</summary>
        public int hasLastPrimordiumAppeared
        {
            get
            {
                return this._hasLastPrimordiumAppeared;
            }
            set
            {
                this._hasLastPrimordiumAppeared = value;
            }
        }
        
        /// <summary>true if ZC_39 is regitered in the calendar</summary>
        public int isMomentRegistredZC_39
        {
            get
            {
                return this._isMomentRegistredZC_39;
            }
            set
            {
                this._isMomentRegistredZC_39 = value;
            }
        }
        
        /// <summary>cumul of the thermal time ( DeltaTT) since the moment ZC_39</summary>
        public double cumulTTFromZC_39
        {
            get
            {
                return this._cumulTTFromZC_39;
            }
            set
            {
                this._cumulTTFromZC_39 = value;
            }
        }
        
        /// <summary>cumul of the thermal time (DeltaTT) since the moment ZC_91</summary>
        public double cumulTTFromZC_91
        {
            get
            {
                return this._cumulTTFromZC_91;
            }
            set
            {
                this._cumulTTFromZC_91 = value;
            }
        }
        
        /// <summary>cumul of the thermal time (DeltaTT) since the moment ZC_65</summary>
        public double cumulTTFromZC_65
        {
            get
            {
                return this._cumulTTFromZC_65;
            }
            set
            {
                this._cumulTTFromZC_65 = value;
            }
        }
        
        /// <summary>current zadok stage see the definition of "GrowthStage" in the Phase class</summary>
        public GrowthStage currentZadokStage
        {
            get
            {
                return this._currentZadokStage;
            }
            set
            {
                this._currentZadokStage = value;
            }
        }
        
        /// <summary>true if the zadok stage has changed this time step</summary>
        public int hasZadokStageChanged
        {
            get
            {
                return this._hasZadokStageChanged;
            }
            set
            {
                this._hasZadokStageChanged = value;
            }
        }
        
        /// <summary>store the amount of new tiller created at each time a new tiller appears</summary>
        public System.Collections.Generic.List<double> tilleringProfile
        {
            get
            {
                return this._tilleringProfile;
            }
            set
            {
                this._tilleringProfile = value;
            }
        }
        
        /// <summary>store the number of tiller for each leaf layer</summary>
        public System.Collections.Generic.List<double> leafTillerNumberArray
        {
            get
            {
                return this._leafTillerNumberArray;
            }
            set
            {
                this._leafTillerNumberArray = value;
            }
        }
        
        /// <summary>shoot number for the whole canopy</summary>
        public double CanopyShootNumber
        {
            get
            {
                return this._CanopyShootNumber;
            }
            set
            {
                this._CanopyShootNumber = value;
            }
        }
        
        /// <summary>average shoot number per plant in the canopy</summary>
        public double AverageShootNumberPerPlant
        {
            get
            {
                return this._AverageShootNumberPerPlant;
            }
            set
            {
                this._AverageShootNumberPerPlant = value;
            }
        }
        
        /// <summary>number of tiller which have appeared</summary>
        public int TillerNumber
        {
            get
            {
                return this._TillerNumber;
            }
            set
            {
                this._TillerNumber = value;
            }
        }
        
        /// <summary>Maize number of tip</summary>
        public double Ntip
        {
            get
            {
                return this._Ntip;
            }
            set
            {
                this._Ntip = value;
            }
        }
        
        /// <summary>Cumul thermal time for maïze since emergence</summary>
        public double cumulTTPhenoMaizeAtEmergence
        {
            get
            {
                return this._cumulTTPhenoMaizeAtEmergence;
            }
            set
            {
                this._cumulTTPhenoMaizeAtEmergence = value;
            }
        }
        
        /// <summary>Photothermal Quotient</summary>
        public double PTQ
        {
            get
            {
                return this._PTQ;
            }
            set
            {
                this._PTQ = value;
            }
        }
        
        /// <summary>Phyllochron with sowing date fix</summary>
        public double Fixphyll
        {
            get
            {
                return this._Fixphyll;
            }
            set
            {
                this._Fixphyll = value;
            }
        }
        
        /// <summary>Past maximum GAI</summary>
        public double pastMaxAI
        {
            get
            {
                return this._pastMaxAI;
            }
            set
            {
                this._pastMaxAI = value;
            }
        }
        #endregion
        
        #region IDomainClass members
        /// <summary>Domain Class description</summary>
        public virtual  string Description
        {
            get
            {
                return "Domain class for the phenology component of Sirius Quality";
            }
        }
        
        /// <summary>Domain Class URL</summary>
        public virtual  string URL
        {
            get
            {
                return "http://";
            }
        }
        
        /// <summary>Domain Class Properties</summary>
        public virtual IDictionary<string, PropertyInfo> PropertiesDescription
        {
            get
            {
                return _parametersIO.GetCachedProperties(typeof(IDomainClass));
            }
        }
        #endregion
        
        /// <summary>Clears the values of the properties of the domain class by using the default value for the type of each property (e.g '0' for numbers, 'the empty string' for strings, etc.)</summary>
        public virtual Boolean ClearValues()
        {
            _currentdate = new DateTime();
            _cumulTT = new List<double>();
            _DayLength = default(System.Double);
            _DeltaTT = default(System.Double);
            _GrainCumulTT = default(System.Double);
            _GAI = default(System.Double);
            _IsLatestLeafInternodeLengthPotPositive = default(System.Int32);
            _Calendar = new Calendar();
            _LeafNumber = default(System.Double);
            _FinalLeafNumber = default(System.Double);
            _phase_ = new Phase();
            _Phyllochron = default(System.Double);
            _Vernaprog = default(System.Double);
            _HasFlagLeafLiguleAppeared = default(System.Int32);
            _MinFinalNumber = default(System.Double);
            _hasLastPrimordiumAppeared = default(System.Int32);
            _isMomentRegistredZC_39 = default(System.Int32);
            _cumulTTFromZC_39 = default(System.Double);
            _cumulTTFromZC_91 = default(System.Double);
            _cumulTTFromZC_65 = default(System.Double);
            _currentZadokStage = new GrowthStage();
            _hasZadokStageChanged = default(System.Int32);
            _tilleringProfile = new List<double>();
            _leafTillerNumberArray = new List<double>();
            _CanopyShootNumber = default(System.Double);
            _AverageShootNumberPerPlant = default(System.Double);
            _TillerNumber = default(System.Int32);
            _Ntip = default(System.Double);
            _cumulTTPhenoMaizeAtEmergence = default(System.Double);
            _PTQ = default(System.Double);
            _Fixphyll = default(System.Double);
            _pastMaxAI = default(System.Double);
            // Returns true if everything is ok
            return true;
        }
        
        #region Clone
        /// <summary>Implement ICloneable.Clone()</summary>
        public virtual Object Clone()
        {
            // Shallow copy by default
            IDomainClass myclass = (IDomainClass) this.MemberwiseClone();
            _parametersIO.PopulateClonedCopy(myclass);
            return myclass;
        }
        #endregion
    }
}

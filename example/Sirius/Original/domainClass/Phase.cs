using System;
namespace SiriusQualityPhenology
{ 
    ///<summary>This enumerates particular moments of the Crop evolution (zadok stages)</summary>
    public enum GrowthStage
    {
        Unknown,
        ZC_00_Sowing,
        ZC_10_Emergence,
        ZC_21_MainShootPlus1Tiller,
        ZC_22_MainShootPlus2Tiller,
        ZC_23_MainShootPlus3Tiller,
        ZC_30_PseudoStemErection,
        ZC_31_1stNodeDetectable,
        ZC_32_2ndNodeDetectable,
        TerminalSpikelet,
        ZC_37_FlagLeafJustVisible,
        ZC_39_FlagLeafLiguleJustVisible,
        ZC_65_Anthesis,
        ZC_75_EndCellDivision,
        ZC_85_MidGrainFilling,
        ZC_91_EndGrainFilling,
        ZC_92_Maturity,
        EndVernalisation,
        FloralInitiation,
        Heading,
        BeginningStemExtension
    }

    ///<summary>
    ///This class contains the phase of the crop developpement. The phase is described by a double between 0 and 7.
    ///You can use the getPhaseAsString method to find out what a number corresponds to.
    ///</summary>
    public class Phase
    {

        public double phaseValue { get; set; }    

        public Phase()
        {

            phaseValue = 0;
        }

        public Phase(Phase toCopy)
        {
            phaseValue = toCopy.phaseValue;
        }

        public static String getPhaseAsString(double p)
        {
           
            if (p < 0)
            {
                return "BeforeSowing";
            }
            else if (p >= 0 && p < 1)
            {
                return "SowingToEmergence";
            }
            else if (p >= 1 && p < 2)
            {
                return "EmergenceToFloralInitiation";
            }
            else if (p >= 2 && p < 3)
            {
                return "FloralInitiationToHeading";
            }
            else if (p >= 3 && p < 4)
            {
                return "HeadingToAnthesis";
            }
            else if (p >= 4 && p <4.5)
            {
                return "AnthesisToEndCellDivision";
            }
            else if (p >= 4.5 && p < 5)
            {
                return "EndCellDivisionToEndGrainFill";
            }
            else if (p >= 5 && p < 6)
            {
                return "EndGrainFillToMaturity";
            }
            else if (p >= 6 && p < 7)
            {
                return "AllOver";
            }
            else
            {
                return "NoPhase";
            }
        }

        public GrowthStage PreviousMoment()
        {
            if (phaseValue < 0)
            {
                return GrowthStage.Unknown;
            }
            else if (phaseValue >= 0 && phaseValue < 1)//SowingToEmergence
            {
                return GrowthStage.ZC_00_Sowing;
            }
            else if (phaseValue >= 1 && phaseValue < 2)//EmergenceToFloralInitiation
            {
                return GrowthStage.ZC_10_Emergence;
            }
            else if (phaseValue >= 2 && phaseValue < 3)//FloralInitiationToAnthesis
            {
                return GrowthStage.FloralInitiation;
            }
            else if (phaseValue >= 3 && phaseValue < 4)//FloralInitiationToAnthesis
            {
                return GrowthStage.Heading;
            }
            else if (phaseValue == 4)//AnthesisToEndCellDivision
            {
                return GrowthStage.ZC_65_Anthesis;
            }
            else if (phaseValue == 4.5)//EndCellDivisionToEndGrainFill
            {
                return GrowthStage.ZC_75_EndCellDivision;
            }
            else if (phaseValue >= 5 && phaseValue < 6)//EndGrainFillToMaturity
            {
                return GrowthStage.ZC_91_EndGrainFilling;
            }
            else if (phaseValue >= 6 && phaseValue < 7)//AllOver
            {
                return GrowthStage.ZC_92_Maturity;
            }
            else
            {
                throw new InvalidOperationException();
            }

        }

        public GrowthStage NextMoment()
        {

            if (phaseValue < 0)
            {
                return GrowthStage.ZC_00_Sowing;
            }
            else if (phaseValue >= 0 && phaseValue < 1)
            {
                return GrowthStage.ZC_10_Emergence;
            }
            else if (phaseValue >= 1 && phaseValue < 2)
            {
                return GrowthStage.FloralInitiation;
            }
            else if (phaseValue >= 2 && phaseValue < 3)
            {
                return GrowthStage.Heading;
            }
            else if (phaseValue >= 3 && phaseValue < 4)
            {
                return GrowthStage.ZC_65_Anthesis;
            }
            else if (phaseValue == 4)
            {
                return GrowthStage.ZC_75_EndCellDivision;
            }
            else if (phaseValue == 4.5)
            {
                return GrowthStage.ZC_91_EndGrainFilling;
            }
            else if (phaseValue >= 5 && phaseValue < 6)
            {
                return GrowthStage.ZC_92_Maturity;
            }
            else if (phaseValue >= 6 && phaseValue < 7)
            {
                throw new InvalidOperationException();
            }
            else
            {
                throw new InvalidOperationException();
            }

        }

        public static string growthStageAsString( GrowthStage moment)
        {
            switch (moment)
            {
                case GrowthStage.BeginningStemExtension: return "Beginning stem extension";
                case GrowthStage.ZC_75_EndCellDivision: return "End cell division";
                case GrowthStage.ZC_91_EndGrainFilling: return "End grain filling";
                default: return moment.ToString();
            }
        }


    }
}

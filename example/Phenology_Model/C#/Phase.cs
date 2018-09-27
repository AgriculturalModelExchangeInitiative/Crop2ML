using System;
public static class Phase
{
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

	public static GrowthStage PreviousMoment(double phaseValue)
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

}


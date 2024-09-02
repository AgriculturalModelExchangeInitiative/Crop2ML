using System;
using System.Collections.Generic;

public static class UpdatePhase
{
	public static void CalculateUpdatePhase(int IsVernalizable, double Dse, double PFLLAnth, double Dcd,
	                                        double Dgf, double Degfm, double MaxDL, double SLDL, int IgnoreGrainMaturation,
	                                        double PHEADANTH, int SwitchMaize, string choosePhyllUse,
	                                        double P,int DayLength, double cumulTT, int Vernaprog, double MinFinalNumber,
	                                        double LeafNumber, double GrainCumulTT, double GAI, int isMomentRegistredZC_39,
	                                        double cumulTTFromZC_39, double phase, double cumulTTFromZC_91, double Fixphyll, double Phyllochron, int hasLastPrimordiumAppeared )
	{
		double FinalLeafNumber;
		
		double phase1 = phase;
		
				
		if (phase1 >= 0 && phase1 < 1)//SowingToEmergence
		{
			//CheckEmergence
			if (SwitchMaize==0)
			{
				if (cumulTT>= Dse)
				{
					phase = 1;//Emergence
				}
				else
				{
					phase = phase1;
				}
			}
			else
			{
				if (cumulTT >= Dse)
				{
					phase= 1;//Emergence
				}
				else
				{
					phase = phase1;
				}
			}
		}
		else if (phase1 >= 1 && phase1 < 2)//EmergenceToFloralInitiation
		{
			if ((IsVernalizable==1 && Vernaprog >= 1) || (IsVernalizable==0))
			{
				//CalculateFinalLeafNumber

				// Pete's version of the method found in Sirius 2005x
				// leaf number associated with the approximate primordium number for
				// last leaf  primordium.  Final leaf number is calculated from the
				// daylength of the day this primordium occurs.  If daylength is more thant 15
				// then set final leaf number to minimum leaf number
				if (SwitchMaize==0)
				{

					if (DayLength > MaxDL)
					{
						FinalLeafNumber = MinFinalNumber;
						hasLastPrimordiumAppeared = 1;
						
					}
					else
					{
						double appFLN = MinFinalNumber + SLDL * (MaxDL - DayLength);
						// calculation of final leaf number from daylength at inflexion plus 2 leaves
						if (appFLN / 2.0 <= LeafNumber)
						{
							FinalLeafNumber = appFLN;
							hasLastPrimordiumAppeared =1;
						}
						else
						{
							phase = phase1;
						}
					}
					//for Maize phenologystate.hasLastPrimordiumAppeared = true;
				}
				else
				{
					hasLastPrimordiumAppeared = 1;
				}

				//CheckFloralInitiation
				if (hasLastPrimordiumAppeared==1)
				{
					phase = 2;//Floralinitiation
				}
				
			}
			else
			{
				phase = phase1;
			}
		}
		else if (phase1 >= 2 && phase1 < 4)//FloralInitiationToAnthesis
		{
			if (isMomentRegistredZC_39==1)
			{
				//calculate the heading date
				if (phase1 < 3)
				{
					double ttFromLastLeafToHeading = 0.0;
					if(choosePhyllUse=="Default")ttFromLastLeafToHeading =(PFLLAnth - PHEADANTH) * Fixphyll;
					else if (choosePhyllUse == "PTQ") ttFromLastLeafToHeading = (PFLLAnth - PHEADANTH) * Phyllochron;
					else if (choosePhyllUse == "Test") ttFromLastLeafToHeading = (PFLLAnth - PHEADANTH) * P;

					if (cumulTTFromZC_39 >= ttFromLastLeafToHeading)
					{
						phase = 3;
					}
					else
					{
						phase = phase1;
					}
				}					
				//CheckAnthesis;
				double ttFromLastLeafToAnthesis =0.0;
				if (choosePhyllUse == "Default") ttFromLastLeafToAnthesis = PFLLAnth * Fixphyll;
				else if (choosePhyllUse == "PTQ") ttFromLastLeafToAnthesis = PFLLAnth * Phyllochron;
				else if (choosePhyllUse == "Test") ttFromLastLeafToAnthesis = PFLLAnth * P;

				if (cumulTTFromZC_39 >= ttFromLastLeafToAnthesis)
				{
						phase = 4;//Anthesis
				}
				else
				{
					phase = phase1;
				}
			}
			else
				{
					phase = phase1;
				}
			
		}
		else if (phase1 == 4)//AnthesisToEndCellDivision
		{
			//CheckEndCellDivision
			if (GrainCumulTT >= Dcd)
			{
				phase = 4.5;//EndCellDivision
			}
			else
			{
				phase = phase1;
			}
		}
		else if (phase1 == 4.5)//EndCellDivisionToEndGrainFill
		{
			// CheckEndGrainFilling
			if (GrainCumulTT >= Dgf || GAI <= 0)
			{
				phase = 5;//End of grain filling
			}
			else
			{
				phase = phase1;
			}

		}
		else if (phase1 >= 5 && phase1 < 6)//EndGrainFillToMaturity
		{
			//CheckMaturity
			///<Comment>To enable ignoring grain maturation duration</Comment>
			double LocalDegfm = Degfm;
			if (IgnoreGrainMaturation==1) LocalDegfm = -1;
				
			if (cumulTTFromZC_91 >= LocalDegfm)
			{
				phase = 6; //maturity
			}
			else
			{
				phase= phase1;
			}

		}
		else if (phase1>= 6 && phase1 < 7)
		{
			phase = phase1;
		}
		else
		{
			throw new Exception("current phase is not between 0 and 7");
		}
			
	}
}
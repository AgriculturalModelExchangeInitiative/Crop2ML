
using System;

public static class PhylSowingDateCorrection
	
{

	public static void CalculatePhylSowingDateCorrection(double Latitude, int SowingDay, int SDsa_sh, double P,
	                                              int SDws, int SDsa_nh, double Rp)
	{
		double FixPhyll;
		if (Latitude < 0)
		{
			if (SowingDay > SDsa_sh)
			{
				FixPhyll = P * (1 - Rp * Math.Min(SowingDay - SDsa_sh, SDws));
			}
			else FixPhyll = P;
		}
		else
		{
			if (SowingDay < SDsa_nh)
			{
				FixPhyll = P * (1 - Rp * Math.Min(SowingDay, SDws));
			}
			else FixPhyll = P;
		}
		

		
	}

}

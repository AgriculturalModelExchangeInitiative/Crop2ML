
using System;
using System.Collections.Generic;


public static class ShootNumber
{
	
	public static void CalculateShootNumber(int CanopyShootNumber, double LeafNumber, double SowingDensity,
	                                 double TargetFertileShoot, List<double>tilleringProfile, List<double>leafTillerNumberArray, int TillerNumber)
	{
		
		int OldCanopyShootNumber = CanopyShootNumber;
		CalcShootNumber(LeafNumber,SowingDensity,TargetFertileShoot);
		
		
		var OldtilleringProfile = new List<double>();
		for (int i = 0; i < tilleringProfile.Count;i++ ) OldtilleringProfile.Add(tilleringProfile[i]);
		
		var leafTillerNumberArray1 = new List<double>();
		for (int i = 0; i < leafTillerNumberArray.Count;i++ ) leafTillerNumberArray1.Add(leafTillerNumberArray[i]);
		

		if (CanopyShootNumber != OldCanopyShootNumber)
		{
			OldtilleringProfile.Add(CanopyShootNumber - OldCanopyShootNumber);
		}
		
		tilleringProfile = new List<double>();
		for (int i = 0; i < OldtilleringProfile.Count;i++ ) tilleringProfile.Add(OldtilleringProfile[i]);

		TillerNumber = tilleringProfile.Count;
		

		for (int i = leafTillerNumberArray1.Count; i < Math.Ceiling(LeafNumber); i++)
		{
			leafTillerNumberArray1.Add(TillerNumber);
		}
		
		leafTillerNumberArray = new List<double>();
		for (int i = 0; i < leafTillerNumberArray1.Count;i++ ) leafTillerNumberArray.Add(leafTillerNumberArray1[i]);
		

	}

	public static void Init(double SowingDensity)
	{

		double CanopyShootNumber = SowingDensity;
		double AverageShootNumberPerPlant;
		AverageShootNumberPerPlant =1;
		List<double>tilleringProfile = new List<double>();
		tilleringProfile.Add(SowingDensity);
		int TillerNumber;
		TillerNumber = 1;
		
	}
	static void CalcShootNumber(double LeafNumber, double SowingDensity,double TargetFertileShoot)
	{
		int EmergedLeaves = (int)Math.Max(1, Math.Ceiling(LeafNumber - 1));
		int Shoots = Fibonacci(EmergedLeaves);
		double CanopyShootNumber = Math.Min(Shoots * SowingDensity, TargetFertileShoot);
		double AverageShootNumberPerPlant = CanopyShootNumber / SowingDensity;
	}
	static int Fibonacci(int N)
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
	
}




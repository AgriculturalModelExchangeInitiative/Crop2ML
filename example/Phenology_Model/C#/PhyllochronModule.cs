
using System;


public static class PhyllochronClass
{
	public static void  CalculatePhyllochron(double LeafNumber, double Ldecr, double Fixphyll,
	                                 double Pdecr, double Pincr, double Lincr )
	{
		double Phyllochron;
		if (LeafNumber < Ldecr) Phyllochron = Fixphyll * Pdecr;
		else if (LeafNumber >= Ldecr && LeafNumber < Lincr) Phyllochron = Fixphyll;
		else Phyllochron = Fixphyll * Pincr;
		
	}
	
}

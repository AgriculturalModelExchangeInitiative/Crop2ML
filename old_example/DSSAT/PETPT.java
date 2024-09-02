public class PETPT {

    static double PETPT(double MSALB, double SRAD, double TMAX, double TMIN, double XHLAI, double EO)
	{
		double ALBEDO;
		double SLANG;
		double EEQ;
		double eo;
		double TD = 0.60*TMAX + 0.40*TMIN;
		if (XHLAI <= 0.0) {
			ALBEDO = MSALB;
		} else {
			ALBEDO = 0.23 - (0.23-MSALB)*Math.exp(-0.75*XHLAI);
		}
		SLANG = SRAD * 23.923;
		EEQ = SLANG * (2.04E-4 - 1.83E-4 * ALBEDO)*(TD+29.0);
		eo = EEQ*1.1;
		if (TMAX < 35.0) {
            eo = EEQ*((TMAX-35.0)*0.05+1.1);
		} else {
            eo = EEQ*0.01*Math.exp(0.18*(TMAX+20.0));
		}

		EO = Math.max(eo, 0.0001d);
        return EO;
	}
    public static void main(String[] args) {
		// TODO code application logic here
		
        System.out.println("PETPT: " + (PETPT(2,2,2,1,0.5,0)));
        
	
	}
    
}

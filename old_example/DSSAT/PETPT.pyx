from math import exp
def ETPT(double MSALB, double SRAD, double TMAX, double TMIN, double XHLAI):

	cdef double ALBEDO, EEQ, SLANG, TD, eo, EO
	TD = 0.60*TMAX + 0.40*TMIN
	if (XHLAI <= 0.0):
		ALBEDO = MSALB
	else:
		ALBEDO = 0.23 - (0.23-MSALB)*exp(-0.75*XHLAI)
	SLANG = SRAD * 23.923
	EEQ = SLANG * (2.04E-4 - 1.83E-4 * ALBEDO)*(TD+29.0)
	eo = EEQ*1.1
	if (TMAX < 35.0):
		eo = EEQ*((TMAX-35.0)*0.05+1.1
	else:
		eo = EEQ*0.01*exp(0.18*(TMAX+20.0)

	EO = max(eo, 0.0001)

	return EO

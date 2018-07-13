cdef double ALBEDO, EEQ, SLANG, TD
TD = 0.60*TMAX + 0.40*TMIN
if (XHLAI <= 0.0):
    ALBEDO = MSALB
else:
    ALBEDO = 0.23 - (0.23-MSALB)*math.exp(-0.75*XHLAI)
SLANG = SRAD * 23.923
EEQ = SLANG * (2.04E-4 - 1.83E-4 * ALBEDO)*(TD+29.0);
eo = EEQ*1.1
if (TMAX < 35.0):
    eo = EEQ*((TMAX-35.0)*0.05+1.1
else:
    eo =  EEQ*0.01*math.exp(0.18*(TMAX+20.0);

#EO = MAX(EO,0.0)   !gives error in DECRAT_C
EO = math.max(eo, 0.0001d)

'''
Created on 18 sept. 2018

@author: midingoy
'''

def CalculatePhylSowingDateCorrection(Latitude, SowingDay, SDsa_sh,  P,SDws, SDsa_nh,  Rp):
    if (Latitude < 0):
        if (SowingDay > SDsa_sh):
            FixPhyll = P * (1 - Rp * min(SowingDay - SDsa_sh, SDws))
        else: FixPhyll = P
    else:
        if (SowingDay < SDsa_nh):
            FixPhyll = P * (1 - Rp * min(SowingDay, SDws))
        else: FixPhyll = P
    return FixPhyll

if __name__ == '__main__':
    pass
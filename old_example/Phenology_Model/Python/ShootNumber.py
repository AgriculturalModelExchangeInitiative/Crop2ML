'''
Created on 18 sept. 2018

@author: midingoy
'''
from math import *
def CalculateShootNumber( CanopyShootNumber,  LeafNumber,  SowingDensity, TargetFertileShoot, tilleringProfile, leafTillerNumberArray,  TillerNumber):
    
    OldCanopyShootNumber = CanopyShootNumber
        
    AverageShootNumberPerPlant = CalcShootNumber(LeafNumber,SowingDensity,TargetFertileShoot)[0]
    CanopyShootNumber = CalcShootNumber(LeafNumber,SowingDensity,TargetFertileShoot)[1]   
    
    OldtilleringProfile=list(tilleringProfile)
    leafTillerNumberArray1 = list(leafTillerNumberArray)    
        
    if (CanopyShootNumber != OldCanopyShootNumber):
        diff = CanopyShootNumber - OldCanopyShootNumber
        OldtilleringProfile.append(diff)
           
    tilleringProfile = list(OldtilleringProfile)
    
    TillerNumber = len(tilleringProfile)
        
    for i in range(len(leafTillerNumberArray1),int(ceil(LeafNumber))):
        leafTillerNumberArray1.append(TillerNumber);
    
    leafTillerNumberArray = list(leafTillerNumberArray1)
      

    return CanopyShootNumber, AverageShootNumberPerPlant, tilleringProfile, TillerNumber, leafTillerNumberArray

def Init(SowingDensity):
    CanopyShootNumber = SowingDensity
    AverageShootNumberPerPlant =1
    tilleringProfile = []
    tilleringProfile.append(SowingDensity);
    TillerNumber = 1;
    return CanopyShootNumber, AverageShootNumberPerPlant,tilleringProfile, TillerNumber
        
        
def CalcShootNumber(LeafNumber,SowingDensity,TargetFertileShoot):
    EmergedLeaves = int(max(1, ceil(LeafNumber - 1)));
    Shoots = Fibonacci(EmergedLeaves);
    CanopyShootNumber = min(Shoots * SowingDensity, TargetFertileShoot);
    AverageShootNumberPerPlant = CanopyShootNumber / SowingDensity;
    return AverageShootNumberPerPlant, CanopyShootNumber 

def Fibonacci(N):
    a = 0;
    b = 1;
    for i in range(0,N):
        temp = a;
        a = b;
        b = temp + b; 
    return a;



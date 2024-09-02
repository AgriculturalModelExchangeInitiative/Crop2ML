package net.simplace.sim.components.Simplace_Soil_Temperature;
import  java.io.*;
import  java.util.*;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
import net.simplace.sim.model.FWSimComponent;
import net.simplace.sim.util.FWSimVarMap;
import net.simplace.sim.util.FWSimVariable;
import net.simplace.sim.util.FWSimVariable.CONTENT_TYPE;
import net.simplace.sim.util.FWSimVariable.DATA_TYPE;
import org.jdom2.Element;


public class STMPsimCalculator extends FWSimComponent
{
    private FWSimVariable<Double[]> cSoilLayerDepth;
    private FWSimVariable<Double> cFirstDayMeanTemp;
    private FWSimVariable<Double> cAVT;
    private FWSimVariable<Double> cABD;
    private FWSimVariable<Double> cDampingDepth;
    private FWSimVariable<Double> iSoilWaterContent;
    private FWSimVariable<Double> iSoilSurfaceTemperature;
    private FWSimVariable<Double[]> SoilTempArray;
    private FWSimVariable<Double[]> rSoilTempArrayRate;
    private FWSimVariable<Double[]> pSoilLayerDepth;

    public STMPsimCalculator(String aName, HashMap<String, FWSimVariable<?>> aFieldMap, HashMap<String, String> aInputMap, Element aSimComponentElement, FWSimVarMap aVarMap, int aOrderNumber)
    {
        super(aName, aFieldMap, aInputMap, aSimComponentElement, aVarMap, aOrderNumber);
    }

    public STMPsimCalculator(){
        super();
    }

    @Override
    public HashMap<String, FWSimVariable<?>> createVariables()
    {
        addVariable(FWSimVariable.createSimVariable("cSoilLayerDepth", "Depth of soil layer", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.constant,"http://www.wurvoc.org/vocabularies/om-1.8/metre", 0.03, 20.0, null, this));
        addVariable(FWSimVariable.createSimVariable("cFirstDayMeanTemp", "Mean air temperature on first day", DATA_TYPE.DOUBLE, CONTENT_TYPE.constant,"http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", -40.0, 50.0, null, this));
        addVariable(FWSimVariable.createSimVariable("cAVT", "Constant Temperature of deepest soil layer - use long term mean air temperature", DATA_TYPE.DOUBLE, CONTENT_TYPE.constant,"http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", -10.0, 20.0, null, this));
        addVariable(FWSimVariable.createSimVariable("cABD", "Mean bulk density", DATA_TYPE.DOUBLE, CONTENT_TYPE.constant,"http://www.wurvoc.org/vocabularies/om-1.8/tonne_per_cubic_metre", 1.0, 4.0, 2.0, this));
        addVariable(FWSimVariable.createSimVariable("cDampingDepth", "Initial value for damping depth of soil", DATA_TYPE.DOUBLE, CONTENT_TYPE.constant,"http://www.wurvoc.org/vocabularies/om-1.8/metre", 1.5, 20.0, 6.0, this));
        addVariable(FWSimVariable.createSimVariable("iSoilWaterContent", "Water content, sum of whole soil profile", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,"http://www.wurvoc.org/vocabularies/om-1.8/millimetre", 1.5, 20.0, 5.0, this));
        addVariable(FWSimVariable.createSimVariable("iSoilSurfaceTemperature", "Temperature at soil surface", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,"http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", 1.5, 20.0, null, this));
        addVariable(FWSimVariable.createSimVariable("SoilTempArray", "Array of soil temperatures in layers ", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.state,"http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", -40.0, 50.0, null, this));
        addVariable(FWSimVariable.createSimVariable("rSoilTempArrayRate", "Array of daily temperature change", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.state,"http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_per_day", -20, 20, null, this));
        addVariable(FWSimVariable.createSimVariable("pSoilLayerDepth", "Depth of soil layer plus additional depth", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.state,"http://www.wurvoc.org/vocabularies/om-1.8/metre", 0.03, 20.0, null, this));

        return iFieldMap;
    }
    @Override
    protected void init()
    {
        Double [] t_cSoilLayerDepth = cSoilLayerDepth.getValue();
        double t_cFirstDayMeanTemp = cFirstDayMeanTemp.getValue();
        double t_cAVT = cAVT.getValue();
        double t_cABD = cABD.getValue();
        double t_cDampingDepth = cDampingDepth.getValue();
        double t_iSoilWaterContent = iSoilWaterContent.getValue();
        double t_iSoilSurfaceTemperature = iSoilSurfaceTemperature.getValue();
        Double [] t_SoilTempArray = SoilTempArray.getValue();;
        Double [] t_rSoilTempArrayRate = rSoilTempArrayRate.getValue();;
        Double [] t_pSoilLayerDepth = pSoilLayerDepth.getValue();;
        double tProfileDepth;
        double additionalDepth;
        double firstAdditionalLayerHight;
        Integer layers;
        Double[] tStmp;
        Double[] tStmpRate;
        Double[] tz;
        Integer i;
        double depth;
        tProfileDepth = t_cSoilLayerDepth[t_cSoilLayerDepth.length - 1];
        additionalDepth = t_cDampingDepth - tProfileDepth;
        firstAdditionalLayerHight = additionalDepth - (double)(Math.floor(additionalDepth));
        layers = (int)(Math.abs((double)((int) Math.ceil(additionalDepth)))) + t_cSoilLayerDepth.length;
        tStmp = new Double [layers];
        tStmpRate = new Double [layers];
        tz = new Double [layers];
        for (i=0 ; i!=tStmp.length ; i+=1)
        {
            if (i < t_cSoilLayerDepth.length)
            {
                depth = t_cSoilLayerDepth[i];
            }
            else
            {
                depth = tProfileDepth + firstAdditionalLayerHight + i - t_cSoilLayerDepth.length;
            }
            tz[i] = depth;
            tStmp[i] = (t_cFirstDayMeanTemp * (t_cDampingDepth - depth) + (t_cAVT * depth)) / t_cDampingDepth;
        }
        t_rSoilTempArrayRate = tStmpRate;
        t_SoilTempArray = tStmp;
        t_pSoilLayerDepth = tz;
        SoilTempArray.setValue(t_SoilTempArray, this);
        rSoilTempArrayRate.setValue(t_rSoilTempArrayRate, this);
        pSoilLayerDepth.setValue(t_pSoilLayerDepth, this);
    }
    @Override
    protected void process()
    {
        Double [] t_cSoilLayerDepth = cSoilLayerDepth.getValue();
        double t_cFirstDayMeanTemp = cFirstDayMeanTemp.getValue();
        double t_cAVT = cAVT.getValue();
        double t_cABD = cABD.getValue();
        double t_cDampingDepth = cDampingDepth.getValue();
        double t_iSoilWaterContent = iSoilWaterContent.getValue();
        double t_iSoilSurfaceTemperature = iSoilSurfaceTemperature.getValue();
        Double [] t_SoilTempArray = SoilTempArray.getValue();
        Double [] t_rSoilTempArrayRate = rSoilTempArrayRate.getValue();
        Double [] t_pSoilLayerDepth = pSoilLayerDepth.getValue();
        double XLAG;
        double XLG1;
        double DP;
        double WC;
        double DD;
        double Z1;
        Integer i;
        double ZD;
        double RATE;
        XLAG = .8d;
        XLG1 = 1 - XLAG;
        DP = 1 + (2.5d * t_cABD / (t_cABD + Math.exp(6.53d - (5.63d * t_cABD))));
        WC = 0.001d * t_iSoilWaterContent / ((.356d - (.144d * t_cABD)) * t_cSoilLayerDepth[(t_cSoilLayerDepth.length - 1)]);
        DD = Math.exp(Math.log(0.5d / DP) * ((1 - WC) / (1 + WC)) * 2) * DP;
        Z1 = (double)(0);
        for (i=0 ; i!=t_SoilTempArray.length ; i+=1)
        {
            ZD = 0.5d * (Z1 + t_pSoilLayerDepth[i]) / DD;
            RATE = ZD / (ZD + Math.exp(-.8669d - (2.0775d * ZD))) * (t_cAVT - t_iSoilSurfaceTemperature);
            RATE = XLG1 * (RATE + t_iSoilSurfaceTemperature - t_SoilTempArray[i]);
            Z1 = t_pSoilLayerDepth[i];
            t_rSoilTempArrayRate[i] = RATE;
            t_SoilTempArray[i] = t_SoilTempArray[i] + t_rSoilTempArrayRate[i];
        }
        SoilTempArray.setValue(t_SoilTempArray, this);
        rSoilTempArrayRate.setValue(t_rSoilTempArrayRate, this);
    }

    public HashMap<String, FWSimVariable<?>> fillTestVariables(int aParamIndex, TEST_STATE aDefineOrCheck)
    {
        if(aParamIndex == 0 && aDefineOrCheck == TEST_STATE.DEFINE) //before process
        {
            FWSimVariable.setValue(new Double[] {0.1,0.5,1.5}, iFieldMap.get("STMPsimCalculator.cSoilLayerDepth"), this);
            FWSimVariable.setValue(15.0, iFieldMap.get("STMPsimCalculator.cFirstDayMeanTemp"), this);
            FWSimVariable.setValue(9.0, iFieldMap.get("STMPsimCalculator.cAVT"), this);
            FWSimVariable.setValue(1.4, iFieldMap.get("STMPsimCalculator.cABD"), this);
            FWSimVariable.setValue(6.0, iFieldMap.get("STMPsimCalculator.cDampingDepth"), this);
            FWSimVariable.setValue(0.3, iFieldMap.get("STMPsimCalculator.iSoilWaterContent"), this);
            FWSimVariable.setValue(6.0, iFieldMap.get("STMPsimCalculator.iSoilSurfaceTemperature"), this);
        }
        else if(aParamIndex ==  0 && aDefineOrCheck == TEST_STATE.CHECK) //after process
        {
            FWSimVariable.setValue(new Double[] {13.624360856350041,13.399968504634286,12.599999999999845,12.2,11.4,10.6,9.799999999999999,9.0}, iFieldMap.get("STMPsimCalculator.SoilTempArray"), this);
        }
        else return null;
        return iFieldMap;
    }

    @Override
    protected FWSimComponent clone(FWSimVarMap aVarMap)
    {
        return new STMPsimCalculator(iName, iFieldMap, iInputMap, iSimComponentElement, aVarMap, iOrderNumber);
    }
}
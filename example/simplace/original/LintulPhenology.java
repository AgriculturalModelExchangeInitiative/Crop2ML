/*
 * SIMPLACE - Scientific Impact assessment and Modeling PLattform for Advanced Crop and Ecosystem management
 *
 * This file is part of the SIMPLACE (before SMILEUtil) project.
 * 
 * SIMPLACE is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *  
 * SIMPLACE is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with SIMPLACE.  If not, see <http://www.gnu.org/licenses/>.
 *
 * LintulPhenology.java
 *
 * Responsible developers: Gunther Krauss, Crop Science Group, Katzenburgweg 5, 53115 Bonn, Germany
 *                         Andreas Enders, Crop Science Group, Katzenburgweg 5, 53115 Bonn, Germany
 * Contact Information:    lapit@uni-bonn.de
 * More information on <http://www.simplace.net>
 */

package net.simplace.sim.components.models.lintul;

import static java.lang.StrictMath.*;

import net.simplace.sim.model.FWSimComponent;
import net.simplace.sim.util.FWSimVarMap;
import net.simplace.sim.util.FWSimVariable;
import net.simplace.sim.util.FWSimVariable.CONTENT_TYPE;
import net.simplace.sim.util.FWSimVariable.DATA_TYPE;

import java.time.LocalDateTime;
import java.util.HashMap;

import org.jdom.Element;

/**
 * WIKI_START
 * LintulPhenology.java calculates the development stage (DevStage) of a crop based on the ratio between accumulated degree days and the a user-defined, crop and cultivar specific temperature sum requirement .
 * In addition, based on the development stage, the date and the day of the year (DOY) when certain phenology events occur (e.g. anthesis, physiological maturity) are determined .
 * 
 * === anthesis ===
 * 
 * The phenological development of the crop starts with the emergence day. This day is either determined by increasing the day of the year (DOY) when sowing occurs by a user-specified number of days.
 * The sowing date (DOY when sowing occurs) is specified in the SimComponent "Management".
 * Crop development between emergence and anthesis is triggered by the accumulated temperature sum (TSUM). TSUM is increased daily by the rate of effective temperature until anthesis (RTEFFAnt),
 * which is an input either from the simComponent WeatherTransformer or from the SimComponent VernalisationAndPhotoresponse (if effective temperature depends on photoperiod and/or vernalisation requirements). 
 * The effective temperature in the WeatherTransformer is calculated as the difference between the average daily air temperature (AirTemperatureMean) and the base temperature of the crop before anthesis
 * (BaseTempBeforeAnt as defined in the crop property file). Only days where AirTemperatureMean > BaseTempBeforeAnt are accounted for.
 * The ratio between actual TSUM at a given day and the air temperature sum to anthesis (AirTemperatureSumAnthesis) is added daily to the development stage index (DevStage) as
 * 
 * WIKI_END
 *   \[
 *   \begin{eqnarray}
 *   DevStage & = & TSUM / AirTemperatureSumAnthesis
 *   \end{eqnarray}
 *   \]
 * WIKI_START
 * 
 *  === maturity ===
 * 
 * Similarly, crop development between anthesis and maturity is triggered by the accumulated temperature sum (TSUM). From anthesis to maturity the daily rate of effective temperature (RTEFFMat)
 * is an input from the SimComponent WeatherTransformer where RTEFFMat is depends on the difference between the average daily air temperature (AirTemperatureMean)
 * and the base temperature of the crop after anthesis (BaseTempBeforeMat as defined in the crop property file). Only days where AirTemperatureMean > BaseTempBeforeMat are accounted for.
 * Again, the ratio between actual TSUM at a given day and the air temperature sum from anthesis to maturity (AirTemperatureSumMaturity) is added daily to the development stage index (DevStage) as 
 * WIKI_END
 *   \[
 *   \begin{eqnarray}
 *   DevStage & = & 1 + TSUM / AirTemperatureSumAnthesis
 *   \end{eqnarray}
 *   \]
 * WIKI_START
  
* 
 *   '''References:'''
 * Goudriaan, H.H. Van Laar, 1994. Modelling Potential Crop Growth Processes, Kluwer Academic Publishers, Dordrecht (1994) 238 pp
 * WIKI_END
 * 
 * @author Gunther Krauss
 * @author Andreas Enders
 * @author Thomas Gaiser
 * 
 * Component for the Lintul crop model
 * 
 */
public class LintulPhenology extends FWSimComponent {


	// Constants


	private FWSimVariable<Double> cAirTemperatureSumMaturity; 
	private FWSimVariable<Double> cAirTemperatureSumMilkripeness;
	private FWSimVariable<Double> cAirTemperatureSumAnthesis;
	private FWSimVariable<Integer> cRelativeDayOfEmergence;     

	// inputs	
	private FWSimVariable<Double> iRTEFFAnt;
	private FWSimVariable<Double> iRTEFFMat;
	private FWSimVariable<Boolean> iDoSow;
	private FWSimVariable<Boolean> iDoHarvest;

	
	//states
	private FWSimVariable<Double> sDevStage; 

	private FWSimVariable<Double> sTSUM;

	// rates
	private FWSimVariable<Double> rDevStageRate; 
	
	// outputs
	private FWSimVariable<LocalDateTime> SowingDate;
	private FWSimVariable<LocalDateTime> EmergenceDate;
	private FWSimVariable<LocalDateTime> MilkripenessDate;
	private FWSimVariable<LocalDateTime> AnthesisDate;
	private FWSimVariable<LocalDateTime> MaturityDate;
	private FWSimVariable<Integer> SowingDOY; 
	private FWSimVariable<Integer> EmergenceDOY;
	private FWSimVariable<Integer> MilkripenessDOY;
	private FWSimVariable<Integer> AnthesisDOY;
	private FWSimVariable<Integer> MaturityDOY;

	private FWSimVariable<Boolean> IsSowing;
	private FWSimVariable<Boolean> IsEmergence;
	private FWSimVariable<Boolean> IsAnthesis;
	private FWSimVariable<Boolean> IsMilkripeness;
	private FWSimVariable<Boolean> IsMaturity;
	private FWSimVariable<Boolean> IsPhenologyEvent;
	
	private FWSimVariable<Double> RTEFF;

	private FWSimVariable<Integer> CropCycleCount;  


	/**
	 * called by clone method only
	 * 
	 * @param aName
	 * @param aFieldMap
	 * @param aInputMap
	 * @param aSimComponentElement
	 * @param aVarMap
	 */
	private LintulPhenology(String aName, HashMap<String, FWSimVariable<?>> aFieldMap,
			HashMap<String, String> aInputMap, Element aSimComponentElement, FWSimVarMap aVarMap, int aOrderNumber)
	{
		super(aName, aFieldMap, aInputMap, aSimComponentElement, aVarMap, aOrderNumber);
	}

	/**
	 * Empty constructor used by class.forName()
	 */
	public LintulPhenology()
	{
		super();
	}

	/**
	 * create the FWSimVariables as interface for this SimComponent
	 * 
	 * @see net.simplace.sim.model.FWSimComponent#createVariables()
	 */
	@Override
	public HashMap<String, FWSimVariable<?>> createVariables() 
	{

		// Constants
		addVariable(FWSimVariable.createSimVariable("cAirTemperatureSumAnthesis",
				"Temperature sum after energence when anthesis of plant is reached", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_day", 0d, 5000d, 650d, this));

		addVariable(FWSimVariable.createSimVariable("cAirTemperatureSumMilkripeness",
				"Temperature sum when milk ripeness of plant is reached", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_day", 0d, 7000d, null, this));// this constant is presently not used
		addVariable(FWSimVariable.createSimVariable("cAirTemperatureSumMaturity",
				"Temperature sum after emergency when maturity of plant is reached", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_day", 0d, 10000d, 1350d, this));
		addVariable(FWSimVariable.createSimVariable("cRelativeDayOfEmergence",
				"Days between sowing and emergence", DATA_TYPE.INT, 
				CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/day", 0, 366, 11, this));

		// inputs
		
		addVariable(FWSimVariable.createSimVariable("iDoSow",
				"true at the day of sowing", DATA_TYPE.BOOLEAN,
				CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, false, this));
		addVariable(FWSimVariable.createSimVariable("iDoHarvest",
				"true at the day of harvest", DATA_TYPE.BOOLEAN,
				CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, false, this));
		addVariable(FWSimVariable.createSimVariable("iRTEFFAnt",
				"Daily effective temperature before anthesis", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", 0d, 40d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("iRTEFFMat",
				"Daily effective temperature after anthesis", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", 0d, 40d, 0d, this));

		// States
		
		addVariable(FWSimVariable.createSimVariable("sDevStage",
				"Development stage of the crop (1.0=anthesis, 2.0=physiological maturity", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.state, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, null, 0d, this));
		addVariable(FWSimVariable.createSimVariable("sTSUM",
				"Temperature sum as accumulated effective temperature after emergence", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.state, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius_day", 0d, 10000d, 0d, this));
		// rates
		addVariable(FWSimVariable.createSimVariable("rDevStageRate",
				"increment of the dev stage", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.rate, "http://www.wurvoc.org/vocabularies/om-1.8/reciprocal_day", 0d, null, 0d, this));

		
		
		// outputs
		addVariable(FWSimVariable.createSimVariable("SowingDate",
				"Date of Sowing", DATA_TYPE.DATE,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("EmergenceDate",
				"Date of Emergence", DATA_TYPE.DATE,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("MilkripenessDate",
				"Date of Milkripeness", DATA_TYPE.DATE,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("AnthesisDate",
				"Date of Anthesis", DATA_TYPE.DATE,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("MaturityDate",
				"Date of Maturity", DATA_TYPE.DATE,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("SowingDOY",
				"DOY of Sowing", DATA_TYPE.INT,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, 0, this));
		addVariable(FWSimVariable.createSimVariable("EmergenceDOY",
				"DOY of Emergence", DATA_TYPE.INT,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, 0, this));
		addVariable(FWSimVariable.createSimVariable("AnthesisDOY",
				"DOY of Anthesis", DATA_TYPE.INT,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, 0, this));
		addVariable(FWSimVariable.createSimVariable("MilkripenessDOY",
				"DOY of Milkripeness", DATA_TYPE.INT,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, 0, this));
		addVariable(FWSimVariable.createSimVariable("MaturityDOY",
				"DOY of Maturity", DATA_TYPE.INT,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, 0, this));

		addVariable(FWSimVariable.createSimVariable("IsSowing",
				"true at the sowing date", DATA_TYPE.BOOLEAN,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, false, this));
		addVariable(FWSimVariable.createSimVariable("IsEmergence",
				"true at the emergence date", DATA_TYPE.BOOLEAN,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, false, this));
		addVariable(FWSimVariable.createSimVariable("IsAnthesis",
				"true at the anthesis date", DATA_TYPE.BOOLEAN,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, false, this));
		addVariable(FWSimVariable.createSimVariable("IsMilkripeness",
				"true at the milkripeness date", DATA_TYPE.BOOLEAN,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, false, this));
		addVariable(FWSimVariable.createSimVariable("IsMaturity",
				"true at the maturity date", DATA_TYPE.BOOLEAN,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, false, this));
		addVariable(FWSimVariable.createSimVariable("IsPhenologyEvent",
				"true if either sowing/emergence/anthesis/maturity date occured", DATA_TYPE.BOOLEAN,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, false, this));


		addVariable(FWSimVariable.createSimVariable("CropCycleCount",
				"Number of growth periods, starting with 0 and incremented on harvest. Used for crop rotation.", DATA_TYPE.INT,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0, 100, 0, this)); // should be renamed into NumberGrowingCycles

		addVariable(FWSimVariable.createSimVariable("RTEFF",
				"Daily effective temperature used to calculate the temperature sum and development stage at a given day", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", 0d, 40d, 0d, this));

		return iFieldMap;
	}

	/**
	 *  initializes the fields by getting input and output FWSimVariables from VarMap
	 *  
	 * @see net.simplace.sim.model.FWSimComponent#init()
	 */
	@Override
	protected void init() 
	{
		CropCycleCount.setDefaultValue();
	}

	/**
	 * process the algorithm and write the results back to VarMap
	 * 
	 * @see net.simplace.sim.model.FWSimComponent#process()
	 */
	@Override
	protected void process() 
	{

		LocalDateTime tCurrentDate = (LocalDateTime) getVariable(FWSimVarMap.CURRENT_DATE).getValue();
		Integer tCurrentDOY = (Integer) getVariable(FWSimVarMap.CURRENT_DOY).getValue();

		IsSowing.setValue(false, this);
		IsEmergence.setValue(false, this);
		IsAnthesis.setValue(false, this);
		IsMilkripeness.setValue(false, this);
		IsMaturity.setValue(false, this);


		//check for resetting the Dates
		if (sDevStage.getValue() > 0 && iDoHarvest.getValue())
		{
			resetStates();
			CropCycleCount.setValue((Integer) CropCycleCount.getValue()+1, this);
		}
		else if (iDoSow.getValue()) 
		{
			resetStates();

			EmergenceDOY.setDefaultValue();
			AnthesisDOY.setDefaultValue();
			MilkripenessDOY.setDefaultValue();
			MaturityDOY.setDefaultValue();

			SowingDate.setValue(tCurrentDate, this);
			SowingDOY.setValue(tCurrentDOY, this);
			IsSowing.setValue(true, this);
		}
		else if (SowingDate.getValue() != null)
		{

			// before emergence
			if(EmergenceDate.getValue() == null && (SowingDate.getValue().isBefore(tCurrentDate) || SowingDate.getValue().isEqual(tCurrentDate)) )
			{
				sDevStage.setValue(0.0001, this);
			}

			// Between emergence and anthesis
			if(AnthesisDate.getValue() == null)
			{
				sTSUM.setValue(sTSUM.getValue()+ iRTEFFAnt.getValue(), this);					
				RTEFF.setValue(iRTEFFAnt.getValue(), this);
				double devstagebefore = sDevStage.getValue();
				sDevStage.setValue(max(sTSUM.getValue() / cAirTemperatureSumAnthesis.getValue(),sDevStage.getValue()), this);
				rDevStageRate.setValue(sDevStage.getValue()-devstagebefore, this);
			}
			// After anthesis
			else
			{
				sTSUM.setValue(sTSUM.getValue()+ iRTEFFMat.getValue(), this);					
				RTEFF.setValue(iRTEFFMat.getValue(), this);
				sDevStage.setValue(1 + (sTSUM.getValue() - cAirTemperatureSumAnthesis.getValue()) / (cAirTemperatureSumMaturity.getValue()-cAirTemperatureSumAnthesis.getValue()), this); //DevStage ends on 2
				rDevStageRate.setValue(RTEFF.getValue()/(cAirTemperatureSumMaturity.getValue()-cAirTemperatureSumAnthesis.getValue()), this);
			}


			if(EmergenceDate.getValue() == null && (
					SowingDate.getValue().plusDays(cRelativeDayOfEmergence.getValue()).isBefore(tCurrentDate) ||
					SowingDate.getValue().plusDays(cRelativeDayOfEmergence.getValue()).isEqual(tCurrentDate)))
			{
				EmergenceDate.setValue(tCurrentDate, this);
				EmergenceDOY.setValue(tCurrentDOY, this);
				IsEmergence.setValue(true, this);
			}
			if (AnthesisDate.getValue() == null && sTSUM.getValue() >= cAirTemperatureSumAnthesis.getValue()) 
			{
				AnthesisDate.setValue(tCurrentDate, this);
				AnthesisDOY.setValue(tCurrentDOY, this);
				IsAnthesis.setValue(true, this);
			}
			if (MilkripenessDate.getValue() == null && cAirTemperatureSumMilkripeness.getValue() != null && sTSUM.getValue() >= cAirTemperatureSumMilkripeness.getValue()) 
			{
				MilkripenessDate.setValue(tCurrentDate, this);
				MilkripenessDOY.setValue(tCurrentDOY, this);
				IsMilkripeness.setValue(true, this);
			}
			if (MaturityDate.getValue() == null && sTSUM.getValue() >= cAirTemperatureSumMaturity.getValue()) 
			{
				MaturityDate.setValue(tCurrentDate, this);
				MaturityDOY.setValue(tCurrentDOY, this);
				IsMaturity.setValue(true, this);
			}

		}
		IsPhenologyEvent.setValue(IsSowing.getValue() || IsEmergence.getValue() || IsAnthesis.getValue() || IsMilkripeness.getValue() || IsMaturity.getValue(), this);

	}

	private void resetStates()
	{
		sDevStage.setDefaultValue();
		RTEFF.setDefaultValue();
		sTSUM.setDefaultValue();

		SowingDate.setDefaultValue();
		EmergenceDate.setDefaultValue();
		MilkripenessDate.setDefaultValue();
		AnthesisDate.setDefaultValue();
		MaturityDate.setDefaultValue();

	}

	/**
	 * creates a clone from this SimComponent for use in other threads
	 * 
	 * @see net.simplace.sim.model.FWSimComponent#clone(net.simplace.sim.util.FWSimVarMap)
	 */
	@Override
	protected FWSimComponent clone(FWSimVarMap aVarMap) 
	{
		return new LintulPhenology(iName, iFieldMap, iInputMap, iSimComponentElement, aVarMap, iOrderNumber);
	}
}
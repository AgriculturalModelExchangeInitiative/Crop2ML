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
 * LintulBiomass.java
 *
 * Responsible developers: Gunther Krauss, Crop Science Group, Katzenburgweg 5, 53115 Bonn, Germany
 *                         Andreas Enders, Crop Science Group, Katzenburgweg 5, 53115 Bonn, Germany
 * Contact Information:    lapit@uni-bonn.de
 * More information on <http://www.simplace.net>
 */

package net.simplace.sim.components.models.lintul;

import static java.lang.StrictMath.*;

import java.util.HashMap;

import net.simplace.sim.components.util.FSTFunctions;
import net.simplace.sim.components.util.InterpolationTable;
import net.simplace.sim.model.FWSimComponent;
import net.simplace.sim.util.FWSimVarMap;
import net.simplace.sim.util.FWSimVariable;
import net.simplace.sim.util.FWSimVariable.CONTENT_TYPE;
import net.simplace.sim.util.FWSimVariable.DATA_TYPE;

import org.jdom.Element;

/**
 * WIKI_START
 *
 * LintulBiomass.java calculates daily increase in crop total biomass and LAI depending on intercepted radiation and the occurence of nitrogen or water stress
 *
 * === Light interception ===
 *
 * Incoming radiation (input variable DTR in MJ m-2) is intercepted by the crop canopy depending on the extinction coefficient kc and the actual LAI
 * assuming that photosynthetically active radiation is 50% of the global radiation
 *
 * Intercepted photosynthetically active radiation (PARINT in MJ m-2) is then calculated as:
 *
 * WIKI_END
 *   \[
 *   \begin{eqnarray}
 *   PARINT & = & 0.5 \cdot DRT (1 - e^{-k LAI})
 *   \end{eqnarray}
 *   \]
 * WIKI_START
 *
 *  === Daily increase in total biomass ===
 *
 * Daily increase in total crop biomass is calculated based on the intercepted photosynthetically active radiation (PARINT) depending on the crop specific light use efficiency (LUE)
 * and on the major stress occuring at the same day.
 * In the case of predominant drought stress, daily total biomass increase (GTOTAL in g m-2) is
 *
 * WIKI_END
 *   \[
 *   \begin{eqnarray}
 *   GTOTAL & = & LUE \cdot PARINT \cdot  TRANRF
 *   \end{eqnarray}
 *   \]
 * WIKI_START
 * where TRANRF is the transpiration reduction factor calculated in the SimComponent LintulWaterStress.java as the ratio
 * between actual and potential crop transpiration.
 *
 * In the case of predominant nitrogen deficiency (i.e. the nitrogen nutrition index NNI is lower than TRANRF), light use efficiency is reduced by the LueReductionToNStress factor
 *  and daily total biomass increase (GTOTAL) is calculated as
 * WIKI_END
 *   \[
 *   \begin{eqnarray}
 *   GTOTAL & = & LUE \cdot PARINT \cdot LueReductionToNStress
 *   \end{eqnarray}
 *   \]
 *
 * WIKI_START
 * where the LueReductionToNStress factor is calculated as
 * WIKI_END
 *
 * \[
 *   \begin{eqnarray}
 *   LueReductionToNStress & = & 1 - LueNStressReduction \cdot e^{(1 - NNI)}
 *   \end{eqnarray}
 * \]
 * WIKI_START
 * The crop specific reduction constant LueNStressReduction and the Nitrogen Nutrition index (NNI) are both dimension less and NNI is defined by the
 *  the ratio between actual crop N concentration and critical crop N concentration (half of optimum N concentration which depends on crop and development stage)
 *  (for details refer to the SimComponent Ndemand.java).
 *
 * === Daily increase in LAI ===
 *
 * The daily increase of total crop biomass is partioned into root, stem, leaves and storage organs depending on a crop development specific partioning factor which is defined in the
 * crop property file. The daily increase in leaf biomass is calculated as the fraction FLV of the total increase GTOTAL (g m-2):
 * WIKI_END
 *
 * \[
 *   \begin{eqnarray}
 *   GLV & = & GTOTAL \cdot FLV
 *   \end{eqnarray}
 * \]
 * WIKI_START
 * Then the daily increase in leaf area index (LAI) is derived from the increase of leaf biomass by multiplication with the specific leaf weight (SLA in m2 g-1)
 * WIKI_END
 * \[
 *   \begin{eqnarray}
 *   GLAI & = & GLV \cdot SLA
 *   \end{eqnarray}
 * \]
 * WIKI_START
 * In the event of nitrogen deficiency, SLA is reduced according to
 * WIKI_END
 *  \[
 *   \begin{eqnarray}
 *   SLA & = & SLA \cdot e^{-SlaNStressReduction(1-NNI)}
 *   \end{eqnarray}
 * \]
 * WIKI_START
 * with the dimension less factor SlaNStressReduction expressing the crop specific sensitivity of specific leaf weight to N stress.
 * The SlaNStressReduction factor which is a value between 0 and 1 must be defined in the crop property file.
 *
 * In the early stage of growth, if the phenological development has not yet passed a user defined stage DevStageRGRL or if LAI is below a user defined critical threshold (0.1875*LaiCritical),
 * the daily increase of LAI (GLAI) is governed by the early rate of growth of green leaves (RGRL) according to the equation
 * WIKI_END
 *
 *  \[
 *   \begin{eqnarray}
 *   GLAI & = & LAI \cdot  (e^{RGRL \cdot DTEFF)}-1) \cdot TRANRF  \cdot e^{-LaiNStressReduction(1-NNI)}
 *   \end{eqnarray}
 *  \]
 * WIKI_START
 *
 * where DTEFF is the effective temperature rate before anthesis (°C d-1) as calculated by the weather transformer. In this early stage, both water stress (TRANRF) as well as nitrogen deficiency (NNI) reduce growth rate of LAI.
 *
 * The output variables GTOTAL and GLAI are important input variables to the SimComponents LintulPartitioning.java and EvapTranDemand.java
 *
 *   '''References:'''
 * Van Oijen, M. and P. Lefelaar. 2008. Lintul-2: water limited crop growth: A simple general crop growth model for water-limited growing conditions. Waageningen University, The Netherlands.
 * Shibu, M. E., P. A. Leffelaar, H. van Keulen, and P. K. Aggarwal. 2010. LINTUL3, a simulation model for nitrogen-limited situations: Application to rice. European Journal of Agronomy 32:255-271.
 * WIKI_END
 *
 * @author Gunther Krauss
 * @author Andreas Enders
 * @author Thomas Gaiser
 * Stati:
 * @unittested
 * @validated
 * @testdata
 * @documented
 * @unitchecked
 * @works
 * @reference
 *
 * unit test, validiert, testdaten liegen vor, liegt dokumentation vor, units checked
 * unit test, testdaten liegen vor, liegt dokumentation vor
 * technische funktion
 *
 */
public class LintulBiomass extends FWSimComponent
{
	// Constants
	private FWSimVariable<Double> cK;
	private FWSimVariable<Double> cLAII;
	private FWSimVariable<Double> cLaiCritical;
	private FWSimVariable<Double> cRDRSHM;
	private FWSimVariable<Double> cRGRL;
	private FWSimVariable<Double> cRDRNS;
	private FWSimVariable<Double> cRDRL;
	private FWSimVariable<Double> cSLA;
	private FWSimVariable<Double> cLueNStressReduction;
	private FWSimVariable<Double> cSlaNStressReduction;
	private FWSimVariable<Double> cLaiNStressReduction;
	private FWSimVariable<Double[]> RDRT;
	private FWSimVariable<Integer> cRelativeDayOfEmergence;
	private FWSimVariable<Double> cDevStageRGRL;
	private FWSimVariable<Double> cGrainToRootsDailyBiomass;
	private FWSimVariable<Double> cGrainToRootsDevStage;

    private FWSimVariable<Double[]> cRDRLeavesTableMeanTemperature;
    private FWSimVariable<Double[]> cRDRLeavesTableRelativeRate;




	// inputs

	private FWSimVariable<Boolean> iDoSow;
	private FWSimVariable<Boolean> iDoHarvest;
	private FWSimVariable<Double> iUnusedRootBiomass;
	private FWSimVariable<Double> iNitrogenNutritionIndex;
	private FWSimVariable<Double> DAVTMP;
	private FWSimVariable<Double> DTEFF;
	private FWSimVariable<Double> RDD;
	private FWSimVariable<Double> iDevStage;
	private FWSimVariable<Double> WCA;
	private FWSimVariable<Double> iTRANRF;
	private FWSimVariable<Double> FRT;
	private FWSimVariable<Double> FST;
	private FWSimVariable<Double> FSO;
	private FWSimVariable<Double> FLV;
	private FWSimVariable<Double> iLUE;

	private FWSimVariable<Double> iLeaveSenescenceHeatStressFactor;

	// states
	private FWSimVariable<Double> sWLVD;
	private FWSimVariable<Double> sWLVG;
	private FWSimVariable<Double> sWRT;
	private FWSimVariable<Double> sWSO;
	private FWSimVariable<Double> sWST;
	private FWSimVariable<Double> rDeadStems;
	private FWSimVariable<Double> rDeadLeaves;
	private FWSimVariable<Double> sWLV;
	private FWSimVariable<Double> sLAI;

	private FWSimVariable<Double> rRWLVG;
	private FWSimVariable<Double> rRWRT;
	private FWSimVariable<Double> rDLAI;

	// outputs
	private FWSimVariable<Double> GTOTAL;
	private FWSimVariable<Double> AGBM;
	private FWSimVariable<Double> PARINT;

	private FWSimVariable<Integer> sDaysSinceSowing;

    private InterpolationTable RDRT_interpol;



		/**
	 * @param aName
	 * @param aFieldMap
	 * @param aInputMap
	 * @param aSimComponentElement
	 * @param aVarMap
		 * @param aOrderNumber
	 */
	public LintulBiomass(String aName, HashMap<String, FWSimVariable<?>> aFieldMap,
			HashMap<String, String> aInputMap, Element aSimComponentElement, FWSimVarMap aVarMap, int aOrderNumber)
	{
		super(aName, aFieldMap, aInputMap, aSimComponentElement, aVarMap, aOrderNumber);
	}

	/**
	 * Empty constructor used by class.forName()
	 */
	public LintulBiomass()
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
		// constants
		addVariable(FWSimVariable.createSimVariable("cK",
				"Extinction coefficient for photosynthetically active radiation",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 0.6, this));
		addVariable(FWSimVariable.createSimVariable("cLAII",
				"Initial LAI",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/square_metre_per_square_metre", 0d, 0.1, 0.012, this));
		addVariable(FWSimVariable.createSimVariable("cLaiCritical",
				"Critical LAI beyond which leaves die due to self-shading",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/square_metre_per_square_metre", 0d, 6d, 4d,	this));
		addVariable(FWSimVariable.createSimVariable("cRDRSHM",
				"Maximum relative death rate of leaves due to shading",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 0.05, this));
		addVariable(FWSimVariable.createSimVariable("cRDRNS",
				"max. relative death rate of leaves due to N stress",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 0.05, this));
		addVariable(FWSimVariable.createSimVariable("cRDRL",
				"max. rel. death rate of leaves due to water stress",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 0.05, this));
		addVariable(FWSimVariable.createSimVariable("cRGRL",
				"Relative growth rate of LAI during exponential growth",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 0.05, this));
		addVariable(FWSimVariable.createSimVariable("cSLA", "Specific Leaf Area",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/square_metre_per_gram", 0d, 20d, 0.20, this));
		addVariable(FWSimVariable.createSimVariable("cLaiNStressReduction",
				"N Stress effect on LAI growth rate",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 1d, this));
		addVariable(FWSimVariable.createSimVariable("cLueNStressReduction",
				"N Stress effect on LUE",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 1d, this));
		addVariable(FWSimVariable.createSimVariable("cSlaNStressReduction",
				"N Stress effect on Specific Leaf Area",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 1d, this));
		addVariable(FWSimVariable.createSimVariable("cRDRT",
				"Table of development specific leaves death rates",
				DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("cRelativeDayOfEmergence",
				"Days between sowing and start of LAI development/plant growth",
				DATA_TYPE.INT, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/day", 0, 366,12, this));
		addVariable(FWSimVariable.createSimVariable("cDevStageRGRL",
				"Development stage of the crop up to which the growth rate of LAI is calculated based on the constant, user defined rate RGRL",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0, 2d, 0.3, this));
		addVariable(FWSimVariable.createSimVariable("cGrainToRootsDailyBiomass",
				"Daily biomass, that is supplied from the seeds to the roots after emergency",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre", 0, 10d, 0.2, this));
		addVariable(FWSimVariable.createSimVariable("cGrainToRootsDevStage",
				"DevStage after emergence up to which root biomass is supplied by the seed",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0, 2d, 0.0, this));

        addVariable(FWSimVariable.createSimVariable("cRDRLeavesTableMeanTemperature", "Daily mean temperature for relative death rate of leaves (c.f. RDRT)", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", null, null, null, this));
        addVariable(FWSimVariable.createSimVariable("cRDRLeavesTableRelativeRate", "Relative death rate of leaves as a function of daily mean temperature (c.f. RDRT)", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/reciprocal_day", null, null, null, this));




		// inputs
		addVariable(FWSimVariable.createSimVariable("iDoHarvest",
				"Flag to specify the day of harvest",
				DATA_TYPE.BOOLEAN, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null,false, this));
		addVariable(FWSimVariable.createSimVariable("iDoSow",
				"Flag to specify the day of sowingt",
				DATA_TYPE.BOOLEAN, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, false, this));
		addVariable(FWSimVariable.createSimVariable("iUnusedRootBiomass",
				"Root Biomass which was not used by roots due to root growth limitations (is returned to biomass growth rate of the next day)",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre_day", 0d, 20000d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("iNitrogenNutritionIndex",
				"Nitrogen Nutrition Index is the ratio between actual crop N concentration and critical N concentration (half of optimum N concentration which depends on crop and development stage)",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 1d, this));
		addVariable(FWSimVariable.createSimVariable("iAirTemperatureMean",
				"Measured daily average air temperature (input calculated from weather file)",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", -50d, 60d, 0.0, this));
		addVariable(FWSimVariable.createSimVariable("iEffectiveTempRateBeforeAnt",
				"Daily effective temperature before anthesis",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", 0d, 40d, 0.0, this));
		addVariable(FWSimVariable.createSimVariable("iEffectiveTempRateAfterAnt",
				"Daily effective temperature after anthesis",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", 0d, 40d, 0.0, this));
		addVariable(FWSimVariable.createSimVariable("iRadiation",
				"Daily global radiation (input from weather file)",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/megajoule_per_square_metre", 0d, 30d, 0.0, this));
		addVariable(FWSimVariable.createSimVariable("iDevStage",
				"Development stage of the plant (1.0=anthesis, 2.0=physiological maturity",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 3d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("iCropAvailWaterContent",
				"Crop available water content in the soil",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/millimetre",0d, 20d, 0.0001, this));
		addVariable(FWSimVariable.createSimVariable("iTRANRF",
				"Transpiration reduction factor (TRANRF) as ratio between actual and potential crop transpiration",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one",0d, 1d, 1d, this));
		addVariable(FWSimVariable.createSimVariable("iPartRootFactor",
				"Proportion of daily total biomass increase partitioned to the roots (input from LintulPartitioning.java)",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one",0d, 1d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("iPartStemsFactor",
				"Proportion of daily total biomass increase partitioned to the stem (input from LintulPartitioning.java)",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one",0d, 1d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("iPartStorageOrgansFactor",
				"Proportion of daily total biomass increase partitioned to the storage organs (input from LintulPartitioning.java)",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one",0d, 1d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("iPartLeavesFactor",
				"Proportion of daily total biomass increase partitioned to the leaves (input from LintulPartitioning.java)",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one",0d, 1d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("iLUE",
				"Light Use Efficiency",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_megajoule",0d, 10d, 3d, this));

		addVariable(FWSimVariable.createSimVariable("iLeaveSenescenceHeatStressFactor",
				"Factor that increases leaf senescence due to heat stress",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, null, 1.0, this));

		// states
		addVariable(FWSimVariable.createSimVariable("sDaysSinceSowing",
				"Days since Sowing",
				DATA_TYPE.INT, CONTENT_TYPE.state, "http://www.wurvoc.org/vocabularies/om-1.8/day", 0, 1000, 0, this));
		addVariable(FWSimVariable.createSimVariable("sWLV",
				"Biomass of leaves",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.state, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre", 0d, 2000d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("sWLVG",
				"Biomass of green leaves",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.state, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre", 0d, 2000d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("sWLVD",
				"Biomass of dead leaves",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.state, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre",0d, 2000d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("sWST",
				"Biomass of stems",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.state, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre",0d, 2000d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("sWSO",
				"Biomass of Storage Organs",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.state, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre",0d, 2000d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("sWRT",
				"Biomass of Roots",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.state, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre",0d, 2000d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("sLAI",
				"Leaf area index",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.state, "http://www.wurvoc.org/vocabularies/om-1.8/square_metre_per_square_metre",0d, 20d, 0d, this));

		// rates
		addVariable(FWSimVariable.createSimVariable("rDeadLeaves",
				"Rate of dead leaves",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.rate, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre_day",-200d, 200d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("rDeadStems",
				"Rate of dead stems",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.rate, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre_day",-200d, 200d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("rRWLVG",
				"Rate of change in weight of green leaves",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.rate, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre_day",-200d, 200d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("rRWRT",
				"Rate of change in weight of roots",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.rate, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre_day",-200d, 200d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("rDLAI",
				"Rate of change of Leaf area index",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.rate, "http://www.wurvoc.org/vocabularies/om-1.8/square_metre_per_square_metre",-10d, 10d, 0d, this));

		// outputs
		addVariable(FWSimVariable.createSimVariable("GTOTAL",
				"Growth rate of total crop dry matter",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre_day", null, null, 0d, this));
		addVariable(FWSimVariable.createSimVariable("AboveGroundBiomass",
				"Sum of Biomass fractions above ground",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre", 0d, 10000d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("PARINT",
				"Intercepted photosynthetically active radiation",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/megajoule_per_square_metre_day",0d, 30d, 0d, this));

		return iFieldMap;
	}

	/**
	 *  initializes the fields by getting input and output FWSimVariables from VarMap
	 *
	 * @see net.simplace.sim.model.FWSimComponent#init()
	 */
	@SuppressWarnings("unchecked")
	@Override
	protected void init()
	{//TODO: InterpolationTables improvement refs #135, #136
    DAVTMP = (FWSimVariable<Double>) getVariable("iAirTemperatureMean");
    DTEFF = (FWSimVariable<Double>) getVariable("iEffectiveTempRateBeforeAnt");
    RDD = (FWSimVariable<Double>) getVariable("iRadiation");
    WCA = (FWSimVariable<Double>) getVariable("iCropAvailWaterContent");
    FRT = (FWSimVariable<Double>) getVariable("iPartRootFactor");
    FST = (FWSimVariable<Double>) getVariable("iPartStemsFactor");
    FSO = (FWSimVariable<Double>) getVariable("iPartStorageOrgansFactor");
    FLV = (FWSimVariable<Double>) getVariable("iPartLeavesFactor");

    AGBM = (FWSimVariable<Double>) getVariable("AboveGroundBiomass");

    RDRT_interpol = new InterpolationTable(cRDRLeavesTableMeanTemperature, cRDRLeavesTableRelativeRate, RDRT, this);

	}


	private void resetOnHarvest()
	{

		sDaysSinceSowing.setDefaultValue();

		sWLV.setDefaultValue();
		sWLVG.setDefaultValue();
		sWLVD.setDefaultValue();
		sWST.setDefaultValue();
		sWSO.setDefaultValue();
		sWRT.setDefaultValue();
		sLAI.setDefaultValue();

		rRWLVG.setDefaultValue();
		rRWRT.setDefaultValue();
		rDLAI.setDefaultValue();

		GTOTAL.setDefaultValue();
		PARINT.setDefaultValue();
		AGBM.setDefaultValue();

		rDeadLeaves.setDefaultValue();
		rDeadStems.setDefaultValue();
	}

	private void resetOnSowing()
	{

		sDaysSinceSowing.setDefaultValue();

		sWLV.setDefaultValue();
		sWLVG.setDefaultValue();
		sWLVD.setDefaultValue();
		sWST.setDefaultValue();
		sWSO.setDefaultValue();
		sWRT.setDefaultValue();
		sLAI.setDefaultValue();
		rRWRT.setValue(0.0001, this);

		rRWLVG.setDefaultValue();
		rRWRT.setDefaultValue();
		rDLAI.setDefaultValue();

		rDeadLeaves.setDefaultValue();
		rDeadStems.setDefaultValue();

		PARINT.setDefaultValue();
		AGBM.setDefaultValue();
	}

	/**
	 * process the algorithm and write the results back to VarMap
	 *
	 * @see net.simplace.sim.model.FWSimComponent#process()
	 */
	@Override
	protected void process()
	{

		if(iDoHarvest.getValue())
			resetOnHarvest();
		if (iDoSow.getValue())
		{

				resetOnSowing();
		}
		else if(RDRT_interpol.getXValues()==null) return;


		double tRLAI = 0; // Growth rate of LAI m 2 m -2 d -1
		double tRWLVG = 0; // Net rate of increase weight of green leaves g m -2 d -1
		double RWST = 0; // Rate of increase weight of stems g m -2 d -1
		double RWSO = 0; // Rate of increase weight of storage organs g m -2 d -1
		double tRWRT = 0; // Rate of increase weight of roots g m -2 d -1

		double DTR = RDD.getValue();// / 1000.0; // Daily global radiation MJ m -2 d -1

		double PARINT_Neu = 0.5 * DTR * (1.0 - exp(-cK.getValue() * sLAI.getValue()));
		// New Intercepted photosynthetically active radiation MJ m -2 d -1

		// There are two formulas in Lintul4
		// double LueReductionToNStress = Exp(-LueNStressReduction * (1 -
		// L2In.NitrogenNutritionIndex));
		//double LueReductionToNStress = FSTFunctions.LIMIT(0, 1, 1 - LueNStressReduction.getValue() * (1 - NitrogenNutritionIndex.getValue()));
        double LueReductionToNStress = FSTFunctions.LIMIT(0, 1, 1 - cLueNStressReduction.getValue()*pow(1.000- iNitrogenNutritionIndex.getValue(),2));
		if (iTRANRF.getValue() < LueReductionToNStress)
		{
			GTOTAL.setValue(iLUE.getValue() * PARINT.getValue() * iTRANRF.getValue(), this);
			// Growth rate of total crop dry matter g m -2 d -1
		}
		else
		{
			GTOTAL.setValue(iLUE.getValue() * PARINT.getValue() * LueReductionToNStress, this);
		}
		GTOTAL.setValue(GTOTAL.getValue() + iUnusedRootBiomass.getValue(), this);



		// /#region DLAI - LAI increase per day

		double DLV;
		double DLAI;
		if (iDevStage.getValue() > 0 && sDaysSinceSowing.getValue() >= cRelativeDayOfEmergence.getValue())
		{
			// SLA reduction due to N Stress
			double tSLA = cSLA.getValue()
					* exp(-cSlaNStressReduction.getValue()
							* (1 - iNitrogenNutritionIndex.getValue()));

			double RDRDV = 0;
			if (iDevStage.getValue() > 1)
			// in Lintul4 there is an extra parameter DVSDLT, (For WW its 1.0)
			{
				RDRDV = FSTFunctions.INSW(iDevStage.getValue() - 1, 0,
						RDRT_interpol.getValueAt(DAVTMP.getValue()));
				// Calculation Method from Lintul2
			}

			double RDRSH = max(0, cRDRSHM.getValue() * (sLAI.getValue() - cLaiCritical.getValue())
					/ cLaiCritical.getValue());
			// RDRSHM - Shading
			double RDRDRY = (1 - iTRANRF.getValue()) * cRDRL.getValue(); // RDRL
			double RDR = max(max(RDRDV, RDRSH), RDRDRY) * iLeaveSenescenceHeatStressFactor.getValue();

			double DLVNS = 0;
			double DLAINS = 0;
			if (iNitrogenNutritionIndex.getValue() < 1) {
				DLVNS = sWLVG.getValue() * cRDRNS.getValue() * (1 - iNitrogenNutritionIndex.getValue()); // RDRNS
				DLAINS = DLVNS * tSLA;
			}

			double DLVS = sWLVG.getValue() * RDR;
			double DLAIS = sLAI.getValue() * RDR;

			DLV = DLVS + DLVNS;
			DLAI = DLAIS + DLAINS;

		}
		else
		{
			DLAI = 0d;
			DLV = 0d;
		}

		if (0 < iDevStage.getValue() && iDevStage.getValue() < cGrainToRootsDevStage.getValue())
		{
			tRWRT = cGrainToRootsDailyBiomass.getValue() + GTOTAL.getValue() * FRT.getValue();
			RWST = GTOTAL.getValue() * FST.getValue();
			RWSO = GTOTAL.getValue() * FSO.getValue();
			tRWLVG = GTOTAL.getValue() * FLV.getValue() - DLV;
		}

		else
		{
			tRWRT = GTOTAL.getValue() * FRT.getValue();
			RWST = GTOTAL.getValue() * FST.getValue();
			RWSO = GTOTAL.getValue() * FSO.getValue();
			tRWLVG = GTOTAL.getValue() * FLV.getValue() - DLV;
		}


		double GLV = FLV.getValue() * GTOTAL.getValue();
		// Growth rate of leaf dry matter g m -2 d -1
		// double GLAI = CalculateGLAI(relDEM, LAI, LAICR, LAII, DevStage,
		// DTEFF, GLV, WC, TRANRF, NitrogenNutritionIndex, DELT);

		// SLA reduction due to N Stress
		double tSLA = cSLA.getValue()
				* exp(-cSlaNStressReduction.getValue() * (1 - iNitrogenNutritionIndex.getValue()));
		double GLAI = tSLA * GLV; // Growth rate of leaf area index m 2 m -2 d
									// -1

		if (iDevStage.getValue() < cDevStageRGRL.getValue() && sLAI.getValue() < 0.1875 * cLaiCritical.getValue()) {
			GLAI = sLAI.getValue()
					* (exp(cRGRL.getValue() * DTEFF.getValue()) - 1.0)
					* iTRANRF.getValue()
					* exp(-cLaiNStressReduction.getValue()
							* (1 - iNitrogenNutritionIndex.getValue()));
		}

		if (iDevStage.getValue() == 0)
		{
			GLAI = 0;
		}
		else if (sLAI.getValue() == 0.0 && WCA.getValue() > 0 && sDaysSinceSowing.getValue() >= cRelativeDayOfEmergence.getValue())
		{
			GLAI = cLAII.getValue();
		}

		tRLAI = GLAI - DLAI;



		sDaysSinceSowing.setValue(sDaysSinceSowing.getValue()+1, this);

		sLAI.setValue(sLAI.getValue()+ tRLAI, this);
		sWLVG.setValue(sWLVG.getValue()+ tRWLVG, this);
		sWLVD.setValue(sWLVD.getValue()+ DLV, this);
		sWST.setValue(sWST.getValue()+ RWST, this);
		sWSO.setValue(sWSO.getValue()+ RWSO, this);
		sWRT.setValue(sWRT.getValue()+ tRWRT - iUnusedRootBiomass.getValue(), this);
		sWLV.setValue(sWLVG.getValue()+ sWLVD.getValue(), this);

		rRWLVG.setValue(tRWLVG, this);
		rRWRT.setValue(tRWRT, this);
		if(iDoSow.getValue() && rRWRT.getValue()==0.0)
			rRWRT.setValue(0.0001, this);
		rDLAI.setValue(tRLAI, this);

		rDeadLeaves.setValue(DLV, this);
		if(iDevStage.getValue() > 1.5)
			rDeadStems.setValue(0.02*sWST.getValue(),this);
		else
			rDeadStems.setValue(0d,this);

		AGBM.setValue(sWLV.getValue() + sWST.getValue() + sWSO.getValue(), this);

		PARINT.setValue(PARINT_Neu,this);

	}

	/**
	 * creates a clone from this SimComponent for use in other threads
	 *
	 * @see net.simplace.sim.model.FWSimComponent#clone(net.simplace.sim.util.FWSimVarMap)
	 */
	@Override
	protected FWSimComponent clone(FWSimVarMap aVarMap)
	{
		return new LintulBiomass(iName, iFieldMap, iInputMap, iSimComponentElement, aVarMap, iOrderNumber);
	}
}
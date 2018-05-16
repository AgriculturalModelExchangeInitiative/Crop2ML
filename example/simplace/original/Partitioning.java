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
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with SIMPLACE. If not, see <http://www.gnu.org/licenses/>.
 *
 * Partitioning.java
 *
 * Responsible developers: Gunther Krauss, Crop Science Group, Katzenburgweg 5, 53115 Bonn, Germany
 * Andreas Enders, Crop Science Group, Katzenburgweg 5, 53115 Bonn, Germany
 * Contact Information: lapit@uni-bonn.de
 * More information on <http://www.simplace.net>
 */

package net.simplace.sim.components.experimental.amei;

import static java.lang.StrictMath.exp;
import static java.lang.StrictMath.max;

import net.simplace.sim.components.util.FSTFunctions;
import net.simplace.sim.model.FWSimComponent;
import net.simplace.sim.util.FWSimVarMap;
import net.simplace.sim.util.FWSimVariable;
import net.simplace.sim.util.FWSimVariable.CONTENT_TYPE;
import net.simplace.sim.util.FWSimVariable.DATA_TYPE;

import java.util.HashMap;

import org.jdom.Element;

/**
 * WIKI_START
 * !Partitioning.java calculates the fractions of the daily total biomass to be distributed into the plant organs leaves
 * (FractionLeaves), roots (FractionRoot), stem (FractionStems) and storage organs (FractionStorageOrgans) in the !SimComponent !LintulBiomass.
 * The crop and development stage specific fractions for each organ provided by the user in the partitioning tables in
 * the crop properties file (FRTTB, FLVTB, FSTTB, FSOTB)
 * are modified daily according to the dominance of either drought or nitrogen stress.
 *
 * === Effect of drought ===
 * If, at a given day, drought stress is dominant, the fraction of biomass transfered to the root is increased by
 * multiplication with the root fraction modification factor (FRTMOD) which is calculated
 * according to the equation
 *
 * WIKI_END
 * \[
 * \begin{eqnarray}
 * FRTMOD & =& Max(1.0, 1.0 / (TRANRF + 0.5)
 * \end{eqnarray}
 * \]
 * WIKI_START
 *
 * where TRANRF is the transpiration reduction factor calculated in the !SimComponent !LintulWaterStress.
 * Both factors are dimensionless, FTR ranging between 0 and 1 and FRTMOD is equal or greater than 1. The root partition
 * fraction provided in the partitioning table (FRTTB) is then multiplied with
 * FRTMOD thereby increasing the amount of assimilates transfered to the roots in the event of moderate to severe
 * drought stress (TRANRF < 0.5). The other fractions (FLVTB, FSTTB, FSOTB) are
 * then reduced equally to ensure that the sum of all fractions remains equal to 1.
 *
 * === Effect of nitrogen stress ===
 * If, at a given day, nitrogen stress is dominant, the fraction of biomass transfered to the leaves is reduced with the
 * leaf fraction modification factor (FLVMOD):
 * WIKI_END
 * \[
 * \begin{eqnarray}
 * FLVMOD & =& e^{-PartitionNStressReduction \cdot (1-NitrogenNutritionIndex)}
 * \end{eqnarray}
 * \]
 * WIKI_START
 * where FLVMOD is the leaf fraction partitioning factor (0,1) and NitrogenNutritionIndex is the Nitrogen Nutrition Index calculated in the
 * !SimComponent NDemand. The `PartitionNStressReduction` factor is
 * user specified in the crop properties file. All factors are dimensionless ranging between 0 and 1. The excess biomass
 * is then transfered to the stem.
 *
 * == References ==
 * van Oijen, M. and P. Lefelaar. 2008. Lintul-2: water limited crop growth: A simple general crop growth model for
 * water-limited growing conditions. Waageningen University, The Netherlands.
 * WIKI_END
 *
 * 
 * @author Gunther Krauss
 * @author Andreas Enders
 * @author Thomas Gaiser
 *
 *         Component for the Lintul crop model
 *
 */
public class Partitioning extends FWSimComponent
{

	// constants
	private FWSimVariable<Double[]> cRootsPartitioningTableDVS;
	private FWSimVariable<Double[]> cRootsPartitioningTableFraction;
	private FWSimVariable<Double[]> cStemsPartitioningTableDVS;
	private FWSimVariable<Double[]> cStemsPartitioningTableFraction;
	private FWSimVariable<Double[]> cStorageOrgansPartitioningTableDVS;
	private FWSimVariable<Double[]> cStorageOrgansPartitioningTableFraction;
	private FWSimVariable<Double[]> cLeavesPartitioningTableDVS;
	private FWSimVariable<Double[]> cLeavesPartitioningTableFraction;

	private FWSimVariable<Double> PartitionNStressReduction;

	// input
	private FWSimVariable<Double> DevStage;
	private FWSimVariable<Double> TRANRF;
	private FWSimVariable<Double> NitrogenNutritionIndex;

	// output
	private FWSimVariable<Double> FractionRoot;
	private FWSimVariable<Double> FractionLeaves;
	private FWSimVariable<Double> FractionStems;
	private FWSimVariable<Double> FractionStorageOrgans;

	/**
	 *
	 * called by clone method only
	 *
	 * @param aName
	 * @param aFieldMap
	 * @param aInputMap
	 * @param aSimComponentElement
	 * @param aVarMap
	 */
	private Partitioning(String aName, HashMap<String, FWSimVariable<?>> aFieldMap, HashMap<String, String> aInputMap,
			Element aSimComponentElement, FWSimVarMap aVarMap, int aOrderNumber)
	{
		super(aName, aFieldMap, aInputMap, aSimComponentElement, aVarMap, aOrderNumber);
	}

	/**
	 * Empty constructor used by class.forName()
	 */
	public Partitioning()
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
		addVariable(FWSimVariable.createSimVariable("cPartitionNStressReduction", "N Stress on Partitioning",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 1d, this));
		addVariable(FWSimVariable.createSimVariable("cFRTTB", "Fractions Table Root", DATA_TYPE.DOUBLEARRAY,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("cFLVTB", "Fractions Table Leaves", DATA_TYPE.DOUBLEARRAY,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("cFSTTB", "Fractions Table Stems", DATA_TYPE.DOUBLEARRAY,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("cFSOTB", "Fractions Table Storage Organs", DATA_TYPE.DOUBLEARRAY,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));

		addVariable(FWSimVariable.createSimVariable("cRootsPartitioningTableDVS",
				"DVS for fraction of total dry matter to roots (c.f. FRTTB)", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.constant,
				"http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("cRootsPartitioningTableFraction",
				"Fraction of total dry matter to roots as function of DVS (c.f. FRTTB)", DATA_TYPE.DOUBLEARRAY,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));

		addVariable(FWSimVariable.createSimVariable("cStemsPartitioningTableDVS",
				"DVS for fraction of total dry matter to stems (c.f. FSTTB)", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.constant,
				"http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("cStemsPartitioningTableFraction",
				"Fraction of total dry matter to stems as function of DVS (c.f. FSTTB)", DATA_TYPE.DOUBLEARRAY,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));

		addVariable(FWSimVariable.createSimVariable("cStorageOrgansPartitioningTableDVS",
				"DVS for fraction of total dry matter to storage organs (c.f. FSOTB)", DATA_TYPE.DOUBLEARRAY,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("cStorageOrgansPartitioningTableFraction",
				"Fraction of total dry matter to storage organs as function of DVS (c.f. FSOTB)", DATA_TYPE.DOUBLEARRAY,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));

		addVariable(FWSimVariable.createSimVariable("cLeavesPartitioningTableDVS",
				"DVS for fraction of total dry matter to leaves (c.f. FLVTB)", DATA_TYPE.DOUBLEARRAY, CONTENT_TYPE.constant,
				"http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));
		addVariable(FWSimVariable.createSimVariable("cLeavesPartitioningTableFraction",
				"Fraction of total dry matter to leaves as function of DVS (c.f. FLVTB)", DATA_TYPE.DOUBLEARRAY,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, null, this));

		addVariable(FWSimVariable.createSimVariable("iDoSow", "if Sowingdate reached fraction tables are initialized",
				DATA_TYPE.BOOLEAN, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, false,
				this));
		addVariable(FWSimVariable.createSimVariable("iTRANRF", "Transpiration reduction factor", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 1d, this));
		addVariable(FWSimVariable.createSimVariable("iNitrogenNutritionIndex", "Nitrogen Nutrition Index", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 1d, this));
		addVariable(FWSimVariable.createSimVariable("iDevStage", "Development stage of the plant", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 3d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("FractionRoot", "Fraction part going to Root compartment",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("FractionLeaves", "Fraction part going to Leaves compartment",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("FractionStems", "Fraction part going to Stems compartment",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("FractionStorageOrgans", "Fraction part going to Storage Organs compartment",
						DATA_TYPE.DOUBLE, CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 0d, this));
		return iFieldMap;
	}

	/**
	 * initializes the fields by getting input and output FWSimVariables from VarMap
	 *
	 * @see net.simplace.sim.model.FWSimComponent#init()
	 */
	@SuppressWarnings("unchecked")
	@Override
	protected void init()
	{
		NitrogenNutritionIndex = (FWSimVariable<Double>) getVariable("iNitrogenNutritionIndex");
		FractionRoot = (FWSimVariable<Double>) getVariable("FractionRoot");
		FractionLeaves = (FWSimVariable<Double>) getVariable("FractionLeaves");
		FractionStems = (FWSimVariable<Double>) getVariable("FractionStems");
		FractionStorageOrgans = (FWSimVariable<Double>) getVariable("FractionStorageOrgans");

	}

	/**
	 * process the algorithm and write the results back to VarMap
	 *
	 * @see net.simplace.sim.model.FWSimComponent#process()
	 */
	@Override
	protected void process()
	{

		// Fraction of dry matter allocated to the roots at optimal water supply
		double FRTWET = FSTFunctions.AFGEN(cRootsPartitioningTableDVS, cRootsPartitioningTableFraction, DevStage.getValue());
		// Drought stress more severe than Nitrogen stress
		if (TRANRF.getValue() < NitrogenNutritionIndex.getValue())
		{
			// Relative modification of FractionRoot by drought
			double FRTMOD = max(1.0, 1.0 / (TRANRF.getValue() + 0.5));
			// Fraction of dry matter allocated to the roots
			FractionRoot.setValue(FRTWET * FRTMOD, this);
			// Relative modification of allocation to shoot by drought
			double FSHMOD = (1.0 - FractionRoot.getValue()) / (1.0 - FractionRoot.getValue() / FRTMOD);
			// Fraction of dry matter allocated to the leaves
			FractionLeaves.setValue(FSTFunctions.AFGEN(cLeavesPartitioningTableDVS, cLeavesPartitioningTableFraction, DevStage.getValue()) * FSHMOD, this);
			// Fraction of dry matter allocated to the stems
			FractionStems.setValue(FSTFunctions.AFGEN(cStemsPartitioningTableDVS, cStemsPartitioningTableFraction, DevStage.getValue()) * FSHMOD, this);
			// Fraction of dry matter allocated to the storage organs
			FractionStorageOrgans.setValue(FSTFunctions.AFGEN(cStorageOrgansPartitioningTableDVS, cStorageOrgansPartitioningTableFraction, DevStage.getValue()) * FSHMOD, this);
		}
		else
		{
			// Relative factor for N-Stress reduction
			double FLVMOD = exp(-PartitionNStressReduction.getValue() * (1 - NitrogenNutritionIndex.getValue()));
			// Fraction of dry matter allocated to the roots
			FractionRoot.setValue(FRTWET, this);
			// Fraction of dry matter allocated to leaves without N-Stress
			double FLVT = FSTFunctions.AFGEN(cLeavesPartitioningTableDVS, cLeavesPartitioningTableFraction, DevStage.getValue());
			// Fraction of dry matter allocated to the leaves
			FractionLeaves.setValue(FLVT * FLVMOD, this);
			// Fraction of dry matter allocated to the stems
			FractionStems.setValue(FSTFunctions.AFGEN(cStemsPartitioningTableDVS, cStemsPartitioningTableFraction, DevStage.getValue()) + FLVT - FractionLeaves.getValue(), this);
			// Fraction of dry matter allocated to the storage organs
			FractionStorageOrgans.setValue(FSTFunctions.AFGEN(cStorageOrgansPartitioningTableDVS, cStorageOrgansPartitioningTableFraction, DevStage.getValue()), this);
		}

	}

	/**
	 * creates a clone from this SimComponent for use in other threads
	 *
	 * @see net.simplace.sim.model.FWSimComponent#clone(net.simplace.sim.util.FWSimVarMap)
	 */
	@Override
	protected FWSimComponent clone(FWSimVarMap aVarMap)
	{
		return new Partitioning(iName, iFieldMap, iInputMap, iSimComponentElement, aVarMap, iOrderNumber);
	}
}
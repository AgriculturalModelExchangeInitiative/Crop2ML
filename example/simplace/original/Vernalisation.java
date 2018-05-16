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
 * Vernalisation.java
 *
 * Responsible developers: Gunther Krauss, Crop Science Group, Katzenburgweg 5, 53115 Bonn, Germany
 * Andreas Enders, Crop Science Group, Katzenburgweg 5, 53115 Bonn, Germany
 * Contact Information: lapit@uni-bonn.de
 * More information on <http://www.simplace.net>
 */

package net.simplace.sim.components.experimental.amei;

import static java.lang.StrictMath.max;
import static java.lang.StrictMath.min;

import net.simplace.sim.model.FWSimComponent;
import net.simplace.sim.util.FWSimVarMap;
import net.simplace.sim.util.FWSimVariable;
import net.simplace.sim.util.FWSimVariable.CONTENT_TYPE;
import net.simplace.sim.util.FWSimVariable.DATA_TYPE;

import java.util.HashMap;

import org.jdom.Element;

/**
 * Reduces daily temperature increment by vernalisation and photoresponse.
 *
 * WIKI_START
 *
 * == Vernalisation==
 *
 * === Vernalisation days ===
 * The daily increment of vernalisation days is determined by daily average temperature `iDAVTEMP`
 * WIKI_END
 * \[
 * \begin{eqnarray}
 * VDI(iDAVTEMP) & = & \left\{
 * \begin{array}{l}
 * 0 & \text{if} \quad iDAVTEMP \lt cTlowCritical\\
 * \frac{iDAVTEMP - cTlowCritical}{cTlow - cTlowCritical} & \text{if} \quad cTlowCritical \le iDAVTEMP \lt cTlow \\
 * 1 & \text{if} \quad cTlow \le iDAVTEMP \lt cThigh \\
 * \frac{cThighCritical - iDAVTEMP}{cThighCritical - cThigh} & \text{if} \quad cThigh \le iDAVTEMP \lt cThighCritical \\
 * 0 & \text{if} \quad cThighCritical \le iDAVTEMP\\
 * \end{array}
 * \right.
 * \end{eqnarray}
 * \]
 * WIKI_START
 *
 * And `sVernalDays` is incremented by `VDI(iDAVTEMP)`.
 *
 * === Vernalisation factor ===
 *
 * `VernalisationFactor` (`VF`) is determined by the `sVernalDays` (`VD`)
 *
 * WIKI_END
 * \[
 * \begin{eqnarray}
 * VF(VD) & = & \left\{
 * \begin{array}{l}
 * 0 & \text{if} \quad VD \lt cVernalDaysMin\\
 * \frac{VD - cVernalDaysMin}{cVernalDaysMax - cVernalDaysMin} & \text{if} \quad cVernalDaysMin \le VD \lt
 * cVernalDaysMax \\
 * 1 & \text{if} \quad cVernalDaysMax \le VD \\
 * \end{array}
 * \right.
 * \end{eqnarray}
 * \]
 * WIKI_START
 *
 *
 * == Applying Vernalisation  ==
 * Before anthesis:
 * WIKI_END
 * \[
 * \begin{eqnarray}
 * RTSUM & = & iTSumBaseAnt \cdot Min(PhotoPeriodeFactor, VernalisationFactor)
 * \end{eqnarray}
 * \]
 * WIKI_START
 *
 * After anthesis:
 * WIKI_END
 * \[
 * \begin{eqnarray}
 * RTSUM & = & iTSumBaseMat \cdot Min(PhotoPeriodeFactor, VernalisationFactor)
 * \end{eqnarray}
 * \]
 * WIKI_START
 *
 * `cApplyPhotoresponse` and `cApplyVernalisation` control whether photoresponse, vernalisation or both are
 * applied. If the control variables are `false`, the coresponding factor is always 1, meaning that the
 * effect is not applied.
 *
 * The state `sVernalDays` has to be reset on each sowing. Therefore the input `iDoSow` has to be provided.
 *
 * == References ==
 * Goudriaan, H.H. Van Laar, 1994. Modelling Potential Crop Growth Processes, Kluwer Academic Publishers, Dordrecht
 * (1994) 238 pp
 * WIKI_END
 *
 */
public class Vernalisation extends FWSimComponent
{

	// Constants
	private FWSimVariable<Boolean> cApplyVernalisation;
	private FWSimVariable<Integer> cVernalDaysMin;
	private FWSimVariable<Integer> cVernalDaysMax;
	private FWSimVariable<Double> cTlowCritical;
	private FWSimVariable<Double> cTlow;
	private FWSimVariable<Double> cThigh;
	private FWSimVariable<Double> cThighCritical;

	// inputs
	private FWSimVariable<Boolean> iDoSow;

	private FWSimVariable<Double> iPhotoPeriodFactor;
	private FWSimVariable<Double> iDAVTMP;
	private FWSimVariable<Double> iDevStage;
	private FWSimVariable<Double> iTSumBaseAnt;
	private FWSimVariable<Double> iTSumBaseShoot;
	private FWSimVariable<Double> iDevStageBaseShoot;
	private FWSimVariable<Double> iTSumBaseMat;

	// States
	private FWSimVariable<Double> sVernalDays;

	// Outputs
	private FWSimVariable<Double> VernalisationFactor;
	private FWSimVariable<Double> RTSUM;

	/**
	 * Called by Clone method only
	 * 
	 * @param aName
	 * @param aFieldMap
	 * @param aInputMap
	 * @param aSimComponentElement
	 * @param aVarMap
	 * @param aOrderNumber
	 */
	public Vernalisation(String aName, HashMap<String, FWSimVariable<?>> aFieldMap,
			HashMap<String, String> aInputMap, Element aSimComponentElement, FWSimVarMap aVarMap, int aOrderNumber)
	{
		super(aName, aFieldMap, aInputMap, aSimComponentElement, aVarMap, aOrderNumber);
	}

	/**
	 * Empty constructor used by class.forName()
	 */
	public Vernalisation()
	{
		super();
	}

	/**
	 * Create the FWSimVariables as interface for this SimComponent
	 *
	 * @see net.simplace.sim.model.FWSimComponent#createVariables()
	 */
	@Override
	public HashMap<String, FWSimVariable<?>> createVariables()
	{

		// Constants
		addVariable(FWSimVariable.createSimVariable("cApplyVernalisation",
				"Flag to activate vernalisation (True: crops requiring vernalisation for flower initialisation",
				DATA_TYPE.BOOLEAN, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, true,
				this));
		addVariable(FWSimVariable.createSimVariable("cVernalDaysMin",
				"Crop specific minimum number of days required for vernalisation", DATA_TYPE.INT, CONTENT_TYPE.constant,
				"http://www.wurvoc.org/vocabularies/om-1.8/one", 0, 100, 10, this));
		addVariable(FWSimVariable.createSimVariable("cVernalDaysMax",
				"Crop specific maximum number of days required for vernalisation", DATA_TYPE.INT, CONTENT_TYPE.constant,
				"http://www.wurvoc.org/vocabularies/om-1.8/one", 0, 400, 70, this));
		addVariable(FWSimVariable.createSimVariable("cTlow",
				"Lower temperature threshold for vernalisation, days with lower mean air temperature are not fully considered as a vernalisation day",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", -40d, 50d,
				3d, this));
		addVariable(FWSimVariable.createSimVariable("cThigh",
				"Upper temperature threshold for vernalisation; days with higher mean air temperature are not fully considered as a vernalisation day ",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", -40d, 50d,
				10d, this));
		addVariable(FWSimVariable.createSimVariable("cTlowCritical",
				"Lower critical temperature threshold for vernalisation, days with lower mean air temperature don't contribute at all for vernalisation day",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", -40d, 50d,
				-4d, this));
		addVariable(FWSimVariable.createSimVariable("cThighCritical",
				"Upper temperature threshold for vernalisation; days with higher mean air temperature don't contribute at all for vernalisation day ",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", -40d, 50d,
				17d, this));

		// inputs
		addVariable(FWSimVariable.createSimVariable("iDoSow", "true if sowing day", DATA_TYPE.BOOLEAN, CONTENT_TYPE.input,
				"http://www.wurvoc.org/vocabularies/om-1.8/one", null, null, false, this));
		addVariable(FWSimVariable.createSimVariable("iPhotoPeriodFactor", "PhotoPeriodFactor", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, null, this));
		addVariable(FWSimVariable.createSimVariable("iDAVTMP", "Daily mean air temperature", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", 0d, 50d, null, this));
		addVariable(FWSimVariable.createSimVariable("iTSumBaseAnt", "Daily effective temperature before anthesis", DATA_TYPE.DOUBLE,
						CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", 0d, 10000d, null, this));
		addVariable(FWSimVariable.createSimVariable("iTSumBaseShoot",
				"Daily effective temperature after shooting - if given", DATA_TYPE.DOUBLE, CONTENT_TYPE.input,
				"http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", 0d, 10000d, null, this));
		addVariable(FWSimVariable.createSimVariable("iDevStageBaseShoot", "Development stage for base shoot",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 10000d, 0.5, this)); 
		addVariable(FWSimVariable.createSimVariable("iTSumBaseMat", "Daily effective temperature after anthesis", DATA_TYPE.DOUBLE,
						CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", 0d, 10000d, null, this));
		addVariable(FWSimVariable.createSimVariable("iDevStage",
				"Development stage of the crop (1.0=anthesis, 2.0=physiological maturity)", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 3d, null, this));

		// States
		addVariable(FWSimVariable.createSimVariable("sVernalDays", "actual Vernal Days", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.state, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 400d, 0d, this));

		// Output
		addVariable(FWSimVariable.createSimVariable("DayLength", "Day Length in minutes", DATA_TYPE.INT, CONTENT_TYPE.out,
				"http://www.wurvoc.org/vocabularies/om-1.8/minute-time", 0, 1441, 0, this));
		addVariable(FWSimVariable.createSimVariable("VernalisationFactor", "daily vernalisation factor", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 0d, this));
		addVariable(FWSimVariable.createSimVariable("RTSUM",
				"Daily effective temperature used to calculate the temperature sum and development stage at a given day",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius", 0d, 50d, null,
				this));

		return iFieldMap;
	}

	/**
	 * @see net.simplace.sim.model.FWSimComponent#init()
	 */

	@Override
	protected void init()
	{
		checkCondition(cApplyVernalisation.getValue() && cVernalDaysMax.getValue() <= cVernalDaysMin.getValue(),
				"cVernalDaysMax " + cVernalDaysMax.getValue() + " have to be bigger than cVernalDaysMin "
						+ cVernalDaysMin.getValue()
						+ ". Module does no valid calculation for vernalisation! Using VernalisationFactor with value 1.");
		checkCondition(
				cApplyVernalisation.getValue() && (cTlowCritical.getValue() >= cTlow.getValue()
						|| cTlow.getValue() >= cThigh.getValue() || cThigh.getValue() >= cThighCritical.getValue()),
				"Temperatures cTlowCritical=" + cTlowCritical.getValue() + ", " + "Temperatures cTlow=" + cTlow.getValue()
						+ ", " + "Temperatures cThigh=" + cThigh.getValue() + ", " + "Temperatures cThighCritical="
						+ cThighCritical.getValue() + " must have distinct and ascending values. "
						+ "Module does no valid calculation for vernalisation! Using VernalisationFactor with value 1. ");

	}

	protected void reset()
	{
		init();
		sVernalDays.setValue(0d, this);
	}

	@Override
	protected void process()
	{
		boolean applyVernalisation = cApplyVernalisation.getValue();

		if (applyVernalisation
				&& (cVernalDaysMax.getValue() <= cVernalDaysMin.getValue() || cTlowCritical.getValue() >= cTlow.getValue()
						|| cTlow.getValue() >= cThigh.getValue() || cThigh.getValue() >= cThighCritical.getValue()))
		{
			applyVernalisation = false;
		}

		if (iDoSow.getValue()) reset();

		final double devstageAnthesis = 1d;

		// the concept of vernal days reduces the temperature rate added
		// to the temperature sum by checking the threshold VernDaysMax,
		// that plant needs to overcome vernalisation
		if (applyVernalisation && sVernalDays.getValue() < cVernalDaysMax.getValue())
		{
			// Thigh signifies an upper threshold
			// for the addition of a fraction of a vernalisation day
			if (iDAVTMP.getValue() >= cThigh.getValue())
			{
				sVernalDays.setValue(
						sVernalDays.getValue() + max(0,
								1 - ((iDAVTMP.getValue() - cThigh.getValue()) / (cThighCritical.getValue() - cThigh.getValue()))),
						this);
			}
			// Tlow signifies a lower threshold
			// for the addition of a fraction of a vernalisation day
			else if (iDAVTMP.getValue() <= cTlow.getValue())
			{
				sVernalDays.setValue(
						sVernalDays.getValue()
								+ max(0, 1 - ((cTlow.getValue() - iDAVTMP.getValue()) / (cTlow.getValue() - cTlowCritical.getValue()))),
						this); // rate >=0, vernal days shouldn't decrease when Temperature is too low or too high
			}
			// if not higher than Thigh and lower than Tlow
			// a whole day is added to the vernal days calculation
			else
			{
				sVernalDays.setValue(sVernalDays.getValue() + 1, this);
			}

			if (sVernalDays.getValue() < cVernalDaysMin.getValue())
			{
				VernalisationFactor.setValue(0d, this);
			}
			else
			{
				VernalisationFactor.setValue(max(0., min(1., (sVernalDays.getValue() - cVernalDaysMin.getValue())
						/ (cVernalDaysMax.getValue() - cVernalDaysMin.getValue()))), this);
			}
		}
		else
		{
			VernalisationFactor.setValue(1d, this);
		}

		double rtsum;
		if (iTSumBaseShoot.getValue() != null && iDevStage.getValue() < devstageAnthesis
				&& iDevStage.getValue() > iDevStageBaseShoot.getValue())
		{
			rtsum = iTSumBaseShoot.getValue() * min(iPhotoPeriodFactor.getValue(), VernalisationFactor.getValue());
		}
		else if (iDevStage.getValue() < devstageAnthesis)
		{
			rtsum = iTSumBaseAnt.getValue() * min(iPhotoPeriodFactor.getValue(), VernalisationFactor.getValue());
		}
		else
		{
			rtsum = iTSumBaseMat.getValue() * min(iPhotoPeriodFactor.getValue(), VernalisationFactor.getValue());
		}
		RTSUM.setValue(rtsum, this);
	}

	/**
	 * @see net.simplace.sim.model.FWSimComponent#clone(net.simplace.sim.util.FWSimVarMap)
	 */
	@Override
	protected FWSimComponent clone(FWSimVarMap aVarMap)
	{
		return new Vernalisation(iName, iFieldMap, iInputMap, iSimComponentElement, aVarMap, iOrderNumber);
	}
}
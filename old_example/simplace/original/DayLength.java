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

import static java.lang.StrictMath.PI;
import static java.lang.StrictMath.asin;
import static java.lang.StrictMath.cos;
import static java.lang.StrictMath.round;
import static java.lang.StrictMath.sin;
import static java.lang.StrictMath.sqrt;

import net.simplace.sim.model.FWSimComponent;
import net.simplace.sim.util.FWSimVarMap;
import net.simplace.sim.util.FWSimVariable;
import net.simplace.sim.util.FWSimVariable.CONTENT_TYPE;
import net.simplace.sim.util.FWSimVariable.DATA_TYPE;

import java.util.HashMap;

import org.jdom.Element;

/**
 * Calculates the day length from physical dependences with the latitude only.
 *
 * WIKI_START
 *
 * 
 * WIKI_END
 *
 */
public class DayLength extends FWSimComponent
{

	private FWSimVariable<Integer[]> pDaylength;

	// Constants
	private FWSimVariable<Double> cLatitude;

	// inputs
	private FWSimVariable<Integer> iDayOfYear;

	// Outputs
	private FWSimVariable<Integer> DayLength;

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
	public DayLength(String aName, HashMap<String, FWSimVariable<?>> aFieldMap,
			HashMap<String, String> aInputMap, Element aSimComponentElement, FWSimVarMap aVarMap, int aOrderNumber)
	{
		super(aName, aFieldMap, aInputMap, aSimComponentElement, aVarMap, aOrderNumber);
	}

	/**
	 * Empty constructor used by class.forName()
	 */
	public DayLength()
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

		// private
		addVariable(FWSimVariable.createSimVariable("pDaylength", "Latitude of the simulated location", DATA_TYPE.INTARRAY,
				CONTENT_TYPE.privat, "http://www.wurvoc.org/vocabularies/om-1.8/minute-time", 0, 1441, null, this));

		// Constants
		addVariable(FWSimVariable.createSimVariable("cLatitude", "Latitude of the simulated location", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/degree", 0d, 90d, 60d, this));

		// inputs
		addVariable(FWSimVariable.createSimVariable("iDayOfYear", "Day Of Year", DATA_TYPE.INT,
				CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/one", 1, 366, null, this));

		// States

		// Output
		addVariable(FWSimVariable.createSimVariable("DayLength", "Day Length in minutes", DATA_TYPE.INT, CONTENT_TYPE.out,
				"http://www.wurvoc.org/vocabularies/om-1.8/minute-time", 0, 1441, 0, this));

		return iFieldMap;
	}

	/**
	 * @see net.simplace.sim.model.FWSimComponent#init()
	 */

	@Override
	protected void init()
	{
		Integer[] dayLengthArray = new Integer[370];
		for (int i = 1; i < 370; i++)
		{
			int TIME = i;
			double tDayLength;
			double SINLAT = sin(PI * cLatitude.getValue() / 180);
			double COSLAT = cos(PI * cLatitude.getValue() / 180);

			double SINDCM = sin(PI * 23.45 / 180);

			double SINDEC = -SINDCM * cos(2 * PI * (TIME + 10) / 365);
			double COSDEC = sqrt(1 - SINDEC * SINDEC);

			double A = SINLAT * SINDEC;
			double B = COSLAT * COSDEC;

			double arg = A / B;
			if (arg > 1) arg = 1;
			if (arg < -1) arg = -1;
			tDayLength = 12 * (1 + (2 / PI) * asin(arg));

			dayLengthArray[i] = (int) round(tDayLength * 60);
		}

		pDaylength.setValue(dayLengthArray, this);

		checkCondition((cLatitude.getValue() == null || cLatitude.getValue() < -90. || cLatitude.getValue() > 90.),
				"No valid latitude provided. Module does no valid calculation for photoresponse! Using PhotoresponseFactor with value 1.");
	}

	@Override
	protected void process()
	{
		DayLength.setValue((Integer) pDaylength.getArrayValue(iDayOfYear.getValue()), this);
	}

	/**
	 * @see net.simplace.sim.model.FWSimComponent#clone(net.simplace.sim.util.FWSimVarMap)
	 */
	@Override
	protected FWSimComponent clone(FWSimVarMap aVarMap)
	{
		return new DayLength(iName, iFieldMap, iInputMap, iSimComponentElement, aVarMap, iOrderNumber);
	}
}
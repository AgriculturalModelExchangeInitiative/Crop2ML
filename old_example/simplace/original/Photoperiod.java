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

import net.simplace.sim.model.FWSimComponent;
import net.simplace.sim.util.FWSimVarMap;
import net.simplace.sim.util.FWSimVariable;
import net.simplace.sim.util.FWSimVariable.CONTENT_TYPE;
import net.simplace.sim.util.FWSimVariable.DATA_TYPE;

import java.util.HashMap;

import org.jdom.Element;

/**
 * Reduces daily temperature increment by photoperiod.
 *
 * WIKI_START
 *
 * Taken as import from BIOMA (Davide Fumagalli). Changed names of the calculation variables.
 * 
 * == References ==
 *
 * Photoperiod effect on development - Stockle, C.O., Donatelli, M., Nelson, R., 2003. CropSyst, a cropping systems simulation model. European Journal of Agronomy, 18, 289-307
 * WIKI_END
 *
 */
public class Photoperiod extends FWSimComponent
{

	// inputs
	private FWSimVariable<Double> iPhotoInhibition;
	private FWSimVariable<Double> iPhotoInsensitivity;
	private FWSimVariable<Integer> iDayLength;

	// Outputs
	private FWSimVariable<Double> PhotoPeriodFactor;

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
	public Photoperiod(String aName, HashMap<String, FWSimVariable<?>> aFieldMap, HashMap<String, String> aInputMap,
			Element aSimComponentElement, FWSimVarMap aVarMap, int aOrderNumber)
	{
		super(aName, aFieldMap, aInputMap, aSimComponentElement, aVarMap, aOrderNumber);
	}

	/**
	 * Empty constructor used by class.forName()
	 */
	public Photoperiod()
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

		// inputs
		addVariable(FWSimVariable.createSimVariable("iPhotoInhibition", "Daily mean air temperature", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/hour", 0., 24., 14., this));
		addVariable(FWSimVariable.createSimVariable("iPhotoInsensitivity", "Daily effective temperature before anthesis",
				DATA_TYPE.DOUBLE, CONTENT_TYPE.constant, "http://www.wurvoc.org/vocabularies/om-1.8/hour", 0., 24.,
				6., this)); // should be renamed into "iTempEffBeforeAnthesis"
		addVariable(FWSimVariable.createSimVariable("iDayLength", "Day length in minutes",
				DATA_TYPE.INT, CONTENT_TYPE.input, "http://www.wurvoc.org/vocabularies/om-1.8/minute", 0, 1440, null, this)); 
		// States

		// Output
		addVariable(FWSimVariable.createSimVariable("PhotoPeriodFactor", "daily photoperiod factor", DATA_TYPE.DOUBLE,
				CONTENT_TYPE.out, "http://www.wurvoc.org/vocabularies/om-1.8/one", 0d, 1d, 0d, this));

		return iFieldMap;
	}

	/**
	 * @see net.simplace.sim.model.FWSimComponent#init()
	 */

	@Override
	protected void init()
	{
		//nothing to init
	}

	@Override
	protected void process()
	{
		PhotoPeriodFactor.setValue((iPhotoInhibition.getValue() - iDayLength.getValue() / 60) //hour in original
				/ (iPhotoInhibition.getValue() - iPhotoInsensitivity.getValue()), this);
		if (iPhotoInsensitivity.getValue() > iPhotoInhibition.getValue())
		{
			PhotoPeriodFactor.setValue((iDayLength.getValue() / 60 - iPhotoInhibition.getValue())
					/ (iPhotoInsensitivity.getValue() - iPhotoInhibition.getValue()), this);
		}
		if (PhotoPeriodFactor.getValue() < 0)
		{
			PhotoPeriodFactor.setValue(0., this);
		}
		if (PhotoPeriodFactor.getValue() > 1)
		{
			PhotoPeriodFactor.setValue(1., this);
		}
	}

	/**
	 * @see net.simplace.sim.model.FWSimComponent#clone(net.simplace.sim.util.FWSimVarMap)
	 */
	@Override
	protected FWSimComponent clone(FWSimVarMap aVarMap)
	{
		return new Photoperiod(iName, iFieldMap, iInputMap, iSimComponentElement, aVarMap, iOrderNumber);
	}

}
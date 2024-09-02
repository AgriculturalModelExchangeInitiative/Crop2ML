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
 * Lintul2PotentialGrowth.java
 *
 * Responsible developers: Gunther Krauss, Crop Science Group, Katzenburgweg 5, 53115 Bonn, Germany
 *                         Andreas Enders, Crop Science Group, Katzenburgweg 5, 53115 Bonn, Germany
 * Contact Information:    lapit@uni-bonn.de
 * More information on <http://www.simplace.net>
 */

package net.simplace.sim.components.experimental.amei;

import net.simplace.sim.model.FWSimComponent;
import net.simplace.sim.model.FWSimComponentGroup;
import net.simplace.sim.util.FWSimVarMap;
import net.simplace.sim.util.FWSimVariable;

import java.util.HashMap;
import java.util.LinkedList;

import org.jdom.Element;

/**
 * Grouped SimComponent to calculate potential growth, using LintulPhenology, Vernalisation, Partitioning and LintulBiomass. 
 * 
 * 
 * Class is defined by the File 
 * net.simplace.sim.components.models.lintul.PotentialGrowth.grp.xml
 * 
 * @see LintulPhenology
 * @see net.simplace.sim.components.crop.VernalisationAndPhotoresponse
 * @see Partitioning
 * @see LintulBiomass
 * @author Andreas Enders
 */
public final class PotentialGrowth extends FWSimComponentGroup
{

	/**
	 * Class for Name Constructor
	 */
	public PotentialGrowth()
	{
		super();
	}

	/**
	 * @param aName
	 * @param aFieldMap
	 * @param aInputMap
	 * @param aSimComponentElement
	 * @param aVarMap
	 * @param aOrderNumber
	 * @param aComponentList
	 * @param aInternalLinkMap
	 */
	private PotentialGrowth(String aName, HashMap<String, FWSimVariable<?>> aFieldMap, HashMap<String, String> aInputMap,
			Element aSimComponentElement, FWSimVarMap aVarMap, int aOrderNumber, LinkedList<FWSimComponent> aComponentList,
			HashMap<String, String> aInternalInputLinkMap, HashMap<String, String> aInternalOutputLinkMap)
	{
		super(aName, aFieldMap, aInputMap, aSimComponentElement, aVarMap, aOrderNumber, aComponentList, aInternalInputLinkMap, aInternalOutputLinkMap);
	}

	/**
	 * Definition of the Configuration_XML Name - by default is the Name of the Class
	 * 
	 */
	public static final String CONFIGURATION_XML = "PotentialGrowth";
	
	/* (non-Javadoc)
	 * @see net.simplace.sim.model.FWSimIntegratableComponentGroup#readComponentList()
	 */
	@Override
	protected void readConfiguration()
	{
		readConfiguration(CONFIGURATION_XML);
	}

	@Override
	protected FWSimComponent clone(FWSimVarMap aVarMap)
	{
		return new PotentialGrowth(getName(), iFieldMap, iInputMap, iSimComponentElement, aVarMap, iOrderNumber, iComponentList, iInternalInputLinkMap, iInternalOutputLinkMap);
	}
}

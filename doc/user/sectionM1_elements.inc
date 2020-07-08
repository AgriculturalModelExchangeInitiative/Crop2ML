.. _sectionM1:

.. marker_model_start

.. _specM_modelunit:

The ``ModelUnit`` element
-------------------------

The top-level element information item in a Crop2ML model unit infoset MUST be referred to as the :code:`ModelUnit` element.

.. code-block:: xml

		         <?xml version="1.0" encoding= ?> (1)
		         <!DOCTYPE ModelUnit PUBLIC " " "https://raw.githubusercontent.com/AgriculturalModelExchangeInitiative/crop2ml/master/ModelUnit.dtd"> (2)
		         <ModelUnit modelid=" " timestep=" " name=" " version=""> (3)
			         ....
		         </ModelUnit>

   | (1) XML header with version 1.0 and "utf-8" encoding

   | (2) External DTD declaration in the file

.. container:: issue-2-1-1

   1. A :code:`ModelUnit` element MUST contain a :code:`name`, a :code:`modelid`, a :code:`version` attributes. (3)

      * The value of the :code:`name` attribute must obey a number of constraints

.. marker_model_start2

      * The value of the :code:`modelid` attribute MUST ensure the provenance of the model. It is composed by an identifier of the platform and model package that provides the model and the name of the model unit separated by a dot. It is used to reference the model elsewhere and must be an unique identifier.
            
         | **Eg**. SQ.EnergyBalance.CropHeatFlux where SQ.EnergyBalance means that it is a strategy of Energy Balance component of SiriusQuality (SQ) model  and CropHeatFlux the name of the model unit
         
      * The value of the :code:`version` corresponds to a state of evolution of the model unit. It is defined by numbering.


   2. A :code:`ModelUnit` element MAY contain a :code:`timestep` attribute. Without this attribute, a time step of one day is considered. 


   3. A :code:`ModelUnit` element MUST contain one additional specific element child:
      
      #. A :code:`Description` element;

      #. An :code:`Inputs` element;

      #. An :code:`Outputs` element;

      #. A :code:`ParameterSets` element;

      #. A :code:`TestSets` element


   4. A :code:`ModelUnit` element MUST contain one or more :code:`Algorithm` element children


   5. A :code:`ModelUnit` element MAY contain one :code:`Initialization` element children


   6. A :code:`ModelUnit` element MAY contain one or more :code:`Function` element children


.. marker_model_end 

.. marker_description_start

.. _specM_description::

The ``Description`` element
---------------------------

This element gives the general information on the model and is composed of a set of character elements.

.. code-block:: xml
		
		          <ModelUnit modelid=" " timestep=" " name=" " version =" ">
			               <Description>
				                    <Title>title</Title>
				                    <Authors>authors</Authors>
				                    <Institution>institution</Institution>
				                    <Reference>reference</Reference>
				                    <Abstract><![CDATA[abstract]]></Abstract>
			               </Description>
			               ...
		          </ModelUnit>


..The :code:`Description` element information item is a child of :code:`ModelUnit` element.

   1. It MUST contain :code:`Title`, :code:`Authors`, :code:`Institution`, and :code:`Abstract` elements with text content.

      * The value of :code:`Title` element is the explicit name of the model and is based on no constraint. It gives the meaning of the process.
      
      * The value of :code:`Authors` element is the names of the authors of the implemented model in a specific platform.

      * The value of :code:`Institution` element is the name of the institution providing the component.

      * The value of :code:`Abstract` element is a brief description of the meaning of the process and provide the context of some experimental conditions used to calibrate the models.

   2. A:code:`Description` element MAY contain :code:`Reference` and :code:`Uri` element.
   
      * The value of :code:`Reference` provides the reference of scientific knowledge of the process. 
      
      * The value of :code:`URI` provides the link of the documentation of the process. 

.. marker_description_end

.. marker_inputs_start

.. _specM_inputs:

The ``Inputs`` element
----------------------

..The :code:`Inputs` element information item is a child of a :code:`ModelUnit` element.

.. code-block:: xml

      <ModelUnit modelid=" " timestep=" " name=" " version =" ">
         ...
         <Inputs>
            <Input name=" " description=" " variablecategory=" " datatype=" " min=" " max=" " default=" " unit=" " uri="" inputtype="variable"/>
            <Input name=" " description=" " parametercategory=" " datatype=" " min=" " max=" " default=" " unit=" " uri=" " inputtype="parameter"/>
            ...
         </Inputs>
         ...
      </ModelUnit>

.. container:: issue-2-3-0

   1. A :code:`Inputs` element MUST contain one or more  :code:`Input` element child.

   2. Each :code:`Input` element MUST have exactly one of each of the following attributes:

      (1) The :code:`name` attribute. The value of the name MUST be a valid Crop2ML name attribute.

          The value of a :code:`name` attribute MUST NOT be identical to the value of the :code:`name` attribute on any sibling :code:`Input` element 

      (2) The :code:`description` attribute

      (3) The :code:`datatype` attribute
         
          The value of a :datatype:`name`attribute is the data type of the value of input. A set of types is defined in Crop2ML as

      (4) An :code:`Unit` attribute MUST be associated to each variable or parameter. Crop2Ml uses by default the SI units or their composition.
   
            |  Eg: unit of Temperature is "degC",
            |  unit of thermal time is "degC*d", d represents "day".
            |  The symbole :code:`*`is used for the product of two units.
            |  The symbole :code:`/`is used for the division of two units.
            |  The symbole :code:`**`is used for the exponential of two units.
            |  An empty unit is expressed by : "dimensionless".          
.. marker_inputs_start2

.. container:: issue-2-3-01

      (5) The :code:`inputtype` attribute

          The value of an :code:`inputtype` attribute is either *variable* or *parameter*.  

   3.  Each :code:`Input` element MAY have exactly one of each of the following attributes:

      (1) The :code:`parametercategory` attribute SHOULD be defined when the value of :code:`inputtype` attribute is *parameter*
      
         The value of :code:`parametercategory` attribute MUST be : *constant*, *species*, *soil* or *genotypic* value.
         
         - Constant does not depend on the species, the soil and genotypic traits. Eg: Avogadro constant, latitude
         
         - soil is related to soil properties, eg : soil water retention
         
         - species is related to species properties: Response of vernalization rate to temperature
         
         - genotypic is related to the genotype

      (2) The :code:`variablecategory` attribute SHOULD be defined when the value of :code:`inputtype` attribute is *variable*
      
          The value of :code:`variablecategory` attribute MUST be a *state*, a *rate* or an "auxiliary". 
         
         - State variables characterize the behavior of the model. They result from the exchange processus in the system.
         
         - Rate variables characterizes the flux of these exchanges 
         
         - Auxiliary variables may be the intermediate product or other. 

      (3) Other attributes :code:`max`, :code:`min`, :code:`default` and :code:`uri`  MAY be specified.
      
          :code:`max` and :code:`min` are often used to implement tests of pre- and post-conditions.
   
          They state the domain of validity of the variables and parameters and constitute a criteria of model composability.


.. marker_inputs_end

.. marker_outputs_start

.. _specM_outputs:

The ``Outputs`` element
-----------------------

..The :code:`Outputs` element information item is a child of a :code:`ModelUnit` element.

.. code-block:: xml

      <ModelUnit modelid=" " timestep=" " name=" " version =" ">
         ...
         <Outputs>
            <Output name=" " description=" " datatype=" " min=" " max=" "  unit=" " uri=" "/>
            <Output name=" " description=" " datatype=" " min=" " max=" "  unit=" " uri=" "/>
            ...
         </Outputs>
         ...
      </ModelUnit>

.. container:: issue-2-3-3

   1. The :code:`Outputs` element MUST contain one or more  :code:`Output` element children.

   2. Each :code:`Output` element MUST have exactly one of each of the following attributes:

      (1) The :code:`name` attribute. 

      (2) The :code:`description` attribute

      (3) The :code:`datatype` attribute

      (4) The :code:`unit` attribute
          
   3.  Each :code:`Output` element MAY have exactly one of each of the following attributes:

      (1) The :code:`variablecategory` attribute 
      
      (2) The attributes :code:`uri`, :code:`max` and :code:`min`  MAY be specified.

| The meanings and constraints of different attributes are the same as those of :code:`Input` element.

.. marker_outputs_end


.. marker_algorithm_start

.. _specM_algorithm:

The ``Algorithm`` element
-------------------------

The *Algorithm* element implements the mathematical behaviour of the model componenent in a language.

..An :code:`Algorithm` element information item is a child of a :code:`ModelUnit` element.

.. code-block:: xml

      <ModelUnit modelid=" " timestep=" " name=" " version =" ">
         ...
         <Algorithm language =" " filename= "" platform=" ">
         ...
      </ModelUnit>

.. container:: issue-2-3-3

   1. An :code:`Algorithm` element MUST contain one of the :code:`language` attribute

   2. An :code:`Algorithm` element MAY contain one of each following attributes

      (1) The :code:`platform` attribute

      (2) The :code:`filename` attribute

   The model specifications defined in Inputs and outputs are sufficient to generate the signature of the function or a skeleton of class in a specific language.
   So, in this code, It'is just necessary to express a snippet of code to describe the dynamic of the model.

   This is an example of algorithm of Diffusion Limited Evaporation of Energy Balance component implemented in C++.
   In this case all variables used have already declared in the model specification.

.. code-block:: c++

   if (deficitOnTopLayers / 1000 <= 0)
	   diffusionLimitedEvaporation = 8.3 * 1000;
   else
   {
	   if (deficitOnTopLayers / 1000 < 25)
		   diffusionLimitedEvaporation = (2 * soilDiffusionConstant * soilDiffusionConstant / (deficitOnTopLayers / 1000)) * 1000;
	   else
		   diffusionLimitedEvaporation = 0;
   }

.. container:: issue-2-3-4

Six languages are supported : Fortran, Java, C++, C#, Python and R.
In order to avoid a re-implementation in this multiplicity of languages, Crop2Ml defines a minimal language CyML based on an intersection of the above languages.

You can visit a full documentation of CyML  `Here <pycrop2ml/readthedocs.fr>`_

*Algorithm implemented in CyML*

.. code-block:: cython

   if deficitOnTopLayers / 1000 <= 0:
      diffusionLimitedEvaporation = 8.3 * 1000
   else:
      if deficitOnTopLayers / 1000 < 25:
         diffusionLimitedEvaporation = (2 * soilDiffusionConstant * soilDiffusionConstant / (deficitOnTopLayers / 1000)) * 1000
      else:
         diffusionLimitedEvaporation = 0

.. marker_algorithm_end


.. marker_initialization_start

.. _specM_initialization:

The ``Initialization`` element
------------------------------

..An :code:`Initialization` element information item is a child of the :code:`ModelUnit` element.

Any model requires the initialization of its state variables.

.. code-block:: xml

      <ModelUnit modelid=" " timestep=" " name=" " version =" ">
         ...
         <Initialization name=" " language=" " filename=" "/>

         ...
      </ModelUnit>

.. container:: issue-2-3-5

   1. An :code:`Initialization` element MAY be specified if initial value is provided by a function or different from default value.e 

   2. An :code:`Initialization` element MUST contain the :code:`name` and :code:`language` attributes
    
      The language used to express initialization code must be the same as that used to encode model algorithm.

      The name of initialisation process is prefixed by *init.* followed by the name of the model.

   2. An :code:`Initialization` element MAY contain the :code:`filename` attribute to specify the path of the initialization file 

.. marker_initialization_end

.. marker_function_start

.. _specM_function:

The ``Function`` element
------------------------

..A :code:`Function` element information item is a child of the :code:`ModelUnit` element.

Function element is related to a mathematical function or service that can be called into model algorithm. 

.. code-block:: xml

      <ModelUnit modelid=" " timestep=" " name=" " version =" ">
         ...
         <Function name=" " language=" " filename=" "/>

         ...
      </ModelUnit>

.. container:: issue-2-3-5

   1. A :code:`Function` element MUST contain the following attriutes:

      (1) A :code:`name` attribute

      (1) A :code:`language` attribute

      (1) A :code:`type` attribute

         The value of A :code:`type` attribute MUST be either :code:`internal` or :code:`external`

         Crop2ML provides a library of well-known mathematical functions such as fibonacci, interpolation.

         If the function exists in this library, the value of code:`type` attribute is :code:`internal`.

   2. A :code:`Function` element MAY contain a :code:`description` attriute  

   A function can also be used for model adaptation such as temporal aggregation, unit conversion to link two models without changing.

   In this case, it won't be declared as a :code:`Function` but as a Crop2ML model with no physiology meaning.


.. marker_function_end

.. marker_parametersets_start

.. _specM_parametersets:

The ``ParameterSets`` element
-----------------------------

..A :code:`Parametersets` element information item is a child of a :code:`ModelUnit` element.

.. code-block:: xml

     <ModelUnit modelid=" " timestep=" " name=" " version =" ">
         ...
         <Parametersets>
            <Parameterset name="" description="" uri = ""/>
            <Parameterset name="" description="" >            
               <Param name="">value</Param>
               <Param name="">value</Param>
               ...
            </Parameterset>
            ...
         ...
      </ModelUnit>

.. container:: issue-2-3-5

   1. A :code:`Parametersets` element MUST contain one or more *Parameterset* element.
	
      A :code:`Parametersets` defines  different ways to parameterize the model. 
   
   2. Each :code:`Parameterset` element MUST have a :code:`name` and :code:`description` attributes 

      A :code:`Parameterset` defines a set of  values of parameters to parameterize the model. 
   
   3. Each :code:`Parameterset` element MAY have one or more :code:`Param` elements 

   4. Each :code:`Param` element MUST contain a :code:`name` attribute and is a text content.

      The value of the :code:`name` attribute MUST already be specified as a value of the :code:`name` attribute of an :code:`input` element.
	
.. marker_parametersets_end

.. marker_testsets_start

.. _specM_testsets:

The ``TestSets`` element
------------------------

..A :code:`Testsets` element information item is a child of a :code:`ModelUnit` element.
 
.. code-block:: xml

      <ModelUnit modelid=" " timestep=" " name=" " version =" ">
         ...
         <Testsets>
            <Testset name="" parameterset = "" description="" uri = ""/>
            <Testset name="" parameterset = "" description="" >            
               <Test name="">
                  <InputValue name="">value</InputValue>
                  ...
                  <OutputValue name="" precision ="">value</OutputValue>
                  ...
               </Test>                 
               ...
            </Testset>
            ...
        </Testsets>
        ...
      </ModelUnit>

.. container:: issue-2-3-6

   1. A :code:`Testsets` element MUST contain one or more *Testset* element.
	
      A :code:`TestSet` defines different model unit tests. 
   
   2. Each :code:`Testset` element MUST have a :code:`name`, a :code:`description` and :code:`parameterset` attributes

      The value of the :code:`parameterset` attribute MUST already be specified as a value of the :code:`name` attribute of an :code:`Parameterset` element.
   
   3. Each :code:`TestSet` element MUST contain one or more :code:`Test` elements 

      A :code:`Test` defines one unit test for the model checking.

   4. Each :code:`Test` element MUST contain a :code:`name` attribute and it MAY have :code:`description` and :code:`uri` attributes.

   5. Each :code:`Test` element MUST contain one or more :code:`InputValue` and :code:`OutputValue` elements.

      The checking consists of run the model with the values of :code:`InputValue` elements and compare the result with the values of :code:`OutputValue`elements.

..marker_testsets_end


.. marker_modelC_start

.. __specM_modelcomposite:

The ``ModelComposition`` element
--------------------------------

The top-level element information item in a Crop2ML model composite infoset MUST be referred to as the :code:`ModelComposition` element.

   1. A :code:`ModelComposition` element MUST contain a :code:`name`and a :code:`id` attributes.

   2. A :code:`ModelComposition` element MAY contain a :code:`version` and a :code:`timestep` attribute. 

   3. A :code:`ModelUnit` element MUST contain one of each additional specific element child:
      
      #. A :code:`Description` element;

      #. An :code:`Composition` element;

   4. A :code:`ModelUnit` element MAY contain one of each additional specific element child:

      #. An :code:`Inputs` element;

      #. An :code:`Outputs` element;

      #. An :code:`Initialization` element;

      #. An :code:`Algorithm` element;
      
      #. A :code:`ParameterSets` element;

      #. A :code:`TestSets` element

      The :code:`Inputs`and :code:`Outputs` elements are explicitely specified when model composite cannot be described as a graph of models.

      In this case an algorithm is provided.

.. marker_modelC_end 

.. marker_descriptionC_start

.. _specM_descriptionC::

The ``Description`` element
---------------------------

This element gives the general information on the model compoite and is composed of a set of character elements.


..The :code:`Description` element information item is a child of :code:`ModelComposition` element.

   It contains the same attributes as those of the :code:`Description` element of :code:`ModelUnit`.

.. marker_descriptionC_end

.. marker_inputsC_start

.. _specM_inputsC:

The ``Inputs`` element
----------------------

..The :code:`Inputs` element information item is a child of :code:`ModelComposition` element.

   It contains one or more :code:`Input` element which are the same attributes as those of the :code:`Input` element of :code:`ModelUnit`.

   #. Implicitly, all the previous states variables, the parameters and the exogeneous variables are the inputs of the model composite.

.. marker_inputsC_end


.. marker_outputsC_start

.. _specM_outputsC:

The ``Outputs`` element
-----------------------

..The :code:`Outputs` element information item is a child of :code:`ModelComposition` element.

   It contains one or more :code:`Output` element which are the same attributes as those of the :code:`Output` element of :code:`ModelUnit`.

   #. Implicitly, all the current state variables are the outputs of the model composite.

.. marker_outputsC_end


.. marker_algorithmC_start

.. _specM_algorithmC:

The ``Algorithm`` element
-------------------------

The :code:`Algorithm` element MAY implement the mathematical behaviour of the model composite in a language by calling other models.

..An :code:`Algorithm` element information item is a child of a :code:`ModelComposition` element.

  It contains the same attributes as those of the :code:`Algorithm` element of a :code:`ModelUnit`.

.. marker_algorithmC_end

.. marker_compositionC_start

.. _specM_compositionC:

The ``Composition`` element
---------------------------

The :code:`Composition` element makes the difference between a model unit and a model composite.

..A :code:`Composition` element information item is only a child of a :code:`ModelComposition` element.

   1. A :code:`Composition` element MUST contain one or more :code:`Model` element

      (1) The :code:`Model` element MUST have a :code:`name` , an :code:`id` and a :code:`filename` attributes

      (2) The :code:`Model` element MAY have a :code:`dixplayx` and a :code:`displayy` attributes representing the position of the model in the space.

   2. A :code:`Composition` element MUST contain one :code:`Links` element

      (1) The :code:`Links` element MAY be empty in the case where :code:`algorithm`element is specified.

      (2) If not, the :code:`Links` element SHOULD have one or more :code:`InputLinks`, :code:`InternalLinks` and :code:`OutputLinks` elements.

          #. An InputLink is a link between an input of the model composite and an input of a sub-model. 

          #. An OutputLink is a link between an output of the model composite and an output of a sub-model. 

          #. An InternalLink is a link between an output of a sub-model and an input of a sub-model. 

      (3) An :code:`InputLink` element MUST contain a :code:`target` and a :code:`source` attributes.

          #. The value of a :code:`target` attribute MUST be composed of the name of a sub-model and the name of an input of this sub-model separated by dot.

          #. The value of a :code:`source` attribute MUST be the name of an input of the model composite.

      (4) An :code:`OutputLink` element MUST also contain a :code:`target` and a :code:`source` attributes.

          #. The value of a :code:`source` attribute MUST be composed of the name of a sub-model and the name of an output of this sub-model separated by dot.

          #. The value of a :code:`target` attribute MUST be the name of an output of the model composite.

      (5) An :code:`InternalLink` element MUST also contain a :code:`target` and a :code:`source` attributes.

          #. The value of a :code:`source` attribute MUST be composed of the name of a sub-model and the name of an output of this sub-model separated by dot.

          #. The value of a :code:`target` attribute MUST be composed of the name of a sub-model and the name of an input of this sub-model separated by dot.


.. marker_compositionC_end


.. marker_initializationC_start

.. _specM_initializationC:

The ``Initialization`` element
------------------------------

The :code:`Initialization` element MAY implement the initialization of all state variables of the model composite.

..The :code:`Initialization` element information item is a child of a :code:`ModelComposition` element.

  It contains the same attributes as those of the :code:`Initialization` element of a :code:`ModelUnit`.


.. marker_initializationC_end


.. marker_parametersetsC_start

.. _specM_parametersetsC:

The ``ParameterSets`` element
-----------------------------

The :code:`ParameterSets` element allows to parameterize the model composite.

..The :code:`ParameterSets` element information item is a child of a :code:`ModelComposition` element.

  It contains the same elements type as those of the :code:`ParameterSets` element of a :code:`ModelUnit`.

	
.. marker_parametersetsC_end

.. marker_testsetsC_start

.. _specM_testsetsC:

The ``TestSets`` element
------------------------

The :code:`TestSets` element allows to make unit tests on the model composite.

..The :code:`TestSets` element information item is a child of a :code:`ModelComposition` element.

  It contains the same elements as those of the :code:`TestSets` element of a :code:`ModelUnit`.

	
..marker_testsetsC_end





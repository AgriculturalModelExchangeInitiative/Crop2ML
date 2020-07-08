============
**Usecases**
============
From some legacy model units of crop modeling platforms we show:
- their representation in crop2ml format (metadata and algorithm)
- transformation in fortran codes
- Test of model units

1- Priestly-Taylor potential evapotranspiration of DSSAT model

1-1- DSSAT fortran code

.. code-block:: fortran

    !  PETPT, Subroutine, J.T. Ritchie

    !  Calculates Priestly-Taylor potential evapotranspiration

    !-----------------------------------------------------------------------

    !  REVISION HISTORY

    !  ??/??/19?? JR  Written

    !  11/04/1993 NBP Modified

    !  10/17/1997 CHP Updated for modular format.

    !  09/01/1999 GH  Incorporated into CROPGRO

    !  07/24/2006 CHP Use MSALB instead of SALB (includes mulch and soil

    !                 water effects on albedo)

    !-----------------------------------------------------------------------

    !  Called by:   WATBAL

    !  Calls:       None

        SUBROUTINE PETPT( &
        
            MSALB, SRAD, TMAX, TMIN, XHLAI,&  ! inputs
        
            EO)                                ! outputs
            IMPLICIT NONE

    !       INPUT VARIABLES:

            REAL MSALB, SRAD, TMAX, TMIN, XHLAI

    !       OUTPUT VARIABLES:

            REAL EO

    !       LOCAL VARIABLES:

            REAL ALBEDO, EEQ, SLANG, TD


    !       Should use TAVG here -- we have it from WEATHER variable!

    !       SSJ 9/18/2006

    !       TD = TAVG

    !       JWJ 2/15/2007 - Can't use TAVG unless coefficients in EEQ

    !         equation are recalibrated.  Keep TD calc as it was

    !         developed.

        TD = 0.60*TMAX+0.40*TMIN
        IF (XHLAI .LE. 0.0) THEN
            ALBEDO = MSALB
        ELSE
            ALBEDO = 0.23-(0.23-MSALB)*EXP(-0.75*XHLAI)
        ENDIF
        SLANG = SRAD*23.923
        EEQ = SLANG*(2.04E-4-1.83E-4*ALBEDO)*(TD+29.0)
        EO = EEQ*1.1
        IF (TMAX .GT. 35.0) THEN
            EO = EEQ*((TMAX-35.0)*0.05+1.1)
        ELSE IF (TMAX .LT. 5.0) THEN
            EO = EEQ*0.01*EXP(0.18*(TMAX+20.0))
        ENDIF
        EO = MAX(EO,0.0001)
        RETURN
        END SUBROUTINE PETPT
    !     PETPT VARIABLES:

    ! ALBEDO  Reflectance of soil-crop surface (fraction)

    ! EEQ     Equilibrium evaporation (mm/d)

    ! EO      Potential evapotranspiration rate (mm/d)

    ! MSALB   Soil albedo with mulch and soil water effects (fraction)

    ! SLANG   Solar radiation

    ! SRAD    Solar radiation (MJ/m2-d)

    ! TD      Approximation of average daily temperature (ºC)

    ! TMAX    Maximum daily temperature (°C)

    ! TMIN    Minimum daily temperature (°C)

    ! XHLAI   Leaf area index (m2[leaf] / m2[ground])


1-2- Creation of Crop2ML package


1-3- Crop2ML representation of model unit

metadata
-------- 

.. code-block:: xml

    <?xml version="1.0" encoding="UTF-8"?>
    <!DOCTYPE ModelUnit PUBLIC " " "https://raw.githubusercontent.com/AgriculturalModelExchangeInitiative/crop2ml/master/ModelUnit.dtd">
    <ModelUnit name="PETPT" modelid="DSSAT.WheatModel.PETPT" timestep="1" version="1.0">
        <Description>
            <Title> PETPT, Subroutine, J.T. Ritchie,  Calculates Priestly-Taylor potential evapotranspiration </Title>
            <Authors>Kwang Soo Kim, luxkwang@snu.ac.kr </Authors>
            <Institution>Seoul National University, Seoul, Korea</Institution>
            <Reference>DSSAT 4.7; original fortran code was written by J.T. Ritchie </Reference>
            <Abstract> See Document at DSSAT 4.7 source code in PETPT.for </Abstract>
        </Description>
        <Inputs>
            <Input name="MSALB" description="Soil albedo with mulch and soil water effects (fraction)" datatype="DOUBLE" min="0" max="1" default="0.3" unit="" inputtype="variable" variablecategory="auxiliary" />
            <Input name="SRAD" description="Solar radiation" datatype="DOUBLE" min="0" max="100" default="100" unit="MJ m-2 d-1" inputtype="variable" variablecategory="auxiliary" />
            <Input name="TMAX" description="Maximum daily temperature" datatype="DOUBLE" min="-50" max="60" default="20" unit="°C" inputtype="variable" variablecategory="auxiliary" />
            <Input name="TMIN" description="Minimum daily temperature" datatype="DOUBLE" min="-50" max="60" default="10" unit="°C" inputtype="variable" variablecategory="auxiliary" />
            <Input name="XHLAI" description="Leaf area index" datatype="DOUBLE" min="0" max="10" default="3" unit="m2 m-2" inputtype="variable" variablecategory="state" />
        </Inputs>
        <Outputs>
            <Output name="EO" description="Potential evapotranspiration rate" datatype="DOUBLE" min="0.0" max="1.0" unit="mm d-1" variablecategory="rate" />
        </Outputs>
        <Algorithm language="cyml" platform="" filename="algo/pyx/petpt.pyx">
        </Algorithm>
        <Parametersets>
            <Parameterset name="Paramset1" description="some values in there" >
            </Parameterset>
        </Parametersets>
        <Testsets>
            <Testset name="Testset1" parameterset = "Paramset1" description="some values in there" >
                <Test name ="test1">
                    <InputValue name="MSALB">0.3</InputValue>
                    <InputValue name="SRAD">100</InputValue>
                    <InputValue name="TMAX">20</InputValue>
                    <InputValue name="TMIN">10</InputValue>
                    <InputValue name="XHLAI">3</InputValue>
                    <OutputValue name="EO" precision ="2">19.01</OutputValue>        	
                </Test>

            </Testset>
        
        </Testsets>

    </ModelUnit>

Algorithm
---------
.. code-block:: cython

    cdef float ALBEDO, EEQ, SLANG, TD
    TD = 0.60*TMAX + 0.40*TMIN
    if (XHLAI <= 0.0):
        ALBEDO = MSALB
    else:
        ALBEDO = 0.23 - (0.23-MSALB)*exp(-0.75*XHLAI)
    SLANG = SRAD * 23.923
    EEQ = SLANG * (2.04E-4 - 1.83E-4 * ALBEDO)*(TD+29.0)
    EO = EEQ*1.1
    if (TMAX > 35.0):
        EO = EEQ*((TMAX-35.0)*0.05+1.1)
    elif (TMAX < 5.0):
        EO = EEQ*0.01*exp(0.18*(TMAX+20.0))
    EO = max(EO, 0.0001)


1-4- Fortran code generated

.. code-block:: fortran

        SUBROUTINE petpt_(MSALB, &
            SRAD, &
            TMAX, &
            TMIN, &
            XHLAI, &
            EO)
            REAL, INTENT(OUT) :: EO
            REAL:: ALBEDO
            REAL:: EEQ
            REAL:: SLANG
            REAL:: TD
            REAL, INTENT(IN) :: MSALB
            REAL, INTENT(IN) :: SRAD
            REAL, INTENT(IN) :: TMAX
            REAL, INTENT(IN) :: TMIN
            REAL, INTENT(IN) :: XHLAI
            !- Description:
        !            - Model Name:  PETPT, Subroutine, J.T. Ritchie,  Calculates Priestly-Taylor potential evapotranspiration 
        !            - Author: Kwang Soo Kim, luxkwang@snu.ac.kr 
        !            - Reference: DSSAT 4.7; original fortran code was written by J.T. Ritchie 
        !            - Institution: Seoul National University, Seoul, Korea
        !            - Abstract:  See Document at DSSAT 4.7 source code in PETPT.for 
            !- inputs:
        !            - name: MSALB
        !                          - description : Soil albedo with mulch and soil water effects (fraction)
        !                          - datatype : DOUBLE
        !                          - min : 0
        !                          - max : 1
        !                          - default : 0.3
        !                          - unit : 
        !                          - inputtype : variable
        !                          - variablecategory : auxiliary
        !            - name: SRAD
        !                          - description : Solar radiation
        !                          - datatype : DOUBLE
        !                          - min : 0
        !                          - max : 100
        !                          - default : 100
        !                          - unit : MJ m-2 d-1
        !                          - inputtype : variable
        !                          - variablecategory : auxiliary
        !            - name: TMAX
        !                          - description : Maximum daily temperature
        !                          - datatype : DOUBLE
        !                          - min : -50
        !                          - max : 60
        !                          - default : 20
        !                          - unit : °C
        !                          - inputtype : variable
        !                          - variablecategory : auxiliary
        !            - name: TMIN
        !                          - description : Minimum daily temperature
        !                          - datatype : DOUBLE
        !                          - min : -50
        !                          - max : 60
        !                          - default : 10
        !                          - unit : °C
        !                          - inputtype : variable
        !                          - variablecategory : auxiliary
        !            - name: XHLAI
        !                          - description : Leaf area index
        !                          - datatype : DOUBLE
        !                          - min : 0
        !                          - max : 10
        !                          - default : 3
        !                          - unit : m2 m-2
        !                          - inputtype : variable
        !                          - variablecategory : state
            !- outputs:
        !            - name: EO
        !                          - description : Potential evapotranspiration rate
        !                          - datatype : DOUBLE
        !                          - min : 0.0
        !                          - max : 1.0
        !                          - unit : mm d-1
        !                          - variablecategory : rate
            TD = 0.60 * TMAX + 0.40 * TMIN
            IF(XHLAI .LE. 0.0) THEN
                ALBEDO = MSALB
            ELSE
                ALBEDO = 0.23 - (0.23 - MSALB) * EXP(-0.75 * XHLAI)
            END IF
            SLANG = SRAD * 23.923
            EEQ = SLANG * (2.04E-4 - 1.83E-4 * ALBEDO) * (TD + 29.0)
            EO = EEQ * 1.1
            IF(TMAX .GT. 35.0) THEN
                EO = EEQ * ((TMAX - 35.0) * 0.05 + 1.1)
            ELSE IF ( TMAX .LT. 5.0) THEN
                EO = EEQ * 0.01 * EXP(0.18 * (TMAX + 20.0))
            END IF
            EO = MAX(EO, 0.0001)
        END SUBROUTINE petpt_


1-5- Test in jupyterlab

.. nbinput::ipython3
    :execution-count: 5

    Program test_test1_PETPT
        REAL:: MSALB

        REAL:: SRAD

        REAL:: TMAX

        REAL:: TMIN

        REAL:: XHLAI

        REAL:: EO

        MSALB = 0.3

        SRAD = 100

        TMAX = 20

        TMIN = 10

        XHLAI = 3

        call petpt_(MSALB,SRAD,TMAX,TMIN,XHLAI,EO)
        print *,EO
    CONTAINS
        SUBROUTINE petpt_(MSALB, &
            SRAD, &
            TMAX, &
            TMIN, &
            XHLAI, &
            EO)
            REAL, INTENT(OUT) :: EO
            REAL:: ALBEDO
            REAL:: EEQ
            REAL:: SLANG
            REAL:: TD
            REAL, INTENT(IN) :: MSALB
            REAL, INTENT(IN) :: SRAD
            REAL, INTENT(IN) :: TMAX
            REAL, INTENT(IN) :: TMIN
            REAL, INTENT(IN) :: XHLAI
            !- Description:
        !            - Model Name:  PETPT, Subroutine, J.T. Ritchie,  Calculates Priestly-Taylor potential evapotranspiration 
        !            - Author: Kwang Soo Kim, luxkwang@snu.ac.kr 
        !            - Reference: DSSAT 4.7; original fortran code was written by J.T. Ritchie 
        !            - Institution: Seoul National University, Seoul, Korea
        !            - Abstract:  See Document at DSSAT 4.7 source code in PETPT.for 
            !- inputs:
        !            - name: MSALB
        !                          - description : Soil albedo with mulch and soil water effects (fraction)
        !                          - datatype : DOUBLE
        !                          - min : 0
        !                          - max : 1
        !                          - default : 0.3
        !                          - unit : 
        !                          - inputtype : variable
        !                          - variablecategory : auxiliary
        !            - name: SRAD
        !                          - description : Solar radiation
        !                          - datatype : DOUBLE
        !                          - min : 0
        !                          - max : 100
        !                          - default : 100
        !                          - unit : MJ m-2 d-1
        !                          - inputtype : variable
        !                          - variablecategory : auxiliary
        !            - name: TMAX
        !                          - description : Maximum daily temperature
        !                          - datatype : DOUBLE
        !                          - min : -50
        !                          - max : 60
        !                          - default : 20
        !                          - unit : °C
        !                          - inputtype : variable
        !                          - variablecategory : auxiliary
        !            - name: TMIN
        !                          - description : Minimum daily temperature
        !                          - datatype : DOUBLE
        !                          - min : -50
        !                          - max : 60
        !                          - default : 10
        !                          - unit : °C
        !                          - inputtype : variable
        !                          - variablecategory : auxiliary
        !            - name: XHLAI
        !                          - description : Leaf area index
        !                          - datatype : DOUBLE
        !                          - min : 0
        !                          - max : 10
        !                          - default : 3
        !                          - unit : m2 m-2
        !                          - inputtype : variable
        !                          - variablecategory : state
            !- outputs:
        !            - name: EO
        !                          - description : Potential evapotranspiration rate
        !                          - datatype : DOUBLE
        !                          - min : 0.0
        !                          - max : 1.0
        !                          - unit : mm d-1
        !                          - variablecategory : rate
            TD = 0.60 * TMAX + 0.40 * TMIN
            IF(XHLAI .LE. 0.0) THEN
                ALBEDO = MSALB
            ELSE
                ALBEDO = 0.23 - (0.23 - MSALB) * EXP(-0.75 * XHLAI)
            END IF
            SLANG = SRAD * 23.923
            EEQ = SLANG * (2.04E-4 - 1.83E-4 * ALBEDO) * (TD + 29.0)
            EO = EEQ * 1.1
            IF(TMAX .GT. 35.0) THEN
                EO = EEQ * ((TMAX - 35.0) * 0.05 + 1.1)
            ELSE IF ( TMAX .LT. 5.0) THEN
                EO = EEQ * 0.01 * EXP(0.18 * (TMAX + 20.0))
            END IF
            EO = MAX(EO, 0.0001)
        END SUBROUTINE petpt_
    END Program

.. nboutput::
    :execution-count: 5

    19.0133114

.. _inform4_2:

.. container:: toggle

  .. container:: header

    See **Data Representation Format in Crop2ML**

  .. container:: infospec

    * The required *datatype* attribute is the type of input value specified in *default* (the default value in the input), *min* (the minimum value in the input) and *max* (the maximum value in the input). A set of
      types is defined in Crop2ML as:

        - STRING to manipulate string variables, eg: phenology development stade "Anthesis"
        
        - DATE : A convention used to express date is dd/mm/yyyy where dd is the day, mm the month and yyyy the year. eg: "15/12/2007"
        
        - DOUBLE: a real number with a decimal eg (15.0 not 15)
        
        - INT: an integer number
        
        - BOOLEAN: A boolean variable takes one of these two values "TRUE" or "FALSE"
      
      Array variables have fixed length which values are between "[" and "]" brackets:
        
        - STRINGARRAY: an array of string variables.
        
        - DOUBLEARRAY: an array of real variables
        
        - INTARRAY: an array of integer variables
        
        - DATEARRAY: an array of dates variables 
        
        - BOOLEANARRAY: an array of boolean variables
      
      List variables have variable length which values are between "[" and "]" brackets:
        
        - STRINGLIST: a list of string variables
        
        - INTLIST: a list of integer variables
        
        - DOUBLELIST: a list of real variables
        
        - BOOLEANLIST: a list of boolean variable 
        
        - DATELIST: a list of date variable
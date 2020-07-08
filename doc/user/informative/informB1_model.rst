.. _inform4_1:

.. container:: toggle

  .. container:: header

     See the constraints

  .. container:: infospec

    The :code:`ModelUnit` is the highest level in the Crop2ML model file, not including the opening :code:`xml` tags.

    A Crop2ML name attribute must obey some constraints:

        - it starts by a letter

        - It is a single word

        - Only alphanumeric characters and underscore characters

               eg: name: CropHeatFlux or crop_heat_flux (valid names),

               name: crop-heat-flux or 1cropheatflux (not valid names)

         - It MUST NOT be a keyword of any programming language

    These are valid Crop2ML name attributes:

      - :code:`myName` is valid (both cases are permitted),

      - :code:`my_other_valid_name` is valid (underscores are permitted), and
      
      - :code:`this1too` is valid (numerals 0-9 are permitted).

    These are not valid Crop2ML name attributes:

      - :code:`my invalid name` is not valid (spaces are not permitted),
      
      - :code:`thisIsInvalidToo!` is not valid (special characters other than the underscore :code:`_` are not permitted),
      
      - :code:`1amNotValidEither` is not valid (must not begin with a number), and
      
      - :code:`" "` empty string is not valid (a name must be present).
      
      - :code:`lambda` is not valid (language keyword not permitted).

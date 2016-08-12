# CsbScaffold

This project provides you with the prototypical folder structure for an organized data analysis project.

Start by renaming the projectName folder to the partcular project name.
In order to include different projects dublicate the folder projectName in place 
CsbScaffold comprises of two folders:  

.env:
* .aux        | Place for auxiliary functions
* .lib        | .dlls that are not managed by the paket manager
* .paket      | library files managed by the paket manager


projectName:
* data         : Place to store the raw measurment data
* documents    : Documentation  
* material     : Additional information (e.g. related publication)
* presentation : Compiled information to present your work 
* results      : Results that are produces during data processing
* scripts      : (e.g. .fsx files)

_______________
to install type:
cd .env

.\\.paket\paket.exe

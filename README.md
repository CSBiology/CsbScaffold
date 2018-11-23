# CsbScaffold

CsbScaffold is meant to simplify F# data analysis using the CSBiology libraries.

It is easy to setup and provides you with a prototypical folder structure for an organized data analysis project. 

_______________
### Setup
Prerequisites: 
* git cli has to be installed
* fake cli has to be installed

After downloading just run the **init.cmd**.

This will download the packages and create a new local branch "projects" (if you have cloned the library). You should use this branch for scripting and version controlling your projects. Use the "master" branch only to make contributions to the Repository.

_______________
### Usage
Start by renaming the projectName folder to the partcular project name.
In order to include different projects duplicate the folder projectName in place.

CsbScaffold comprises of two folders:  

.env:
* .aux        | Place for auxiliary functions
* .lib        | .dlls that are not managed by the paket manager
* .paket      | library files managed by the paket manager


projectName:
* data         : Place to store the raw measurement data
* documents    : Documentation  
* material     : Additional information (e.g. related publication)
* presentation : Compiled information to present your work 
* results      : Results that are produced during data processing
* scripts      : (e.g. .fsx files)
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
* data         : Place to store the raw measurement data
* documents    : Documentation  
* material     : Additional information (e.g. related publication)
* presentation : Compiled information to present your work 
* results      : Results that are produced during data processing
* scripts      : (e.g. .fsx files)

_______________
### Setup

To install run the **init.cmd**

The installation will create a new local branch "projects". You should use this branch for scripting and version controlling your projects. Use the "master" branch only to make contributions to the Repository.
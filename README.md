# CsbScaffold

CsbScaffold is meant to simplify F# data analysis using the CSBiology libraries.

It is easy to setup and provides you with a prototypical folder structure for an organized data analysis project. 

_______________
### Setup
Prerequisites: 
* git cli has to be installed: https://git-scm.com/download/win

# How to set up the environment for our tasks

This simple guide will set you up for the tasks we have prepared for you. After finishing it, you will have installed Visual studio code, .NetCore SDK & .Net Framework, and the F# runtime environment.

</br>

## Step 1 - Install .NetCore SDK

[Click here](https://dotnet.microsoft.com/download), and download both .Net Core SDK and .Net Framework 4.7.2 Dev Pack:

![Step1](https://i.imgur.com/0ibgjnw.png)

Run both of the downloaded files. This will prompt you through the respective installation.

_Note: It is possible that you have one ore both of these already installed on your pc._

</br>

## Step 2 - Install Visual Studio Build Tools 2017 

[Click here](https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio-2017), and download the VS 2017 Build Tools installer:

![Step2.png](https://i.imgur.com/tNyR7KU.png)

Run both of the downloaded files. This will prompt you through the respective installation.

</br>

## Step 3 - Install Visual Studio Code

[Click here](https://code.visualstudio.com/) and download and install Visual Studio Code.

![Step3.png](https://i.imgur.com/0FrG7ZM.png)

## Step 4 - Install the Ionide-fsharp extension for VS Code

After you installed visual studio, open it. your window should look like this:

![Step4.1.png](https://i.imgur.com/QvRnzwJ.png)

click on the extensions symbol to the left:

![Step4.2.png](https://i.imgur.com/XvzFtY6.png)

type 'Ionide' into the searchbar and install it

![Step4.3.png](https://i.imgur.com/0thIpnA.png)

## Step 5 - Install fake cli:

To install FAKE globally, run:

`dotnet tool install fake-cli -g`

To install FAKE into your_tool_path, run:

`dotnet tool install fake-cli --tool-path yourtoolpath`

After installing the prerequisites above, open a console, navigate to the root folder and run the **init.cmd**.

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
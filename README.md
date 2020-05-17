# CSB Scaffold v2 (name is WIP)

this branch contains the preliminary work for the next iteration of our scripting environment for 
bioinformatics with F#/BioFSharp.

The focus of v2 is on the following (WIP):

### Core Features:

##### Distribute as `dotnet new` template 
- WIP
- Discussion @ #3

##### Offer different opinionated package groups targeting different workflows

 - Common for all packages:
   - WIP

 - `CSB` (default, name could change to simply `default`): 
   - BioFSharp (all packages)
   - FSharpAux/FSharpAux.IO
   - FSharp.Stats

 - `DataScience`
   - FSharp.Plotly
   - Deedle
   - R Provider
   - More type providers?
   - WIP

##### Take advantage of the improved F#/.NET tooling
 - use local tools
 - automate as much as possible with the power of FAKE
 - use packet to generate load scripts
 - Maybe use Fornax/FSharp.Formatting to generate reports?
 - WIP



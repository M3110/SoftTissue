# IC Soft Tissue App
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

# TODO
 ALTERAR NOME DAS OPERAÇÕES, PORQUE ESTÃO MUITO GRANDES E RESOLVER AS CONCORRÊNCIAS PELO NAMESPACE, EX:
 - LINEARMODEL.CALCULATERESULTS
 - QUASILINEARMODEL.CALCULATERESULTS

## UNRELEASED
### Changed
 - Improved generalizations at Viscoelastic Models.
 - Operations that calculate strain and stress for viscoelastic models to inherit from operation CalculateResults.
 - Operation AnalyzeResults to also consider when the stress increase.
 - Changed SkipPoints controller to use FromQuery instead of FromBody.
 - Renamed classes that contain LinearViscoelasticity or QuasiLinearViscoelasticity in name from Linear and QuasiLinear.
### Added
 - Methods CalculateResultsAsync and CalculateInitialConditionsAsync to be generic for viscoelastic models.
 - Enum StressDirection that indicates if the stress is increasing or decreasing.
 - Property StressDirection in AnalyzeResultsRequest.
 - Generic contract OperationResponseBase that do not contain ResponseData.
 - Extension method for OperationResponseBase.
### Fixed
 - Improved operation CalculateQuasiLinearViscoelasticityStress based on final project necessities.
 - Improved QuasiLinearViscoelasticityModel based on bibliographies.
### Removed
 - Response contracts for operations that inherit operation CalculateResults.

## [2.1.2] - 2021-04-26
### Fixed
 - Operation AnalyzeResults to not write the first and second derivative.

## [2.1.1] - 2021-04-26
### Fixed
 - File .gitignore to ignore folder bibliographies.
### Removed
 - Folder bibliographies.

## [2.1.0] - 2021-04-25
### Fixed
 - Operation AnalyzeAndExtrapolateResults that was writing invalid results in the solution file and moved main parts that analyze the results to a new operation.
### Added
 - Operation AnalyzeResults.

## [2.0.1] - 2021-03-31
### Fixed
 - Data Contracts to not use JsonConstructor that was crashing when using Swagger.

## [2.0.0] - 2021-03-17
### Added
 - Data contracts to CalculateResult operation.
 - Operation SkipPoints.
 - Operation AnalyzeAndExtrapolateResults.
 - Operation GenerateDomain.
 - Operation CalculateConvergenceTime.
 - Operation to calculate stress for experimental models.
 - Enum ViscoelasticConsideration with each viscoelastic consideration.
 - Nuget package CsvHelper.
 - Nuget package Microsoft.AspNetCore.Mvc.NewtonsoftJson.
 - Nuget package Newtonsoft.Json.
### Changed
 - Concrete classes in the DataContracts project to be sealed and 'set' method to be private.
 - Generic classes in the DataContracts project to be abstract and 'set' method to be protected.
 - Data contracts of operations that inheriting from CalculteResult inherite from CalculateResult data contract.
 - Quasi-linear operations considering multiple relaxations.
 - RelavativePrecision from 5e-3 to 1e-3 and Precision from 1e-10 to 1e-6.
 - Moved sensitivity analysis operations to a new folder.
 - CalculateStress operations for Quasi-Linear Viscoelastic model to process considering ramp time or not, or considering Reduced Relaxation Function or Simplified Reduced Relaxation Function.

## [1.1.0] - 2020-11-01
### Added
 - Operation CalculateResult and CalculateResultSensitivityAnalysis with the generical methods and properties shared between operations.
 - Method to calculate the initial conditions for Quasi-Linear Viscoelasticity Model.
 - Operation to generate the domain for Fung Model parameters.

## [1.0.1] - 2020-09-30
### Fixed
 - Fixed all application to prepare it to be easier to add a new viscoelastic model or a new operation.

## [1.0.0] - 2020-09-27
### Added
 - First version of the program.
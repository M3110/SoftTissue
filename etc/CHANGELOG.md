# IC Soft Tissue App
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## UNRELEASED
### Added
- Operation AnalyzeAndPredictResults.
- Operation GenerateDomain.
- Operation CalculateConvergenceTime.
- Operation to calculate stress for experimental models.
- Enum ViscoelasticConsideration with each viscoelastic consideration.
### Changed
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
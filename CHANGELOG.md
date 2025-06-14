# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## 1.0.3 - 2025-06-12 

### Added

* Write content to a temporary file first in MoBroPersistenceManager
* Interfaces for categorized and labeled items

### Changed

* Use concurrent dictionary in MoBroService
* Increased the max length of Ids to 256 characters

### Fixed

* Moved validation attributes from interface to implementing classes
* Some missing and incorrect ID validation attributes

## 1.0.2 - 2025-06-01

### Added

* Core metric type and value type for 'boolean' metrics
* Additional fields for the json configuration: Homepage, Repository, Tags

### Changed

* Removed core category 'Audio'
* Make services public

### Fixed

* Metric stage was skipped for action builder
* Corrected default value for 'executionMode' in json configuration to 'AdminBackground'

## 1.0.1 - 2025-03-17

### Fixed

* Downgraded Microsoft.Extensions.Logging and Serilog.Extensions.Logging to v8.0.0 to be compatible with the MoBro data
  service

## 1.0.0 - 2025-01-12

Official 1.0 release  
No functional changes compared to latest 0.X version

## 0.6.0 - 2024-11-21

### Added

* Added name of invalid field to MoBroItemValidationException
* Added execution mode to json plugin configuration

## 0.5.0 - 2024-10-18

### Added

* Added optional metric to action representing the value adjusted or influenced by that action

### Changed

* Moved 'Resume' and 'Pause' functionality from plugin interface to scheduler
* Updated dependencies
* Removed possibility for actions to return results

## 0.4.0 - 2024-09-03

### Added

* Added function to IMoBroService to get all currently registered items
* Added additional guard clauses
* Added IMoBroPersistenceManager service
* Added IMoBroFileManager
* Additional validation on item registration
* Added Shutdown function to IMoBroPlugin

### Changed

* Expose function publicly to set additional detail in PluginException
* Updated to .NET 8

## 0.3.0 - 2023-07-27

### Added

* Added function to IMoBroService to get the current value of a metric

### Changed

* Seal all model classes
* Removed interfaces for certain model classes
* Removed IDisposable interface from IMoBroPlugin
* Moved Category and Group models to separate folder

## 0.2.0 - 2023-07-03

### Added

* Allow to use DateTimeOffset for MetricValue
* Validation for metric values and plugin items when running locally
* Mapping function from CoreMetricType to the associated MetricValueType
* New 'Currency' MetricValueType
* New core metric types for currencies

### Changed

* Adjusted default logging template for console
* Split project up into separate SDK and SDK.Core projects
* Moved unit test previously located in the MoBro service repository

### Fixed

* Corrected property name for default value of setting fields

## 0.1.9 - 2023-05-14

### Added

* Json schema for 'mobro_plugin_config.json'
* NuGet package icon
* Plugin wrapper to locally test and debug plugins without the need for the actual MoBro service

## 0.1.8 - 2023-04-10

### Fixed

* Fixed order of newly added 'Audio' core category

## 0.1.7 - 2023-03-31

### Added

* Added new 'Audio' core category
* Added new 'Angle' core metric type

## 0.1.6 - 2023-03-08

### Changed

* Renamed key of numeric settings field from 'numeric' to 'number'

## 0.1.5 - 2023-01-23

### Added

* Support for basic actions
* Pause and resume functions in the MoBroPlugin interface
* Allow to pass additional error details in plugin exceptions
* Option to actively notify service of an unrecoverable error
* Scheduler for recurring tasks

### Changed

* Switched from polling metrics to the plugin pushing value updates to the service
* Metric value as struct to avoid unnecessary allocations
* Renamed core category 'sound' to 'media'
* Removed core metric type 'miscellaneous'
* Adjustments to the MoBroPlugin and MoBroService interfaces
* Adjustments to the MoBro item builders
* Upgraded to .NET 7

## 0.0.1 - 2022-10-17

First test release

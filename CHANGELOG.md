# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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

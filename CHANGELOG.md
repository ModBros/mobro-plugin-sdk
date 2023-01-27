# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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

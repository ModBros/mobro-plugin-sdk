# mobro-plugin-sdk

[![Nuget](https://img.shields.io/nuget/v/MoBro.Plugin.SDK?style=flat-square)](https://www.nuget.org/packages/MoBro.Plugin.SDK)
![GitHub](https://img.shields.io/github/license/ModBros/mobro-plugin-sdk)
[![MoBro](https://img.shields.io/badge/-MoBro-red.svg)](https://mobro.app)
[![Discord](https://img.shields.io/discord/620204412706750466.svg?color=7389D8&labelColor=6A7EC2&logo=discord&logoColor=ffffff&style=flat-square)](https://discord.com/invite/DSNX4ds)

This is the official repository for the MoBro Plugin SDK.  
The SDK is used to develop plugins for [MoBro](https://mobro.app).

## Developer documentation

Detailed developer documentation on how to use the SDK can be found
on [developer.mobro.app](https://developer.mobro.app).

Also, feel free to visit us on our [Discord](https://discord.com/invite/DSNX4ds) server for any questions or in case you
run into any issues.

## Packages

This repository is split into two projects, resulting in two separate packages:

### MoBro.Plugin.SDK

This is the primary package you should include when creating a plugin for MoBro. It includes the `MoBro.Plugin.SDK.Core`
package and additional developer tools for running and testing a plugin locally without needing to install it in the
MoBro application.

### MoBro.Plugin.SDK.Core

This is the 'core' part of the MoBro plugin SDK and contains only the necessary interfaces and types required for a
plugin to be loaded. This package has no additional dependencies and is included in and used by MoBro to load plugins.

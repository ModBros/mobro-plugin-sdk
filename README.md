# mobro-plugin-sdk

[![Nuget](https://img.shields.io/nuget/v/MoBro.Plugin.SDK?style=flat-square)](https://www.nuget.org/packages/MoBro.Plugin.SDK)
[![Nuget](https://img.shields.io/nuget/dt/MoBro.Plugin.SDK?style=flat-square)](https://www.nuget.org/packages/MoBro.Plugin.SDK)
![GitHub](https://img.shields.io/github/license/ModBros/mobro-plugin-sdk)
[![Discord](https://img.shields.io/discord/620204412706750466.svg?color=7389D8&labelColor=6A7EC2&logo=discord&logoColor=ffffff&style=flat-square)](https://discord.com/invite/DSNX4ds)

This is the official repository of the MoBro Plugin SDK.  
The SDK is used to develop plugins for [MoBro](https://mobro.app) - an application for universal data visualization.

**Caution: The SDK is still in development and subject to potential breaking changes.**

## Developer documentation

Detailed developer documentation on how to use the SDK can be found
on [developer.mobro.app](https://developer.mobro.app)

## Packages

This repository is split into two project that result in two separate packages.

### MoBro.Plugin.SDK

This is the main and only package that you should include if you're creating a plugin for MoBro.  
This package includes the MoBro.Plugin.SDK.Core package as well as additional developer tools to run and test a plugin
locally without having to install the plugin to the actual MoBro application.

### MoBro.Plugin.SDK.Core

This is the 'core' part of the MoBro plugin SDK and only contains the necessary interfaces and types that are required
for a plugin to be loaded.  
This package lacks any additional dependencies and is included in and used by MoBro to load the plugins.

----

Feel free to visit us on our [Discord](https://discord.com/invite/DSNX4ds) or [Forum](https://www.mod-bros.com/en/forum)
for any questions or in case you run into any issues.

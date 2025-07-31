# DllToDependency

## Overview

DllToDependency is a tool designed to simplify the process of using DLLs as dependencies in .NET applications, particularly when developing in Visual Studio Code on Linux. Unlike Visual Studio, which has built-in support for managing DLL dependencies, Visual Studio Code requires a custom solution for Linux environments. This tool allows you to convert DLLs into a readable format for inclusion in `.csproj` files as dependencies and optionally copy the DLLs to a specified path.

## Features

- **Convert DLLs to Dependencies**: Transform provided DLL files into a format that can be referenced in a `.csproj` file.
- **Custom Path Copying**: Copy the DLL files to a user-specified directory for easy integration into your project.
- **Linux-Friendly**: Specifically designed for .NET developers working in Visual Studio Code on Linux, where native DLL dependency management is limited.

## Usage

1. **Provide DLL Files**: Input the DLL files you want to use as dependencies.
2. **Generate Dependency References**: The tool processes the DLLs and generates the necessary entries for your `.csproj` file.
3. **Optional File Copying**: Specify a target path to copy the DLLs, ensuring they are correctly placed for your project.
4. **Integrate into Project**: Add the generated references to your `.csproj` file to use the DLLs as dependencies.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/JRVR11/DllToDependency.git
   ```
2. Navigate to the project directory:
   ```bash
   cd DllToDependency
   ```
3. Follow the setup instructions specific to your environment (detailed instructions to be added based on the tool's implementation).

## Building

This is only built for linux, to build for your OS, do the second step of the Installation and run:
```bash
dotnet build
```
And to run/use it:
```bash
dotnet run
```
or by executing the file it generates after building.

## Why This Tool?

While Visual Studio provides robust support for managing DLL dependencies, developers using Visual Studio Code on Linux face challenges due to the lack of native support. DllToDependency fills this gap by offering a simple, effective way to incorporate DLLs into .NET projects, making it an essential tool for Linux-based .NET development.

## Contributing

Contributions are welcome! If you have suggestions, bug reports, or want to add features, please:
1. Fork the repository.
2. Create a new branch for your feature or fix.
3. Submit a pull request with a clear description of your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For questions or support, please open an issue on the [GitHub Issues page](https://github.com/JRVR11/DllToDependency/issues).

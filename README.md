
# FileClipboard

## About
FileClipboard is a simple CLI tool that copies one or more file paths to your clipboard as a file drop list. This makes it easy to share files in applications that support file drops, such as Discord or Windows Explorer.

## Features
- **Copy Files to Clipboard:** Quickly copies the full file paths of one or more files.
- **Multi-file Support:** Accepts multiple file paths as command-line arguments.
- **Interactive Mode:** Provides an interactive menu when no arguments are given, including an option to add the tool's directory to your PATH.
- **Built-in Help:** Use `--help` or `-h` to display usage instructions.

## Requirements
- Windows
- [.NET 9.0 or later](https://dotnet.microsoft.com/download) (with Windows Forms support enabled)

## Usage
1. **Interactive Mode:**  
   Run the program without any arguments:
   ```bash
   FileClipboard.exe
   ```
   This will display an interactive menu with additional options.

2. **Copy Files to Clipboard:**  
   Run the program with one or more file paths:
   ```bash
   FileClipboard.exe file1.mp4 file2.txt
   ```
   The valid file paths will be copied to your clipboard as a file drop list.


## License
This project is dedicated to the public domain under the terms of the [CC0 1.0 Universal (CC0 1.0) Public Domain Dedication](https://creativecommons.org/publicdomain/zero/1.0/).

## Contributing
Feel free to contribute by submitting pull requests or reporting issues!

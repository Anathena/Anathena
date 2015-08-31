# Fasm.NET

A managed wrapper to use FASM compiler from .NET applications.

This library is written in C++/CLI and embeds FASM compiler as a linkable *Microsoft COFF* object.  
As FASM compiler is built in 32-bit, the managed assembly can only be used within a 32-bit development.

The branches match with the current versions of FASM compiler as published on the official website.  
- The branch `master` contains the latest tested version: **1.70.03 (2012-06-29)**  
- The branch `development` contains the latest development snapshot version: **None**

## Features

Fasm.NET offers the following features:

- Assemble mnemonics on the fly
- Assemble a file
- Assemble multiple files
- Create a instance of the class to assemble your mnemonics code with ease
- Throw managed and detailed exceptions when compilation errors occur
- Customize the memory size and the number of pass
- Get the version of the FASM compiler

## Examples

Here are some examples written in C# showing how the library can be used.

### Assemble 32-bit inline mnemonics

```csharp
var asm = FasmNet.Assemble("use32\nretn");
```

The variable `asm` is an array of bytes containing the assembly code.

### Assemble 64-bit mnemonics using an array

```csharp
string[] mnemonics =
	{
		"use64",
		"mov rax, 1",
		"retn"
	};

var asm = FasmNet.Assemble(mnemonics, 1000, 10);
```

Here is used an overloaded definition of the method to specify the memory size (1000 bytes) and the number of pass (10).  
The variable `asm` is an array of bytes containing the assembly code.

### Assemble two files

```csharp
var files = new[] {"constants.inc", "main.asm"};
var asm = FasmNet.AssembleFiles(files);
```

The variable `files` is an array containing the files to assemble and `asm` is an array of bytes containing the assembly code.

### Handle managed exceptions

```csharp
var oops = "wrong";

try
{
	FasmNet.Assemble(oops);
}
catch (FasmAssemblerException ex)
{
	Debug.WriteLine("Error definition: {0}; Error code: {1}; Error line: {2}; Error offset: {3}; Mnemonics: {4}",
		ex.ErrorCode, (int)ex.ErrorCode, ex.ErrorLine, ex.ErrorOffset, ex.Mnemonics);
}
```

In this example, the assembling operation fails and an error is thrown.  
The variable `ex` contains what, where and why the assembling operation couldn't terminate properly.

### Use an instance of the class for more ease

```csharp
IntPtr loadLibraryPtr = [...];
IntPtr start = new IntPtr(0x1000);
var fasm = new FasmNet();

fasm.AddLine("use32");
fasm.AddLine("mov eax,{0}", loadLibraryPtr);
fasm.AddLine("call eax");
fasm.AddLine("retn");

var asm = fasm.Assemble();
var rebased = fasm.Assemble(start);
```

The variable `asm` is an array of bytes containing the assembly code.  
The variable `rebased` is an array of bytes containing the assembly code with a specified origin (given by the variable `start`).

## Credits

I would thank Tomasz Grysztar, the author of FASM assembler and Shynd, who has done a similar job and gave me the idea to make this wrapper in C++/CLI.

## Author

- [ZenLulz (Jämes Ménétrey)](https://github.com/ZenLulz)

## License

Fasm.NET is licensed under the MIT License (as of v2.0.0). Dependencies are under their respective (different) licenses. Please respect all license agreements. 
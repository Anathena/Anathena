﻿/***********************************************************************
Copyright 2016 Stefan Hausotte

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

*************************************************************************/

using Ana.Source.Engine.OperatingSystems.Windows.Pe.Parser;
using Ana.Source.Engine.OperatingSystems.Windows.Pe.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Ana.Source.Engine.OperatingSystems.Windows.Pe
{
    internal class DataDirectories
    {
        private readonly byte[] _buff;
        private readonly IMAGE_DATA_DIRECTORY[] _dataDirectories;

        private readonly bool _is32Bit;
        private readonly IMAGE_SECTION_HEADER[] _sectionHeaders;
        private ExportedFunctionsParser _exportedFunctionsParser;
        private ImageBaseRelocationsParser _imageBaseRelocationsParser;
        private ImageDebugDirectoryParser _imageDebugDirectoryParser;
        private ImageBoundImportDescriptorParser _imageBoundImportDescriptorParser;
        private ImageExportDirectoriesParser _imageExportDirectoriesParser;
        private ImageImportDescriptorsParser _imageImportDescriptorsParser;
        private ImageResourceDirectoryParser _imageResourceDirectoryParser;
        private ImportedFunctionsParser _importedFunctionsParser;
        private PKCS7Parser _pkcs7Parser;
        private RuntimeFunctionsParser _runtimeFunctionsParser;
        private WinCertificateParser _winCertificateParser;
        private ImageTlsDirectoryParser _imageTlsDirectoryParser;
        private ImageDelayImportDescriptorParser _imageDelayImportDescriptorParser;
        private ImageLoadConfigDirectoryParser _imageLoadConfigDirectoryParser;

        public List<Exception> RvaToFileMappingExceptions = new List<Exception>();


        public DataDirectories(
            byte[] buff,
            ICollection<IMAGE_DATA_DIRECTORY> dataDirectories,
            ICollection<IMAGE_SECTION_HEADER> sectionHeaders,
            bool is32Bit
            )
        {
            _buff = buff;
            _dataDirectories = dataDirectories.ToArray();
            _sectionHeaders = sectionHeaders.ToArray();
            _is32Bit = is32Bit;

            InitAllParsers();
        }

        public IMAGE_EXPORT_DIRECTORY ImageExportDirectories => _imageExportDirectoriesParser?.GetParserTarget();
        public IMAGE_IMPORT_DESCRIPTOR[] ImageImportDescriptors => _imageImportDescriptorsParser?.GetParserTarget();
        public IMAGE_RESOURCE_DIRECTORY ImageResourceDirectory => _imageResourceDirectoryParser?.GetParserTarget();
        public IMAGE_BASE_RELOCATION[] ImageBaseRelocations => _imageBaseRelocationsParser?.GetParserTarget();
        public WIN_CERTIFICATE WinCertificate => _winCertificateParser?.GetParserTarget();
        public IMAGE_DEBUG_DIRECTORY ImageDebugDirectory => _imageDebugDirectoryParser?.GetParserTarget();
        public RUNTIME_FUNCTION[] RuntimeFunctions => _runtimeFunctionsParser?.GetParserTarget();
        public ExportFunction[] ExportFunctions => _exportedFunctionsParser?.GetParserTarget();
        public ImportFunction[] ImportFunctions => _importedFunctionsParser?.GetParserTarget();
        public IMAGE_BOUND_IMPORT_DESCRIPTOR ImageBoundImportDescriptor => _imageBoundImportDescriptorParser?.GetParserTarget();
        public IMAGE_TLS_DIRECTORY ImageTlsDirectory => _imageTlsDirectoryParser?.GetParserTarget();
        public X509Certificate2 PKCS7 => _pkcs7Parser?.GetParserTarget();
        public IMAGE_DELAY_IMPORT_DESCRIPTOR ImageDelayImportDescriptor => _imageDelayImportDescriptorParser?.GetParserTarget();
        public IMAGE_LOAD_CONFIG_DIRECTORY ImageLoadConfigDirectory => _imageLoadConfigDirectoryParser?.GetParserTarget();

        private void InitAllParsers()
        {
            _imageExportDirectoriesParser = InitImageExportDirectoryParser();
            _runtimeFunctionsParser = InitRuntimeFunctionsParser();
            _imageImportDescriptorsParser = InitImageImportDescriptorsParser();
            _imageBaseRelocationsParser = InitImageBaseRelocationsParser();
            _imageResourceDirectoryParser = InitImageResourceDirectoryParser();
            _imageDebugDirectoryParser = InitImageDebugDirectoryParser();
            _winCertificateParser = InitWinCertificateParser();
            _exportedFunctionsParser = InitExportFunctionParser();
            _importedFunctionsParser = InitImportedFunctionsParser();
            _imageBoundImportDescriptorParser = InitBoundImportDescriptorParser();
            _imageTlsDirectoryParser = InitImageTlsDirectoryParser();
            _pkcs7Parser = InitPKCS7Parser();
            _imageDelayImportDescriptorParser = InitImageDelayImportDescriptorParser();
            _imageLoadConfigDirectoryParser = InitImageLoadConfigDirectoryParser();
        }

        private ImageLoadConfigDirectoryParser InitImageLoadConfigDirectoryParser()
        {
            var rawAddress =
                SafeRVAtoFileMapping(_dataDirectories[(int)Constants.DataDirectoryIndex.LoadConfig].VirtualAddress);

            if (rawAddress == null)
                return null;

            return new ImageLoadConfigDirectoryParser(_buff, rawAddress.Value, !_is32Bit);
        }

        private ImageDelayImportDescriptorParser InitImageDelayImportDescriptorParser()
        {
            var rawAddress =
                SafeRVAtoFileMapping(_dataDirectories[(int)Constants.DataDirectoryIndex.DelayImport].VirtualAddress);

            if (rawAddress == null)
                return null;

            return new ImageDelayImportDescriptorParser(_buff, rawAddress.Value);
        }

        private ImageTlsDirectoryParser InitImageTlsDirectoryParser()
        {
            var rawAddress =
                SafeRVAtoFileMapping(_dataDirectories[(int)Constants.DataDirectoryIndex.TLS].VirtualAddress);

            if (rawAddress == null)
                return null;

            return new ImageTlsDirectoryParser(_buff, rawAddress.Value, !_is32Bit, _sectionHeaders);
        }

        private ImageBoundImportDescriptorParser InitBoundImportDescriptorParser()
        {
            var rawAddress =
                SafeRVAtoFileMapping(_dataDirectories[(int)Constants.DataDirectoryIndex.BoundImport].VirtualAddress);

            if (rawAddress == null)
                return null;

            return new ImageBoundImportDescriptorParser(_buff, rawAddress.Value);
        }

        private ImportedFunctionsParser InitImportedFunctionsParser()
        {
            return new ImportedFunctionsParser(
                _buff,
                ImageImportDescriptors,
                _sectionHeaders,
                !_is32Bit
                );
        }

        private PKCS7Parser InitPKCS7Parser()
        {
            return new PKCS7Parser(WinCertificate);
        }

        private ExportedFunctionsParser InitExportFunctionParser()
        {
            return new ExportedFunctionsParser(_buff, ImageExportDirectories, _sectionHeaders);
        }

        private WinCertificateParser InitWinCertificateParser()
        {
            // The security directory is the only one where the DATA_DIRECTORY VirtualAddress
            // is not an RVA but an raw offset.
            var rawAddress = _dataDirectories[(int)Constants.DataDirectoryIndex.Security].VirtualAddress;

            if (rawAddress == 0)
                return null;

            return new WinCertificateParser(_buff, rawAddress);
        }

        private ImageDebugDirectoryParser InitImageDebugDirectoryParser()
        {
            var rawAddress =
                SafeRVAtoFileMapping(_dataDirectories[(int)Constants.DataDirectoryIndex.Debug].VirtualAddress);

            if (rawAddress == null)
                return null;

            return new ImageDebugDirectoryParser(_buff, rawAddress.Value);
        }

        private ImageResourceDirectoryParser InitImageResourceDirectoryParser()
        {
            var rawAddress =
                SafeRVAtoFileMapping(_dataDirectories[(int)Constants.DataDirectoryIndex.Resource].VirtualAddress);

            if (rawAddress == null)
                return null;

            return new ImageResourceDirectoryParser(_buff, rawAddress.Value);
        }

        private ImageBaseRelocationsParser InitImageBaseRelocationsParser()
        {
            var rawAddress =
                SafeRVAtoFileMapping(_dataDirectories[(int)Constants.DataDirectoryIndex.BaseReloc].VirtualAddress);

            if (rawAddress == null)
                return null;

            return new ImageBaseRelocationsParser(
                _buff,
                rawAddress.Value,
                _dataDirectories[(int)Constants.DataDirectoryIndex.BaseReloc].Size,
                _sectionHeaders
                );
        }

        private ImageExportDirectoriesParser InitImageExportDirectoryParser()
        {
            var rawAddress =
                SafeRVAtoFileMapping(_dataDirectories[(int)Constants.DataDirectoryIndex.Export].VirtualAddress);
            if (rawAddress == null)
                return null;

            return new ImageExportDirectoriesParser(_buff, rawAddress.Value);
        }

        private RuntimeFunctionsParser InitRuntimeFunctionsParser()
        {
            var rawAddress =
                SafeRVAtoFileMapping(_dataDirectories[(int)Constants.DataDirectoryIndex.Exception].VirtualAddress);

            if (rawAddress == null)
                return null;

            return new RuntimeFunctionsParser(
                _buff,
                rawAddress.Value,
                _is32Bit,
                _dataDirectories[(int)Constants.DataDirectoryIndex.Exception].Size,
                _sectionHeaders
                );
        }

        private ImageImportDescriptorsParser InitImageImportDescriptorsParser()
        {
            var rawAddress =
                SafeRVAtoFileMapping(_dataDirectories[(int)Constants.DataDirectoryIndex.Import].VirtualAddress);

            if (rawAddress == null)
                return null;

            return new ImageImportDescriptorsParser(_buff, rawAddress.Value);
        }

        private uint? SafeRVAtoFileMapping(uint rva)
        {
            uint? rawAddress = null;
            try
            {
                rawAddress = rva.RVAtoFileMapping(_sectionHeaders);
            }
            catch (Exception exception)
            {
                RvaToFileMappingExceptions.Add(exception);
            }

            return rawAddress;
        }
    }
}
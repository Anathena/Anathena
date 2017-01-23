namespace Ana.Source.SignatureCollector
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;

    /// <summary>
    /// A class for storing an executale signature.
    /// </summary>
    [DataContract]
    internal class SignatureModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureModel" /> class.
        /// </summary>
        public SignatureModel()
        {
        }

        /// <summary>
        /// Gets or sets the 'window title' of the signature.
        /// </summary>
        [DataMember]
        public String WindowTitle { get; set; }

        /// <summary>
        /// Gets or sets the 'binary version' of the signature.
        /// </summary>
        [DataMember]
        public String BinaryVersion { get; set; }

        /// <summary>
        /// Gets or sets the 'binary header hash' of the signature.
        /// </summary>
        [DataMember]
        public String BinaryHeaderHash { get; set; }

        /// <summary>
        /// Gets or sets the 'binary import hash' of the signature.
        /// </summary>
        [DataMember]
        public String BinaryImportHash { get; set; }

        /// <summary>
        /// Gets or sets the 'main module hash' of the signature.
        /// </summary>
        [DataMember]
        public String MainModuleHash { get; set; }

        /// <summary>
        /// Gets or sets the 'emulator hash' of the signature.
        /// </summary>
        [DataMember]
        public String EmulatorHash { get; set; }

        /// <summary>
        /// Deserialized a stored base64 application signature.
        /// </summary>
        /// <param name="base64Signature">The base64 signature.</param>
        /// <returns>The deserialized signature object.</returns>
        public static SignatureModel Deserialize(String base64Signature)
        {
            Byte[] bytes = Convert.FromBase64String(base64Signature);
            MemoryStream memoryStream = new MemoryStream(bytes);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SignatureModel));
            SignatureModel signature = (SignatureModel)serializer.ReadObject(memoryStream);
            return signature;
        }

        /// <summary>
        /// Gets the full application signature, which comes from serializing this object to JSON and converting it to base64.
        /// </summary>
        /// <returns>The base64 encoded signature string.</returns>
        public String GetFullSignature()
        {
            if (String.IsNullOrEmpty(this.WindowTitle) &&
                String.IsNullOrEmpty(this.BinaryVersion) &&
                String.IsNullOrEmpty(this.BinaryHeaderHash) &&
                String.IsNullOrEmpty(this.BinaryImportHash) &&
                String.IsNullOrEmpty(this.MainModuleHash) &&
                String.IsNullOrEmpty(this.EmulatorHash))
            {
                return String.Empty;
            }

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SignatureModel));
            MemoryStream memoryStream = new MemoryStream();
            serializer.WriteObject(memoryStream, this);
            memoryStream.Position = 0;
            StreamReader streamReader = new StreamReader(memoryStream);
            String json = streamReader.ReadToEnd();
            Byte[] bytes = Encoding.UTF8.GetBytes(json);
            String base64Signature = Convert.ToBase64String(bytes);

            return base64Signature;
        }
    }
    //// End class
}
//// End namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.Cryptography
{
	/// <summary>
	/// Hash Algorith type
	/// </summary>
	public enum HashAlgorithm
	{
		/// <summary>
		/// Message Digest Algorithm 2
		/// </summary>
		MD2=1,
		/// <summary>
		/// Message-Digest algorithm 5
		/// </summary>
		MD5=2,
		/// <summary>
		/// Secure Hash Algorithm 1
		/// </summary>
		Sha1=3,
		/// <summary>
		/// Secure Hash Algorithm 256
		/// </summary>
		Sha256=4,
		/// <summary>
		/// Secure Hash Algorithm 384
		/// </summary>
		Sha384=5,
		/// <summary>
		/// Secure Hash Algorithm 512
		/// </summary>
		Sha512=6
	}

	public enum KeyType
	: short
	{
		/// <summary>
		/// SYM_KEY
		/// </summary>
		Symmetric = 1,
		/// <summary>
		/// PRIVATE_KEY
		/// </summary>
		Private = 2,
		/// <summary>
		/// PUBLIC_KEY
		/// </summary>
		Public = 3,
	}

	public enum AlgorithmType
		: short
	{
		Rc4 = 1,
		AesCbc = 2,
		Hmac = 257
	}
}

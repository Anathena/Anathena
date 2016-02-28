// --------------------------------------------------------------------------------
// SharpDisasm (File: SharpDisasm\optable.tt)
// Copyright (c) 2014-2015 Justin Stenning
// http://spazzarama.com
// https://github.com/spazzarama/SharpDisasm
// https://sharpdisasm.codeplex.com/
//
// SharpDisasm is distributed under the 2-clause "Simplified BSD License".
//
// Portions of SharpDisasm are ported to C# from udis86 a C disassembler project
// also distributed under the terms of the 2-clause "Simplified BSD License" and
// Copyright (c) 2002-2012, Vivek Thampi <vivek.mt@gmail.com>
// All rights reserved.
// UDIS86: https://github.com/vmt/udis86
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
// 
// 1. Redistributions of source code must retain the above copyright notice, 
//    this list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright notice, 
//    this list of conditions and the following disclaimer in the documentation 
//    and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON 
// ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// --------------------------------------------------------------------------------

// Do not edit. File generated from optable.xml on 27-Feb-2016 09:45 PM
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#pragma warning disable 1591
namespace SharpDisasm.Udis86
{
    internal static class InstructionTables
    {
        #region Lookup Tables
        public const int INVALID = 0;
        
        internal static readonly ushort[] ud_itab__0 = {
          /* 00 */          15,          16,          17,          18,
          /* 04 */          19,          20,    0x8000|1,    0x8000|2,
          /* 08 */         964,         965,         966,         967,
          /* 0c */         968,         969,    0x8000|3,    0x8000|4,
          /* 10 */           5,           6,           7,           8,
          /* 14 */           9,          10,  0x8000|284,  0x8000|285,
          /* 18 */        1336,        1337,        1338,        1339,
          /* 1c */        1340,        1341,  0x8000|286,  0x8000|287,
          /* 20 */          49,          50,          51,          52,
          /* 24 */          53,          54,     INVALID,  0x8000|288,
          /* 28 */        1407,        1408,        1409,        1410,
          /* 2c */        1411,        1412,     INVALID,  0x8000|289,
          /* 30 */        1487,        1488,        1489,        1490,
          /* 34 */        1491,        1492,     INVALID,  0x8000|290,
          /* 38 */         100,         101,         102,         103,
          /* 3c */         104,         105,     INVALID,  0x8000|291,
          /* 40 */         699,         700,         701,         702,
          /* 44 */         703,         704,         705,         706,
          /* 48 */         175,         176,         177,         178,
          /* 4c */         179,         180,         181,         182,
          /* 50 */        1246,        1247,        1248,        1249,
          /* 54 */        1250,        1251,        1252,        1253,
          /* 58 */        1101,        1102,        1103,        1104,
          /* 5c */        1105,        1106,        1107,        1108,
          /* 60 */  0x8000|292,  0x8000|295,  0x8000|298,  0x8000|299,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */        1254,         697,        1256,         698,
          /* 6c */         709,  0x8000|300,         982,  0x8000|301,
          /* 70 */         726,         728,         730,         732,
          /* 74 */         734,         736,         738,         740,
          /* 78 */         742,         744,         746,         748,
          /* 7c */         750,         752,         754,         756,
          /* 80 */  0x8000|302,  0x8000|303,  0x8000|304,  0x8000|313,
          /* 84 */        1433,        1434,        1475,        1476,
          /* 88 */         828,         829,         830,         831,
          /* 8c */         832,         770,         833,  0x8000|314,
          /* 90 */        1477,        1478,        1479,        1480,
          /* 94 */        1481,        1482,        1483,        1484,
          /* 98 */  0x8000|315,  0x8000|316,  0x8000|317,        1470,
          /* 9c */  0x8000|318,  0x8000|322,        1310,         766,
          /* a0 */         834,         835,         836,         837,
          /* a4 */         922,  0x8000|326,         114,  0x8000|327,
          /* a8 */        1435,        1436,        1402,  0x8000|328,
          /* ac */         790,  0x8000|329,        1346,  0x8000|330,
          /* b0 */         838,         839,         840,         841,
          /* b4 */         842,         843,         844,         845,
          /* b8 */         846,         847,         848,         849,
          /* bc */         850,         851,         852,         853,
          /* c0 */  0x8000|331,  0x8000|332,        1301,        1302,
          /* c4 */  0x8000|333,  0x8000|403,  0x8000|405,  0x8000|406,
          /* c8 */         200,         776,        1303,        1304,
          /* cc */         713,         714,  0x8000|407,  0x8000|408,
          /* d0 */  0x8000|409,  0x8000|410,  0x8000|411,  0x8000|412,
          /* d4 */  0x8000|413,  0x8000|414,  0x8000|415,        1486,
          /* d8 */  0x8000|416,  0x8000|419,  0x8000|422,  0x8000|425,
          /* dc */  0x8000|428,  0x8000|431,  0x8000|434,  0x8000|437,
          /* e0 */         794,         795,         796,  0x8000|440,
          /* e4 */         690,         691,         978,         979,
          /* e8 */          72,         763,  0x8000|441,         765,
          /* ec */         692,         693,         980,         981,
          /* f0 */         789,         712,        1299,        1300,
          /* f4 */         687,          83,  0x8000|442,  0x8000|443,
          /* f8 */          77,        1395,          81,        1398,
          /* fc */          78,        1396,  0x8000|444,  0x8000|445,
        };
        
        internal static readonly ushort[] ud_itab__1 = {
          /* 00 */        1240,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__2 = {
          /* 00 */        1096,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__3 = {
          /* 00 */        1241,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__4 = {
          /* 00 */    0x8000|5,    0x8000|6,         767,         797,
          /* 04 */     INVALID,        1426,          82,        1431,
          /* 08 */         716,        1471,     INVALID,        1444,
          /* 0c */     INVALID,   0x8000|27,         430,   0x8000|28,
          /* 10 */   0x8000|29,   0x8000|30,   0x8000|31,   0x8000|34,
          /* 14 */   0x8000|35,   0x8000|36,   0x8000|37,   0x8000|40,
          /* 18 */   0x8000|41,         955,         956,         957,
          /* 1c */         958,         959,         960,         961,
          /* 20 */         854,         855,         856,         857,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */   0x8000|42,   0x8000|43,   0x8000|44,   0x8000|45,
          /* 2c */   0x8000|46,   0x8000|47,   0x8000|48,   0x8000|49,
          /* 30 */        1472,        1297,        1295,        1296,
          /* 34 */   0x8000|50,   0x8000|52,     INVALID,        1514,
          /* 38 */   0x8000|54,     INVALID,  0x8000|116,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */          84,          85,          86,          87,
          /* 44 */          88,          89,          90,          91,
          /* 48 */          92,          93,          94,          95,
          /* 4c */          96,          97,          98,          99,
          /* 50 */  0x8000|143,  0x8000|144,  0x8000|145,  0x8000|146,
          /* 54 */  0x8000|147,  0x8000|148,  0x8000|149,  0x8000|150,
          /* 58 */  0x8000|151,  0x8000|152,  0x8000|153,  0x8000|154,
          /* 5c */  0x8000|155,  0x8000|156,  0x8000|157,  0x8000|158,
          /* 60 */  0x8000|159,  0x8000|160,  0x8000|161,  0x8000|162,
          /* 64 */  0x8000|163,  0x8000|164,  0x8000|165,  0x8000|166,
          /* 68 */  0x8000|167,  0x8000|168,  0x8000|169,  0x8000|170,
          /* 6c */  0x8000|171,  0x8000|172,  0x8000|173,  0x8000|176,
          /* 70 */  0x8000|177,  0x8000|178,  0x8000|182,  0x8000|186,
          /* 74 */  0x8000|191,  0x8000|192,  0x8000|193,         199,
          /* 78 */  0x8000|194,  0x8000|195,     INVALID,     INVALID,
          /* 7c */  0x8000|196,  0x8000|197,  0x8000|198,  0x8000|201,
          /* 80 */         727,         729,         731,         733,
          /* 84 */         735,         737,         739,         741,
          /* 88 */         743,         745,         747,         749,
          /* 8c */         751,         753,         755,         757,
          /* 90 */        1350,        1351,        1352,        1353,
          /* 94 */        1354,        1355,        1356,        1357,
          /* 98 */        1358,        1359,        1360,        1361,
          /* 9c */        1362,        1363,        1364,        1365,
          /* a0 */        1245,        1100,         131,        1670,
          /* a4 */        1375,        1376,  0x8000|202,  0x8000|207,
          /* a8 */        1244,        1099,        1305,        1675,
          /* ac */        1377,        1378,  0x8000|215,         694,
          /* b0 */         122,         123,         775,        1673,
          /* b4 */         772,         773,         940,         941,
          /* b8 */  0x8000|221,     INVALID,  0x8000|222,        1671,
          /* bc */        1659,        1660,         930,         931,
          /* c0 */        1473,        1474,  0x8000|223,         904,
          /* c4 */  0x8000|224,  0x8000|225,  0x8000|226,  0x8000|227,
          /* c8 */        1661,        1662,        1663,        1664,
          /* cc */        1665,        1666,        1667,        1668,
          /* d0 */  0x8000|236,  0x8000|237,  0x8000|238,  0x8000|239,
          /* d4 */  0x8000|240,  0x8000|241,  0x8000|242,  0x8000|243,
          /* d8 */  0x8000|244,  0x8000|245,  0x8000|246,  0x8000|247,
          /* dc */  0x8000|248,  0x8000|249,  0x8000|250,  0x8000|251,
          /* e0 */  0x8000|252,  0x8000|253,  0x8000|254,  0x8000|255,
          /* e4 */  0x8000|256,  0x8000|257,  0x8000|258,  0x8000|259,
          /* e8 */  0x8000|260,  0x8000|261,  0x8000|262,  0x8000|263,
          /* ec */  0x8000|264,  0x8000|265,  0x8000|266,  0x8000|267,
          /* f0 */  0x8000|268,  0x8000|269,  0x8000|270,  0x8000|271,
          /* f4 */  0x8000|272,  0x8000|273,  0x8000|274,  0x8000|275,
          /* f8 */  0x8000|277,  0x8000|278,  0x8000|279,  0x8000|280,
          /* fc */  0x8000|281,  0x8000|282,  0x8000|283,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__5 = {
          /* 00 */        1384,        1406,         786,         798,
          /* 04 */        1453,        1454,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__6 = {
          /* 00 */    0x8000|7,    0x8000|8,
        };
        
        internal static readonly ushort[] ud_itab__7 = {
          /* 00 */        1374,        1383,         785,         774,
          /* 04 */        1385,     INVALID,         787,         719,
        };
        
        internal static readonly ushort[] ud_itab__8 = {
          /* 00 */    0x8000|9,   0x8000|14,   0x8000|15,   0x8000|16,
          /* 04 */        1386,     INVALID,         788,   0x8000|25,
        };
        
        internal static readonly ushort[] ud_itab__9 = {
          /* 00 */     INVALID,   0x8000|10,   0x8000|11,   0x8000|12,
          /* 04 */   0x8000|13,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__10 = {
          /* 00 */     INVALID,        1455,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__11 = {
          /* 00 */     INVALID,        1461,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__12 = {
          /* 00 */     INVALID,        1462,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__13 = {
          /* 00 */     INVALID,        1463,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__14 = {
          /* 00 */         824,         952,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__15 = {
          /* 00 */        1485,        1508,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__16 = {
          /* 00 */   0x8000|17,   0x8000|18,   0x8000|19,   0x8000|20,
          /* 04 */   0x8000|21,   0x8000|22,   0x8000|23,   0x8000|24,
        };
        
        internal static readonly ushort[] ud_itab__17 = {
          /* 00 */        1466,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__18 = {
          /* 00 */        1467,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__19 = {
          /* 00 */        1468,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__20 = {
          /* 00 */        1469,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__21 = {
          /* 00 */        1397,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__22 = {
          /* 00 */          80,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__23 = {
          /* 00 */        1399,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__24 = {
          /* 00 */         720,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__25 = {
          /* 00 */        1425,   0x8000|26,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__26 = {
          /* 00 */        1298,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__27 = {
          /* 00 */        1119,        1120,        1121,        1122,
          /* 04 */        1123,        1124,        1125,        1126,
        };
        
        internal static readonly ushort[] ud_itab__28 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 0c */        1216,        1217,     INVALID,     INVALID,
          /* 10 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 14 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */        1218,        1219,     INVALID,     INVALID,
          /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,        1220,     INVALID,
          /* 8c */     INVALID,     INVALID,        1221,     INVALID,
          /* 90 */        1222,     INVALID,     INVALID,     INVALID,
          /* 94 */        1223,     INVALID,        1224,        1225,
          /* 98 */     INVALID,     INVALID,        1226,     INVALID,
          /* 9c */     INVALID,     INVALID,        1227,     INVALID,
          /* a0 */        1228,     INVALID,     INVALID,     INVALID,
          /* a4 */        1229,     INVALID,        1230,        1231,
          /* a8 */     INVALID,     INVALID,        1232,     INVALID,
          /* ac */     INVALID,     INVALID,        1233,     INVALID,
          /* b0 */        1234,     INVALID,     INVALID,     INVALID,
          /* b4 */        1235,     INVALID,        1236,        1237,
          /* b8 */     INVALID,     INVALID,     INVALID,        1238,
          /* bc */     INVALID,     INVALID,     INVALID,        1239,
          /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* dc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__29 = {
          /* 00 */         936,         925,         928,         932,
        };
        
        internal static readonly ushort[] ud_itab__30 = {
          /* 00 */         938,         926,         929,         934,
        };
        
        internal static readonly ushort[] ud_itab__31 = {
          /* 00 */   0x8000|32,   0x8000|33,
        };
        
        internal static readonly ushort[] ud_itab__32 = {
          /* 00 */         892,        1563,        1571,         888,
        };
        
        internal static readonly ushort[] ud_itab__33 = {
          /* 00 */         896,        1561,        1569,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__34 = {
          /* 00 */         894,     INVALID,     INVALID,         890,
        };
        
        internal static readonly ushort[] ud_itab__35 = {
          /* 00 */        1449,     INVALID,     INVALID,        1451,
        };
        
        internal static readonly ushort[] ud_itab__36 = {
          /* 00 */        1447,     INVALID,     INVALID,        1445,
        };
        
        internal static readonly ushort[] ud_itab__37 = {
          /* 00 */   0x8000|38,   0x8000|39,
        };
        
        internal static readonly ushort[] ud_itab__38 = {
          /* 00 */         882,     INVALID,        1567,         878,
        };
        
        internal static readonly ushort[] ud_itab__39 = {
          /* 00 */         886,     INVALID,        1565,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__40 = {
          /* 00 */         884,     INVALID,     INVALID,         880,
        };
        
        internal static readonly ushort[] ud_itab__41 = {
          /* 00 */        1127,        1128,        1129,        1130,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__42 = {
          /* 00 */         862,     INVALID,     INVALID,         858,
        };
        
        internal static readonly ushort[] ud_itab__43 = {
          /* 00 */         864,     INVALID,     INVALID,         860,
        };
        
        internal static readonly ushort[] ud_itab__44 = {
          /* 00 */         141,         152,         154,         142,
        };
        
        internal static readonly ushort[] ud_itab__45 = {
          /* 00 */         907,     INVALID,     INVALID,         905,
        };
        
        internal static readonly ushort[] ud_itab__46 = {
          /* 00 */         165,         166,         168,         162,
        };
        
        internal static readonly ushort[] ud_itab__47 = {
          /* 00 */         147,         148,         158,         138,
        };
        
        internal static readonly ushort[] ud_itab__48 = {
          /* 00 */        1442,     INVALID,     INVALID,        1440,
        };
        
        internal static readonly ushort[] ud_itab__49 = {
          /* 00 */         129,     INVALID,     INVALID,         127,
        };
        
        internal static readonly ushort[] ud_itab__50 = {
          /* 00 */        1427,   0x8000|51,
        };
        
        internal static readonly ushort[] ud_itab__51 = {
          /* 00 */     INVALID,        1428,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__52 = {
          /* 00 */        1429,   0x8000|53,
        };
        
        internal static readonly ushort[] ud_itab__53 = {
          /* 00 */     INVALID,        1430,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__54 = {
          /* 00 */   0x8000|55,   0x8000|56,   0x8000|57,   0x8000|58,
          /* 04 */   0x8000|59,   0x8000|60,   0x8000|61,   0x8000|62,
          /* 08 */   0x8000|63,   0x8000|64,   0x8000|65,   0x8000|66,
          /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 10 */   0x8000|67,     INVALID,     INVALID,     INVALID,
          /* 14 */   0x8000|68,   0x8000|69,     INVALID,   0x8000|70,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */   0x8000|71,   0x8000|72,   0x8000|73,     INVALID,
          /* 20 */   0x8000|74,   0x8000|75,   0x8000|76,   0x8000|77,
          /* 24 */   0x8000|78,   0x8000|79,     INVALID,     INVALID,
          /* 28 */   0x8000|80,   0x8000|81,   0x8000|82,   0x8000|83,
          /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 30 */   0x8000|84,   0x8000|85,   0x8000|86,   0x8000|87,
          /* 34 */   0x8000|88,   0x8000|89,     INVALID,   0x8000|90,
          /* 38 */   0x8000|91,   0x8000|92,   0x8000|93,   0x8000|94,
          /* 3c */   0x8000|95,   0x8000|96,   0x8000|97,   0x8000|98,
          /* 40 */   0x8000|99,  0x8000|100,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 80 */  0x8000|101,  0x8000|105,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,  0x8000|109,
          /* dc */  0x8000|110,  0x8000|111,  0x8000|112,  0x8000|113,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */  0x8000|114,  0x8000|115,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__55 = {
          /* 00 */        1582,     INVALID,     INVALID,        1583,
        };
        
        internal static readonly ushort[] ud_itab__56 = {
          /* 00 */        1585,     INVALID,     INVALID,        1586,
        };
        
        internal static readonly ushort[] ud_itab__57 = {
          /* 00 */        1588,     INVALID,     INVALID,        1589,
        };
        
        internal static readonly ushort[] ud_itab__58 = {
          /* 00 */        1591,     INVALID,     INVALID,        1592,
        };
        
        internal static readonly ushort[] ud_itab__59 = {
          /* 00 */        1594,     INVALID,     INVALID,        1595,
        };
        
        internal static readonly ushort[] ud_itab__60 = {
          /* 00 */        1597,     INVALID,     INVALID,        1598,
        };
        
        internal static readonly ushort[] ud_itab__61 = {
          /* 00 */        1600,     INVALID,     INVALID,        1601,
        };
        
        internal static readonly ushort[] ud_itab__62 = {
          /* 00 */        1603,     INVALID,     INVALID,        1604,
        };
        
        internal static readonly ushort[] ud_itab__63 = {
          /* 00 */        1606,     INVALID,     INVALID,        1607,
        };
        
        internal static readonly ushort[] ud_itab__64 = {
          /* 00 */        1612,     INVALID,     INVALID,        1613,
        };
        
        internal static readonly ushort[] ud_itab__65 = {
          /* 00 */        1609,     INVALID,     INVALID,        1610,
        };
        
        internal static readonly ushort[] ud_itab__66 = {
          /* 00 */        1615,     INVALID,     INVALID,        1616,
        };
        
        internal static readonly ushort[] ud_itab__67 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1621,
        };
        
        internal static readonly ushort[] ud_itab__68 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1657,
        };
        
        internal static readonly ushort[] ud_itab__69 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1656,
        };
        
        internal static readonly ushort[] ud_itab__70 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1711,
        };
        
        internal static readonly ushort[] ud_itab__71 = {
          /* 00 */        1573,     INVALID,     INVALID,        1574,
        };
        
        internal static readonly ushort[] ud_itab__72 = {
          /* 00 */        1576,     INVALID,     INVALID,        1577,
        };
        
        internal static readonly ushort[] ud_itab__73 = {
          /* 00 */        1579,     INVALID,     INVALID,        1580,
        };
        
        internal static readonly ushort[] ud_itab__74 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1685,
        };
        
        internal static readonly ushort[] ud_itab__75 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1687,
        };
        
        internal static readonly ushort[] ud_itab__76 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1689,
        };
        
        internal static readonly ushort[] ud_itab__77 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1691,
        };
        
        internal static readonly ushort[] ud_itab__78 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1693,
        };
        
        internal static readonly ushort[] ud_itab__79 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1695,
        };
        
        internal static readonly ushort[] ud_itab__80 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1622,
        };
        
        internal static readonly ushort[] ud_itab__81 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1708,
        };
        
        internal static readonly ushort[] ud_itab__82 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1681,
        };
        
        internal static readonly ushort[] ud_itab__83 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1683,
        };
        
        internal static readonly ushort[] ud_itab__84 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1696,
        };
        
        internal static readonly ushort[] ud_itab__85 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1698,
        };
        
        internal static readonly ushort[] ud_itab__86 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1700,
        };
        
        internal static readonly ushort[] ud_itab__87 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1702,
        };
        
        internal static readonly ushort[] ud_itab__88 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1704,
        };
        
        internal static readonly ushort[] ud_itab__89 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1706,
        };
        
        internal static readonly ushort[] ud_itab__90 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1717,
        };
        
        internal static readonly ushort[] ud_itab__91 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1624,
        };
        
        internal static readonly ushort[] ud_itab__92 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1626,
        };
        
        internal static readonly ushort[] ud_itab__93 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1628,
        };
        
        internal static readonly ushort[] ud_itab__94 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1630,
        };
        
        internal static readonly ushort[] ud_itab__95 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1632,
        };
        
        internal static readonly ushort[] ud_itab__96 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1634,
        };
        
        internal static readonly ushort[] ud_itab__97 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1638,
        };
        
        internal static readonly ushort[] ud_itab__98 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1636,
        };
        
        internal static readonly ushort[] ud_itab__99 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1640,
        };
        
        internal static readonly ushort[] ud_itab__100 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1642,
        };
        
        internal static readonly ushort[] ud_itab__101 = {
          /* 00 */     INVALID,     INVALID,     INVALID,  0x8000|102,
        };
        
        internal static readonly ushort[] ud_itab__102 = {
          /* 00 */  0x8000|103,  0x8000|104,
        };
        
        internal static readonly ushort[] ud_itab__103 = {
          /* 00 */     INVALID,         717,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__104 = {
          /* 00 */     INVALID,         718,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__105 = {
          /* 00 */     INVALID,     INVALID,     INVALID,  0x8000|106,
        };
        
        internal static readonly ushort[] ud_itab__106 = {
          /* 00 */  0x8000|107,  0x8000|108,
        };
        
        internal static readonly ushort[] ud_itab__107 = {
          /* 00 */     INVALID,         721,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__108 = {
          /* 00 */     INVALID,         722,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__109 = {
          /* 00 */     INVALID,     INVALID,     INVALID,          45,
        };
        
        internal static readonly ushort[] ud_itab__110 = {
          /* 00 */     INVALID,     INVALID,     INVALID,          41,
        };
        
        internal static readonly ushort[] ud_itab__111 = {
          /* 00 */     INVALID,     INVALID,     INVALID,          43,
        };
        
        internal static readonly ushort[] ud_itab__112 = {
          /* 00 */     INVALID,     INVALID,     INVALID,          37,
        };
        
        internal static readonly ushort[] ud_itab__113 = {
          /* 00 */     INVALID,     INVALID,     INVALID,          39,
        };
        
        internal static readonly ushort[] ud_itab__114 = {
          /* 00 */        1723,        1725,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__115 = {
          /* 00 */        1724,        1726,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__116 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 08 */  0x8000|117,  0x8000|118,  0x8000|119,  0x8000|120,
          /* 0c */  0x8000|121,  0x8000|122,  0x8000|123,  0x8000|124,
          /* 10 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 14 */  0x8000|125,  0x8000|126,  0x8000|127,  0x8000|129,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */  0x8000|130,  0x8000|131,  0x8000|132,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */  0x8000|134,  0x8000|135,  0x8000|136,     INVALID,
          /* 44 */  0x8000|137,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 60 */  0x8000|138,  0x8000|139,  0x8000|140,  0x8000|141,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* dc */     INVALID,     INVALID,     INVALID,  0x8000|142,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__117 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1644,
        };
        
        internal static readonly ushort[] ud_itab__118 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1646,
        };
        
        internal static readonly ushort[] ud_itab__119 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1648,
        };
        
        internal static readonly ushort[] ud_itab__120 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1650,
        };
        
        internal static readonly ushort[] ud_itab__121 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1654,
        };
        
        internal static readonly ushort[] ud_itab__122 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1652,
        };
        
        internal static readonly ushort[] ud_itab__123 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1677,
        };
        
        internal static readonly ushort[] ud_itab__124 = {
          /* 00 */        1618,     INVALID,     INVALID,        1619,
        };
        
        internal static readonly ushort[] ud_itab__125 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1045,
        };
        
        internal static readonly ushort[] ud_itab__126 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1056,
        };
        
        internal static readonly ushort[] ud_itab__127 = {
          /* 00 */     INVALID,     INVALID,     INVALID,  0x8000|128,
        };
        
        internal static readonly ushort[] ud_itab__128 = {
          /* 00 */        1047,        1049,        1051,
        };
        
        internal static readonly ushort[] ud_itab__129 = {
          /* 00 */     INVALID,     INVALID,     INVALID,         201,
        };
        
        internal static readonly ushort[] ud_itab__130 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1058,
        };
        
        internal static readonly ushort[] ud_itab__131 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1557,
        };
        
        internal static readonly ushort[] ud_itab__132 = {
          /* 00 */     INVALID,     INVALID,     INVALID,  0x8000|133,
        };
        
        internal static readonly ushort[] ud_itab__133 = {
          /* 00 */        1062,        1063,        1064,
        };
        
        internal static readonly ushort[] ud_itab__134 = {
          /* 00 */     INVALID,     INVALID,     INVALID,         197,
        };
        
        internal static readonly ushort[] ud_itab__135 = {
          /* 00 */     INVALID,     INVALID,     INVALID,         195,
        };
        
        internal static readonly ushort[] ud_itab__136 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1679,
        };
        
        internal static readonly ushort[] ud_itab__137 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1512,
        };
        
        internal static readonly ushort[] ud_itab__138 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1715,
        };
        
        internal static readonly ushort[] ud_itab__139 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1713,
        };
        
        internal static readonly ushort[] ud_itab__140 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1721,
        };
        
        internal static readonly ushort[] ud_itab__141 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1719,
        };
        
        internal static readonly ushort[] ud_itab__142 = {
          /* 00 */     INVALID,     INVALID,     INVALID,          47,
        };
        
        internal static readonly ushort[] ud_itab__143 = {
          /* 00 */         900,     INVALID,     INVALID,         898,
        };
        
        internal static readonly ushort[] ud_itab__144 = {
          /* 00 */        1387,        1391,        1393,        1389,
        };
        
        internal static readonly ushort[] ud_itab__145 = {
          /* 00 */        1306,     INVALID,        1308,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__146 = {
          /* 00 */        1291,     INVALID,        1293,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__147 = {
          /* 00 */          61,     INVALID,     INVALID,          59,
        };
        
        internal static readonly ushort[] ud_itab__148 = {
          /* 00 */          65,     INVALID,     INVALID,          63,
        };
        
        internal static readonly ushort[] ud_itab__149 = {
          /* 00 */         976,     INVALID,     INVALID,         974,
        };
        
        internal static readonly ushort[] ud_itab__150 = {
          /* 00 */        1499,     INVALID,     INVALID,        1497,
        };
        
        internal static readonly ushort[] ud_itab__151 = {
          /* 00 */          27,          29,          31,          25,
        };
        
        internal static readonly ushort[] ud_itab__152 = {
          /* 00 */         946,         948,         950,         944,
        };
        
        internal static readonly ushort[] ud_itab__153 = {
          /* 00 */         145,         150,         156,         139,
        };
        
        internal static readonly ushort[] ud_itab__154 = {
          /* 00 */         134,     INVALID,         163,         143,
        };
        
        internal static readonly ushort[] ud_itab__155 = {
          /* 00 */        1419,        1421,        1423,        1417,
        };
        
        internal static readonly ushort[] ud_itab__156 = {
          /* 00 */         818,         820,         822,         816,
        };
        
        internal static readonly ushort[] ud_itab__157 = {
          /* 00 */         189,         191,         193,         187,
        };
        
        internal static readonly ushort[] ud_itab__158 = {
          /* 00 */         802,         804,         806,         800,
        };
        
        internal static readonly ushort[] ud_itab__159 = {
          /* 00 */        1209,     INVALID,     INVALID,        1207,
        };
        
        internal static readonly ushort[] ud_itab__160 = {
          /* 00 */        1212,     INVALID,     INVALID,        1210,
        };
        
        internal static readonly ushort[] ud_itab__161 = {
          /* 00 */        1215,     INVALID,     INVALID,        1213,
        };
        
        internal static readonly ushort[] ud_itab__162 = {
          /* 00 */         987,     INVALID,     INVALID,         985,
        };
        
        internal static readonly ushort[] ud_itab__163 = {
          /* 00 */        1038,     INVALID,     INVALID,        1036,
        };
        
        internal static readonly ushort[] ud_itab__164 = {
          /* 00 */        1041,     INVALID,     INVALID,        1039,
        };
        
        internal static readonly ushort[] ud_itab__165 = {
          /* 00 */        1044,     INVALID,     INVALID,        1042,
        };
        
        internal static readonly ushort[] ud_itab__166 = {
          /* 00 */         993,     INVALID,     INVALID,         991,
        };
        
        internal static readonly ushort[] ud_itab__167 = {
          /* 00 */        1200,     INVALID,     INVALID,        1198,
        };
        
        internal static readonly ushort[] ud_itab__168 = {
          /* 00 */        1203,     INVALID,     INVALID,        1201,
        };
        
        internal static readonly ushort[] ud_itab__169 = {
          /* 00 */        1206,     INVALID,     INVALID,        1204,
        };
        
        internal static readonly ushort[] ud_itab__170 = {
          /* 00 */         990,     INVALID,     INVALID,         988,
        };
        
        internal static readonly ushort[] ud_itab__171 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1547,
        };
        
        internal static readonly ushort[] ud_itab__172 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1545,
        };
        
        internal static readonly ushort[] ud_itab__173 = {
          /* 00 */  0x8000|174,     INVALID,     INVALID,  0x8000|175,
        };
        
        internal static readonly ushort[] ud_itab__174 = {
          /* 00 */         866,         867,         910,
        };
        
        internal static readonly ushort[] ud_itab__175 = {
          /* 00 */         868,         870,         911,
        };
        
        internal static readonly ushort[] ud_itab__176 = {
          /* 00 */         920,     INVALID,        1522,        1517,
        };
        
        internal static readonly ushort[] ud_itab__177 = {
          /* 00 */        1134,        1537,        1535,        1539,
        };
        
        internal static readonly ushort[] ud_itab__178 = {
          /* 00 */     INVALID,     INVALID,  0x8000|179,     INVALID,
          /* 04 */  0x8000|180,     INVALID,  0x8000|181,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__179 = {
          /* 00 */        1159,     INVALID,     INVALID,        1163,
        };
        
        internal static readonly ushort[] ud_itab__180 = {
          /* 00 */        1152,     INVALID,     INVALID,        1150,
        };
        
        internal static readonly ushort[] ud_itab__181 = {
          /* 00 */        1138,     INVALID,     INVALID,        1137,
        };
        
        internal static readonly ushort[] ud_itab__182 = {
          /* 00 */     INVALID,     INVALID,  0x8000|183,     INVALID,
          /* 04 */  0x8000|184,     INVALID,  0x8000|185,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__183 = {
          /* 00 */        1165,     INVALID,     INVALID,        1169,
        };
        
        internal static readonly ushort[] ud_itab__184 = {
          /* 00 */        1153,     INVALID,     INVALID,        1157,
        };
        
        internal static readonly ushort[] ud_itab__185 = {
          /* 00 */        1142,     INVALID,     INVALID,        1141,
        };
        
        internal static readonly ushort[] ud_itab__186 = {
          /* 00 */     INVALID,     INVALID,  0x8000|187,  0x8000|188,
          /* 04 */     INVALID,     INVALID,  0x8000|189,  0x8000|190,
        };
        
        internal static readonly ushort[] ud_itab__187 = {
          /* 00 */        1171,     INVALID,     INVALID,        1175,
        };
        
        internal static readonly ushort[] ud_itab__188 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1543,
        };
        
        internal static readonly ushort[] ud_itab__189 = {
          /* 00 */        1146,     INVALID,     INVALID,        1145,
        };
        
        internal static readonly ushort[] ud_itab__190 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1541,
        };
        
        internal static readonly ushort[] ud_itab__191 = {
          /* 00 */        1027,     INVALID,     INVALID,        1028,
        };
        
        internal static readonly ushort[] ud_itab__192 = {
          /* 00 */        1030,     INVALID,     INVALID,        1031,
        };
        
        internal static readonly ushort[] ud_itab__193 = {
          /* 00 */        1033,     INVALID,     INVALID,        1034,
        };
        
        internal static readonly ushort[] ud_itab__194 = {
          /* 00 */     INVALID,        1464,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__195 = {
          /* 00 */     INVALID,        1465,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__196 = {
          /* 00 */     INVALID,        1551,     INVALID,        1549,
        };
        
        internal static readonly ushort[] ud_itab__197 = {
          /* 00 */     INVALID,        1555,     INVALID,        1553,
        };
        
        internal static readonly ushort[] ud_itab__198 = {
          /* 00 */  0x8000|199,     INVALID,         916,  0x8000|200,
        };
        
        internal static readonly ushort[] ud_itab__199 = {
          /* 00 */         872,         873,         913,
        };
        
        internal static readonly ushort[] ud_itab__200 = {
          /* 00 */         874,         876,         914,
        };
        
        internal static readonly ushort[] ud_itab__201 = {
          /* 00 */         921,     INVALID,        1524,        1515,
        };
        
        internal static readonly ushort[] ud_itab__202 = {
          /* 00 */     INVALID,  0x8000|203,
        };
        
        internal static readonly ushort[] ud_itab__203 = {
          /* 00 */  0x8000|204,  0x8000|205,  0x8000|206,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__204 = {
          /* 00 */         825,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__205 = {
          /* 00 */        1509,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__206 = {
          /* 00 */        1510,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__207 = {
          /* 00 */     INVALID,  0x8000|208,
        };
        
        internal static readonly ushort[] ud_itab__208 = {
          /* 00 */  0x8000|209,  0x8000|210,  0x8000|211,  0x8000|212,
          /* 04 */  0x8000|213,  0x8000|214,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__209 = {
          /* 00 */        1511,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__210 = {
          /* 00 */        1501,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__211 = {
          /* 00 */        1502,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__212 = {
          /* 00 */        1503,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__213 = {
          /* 00 */        1504,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__214 = {
          /* 00 */        1505,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__215 = {
          /* 00 */  0x8000|216,  0x8000|217,
        };
        
        internal static readonly ushort[] ud_itab__216 = {
          /* 00 */         683,         682,         768,        1400,
          /* 04 */        1507,        1506,     INVALID,          79,
        };
        
        internal static readonly ushort[] ud_itab__217 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,  0x8000|218,  0x8000|219,  0x8000|220,
        };
        
        internal static readonly ushort[] ud_itab__218 = {
          /* 00 */         777,         778,         779,         780,
          /* 04 */         781,         782,         783,         784,
        };
        
        internal static readonly ushort[] ud_itab__219 = {
          /* 00 */         808,         809,         810,         811,
          /* 04 */         812,         813,         814,         815,
        };
        
        internal static readonly ushort[] ud_itab__220 = {
          /* 00 */        1366,        1367,        1368,        1369,
          /* 04 */        1370,        1371,        1372,        1373,
        };
        
        internal static readonly ushort[] ud_itab__221 = {
          /* 00 */     INVALID,     INVALID,        1710,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__222 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */        1669,        1676,        1674,        1672,
        };
        
        internal static readonly ushort[] ud_itab__223 = {
          /* 00 */         112,         117,         120,         110,
        };
        
        internal static readonly ushort[] ud_itab__224 = {
          /* 00 */        1059,     INVALID,     INVALID,        1060,
        };
        
        internal static readonly ushort[] ud_itab__225 = {
          /* 00 */        1055,     INVALID,     INVALID,        1053,
        };
        
        internal static readonly ushort[] ud_itab__226 = {
          /* 00 */        1381,     INVALID,     INVALID,        1379,
        };
        
        internal static readonly ushort[] ud_itab__227 = {
          /* 00 */  0x8000|228,  0x8000|235,
        };
        
        internal static readonly ushort[] ud_itab__228 = {
          /* 00 */     INVALID,  0x8000|229,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,  0x8000|230,  0x8000|234,
        };
        
        internal static readonly ushort[] ud_itab__229 = {
          /* 00 */         124,         125,         126,
        };
        
        internal static readonly ushort[] ud_itab__230 = {
          /* 00 */  0x8000|231,     INVALID,  0x8000|232,  0x8000|233,
        };
        
        internal static readonly ushort[] ud_itab__231 = {
          /* 00 */     INVALID,        1459,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__232 = {
          /* 00 */     INVALID,        1458,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__233 = {
          /* 00 */     INVALID,        1457,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__234 = {
          /* 00 */     INVALID,        1460,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__235 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,        1456,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__236 = {
          /* 00 */     INVALID,          35,     INVALID,          33,
        };
        
        internal static readonly ushort[] ud_itab__237 = {
          /* 00 */        1160,     INVALID,     INVALID,        1161,
        };
        
        internal static readonly ushort[] ud_itab__238 = {
          /* 00 */        1166,     INVALID,     INVALID,        1167,
        };
        
        internal static readonly ushort[] ud_itab__239 = {
          /* 00 */        1172,     INVALID,     INVALID,        1173,
        };
        
        internal static readonly ushort[] ud_itab__240 = {
          /* 00 */        1527,     INVALID,     INVALID,        1528,
        };
        
        internal static readonly ushort[] ud_itab__241 = {
          /* 00 */        1093,     INVALID,     INVALID,        1094,
        };
        
        internal static readonly ushort[] ud_itab__242 = {
          /* 00 */     INVALID,        1521,        1526,         918,
        };
        
        internal static readonly ushort[] ud_itab__243 = {
          /* 00 */        1086,     INVALID,     INVALID,        1084,
        };
        
        internal static readonly ushort[] ud_itab__244 = {
          /* 00 */        1192,     INVALID,     INVALID,        1193,
        };
        
        internal static readonly ushort[] ud_itab__245 = {
          /* 00 */        1195,     INVALID,     INVALID,        1196,
        };
        
        internal static readonly ushort[] ud_itab__246 = {
          /* 00 */        1083,     INVALID,     INVALID,        1081,
        };
        
        internal static readonly ushort[] ud_itab__247 = {
          /* 00 */        1017,     INVALID,     INVALID,        1015,
        };
        
        internal static readonly ushort[] ud_itab__248 = {
          /* 00 */        1009,     INVALID,     INVALID,        1010,
        };
        
        internal static readonly ushort[] ud_itab__249 = {
          /* 00 */        1012,     INVALID,     INVALID,        1013,
        };
        
        internal static readonly ushort[] ud_itab__250 = {
          /* 00 */        1075,     INVALID,     INVALID,        1076,
        };
        
        internal static readonly ushort[] ud_itab__251 = {
          /* 00 */        1020,     INVALID,     INVALID,        1018,
        };
        
        internal static readonly ushort[] ud_itab__252 = {
          /* 00 */        1023,     INVALID,     INVALID,        1021,
        };
        
        internal static readonly ushort[] ud_itab__253 = {
          /* 00 */        1147,     INVALID,     INVALID,        1148,
        };
        
        internal static readonly ushort[] ud_itab__254 = {
          /* 00 */        1156,     INVALID,     INVALID,        1154,
        };
        
        internal static readonly ushort[] ud_itab__255 = {
          /* 00 */        1026,     INVALID,     INVALID,        1024,
        };
        
        internal static readonly ushort[] ud_itab__256 = {
          /* 00 */        1087,     INVALID,     INVALID,        1088,
        };
        
        internal static readonly ushort[] ud_itab__257 = {
          /* 00 */        1092,     INVALID,     INVALID,        1090,
        };
        
        internal static readonly ushort[] ud_itab__258 = {
          /* 00 */     INVALID,         136,         132,         160,
        };
        
        internal static readonly ushort[] ud_itab__259 = {
          /* 00 */         909,     INVALID,     INVALID,         902,
        };
        
        internal static readonly ushort[] ud_itab__260 = {
          /* 00 */        1186,     INVALID,     INVALID,        1187,
        };
        
        internal static readonly ushort[] ud_itab__261 = {
          /* 00 */        1189,     INVALID,     INVALID,        1190,
        };
        
        internal static readonly ushort[] ud_itab__262 = {
          /* 00 */        1080,     INVALID,     INVALID,        1078,
        };
        
        internal static readonly ushort[] ud_itab__263 = {
          /* 00 */        1118,     INVALID,     INVALID,        1116,
        };
        
        internal static readonly ushort[] ud_itab__264 = {
          /* 00 */        1003,     INVALID,     INVALID,        1004,
        };
        
        internal static readonly ushort[] ud_itab__265 = {
          /* 00 */        1006,     INVALID,     INVALID,        1007,
        };
        
        internal static readonly ushort[] ud_itab__266 = {
          /* 00 */        1074,     INVALID,     INVALID,        1072,
        };
        
        internal static readonly ushort[] ud_itab__267 = {
          /* 00 */        1266,     INVALID,     INVALID,        1264,
        };
        
        internal static readonly ushort[] ud_itab__268 = {
          /* 00 */     INVALID,        1559,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__269 = {
          /* 00 */        1136,     INVALID,     INVALID,        1135,
        };
        
        internal static readonly ushort[] ud_itab__270 = {
          /* 00 */        1140,     INVALID,     INVALID,        1139,
        };
        
        internal static readonly ushort[] ud_itab__271 = {
          /* 00 */        1144,     INVALID,     INVALID,        1143,
        };
        
        internal static readonly ushort[] ud_itab__272 = {
          /* 00 */        1533,     INVALID,     INVALID,        1534,
        };
        
        internal static readonly ushort[] ud_itab__273 = {
          /* 00 */        1069,     INVALID,     INVALID,        1070,
        };
        
        internal static readonly ushort[] ud_itab__274 = {
          /* 00 */        1133,     INVALID,     INVALID,        1131,
        };
        
        internal static readonly ushort[] ud_itab__275 = {
          /* 00 */     INVALID,  0x8000|276,
        };
        
        internal static readonly ushort[] ud_itab__276 = {
          /* 00 */         799,     INVALID,     INVALID,        1519,
        };
        
        internal static readonly ushort[] ud_itab__277 = {
          /* 00 */        1179,     INVALID,     INVALID,        1177,
        };
        
        internal static readonly ushort[] ud_itab__278 = {
          /* 00 */        1182,     INVALID,     INVALID,        1180,
        };
        
        internal static readonly ushort[] ud_itab__279 = {
          /* 00 */        1183,     INVALID,     INVALID,        1184,
        };
        
        internal static readonly ushort[] ud_itab__280 = {
          /* 00 */        1532,     INVALID,     INVALID,        1530,
        };
        
        internal static readonly ushort[] ud_itab__281 = {
          /* 00 */         996,     INVALID,     INVALID,         994,
        };
        
        internal static readonly ushort[] ud_itab__282 = {
          /* 00 */         997,     INVALID,     INVALID,         998,
        };
        
        internal static readonly ushort[] ud_itab__283 = {
          /* 00 */        1000,     INVALID,     INVALID,        1001,
        };
        
        internal static readonly ushort[] ud_itab__284 = {
          /* 00 */        1242,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__285 = {
          /* 00 */        1097,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__286 = {
          /* 00 */        1243,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__287 = {
          /* 00 */        1098,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__288 = {
          /* 00 */         173,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__289 = {
          /* 00 */         174,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__290 = {
          /* 00 */           1,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__291 = {
          /* 00 */           4,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__292 = {
          /* 00 */  0x8000|293,  0x8000|294,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__293 = {
          /* 00 */        1257,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__294 = {
          /* 00 */        1258,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__295 = {
          /* 00 */  0x8000|296,  0x8000|297,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__296 = {
          /* 00 */        1110,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__297 = {
          /* 00 */        1111,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__298 = {
          /* 00 */        1658,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__299 = {
          /* 00 */          67,          68,
        };
        
        internal static readonly ushort[] ud_itab__300 = {
          /* 00 */         710,         711,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__301 = {
          /* 00 */         983,         984,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__302 = {
          /* 00 */          21,         970,          11,        1342,
          /* 04 */          55,        1413,        1493,         106,
        };
        
        internal static readonly ushort[] ud_itab__303 = {
          /* 00 */          23,         971,          13,        1343,
          /* 04 */          57,        1414,        1494,         108,
        };
        
        internal static readonly ushort[] ud_itab__304 = {
          /* 00 */  0x8000|305,  0x8000|306,  0x8000|307,  0x8000|308,
          /* 04 */  0x8000|309,  0x8000|310,  0x8000|311,  0x8000|312,
        };
        
        internal static readonly ushort[] ud_itab__305 = {
          /* 00 */          22,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__306 = {
          /* 00 */         972,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__307 = {
          /* 00 */          12,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__308 = {
          /* 00 */        1344,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__309 = {
          /* 00 */          56,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__310 = {
          /* 00 */        1415,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__311 = {
          /* 00 */        1495,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__312 = {
          /* 00 */         107,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__313 = {
          /* 00 */          24,         973,          14,        1345,
          /* 04 */          58,        1416,        1496,         109,
        };
        
        internal static readonly ushort[] ud_itab__314 = {
          /* 00 */        1109,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__315 = {
          /* 00 */          74,          75,          76,
        };
        
        internal static readonly ushort[] ud_itab__316 = {
          /* 00 */         170,         171,         172,
        };
        
        internal static readonly ushort[] ud_itab__317 = {
          /* 00 */          73,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__318 = {
          /* 00 */  0x8000|319,  0x8000|320,  0x8000|321,
        };
        
        internal static readonly ushort[] ud_itab__319 = {
          /* 00 */        1259,        1260,
        };
        
        internal static readonly ushort[] ud_itab__320 = {
          /* 00 */        1261,        1262,
        };
        
        internal static readonly ushort[] ud_itab__321 = {
          /* 00 */     INVALID,        1263,
        };
        
        internal static readonly ushort[] ud_itab__322 = {
          /* 00 */  0x8000|323,  0x8000|324,  0x8000|325,
        };
        
        internal static readonly ushort[] ud_itab__323 = {
          /* 00 */        1112,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__324 = {
          /* 00 */        1113,        1114,
        };
        
        internal static readonly ushort[] ud_itab__325 = {
          /* 00 */     INVALID,        1115,
        };
        
        internal static readonly ushort[] ud_itab__326 = {
          /* 00 */         923,         924,         927,
        };
        
        internal static readonly ushort[] ud_itab__327 = {
          /* 00 */         115,         116,         119,
        };
        
        internal static readonly ushort[] ud_itab__328 = {
          /* 00 */        1403,        1404,        1405,
        };
        
        internal static readonly ushort[] ud_itab__329 = {
          /* 00 */         791,         792,         793,
        };
        
        internal static readonly ushort[] ud_itab__330 = {
          /* 00 */        1347,        1348,        1349,
        };
        
        internal static readonly ushort[] ud_itab__331 = {
          /* 00 */        1279,        1286,        1267,        1275,
          /* 04 */        1327,        1334,        1318,        1313,
        };
        
        internal static readonly ushort[] ud_itab__332 = {
          /* 00 */        1284,        1287,        1268,        1274,
          /* 04 */        1323,        1330,        1319,        1315,
        };
        
        internal static readonly ushort[] ud_itab__333 = {
          /* 00 */  0x8000|334,  0x8000|335,     INVALID,     INVALID,
          /* 04 */     INVALID,  0x8000|341,  0x8000|357,  0x8000|369,
          /* 08 */     INVALID,  0x8000|394,     INVALID,     INVALID,
          /* 0c */     INVALID,  0x8000|399,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__334 = {
          /* 00 */         771,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__335 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 10 */         937,         939,  0x8000|336,         895,
          /* 14 */        1450,        1448,  0x8000|337,         885,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */         863,         865,     INVALID,         908,
          /* 2c */     INVALID,     INVALID,        1443,         130,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */         901,        1388,        1307,        1292,
          /* 54 */          62,          66,         977,        1500,
          /* 58 */          28,         947,         146,         135,
          /* 5c */        1420,         819,         190,         803,
          /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,  0x8000|338,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,  0x8000|339,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,         113,     INVALID,
          /* c4 */     INVALID,     INVALID,        1382,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* dc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__336 = {
          /* 00 */         893,         897,
        };
        
        internal static readonly ushort[] ud_itab__337 = {
          /* 00 */         883,         887,
        };
        
        internal static readonly ushort[] ud_itab__338 = {
          /* 00 */        1742,        1743,
        };
        
        internal static readonly ushort[] ud_itab__339 = {
          /* 00 */  0x8000|340,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__340 = {
          /* 00 */     INVALID,     INVALID,     INVALID,        1401,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__341 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 10 */         933,         935,  0x8000|342,         891,
          /* 14 */        1452,        1446,  0x8000|343,         881,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */         859,         861,     INVALID,         906,
          /* 2c */     INVALID,     INVALID,        1441,         128,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */         899,        1390,     INVALID,     INVALID,
          /* 54 */          60,          64,         975,        1498,
          /* 58 */          26,         945,         140,         144,
          /* 5c */        1418,         817,         188,         801,
          /* 60 */        1208,        1211,        1214,         986,
          /* 64 */        1037,        1040,        1043,         992,
          /* 68 */        1199,        1202,        1205,         989,
          /* 6c */        1548,        1546,  0x8000|344,        1518,
          /* 70 */        1540,  0x8000|345,  0x8000|347,  0x8000|349,
          /* 74 */        1029,        1032,        1035,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */        1550,        1554,  0x8000|351,        1516,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,         111,     INVALID,
          /* c4 */        1061,        1054,        1380,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */          34,        1162,        1168,        1174,
          /* d4 */        1529,        1095,         919,  0x8000|352,
          /* d8 */        1194,        1197,        1082,        1016,
          /* dc */        1011,        1014,        1077,        1019,
          /* e0 */        1022,        1149,        1155,        1025,
          /* e4 */        1089,        1091,         161,         903,
          /* e8 */        1188,        1191,        1079,        1117,
          /* ec */        1005,        1008,        1073,        1265,
          /* f0 */     INVALID,  0x8000|353,  0x8000|354,  0x8000|355,
          /* f4 */     INVALID,        1071,        1132,  0x8000|356,
          /* f8 */        1178,        1181,        1185,        1531,
          /* fc */         995,         999,        1002,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__342 = {
          /* 00 */         889,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__343 = {
          /* 00 */         879,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__344 = {
          /* 00 */         869,         871,         912,
        };
        
        internal static readonly ushort[] ud_itab__345 = {
          /* 00 */     INVALID,     INVALID,        1164,     INVALID,
          /* 04 */        1151,     INVALID,  0x8000|346,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__346 = {
          /* 00 */        1756,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__347 = {
          /* 00 */     INVALID,     INVALID,        1170,     INVALID,
          /* 04 */        1158,     INVALID,  0x8000|348,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__348 = {
          /* 00 */        1758,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__349 = {
          /* 00 */     INVALID,     INVALID,        1176,        1544,
          /* 04 */     INVALID,     INVALID,  0x8000|350,        1542,
        };
        
        internal static readonly ushort[] ud_itab__350 = {
          /* 00 */        1760,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__351 = {
          /* 00 */         875,         877,         915,
        };
        
        internal static readonly ushort[] ud_itab__352 = {
          /* 00 */        1085,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__353 = {
          /* 00 */        1755,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__354 = {
          /* 00 */        1757,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__355 = {
          /* 00 */        1759,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__356 = {
          /* 00 */     INVALID,        1520,
        };
        
        internal static readonly ushort[] ud_itab__357 = {
          /* 00 */        1584,        1587,        1590,        1593,
          /* 04 */        1596,        1599,        1602,        1605,
          /* 08 */        1608,        1614,        1611,        1617,
          /* 0c */  0x8000|358,  0x8000|359,  0x8000|360,  0x8000|361,
          /* 10 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 14 */     INVALID,     INVALID,     INVALID,        1712,
          /* 18 */  0x8000|362,  0x8000|363,     INVALID,     INVALID,
          /* 1c */        1575,        1578,        1581,     INVALID,
          /* 20 */        1686,        1688,        1690,        1692,
          /* 24 */        1694,     INVALID,     INVALID,     INVALID,
          /* 28 */        1623,        1709,        1682,        1684,
          /* 2c */  0x8000|365,  0x8000|366,  0x8000|367,  0x8000|368,
          /* 30 */        1697,        1699,        1701,        1703,
          /* 34 */        1705,        1707,     INVALID,        1718,
          /* 38 */        1625,        1627,        1629,        1631,
          /* 3c */        1633,        1635,        1639,        1637,
          /* 40 */        1641,        1643,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,          46,
          /* dc */          42,          44,          38,          40,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__358 = {
          /* 00 */        1737,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__359 = {
          /* 00 */        1735,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__360 = {
          /* 00 */        1740,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__361 = {
          /* 00 */        1741,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__362 = {
          /* 00 */        1727,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__363 = {
          /* 00 */  0x8000|364,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__364 = {
          /* 00 */     INVALID,        1728,
        };
        
        internal static readonly ushort[] ud_itab__365 = {
          /* 00 */        1731,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__366 = {
          /* 00 */        1733,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__367 = {
          /* 00 */        1732,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__368 = {
          /* 00 */        1734,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__369 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */  0x8000|370,  0x8000|371,  0x8000|372,     INVALID,
          /* 08 */        1645,        1647,        1649,        1651,
          /* 0c */        1655,        1653,        1678,        1620,
          /* 10 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 14 */  0x8000|374,        1057,  0x8000|375,         202,
          /* 18 */  0x8000|379,  0x8000|381,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */  0x8000|383,        1558,  0x8000|385,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */         198,         196,        1680,     INVALID,
          /* 44 */        1513,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,  0x8000|391,  0x8000|392,
          /* 4c */  0x8000|393,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 60 */        1716,        1714,        1722,        1720,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* dc */     INVALID,     INVALID,     INVALID,          48,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__370 = {
          /* 00 */        1738,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__371 = {
          /* 00 */        1736,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__372 = {
          /* 00 */  0x8000|373,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__373 = {
          /* 00 */     INVALID,        1739,
        };
        
        internal static readonly ushort[] ud_itab__374 = {
          /* 00 */        1046,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__375 = {
          /* 00 */  0x8000|376,  0x8000|377,  0x8000|378,
        };
        
        internal static readonly ushort[] ud_itab__376 = {
          /* 00 */        1048,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__377 = {
          /* 00 */        1050,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__378 = {
          /* 00 */     INVALID,        1052,
        };
        
        internal static readonly ushort[] ud_itab__379 = {
          /* 00 */  0x8000|380,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__380 = {
          /* 00 */     INVALID,        1730,
        };
        
        internal static readonly ushort[] ud_itab__381 = {
          /* 00 */  0x8000|382,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__382 = {
          /* 00 */     INVALID,        1729,
        };
        
        internal static readonly ushort[] ud_itab__383 = {
          /* 00 */  0x8000|384,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__384 = {
          /* 00 */        1065,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__385 = {
          /* 00 */  0x8000|386,  0x8000|388,
        };
        
        internal static readonly ushort[] ud_itab__386 = {
          /* 00 */  0x8000|387,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__387 = {
          /* 00 */        1066,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__388 = {
          /* 00 */  0x8000|389,  0x8000|390,
        };
        
        internal static readonly ushort[] ud_itab__389 = {
          /* 00 */        1067,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__390 = {
          /* 00 */        1068,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__391 = {
          /* 00 */        1745,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__392 = {
          /* 00 */        1744,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__393 = {
          /* 00 */        1754,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__394 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 10 */  0x8000|395,  0x8000|396,  0x8000|397,     INVALID,
          /* 14 */     INVALID,     INVALID,  0x8000|398,     INVALID,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */     INVALID,     INVALID,         155,     INVALID,
          /* 2c */         169,         159,     INVALID,     INVALID,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,        1394,        1309,        1294,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */          32,         951,         157,         164,
          /* 5c */        1424,         823,         194,         807,
          /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,        1523,
          /* 70 */        1536,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */     INVALID,     INVALID,         917,        1525,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,         121,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* dc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,         133,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__395 = {
          /* 00 */        1751,        1750,
        };
        
        internal static readonly ushort[] ud_itab__396 = {
          /* 00 */        1753,        1752,
        };
        
        internal static readonly ushort[] ud_itab__397 = {
          /* 00 */        1572,        1570,
        };
        
        internal static readonly ushort[] ud_itab__398 = {
          /* 00 */        1568,        1566,
        };
        
        internal static readonly ushort[] ud_itab__399 = {
          /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 10 */  0x8000|400,  0x8000|401,  0x8000|402,     INVALID,
          /* 14 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */     INVALID,     INVALID,         153,     INVALID,
          /* 2c */         167,         149,     INVALID,     INVALID,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 50 */     INVALID,        1392,     INVALID,     INVALID,
          /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 58 */          30,         949,         151,     INVALID,
          /* 5c */        1422,         821,         192,         805,
          /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 70 */        1538,     INVALID,     INVALID,     INVALID,
          /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 7c */        1552,        1556,     INVALID,     INVALID,
          /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c0 */     INVALID,     INVALID,         118,     INVALID,
          /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d0 */          36,     INVALID,     INVALID,     INVALID,
          /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* dc */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* e4 */     INVALID,     INVALID,         137,     INVALID,
          /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f0 */        1560,     INVALID,     INVALID,     INVALID,
          /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__400 = {
          /* 00 */        1747,        1746,
        };
        
        internal static readonly ushort[] ud_itab__401 = {
          /* 00 */        1749,        1748,
        };
        
        internal static readonly ushort[] ud_itab__402 = {
          /* 00 */        1564,        1562,
        };
        
        internal static readonly ushort[] ud_itab__403 = {
          /* 00 */  0x8000|404,  0x8000|335,     INVALID,     INVALID,
          /* 04 */     INVALID,  0x8000|341,  0x8000|357,  0x8000|369,
          /* 08 */     INVALID,  0x8000|394,     INVALID,     INVALID,
          /* 0c */     INVALID,  0x8000|399,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__404 = {
          /* 00 */         769,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__405 = {
          /* 00 */         826,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__406 = {
          /* 00 */         827,     INVALID,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__407 = {
          /* 00 */         715,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__408 = {
          /* 00 */         723,         724,         725,
        };
        
        internal static readonly ushort[] ud_itab__409 = {
          /* 00 */        1280,        1285,        1269,        1273,
          /* 04 */        1326,        1333,        1320,        1314,
        };
        
        internal static readonly ushort[] ud_itab__410 = {
          /* 00 */        1281,        1288,        1272,        1276,
          /* 04 */        1325,        1332,        1329,        1312,
        };
        
        internal static readonly ushort[] ud_itab__411 = {
          /* 00 */        1282,        1289,        1270,        1277,
          /* 04 */        1324,        1331,        1321,        1316,
        };
        
        internal static readonly ushort[] ud_itab__412 = {
          /* 00 */        1283,        1290,        1271,        1278,
          /* 04 */        1328,        1335,        1322,        1317,
        };
        
        internal static readonly ushort[] ud_itab__413 = {
          /* 00 */           3,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__414 = {
          /* 00 */           2,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__415 = {
          /* 00 */        1311,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__416 = {
          /* 00 */  0x8000|417,  0x8000|418,
        };
        
        internal static readonly ushort[] ud_itab__417 = {
          /* 00 */         206,         503,         307,         357,
          /* 04 */         587,         630,         387,         413,
        };
        
        internal static readonly ushort[] ud_itab__418 = {
          /* 00 */         215,         216,         217,         218,
          /* 04 */         219,         220,         221,         222,
          /* 08 */         504,         505,         506,         507,
          /* 0c */         508,         509,         510,         511,
          /* 10 */         309,         310,         311,         312,
          /* 14 */         313,         314,         315,         316,
          /* 18 */         359,         360,         361,         362,
          /* 1c */         363,         364,         365,         366,
          /* 20 */         589,         590,         591,         592,
          /* 24 */         593,         594,         595,         596,
          /* 28 */         614,         615,         616,         617,
          /* 2c */         618,         619,         620,         621,
          /* 30 */         388,         389,         390,         391,
          /* 34 */         392,         393,         394,         395,
          /* 38 */         414,         415,         416,         417,
          /* 3c */         418,         419,         420,         421,
        };
        
        internal static readonly ushort[] ud_itab__419 = {
          /* 00 */  0x8000|420,  0x8000|421,
        };
        
        internal static readonly ushort[] ud_itab__420 = {
          /* 00 */         476,     INVALID,         573,         540,
          /* 04 */         493,         492,         584,         583,
        };
        
        internal static readonly ushort[] ud_itab__421 = {
          /* 00 */         477,         478,         479,         480,
          /* 04 */         481,         482,         483,         484,
          /* 08 */         658,         659,         660,         661,
          /* 0c */         662,         663,         664,         665,
          /* 10 */         522,     INVALID,     INVALID,     INVALID,
          /* 14 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 18 */         549,         550,         551,         552,
          /* 1c */         553,         554,         555,         556,
          /* 20 */         233,         204,     INVALID,     INVALID,
          /* 24 */         639,         657,     INVALID,     INVALID,
          /* 28 */         485,         486,         487,         488,
          /* 2c */         489,         490,         491,     INVALID,
          /* 30 */         203,         685,         529,         526,
          /* 34 */         684,         528,         377,         454,
          /* 38 */         527,         686,         537,         536,
          /* 3c */         530,         534,         535,         376,
        };
        
        internal static readonly ushort[] ud_itab__422 = {
          /* 00 */  0x8000|423,  0x8000|424,
        };
        
        internal static readonly ushort[] ud_itab__423 = {
          /* 00 */         456,         520,         448,         450,
          /* 04 */         462,         464,         460,         458,
        };
        
        internal static readonly ushort[] ud_itab__424 = {
          /* 00 */         235,         236,         237,         238,
          /* 04 */         239,         240,         241,         242,
          /* 08 */         243,         244,         245,         246,
          /* 0c */         247,         248,         249,         250,
          /* 10 */         251,         252,         253,         254,
          /* 14 */         255,         256,         257,         258,
          /* 18 */         259,         260,         261,         262,
          /* 1c */         263,         264,         265,         266,
          /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */     INVALID,         656,     INVALID,     INVALID,
          /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__425 = {
          /* 00 */  0x8000|426,  0x8000|427,
        };
        
        internal static readonly ushort[] ud_itab__426 = {
          /* 00 */         453,         471,         467,         470,
          /* 04 */     INVALID,         474,     INVALID,         538,
        };
        
        internal static readonly ushort[] ud_itab__427 = {
          /* 00 */         267,         268,         269,         270,
          /* 04 */         271,         272,         273,         274,
          /* 08 */         275,         276,         277,         278,
          /* 0c */         279,         280,         281,         282,
          /* 10 */         283,         284,         285,         286,
          /* 14 */         287,         288,         289,         290,
          /* 18 */         291,         292,         293,         294,
          /* 1c */         295,         296,         297,         298,
          /* 20 */         524,         523,         234,         455,
          /* 24 */         525,         532,     INVALID,     INVALID,
          /* 28 */         299,         300,         301,         302,
          /* 2c */         303,         304,         305,         306,
          /* 30 */         333,         334,         335,         336,
          /* 34 */         337,         338,         339,         340,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__428 = {
          /* 00 */  0x8000|429,  0x8000|430,
        };
        
        internal static readonly ushort[] ud_itab__429 = {
          /* 00 */         205,         494,         308,         358,
          /* 04 */         588,         613,         378,         404,
        };
        
        internal static readonly ushort[] ud_itab__430 = {
          /* 00 */         207,         208,         209,         210,
          /* 04 */         211,         212,         213,         214,
          /* 08 */         495,         496,         497,         498,
          /* 0c */         499,         500,         501,         502,
          /* 10 */         317,         318,         319,         320,
          /* 14 */         321,         322,         323,         324,
          /* 18 */         325,         326,         327,         328,
          /* 1c */         329,         330,         331,         332,
          /* 20 */         622,         623,         624,         625,
          /* 24 */         626,         627,         628,         629,
          /* 28 */         597,         598,         599,         600,
          /* 2c */         601,         602,         603,         604,
          /* 30 */         405,         406,         407,         408,
          /* 34 */         409,         410,         411,         412,
          /* 38 */         379,         380,         381,         382,
          /* 3c */         383,         384,         385,         386,
        };
        
        internal static readonly ushort[] ud_itab__431 = {
          /* 00 */  0x8000|432,  0x8000|433,
        };
        
        internal static readonly ushort[] ud_itab__432 = {
          /* 00 */         475,         472,         574,         539,
          /* 04 */         531,     INVALID,         533,         585,
        };
        
        internal static readonly ushort[] ud_itab__433 = {
          /* 00 */         431,         432,         433,         434,
          /* 04 */         435,         436,         437,         438,
          /* 08 */         666,         667,         668,         669,
          /* 0c */         670,         671,         672,         673,
          /* 10 */         575,         576,         577,         578,
          /* 14 */         579,         580,         581,         582,
          /* 18 */         541,         542,         543,         544,
          /* 1c */         545,         546,         547,         548,
          /* 20 */         640,         641,         642,         643,
          /* 24 */         644,         645,         646,         647,
          /* 28 */         648,         649,         650,         651,
          /* 2c */         652,         653,         654,         655,
          /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__434 = {
          /* 00 */  0x8000|435,  0x8000|436,
        };
        
        internal static readonly ushort[] ud_itab__435 = {
          /* 00 */         457,         521,         447,         449,
          /* 04 */         463,         465,         461,         459,
        };
        
        internal static readonly ushort[] ud_itab__436 = {
          /* 00 */         223,         224,         225,         226,
          /* 04 */         227,         228,         229,         230,
          /* 08 */         512,         513,         514,         515,
          /* 0c */         516,         517,         518,         519,
          /* 10 */         367,         368,         369,         370,
          /* 14 */         371,         372,         373,         374,
          /* 18 */     INVALID,         375,     INVALID,     INVALID,
          /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 20 */         631,         632,         633,         634,
          /* 24 */         635,         636,         637,         638,
          /* 28 */         605,         606,         607,         608,
          /* 2c */         609,         610,         611,         612,
          /* 30 */         422,         423,         424,         425,
          /* 34 */         426,         427,         428,         429,
          /* 38 */         396,         397,         398,         399,
          /* 3c */         400,         401,         402,         403,
        };
        
        internal static readonly ushort[] ud_itab__437 = {
          /* 00 */  0x8000|438,  0x8000|439,
        };
        
        internal static readonly ushort[] ud_itab__438 = {
          /* 00 */         451,         473,         466,         468,
          /* 04 */         231,         452,         232,         469,
        };
        
        internal static readonly ushort[] ud_itab__439 = {
          /* 00 */         439,         440,         441,         442,
          /* 04 */         443,         444,         445,         446,
          /* 08 */         674,         675,         676,         677,
          /* 0c */         678,         679,         680,         681,
          /* 10 */         557,         558,         559,         560,
          /* 14 */         561,         562,         563,         564,
          /* 18 */         565,         566,         567,         568,
          /* 1c */         569,         570,         571,         572,
          /* 20 */         586,     INVALID,     INVALID,     INVALID,
          /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 28 */         341,         342,         343,         344,
          /* 2c */         345,         346,         347,         348,
          /* 30 */         349,         350,         351,         352,
          /* 34 */         353,         354,         355,         356,
          /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
          /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__440 = {
          /* 00 */         758,         759,         760,
        };
        
        internal static readonly ushort[] ud_itab__441 = {
          /* 00 */         764,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__442 = {
          /* 00 */        1432,        1437,         962,         953,
          /* 04 */         942,         695,         186,         689,
        };
        
        internal static readonly ushort[] ud_itab__443 = {
          /* 00 */        1438,        1439,         963,         954,
          /* 04 */         943,         696,         185,         688,
        };
        
        internal static readonly ushort[] ud_itab__444 = {
          /* 00 */         708,         183,     INVALID,     INVALID,
          /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__445 = {
          /* 00 */         707,         184,  0x8000|446,          71,
          /* 04 */         761,         762,        1255,     INVALID,
        };
        
        internal static readonly ushort[] ud_itab__446 = {
          /* 00 */          69,          70,
        };
        #endregion
        
        #region Lookup Table List
        
        internal static readonly ud_lookup_table_list_entry[] ud_lookup_table_list = new ud_lookup_table_list_entry[] {
            /* 000 */ new ud_lookup_table_list_entry( ud_itab__0, ud_table_type.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 001 */ new ud_lookup_table_list_entry( ud_itab__1, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 002 */ new ud_lookup_table_list_entry( ud_itab__2, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 003 */ new ud_lookup_table_list_entry( ud_itab__3, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 004 */ new ud_lookup_table_list_entry( ud_itab__4, ud_table_type.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 005 */ new ud_lookup_table_list_entry( ud_itab__5, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 006 */ new ud_lookup_table_list_entry( ud_itab__6, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 007 */ new ud_lookup_table_list_entry( ud_itab__7, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 008 */ new ud_lookup_table_list_entry( ud_itab__8, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 009 */ new ud_lookup_table_list_entry( ud_itab__9, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 010 */ new ud_lookup_table_list_entry( ud_itab__10, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 011 */ new ud_lookup_table_list_entry( ud_itab__11, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 012 */ new ud_lookup_table_list_entry( ud_itab__12, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 013 */ new ud_lookup_table_list_entry( ud_itab__13, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 014 */ new ud_lookup_table_list_entry( ud_itab__14, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 015 */ new ud_lookup_table_list_entry( ud_itab__15, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 016 */ new ud_lookup_table_list_entry( ud_itab__16, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 017 */ new ud_lookup_table_list_entry( ud_itab__17, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 018 */ new ud_lookup_table_list_entry( ud_itab__18, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 019 */ new ud_lookup_table_list_entry( ud_itab__19, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 020 */ new ud_lookup_table_list_entry( ud_itab__20, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 021 */ new ud_lookup_table_list_entry( ud_itab__21, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 022 */ new ud_lookup_table_list_entry( ud_itab__22, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 023 */ new ud_lookup_table_list_entry( ud_itab__23, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 024 */ new ud_lookup_table_list_entry( ud_itab__24, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 025 */ new ud_lookup_table_list_entry( ud_itab__25, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 026 */ new ud_lookup_table_list_entry( ud_itab__26, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 027 */ new ud_lookup_table_list_entry( ud_itab__27, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 028 */ new ud_lookup_table_list_entry( ud_itab__28, ud_table_type.UD_TAB__OPC_3DNOW, "/3dnow" ),
            /* 029 */ new ud_lookup_table_list_entry( ud_itab__29, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 030 */ new ud_lookup_table_list_entry( ud_itab__30, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 031 */ new ud_lookup_table_list_entry( ud_itab__31, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 032 */ new ud_lookup_table_list_entry( ud_itab__32, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 033 */ new ud_lookup_table_list_entry( ud_itab__33, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 034 */ new ud_lookup_table_list_entry( ud_itab__34, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 035 */ new ud_lookup_table_list_entry( ud_itab__35, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 036 */ new ud_lookup_table_list_entry( ud_itab__36, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 037 */ new ud_lookup_table_list_entry( ud_itab__37, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 038 */ new ud_lookup_table_list_entry( ud_itab__38, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 039 */ new ud_lookup_table_list_entry( ud_itab__39, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 040 */ new ud_lookup_table_list_entry( ud_itab__40, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 041 */ new ud_lookup_table_list_entry( ud_itab__41, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 042 */ new ud_lookup_table_list_entry( ud_itab__42, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 043 */ new ud_lookup_table_list_entry( ud_itab__43, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 044 */ new ud_lookup_table_list_entry( ud_itab__44, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 045 */ new ud_lookup_table_list_entry( ud_itab__45, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 046 */ new ud_lookup_table_list_entry( ud_itab__46, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 047 */ new ud_lookup_table_list_entry( ud_itab__47, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 048 */ new ud_lookup_table_list_entry( ud_itab__48, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 049 */ new ud_lookup_table_list_entry( ud_itab__49, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 050 */ new ud_lookup_table_list_entry( ud_itab__50, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 051 */ new ud_lookup_table_list_entry( ud_itab__51, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 052 */ new ud_lookup_table_list_entry( ud_itab__52, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 053 */ new ud_lookup_table_list_entry( ud_itab__53, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 054 */ new ud_lookup_table_list_entry( ud_itab__54, ud_table_type.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 055 */ new ud_lookup_table_list_entry( ud_itab__55, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 056 */ new ud_lookup_table_list_entry( ud_itab__56, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 057 */ new ud_lookup_table_list_entry( ud_itab__57, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 058 */ new ud_lookup_table_list_entry( ud_itab__58, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 059 */ new ud_lookup_table_list_entry( ud_itab__59, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 060 */ new ud_lookup_table_list_entry( ud_itab__60, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 061 */ new ud_lookup_table_list_entry( ud_itab__61, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 062 */ new ud_lookup_table_list_entry( ud_itab__62, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 063 */ new ud_lookup_table_list_entry( ud_itab__63, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 064 */ new ud_lookup_table_list_entry( ud_itab__64, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 065 */ new ud_lookup_table_list_entry( ud_itab__65, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 066 */ new ud_lookup_table_list_entry( ud_itab__66, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 067 */ new ud_lookup_table_list_entry( ud_itab__67, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 068 */ new ud_lookup_table_list_entry( ud_itab__68, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 069 */ new ud_lookup_table_list_entry( ud_itab__69, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 070 */ new ud_lookup_table_list_entry( ud_itab__70, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 071 */ new ud_lookup_table_list_entry( ud_itab__71, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 072 */ new ud_lookup_table_list_entry( ud_itab__72, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 073 */ new ud_lookup_table_list_entry( ud_itab__73, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 074 */ new ud_lookup_table_list_entry( ud_itab__74, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 075 */ new ud_lookup_table_list_entry( ud_itab__75, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 076 */ new ud_lookup_table_list_entry( ud_itab__76, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 077 */ new ud_lookup_table_list_entry( ud_itab__77, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 078 */ new ud_lookup_table_list_entry( ud_itab__78, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 079 */ new ud_lookup_table_list_entry( ud_itab__79, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 080 */ new ud_lookup_table_list_entry( ud_itab__80, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 081 */ new ud_lookup_table_list_entry( ud_itab__81, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 082 */ new ud_lookup_table_list_entry( ud_itab__82, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 083 */ new ud_lookup_table_list_entry( ud_itab__83, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 084 */ new ud_lookup_table_list_entry( ud_itab__84, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 085 */ new ud_lookup_table_list_entry( ud_itab__85, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 086 */ new ud_lookup_table_list_entry( ud_itab__86, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 087 */ new ud_lookup_table_list_entry( ud_itab__87, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 088 */ new ud_lookup_table_list_entry( ud_itab__88, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 089 */ new ud_lookup_table_list_entry( ud_itab__89, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 090 */ new ud_lookup_table_list_entry( ud_itab__90, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 091 */ new ud_lookup_table_list_entry( ud_itab__91, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 092 */ new ud_lookup_table_list_entry( ud_itab__92, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 093 */ new ud_lookup_table_list_entry( ud_itab__93, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 094 */ new ud_lookup_table_list_entry( ud_itab__94, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 095 */ new ud_lookup_table_list_entry( ud_itab__95, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 096 */ new ud_lookup_table_list_entry( ud_itab__96, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 097 */ new ud_lookup_table_list_entry( ud_itab__97, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 098 */ new ud_lookup_table_list_entry( ud_itab__98, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 099 */ new ud_lookup_table_list_entry( ud_itab__99, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 100 */ new ud_lookup_table_list_entry( ud_itab__100, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 101 */ new ud_lookup_table_list_entry( ud_itab__101, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 102 */ new ud_lookup_table_list_entry( ud_itab__102, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 103 */ new ud_lookup_table_list_entry( ud_itab__103, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 104 */ new ud_lookup_table_list_entry( ud_itab__104, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 105 */ new ud_lookup_table_list_entry( ud_itab__105, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 106 */ new ud_lookup_table_list_entry( ud_itab__106, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 107 */ new ud_lookup_table_list_entry( ud_itab__107, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 108 */ new ud_lookup_table_list_entry( ud_itab__108, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 109 */ new ud_lookup_table_list_entry( ud_itab__109, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 110 */ new ud_lookup_table_list_entry( ud_itab__110, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 111 */ new ud_lookup_table_list_entry( ud_itab__111, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 112 */ new ud_lookup_table_list_entry( ud_itab__112, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 113 */ new ud_lookup_table_list_entry( ud_itab__113, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 114 */ new ud_lookup_table_list_entry( ud_itab__114, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 115 */ new ud_lookup_table_list_entry( ud_itab__115, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 116 */ new ud_lookup_table_list_entry( ud_itab__116, ud_table_type.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 117 */ new ud_lookup_table_list_entry( ud_itab__117, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 118 */ new ud_lookup_table_list_entry( ud_itab__118, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 119 */ new ud_lookup_table_list_entry( ud_itab__119, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 120 */ new ud_lookup_table_list_entry( ud_itab__120, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 121 */ new ud_lookup_table_list_entry( ud_itab__121, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 122 */ new ud_lookup_table_list_entry( ud_itab__122, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 123 */ new ud_lookup_table_list_entry( ud_itab__123, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 124 */ new ud_lookup_table_list_entry( ud_itab__124, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 125 */ new ud_lookup_table_list_entry( ud_itab__125, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 126 */ new ud_lookup_table_list_entry( ud_itab__126, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 127 */ new ud_lookup_table_list_entry( ud_itab__127, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 128 */ new ud_lookup_table_list_entry( ud_itab__128, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 129 */ new ud_lookup_table_list_entry( ud_itab__129, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 130 */ new ud_lookup_table_list_entry( ud_itab__130, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 131 */ new ud_lookup_table_list_entry( ud_itab__131, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 132 */ new ud_lookup_table_list_entry( ud_itab__132, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 133 */ new ud_lookup_table_list_entry( ud_itab__133, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 134 */ new ud_lookup_table_list_entry( ud_itab__134, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 135 */ new ud_lookup_table_list_entry( ud_itab__135, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 136 */ new ud_lookup_table_list_entry( ud_itab__136, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 137 */ new ud_lookup_table_list_entry( ud_itab__137, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 138 */ new ud_lookup_table_list_entry( ud_itab__138, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 139 */ new ud_lookup_table_list_entry( ud_itab__139, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 140 */ new ud_lookup_table_list_entry( ud_itab__140, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 141 */ new ud_lookup_table_list_entry( ud_itab__141, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 142 */ new ud_lookup_table_list_entry( ud_itab__142, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 143 */ new ud_lookup_table_list_entry( ud_itab__143, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 144 */ new ud_lookup_table_list_entry( ud_itab__144, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 145 */ new ud_lookup_table_list_entry( ud_itab__145, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 146 */ new ud_lookup_table_list_entry( ud_itab__146, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 147 */ new ud_lookup_table_list_entry( ud_itab__147, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 148 */ new ud_lookup_table_list_entry( ud_itab__148, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 149 */ new ud_lookup_table_list_entry( ud_itab__149, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 150 */ new ud_lookup_table_list_entry( ud_itab__150, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 151 */ new ud_lookup_table_list_entry( ud_itab__151, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 152 */ new ud_lookup_table_list_entry( ud_itab__152, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 153 */ new ud_lookup_table_list_entry( ud_itab__153, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 154 */ new ud_lookup_table_list_entry( ud_itab__154, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 155 */ new ud_lookup_table_list_entry( ud_itab__155, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 156 */ new ud_lookup_table_list_entry( ud_itab__156, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 157 */ new ud_lookup_table_list_entry( ud_itab__157, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 158 */ new ud_lookup_table_list_entry( ud_itab__158, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 159 */ new ud_lookup_table_list_entry( ud_itab__159, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 160 */ new ud_lookup_table_list_entry( ud_itab__160, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 161 */ new ud_lookup_table_list_entry( ud_itab__161, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 162 */ new ud_lookup_table_list_entry( ud_itab__162, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 163 */ new ud_lookup_table_list_entry( ud_itab__163, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 164 */ new ud_lookup_table_list_entry( ud_itab__164, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 165 */ new ud_lookup_table_list_entry( ud_itab__165, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 166 */ new ud_lookup_table_list_entry( ud_itab__166, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 167 */ new ud_lookup_table_list_entry( ud_itab__167, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 168 */ new ud_lookup_table_list_entry( ud_itab__168, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 169 */ new ud_lookup_table_list_entry( ud_itab__169, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 170 */ new ud_lookup_table_list_entry( ud_itab__170, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 171 */ new ud_lookup_table_list_entry( ud_itab__171, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 172 */ new ud_lookup_table_list_entry( ud_itab__172, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 173 */ new ud_lookup_table_list_entry( ud_itab__173, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 174 */ new ud_lookup_table_list_entry( ud_itab__174, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 175 */ new ud_lookup_table_list_entry( ud_itab__175, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 176 */ new ud_lookup_table_list_entry( ud_itab__176, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 177 */ new ud_lookup_table_list_entry( ud_itab__177, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 178 */ new ud_lookup_table_list_entry( ud_itab__178, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 179 */ new ud_lookup_table_list_entry( ud_itab__179, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 180 */ new ud_lookup_table_list_entry( ud_itab__180, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 181 */ new ud_lookup_table_list_entry( ud_itab__181, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 182 */ new ud_lookup_table_list_entry( ud_itab__182, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 183 */ new ud_lookup_table_list_entry( ud_itab__183, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 184 */ new ud_lookup_table_list_entry( ud_itab__184, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 185 */ new ud_lookup_table_list_entry( ud_itab__185, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 186 */ new ud_lookup_table_list_entry( ud_itab__186, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 187 */ new ud_lookup_table_list_entry( ud_itab__187, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 188 */ new ud_lookup_table_list_entry( ud_itab__188, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 189 */ new ud_lookup_table_list_entry( ud_itab__189, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 190 */ new ud_lookup_table_list_entry( ud_itab__190, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 191 */ new ud_lookup_table_list_entry( ud_itab__191, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 192 */ new ud_lookup_table_list_entry( ud_itab__192, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 193 */ new ud_lookup_table_list_entry( ud_itab__193, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 194 */ new ud_lookup_table_list_entry( ud_itab__194, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 195 */ new ud_lookup_table_list_entry( ud_itab__195, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 196 */ new ud_lookup_table_list_entry( ud_itab__196, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 197 */ new ud_lookup_table_list_entry( ud_itab__197, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 198 */ new ud_lookup_table_list_entry( ud_itab__198, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 199 */ new ud_lookup_table_list_entry( ud_itab__199, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 200 */ new ud_lookup_table_list_entry( ud_itab__200, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 201 */ new ud_lookup_table_list_entry( ud_itab__201, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 202 */ new ud_lookup_table_list_entry( ud_itab__202, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 203 */ new ud_lookup_table_list_entry( ud_itab__203, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 204 */ new ud_lookup_table_list_entry( ud_itab__204, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 205 */ new ud_lookup_table_list_entry( ud_itab__205, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 206 */ new ud_lookup_table_list_entry( ud_itab__206, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 207 */ new ud_lookup_table_list_entry( ud_itab__207, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 208 */ new ud_lookup_table_list_entry( ud_itab__208, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 209 */ new ud_lookup_table_list_entry( ud_itab__209, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 210 */ new ud_lookup_table_list_entry( ud_itab__210, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 211 */ new ud_lookup_table_list_entry( ud_itab__211, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 212 */ new ud_lookup_table_list_entry( ud_itab__212, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 213 */ new ud_lookup_table_list_entry( ud_itab__213, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 214 */ new ud_lookup_table_list_entry( ud_itab__214, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 215 */ new ud_lookup_table_list_entry( ud_itab__215, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 216 */ new ud_lookup_table_list_entry( ud_itab__216, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 217 */ new ud_lookup_table_list_entry( ud_itab__217, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 218 */ new ud_lookup_table_list_entry( ud_itab__218, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 219 */ new ud_lookup_table_list_entry( ud_itab__219, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 220 */ new ud_lookup_table_list_entry( ud_itab__220, ud_table_type.UD_TAB__OPC_RM, "/rm" ),
            /* 221 */ new ud_lookup_table_list_entry( ud_itab__221, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 222 */ new ud_lookup_table_list_entry( ud_itab__222, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 223 */ new ud_lookup_table_list_entry( ud_itab__223, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 224 */ new ud_lookup_table_list_entry( ud_itab__224, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 225 */ new ud_lookup_table_list_entry( ud_itab__225, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 226 */ new ud_lookup_table_list_entry( ud_itab__226, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 227 */ new ud_lookup_table_list_entry( ud_itab__227, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 228 */ new ud_lookup_table_list_entry( ud_itab__228, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 229 */ new ud_lookup_table_list_entry( ud_itab__229, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 230 */ new ud_lookup_table_list_entry( ud_itab__230, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 231 */ new ud_lookup_table_list_entry( ud_itab__231, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 232 */ new ud_lookup_table_list_entry( ud_itab__232, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 233 */ new ud_lookup_table_list_entry( ud_itab__233, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 234 */ new ud_lookup_table_list_entry( ud_itab__234, ud_table_type.UD_TAB__OPC_VENDOR, "/vendor" ),
            /* 235 */ new ud_lookup_table_list_entry( ud_itab__235, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 236 */ new ud_lookup_table_list_entry( ud_itab__236, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 237 */ new ud_lookup_table_list_entry( ud_itab__237, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 238 */ new ud_lookup_table_list_entry( ud_itab__238, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 239 */ new ud_lookup_table_list_entry( ud_itab__239, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 240 */ new ud_lookup_table_list_entry( ud_itab__240, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 241 */ new ud_lookup_table_list_entry( ud_itab__241, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 242 */ new ud_lookup_table_list_entry( ud_itab__242, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 243 */ new ud_lookup_table_list_entry( ud_itab__243, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 244 */ new ud_lookup_table_list_entry( ud_itab__244, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 245 */ new ud_lookup_table_list_entry( ud_itab__245, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 246 */ new ud_lookup_table_list_entry( ud_itab__246, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 247 */ new ud_lookup_table_list_entry( ud_itab__247, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 248 */ new ud_lookup_table_list_entry( ud_itab__248, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 249 */ new ud_lookup_table_list_entry( ud_itab__249, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 250 */ new ud_lookup_table_list_entry( ud_itab__250, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 251 */ new ud_lookup_table_list_entry( ud_itab__251, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 252 */ new ud_lookup_table_list_entry( ud_itab__252, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 253 */ new ud_lookup_table_list_entry( ud_itab__253, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 254 */ new ud_lookup_table_list_entry( ud_itab__254, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 255 */ new ud_lookup_table_list_entry( ud_itab__255, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 256 */ new ud_lookup_table_list_entry( ud_itab__256, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 257 */ new ud_lookup_table_list_entry( ud_itab__257, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 258 */ new ud_lookup_table_list_entry( ud_itab__258, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 259 */ new ud_lookup_table_list_entry( ud_itab__259, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 260 */ new ud_lookup_table_list_entry( ud_itab__260, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 261 */ new ud_lookup_table_list_entry( ud_itab__261, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 262 */ new ud_lookup_table_list_entry( ud_itab__262, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 263 */ new ud_lookup_table_list_entry( ud_itab__263, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 264 */ new ud_lookup_table_list_entry( ud_itab__264, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 265 */ new ud_lookup_table_list_entry( ud_itab__265, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 266 */ new ud_lookup_table_list_entry( ud_itab__266, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 267 */ new ud_lookup_table_list_entry( ud_itab__267, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 268 */ new ud_lookup_table_list_entry( ud_itab__268, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 269 */ new ud_lookup_table_list_entry( ud_itab__269, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 270 */ new ud_lookup_table_list_entry( ud_itab__270, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 271 */ new ud_lookup_table_list_entry( ud_itab__271, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 272 */ new ud_lookup_table_list_entry( ud_itab__272, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 273 */ new ud_lookup_table_list_entry( ud_itab__273, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 274 */ new ud_lookup_table_list_entry( ud_itab__274, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 275 */ new ud_lookup_table_list_entry( ud_itab__275, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 276 */ new ud_lookup_table_list_entry( ud_itab__276, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 277 */ new ud_lookup_table_list_entry( ud_itab__277, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 278 */ new ud_lookup_table_list_entry( ud_itab__278, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 279 */ new ud_lookup_table_list_entry( ud_itab__279, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 280 */ new ud_lookup_table_list_entry( ud_itab__280, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 281 */ new ud_lookup_table_list_entry( ud_itab__281, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 282 */ new ud_lookup_table_list_entry( ud_itab__282, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 283 */ new ud_lookup_table_list_entry( ud_itab__283, ud_table_type.UD_TAB__OPC_SSE, "/sse" ),
            /* 284 */ new ud_lookup_table_list_entry( ud_itab__284, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 285 */ new ud_lookup_table_list_entry( ud_itab__285, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 286 */ new ud_lookup_table_list_entry( ud_itab__286, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 287 */ new ud_lookup_table_list_entry( ud_itab__287, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 288 */ new ud_lookup_table_list_entry( ud_itab__288, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 289 */ new ud_lookup_table_list_entry( ud_itab__289, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 290 */ new ud_lookup_table_list_entry( ud_itab__290, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 291 */ new ud_lookup_table_list_entry( ud_itab__291, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 292 */ new ud_lookup_table_list_entry( ud_itab__292, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 293 */ new ud_lookup_table_list_entry( ud_itab__293, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 294 */ new ud_lookup_table_list_entry( ud_itab__294, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 295 */ new ud_lookup_table_list_entry( ud_itab__295, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 296 */ new ud_lookup_table_list_entry( ud_itab__296, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 297 */ new ud_lookup_table_list_entry( ud_itab__297, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 298 */ new ud_lookup_table_list_entry( ud_itab__298, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 299 */ new ud_lookup_table_list_entry( ud_itab__299, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 300 */ new ud_lookup_table_list_entry( ud_itab__300, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 301 */ new ud_lookup_table_list_entry( ud_itab__301, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 302 */ new ud_lookup_table_list_entry( ud_itab__302, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 303 */ new ud_lookup_table_list_entry( ud_itab__303, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 304 */ new ud_lookup_table_list_entry( ud_itab__304, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 305 */ new ud_lookup_table_list_entry( ud_itab__305, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 306 */ new ud_lookup_table_list_entry( ud_itab__306, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 307 */ new ud_lookup_table_list_entry( ud_itab__307, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 308 */ new ud_lookup_table_list_entry( ud_itab__308, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 309 */ new ud_lookup_table_list_entry( ud_itab__309, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 310 */ new ud_lookup_table_list_entry( ud_itab__310, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 311 */ new ud_lookup_table_list_entry( ud_itab__311, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 312 */ new ud_lookup_table_list_entry( ud_itab__312, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 313 */ new ud_lookup_table_list_entry( ud_itab__313, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 314 */ new ud_lookup_table_list_entry( ud_itab__314, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 315 */ new ud_lookup_table_list_entry( ud_itab__315, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 316 */ new ud_lookup_table_list_entry( ud_itab__316, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 317 */ new ud_lookup_table_list_entry( ud_itab__317, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 318 */ new ud_lookup_table_list_entry( ud_itab__318, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 319 */ new ud_lookup_table_list_entry( ud_itab__319, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 320 */ new ud_lookup_table_list_entry( ud_itab__320, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 321 */ new ud_lookup_table_list_entry( ud_itab__321, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 322 */ new ud_lookup_table_list_entry( ud_itab__322, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 323 */ new ud_lookup_table_list_entry( ud_itab__323, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 324 */ new ud_lookup_table_list_entry( ud_itab__324, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 325 */ new ud_lookup_table_list_entry( ud_itab__325, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 326 */ new ud_lookup_table_list_entry( ud_itab__326, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 327 */ new ud_lookup_table_list_entry( ud_itab__327, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 328 */ new ud_lookup_table_list_entry( ud_itab__328, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 329 */ new ud_lookup_table_list_entry( ud_itab__329, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 330 */ new ud_lookup_table_list_entry( ud_itab__330, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 331 */ new ud_lookup_table_list_entry( ud_itab__331, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 332 */ new ud_lookup_table_list_entry( ud_itab__332, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 333 */ new ud_lookup_table_list_entry( ud_itab__333, ud_table_type.UD_TAB__OPC_VEX, "/vex" ),
            /* 334 */ new ud_lookup_table_list_entry( ud_itab__334, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 335 */ new ud_lookup_table_list_entry( ud_itab__335, ud_table_type.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 336 */ new ud_lookup_table_list_entry( ud_itab__336, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 337 */ new ud_lookup_table_list_entry( ud_itab__337, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 338 */ new ud_lookup_table_list_entry( ud_itab__338, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 339 */ new ud_lookup_table_list_entry( ud_itab__339, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 340 */ new ud_lookup_table_list_entry( ud_itab__340, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 341 */ new ud_lookup_table_list_entry( ud_itab__341, ud_table_type.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 342 */ new ud_lookup_table_list_entry( ud_itab__342, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 343 */ new ud_lookup_table_list_entry( ud_itab__343, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 344 */ new ud_lookup_table_list_entry( ud_itab__344, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 345 */ new ud_lookup_table_list_entry( ud_itab__345, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 346 */ new ud_lookup_table_list_entry( ud_itab__346, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 347 */ new ud_lookup_table_list_entry( ud_itab__347, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 348 */ new ud_lookup_table_list_entry( ud_itab__348, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 349 */ new ud_lookup_table_list_entry( ud_itab__349, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 350 */ new ud_lookup_table_list_entry( ud_itab__350, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 351 */ new ud_lookup_table_list_entry( ud_itab__351, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 352 */ new ud_lookup_table_list_entry( ud_itab__352, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 353 */ new ud_lookup_table_list_entry( ud_itab__353, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 354 */ new ud_lookup_table_list_entry( ud_itab__354, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 355 */ new ud_lookup_table_list_entry( ud_itab__355, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 356 */ new ud_lookup_table_list_entry( ud_itab__356, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 357 */ new ud_lookup_table_list_entry( ud_itab__357, ud_table_type.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 358 */ new ud_lookup_table_list_entry( ud_itab__358, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 359 */ new ud_lookup_table_list_entry( ud_itab__359, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 360 */ new ud_lookup_table_list_entry( ud_itab__360, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 361 */ new ud_lookup_table_list_entry( ud_itab__361, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 362 */ new ud_lookup_table_list_entry( ud_itab__362, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 363 */ new ud_lookup_table_list_entry( ud_itab__363, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 364 */ new ud_lookup_table_list_entry( ud_itab__364, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 365 */ new ud_lookup_table_list_entry( ud_itab__365, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 366 */ new ud_lookup_table_list_entry( ud_itab__366, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 367 */ new ud_lookup_table_list_entry( ud_itab__367, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 368 */ new ud_lookup_table_list_entry( ud_itab__368, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 369 */ new ud_lookup_table_list_entry( ud_itab__369, ud_table_type.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 370 */ new ud_lookup_table_list_entry( ud_itab__370, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 371 */ new ud_lookup_table_list_entry( ud_itab__371, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 372 */ new ud_lookup_table_list_entry( ud_itab__372, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 373 */ new ud_lookup_table_list_entry( ud_itab__373, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 374 */ new ud_lookup_table_list_entry( ud_itab__374, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 375 */ new ud_lookup_table_list_entry( ud_itab__375, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 376 */ new ud_lookup_table_list_entry( ud_itab__376, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 377 */ new ud_lookup_table_list_entry( ud_itab__377, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 378 */ new ud_lookup_table_list_entry( ud_itab__378, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 379 */ new ud_lookup_table_list_entry( ud_itab__379, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 380 */ new ud_lookup_table_list_entry( ud_itab__380, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 381 */ new ud_lookup_table_list_entry( ud_itab__381, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 382 */ new ud_lookup_table_list_entry( ud_itab__382, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 383 */ new ud_lookup_table_list_entry( ud_itab__383, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 384 */ new ud_lookup_table_list_entry( ud_itab__384, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 385 */ new ud_lookup_table_list_entry( ud_itab__385, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 386 */ new ud_lookup_table_list_entry( ud_itab__386, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 387 */ new ud_lookup_table_list_entry( ud_itab__387, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 388 */ new ud_lookup_table_list_entry( ud_itab__388, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 389 */ new ud_lookup_table_list_entry( ud_itab__389, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 390 */ new ud_lookup_table_list_entry( ud_itab__390, ud_table_type.UD_TAB__OPC_VEX_L, "/vexl" ),
            /* 391 */ new ud_lookup_table_list_entry( ud_itab__391, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 392 */ new ud_lookup_table_list_entry( ud_itab__392, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 393 */ new ud_lookup_table_list_entry( ud_itab__393, ud_table_type.UD_TAB__OPC_VEX_W, "/vexw" ),
            /* 394 */ new ud_lookup_table_list_entry( ud_itab__394, ud_table_type.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 395 */ new ud_lookup_table_list_entry( ud_itab__395, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 396 */ new ud_lookup_table_list_entry( ud_itab__396, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 397 */ new ud_lookup_table_list_entry( ud_itab__397, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 398 */ new ud_lookup_table_list_entry( ud_itab__398, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 399 */ new ud_lookup_table_list_entry( ud_itab__399, ud_table_type.UD_TAB__OPC_TABLE, "opctbl" ),
            /* 400 */ new ud_lookup_table_list_entry( ud_itab__400, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 401 */ new ud_lookup_table_list_entry( ud_itab__401, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 402 */ new ud_lookup_table_list_entry( ud_itab__402, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 403 */ new ud_lookup_table_list_entry( ud_itab__403, ud_table_type.UD_TAB__OPC_VEX, "/vex" ),
            /* 404 */ new ud_lookup_table_list_entry( ud_itab__404, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 405 */ new ud_lookup_table_list_entry( ud_itab__405, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 406 */ new ud_lookup_table_list_entry( ud_itab__406, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 407 */ new ud_lookup_table_list_entry( ud_itab__407, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 408 */ new ud_lookup_table_list_entry( ud_itab__408, ud_table_type.UD_TAB__OPC_OSIZE, "/o" ),
            /* 409 */ new ud_lookup_table_list_entry( ud_itab__409, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 410 */ new ud_lookup_table_list_entry( ud_itab__410, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 411 */ new ud_lookup_table_list_entry( ud_itab__411, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 412 */ new ud_lookup_table_list_entry( ud_itab__412, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 413 */ new ud_lookup_table_list_entry( ud_itab__413, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 414 */ new ud_lookup_table_list_entry( ud_itab__414, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 415 */ new ud_lookup_table_list_entry( ud_itab__415, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 416 */ new ud_lookup_table_list_entry( ud_itab__416, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 417 */ new ud_lookup_table_list_entry( ud_itab__417, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 418 */ new ud_lookup_table_list_entry( ud_itab__418, ud_table_type.UD_TAB__OPC_X87, "/x87" ),
            /* 419 */ new ud_lookup_table_list_entry( ud_itab__419, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 420 */ new ud_lookup_table_list_entry( ud_itab__420, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 421 */ new ud_lookup_table_list_entry( ud_itab__421, ud_table_type.UD_TAB__OPC_X87, "/x87" ),
            /* 422 */ new ud_lookup_table_list_entry( ud_itab__422, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 423 */ new ud_lookup_table_list_entry( ud_itab__423, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 424 */ new ud_lookup_table_list_entry( ud_itab__424, ud_table_type.UD_TAB__OPC_X87, "/x87" ),
            /* 425 */ new ud_lookup_table_list_entry( ud_itab__425, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 426 */ new ud_lookup_table_list_entry( ud_itab__426, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 427 */ new ud_lookup_table_list_entry( ud_itab__427, ud_table_type.UD_TAB__OPC_X87, "/x87" ),
            /* 428 */ new ud_lookup_table_list_entry( ud_itab__428, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 429 */ new ud_lookup_table_list_entry( ud_itab__429, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 430 */ new ud_lookup_table_list_entry( ud_itab__430, ud_table_type.UD_TAB__OPC_X87, "/x87" ),
            /* 431 */ new ud_lookup_table_list_entry( ud_itab__431, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 432 */ new ud_lookup_table_list_entry( ud_itab__432, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 433 */ new ud_lookup_table_list_entry( ud_itab__433, ud_table_type.UD_TAB__OPC_X87, "/x87" ),
            /* 434 */ new ud_lookup_table_list_entry( ud_itab__434, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 435 */ new ud_lookup_table_list_entry( ud_itab__435, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 436 */ new ud_lookup_table_list_entry( ud_itab__436, ud_table_type.UD_TAB__OPC_X87, "/x87" ),
            /* 437 */ new ud_lookup_table_list_entry( ud_itab__437, ud_table_type.UD_TAB__OPC_MOD, "/mod" ),
            /* 438 */ new ud_lookup_table_list_entry( ud_itab__438, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 439 */ new ud_lookup_table_list_entry( ud_itab__439, ud_table_type.UD_TAB__OPC_X87, "/x87" ),
            /* 440 */ new ud_lookup_table_list_entry( ud_itab__440, ud_table_type.UD_TAB__OPC_ASIZE, "/a" ),
            /* 441 */ new ud_lookup_table_list_entry( ud_itab__441, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
            /* 442 */ new ud_lookup_table_list_entry( ud_itab__442, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 443 */ new ud_lookup_table_list_entry( ud_itab__443, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 444 */ new ud_lookup_table_list_entry( ud_itab__444, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 445 */ new ud_lookup_table_list_entry( ud_itab__445, ud_table_type.UD_TAB__OPC_REG, "/reg" ),
            /* 446 */ new ud_lookup_table_list_entry( ud_itab__446, ud_table_type.UD_TAB__OPC_MODE, "/m" ),
        };
        #endregion
        
        #region Operand Definitions
        
        /// <summary>
        /// itab entry operand definitions (for readability)
        /// </summary>
        internal static class OpDefs
        {
            internal static readonly ud_itab_entry_operand O_AL    = new ud_itab_entry_operand(ud_operand_code.OP_AL   , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_Av    = new ud_itab_entry_operand(ud_operand_code.OP_A    , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_AX    = new ud_itab_entry_operand(ud_operand_code.OP_AX   , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_C     = new ud_itab_entry_operand(ud_operand_code.OP_C    , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_CL    = new ud_itab_entry_operand(ud_operand_code.OP_CL   , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_CS    = new ud_itab_entry_operand(ud_operand_code.OP_CS   , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_CX    = new ud_itab_entry_operand(ud_operand_code.OP_CX   , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_D     = new ud_itab_entry_operand(ud_operand_code.OP_D    , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_DL    = new ud_itab_entry_operand(ud_operand_code.OP_DL   , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_DS    = new ud_itab_entry_operand(ud_operand_code.OP_DS   , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_DX    = new ud_itab_entry_operand(ud_operand_code.OP_DX   , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_E     = new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_eAX   = new ud_itab_entry_operand(ud_operand_code.OP_eAX  , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_Eb    = new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_eCX   = new ud_itab_entry_operand(ud_operand_code.OP_eCX  , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_Ed    = new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_D   );
            internal static readonly ud_itab_entry_operand O_eDX   = new ud_itab_entry_operand(ud_operand_code.OP_eDX  , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_Eq    = new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_Q   );
            internal static readonly ud_itab_entry_operand O_ES    = new ud_itab_entry_operand(ud_operand_code.OP_ES   , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_Ev    = new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_Ew    = new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_Ey    = new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_Y   );
            internal static readonly ud_itab_entry_operand O_Ez    = new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_FS    = new ud_itab_entry_operand(ud_operand_code.OP_FS   , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_Fv    = new ud_itab_entry_operand(ud_operand_code.OP_F    , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_G     = new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_Gb    = new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_Gd    = new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_D   );
            internal static readonly ud_itab_entry_operand O_Gq    = new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_Q   );
            internal static readonly ud_itab_entry_operand O_GS    = new ud_itab_entry_operand(ud_operand_code.OP_GS   , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_Gv    = new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_Gw    = new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_Gy    = new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_Y   );
            internal static readonly ud_itab_entry_operand O_Gz    = new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_H     = new ud_itab_entry_operand(ud_operand_code.OP_H    , ud_operand_size.SZ_X   );
            internal static readonly ud_itab_entry_operand O_Hqq   = new ud_itab_entry_operand(ud_operand_code.OP_H    , ud_operand_size.SZ_QQ  );
            internal static readonly ud_itab_entry_operand O_Hx    = new ud_itab_entry_operand(ud_operand_code.OP_H    , ud_operand_size.SZ_X   );
            internal static readonly ud_itab_entry_operand O_I1    = new ud_itab_entry_operand(ud_operand_code.OP_I1   , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_I3    = new ud_itab_entry_operand(ud_operand_code.OP_I3   , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_Ib    = new ud_itab_entry_operand(ud_operand_code.OP_I    , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_Iv    = new ud_itab_entry_operand(ud_operand_code.OP_I    , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_Iw    = new ud_itab_entry_operand(ud_operand_code.OP_I    , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_Iz    = new ud_itab_entry_operand(ud_operand_code.OP_I    , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_Jb    = new ud_itab_entry_operand(ud_operand_code.OP_J    , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_Jv    = new ud_itab_entry_operand(ud_operand_code.OP_J    , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_Jz    = new ud_itab_entry_operand(ud_operand_code.OP_J    , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_L     = new ud_itab_entry_operand(ud_operand_code.OP_L    , ud_operand_size.SZ_O   );
            internal static readonly ud_itab_entry_operand O_Lx    = new ud_itab_entry_operand(ud_operand_code.OP_L    , ud_operand_size.SZ_X   );
            internal static readonly ud_itab_entry_operand O_M     = new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_Mb    = new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_MbRd  = new ud_itab_entry_operand(ud_operand_code.OP_MR   , ud_operand_size.SZ_BD  );
            internal static readonly ud_itab_entry_operand O_MbRv  = new ud_itab_entry_operand(ud_operand_code.OP_MR   , ud_operand_size.SZ_BV  );
            internal static readonly ud_itab_entry_operand O_Md    = new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_D   );
            internal static readonly ud_itab_entry_operand O_Mdq   = new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_DQ  );
            internal static readonly ud_itab_entry_operand O_MdRy  = new ud_itab_entry_operand(ud_operand_code.OP_MR   , ud_operand_size.SZ_DY  );
            internal static readonly ud_itab_entry_operand O_MdU   = new ud_itab_entry_operand(ud_operand_code.OP_MU   , ud_operand_size.SZ_DO  );
            internal static readonly ud_itab_entry_operand O_Mo    = new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_O   );
            internal static readonly ud_itab_entry_operand O_Mq    = new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_Q   );
            internal static readonly ud_itab_entry_operand O_MqU   = new ud_itab_entry_operand(ud_operand_code.OP_MU   , ud_operand_size.SZ_QO  );
            internal static readonly ud_itab_entry_operand O_Ms    = new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_Mt    = new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_T   );
            internal static readonly ud_itab_entry_operand O_Mv    = new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_Mw    = new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_MwRd  = new ud_itab_entry_operand(ud_operand_code.OP_MR   , ud_operand_size.SZ_WD  );
            internal static readonly ud_itab_entry_operand O_MwRv  = new ud_itab_entry_operand(ud_operand_code.OP_MR   , ud_operand_size.SZ_WV  );
            internal static readonly ud_itab_entry_operand O_MwRy  = new ud_itab_entry_operand(ud_operand_code.OP_MR   , ud_operand_size.SZ_WY  );
            internal static readonly ud_itab_entry_operand O_MwU   = new ud_itab_entry_operand(ud_operand_code.OP_MU   , ud_operand_size.SZ_WO  );
            internal static readonly ud_itab_entry_operand O_N     = new ud_itab_entry_operand(ud_operand_code.OP_N    , ud_operand_size.SZ_Q   );
            internal static readonly ud_itab_entry_operand O_NONE  = new ud_itab_entry_operand(ud_operand_code.OP_NONE , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_Ob    = new ud_itab_entry_operand(ud_operand_code.OP_O    , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_Ov    = new ud_itab_entry_operand(ud_operand_code.OP_O    , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_Ow    = new ud_itab_entry_operand(ud_operand_code.OP_O    , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_P     = new ud_itab_entry_operand(ud_operand_code.OP_P    , ud_operand_size.SZ_Q   );
            internal static readonly ud_itab_entry_operand O_Q     = new ud_itab_entry_operand(ud_operand_code.OP_Q    , ud_operand_size.SZ_Q   );
            internal static readonly ud_itab_entry_operand O_R     = new ud_itab_entry_operand(ud_operand_code.OP_R    , ud_operand_size.SZ_RDQ );
            internal static readonly ud_itab_entry_operand O_R0b   = new ud_itab_entry_operand(ud_operand_code.OP_R0   , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_R0v   = new ud_itab_entry_operand(ud_operand_code.OP_R0   , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_R0w   = new ud_itab_entry_operand(ud_operand_code.OP_R0   , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_R0y   = new ud_itab_entry_operand(ud_operand_code.OP_R0   , ud_operand_size.SZ_Y   );
            internal static readonly ud_itab_entry_operand O_R0z   = new ud_itab_entry_operand(ud_operand_code.OP_R0   , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_R1b   = new ud_itab_entry_operand(ud_operand_code.OP_R1   , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_R1v   = new ud_itab_entry_operand(ud_operand_code.OP_R1   , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_R1w   = new ud_itab_entry_operand(ud_operand_code.OP_R1   , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_R1y   = new ud_itab_entry_operand(ud_operand_code.OP_R1   , ud_operand_size.SZ_Y   );
            internal static readonly ud_itab_entry_operand O_R1z   = new ud_itab_entry_operand(ud_operand_code.OP_R1   , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_R2b   = new ud_itab_entry_operand(ud_operand_code.OP_R2   , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_R2v   = new ud_itab_entry_operand(ud_operand_code.OP_R2   , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_R2w   = new ud_itab_entry_operand(ud_operand_code.OP_R2   , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_R2y   = new ud_itab_entry_operand(ud_operand_code.OP_R2   , ud_operand_size.SZ_Y   );
            internal static readonly ud_itab_entry_operand O_R2z   = new ud_itab_entry_operand(ud_operand_code.OP_R2   , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_R3b   = new ud_itab_entry_operand(ud_operand_code.OP_R3   , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_R3v   = new ud_itab_entry_operand(ud_operand_code.OP_R3   , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_R3w   = new ud_itab_entry_operand(ud_operand_code.OP_R3   , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_R3y   = new ud_itab_entry_operand(ud_operand_code.OP_R3   , ud_operand_size.SZ_Y   );
            internal static readonly ud_itab_entry_operand O_R3z   = new ud_itab_entry_operand(ud_operand_code.OP_R3   , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_R4b   = new ud_itab_entry_operand(ud_operand_code.OP_R4   , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_R4v   = new ud_itab_entry_operand(ud_operand_code.OP_R4   , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_R4w   = new ud_itab_entry_operand(ud_operand_code.OP_R4   , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_R4y   = new ud_itab_entry_operand(ud_operand_code.OP_R4   , ud_operand_size.SZ_Y   );
            internal static readonly ud_itab_entry_operand O_R4z   = new ud_itab_entry_operand(ud_operand_code.OP_R4   , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_R5b   = new ud_itab_entry_operand(ud_operand_code.OP_R5   , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_R5v   = new ud_itab_entry_operand(ud_operand_code.OP_R5   , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_R5w   = new ud_itab_entry_operand(ud_operand_code.OP_R5   , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_R5y   = new ud_itab_entry_operand(ud_operand_code.OP_R5   , ud_operand_size.SZ_Y   );
            internal static readonly ud_itab_entry_operand O_R5z   = new ud_itab_entry_operand(ud_operand_code.OP_R5   , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_R6b   = new ud_itab_entry_operand(ud_operand_code.OP_R6   , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_R6v   = new ud_itab_entry_operand(ud_operand_code.OP_R6   , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_R6w   = new ud_itab_entry_operand(ud_operand_code.OP_R6   , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_R6y   = new ud_itab_entry_operand(ud_operand_code.OP_R6   , ud_operand_size.SZ_Y   );
            internal static readonly ud_itab_entry_operand O_R6z   = new ud_itab_entry_operand(ud_operand_code.OP_R6   , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_R7b   = new ud_itab_entry_operand(ud_operand_code.OP_R7   , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_R7v   = new ud_itab_entry_operand(ud_operand_code.OP_R7   , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_R7w   = new ud_itab_entry_operand(ud_operand_code.OP_R7   , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_R7y   = new ud_itab_entry_operand(ud_operand_code.OP_R7   , ud_operand_size.SZ_Y   );
            internal static readonly ud_itab_entry_operand O_R7z   = new ud_itab_entry_operand(ud_operand_code.OP_R7   , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_rAX   = new ud_itab_entry_operand(ud_operand_code.OP_rAX  , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_rCX   = new ud_itab_entry_operand(ud_operand_code.OP_rCX  , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_rDX   = new ud_itab_entry_operand(ud_operand_code.OP_rDX  , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_S     = new ud_itab_entry_operand(ud_operand_code.OP_S    , ud_operand_size.SZ_W   );
            internal static readonly ud_itab_entry_operand O_sIb   = new ud_itab_entry_operand(ud_operand_code.OP_sI   , ud_operand_size.SZ_B   );
            internal static readonly ud_itab_entry_operand O_sIv   = new ud_itab_entry_operand(ud_operand_code.OP_sI   , ud_operand_size.SZ_V   );
            internal static readonly ud_itab_entry_operand O_sIz   = new ud_itab_entry_operand(ud_operand_code.OP_sI   , ud_operand_size.SZ_Z   );
            internal static readonly ud_itab_entry_operand O_SS    = new ud_itab_entry_operand(ud_operand_code.OP_SS   , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_ST0   = new ud_itab_entry_operand(ud_operand_code.OP_ST0  , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_ST1   = new ud_itab_entry_operand(ud_operand_code.OP_ST1  , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_ST2   = new ud_itab_entry_operand(ud_operand_code.OP_ST2  , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_ST3   = new ud_itab_entry_operand(ud_operand_code.OP_ST3  , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_ST4   = new ud_itab_entry_operand(ud_operand_code.OP_ST4  , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_ST5   = new ud_itab_entry_operand(ud_operand_code.OP_ST5  , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_ST6   = new ud_itab_entry_operand(ud_operand_code.OP_ST6  , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_ST7   = new ud_itab_entry_operand(ud_operand_code.OP_ST7  , ud_operand_size.SZ_NA  );
            internal static readonly ud_itab_entry_operand O_U     = new ud_itab_entry_operand(ud_operand_code.OP_U    , ud_operand_size.SZ_O   );
            internal static readonly ud_itab_entry_operand O_Ux    = new ud_itab_entry_operand(ud_operand_code.OP_U    , ud_operand_size.SZ_X   );
            internal static readonly ud_itab_entry_operand O_V     = new ud_itab_entry_operand(ud_operand_code.OP_V    , ud_operand_size.SZ_DQ  );
            internal static readonly ud_itab_entry_operand O_Vdq   = new ud_itab_entry_operand(ud_operand_code.OP_V    , ud_operand_size.SZ_DQ  );
            internal static readonly ud_itab_entry_operand O_Vqq   = new ud_itab_entry_operand(ud_operand_code.OP_V    , ud_operand_size.SZ_QQ  );
            internal static readonly ud_itab_entry_operand O_Vsd   = new ud_itab_entry_operand(ud_operand_code.OP_V    , ud_operand_size.SZ_Q   );
            internal static readonly ud_itab_entry_operand O_Vx    = new ud_itab_entry_operand(ud_operand_code.OP_V    , ud_operand_size.SZ_X   );
            internal static readonly ud_itab_entry_operand O_W     = new ud_itab_entry_operand(ud_operand_code.OP_W    , ud_operand_size.SZ_DQ  );
            internal static readonly ud_itab_entry_operand O_Wdq   = new ud_itab_entry_operand(ud_operand_code.OP_W    , ud_operand_size.SZ_DQ  );
            internal static readonly ud_itab_entry_operand O_Wqq   = new ud_itab_entry_operand(ud_operand_code.OP_W    , ud_operand_size.SZ_QQ  );
            internal static readonly ud_itab_entry_operand O_Wsd   = new ud_itab_entry_operand(ud_operand_code.OP_W    , ud_operand_size.SZ_Q   );
            internal static readonly ud_itab_entry_operand O_Wx    = new ud_itab_entry_operand(ud_operand_code.OP_W    , ud_operand_size.SZ_X   );
        }
        #endregion
        
        #region Instruction Table and Mnemonics
        internal static readonly ud_itab_entry[] ud_itab = new ud_itab_entry[]
        {
            /* 0000 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinvalid, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0001 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaaa, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0002 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaad, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0003 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaam, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0004 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaas, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0005 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadc, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0006 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadc, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0007 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadc, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0008 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadc, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0009 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadc, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0010 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadc, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0011 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadc, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0012 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadc, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_inv64 ),
            /* 0013 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadc, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0014 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadc, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0015 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadd, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0016 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadd, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0017 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadd, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0018 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadd, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0019 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadd, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0020 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadd, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0021 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadd, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0022 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadd, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_inv64 ),
            /* 0023 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadd, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0024 */ new ud_itab_entry( ud_mnemonic_code.UD_Iadd, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0025 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaddpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0026 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivaddpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0027 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaddps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0028 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivaddps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0029 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaddsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0030 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivaddsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0031 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaddss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0032 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivaddss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0033 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaddsubpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0034 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivaddsubpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0035 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaddsubps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0036 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivaddsubps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0037 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaesdec, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0038 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivaesdec, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0039 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaesdeclast, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0040 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivaesdeclast, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0041 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaesenc, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0042 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivaesenc, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0043 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaesenclast, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0044 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivaesenclast, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0045 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaesimc, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0046 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivaesimc, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0047 */ new ud_itab_entry( ud_mnemonic_code.UD_Iaeskeygenassist, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0048 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivaeskeygenassist, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0049 */ new ud_itab_entry( ud_mnemonic_code.UD_Iand, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0050 */ new ud_itab_entry( ud_mnemonic_code.UD_Iand, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0051 */ new ud_itab_entry( ud_mnemonic_code.UD_Iand, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0052 */ new ud_itab_entry( ud_mnemonic_code.UD_Iand, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0053 */ new ud_itab_entry( ud_mnemonic_code.UD_Iand, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0054 */ new ud_itab_entry( ud_mnemonic_code.UD_Iand, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0055 */ new ud_itab_entry( ud_mnemonic_code.UD_Iand, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0056 */ new ud_itab_entry( ud_mnemonic_code.UD_Iand, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_inv64 ),
            /* 0057 */ new ud_itab_entry( ud_mnemonic_code.UD_Iand, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0058 */ new ud_itab_entry( ud_mnemonic_code.UD_Iand, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0059 */ new ud_itab_entry( ud_mnemonic_code.UD_Iandpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0060 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivandpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0061 */ new ud_itab_entry( ud_mnemonic_code.UD_Iandps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0062 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivandps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0063 */ new ud_itab_entry( ud_mnemonic_code.UD_Iandnpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0064 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivandnpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0065 */ new ud_itab_entry( ud_mnemonic_code.UD_Iandnps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0066 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivandnps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0067 */ new ud_itab_entry( ud_mnemonic_code.UD_Iarpl, OpDefs.O_Ew, OpDefs.O_Gw, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso ),
            /* 0068 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovsxd, OpDefs.O_Gq, OpDefs.O_Ed, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexr|BitOps.P_rexb ),
            /* 0069 */ new ud_itab_entry( ud_mnemonic_code.UD_Icall, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0070 */ new ud_itab_entry( ud_mnemonic_code.UD_Icall, OpDefs.O_Eq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_def64 ),
            /* 0071 */ new ud_itab_entry( ud_mnemonic_code.UD_Icall, OpDefs.O_Fv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0072 */ new ud_itab_entry( ud_mnemonic_code.UD_Icall, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0073 */ new ud_itab_entry( ud_mnemonic_code.UD_Icall, OpDefs.O_Av, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0074 */ new ud_itab_entry( ud_mnemonic_code.UD_Icbw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0075 */ new ud_itab_entry( ud_mnemonic_code.UD_Icwde, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0076 */ new ud_itab_entry( ud_mnemonic_code.UD_Icdqe, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0077 */ new ud_itab_entry( ud_mnemonic_code.UD_Iclc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0078 */ new ud_itab_entry( ud_mnemonic_code.UD_Icld, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0079 */ new ud_itab_entry( ud_mnemonic_code.UD_Iclflush, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0080 */ new ud_itab_entry( ud_mnemonic_code.UD_Iclgi, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0081 */ new ud_itab_entry( ud_mnemonic_code.UD_Icli, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0082 */ new ud_itab_entry( ud_mnemonic_code.UD_Iclts, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0083 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0084 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovo, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0085 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovno, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0086 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovb, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0087 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovae, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0088 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovz, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0089 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovnz, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0090 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovbe, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0091 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmova, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0092 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovs, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0093 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovns, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0094 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovp, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0095 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovnp, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0096 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovl, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0097 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovge, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0098 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovle, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0099 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmovg, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0100 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmp, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0101 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmp, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0102 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmp, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0103 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmp, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0104 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmp, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0105 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmp, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0106 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmp, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0107 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmp, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_inv64 ),
            /* 0108 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmp, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0109 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmp, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0110 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmppd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0111 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcmppd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0112 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmpps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0113 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcmpps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0114 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmpsb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_strz|BitOps.P_seg ),
            /* 0115 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmpsw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_strz|BitOps.P_oso|BitOps.P_rexw|BitOps.P_seg ),
            /* 0116 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmpsd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_strz|BitOps.P_oso|BitOps.P_rexw|BitOps.P_seg ),
            /* 0117 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmpsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0118 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcmpsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0119 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmpsq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_strz|BitOps.P_oso|BitOps.P_rexw|BitOps.P_seg ),
            /* 0120 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmpss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0121 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcmpss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0122 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmpxchg, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0123 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmpxchg, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0124 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmpxchg8b, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0125 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmpxchg8b, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0126 */ new ud_itab_entry( ud_mnemonic_code.UD_Icmpxchg16b, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0127 */ new ud_itab_entry( ud_mnemonic_code.UD_Icomisd, OpDefs.O_Vsd, OpDefs.O_Wsd, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0128 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcomisd, OpDefs.O_Vsd, OpDefs.O_Wsd, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0129 */ new ud_itab_entry( ud_mnemonic_code.UD_Icomiss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0130 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcomiss, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0131 */ new ud_itab_entry( ud_mnemonic_code.UD_Icpuid, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0132 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtdq2pd, OpDefs.O_V, OpDefs.O_Wdq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0133 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvtdq2pd, OpDefs.O_Vx, OpDefs.O_Wdq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0134 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtdq2ps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0135 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvtdq2ps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0136 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtpd2dq, OpDefs.O_Vdq, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0137 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvtpd2dq, OpDefs.O_Vdq, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0138 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtpd2pi, OpDefs.O_P, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0139 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtpd2ps, OpDefs.O_Vdq, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0140 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvtpd2ps, OpDefs.O_Vdq, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0141 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtpi2ps, OpDefs.O_V, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0142 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtpi2pd, OpDefs.O_V, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0143 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtps2dq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0144 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvtps2dq, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0145 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtps2pd, OpDefs.O_V, OpDefs.O_Wdq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0146 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvtps2pd, OpDefs.O_Vx, OpDefs.O_Wdq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0147 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtps2pi, OpDefs.O_P, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0148 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtsd2si, OpDefs.O_Gy, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0149 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvtsd2si, OpDefs.O_Gy, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0150 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtsd2ss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0151 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvtsd2ss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0152 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtsi2sd, OpDefs.O_V, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0153 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvtsi2sd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Ey, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0154 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtsi2ss, OpDefs.O_V, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0155 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvtsi2ss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Ey, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0156 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtss2sd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0157 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvtss2sd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0158 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvtss2si, OpDefs.O_Gy, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0159 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvtss2si, OpDefs.O_Gy, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0160 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvttpd2dq, OpDefs.O_Vdq, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0161 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvttpd2dq, OpDefs.O_Vdq, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0162 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvttpd2pi, OpDefs.O_P, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0163 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvttps2dq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0164 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvttps2dq, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0165 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvttps2pi, OpDefs.O_P, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0166 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvttsd2si, OpDefs.O_Gy, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0167 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvttsd2si, OpDefs.O_Gy, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0168 */ new ud_itab_entry( ud_mnemonic_code.UD_Icvttss2si, OpDefs.O_Gy, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0169 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivcvttss2si, OpDefs.O_Gy, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0170 */ new ud_itab_entry( ud_mnemonic_code.UD_Icwd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0171 */ new ud_itab_entry( ud_mnemonic_code.UD_Icdq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0172 */ new ud_itab_entry( ud_mnemonic_code.UD_Icqo, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0173 */ new ud_itab_entry( ud_mnemonic_code.UD_Idaa, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_inv64 ),
            /* 0174 */ new ud_itab_entry( ud_mnemonic_code.UD_Idas, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_inv64 ),
            /* 0175 */ new ud_itab_entry( ud_mnemonic_code.UD_Idec, OpDefs.O_R0z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0176 */ new ud_itab_entry( ud_mnemonic_code.UD_Idec, OpDefs.O_R1z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0177 */ new ud_itab_entry( ud_mnemonic_code.UD_Idec, OpDefs.O_R2z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0178 */ new ud_itab_entry( ud_mnemonic_code.UD_Idec, OpDefs.O_R3z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0179 */ new ud_itab_entry( ud_mnemonic_code.UD_Idec, OpDefs.O_R4z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0180 */ new ud_itab_entry( ud_mnemonic_code.UD_Idec, OpDefs.O_R5z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0181 */ new ud_itab_entry( ud_mnemonic_code.UD_Idec, OpDefs.O_R6z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0182 */ new ud_itab_entry( ud_mnemonic_code.UD_Idec, OpDefs.O_R7z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0183 */ new ud_itab_entry( ud_mnemonic_code.UD_Idec, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0184 */ new ud_itab_entry( ud_mnemonic_code.UD_Idec, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0185 */ new ud_itab_entry( ud_mnemonic_code.UD_Idiv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0186 */ new ud_itab_entry( ud_mnemonic_code.UD_Idiv, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0187 */ new ud_itab_entry( ud_mnemonic_code.UD_Idivpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0188 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivdivpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0189 */ new ud_itab_entry( ud_mnemonic_code.UD_Idivps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0190 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivdivps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0191 */ new ud_itab_entry( ud_mnemonic_code.UD_Idivsd, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0192 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivdivsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_MqU, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0193 */ new ud_itab_entry( ud_mnemonic_code.UD_Idivss, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0194 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivdivss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_MdU, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0195 */ new ud_itab_entry( ud_mnemonic_code.UD_Idppd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0196 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivdppd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0197 */ new ud_itab_entry( ud_mnemonic_code.UD_Idpps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0198 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivdpps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0199 */ new ud_itab_entry( ud_mnemonic_code.UD_Iemms, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0200 */ new ud_itab_entry( ud_mnemonic_code.UD_Ienter, OpDefs.O_Iw, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_def64 ),
            /* 0201 */ new ud_itab_entry( ud_mnemonic_code.UD_Iextractps, OpDefs.O_MdRy, OpDefs.O_V, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0202 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivextractps, OpDefs.O_MdRy, OpDefs.O_Vx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0203 */ new ud_itab_entry( ud_mnemonic_code.UD_If2xm1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0204 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifabs, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0205 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0206 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0207 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0208 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0209 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0210 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0211 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0212 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0213 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0214 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0215 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0216 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0217 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0218 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0219 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0220 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0221 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0222 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifadd, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0223 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifaddp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0224 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifaddp, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0225 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifaddp, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0226 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifaddp, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0227 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifaddp, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0228 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifaddp, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0229 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifaddp, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0230 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifaddp, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0231 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifbld, OpDefs.O_Mt, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0232 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifbstp, OpDefs.O_Mt, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0233 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifchs, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0234 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifclex, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0235 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0236 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0237 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0238 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0239 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0240 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0241 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0242 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovb, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0243 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0244 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0245 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0246 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0247 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0248 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0249 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0250 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmove, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0251 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0252 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0253 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0254 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0255 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0256 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0257 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0258 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovbe, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0259 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0260 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0261 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0262 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0263 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0264 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0265 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0266 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovu, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0267 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0268 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0269 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0270 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0271 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0272 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0273 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0274 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnb, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0275 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0276 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0277 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0278 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0279 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0280 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0281 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0282 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovne, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0283 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0284 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0285 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0286 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0287 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0288 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0289 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0290 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnbe, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0291 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0292 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0293 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0294 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0295 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0296 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0297 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0298 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcmovnu, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0299 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0300 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0301 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0302 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0303 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0304 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0305 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0306 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomi, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0307 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0308 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0309 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0310 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0311 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0312 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0313 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0314 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0315 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0316 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0317 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0318 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom2, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0319 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom2, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0320 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom2, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0321 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom2, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0322 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom2, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0323 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom2, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0324 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcom2, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0325 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0326 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp3, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0327 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp3, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0328 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp3, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0329 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp3, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0330 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp3, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0331 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp3, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0332 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp3, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0333 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0334 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0335 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0336 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0337 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0338 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0339 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0340 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomi, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0341 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0342 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0343 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0344 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0345 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0346 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0347 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0348 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomip, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0349 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0350 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0351 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0352 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0353 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0354 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0355 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0356 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomip, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0357 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0358 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0359 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0360 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0361 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0362 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0363 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0364 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0365 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0366 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0367 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0368 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp5, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0369 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp5, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0370 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp5, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0371 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp5, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0372 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp5, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0373 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp5, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0374 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcomp5, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0375 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcompp, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0376 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifcos, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0377 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdecstp, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0378 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0379 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0380 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0381 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0382 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0383 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0384 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0385 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0386 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0387 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0388 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0389 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0390 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0391 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0392 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0393 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0394 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0395 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdiv, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0396 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0397 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivp, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0398 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivp, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0399 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivp, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0400 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivp, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0401 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivp, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0402 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivp, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0403 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivp, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0404 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0405 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0406 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0407 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0408 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0409 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0410 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0411 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0412 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0413 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0414 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0415 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0416 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0417 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0418 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0419 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0420 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0421 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivr, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0422 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivrp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0423 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivrp, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0424 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivrp, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0425 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivrp, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0426 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivrp, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0427 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivrp, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0428 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivrp, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0429 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifdivrp, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0430 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifemms, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0431 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffree, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0432 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffree, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0433 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffree, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0434 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffree, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0435 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffree, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0436 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffree, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0437 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffree, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0438 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffree, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0439 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffreep, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0440 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffreep, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0441 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffreep, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0442 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffreep, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0443 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffreep, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0444 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffreep, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0445 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffreep, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0446 */ new ud_itab_entry( ud_mnemonic_code.UD_Iffreep, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0447 */ new ud_itab_entry( ud_mnemonic_code.UD_Ificom, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0448 */ new ud_itab_entry( ud_mnemonic_code.UD_Ificom, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0449 */ new ud_itab_entry( ud_mnemonic_code.UD_Ificomp, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0450 */ new ud_itab_entry( ud_mnemonic_code.UD_Ificomp, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0451 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifild, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0452 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifild, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0453 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifild, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0454 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifincstp, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0455 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifninit, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0456 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifiadd, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0457 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifiadd, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0458 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifidivr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0459 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifidivr, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0460 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifidiv, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0461 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifidiv, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0462 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifisub, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0463 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifisub, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0464 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifisubr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0465 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifisubr, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0466 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifist, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0467 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifist, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0468 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifistp, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0469 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifistp, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0470 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifistp, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0471 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifisttp, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0472 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifisttp, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0473 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifisttp, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0474 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifld, OpDefs.O_Mt, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0475 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifld, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0476 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifld, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0477 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifld, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0478 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifld, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0479 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifld, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0480 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifld, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0481 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifld, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0482 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifld, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0483 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifld, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0484 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifld, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0485 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifld1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0486 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifldl2t, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0487 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifldl2e, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0488 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifldpi, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0489 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifldlg2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0490 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifldln2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0491 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifldz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0492 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifldcw, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0493 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifldenv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0494 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0495 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0496 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0497 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0498 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0499 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0500 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0501 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0502 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0503 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0504 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0505 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0506 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0507 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0508 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0509 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0510 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0511 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmul, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0512 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmulp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0513 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmulp, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0514 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmulp, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0515 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmulp, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0516 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmulp, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0517 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmulp, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0518 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmulp, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0519 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifmulp, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0520 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifimul, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0521 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifimul, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0522 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifnop, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0523 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifndisi, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0524 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifneni, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0525 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifnsetpm, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0526 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifpatan, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0527 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifprem, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0528 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifprem1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0529 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifptan, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0530 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifrndint, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0531 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifrstor, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0532 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifrstpm, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0533 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifnsave, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0534 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifscale, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0535 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsin, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0536 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsincos, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0537 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsqrt, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0538 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp, OpDefs.O_Mt, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0539 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0540 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0541 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0542 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0543 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0544 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0545 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0546 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0547 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0548 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0549 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0550 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp1, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0551 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp1, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0552 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp1, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0553 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp1, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0554 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp1, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0555 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp1, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0556 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp1, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0557 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp8, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0558 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp8, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0559 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp8, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0560 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp8, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0561 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp8, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0562 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp8, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0563 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp8, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0564 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp8, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0565 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp9, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0566 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp9, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0567 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp9, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0568 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp9, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0569 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp9, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0570 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp9, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0571 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp9, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0572 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifstp9, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0573 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifst, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0574 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifst, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0575 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifst, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0576 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifst, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0577 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifst, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0578 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifst, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0579 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifst, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0580 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifst, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0581 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifst, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0582 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifst, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0583 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifnstcw, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0584 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifnstenv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0585 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifnstsw, OpDefs.O_Mw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0586 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifnstsw, OpDefs.O_AX, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0587 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0588 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0589 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0590 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0591 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0592 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0593 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0594 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0595 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0596 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0597 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0598 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0599 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0600 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0601 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0602 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0603 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0604 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsub, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0605 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0606 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubp, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0607 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubp, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0608 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubp, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0609 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubp, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0610 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubp, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0611 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubp, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0612 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubp, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0613 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0614 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0615 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0616 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0617 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0618 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0619 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0620 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0621 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0622 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0623 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0624 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0625 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0626 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0627 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0628 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0629 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0630 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0631 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubrp, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0632 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubrp, OpDefs.O_ST1, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0633 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubrp, OpDefs.O_ST2, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0634 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubrp, OpDefs.O_ST3, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0635 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubrp, OpDefs.O_ST4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0636 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubrp, OpDefs.O_ST5, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0637 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubrp, OpDefs.O_ST6, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0638 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifsubrp, OpDefs.O_ST7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0639 */ new ud_itab_entry( ud_mnemonic_code.UD_Iftst, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0640 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucom, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0641 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucom, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0642 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucom, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0643 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucom, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0644 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucom, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0645 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucom, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0646 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucom, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0647 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucom, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0648 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomp, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0649 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomp, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0650 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomp, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0651 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomp, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0652 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomp, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0653 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomp, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0654 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomp, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0655 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucomp, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0656 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifucompp, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0657 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxam, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0658 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0659 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0660 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0661 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0662 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0663 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0664 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0665 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch, OpDefs.O_ST0, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0666 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch4, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0667 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch4, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0668 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch4, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0669 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch4, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0670 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch4, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0671 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch4, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0672 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch4, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0673 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch4, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0674 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch7, OpDefs.O_ST0, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0675 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch7, OpDefs.O_ST1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0676 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch7, OpDefs.O_ST2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0677 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch7, OpDefs.O_ST3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0678 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch7, OpDefs.O_ST4, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0679 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch7, OpDefs.O_ST5, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0680 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch7, OpDefs.O_ST6, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0681 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxch7, OpDefs.O_ST7, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0682 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxrstor, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0683 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxsave, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0684 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifxtract, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0685 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifyl2x, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0686 */ new ud_itab_entry( ud_mnemonic_code.UD_Ifyl2xp1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0687 */ new ud_itab_entry( ud_mnemonic_code.UD_Ihlt, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0688 */ new ud_itab_entry( ud_mnemonic_code.UD_Iidiv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0689 */ new ud_itab_entry( ud_mnemonic_code.UD_Iidiv, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0690 */ new ud_itab_entry( ud_mnemonic_code.UD_Iin, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0691 */ new ud_itab_entry( ud_mnemonic_code.UD_Iin, OpDefs.O_eAX, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0692 */ new ud_itab_entry( ud_mnemonic_code.UD_Iin, OpDefs.O_AL, OpDefs.O_DX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0693 */ new ud_itab_entry( ud_mnemonic_code.UD_Iin, OpDefs.O_eAX, OpDefs.O_DX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0694 */ new ud_itab_entry( ud_mnemonic_code.UD_Iimul, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0695 */ new ud_itab_entry( ud_mnemonic_code.UD_Iimul, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0696 */ new ud_itab_entry( ud_mnemonic_code.UD_Iimul, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0697 */ new ud_itab_entry( ud_mnemonic_code.UD_Iimul, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_Iz, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0698 */ new ud_itab_entry( ud_mnemonic_code.UD_Iimul, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0699 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinc, OpDefs.O_R0z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0700 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinc, OpDefs.O_R1z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0701 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinc, OpDefs.O_R2z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0702 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinc, OpDefs.O_R3z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0703 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinc, OpDefs.O_R4z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0704 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinc, OpDefs.O_R5z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0705 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinc, OpDefs.O_R6z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0706 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinc, OpDefs.O_R7z, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0707 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinc, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0708 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinc, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0709 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinsb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg ),
            /* 0710 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinsw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_oso|BitOps.P_seg ),
            /* 0711 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinsd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_oso|BitOps.P_seg ),
            /* 0712 */ new ud_itab_entry( ud_mnemonic_code.UD_Iint1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0713 */ new ud_itab_entry( ud_mnemonic_code.UD_Iint3, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0714 */ new ud_itab_entry( ud_mnemonic_code.UD_Iint, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0715 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinto, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_inv64 ),
            /* 0716 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinvd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0717 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinvept, OpDefs.O_Gd, OpDefs.O_Mo, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0718 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinvept, OpDefs.O_Gq, OpDefs.O_Mo, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0719 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinvlpg, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0720 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinvlpga, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0721 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinvvpid, OpDefs.O_Gd, OpDefs.O_Mo, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0722 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinvvpid, OpDefs.O_Gq, OpDefs.O_Mo, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0723 */ new ud_itab_entry( ud_mnemonic_code.UD_Iiretw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0724 */ new ud_itab_entry( ud_mnemonic_code.UD_Iiretd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0725 */ new ud_itab_entry( ud_mnemonic_code.UD_Iiretq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0726 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijo, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0727 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijo, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0728 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijno, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0729 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijno, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0730 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijb, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0731 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijb, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0732 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijae, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0733 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijae, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0734 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijz, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0735 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijz, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0736 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijnz, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0737 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijnz, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0738 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijbe, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0739 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijbe, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0740 */ new ud_itab_entry( ud_mnemonic_code.UD_Ija, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0741 */ new ud_itab_entry( ud_mnemonic_code.UD_Ija, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0742 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijs, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0743 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijs, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0744 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijns, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0745 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijns, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0746 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijp, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0747 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijp, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0748 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijnp, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0749 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijnp, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0750 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijl, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0751 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijl, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0752 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijge, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0753 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijge, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0754 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijle, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0755 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijle, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0756 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijg, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0757 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijg, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0758 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijcxz, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso ),
            /* 0759 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijecxz, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso ),
            /* 0760 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijrcxz, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso ),
            /* 0761 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijmp, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_def64 ),
            /* 0762 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijmp, OpDefs.O_Fv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0763 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijmp, OpDefs.O_Jz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 0764 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijmp, OpDefs.O_Av, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0765 */ new ud_itab_entry( ud_mnemonic_code.UD_Ijmp, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_def64 ),
            /* 0766 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilahf, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0767 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilar, OpDefs.O_Gv, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0768 */ new ud_itab_entry( ud_mnemonic_code.UD_Ildmxcsr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0769 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilds, OpDefs.O_Gv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso ),
            /* 0770 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilea, OpDefs.O_Gv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0771 */ new ud_itab_entry( ud_mnemonic_code.UD_Iles, OpDefs.O_Gv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso ),
            /* 0772 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilfs, OpDefs.O_Gz, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0773 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilgs, OpDefs.O_Gz, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0774 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilidt, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0775 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilss, OpDefs.O_Gv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0776 */ new ud_itab_entry( ud_mnemonic_code.UD_Ileave, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0777 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0778 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0779 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0780 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0781 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0782 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0783 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0784 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0785 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilgdt, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0786 */ new ud_itab_entry( ud_mnemonic_code.UD_Illdt, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0787 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilmsw, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0788 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilmsw, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0789 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilock, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0790 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilodsb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg ),
            /* 0791 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilodsw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg|BitOps.P_oso|BitOps.P_rexw ),
            /* 0792 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilodsd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg|BitOps.P_oso|BitOps.P_rexw ),
            /* 0793 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilodsq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg|BitOps.P_oso|BitOps.P_rexw ),
            /* 0794 */ new ud_itab_entry( ud_mnemonic_code.UD_Iloopne, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0795 */ new ud_itab_entry( ud_mnemonic_code.UD_Iloope, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0796 */ new ud_itab_entry( ud_mnemonic_code.UD_Iloop, OpDefs.O_Jb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0797 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilsl, OpDefs.O_Gv, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0798 */ new ud_itab_entry( ud_mnemonic_code.UD_Iltr, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0799 */ new ud_itab_entry( ud_mnemonic_code.UD_Imaskmovq, OpDefs.O_P, OpDefs.O_N, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0800 */ new ud_itab_entry( ud_mnemonic_code.UD_Imaxpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0801 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmaxpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0802 */ new ud_itab_entry( ud_mnemonic_code.UD_Imaxps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0803 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmaxps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0804 */ new ud_itab_entry( ud_mnemonic_code.UD_Imaxsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0805 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmaxsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0806 */ new ud_itab_entry( ud_mnemonic_code.UD_Imaxss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0807 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmaxss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0808 */ new ud_itab_entry( ud_mnemonic_code.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0809 */ new ud_itab_entry( ud_mnemonic_code.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0810 */ new ud_itab_entry( ud_mnemonic_code.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0811 */ new ud_itab_entry( ud_mnemonic_code.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0812 */ new ud_itab_entry( ud_mnemonic_code.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0813 */ new ud_itab_entry( ud_mnemonic_code.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0814 */ new ud_itab_entry( ud_mnemonic_code.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0815 */ new ud_itab_entry( ud_mnemonic_code.UD_Imfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0816 */ new ud_itab_entry( ud_mnemonic_code.UD_Iminpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0817 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivminpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0818 */ new ud_itab_entry( ud_mnemonic_code.UD_Iminps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0819 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivminps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0820 */ new ud_itab_entry( ud_mnemonic_code.UD_Iminsd, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0821 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivminsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_MqU, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0822 */ new ud_itab_entry( ud_mnemonic_code.UD_Iminss, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0823 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivminss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_MdU, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0824 */ new ud_itab_entry( ud_mnemonic_code.UD_Imonitor, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0825 */ new ud_itab_entry( ud_mnemonic_code.UD_Imontmul, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0826 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0827 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0828 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0829 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0830 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0831 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0832 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_MwRv, OpDefs.O_S, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0833 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_S, OpDefs.O_MwRv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0834 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_AL, OpDefs.O_Ob, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0835 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_rAX, OpDefs.O_Ov, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw ),
            /* 0836 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_Ob, OpDefs.O_AL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0837 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_Ov, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw ),
            /* 0838 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R0b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 0839 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R1b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 0840 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R2b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 0841 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R3b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 0842 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R4b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 0843 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R5b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 0844 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R6b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 0845 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R7b, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 0846 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R0v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 0847 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R1v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 0848 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R2v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 0849 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R3v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 0850 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R4v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 0851 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R5v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 0852 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R6v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 0853 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R7v, OpDefs.O_Iv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 0854 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R, OpDefs.O_C, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexb ),
            /* 0855 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_R, OpDefs.O_D, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexb ),
            /* 0856 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_C, OpDefs.O_R, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexb ),
            /* 0857 */ new ud_itab_entry( ud_mnemonic_code.UD_Imov, OpDefs.O_D, OpDefs.O_R, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexb ),
            /* 0858 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovapd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0859 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovapd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0860 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovapd, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0861 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovapd, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0862 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovaps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0863 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovaps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0864 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovaps, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0865 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovaps, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0866 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovd, OpDefs.O_P, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0867 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovd, OpDefs.O_P, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0868 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovd, OpDefs.O_V, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0869 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovd, OpDefs.O_Vx, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0870 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovd, OpDefs.O_V, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0871 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovd, OpDefs.O_Vx, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0872 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovd, OpDefs.O_Ey, OpDefs.O_P, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0873 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovd, OpDefs.O_Ey, OpDefs.O_P, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0874 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovd, OpDefs.O_Ey, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0875 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovd, OpDefs.O_Ey, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0876 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovd, OpDefs.O_Ey, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0877 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovd, OpDefs.O_Ey, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0878 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovhpd, OpDefs.O_V, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0879 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovhpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_M, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0880 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovhpd, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0881 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovhpd, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0882 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovhps, OpDefs.O_V, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0883 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovhps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_M, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0884 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovhps, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0885 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovhps, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0886 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovlhps, OpDefs.O_V, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0887 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovlhps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0888 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovlpd, OpDefs.O_V, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0889 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovlpd, OpDefs.O_Vx, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0890 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovlpd, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0891 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovlpd, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0892 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovlps, OpDefs.O_V, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0893 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovlps, OpDefs.O_Vx, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0894 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovlps, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0895 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovlps, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0896 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovhlps, OpDefs.O_V, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0897 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovhlps, OpDefs.O_Vx, OpDefs.O_Ux, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0898 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovmskpd, OpDefs.O_Gd, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexb ),
            /* 0899 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovmskpd, OpDefs.O_Gd, OpDefs.O_Ux, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0900 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovmskps, OpDefs.O_Gd, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexb ),
            /* 0901 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovmskps, OpDefs.O_Gd, OpDefs.O_Ux, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexb ),
            /* 0902 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovntdq, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0903 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovntdq, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0904 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovnti, OpDefs.O_M, OpDefs.O_Gy, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0905 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovntpd, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0906 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovntpd, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0907 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovntps, OpDefs.O_M, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0908 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovntps, OpDefs.O_M, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0909 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovntq, OpDefs.O_M, OpDefs.O_P, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0910 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovq, OpDefs.O_P, OpDefs.O_Eq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0911 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovq, OpDefs.O_V, OpDefs.O_Eq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0912 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovq, OpDefs.O_Vx, OpDefs.O_Eq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0913 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovq, OpDefs.O_Eq, OpDefs.O_P, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0914 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovq, OpDefs.O_Eq, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0915 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovq, OpDefs.O_Eq, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0916 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0917 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovq, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0918 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovq, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0919 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovq, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0920 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0921 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovq, OpDefs.O_Q, OpDefs.O_P, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0922 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovsb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg ),
            /* 0923 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovsw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg|BitOps.P_oso|BitOps.P_rexw ),
            /* 0924 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovsd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg|BitOps.P_oso|BitOps.P_rexw ),
            /* 0925 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovsd, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0926 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovsd, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0927 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovsq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg|BitOps.P_oso|BitOps.P_rexw ),
            /* 0928 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovss, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0929 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovss, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0930 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovsx, OpDefs.O_Gv, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0931 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovsx, OpDefs.O_Gy, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0932 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovupd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0933 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovupd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0934 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovupd, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0935 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovupd, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0936 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovups, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0937 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovups, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0938 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovups, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0939 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovups, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0940 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovzx, OpDefs.O_Gv, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0941 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovzx, OpDefs.O_Gy, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0942 */ new ud_itab_entry( ud_mnemonic_code.UD_Imul, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0943 */ new ud_itab_entry( ud_mnemonic_code.UD_Imul, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0944 */ new ud_itab_entry( ud_mnemonic_code.UD_Imulpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0945 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmulpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0946 */ new ud_itab_entry( ud_mnemonic_code.UD_Imulps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0947 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmulps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0948 */ new ud_itab_entry( ud_mnemonic_code.UD_Imulsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0949 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmulsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0950 */ new ud_itab_entry( ud_mnemonic_code.UD_Imulss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0951 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmulss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0952 */ new ud_itab_entry( ud_mnemonic_code.UD_Imwait, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0953 */ new ud_itab_entry( ud_mnemonic_code.UD_Ineg, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0954 */ new ud_itab_entry( ud_mnemonic_code.UD_Ineg, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0955 */ new ud_itab_entry( ud_mnemonic_code.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0956 */ new ud_itab_entry( ud_mnemonic_code.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0957 */ new ud_itab_entry( ud_mnemonic_code.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0958 */ new ud_itab_entry( ud_mnemonic_code.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0959 */ new ud_itab_entry( ud_mnemonic_code.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0960 */ new ud_itab_entry( ud_mnemonic_code.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0961 */ new ud_itab_entry( ud_mnemonic_code.UD_Inop, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0962 */ new ud_itab_entry( ud_mnemonic_code.UD_Inot, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0963 */ new ud_itab_entry( ud_mnemonic_code.UD_Inot, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0964 */ new ud_itab_entry( ud_mnemonic_code.UD_Ior, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0965 */ new ud_itab_entry( ud_mnemonic_code.UD_Ior, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0966 */ new ud_itab_entry( ud_mnemonic_code.UD_Ior, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0967 */ new ud_itab_entry( ud_mnemonic_code.UD_Ior, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0968 */ new ud_itab_entry( ud_mnemonic_code.UD_Ior, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0969 */ new ud_itab_entry( ud_mnemonic_code.UD_Ior, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 0970 */ new ud_itab_entry( ud_mnemonic_code.UD_Ior, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0971 */ new ud_itab_entry( ud_mnemonic_code.UD_Ior, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0972 */ new ud_itab_entry( ud_mnemonic_code.UD_Ior, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0973 */ new ud_itab_entry( ud_mnemonic_code.UD_Ior, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0974 */ new ud_itab_entry( ud_mnemonic_code.UD_Iorpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0975 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivorpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0976 */ new ud_itab_entry( ud_mnemonic_code.UD_Iorps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0977 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivorps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0978 */ new ud_itab_entry( ud_mnemonic_code.UD_Iout, OpDefs.O_Ib, OpDefs.O_AL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0979 */ new ud_itab_entry( ud_mnemonic_code.UD_Iout, OpDefs.O_Ib, OpDefs.O_eAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0980 */ new ud_itab_entry( ud_mnemonic_code.UD_Iout, OpDefs.O_DX, OpDefs.O_AL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 0981 */ new ud_itab_entry( ud_mnemonic_code.UD_Iout, OpDefs.O_DX, OpDefs.O_eAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 0982 */ new ud_itab_entry( ud_mnemonic_code.UD_Ioutsb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg ),
            /* 0983 */ new ud_itab_entry( ud_mnemonic_code.UD_Ioutsw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_oso|BitOps.P_seg ),
            /* 0984 */ new ud_itab_entry( ud_mnemonic_code.UD_Ioutsd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_oso|BitOps.P_seg ),
            /* 0985 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipacksswb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0986 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpacksswb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0987 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipacksswb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0988 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipackssdw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0989 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpackssdw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0990 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipackssdw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0991 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipackuswb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0992 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpackuswb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0993 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipackuswb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0994 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0995 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpaddb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 0996 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0997 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0998 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 0999 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpaddw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1000 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1001 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1002 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpaddd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1003 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddsb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1004 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddsb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1005 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpaddsb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1006 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1007 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1008 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpaddsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1009 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddusb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1010 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddusb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1011 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpaddusb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1012 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddusw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1013 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddusw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1014 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpaddusw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1015 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipand, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1016 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpand, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1017 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipand, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1018 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipandn, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1019 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpandn, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1020 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipandn, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1021 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipavgb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1022 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpavgb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1023 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipavgb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1024 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipavgw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1025 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpavgw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1026 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipavgw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1027 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpeqb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1028 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpeqb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1029 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpcmpeqb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1030 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpeqw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1031 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpeqw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1032 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpcmpeqw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1033 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpeqd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1034 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpeqd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1035 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpcmpeqd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1036 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpgtb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1037 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpcmpgtb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1038 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpgtb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1039 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpgtw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1040 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpcmpgtw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1041 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpgtw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1042 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpgtd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1043 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpcmpgtd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1044 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpgtd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1045 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipextrb, OpDefs.O_MbRv, OpDefs.O_V, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexx|BitOps.P_rexr|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1046 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpextrb, OpDefs.O_MbRv, OpDefs.O_Vx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexx|BitOps.P_rexr|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1047 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipextrd, OpDefs.O_Ed, OpDefs.O_V, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1048 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpextrd, OpDefs.O_Ed, OpDefs.O_Vx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1049 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipextrd, OpDefs.O_Ed, OpDefs.O_V, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1050 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpextrd, OpDefs.O_Ed, OpDefs.O_Vx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1051 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipextrq, OpDefs.O_Eq, OpDefs.O_V, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1052 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpextrq, OpDefs.O_Eq, OpDefs.O_Vx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1053 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipextrw, OpDefs.O_Gd, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexb ),
            /* 1054 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpextrw, OpDefs.O_Gd, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexb ),
            /* 1055 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipextrw, OpDefs.O_Gd, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1056 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipextrw, OpDefs.O_MwRd, OpDefs.O_V, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexr|BitOps.P_rexb ),
            /* 1057 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpextrw, OpDefs.O_MwRd, OpDefs.O_Vx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexr|BitOps.P_rexb ),
            /* 1058 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipinsrb, OpDefs.O_V, OpDefs.O_MbRd, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1059 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipinsrw, OpDefs.O_P, OpDefs.O_MwRy, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1060 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipinsrw, OpDefs.O_V, OpDefs.O_MwRy, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1061 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpinsrw, OpDefs.O_Vx, OpDefs.O_MwRy, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1062 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipinsrd, OpDefs.O_V, OpDefs.O_Ed, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1063 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipinsrd, OpDefs.O_V, OpDefs.O_Ed, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1064 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipinsrq, OpDefs.O_V, OpDefs.O_Eq, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1065 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpinsrb, OpDefs.O_V, OpDefs.O_H, OpDefs.O_MbRd, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1066 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpinsrd, OpDefs.O_V, OpDefs.O_H, OpDefs.O_Ed, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1067 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpinsrd, OpDefs.O_V, OpDefs.O_H, OpDefs.O_Ed, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1068 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpinsrq, OpDefs.O_V, OpDefs.O_H, OpDefs.O_Eq, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1069 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmaddwd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1070 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmaddwd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1071 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmaddwd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1072 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmaxsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1073 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmaxsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1074 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmaxsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1075 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmaxub, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1076 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmaxub, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1077 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmaxub, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1078 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipminsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1079 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpminsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1080 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipminsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1081 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipminub, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1082 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpminub, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1083 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipminub, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1084 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovmskb, OpDefs.O_Gd, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1085 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmovmskb, OpDefs.O_Gd, OpDefs.O_Ux, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1086 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovmskb, OpDefs.O_Gd, OpDefs.O_N, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1087 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmulhuw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1088 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmulhuw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1089 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmulhuw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1090 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmulhw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1091 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmulhw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1092 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmulhw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1093 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmullw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1094 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmullw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1095 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmullw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1096 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_ES, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_inv64 ),
            /* 1097 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_SS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_inv64 ),
            /* 1098 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_DS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_inv64 ),
            /* 1099 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_GS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1100 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_FS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1101 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_R0v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1102 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_R1v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1103 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_R2v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1104 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_R3v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1105 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_R4v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1106 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_R5v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1107 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_R6v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1108 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_R7v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1109 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipop, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1110 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipopa, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_inv64 ),
            /* 1111 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipopad, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_inv64 ),
            /* 1112 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipopfw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 1113 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipopfd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 1114 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipopfq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 1115 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipopfq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 1116 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipor, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1117 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpor, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1118 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipor, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1119 */ new ud_itab_entry( ud_mnemonic_code.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1120 */ new ud_itab_entry( ud_mnemonic_code.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1121 */ new ud_itab_entry( ud_mnemonic_code.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1122 */ new ud_itab_entry( ud_mnemonic_code.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1123 */ new ud_itab_entry( ud_mnemonic_code.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1124 */ new ud_itab_entry( ud_mnemonic_code.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1125 */ new ud_itab_entry( ud_mnemonic_code.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1126 */ new ud_itab_entry( ud_mnemonic_code.UD_Iprefetch, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1127 */ new ud_itab_entry( ud_mnemonic_code.UD_Iprefetchnta, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1128 */ new ud_itab_entry( ud_mnemonic_code.UD_Iprefetcht0, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1129 */ new ud_itab_entry( ud_mnemonic_code.UD_Iprefetcht1, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1130 */ new ud_itab_entry( ud_mnemonic_code.UD_Iprefetcht2, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1131 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsadbw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1132 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsadbw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1133 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsadbw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1134 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipshufw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1135 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsllw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1136 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsllw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1137 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsllw, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1138 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsllw, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1139 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipslld, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1140 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipslld, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1141 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipslld, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1142 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipslld, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1143 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsllq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1144 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsllq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1145 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsllq, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1146 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsllq, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1147 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsraw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1148 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsraw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1149 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsraw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1150 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsraw, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1151 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsraw, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1152 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsraw, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1153 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrad, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1154 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrad, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1155 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsrad, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1156 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrad, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1157 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrad, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1158 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsrad, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1159 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrlw, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1160 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrlw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1161 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrlw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1162 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsrlw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1163 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrlw, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1164 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsrlw, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1165 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrld, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1166 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrld, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1167 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrld, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1168 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsrld, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1169 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrld, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1170 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsrld, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1171 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrlq, OpDefs.O_N, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1172 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrlq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1173 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrlq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1174 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsrlq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1175 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrlq, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1176 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsrlq, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1177 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1178 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsubb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1179 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1180 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1181 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsubw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1182 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1183 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1184 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1185 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsubd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1186 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubsb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1187 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubsb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1188 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsubsb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1189 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1190 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1191 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsubsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1192 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubusb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1193 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubusb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1194 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsubusb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1195 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubusw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1196 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubusw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1197 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsubusw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1198 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpckhbw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1199 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpunpckhbw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1200 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpckhbw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1201 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpckhwd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1202 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpunpckhwd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1203 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpckhwd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1204 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpckhdq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1205 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpunpckhdq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1206 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpckhdq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1207 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpcklbw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1208 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpunpcklbw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1209 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpcklbw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1210 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpcklwd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1211 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpunpcklwd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1212 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpcklwd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1213 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpckldq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1214 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpunpckldq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1215 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpckldq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1216 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipi2fw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1217 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipi2fd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1218 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipf2iw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1219 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipf2id, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1220 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfnacc, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1221 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfpnacc, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1222 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfcmpge, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1223 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfmin, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1224 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfrcp, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1225 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfrsqrt, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1226 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfsub, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1227 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfadd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1228 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfcmpgt, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1229 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfmax, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1230 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfrcpit1, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1231 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfrsqit1, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1232 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfsubr, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1233 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfacc, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1234 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfcmpeq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1235 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfmul, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1236 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipfrcpit2, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1237 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmulhrw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1238 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipswapd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1239 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipavgusb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1240 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_ES, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_inv64 ),
            /* 1241 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_CS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_inv64 ),
            /* 1242 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_SS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_inv64 ),
            /* 1243 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_DS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_inv64 ),
            /* 1244 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_GS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1245 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_FS, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1246 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_R0v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1247 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_R1v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1248 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_R2v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1249 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_R3v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1250 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_R4v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1251 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_R5v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1252 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_R6v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1253 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_R7v, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1254 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 1255 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1256 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipush, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_def64 ),
            /* 1257 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipusha, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_inv64 ),
            /* 1258 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipushad, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_inv64 ),
            /* 1259 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipushfw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 1260 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipushfw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_def64 ),
            /* 1261 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipushfd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso ),
            /* 1262 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipushfq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_def64 ),
            /* 1263 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipushfq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_def64 ),
            /* 1264 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipxor, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1265 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpxor, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1266 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipxor, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1267 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircl, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1268 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircl, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1269 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircl, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1270 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircl, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1271 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircl, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1272 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircl, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1273 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircr, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1274 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircr, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1275 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircr, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1276 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircr, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1277 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircr, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1278 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircr, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1279 */ new ud_itab_entry( ud_mnemonic_code.UD_Irol, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1280 */ new ud_itab_entry( ud_mnemonic_code.UD_Irol, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1281 */ new ud_itab_entry( ud_mnemonic_code.UD_Irol, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1282 */ new ud_itab_entry( ud_mnemonic_code.UD_Irol, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1283 */ new ud_itab_entry( ud_mnemonic_code.UD_Irol, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1284 */ new ud_itab_entry( ud_mnemonic_code.UD_Irol, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1285 */ new ud_itab_entry( ud_mnemonic_code.UD_Iror, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1286 */ new ud_itab_entry( ud_mnemonic_code.UD_Iror, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1287 */ new ud_itab_entry( ud_mnemonic_code.UD_Iror, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1288 */ new ud_itab_entry( ud_mnemonic_code.UD_Iror, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1289 */ new ud_itab_entry( ud_mnemonic_code.UD_Iror, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1290 */ new ud_itab_entry( ud_mnemonic_code.UD_Iror, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1291 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircpps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1292 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivrcpps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1293 */ new ud_itab_entry( ud_mnemonic_code.UD_Ircpss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1294 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivrcpss, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1295 */ new ud_itab_entry( ud_mnemonic_code.UD_Irdmsr, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1296 */ new ud_itab_entry( ud_mnemonic_code.UD_Irdpmc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1297 */ new ud_itab_entry( ud_mnemonic_code.UD_Irdtsc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1298 */ new ud_itab_entry( ud_mnemonic_code.UD_Irdtscp, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1299 */ new ud_itab_entry( ud_mnemonic_code.UD_Irepne, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1300 */ new ud_itab_entry( ud_mnemonic_code.UD_Irep, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1301 */ new ud_itab_entry( ud_mnemonic_code.UD_Iret, OpDefs.O_Iw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1302 */ new ud_itab_entry( ud_mnemonic_code.UD_Iret, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1303 */ new ud_itab_entry( ud_mnemonic_code.UD_Iretf, OpDefs.O_Iw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1304 */ new ud_itab_entry( ud_mnemonic_code.UD_Iretf, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1305 */ new ud_itab_entry( ud_mnemonic_code.UD_Irsm, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1306 */ new ud_itab_entry( ud_mnemonic_code.UD_Irsqrtps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1307 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivrsqrtps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1308 */ new ud_itab_entry( ud_mnemonic_code.UD_Irsqrtss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1309 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivrsqrtss, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1310 */ new ud_itab_entry( ud_mnemonic_code.UD_Isahf, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1311 */ new ud_itab_entry( ud_mnemonic_code.UD_Isalc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_inv64 ),
            /* 1312 */ new ud_itab_entry( ud_mnemonic_code.UD_Isar, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1313 */ new ud_itab_entry( ud_mnemonic_code.UD_Isar, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1314 */ new ud_itab_entry( ud_mnemonic_code.UD_Isar, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1315 */ new ud_itab_entry( ud_mnemonic_code.UD_Isar, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1316 */ new ud_itab_entry( ud_mnemonic_code.UD_Isar, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1317 */ new ud_itab_entry( ud_mnemonic_code.UD_Isar, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1318 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishl, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1319 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishl, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1320 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishl, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1321 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishl, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1322 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishl, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1323 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishl, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1324 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishl, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1325 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishl, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1326 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishl, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1327 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishl, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1328 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishl, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1329 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishl, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1330 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishr, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1331 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishr, OpDefs.O_Eb, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1332 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishr, OpDefs.O_Ev, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1333 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishr, OpDefs.O_Eb, OpDefs.O_I1, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1334 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishr, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1335 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishr, OpDefs.O_Ev, OpDefs.O_CL, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1336 */ new ud_itab_entry( ud_mnemonic_code.UD_Isbb, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1337 */ new ud_itab_entry( ud_mnemonic_code.UD_Isbb, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1338 */ new ud_itab_entry( ud_mnemonic_code.UD_Isbb, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1339 */ new ud_itab_entry( ud_mnemonic_code.UD_Isbb, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1340 */ new ud_itab_entry( ud_mnemonic_code.UD_Isbb, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1341 */ new ud_itab_entry( ud_mnemonic_code.UD_Isbb, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 1342 */ new ud_itab_entry( ud_mnemonic_code.UD_Isbb, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1343 */ new ud_itab_entry( ud_mnemonic_code.UD_Isbb, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1344 */ new ud_itab_entry( ud_mnemonic_code.UD_Isbb, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_inv64 ),
            /* 1345 */ new ud_itab_entry( ud_mnemonic_code.UD_Isbb, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1346 */ new ud_itab_entry( ud_mnemonic_code.UD_Iscasb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_strz ),
            /* 1347 */ new ud_itab_entry( ud_mnemonic_code.UD_Iscasw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_strz|BitOps.P_oso|BitOps.P_rexw ),
            /* 1348 */ new ud_itab_entry( ud_mnemonic_code.UD_Iscasd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_strz|BitOps.P_oso|BitOps.P_rexw ),
            /* 1349 */ new ud_itab_entry( ud_mnemonic_code.UD_Iscasq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_strz|BitOps.P_oso|BitOps.P_rexw ),
            /* 1350 */ new ud_itab_entry( ud_mnemonic_code.UD_Iseto, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1351 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetno, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1352 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1353 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetae, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1354 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetz, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1355 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetnz, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1356 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetbe, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1357 */ new ud_itab_entry( ud_mnemonic_code.UD_Iseta, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1358 */ new ud_itab_entry( ud_mnemonic_code.UD_Isets, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1359 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetns, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1360 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetp, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1361 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetnp, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1362 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetl, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1363 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetge, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1364 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetle, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1365 */ new ud_itab_entry( ud_mnemonic_code.UD_Isetg, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1366 */ new ud_itab_entry( ud_mnemonic_code.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1367 */ new ud_itab_entry( ud_mnemonic_code.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1368 */ new ud_itab_entry( ud_mnemonic_code.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1369 */ new ud_itab_entry( ud_mnemonic_code.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1370 */ new ud_itab_entry( ud_mnemonic_code.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1371 */ new ud_itab_entry( ud_mnemonic_code.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1372 */ new ud_itab_entry( ud_mnemonic_code.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1373 */ new ud_itab_entry( ud_mnemonic_code.UD_Isfence, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1374 */ new ud_itab_entry( ud_mnemonic_code.UD_Isgdt, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1375 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishld, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1376 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishld, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_CL, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1377 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishrd, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1378 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishrd, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_CL, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1379 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishufpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1380 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivshufpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1381 */ new ud_itab_entry( ud_mnemonic_code.UD_Ishufps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1382 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivshufps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1383 */ new ud_itab_entry( ud_mnemonic_code.UD_Isidt, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1384 */ new ud_itab_entry( ud_mnemonic_code.UD_Isldt, OpDefs.O_MwRv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1385 */ new ud_itab_entry( ud_mnemonic_code.UD_Ismsw, OpDefs.O_MwRv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1386 */ new ud_itab_entry( ud_mnemonic_code.UD_Ismsw, OpDefs.O_MwRv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1387 */ new ud_itab_entry( ud_mnemonic_code.UD_Isqrtps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1388 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivsqrtps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1389 */ new ud_itab_entry( ud_mnemonic_code.UD_Isqrtpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1390 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivsqrtpd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1391 */ new ud_itab_entry( ud_mnemonic_code.UD_Isqrtsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1392 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivsqrtsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1393 */ new ud_itab_entry( ud_mnemonic_code.UD_Isqrtss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1394 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivsqrtss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1395 */ new ud_itab_entry( ud_mnemonic_code.UD_Istc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1396 */ new ud_itab_entry( ud_mnemonic_code.UD_Istd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1397 */ new ud_itab_entry( ud_mnemonic_code.UD_Istgi, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1398 */ new ud_itab_entry( ud_mnemonic_code.UD_Isti, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1399 */ new ud_itab_entry( ud_mnemonic_code.UD_Iskinit, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1400 */ new ud_itab_entry( ud_mnemonic_code.UD_Istmxcsr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1401 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivstmxcsr, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1402 */ new ud_itab_entry( ud_mnemonic_code.UD_Istosb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg ),
            /* 1403 */ new ud_itab_entry( ud_mnemonic_code.UD_Istosw, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg|BitOps.P_oso|BitOps.P_rexw ),
            /* 1404 */ new ud_itab_entry( ud_mnemonic_code.UD_Istosd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg|BitOps.P_oso|BitOps.P_rexw ),
            /* 1405 */ new ud_itab_entry( ud_mnemonic_code.UD_Istosq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_str|BitOps.P_seg|BitOps.P_oso|BitOps.P_rexw ),
            /* 1406 */ new ud_itab_entry( ud_mnemonic_code.UD_Istr, OpDefs.O_MwRv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1407 */ new ud_itab_entry( ud_mnemonic_code.UD_Isub, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1408 */ new ud_itab_entry( ud_mnemonic_code.UD_Isub, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1409 */ new ud_itab_entry( ud_mnemonic_code.UD_Isub, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1410 */ new ud_itab_entry( ud_mnemonic_code.UD_Isub, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1411 */ new ud_itab_entry( ud_mnemonic_code.UD_Isub, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1412 */ new ud_itab_entry( ud_mnemonic_code.UD_Isub, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 1413 */ new ud_itab_entry( ud_mnemonic_code.UD_Isub, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1414 */ new ud_itab_entry( ud_mnemonic_code.UD_Isub, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1415 */ new ud_itab_entry( ud_mnemonic_code.UD_Isub, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_inv64 ),
            /* 1416 */ new ud_itab_entry( ud_mnemonic_code.UD_Isub, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1417 */ new ud_itab_entry( ud_mnemonic_code.UD_Isubpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1418 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivsubpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1419 */ new ud_itab_entry( ud_mnemonic_code.UD_Isubps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1420 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivsubps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1421 */ new ud_itab_entry( ud_mnemonic_code.UD_Isubsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1422 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivsubsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1423 */ new ud_itab_entry( ud_mnemonic_code.UD_Isubss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1424 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivsubss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1425 */ new ud_itab_entry( ud_mnemonic_code.UD_Iswapgs, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1426 */ new ud_itab_entry( ud_mnemonic_code.UD_Isyscall, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1427 */ new ud_itab_entry( ud_mnemonic_code.UD_Isysenter, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1428 */ new ud_itab_entry( ud_mnemonic_code.UD_Isysenter, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1429 */ new ud_itab_entry( ud_mnemonic_code.UD_Isysexit, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1430 */ new ud_itab_entry( ud_mnemonic_code.UD_Isysexit, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1431 */ new ud_itab_entry( ud_mnemonic_code.UD_Isysret, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1432 */ new ud_itab_entry( ud_mnemonic_code.UD_Itest, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1433 */ new ud_itab_entry( ud_mnemonic_code.UD_Itest, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1434 */ new ud_itab_entry( ud_mnemonic_code.UD_Itest, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1435 */ new ud_itab_entry( ud_mnemonic_code.UD_Itest, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1436 */ new ud_itab_entry( ud_mnemonic_code.UD_Itest, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 1437 */ new ud_itab_entry( ud_mnemonic_code.UD_Itest, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1438 */ new ud_itab_entry( ud_mnemonic_code.UD_Itest, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1439 */ new ud_itab_entry( ud_mnemonic_code.UD_Itest, OpDefs.O_Ev, OpDefs.O_Iz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1440 */ new ud_itab_entry( ud_mnemonic_code.UD_Iucomisd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1441 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivucomisd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1442 */ new ud_itab_entry( ud_mnemonic_code.UD_Iucomiss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1443 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivucomiss, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1444 */ new ud_itab_entry( ud_mnemonic_code.UD_Iud2, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1445 */ new ud_itab_entry( ud_mnemonic_code.UD_Iunpckhpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1446 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivunpckhpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1447 */ new ud_itab_entry( ud_mnemonic_code.UD_Iunpckhps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1448 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivunpckhps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1449 */ new ud_itab_entry( ud_mnemonic_code.UD_Iunpcklps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1450 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivunpcklps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1451 */ new ud_itab_entry( ud_mnemonic_code.UD_Iunpcklpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1452 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivunpcklpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1453 */ new ud_itab_entry( ud_mnemonic_code.UD_Iverr, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1454 */ new ud_itab_entry( ud_mnemonic_code.UD_Iverw, OpDefs.O_Ew, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1455 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmcall, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1456 */ new ud_itab_entry( ud_mnemonic_code.UD_Irdrand, OpDefs.O_R, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1457 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmclear, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1458 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmxon, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1459 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmptrld, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1460 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmptrst, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1461 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmlaunch, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1462 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmresume, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1463 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmxoff, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1464 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmread, OpDefs.O_Ey, OpDefs.O_Gy, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1465 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmwrite, OpDefs.O_Gy, OpDefs.O_Ey, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_def64 ),
            /* 1466 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmrun, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1467 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmmcall, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1468 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmload, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1469 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmsave, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1470 */ new ud_itab_entry( ud_mnemonic_code.UD_Iwait, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1471 */ new ud_itab_entry( ud_mnemonic_code.UD_Iwbinvd, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1472 */ new ud_itab_entry( ud_mnemonic_code.UD_Iwrmsr, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1473 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixadd, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1474 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixadd, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1475 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixchg, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1476 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixchg, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1477 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixchg, OpDefs.O_R0v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1478 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixchg, OpDefs.O_R1v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1479 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixchg, OpDefs.O_R2v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1480 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixchg, OpDefs.O_R3v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1481 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixchg, OpDefs.O_R4v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1482 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixchg, OpDefs.O_R5v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1483 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixchg, OpDefs.O_R6v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1484 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixchg, OpDefs.O_R7v, OpDefs.O_rAX, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1485 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixgetbv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1486 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixlatb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexw|BitOps.P_seg ),
            /* 1487 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixor, OpDefs.O_Eb, OpDefs.O_Gb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1488 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixor, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1489 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixor, OpDefs.O_Gb, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1490 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixor, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1491 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixor, OpDefs.O_AL, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1492 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixor, OpDefs.O_rAX, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw ),
            /* 1493 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixor, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1494 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixor, OpDefs.O_Ev, OpDefs.O_sIz, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1495 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixor, OpDefs.O_Eb, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_inv64 ),
            /* 1496 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixor, OpDefs.O_Ev, OpDefs.O_sIb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1497 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixorpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1498 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivxorpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1499 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixorps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1500 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivxorps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1501 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixcryptecb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1502 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixcryptcbc, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1503 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixcryptctr, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1504 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixcryptcfb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1505 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixcryptofb, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1506 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixrstor, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1507 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixsave, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1508 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixsetbv, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1509 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixsha1, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1510 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixsha256, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1511 */ new ud_itab_entry( ud_mnemonic_code.UD_Ixstore, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1512 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipclmulqdq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1513 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpclmulqdq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1514 */ new ud_itab_entry( ud_mnemonic_code.UD_Igetsec, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1515 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovdqa, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1516 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovdqa, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1517 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovdqa, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1518 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovdqa, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1519 */ new ud_itab_entry( ud_mnemonic_code.UD_Imaskmovdqu, OpDefs.O_V, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1520 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmaskmovdqu, OpDefs.O_Vx, OpDefs.O_Ux, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1521 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovdq2q, OpDefs.O_P, OpDefs.O_U, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexb ),
            /* 1522 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovdqu, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1523 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovdqu, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1524 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovdqu, OpDefs.O_W, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1525 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovdqu, OpDefs.O_Wx, OpDefs.O_Vx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1526 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovq2dq, OpDefs.O_V, OpDefs.O_N, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr ),
            /* 1527 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1528 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipaddq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1529 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpaddq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1530 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1531 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsubq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1532 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsubq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1533 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmuludq, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1534 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmuludq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1535 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipshufhw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1536 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpshufhw, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1537 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipshuflw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1538 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpshuflw, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1539 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipshufd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1540 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpshufd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1541 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipslldq, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1542 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpslldq, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1543 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsrldq, OpDefs.O_U, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1544 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsrldq, OpDefs.O_Hx, OpDefs.O_Ux, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_rexb ),
            /* 1545 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpckhqdq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1546 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpunpckhqdq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1547 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipunpcklqdq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1548 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpunpcklqdq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1549 */ new ud_itab_entry( ud_mnemonic_code.UD_Ihaddpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1550 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivhaddpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1551 */ new ud_itab_entry( ud_mnemonic_code.UD_Ihaddps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1552 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivhaddps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1553 */ new ud_itab_entry( ud_mnemonic_code.UD_Ihsubpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1554 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivhsubpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1555 */ new ud_itab_entry( ud_mnemonic_code.UD_Ihsubps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1556 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivhsubps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1557 */ new ud_itab_entry( ud_mnemonic_code.UD_Iinsertps, OpDefs.O_V, OpDefs.O_Md, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1558 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivinsertps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Md, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1559 */ new ud_itab_entry( ud_mnemonic_code.UD_Ilddqu, OpDefs.O_V, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1560 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivlddqu, OpDefs.O_Vx, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1561 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovddup, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1562 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovddup, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1563 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovddup, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1564 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovddup, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1565 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovshdup, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1566 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovshdup, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1567 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovshdup, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1568 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovshdup, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1569 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovsldup, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1570 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovsldup, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1571 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovsldup, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1572 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovsldup, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1573 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipabsb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1574 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipabsb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1575 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpabsb, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1576 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipabsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1577 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipabsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1578 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpabsw, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1579 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipabsd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1580 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipabsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1581 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpabsd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1582 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipshufb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1583 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipshufb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1584 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpshufb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1585 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphaddw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1586 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphaddw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1587 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivphaddw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1588 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphaddd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1589 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphaddd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1590 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivphaddd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1591 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphaddsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1592 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphaddsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1593 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivphaddsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1594 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmaddubsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1595 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmaddubsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1596 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmaddubsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1597 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphsubw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1598 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphsubw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1599 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivphsubw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1600 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphsubd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1601 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphsubd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1602 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivphsubd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1603 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphsubsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1604 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphsubsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1605 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivphsubsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1606 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsignb, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1607 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsignb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1608 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsignb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1609 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsignd, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1610 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsignd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1611 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsignd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1612 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsignw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1613 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipsignw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1614 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsignw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1615 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmulhrsw, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1616 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmulhrsw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1617 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmulhrsw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1618 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipalignr, OpDefs.O_P, OpDefs.O_Q, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1619 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipalignr, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1620 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpalignr, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1621 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipblendvb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1622 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmuldq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1623 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmuldq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1624 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipminsb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1625 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpminsb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1626 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipminsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1627 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpminsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1628 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipminuw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1629 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpminuw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1630 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipminud, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1631 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpminud, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1632 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmaxsb, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1633 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmaxsb, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1634 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmaxsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1635 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmaxsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1636 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmaxud, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1637 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmaxud, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1638 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmaxuw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1639 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmaxuw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1640 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmulld, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1641 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmulld, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1642 */ new ud_itab_entry( ud_mnemonic_code.UD_Iphminposuw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1643 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivphminposuw, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1644 */ new ud_itab_entry( ud_mnemonic_code.UD_Iroundps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1645 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivroundps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1646 */ new ud_itab_entry( ud_mnemonic_code.UD_Iroundpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1647 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivroundpd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1648 */ new ud_itab_entry( ud_mnemonic_code.UD_Iroundss, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1649 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivroundss, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1650 */ new ud_itab_entry( ud_mnemonic_code.UD_Iroundsd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1651 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivroundsd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1652 */ new ud_itab_entry( ud_mnemonic_code.UD_Iblendpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1653 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivblendpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1654 */ new ud_itab_entry( ud_mnemonic_code.UD_Iblendps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1655 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivblendps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1656 */ new ud_itab_entry( ud_mnemonic_code.UD_Iblendvpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1657 */ new ud_itab_entry( ud_mnemonic_code.UD_Iblendvps, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1658 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibound, OpDefs.O_Gv, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso ),
            /* 1659 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibsf, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1660 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibsr, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1661 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibswap, OpDefs.O_R0y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1662 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibswap, OpDefs.O_R1y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1663 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibswap, OpDefs.O_R2y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1664 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibswap, OpDefs.O_R3y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1665 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibswap, OpDefs.O_R4y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1666 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibswap, OpDefs.O_R5y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1667 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibswap, OpDefs.O_R6y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1668 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibswap, OpDefs.O_R7y, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexb ),
            /* 1669 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibt, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1670 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibt, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1671 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibtc, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1672 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibtc, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1673 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibtr, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1674 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibtr, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1675 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibts, OpDefs.O_Ev, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1676 */ new ud_itab_entry( ud_mnemonic_code.UD_Ibts, OpDefs.O_Ev, OpDefs.O_Ib, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexw|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1677 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipblendw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1678 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpblendw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1679 */ new ud_itab_entry( ud_mnemonic_code.UD_Impsadbw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1680 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmpsadbw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1681 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovntdqa, OpDefs.O_V, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1682 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovntdqa, OpDefs.O_Vx, OpDefs.O_M, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1683 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipackusdw, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1684 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpackusdw, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1685 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovsxbw, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1686 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmovsxbw, OpDefs.O_Vx, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1687 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovsxbd, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1688 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmovsxbd, OpDefs.O_Vx, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1689 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovsxbq, OpDefs.O_V, OpDefs.O_MwU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1690 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmovsxbq, OpDefs.O_Vx, OpDefs.O_MwU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1691 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovsxwd, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1692 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmovsxwd, OpDefs.O_Vx, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1693 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovsxwq, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1694 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmovsxwq, OpDefs.O_Vx, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1695 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovsxdq, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1696 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovzxbw, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1697 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmovzxbw, OpDefs.O_Vx, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1698 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovzxbd, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1699 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmovzxbd, OpDefs.O_Vx, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1700 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovzxbq, OpDefs.O_V, OpDefs.O_MwU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1701 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmovzxbq, OpDefs.O_Vx, OpDefs.O_MwU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1702 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovzxwd, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1703 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmovzxwd, OpDefs.O_Vx, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1704 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovzxwq, OpDefs.O_V, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1705 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmovzxwq, OpDefs.O_Vx, OpDefs.O_MdU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1706 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipmovzxdq, OpDefs.O_V, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1707 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpmovzxdq, OpDefs.O_Vx, OpDefs.O_MqU, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1708 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpeqq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1709 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpcmpeqq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1710 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipopcnt, OpDefs.O_Gv, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1711 */ new ud_itab_entry( ud_mnemonic_code.UD_Iptest, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1712 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivptest, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1713 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpestri, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1714 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpcmpestri, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1715 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpestrm, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1716 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpcmpestrm, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1717 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpgtq, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1718 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpcmpgtq, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1719 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpistri, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1720 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpcmpistri, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1721 */ new ud_itab_entry( ud_mnemonic_code.UD_Ipcmpistrm, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1722 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpcmpistrm, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1723 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovbe, OpDefs.O_Gv, OpDefs.O_Mv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1724 */ new ud_itab_entry( ud_mnemonic_code.UD_Imovbe, OpDefs.O_Mv, OpDefs.O_Gv, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1725 */ new ud_itab_entry( ud_mnemonic_code.UD_Icrc32, OpDefs.O_Gy, OpDefs.O_Eb, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1726 */ new ud_itab_entry( ud_mnemonic_code.UD_Icrc32, OpDefs.O_Gy, OpDefs.O_Ev, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_oso|BitOps.P_rexr|BitOps.P_rexw|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1727 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivbroadcastss, OpDefs.O_V, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1728 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivbroadcastsd, OpDefs.O_Vqq, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1729 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivextractf128, OpDefs.O_Wdq, OpDefs.O_Vqq, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1730 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivinsertf128, OpDefs.O_Vqq, OpDefs.O_Hqq, OpDefs.O_Wdq, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1731 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmaskmovps, OpDefs.O_V, OpDefs.O_H, OpDefs.O_M, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1732 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmaskmovps, OpDefs.O_M, OpDefs.O_H, OpDefs.O_V, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1733 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmaskmovpd, OpDefs.O_V, OpDefs.O_H, OpDefs.O_M, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1734 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmaskmovpd, OpDefs.O_M, OpDefs.O_H, OpDefs.O_V, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1735 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpermilpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1736 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpermilpd, OpDefs.O_V, OpDefs.O_W, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1737 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpermilps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1738 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpermilps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_Ib, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1739 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivperm2f128, OpDefs.O_Vqq, OpDefs.O_Hqq, OpDefs.O_Wqq, OpDefs.O_Ib, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1740 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivtestps, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1741 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivtestpd, OpDefs.O_Vx, OpDefs.O_Wx, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1742 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivzeroupper, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1743 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivzeroall, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_none ),
            /* 1744 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivblendvpd, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Lx, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1745 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivblendvps, OpDefs.O_Vx, OpDefs.O_Hx, OpDefs.O_Wx, OpDefs.O_Lx, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb|BitOps.P_vexl ),
            /* 1746 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovsd, OpDefs.O_V, OpDefs.O_H, OpDefs.O_U, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1747 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovsd, OpDefs.O_V, OpDefs.O_Mq, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1748 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovsd, OpDefs.O_U, OpDefs.O_H, OpDefs.O_V, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1749 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovsd, OpDefs.O_Mq, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1750 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovss, OpDefs.O_V, OpDefs.O_H, OpDefs.O_U, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1751 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovss, OpDefs.O_V, OpDefs.O_Md, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1752 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovss, OpDefs.O_U, OpDefs.O_H, OpDefs.O_V, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1753 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivmovss, OpDefs.O_Md, OpDefs.O_V, OpDefs.O_NONE, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1754 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpblendvb, OpDefs.O_V, OpDefs.O_H, OpDefs.O_W, OpDefs.O_L, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1755 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsllw, OpDefs.O_V, OpDefs.O_H, OpDefs.O_W, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1756 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsllw, OpDefs.O_H, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1757 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpslld, OpDefs.O_V, OpDefs.O_H, OpDefs.O_W, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1758 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpslld, OpDefs.O_H, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1759 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsllq, OpDefs.O_V, OpDefs.O_H, OpDefs.O_W, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
            /* 1760 */ new ud_itab_entry( ud_mnemonic_code.UD_Ivpsllq, OpDefs.O_H, OpDefs.O_V, OpDefs.O_W, OpDefs.O_NONE, BitOps.P_aso|BitOps.P_rexr|BitOps.P_rexx|BitOps.P_rexb ),
        };
        
        
        internal static readonly string[] ud_mnemonics_str = new string[]
        {
            "aaa",
            "aad",
            "aam",
            "aas",
            "adc",
            "add",
            "addpd",
            "addps",
            "addsd",
            "addss",
            "addsubpd",
            "addsubps",
            "aesdec",
            "aesdeclast",
            "aesenc",
            "aesenclast",
            "aesimc",
            "aeskeygenassist",
            "and",
            "andnpd",
            "andnps",
            "andpd",
            "andps",
            "arpl",
            "blendpd",
            "blendps",
            "blendvpd",
            "blendvps",
            "bound",
            "bsf",
            "bsr",
            "bswap",
            "bt",
            "btc",
            "btr",
            "bts",
            "call",
            "cbw",
            "cdq",
            "cdqe",
            "clc",
            "cld",
            "clflush",
            "clgi",
            "cli",
            "clts",
            "cmc",
            "cmova",
            "cmovae",
            "cmovb",
            "cmovbe",
            "cmovg",
            "cmovge",
            "cmovl",
            "cmovle",
            "cmovno",
            "cmovnp",
            "cmovns",
            "cmovnz",
            "cmovo",
            "cmovp",
            "cmovs",
            "cmovz",
            "cmp",
            "cmppd",
            "cmpps",
            "cmpsb",
            "cmpsd",
            "cmpsq",
            "cmpss",
            "cmpsw",
            "cmpxchg",
            "cmpxchg16b",
            "cmpxchg8b",
            "comisd",
            "comiss",
            "cpuid",
            "cqo",
            "crc32",
            "cvtdq2pd",
            "cvtdq2ps",
            "cvtpd2dq",
            "cvtpd2pi",
            "cvtpd2ps",
            "cvtpi2pd",
            "cvtpi2ps",
            "cvtps2dq",
            "cvtps2pd",
            "cvtps2pi",
            "cvtsd2si",
            "cvtsd2ss",
            "cvtsi2sd",
            "cvtsi2ss",
            "cvtss2sd",
            "cvtss2si",
            "cvttpd2dq",
            "cvttpd2pi",
            "cvttps2dq",
            "cvttps2pi",
            "cvttsd2si",
            "cvttss2si",
            "cwd",
            "cwde",
            "daa",
            "das",
            "dec",
            "div",
            "divpd",
            "divps",
            "divsd",
            "divss",
            "dppd",
            "dpps",
            "emms",
            "enter",
            "extractps",
            "f2xm1",
            "fabs",
            "fadd",
            "faddp",
            "fbld",
            "fbstp",
            "fchs",
            "fclex",
            "fcmovb",
            "fcmovbe",
            "fcmove",
            "fcmovnb",
            "fcmovnbe",
            "fcmovne",
            "fcmovnu",
            "fcmovu",
            "fcom",
            "fcom2",
            "fcomi",
            "fcomip",
            "fcomp",
            "fcomp3",
            "fcomp5",
            "fcompp",
            "fcos",
            "fdecstp",
            "fdiv",
            "fdivp",
            "fdivr",
            "fdivrp",
            "femms",
            "ffree",
            "ffreep",
            "fiadd",
            "ficom",
            "ficomp",
            "fidiv",
            "fidivr",
            "fild",
            "fimul",
            "fincstp",
            "fist",
            "fistp",
            "fisttp",
            "fisub",
            "fisubr",
            "fld",
            "fld1",
            "fldcw",
            "fldenv",
            "fldl2e",
            "fldl2t",
            "fldlg2",
            "fldln2",
            "fldpi",
            "fldz",
            "fmul",
            "fmulp",
            "fndisi",
            "fneni",
            "fninit",
            "fnop",
            "fnsave",
            "fnsetpm",
            "fnstcw",
            "fnstenv",
            "fnstsw",
            "fpatan",
            "fprem",
            "fprem1",
            "fptan",
            "frndint",
            "frstor",
            "frstpm",
            "fscale",
            "fsin",
            "fsincos",
            "fsqrt",
            "fst",
            "fstp",
            "fstp1",
            "fstp8",
            "fstp9",
            "fsub",
            "fsubp",
            "fsubr",
            "fsubrp",
            "ftst",
            "fucom",
            "fucomi",
            "fucomip",
            "fucomp",
            "fucompp",
            "fxam",
            "fxch",
            "fxch4",
            "fxch7",
            "fxrstor",
            "fxsave",
            "fxtract",
            "fyl2x",
            "fyl2xp1",
            "getsec",
            "haddpd",
            "haddps",
            "hlt",
            "hsubpd",
            "hsubps",
            "idiv",
            "imul",
            "in",
            "inc",
            "insb",
            "insd",
            "insertps",
            "insw",
            "int",
            "int1",
            "int3",
            "into",
            "invd",
            "invept",
            "invlpg",
            "invlpga",
            "invvpid",
            "iretd",
            "iretq",
            "iretw",
            "ja",
            "jae",
            "jb",
            "jbe",
            "jcxz",
            "jecxz",
            "jg",
            "jge",
            "jl",
            "jle",
            "jmp",
            "jno",
            "jnp",
            "jns",
            "jnz",
            "jo",
            "jp",
            "jrcxz",
            "js",
            "jz",
            "lahf",
            "lar",
            "lddqu",
            "ldmxcsr",
            "lds",
            "lea",
            "leave",
            "les",
            "lfence",
            "lfs",
            "lgdt",
            "lgs",
            "lidt",
            "lldt",
            "lmsw",
            "lock",
            "lodsb",
            "lodsd",
            "lodsq",
            "lodsw",
            "loop",
            "loope",
            "loopne",
            "lsl",
            "lss",
            "ltr",
            "maskmovdqu",
            "maskmovq",
            "maxpd",
            "maxps",
            "maxsd",
            "maxss",
            "mfence",
            "minpd",
            "minps",
            "minsd",
            "minss",
            "monitor",
            "montmul",
            "mov",
            "movapd",
            "movaps",
            "movbe",
            "movd",
            "movddup",
            "movdq2q",
            "movdqa",
            "movdqu",
            "movhlps",
            "movhpd",
            "movhps",
            "movlhps",
            "movlpd",
            "movlps",
            "movmskpd",
            "movmskps",
            "movntdq",
            "movntdqa",
            "movnti",
            "movntpd",
            "movntps",
            "movntq",
            "movq",
            "movq2dq",
            "movsb",
            "movsd",
            "movshdup",
            "movsldup",
            "movsq",
            "movss",
            "movsw",
            "movsx",
            "movsxd",
            "movupd",
            "movups",
            "movzx",
            "mpsadbw",
            "mul",
            "mulpd",
            "mulps",
            "mulsd",
            "mulss",
            "mwait",
            "neg",
            "nop",
            "not",
            "or",
            "orpd",
            "orps",
            "out",
            "outsb",
            "outsd",
            "outsw",
            "pabsb",
            "pabsd",
            "pabsw",
            "packssdw",
            "packsswb",
            "packusdw",
            "packuswb",
            "paddb",
            "paddd",
            "paddq",
            "paddsb",
            "paddsw",
            "paddusb",
            "paddusw",
            "paddw",
            "palignr",
            "pand",
            "pandn",
            "pavgb",
            "pavgusb",
            "pavgw",
            "pblendvb",
            "pblendw",
            "pclmulqdq",
            "pcmpeqb",
            "pcmpeqd",
            "pcmpeqq",
            "pcmpeqw",
            "pcmpestri",
            "pcmpestrm",
            "pcmpgtb",
            "pcmpgtd",
            "pcmpgtq",
            "pcmpgtw",
            "pcmpistri",
            "pcmpistrm",
            "pextrb",
            "pextrd",
            "pextrq",
            "pextrw",
            "pf2id",
            "pf2iw",
            "pfacc",
            "pfadd",
            "pfcmpeq",
            "pfcmpge",
            "pfcmpgt",
            "pfmax",
            "pfmin",
            "pfmul",
            "pfnacc",
            "pfpnacc",
            "pfrcp",
            "pfrcpit1",
            "pfrcpit2",
            "pfrsqit1",
            "pfrsqrt",
            "pfsub",
            "pfsubr",
            "phaddd",
            "phaddsw",
            "phaddw",
            "phminposuw",
            "phsubd",
            "phsubsw",
            "phsubw",
            "pi2fd",
            "pi2fw",
            "pinsrb",
            "pinsrd",
            "pinsrq",
            "pinsrw",
            "pmaddubsw",
            "pmaddwd",
            "pmaxsb",
            "pmaxsd",
            "pmaxsw",
            "pmaxub",
            "pmaxud",
            "pmaxuw",
            "pminsb",
            "pminsd",
            "pminsw",
            "pminub",
            "pminud",
            "pminuw",
            "pmovmskb",
            "pmovsxbd",
            "pmovsxbq",
            "pmovsxbw",
            "pmovsxdq",
            "pmovsxwd",
            "pmovsxwq",
            "pmovzxbd",
            "pmovzxbq",
            "pmovzxbw",
            "pmovzxdq",
            "pmovzxwd",
            "pmovzxwq",
            "pmuldq",
            "pmulhrsw",
            "pmulhrw",
            "pmulhuw",
            "pmulhw",
            "pmulld",
            "pmullw",
            "pmuludq",
            "pop",
            "popa",
            "popad",
            "popcnt",
            "popfd",
            "popfq",
            "popfw",
            "por",
            "prefetch",
            "prefetchnta",
            "prefetcht0",
            "prefetcht1",
            "prefetcht2",
            "psadbw",
            "pshufb",
            "pshufd",
            "pshufhw",
            "pshuflw",
            "pshufw",
            "psignb",
            "psignd",
            "psignw",
            "pslld",
            "pslldq",
            "psllq",
            "psllw",
            "psrad",
            "psraw",
            "psrld",
            "psrldq",
            "psrlq",
            "psrlw",
            "psubb",
            "psubd",
            "psubq",
            "psubsb",
            "psubsw",
            "psubusb",
            "psubusw",
            "psubw",
            "pswapd",
            "ptest",
            "punpckhbw",
            "punpckhdq",
            "punpckhqdq",
            "punpckhwd",
            "punpcklbw",
            "punpckldq",
            "punpcklqdq",
            "punpcklwd",
            "push",
            "pusha",
            "pushad",
            "pushfd",
            "pushfq",
            "pushfw",
            "pxor",
            "rcl",
            "rcpps",
            "rcpss",
            "rcr",
            "rdmsr",
            "rdpmc",
            "rdrand",
            "rdtsc",
            "rdtscp",
            "rep",
            "repne",
            "ret",
            "retf",
            "rol",
            "ror",
            "roundpd",
            "roundps",
            "roundsd",
            "roundss",
            "rsm",
            "rsqrtps",
            "rsqrtss",
            "sahf",
            "salc",
            "sar",
            "sbb",
            "scasb",
            "scasd",
            "scasq",
            "scasw",
            "seta",
            "setae",
            "setb",
            "setbe",
            "setg",
            "setge",
            "setl",
            "setle",
            "setno",
            "setnp",
            "setns",
            "setnz",
            "seto",
            "setp",
            "sets",
            "setz",
            "sfence",
            "sgdt",
            "shl",
            "shld",
            "shr",
            "shrd",
            "shufpd",
            "shufps",
            "sidt",
            "skinit",
            "sldt",
            "smsw",
            "sqrtpd",
            "sqrtps",
            "sqrtsd",
            "sqrtss",
            "stc",
            "std",
            "stgi",
            "sti",
            "stmxcsr",
            "stosb",
            "stosd",
            "stosq",
            "stosw",
            "str",
            "sub",
            "subpd",
            "subps",
            "subsd",
            "subss",
            "swapgs",
            "syscall",
            "sysenter",
            "sysexit",
            "sysret",
            "test",
            "ucomisd",
            "ucomiss",
            "ud2",
            "unpckhpd",
            "unpckhps",
            "unpcklpd",
            "unpcklps",
            "vaddpd",
            "vaddps",
            "vaddsd",
            "vaddss",
            "vaddsubpd",
            "vaddsubps",
            "vaesdec",
            "vaesdeclast",
            "vaesenc",
            "vaesenclast",
            "vaesimc",
            "vaeskeygenassist",
            "vandnpd",
            "vandnps",
            "vandpd",
            "vandps",
            "vblendpd",
            "vblendps",
            "vblendvpd",
            "vblendvps",
            "vbroadcastsd",
            "vbroadcastss",
            "vcmppd",
            "vcmpps",
            "vcmpsd",
            "vcmpss",
            "vcomisd",
            "vcomiss",
            "vcvtdq2pd",
            "vcvtdq2ps",
            "vcvtpd2dq",
            "vcvtpd2ps",
            "vcvtps2dq",
            "vcvtps2pd",
            "vcvtsd2si",
            "vcvtsd2ss",
            "vcvtsi2sd",
            "vcvtsi2ss",
            "vcvtss2sd",
            "vcvtss2si",
            "vcvttpd2dq",
            "vcvttps2dq",
            "vcvttsd2si",
            "vcvttss2si",
            "vdivpd",
            "vdivps",
            "vdivsd",
            "vdivss",
            "vdppd",
            "vdpps",
            "verr",
            "verw",
            "vextractf128",
            "vextractps",
            "vhaddpd",
            "vhaddps",
            "vhsubpd",
            "vhsubps",
            "vinsertf128",
            "vinsertps",
            "vlddqu",
            "vmaskmovdqu",
            "vmaskmovpd",
            "vmaskmovps",
            "vmaxpd",
            "vmaxps",
            "vmaxsd",
            "vmaxss",
            "vmcall",
            "vmclear",
            "vminpd",
            "vminps",
            "vminsd",
            "vminss",
            "vmlaunch",
            "vmload",
            "vmmcall",
            "vmovapd",
            "vmovaps",
            "vmovd",
            "vmovddup",
            "vmovdqa",
            "vmovdqu",
            "vmovhlps",
            "vmovhpd",
            "vmovhps",
            "vmovlhps",
            "vmovlpd",
            "vmovlps",
            "vmovmskpd",
            "vmovmskps",
            "vmovntdq",
            "vmovntdqa",
            "vmovntpd",
            "vmovntps",
            "vmovq",
            "vmovsd",
            "vmovshdup",
            "vmovsldup",
            "vmovss",
            "vmovupd",
            "vmovups",
            "vmpsadbw",
            "vmptrld",
            "vmptrst",
            "vmread",
            "vmresume",
            "vmrun",
            "vmsave",
            "vmulpd",
            "vmulps",
            "vmulsd",
            "vmulss",
            "vmwrite",
            "vmxoff",
            "vmxon",
            "vorpd",
            "vorps",
            "vpabsb",
            "vpabsd",
            "vpabsw",
            "vpackssdw",
            "vpacksswb",
            "vpackusdw",
            "vpackuswb",
            "vpaddb",
            "vpaddd",
            "vpaddq",
            "vpaddsb",
            "vpaddsw",
            "vpaddusb",
            "vpaddusw",
            "vpaddw",
            "vpalignr",
            "vpand",
            "vpandn",
            "vpavgb",
            "vpavgw",
            "vpblendvb",
            "vpblendw",
            "vpclmulqdq",
            "vpcmpeqb",
            "vpcmpeqd",
            "vpcmpeqq",
            "vpcmpeqw",
            "vpcmpestri",
            "vpcmpestrm",
            "vpcmpgtb",
            "vpcmpgtd",
            "vpcmpgtq",
            "vpcmpgtw",
            "vpcmpistri",
            "vpcmpistrm",
            "vperm2f128",
            "vpermilpd",
            "vpermilps",
            "vpextrb",
            "vpextrd",
            "vpextrq",
            "vpextrw",
            "vphaddd",
            "vphaddsw",
            "vphaddw",
            "vphminposuw",
            "vphsubd",
            "vphsubsw",
            "vphsubw",
            "vpinsrb",
            "vpinsrd",
            "vpinsrq",
            "vpinsrw",
            "vpmaddubsw",
            "vpmaddwd",
            "vpmaxsb",
            "vpmaxsd",
            "vpmaxsw",
            "vpmaxub",
            "vpmaxud",
            "vpmaxuw",
            "vpminsb",
            "vpminsd",
            "vpminsw",
            "vpminub",
            "vpminud",
            "vpminuw",
            "vpmovmskb",
            "vpmovsxbd",
            "vpmovsxbq",
            "vpmovsxbw",
            "vpmovsxwd",
            "vpmovsxwq",
            "vpmovzxbd",
            "vpmovzxbq",
            "vpmovzxbw",
            "vpmovzxdq",
            "vpmovzxwd",
            "vpmovzxwq",
            "vpmuldq",
            "vpmulhrsw",
            "vpmulhuw",
            "vpmulhw",
            "vpmulld",
            "vpmullw",
            "vpor",
            "vpsadbw",
            "vpshufb",
            "vpshufd",
            "vpshufhw",
            "vpshuflw",
            "vpsignb",
            "vpsignd",
            "vpsignw",
            "vpslld",
            "vpslldq",
            "vpsllq",
            "vpsllw",
            "vpsrad",
            "vpsraw",
            "vpsrld",
            "vpsrldq",
            "vpsrlq",
            "vpsrlw",
            "vpsubb",
            "vpsubd",
            "vpsubq",
            "vpsubsb",
            "vpsubsw",
            "vpsubusb",
            "vpsubusw",
            "vpsubw",
            "vptest",
            "vpunpckhbw",
            "vpunpckhdq",
            "vpunpckhqdq",
            "vpunpckhwd",
            "vpunpcklbw",
            "vpunpckldq",
            "vpunpcklqdq",
            "vpunpcklwd",
            "vpxor",
            "vrcpps",
            "vrcpss",
            "vroundpd",
            "vroundps",
            "vroundsd",
            "vroundss",
            "vrsqrtps",
            "vrsqrtss",
            "vshufpd",
            "vshufps",
            "vsqrtpd",
            "vsqrtps",
            "vsqrtsd",
            "vsqrtss",
            "vstmxcsr",
            "vsubpd",
            "vsubps",
            "vsubsd",
            "vsubss",
            "vtestpd",
            "vtestps",
            "vucomisd",
            "vucomiss",
            "vunpckhpd",
            "vunpckhps",
            "vunpcklpd",
            "vunpcklps",
            "vxorpd",
            "vxorps",
            "vzeroall",
            "vzeroupper",
            "wait",
            "wbinvd",
            "wrmsr",
            "xadd",
            "xchg",
            "xcryptcbc",
            "xcryptcfb",
            "xcryptctr",
            "xcryptecb",
            "xcryptofb",
            "xgetbv",
            "xlatb",
            "xor",
            "xorpd",
            "xorps",
            "xrstor",
            "xsave",
            "xsetbv",
            "xsha1",
            "xsha256",
            "xstore",
            "invalid",
            "3dnow",
            "none",
            "db",
            "pause"
        };
        #endregion
    }
    
    #region Enums
    
    public enum ud_table_type
    {
        UD_TAB__OPC_TABLE,
        UD_TAB__OPC_SSE,
        UD_TAB__OPC_REG,
        UD_TAB__OPC_RM,
        UD_TAB__OPC_MOD,
        UD_TAB__OPC_MODE,
        UD_TAB__OPC_X87,
        UD_TAB__OPC_ASIZE,
        UD_TAB__OPC_OSIZE,
        UD_TAB__OPC_3DNOW,
        UD_TAB__OPC_VENDOR,
        UD_TAB__OPC_VEX,
        UD_TAB__OPC_VEX_W,
        UD_TAB__OPC_VEX_L
    }
    
    public enum ud_mnemonic_code
    {
        UD_Iaaa,
        UD_Iaad,
        UD_Iaam,
        UD_Iaas,
        UD_Iadc,
        UD_Iadd,
        UD_Iaddpd,
        UD_Iaddps,
        UD_Iaddsd,
        UD_Iaddss,
        UD_Iaddsubpd,
        UD_Iaddsubps,
        UD_Iaesdec,
        UD_Iaesdeclast,
        UD_Iaesenc,
        UD_Iaesenclast,
        UD_Iaesimc,
        UD_Iaeskeygenassist,
        UD_Iand,
        UD_Iandnpd,
        UD_Iandnps,
        UD_Iandpd,
        UD_Iandps,
        UD_Iarpl,
        UD_Iblendpd,
        UD_Iblendps,
        UD_Iblendvpd,
        UD_Iblendvps,
        UD_Ibound,
        UD_Ibsf,
        UD_Ibsr,
        UD_Ibswap,
        UD_Ibt,
        UD_Ibtc,
        UD_Ibtr,
        UD_Ibts,
        UD_Icall,
        UD_Icbw,
        UD_Icdq,
        UD_Icdqe,
        UD_Iclc,
        UD_Icld,
        UD_Iclflush,
        UD_Iclgi,
        UD_Icli,
        UD_Iclts,
        UD_Icmc,
        UD_Icmova,
        UD_Icmovae,
        UD_Icmovb,
        UD_Icmovbe,
        UD_Icmovg,
        UD_Icmovge,
        UD_Icmovl,
        UD_Icmovle,
        UD_Icmovno,
        UD_Icmovnp,
        UD_Icmovns,
        UD_Icmovnz,
        UD_Icmovo,
        UD_Icmovp,
        UD_Icmovs,
        UD_Icmovz,
        UD_Icmp,
        UD_Icmppd,
        UD_Icmpps,
        UD_Icmpsb,
        UD_Icmpsd,
        UD_Icmpsq,
        UD_Icmpss,
        UD_Icmpsw,
        UD_Icmpxchg,
        UD_Icmpxchg16b,
        UD_Icmpxchg8b,
        UD_Icomisd,
        UD_Icomiss,
        UD_Icpuid,
        UD_Icqo,
        UD_Icrc32,
        UD_Icvtdq2pd,
        UD_Icvtdq2ps,
        UD_Icvtpd2dq,
        UD_Icvtpd2pi,
        UD_Icvtpd2ps,
        UD_Icvtpi2pd,
        UD_Icvtpi2ps,
        UD_Icvtps2dq,
        UD_Icvtps2pd,
        UD_Icvtps2pi,
        UD_Icvtsd2si,
        UD_Icvtsd2ss,
        UD_Icvtsi2sd,
        UD_Icvtsi2ss,
        UD_Icvtss2sd,
        UD_Icvtss2si,
        UD_Icvttpd2dq,
        UD_Icvttpd2pi,
        UD_Icvttps2dq,
        UD_Icvttps2pi,
        UD_Icvttsd2si,
        UD_Icvttss2si,
        UD_Icwd,
        UD_Icwde,
        UD_Idaa,
        UD_Idas,
        UD_Idec,
        UD_Idiv,
        UD_Idivpd,
        UD_Idivps,
        UD_Idivsd,
        UD_Idivss,
        UD_Idppd,
        UD_Idpps,
        UD_Iemms,
        UD_Ienter,
        UD_Iextractps,
        UD_If2xm1,
        UD_Ifabs,
        UD_Ifadd,
        UD_Ifaddp,
        UD_Ifbld,
        UD_Ifbstp,
        UD_Ifchs,
        UD_Ifclex,
        UD_Ifcmovb,
        UD_Ifcmovbe,
        UD_Ifcmove,
        UD_Ifcmovnb,
        UD_Ifcmovnbe,
        UD_Ifcmovne,
        UD_Ifcmovnu,
        UD_Ifcmovu,
        UD_Ifcom,
        UD_Ifcom2,
        UD_Ifcomi,
        UD_Ifcomip,
        UD_Ifcomp,
        UD_Ifcomp3,
        UD_Ifcomp5,
        UD_Ifcompp,
        UD_Ifcos,
        UD_Ifdecstp,
        UD_Ifdiv,
        UD_Ifdivp,
        UD_Ifdivr,
        UD_Ifdivrp,
        UD_Ifemms,
        UD_Iffree,
        UD_Iffreep,
        UD_Ifiadd,
        UD_Ificom,
        UD_Ificomp,
        UD_Ifidiv,
        UD_Ifidivr,
        UD_Ifild,
        UD_Ifimul,
        UD_Ifincstp,
        UD_Ifist,
        UD_Ifistp,
        UD_Ifisttp,
        UD_Ifisub,
        UD_Ifisubr,
        UD_Ifld,
        UD_Ifld1,
        UD_Ifldcw,
        UD_Ifldenv,
        UD_Ifldl2e,
        UD_Ifldl2t,
        UD_Ifldlg2,
        UD_Ifldln2,
        UD_Ifldpi,
        UD_Ifldz,
        UD_Ifmul,
        UD_Ifmulp,
        UD_Ifndisi,
        UD_Ifneni,
        UD_Ifninit,
        UD_Ifnop,
        UD_Ifnsave,
        UD_Ifnsetpm,
        UD_Ifnstcw,
        UD_Ifnstenv,
        UD_Ifnstsw,
        UD_Ifpatan,
        UD_Ifprem,
        UD_Ifprem1,
        UD_Ifptan,
        UD_Ifrndint,
        UD_Ifrstor,
        UD_Ifrstpm,
        UD_Ifscale,
        UD_Ifsin,
        UD_Ifsincos,
        UD_Ifsqrt,
        UD_Ifst,
        UD_Ifstp,
        UD_Ifstp1,
        UD_Ifstp8,
        UD_Ifstp9,
        UD_Ifsub,
        UD_Ifsubp,
        UD_Ifsubr,
        UD_Ifsubrp,
        UD_Iftst,
        UD_Ifucom,
        UD_Ifucomi,
        UD_Ifucomip,
        UD_Ifucomp,
        UD_Ifucompp,
        UD_Ifxam,
        UD_Ifxch,
        UD_Ifxch4,
        UD_Ifxch7,
        UD_Ifxrstor,
        UD_Ifxsave,
        UD_Ifxtract,
        UD_Ifyl2x,
        UD_Ifyl2xp1,
        UD_Igetsec,
        UD_Ihaddpd,
        UD_Ihaddps,
        UD_Ihlt,
        UD_Ihsubpd,
        UD_Ihsubps,
        UD_Iidiv,
        UD_Iimul,
        UD_Iin,
        UD_Iinc,
        UD_Iinsb,
        UD_Iinsd,
        UD_Iinsertps,
        UD_Iinsw,
        UD_Iint,
        UD_Iint1,
        UD_Iint3,
        UD_Iinto,
        UD_Iinvd,
        UD_Iinvept,
        UD_Iinvlpg,
        UD_Iinvlpga,
        UD_Iinvvpid,
        UD_Iiretd,
        UD_Iiretq,
        UD_Iiretw,
        UD_Ija,
        UD_Ijae,
        UD_Ijb,
        UD_Ijbe,
        UD_Ijcxz,
        UD_Ijecxz,
        UD_Ijg,
        UD_Ijge,
        UD_Ijl,
        UD_Ijle,
        UD_Ijmp,
        UD_Ijno,
        UD_Ijnp,
        UD_Ijns,
        UD_Ijnz,
        UD_Ijo,
        UD_Ijp,
        UD_Ijrcxz,
        UD_Ijs,
        UD_Ijz,
        UD_Ilahf,
        UD_Ilar,
        UD_Ilddqu,
        UD_Ildmxcsr,
        UD_Ilds,
        UD_Ilea,
        UD_Ileave,
        UD_Iles,
        UD_Ilfence,
        UD_Ilfs,
        UD_Ilgdt,
        UD_Ilgs,
        UD_Ilidt,
        UD_Illdt,
        UD_Ilmsw,
        UD_Ilock,
        UD_Ilodsb,
        UD_Ilodsd,
        UD_Ilodsq,
        UD_Ilodsw,
        UD_Iloop,
        UD_Iloope,
        UD_Iloopne,
        UD_Ilsl,
        UD_Ilss,
        UD_Iltr,
        UD_Imaskmovdqu,
        UD_Imaskmovq,
        UD_Imaxpd,
        UD_Imaxps,
        UD_Imaxsd,
        UD_Imaxss,
        UD_Imfence,
        UD_Iminpd,
        UD_Iminps,
        UD_Iminsd,
        UD_Iminss,
        UD_Imonitor,
        UD_Imontmul,
        UD_Imov,
        UD_Imovapd,
        UD_Imovaps,
        UD_Imovbe,
        UD_Imovd,
        UD_Imovddup,
        UD_Imovdq2q,
        UD_Imovdqa,
        UD_Imovdqu,
        UD_Imovhlps,
        UD_Imovhpd,
        UD_Imovhps,
        UD_Imovlhps,
        UD_Imovlpd,
        UD_Imovlps,
        UD_Imovmskpd,
        UD_Imovmskps,
        UD_Imovntdq,
        UD_Imovntdqa,
        UD_Imovnti,
        UD_Imovntpd,
        UD_Imovntps,
        UD_Imovntq,
        UD_Imovq,
        UD_Imovq2dq,
        UD_Imovsb,
        UD_Imovsd,
        UD_Imovshdup,
        UD_Imovsldup,
        UD_Imovsq,
        UD_Imovss,
        UD_Imovsw,
        UD_Imovsx,
        UD_Imovsxd,
        UD_Imovupd,
        UD_Imovups,
        UD_Imovzx,
        UD_Impsadbw,
        UD_Imul,
        UD_Imulpd,
        UD_Imulps,
        UD_Imulsd,
        UD_Imulss,
        UD_Imwait,
        UD_Ineg,
        UD_Inop,
        UD_Inot,
        UD_Ior,
        UD_Iorpd,
        UD_Iorps,
        UD_Iout,
        UD_Ioutsb,
        UD_Ioutsd,
        UD_Ioutsw,
        UD_Ipabsb,
        UD_Ipabsd,
        UD_Ipabsw,
        UD_Ipackssdw,
        UD_Ipacksswb,
        UD_Ipackusdw,
        UD_Ipackuswb,
        UD_Ipaddb,
        UD_Ipaddd,
        UD_Ipaddq,
        UD_Ipaddsb,
        UD_Ipaddsw,
        UD_Ipaddusb,
        UD_Ipaddusw,
        UD_Ipaddw,
        UD_Ipalignr,
        UD_Ipand,
        UD_Ipandn,
        UD_Ipavgb,
        UD_Ipavgusb,
        UD_Ipavgw,
        UD_Ipblendvb,
        UD_Ipblendw,
        UD_Ipclmulqdq,
        UD_Ipcmpeqb,
        UD_Ipcmpeqd,
        UD_Ipcmpeqq,
        UD_Ipcmpeqw,
        UD_Ipcmpestri,
        UD_Ipcmpestrm,
        UD_Ipcmpgtb,
        UD_Ipcmpgtd,
        UD_Ipcmpgtq,
        UD_Ipcmpgtw,
        UD_Ipcmpistri,
        UD_Ipcmpistrm,
        UD_Ipextrb,
        UD_Ipextrd,
        UD_Ipextrq,
        UD_Ipextrw,
        UD_Ipf2id,
        UD_Ipf2iw,
        UD_Ipfacc,
        UD_Ipfadd,
        UD_Ipfcmpeq,
        UD_Ipfcmpge,
        UD_Ipfcmpgt,
        UD_Ipfmax,
        UD_Ipfmin,
        UD_Ipfmul,
        UD_Ipfnacc,
        UD_Ipfpnacc,
        UD_Ipfrcp,
        UD_Ipfrcpit1,
        UD_Ipfrcpit2,
        UD_Ipfrsqit1,
        UD_Ipfrsqrt,
        UD_Ipfsub,
        UD_Ipfsubr,
        UD_Iphaddd,
        UD_Iphaddsw,
        UD_Iphaddw,
        UD_Iphminposuw,
        UD_Iphsubd,
        UD_Iphsubsw,
        UD_Iphsubw,
        UD_Ipi2fd,
        UD_Ipi2fw,
        UD_Ipinsrb,
        UD_Ipinsrd,
        UD_Ipinsrq,
        UD_Ipinsrw,
        UD_Ipmaddubsw,
        UD_Ipmaddwd,
        UD_Ipmaxsb,
        UD_Ipmaxsd,
        UD_Ipmaxsw,
        UD_Ipmaxub,
        UD_Ipmaxud,
        UD_Ipmaxuw,
        UD_Ipminsb,
        UD_Ipminsd,
        UD_Ipminsw,
        UD_Ipminub,
        UD_Ipminud,
        UD_Ipminuw,
        UD_Ipmovmskb,
        UD_Ipmovsxbd,
        UD_Ipmovsxbq,
        UD_Ipmovsxbw,
        UD_Ipmovsxdq,
        UD_Ipmovsxwd,
        UD_Ipmovsxwq,
        UD_Ipmovzxbd,
        UD_Ipmovzxbq,
        UD_Ipmovzxbw,
        UD_Ipmovzxdq,
        UD_Ipmovzxwd,
        UD_Ipmovzxwq,
        UD_Ipmuldq,
        UD_Ipmulhrsw,
        UD_Ipmulhrw,
        UD_Ipmulhuw,
        UD_Ipmulhw,
        UD_Ipmulld,
        UD_Ipmullw,
        UD_Ipmuludq,
        UD_Ipop,
        UD_Ipopa,
        UD_Ipopad,
        UD_Ipopcnt,
        UD_Ipopfd,
        UD_Ipopfq,
        UD_Ipopfw,
        UD_Ipor,
        UD_Iprefetch,
        UD_Iprefetchnta,
        UD_Iprefetcht0,
        UD_Iprefetcht1,
        UD_Iprefetcht2,
        UD_Ipsadbw,
        UD_Ipshufb,
        UD_Ipshufd,
        UD_Ipshufhw,
        UD_Ipshuflw,
        UD_Ipshufw,
        UD_Ipsignb,
        UD_Ipsignd,
        UD_Ipsignw,
        UD_Ipslld,
        UD_Ipslldq,
        UD_Ipsllq,
        UD_Ipsllw,
        UD_Ipsrad,
        UD_Ipsraw,
        UD_Ipsrld,
        UD_Ipsrldq,
        UD_Ipsrlq,
        UD_Ipsrlw,
        UD_Ipsubb,
        UD_Ipsubd,
        UD_Ipsubq,
        UD_Ipsubsb,
        UD_Ipsubsw,
        UD_Ipsubusb,
        UD_Ipsubusw,
        UD_Ipsubw,
        UD_Ipswapd,
        UD_Iptest,
        UD_Ipunpckhbw,
        UD_Ipunpckhdq,
        UD_Ipunpckhqdq,
        UD_Ipunpckhwd,
        UD_Ipunpcklbw,
        UD_Ipunpckldq,
        UD_Ipunpcklqdq,
        UD_Ipunpcklwd,
        UD_Ipush,
        UD_Ipusha,
        UD_Ipushad,
        UD_Ipushfd,
        UD_Ipushfq,
        UD_Ipushfw,
        UD_Ipxor,
        UD_Ircl,
        UD_Ircpps,
        UD_Ircpss,
        UD_Ircr,
        UD_Irdmsr,
        UD_Irdpmc,
        UD_Irdrand,
        UD_Irdtsc,
        UD_Irdtscp,
        UD_Irep,
        UD_Irepne,
        UD_Iret,
        UD_Iretf,
        UD_Irol,
        UD_Iror,
        UD_Iroundpd,
        UD_Iroundps,
        UD_Iroundsd,
        UD_Iroundss,
        UD_Irsm,
        UD_Irsqrtps,
        UD_Irsqrtss,
        UD_Isahf,
        UD_Isalc,
        UD_Isar,
        UD_Isbb,
        UD_Iscasb,
        UD_Iscasd,
        UD_Iscasq,
        UD_Iscasw,
        UD_Iseta,
        UD_Isetae,
        UD_Isetb,
        UD_Isetbe,
        UD_Isetg,
        UD_Isetge,
        UD_Isetl,
        UD_Isetle,
        UD_Isetno,
        UD_Isetnp,
        UD_Isetns,
        UD_Isetnz,
        UD_Iseto,
        UD_Isetp,
        UD_Isets,
        UD_Isetz,
        UD_Isfence,
        UD_Isgdt,
        UD_Ishl,
        UD_Ishld,
        UD_Ishr,
        UD_Ishrd,
        UD_Ishufpd,
        UD_Ishufps,
        UD_Isidt,
        UD_Iskinit,
        UD_Isldt,
        UD_Ismsw,
        UD_Isqrtpd,
        UD_Isqrtps,
        UD_Isqrtsd,
        UD_Isqrtss,
        UD_Istc,
        UD_Istd,
        UD_Istgi,
        UD_Isti,
        UD_Istmxcsr,
        UD_Istosb,
        UD_Istosd,
        UD_Istosq,
        UD_Istosw,
        UD_Istr,
        UD_Isub,
        UD_Isubpd,
        UD_Isubps,
        UD_Isubsd,
        UD_Isubss,
        UD_Iswapgs,
        UD_Isyscall,
        UD_Isysenter,
        UD_Isysexit,
        UD_Isysret,
        UD_Itest,
        UD_Iucomisd,
        UD_Iucomiss,
        UD_Iud2,
        UD_Iunpckhpd,
        UD_Iunpckhps,
        UD_Iunpcklpd,
        UD_Iunpcklps,
        UD_Ivaddpd,
        UD_Ivaddps,
        UD_Ivaddsd,
        UD_Ivaddss,
        UD_Ivaddsubpd,
        UD_Ivaddsubps,
        UD_Ivaesdec,
        UD_Ivaesdeclast,
        UD_Ivaesenc,
        UD_Ivaesenclast,
        UD_Ivaesimc,
        UD_Ivaeskeygenassist,
        UD_Ivandnpd,
        UD_Ivandnps,
        UD_Ivandpd,
        UD_Ivandps,
        UD_Ivblendpd,
        UD_Ivblendps,
        UD_Ivblendvpd,
        UD_Ivblendvps,
        UD_Ivbroadcastsd,
        UD_Ivbroadcastss,
        UD_Ivcmppd,
        UD_Ivcmpps,
        UD_Ivcmpsd,
        UD_Ivcmpss,
        UD_Ivcomisd,
        UD_Ivcomiss,
        UD_Ivcvtdq2pd,
        UD_Ivcvtdq2ps,
        UD_Ivcvtpd2dq,
        UD_Ivcvtpd2ps,
        UD_Ivcvtps2dq,
        UD_Ivcvtps2pd,
        UD_Ivcvtsd2si,
        UD_Ivcvtsd2ss,
        UD_Ivcvtsi2sd,
        UD_Ivcvtsi2ss,
        UD_Ivcvtss2sd,
        UD_Ivcvtss2si,
        UD_Ivcvttpd2dq,
        UD_Ivcvttps2dq,
        UD_Ivcvttsd2si,
        UD_Ivcvttss2si,
        UD_Ivdivpd,
        UD_Ivdivps,
        UD_Ivdivsd,
        UD_Ivdivss,
        UD_Ivdppd,
        UD_Ivdpps,
        UD_Iverr,
        UD_Iverw,
        UD_Ivextractf128,
        UD_Ivextractps,
        UD_Ivhaddpd,
        UD_Ivhaddps,
        UD_Ivhsubpd,
        UD_Ivhsubps,
        UD_Ivinsertf128,
        UD_Ivinsertps,
        UD_Ivlddqu,
        UD_Ivmaskmovdqu,
        UD_Ivmaskmovpd,
        UD_Ivmaskmovps,
        UD_Ivmaxpd,
        UD_Ivmaxps,
        UD_Ivmaxsd,
        UD_Ivmaxss,
        UD_Ivmcall,
        UD_Ivmclear,
        UD_Ivminpd,
        UD_Ivminps,
        UD_Ivminsd,
        UD_Ivminss,
        UD_Ivmlaunch,
        UD_Ivmload,
        UD_Ivmmcall,
        UD_Ivmovapd,
        UD_Ivmovaps,
        UD_Ivmovd,
        UD_Ivmovddup,
        UD_Ivmovdqa,
        UD_Ivmovdqu,
        UD_Ivmovhlps,
        UD_Ivmovhpd,
        UD_Ivmovhps,
        UD_Ivmovlhps,
        UD_Ivmovlpd,
        UD_Ivmovlps,
        UD_Ivmovmskpd,
        UD_Ivmovmskps,
        UD_Ivmovntdq,
        UD_Ivmovntdqa,
        UD_Ivmovntpd,
        UD_Ivmovntps,
        UD_Ivmovq,
        UD_Ivmovsd,
        UD_Ivmovshdup,
        UD_Ivmovsldup,
        UD_Ivmovss,
        UD_Ivmovupd,
        UD_Ivmovups,
        UD_Ivmpsadbw,
        UD_Ivmptrld,
        UD_Ivmptrst,
        UD_Ivmread,
        UD_Ivmresume,
        UD_Ivmrun,
        UD_Ivmsave,
        UD_Ivmulpd,
        UD_Ivmulps,
        UD_Ivmulsd,
        UD_Ivmulss,
        UD_Ivmwrite,
        UD_Ivmxoff,
        UD_Ivmxon,
        UD_Ivorpd,
        UD_Ivorps,
        UD_Ivpabsb,
        UD_Ivpabsd,
        UD_Ivpabsw,
        UD_Ivpackssdw,
        UD_Ivpacksswb,
        UD_Ivpackusdw,
        UD_Ivpackuswb,
        UD_Ivpaddb,
        UD_Ivpaddd,
        UD_Ivpaddq,
        UD_Ivpaddsb,
        UD_Ivpaddsw,
        UD_Ivpaddusb,
        UD_Ivpaddusw,
        UD_Ivpaddw,
        UD_Ivpalignr,
        UD_Ivpand,
        UD_Ivpandn,
        UD_Ivpavgb,
        UD_Ivpavgw,
        UD_Ivpblendvb,
        UD_Ivpblendw,
        UD_Ivpclmulqdq,
        UD_Ivpcmpeqb,
        UD_Ivpcmpeqd,
        UD_Ivpcmpeqq,
        UD_Ivpcmpeqw,
        UD_Ivpcmpestri,
        UD_Ivpcmpestrm,
        UD_Ivpcmpgtb,
        UD_Ivpcmpgtd,
        UD_Ivpcmpgtq,
        UD_Ivpcmpgtw,
        UD_Ivpcmpistri,
        UD_Ivpcmpistrm,
        UD_Ivperm2f128,
        UD_Ivpermilpd,
        UD_Ivpermilps,
        UD_Ivpextrb,
        UD_Ivpextrd,
        UD_Ivpextrq,
        UD_Ivpextrw,
        UD_Ivphaddd,
        UD_Ivphaddsw,
        UD_Ivphaddw,
        UD_Ivphminposuw,
        UD_Ivphsubd,
        UD_Ivphsubsw,
        UD_Ivphsubw,
        UD_Ivpinsrb,
        UD_Ivpinsrd,
        UD_Ivpinsrq,
        UD_Ivpinsrw,
        UD_Ivpmaddubsw,
        UD_Ivpmaddwd,
        UD_Ivpmaxsb,
        UD_Ivpmaxsd,
        UD_Ivpmaxsw,
        UD_Ivpmaxub,
        UD_Ivpmaxud,
        UD_Ivpmaxuw,
        UD_Ivpminsb,
        UD_Ivpminsd,
        UD_Ivpminsw,
        UD_Ivpminub,
        UD_Ivpminud,
        UD_Ivpminuw,
        UD_Ivpmovmskb,
        UD_Ivpmovsxbd,
        UD_Ivpmovsxbq,
        UD_Ivpmovsxbw,
        UD_Ivpmovsxwd,
        UD_Ivpmovsxwq,
        UD_Ivpmovzxbd,
        UD_Ivpmovzxbq,
        UD_Ivpmovzxbw,
        UD_Ivpmovzxdq,
        UD_Ivpmovzxwd,
        UD_Ivpmovzxwq,
        UD_Ivpmuldq,
        UD_Ivpmulhrsw,
        UD_Ivpmulhuw,
        UD_Ivpmulhw,
        UD_Ivpmulld,
        UD_Ivpmullw,
        UD_Ivpor,
        UD_Ivpsadbw,
        UD_Ivpshufb,
        UD_Ivpshufd,
        UD_Ivpshufhw,
        UD_Ivpshuflw,
        UD_Ivpsignb,
        UD_Ivpsignd,
        UD_Ivpsignw,
        UD_Ivpslld,
        UD_Ivpslldq,
        UD_Ivpsllq,
        UD_Ivpsllw,
        UD_Ivpsrad,
        UD_Ivpsraw,
        UD_Ivpsrld,
        UD_Ivpsrldq,
        UD_Ivpsrlq,
        UD_Ivpsrlw,
        UD_Ivpsubb,
        UD_Ivpsubd,
        UD_Ivpsubq,
        UD_Ivpsubsb,
        UD_Ivpsubsw,
        UD_Ivpsubusb,
        UD_Ivpsubusw,
        UD_Ivpsubw,
        UD_Ivptest,
        UD_Ivpunpckhbw,
        UD_Ivpunpckhdq,
        UD_Ivpunpckhqdq,
        UD_Ivpunpckhwd,
        UD_Ivpunpcklbw,
        UD_Ivpunpckldq,
        UD_Ivpunpcklqdq,
        UD_Ivpunpcklwd,
        UD_Ivpxor,
        UD_Ivrcpps,
        UD_Ivrcpss,
        UD_Ivroundpd,
        UD_Ivroundps,
        UD_Ivroundsd,
        UD_Ivroundss,
        UD_Ivrsqrtps,
        UD_Ivrsqrtss,
        UD_Ivshufpd,
        UD_Ivshufps,
        UD_Ivsqrtpd,
        UD_Ivsqrtps,
        UD_Ivsqrtsd,
        UD_Ivsqrtss,
        UD_Ivstmxcsr,
        UD_Ivsubpd,
        UD_Ivsubps,
        UD_Ivsubsd,
        UD_Ivsubss,
        UD_Ivtestpd,
        UD_Ivtestps,
        UD_Ivucomisd,
        UD_Ivucomiss,
        UD_Ivunpckhpd,
        UD_Ivunpckhps,
        UD_Ivunpcklpd,
        UD_Ivunpcklps,
        UD_Ivxorpd,
        UD_Ivxorps,
        UD_Ivzeroall,
        UD_Ivzeroupper,
        UD_Iwait,
        UD_Iwbinvd,
        UD_Iwrmsr,
        UD_Ixadd,
        UD_Ixchg,
        UD_Ixcryptcbc,
        UD_Ixcryptcfb,
        UD_Ixcryptctr,
        UD_Ixcryptecb,
        UD_Ixcryptofb,
        UD_Ixgetbv,
        UD_Ixlatb,
        UD_Ixor,
        UD_Ixorpd,
        UD_Ixorps,
        UD_Ixrstor,
        UD_Ixsave,
        UD_Ixsetbv,
        UD_Ixsha1,
        UD_Ixsha256,
        UD_Ixstore,
        UD_Iinvalid,
        UD_I3dnow,
        UD_Inone,
        UD_Idb,
        UD_Ipause,
        UD_MAX_MNEMONIC_CODE
    }
    
    #endregion
}
#pragma warning restore 1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko
{
	public class PrintSettings : nsIPrintSettings
	{
		Dictionary<int, bool> m_printOptions = new Dictionary<int, bool>();
		int m_printOptionsBits;
		double m_effectivePageWidth;
		double m_effectivePageHeight;
		nsIPrintSession m_printSession;
		int m_startPageRange;
		int m_endPageRange;
		double m_edgeTop;
		double m_edgeLeft;
		double m_edgeBottom;
		double m_edgeRight;
		double m_marginTop;
		double m_marginLeft;
		double m_marginBottom;
		double m_marginRight;
		double m_unwriteableMarginTop;
		double m_unwriteableMarginLeft;
		double m_unwriteableMarginBottom;
		double m_unwriteableMarginRight;
		double m_scaling;
		bool m_printBGColors;
		bool m_printBGImages;
		short m_printRange;
		string m_title;
		string m_docURL;
		string m_headerStrLeft;
		string m_headerStrCenter;
		string m_headerStrRight;
		string m_footerStrLeft;
		string m_footerStrCenter;
		string m_footerStrRight;
		short m_howToEnableFrameUI;
		bool m_IsCancelled;
		short m_printFrameTypeUsage;
		short m_printFrameType;
		bool m_printSilent;
		bool m_shrinkToFit;
		bool m_showPrintProgress;
		string m_paperName;
		short m_paperSizeType;
		short m_paperData;
		double m_paperWidth;
		double m_paperHeight;
		short m_paperSizeUnit;
		string m_plexName;
		string m_colorspace;
		string m_resolutionName;
		bool m_downloadFonts;
		bool m_printReversed;
		bool m_printInColor;
		int m_orientation;
		string m_printCommand;
		int m_numCopies;
		string m_printerName;
		bool m_printToFile;
		string m_toFileName;
		short m_outputFormat;
		int m_printPageDelay;
		bool m_isInitializedFromPrinter;
		bool m_isInitializedFromPrefs;

#region NonInterface methods
		public void SetPrintOptionsBits(int printOptionsBits)
		{
			m_printOptionsBits = printOptionsBits;
		}

		public void  SetEffectivePageSize(double aWidth, double aHeight)
		{
			m_effectivePageWidth = aWidth;
			m_effectivePageHeight = aHeight;
		}
#endregion

		#region nsIPrintSettings

		public void SetPrintOptions(int aType, bool aTurnOnOff)
		{			
			m_printOptions[aType] = aTurnOnOff;
		}

		public bool GetPrintOptions(int aType)
		{
			Console.WriteLine("1 aType = {0}", aType);
			if (!m_printOptions.ContainsKey(aType))
				return false;

			return m_printOptions[aType];
		}

		public int GetPrintOptionsBits()
		{
			Console.WriteLine("2");
			return m_printOptionsBits;
		}

		public void GetEffectivePageSize(ref double aWidth, ref double aHeight)
		{
			Console.WriteLine("3");
			aWidth = m_effectivePageWidth;
			aHeight = m_effectivePageHeight;
		}

		public nsIPrintSettings Clone()
		{
			Console.WriteLine("4");
			// TODO deal with refs?
			return (PrintSettings)this.MemberwiseClone();
		}

		public void Assign(nsIPrintSettings aPS)
		{
			Console.WriteLine("5");
			throw new NotImplementedException();
		}

		public nsIPrintSession GetPrintSessionAttribute()
		{
			Console.WriteLine("6");
			return m_printSession;
		}

		public void SetPrintSessionAttribute(nsIPrintSession aPrintSession)
		{
			m_printSession = aPrintSession;
		}

		public int GetStartPageRangeAttribute()
		{
			Console.WriteLine("7");
			return m_startPageRange;
		}

		public void SetStartPageRangeAttribute(int aStartPageRange)
		{
			m_startPageRange = aStartPageRange;
		}

		public int GetEndPageRangeAttribute()
		{
			Console.WriteLine("8");
			return m_endPageRange;
		}

		public void SetEndPageRangeAttribute(int aEndPageRange)
		{
			m_endPageRange = aEndPageRange;
		}		

		public double GetEdgeTopAttribute()
		{
			Console.WriteLine("9");
			return m_edgeTop;
		}

		public void SetEdgeTopAttribute(double aEdgeTop)
		{
			m_edgeTop = aEdgeTop;
		}

		public double GetEdgeLeftAttribute()
		{
			Console.WriteLine("10");
			return m_edgeLeft;
		}

		public void SetEdgeLeftAttribute(double aEdgeLeft)
		{
			m_edgeLeft = aEdgeLeft;
		}

		public double GetEdgeBottomAttribute()
		{
			Console.WriteLine("11");
			return m_edgeBottom;
		}

		public void SetEdgeBottomAttribute(double aEdgeBottom)
		{
			m_edgeBottom = aEdgeBottom;
		}

		public double GetEdgeRightAttribute()
		{
			Console.WriteLine("12");
			return m_edgeRight;
		}

		public void SetEdgeRightAttribute(double aEdgeRight)
		{
			m_edgeRight = aEdgeRight;
		}

		public double GetMarginTopAttribute()
		{
			Console.WriteLine("13");
			return m_marginTop;
		}

		public void SetMarginTopAttribute(double aMarginTop)
		{
			m_marginTop = aMarginTop;
		}

		public double GetMarginLeftAttribute()
		{
			Console.WriteLine("14");
			return m_marginLeft;
		}

		public void SetMarginLeftAttribute(double aMarginLeft)
		{
			m_marginLeft = aMarginLeft;
		}

		public double GetMarginBottomAttribute()
		{
			Console.WriteLine("15");
			return m_marginBottom;
		}

		public void SetMarginBottomAttribute(double aMarginBottom)
		{
			m_marginBottom = aMarginBottom;
		}

		public double GetMarginRightAttribute()
		{
			Console.WriteLine("16");
			return m_marginRight;
		}

		public void SetMarginRightAttribute(double aMarginRight)
		{
			m_marginRight = aMarginRight;
		}

		public double GetUnwriteableMarginTopAttribute()
		{
			Console.WriteLine("17");
			return m_unwriteableMarginTop;
		}

		public void SetUnwriteableMarginTopAttribute(double aUnwriteableMarginTop)
		{
			m_unwriteableMarginTop = aUnwriteableMarginTop;
		}

		public double GetUnwriteableMarginLeftAttribute()
		{
			Console.WriteLine("18");
			return m_unwriteableMarginLeft;
		}

		public void SetUnwriteableMarginLeftAttribute(double aUnwriteableMarginLeft)
		{
			m_unwriteableMarginLeft = aUnwriteableMarginLeft;
		}

		public double GetUnwriteableMarginBottomAttribute()
		{
			Console.WriteLine("19");
			return m_unwriteableMarginBottom;
		}

		public void SetUnwriteableMarginBottomAttribute(double aUnwriteableMarginBottom)
		{
			m_unwriteableMarginBottom = aUnwriteableMarginBottom;
		}

		public double GetUnwriteableMarginRightAttribute()
		{
			Console.WriteLine("20");
			return m_unwriteableMarginRight;
		}

		public void SetUnwriteableMarginRightAttribute(double aUnwriteableMarginRight)
		{
			m_unwriteableMarginRight = aUnwriteableMarginRight;
		}
		
		public double GetScalingAttribute()
		{
			Console.WriteLine("21");
			return m_scaling;
		}

		public void SetScalingAttribute(double aScaling)
		{
			m_scaling = aScaling;
		}

		public bool GetPrintBGColorsAttribute()
		{
			Console.WriteLine("22");
			return m_printBGColors;
		}

		public void SetPrintBGColorsAttribute(bool aPrintBGColors)
		{
			m_printBGColors = aPrintBGColors;
		}

		public bool GetPrintBGImagesAttribute()
		{
			Console.WriteLine("23");
			return m_printBGImages;
		}

		public void SetPrintBGImagesAttribute(bool aPrintBGImages)
		{
			m_printBGImages = aPrintBGImages;
		}

		public short GetPrintRangeAttribute()
		{
			Console.WriteLine("24");
			return m_printRange;
		}

		public void SetPrintRangeAttribute(short aPrintRange)
		{
			m_printRange = aPrintRange;
		}

		public string GetTitleAttribute()
		{
			Console.WriteLine("25");
			return m_title;
		}

		public void SetTitleAttribute(string aTitle)
		{
			m_title = aTitle;
		}
		
		public string GetDocURLAttribute()
		{
			Console.WriteLine("26");
			return m_docURL;
		}

		public void SetDocURLAttribute(string aDocURL)
		{
			m_docURL = aDocURL;
		}

		public string GetHeaderStrLeftAttribute()
		{
			Console.WriteLine("27");
			return m_headerStrLeft;
		}

		public void SetHeaderStrLeftAttribute(string aHeaderStrLeft)
		{
			m_headerStrLeft = aHeaderStrLeft;
		}

		public string GetHeaderStrCenterAttribute()
		{
			Console.WriteLine("28");
			return m_headerStrCenter;
		}

		public void SetHeaderStrCenterAttribute(string aHeaderStrCenter)
		{
			m_headerStrCenter = aHeaderStrCenter;
		}

		public string GetHeaderStrRightAttribute()
		{
			Console.WriteLine("29");
			return m_headerStrRight;
		}

		public void SetHeaderStrRightAttribute(string aHeaderStrRight)
		{
			m_headerStrRight = aHeaderStrRight;
		}

		public string GetFooterStrLeftAttribute()
		{
			Console.WriteLine("30");
			return m_footerStrLeft;
		}

		public void SetFooterStrLeftAttribute(string aFooterStrLeft)
		{
			m_footerStrLeft = aFooterStrLeft;
		}

		public string GetFooterStrCenterAttribute()
		{
			Console.WriteLine("31");
			return m_footerStrCenter;
		}

		public void SetFooterStrCenterAttribute(string aFooterStrCenter)
		{
			m_footerStrCenter = aFooterStrCenter;
		}

		public string GetFooterStrRightAttribute()
		{
			Console.WriteLine("32");
			return m_footerStrRight;
		}

		public void SetFooterStrRightAttribute(string aFooterStrRight)
		{
			m_footerStrRight = aFooterStrRight;
		}

		public short GetHowToEnableFrameUIAttribute()
		{
			Console.WriteLine("33");
			return m_howToEnableFrameUI;
		}

		public void SetHowToEnableFrameUIAttribute(short aHowToEnableFrameUI)
		{
			m_howToEnableFrameUI = aHowToEnableFrameUI;
		}

		public bool GetIsCancelledAttribute()
		{
			Console.WriteLine("34");
			return m_IsCancelled;
		}

		public void SetIsCancelledAttribute(bool aIsCancelled)
		{
			m_IsCancelled = aIsCancelled;
		}

		public short GetPrintFrameTypeUsageAttribute()
		{
			Console.WriteLine("35");
			return m_printFrameTypeUsage;
		}

		public void SetPrintFrameTypeUsageAttribute(short aPrintFrameTypeUsage)
		{
			m_printFrameTypeUsage = aPrintFrameTypeUsage;
		}

		public short GetPrintFrameTypeAttribute()
		{
			Console.WriteLine("36");
			return m_printFrameType;
		}

		public void SetPrintFrameTypeAttribute(short aPrintFrameType)
		{
			m_printFrameType = aPrintFrameType;
		}

		public bool GetPrintSilentAttribute()
		{
			Console.WriteLine("37");
			return m_printSilent;
		}

		public void SetPrintSilentAttribute(bool aPrintSilent)
		{
			m_printSilent = aPrintSilent;
		}

		public bool GetShrinkToFitAttribute()
		{
			Console.WriteLine("38");
			return m_shrinkToFit;
		}

		public void SetShrinkToFitAttribute(bool aShrinkToFit)
		{
			m_shrinkToFit = aShrinkToFit;
		}

		public bool GetShowPrintProgressAttribute()
		{
			Console.WriteLine("39");
			return m_showPrintProgress;
		}

		public void SetShowPrintProgressAttribute(bool aShowPrintProgress)
		{
			m_showPrintProgress = aShowPrintProgress;
		}

		public string GetPaperNameAttribute()
		{
			Console.WriteLine("40");
			return m_paperName;
		}

		public void SetPaperNameAttribute(string aPaperName)
		{
			m_paperName = aPaperName;
		}

		public short GetPaperSizeTypeAttribute()
		{
			Console.WriteLine("41");
			return m_paperSizeType;
		}

		public void SetPaperSizeTypeAttribute(short aPaperSizeType)
		{
			m_paperSizeType = aPaperSizeType;
		}

		public short GetPaperDataAttribute()
		{
			Console.WriteLine("42");
			return m_paperData;
		}

		public void SetPaperDataAttribute(short aPaperData)
		{
			m_paperData = aPaperData;
		}

		public double GetPaperWidthAttribute()
		{
			Console.WriteLine("43");
			return m_paperWidth;
		}

		public void SetPaperWidthAttribute(double aPaperWidth)
		{
			m_paperWidth = aPaperWidth;
		}

		public double GetPaperHeightAttribute()
		{
			Console.WriteLine("44");
			return m_paperHeight;
		}

		public void SetPaperHeightAttribute(double aPaperHeight)
		{
			m_paperHeight = aPaperHeight;
		}

		public short GetPaperSizeUnitAttribute()
		{
			Console.WriteLine("45");
			return m_paperSizeUnit;
		}

		public void SetPaperSizeUnitAttribute(short aPaperSizeUnit)
		{
			m_paperSizeUnit = aPaperSizeUnit;
		}

		public string GetPlexNameAttribute()
		{
			Console.WriteLine("46");
			return m_plexName;
		}

		public void SetPlexNameAttribute(string aPlexName)
		{
			m_plexName = aPlexName;
		}
	
		public string GetColorspaceAttribute()
		{
			Console.WriteLine("47");
			return m_colorspace;
		}

		public void SetColorspaceAttribute(string aColorspace)
		{
			m_colorspace = aColorspace;
		}

		public string GetResolutionNameAttribute()
		{
			Console.WriteLine("48");
			return m_resolutionName;
		}

		public void SetResolutionNameAttribute(string aResolutionName)
		{
			m_resolutionName = aResolutionName;
		}

		public bool GetDownloadFontsAttribute()
		{
			Console.WriteLine("49");
			return m_downloadFonts;
		}

		public void SetDownloadFontsAttribute(bool aDownloadFonts)
		{
			m_downloadFonts = aDownloadFonts;
		}

		public bool GetPrintReversedAttribute()
		{
			Console.WriteLine("50");
			return m_printReversed;
		}

		public void SetPrintReversedAttribute(bool aPrintReversed)
		{
			m_printReversed = aPrintReversed;
		}

		public bool GetPrintInColorAttribute()
		{
			Console.WriteLine("51");
			return m_printInColor;
		}

		public void SetPrintInColorAttribute(bool aPrintInColor)
		{
			m_printInColor = aPrintInColor;
		}

		public int GetOrientationAttribute()
		{
			Console.WriteLine("52");
			return m_orientation;
		}

		public void SetOrientationAttribute(int aOrientation)
		{
			m_orientation = aOrientation;
		}

		public string GetPrintCommandAttribute()
		{
			Console.WriteLine("53");
			return m_printCommand;
		}

		public void SetPrintCommandAttribute(string aPrintCommand)
		{
			m_printCommand = aPrintCommand;
		}

		public int GetNumCopiesAttribute()
		{
			Console.WriteLine("54");
			return m_numCopies;
		}

		public void SetNumCopiesAttribute(int aNumCopies)
		{
			m_numCopies = aNumCopies;
		}

		public string GetPrinterNameAttribute()
		{
			Console.WriteLine("55");
			return m_printerName;
		}

		public void SetPrinterNameAttribute(string aPrinterName)
		{
			m_printerName = aPrinterName;
		}

		public bool GetPrintToFileAttribute()
		{
			Console.WriteLine("56");
			return m_printToFile;
		}

		public void SetPrintToFileAttribute(bool aPrintToFile)
		{
			m_printToFile = aPrintToFile;
		}

		public string GetToFileNameAttribute()
		{
			Console.WriteLine("57");
			return m_toFileName;
		}

		public void SetToFileNameAttribute(string aToFileName)
		{
			m_toFileName = aToFileName;
		}

		public short GetOutputFormatAttribute()
		{
			Console.WriteLine("58");
			return m_outputFormat;
		}

		public void SetOutputFormatAttribute(short aOutputFormat)
		{
			m_outputFormat = aOutputFormat;
		}

		public int GetPrintPageDelayAttribute()
		{
			Console.WriteLine("59");
			return m_printPageDelay;
		}

		public void SetPrintPageDelayAttribute(int aPrintPageDelay)
		{
			m_printPageDelay = aPrintPageDelay;
		}

		public int GetResolutionAttribute()
		{
			throw new NotImplementedException();
		}

		public void SetResolutionAttribute(int aResolution)
		{
			throw new NotImplementedException();
		}

		public int GetDuplexAttribute()
		{
			throw new NotImplementedException();
		}

		public void SetDuplexAttribute(int aDuplex)
		{
			throw new NotImplementedException();
		}

		public bool GetIsInitializedFromPrinterAttribute()
		{
			Console.WriteLine("60");
			return m_isInitializedFromPrinter;
		}

		public void SetIsInitializedFromPrinterAttribute(bool aIsInitializedFromPrinter)
		{
			m_isInitializedFromPrinter = aIsInitializedFromPrinter;
		}

		public bool GetIsInitializedFromPrefsAttribute()
		{
			Console.WriteLine("61");
			return m_isInitializedFromPrefs;
		}

		public void SetIsInitializedFromPrefsAttribute(bool aIsInitializedFromPrefs)
		{
			m_isInitializedFromPrefs = aIsInitializedFromPrefs;
		}

		
		public void SetMarginInTwips(IntPtr aMargin)
		{
			Console.WriteLine("62");			
		}

		public void SetEdgeInTwips(IntPtr aEdge)
		{
			
		}

		public void GetMarginInTwips(IntPtr aMargin)
		{
			Console.WriteLine("63");			
		}

		public void GetEdgeInTwips(IntPtr aEdge)
		{
			Console.WriteLine("65");
			
		}


		public void SetupSilentPrinting()
		{
			Console.WriteLine("66");
		}

		public void SetUnwriteableMarginInTwips(IntPtr aEdge)
		{
			throw new NotImplementedException();
		}

		public void GetUnwriteableMarginInTwips(IntPtr aEdge)
		{
			throw new NotImplementedException();
		}

		public void GetPageRanges(System.IntPtr aPages)
		{

		}

		#endregion

		#region nsIPrintSettings Members


		public bool GetPersistMarginBoxSettingsAttribute()
		{
			throw new NotImplementedException();
		}

		public void SetPersistMarginBoxSettingsAttribute(bool aPersistMarginBoxSettings)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

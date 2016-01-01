<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" >
<xsl:template match="x86optable">
<html>
   <head>
    <title>x86 opcode table</title>
    <style>
     body {  font-family: "lucida sans", georgia, helvetica, arial, verdana, georgia; }

    .mnm {
       border-bottom: 1px dotted #cdcdcd;
       font-family: "lucida sans", georgia, helvetica, arial, verdana, georgia;
       border-right: 1px solid #cdcdcd;
       font-size: 1em;
    }

    .opc {
       border-bottom: 1px dotted #cdcdcd;
       border-right: 1px solid #cdcdcd;
       font-family: monospace;
       font-size: 1.1em;
    }

    .vdr {
       border-bottom: 1px dotted #cdcdcd;
       border-right: 1px solid #cdcdcd;
       font-size: .9em;
    }      
    </style>
   </head>
   <body> 
      <h1 style="text-align:left; padding-left:8px;">x86/optable.xml</h1>
	<p style="text-align:left; padding-left:8px">
		<a style="text-decoration:none" href="https://github.com/vmt/udis86">github.com/vmt/udis86</a></p>
      <table cellpadding="4" cellspacing="6" width="800px"> 
         <tr bgcolor='lightblue'>
            <td align="center">Mnemonic</td>
	    <td align="center">Opcodes</td>
	    <td align="center">Vendor</td>
         </tr>
         <xsl:for-each select="instruction">
            <tr>   
                <td class="mnm" align="center" valign="middle">
                    <xsl:for-each select="mnemonic">
                        <xsl:apply-templates/>	
                    </xsl:for-each>
                </td>
		        <td class="opc">
		            <xsl:for-each select="def">
                        <xsl:for-each select="pfx"><xsl:apply-templates/></xsl:for-each> ; 
                        <xsl:for-each select="opc"><xsl:apply-templates/></xsl:for-each> ;
                        <xsl:for-each select="opr"><xsl:apply-templates/></xsl:for-each>
                        <br/>
                    </xsl:for-each>
                </td>
		<td class="vdr" align="center" valign="top">
		<xsl:for-each select="vendor">
		<xsl:apply-templates/>	
		<br/>
		</xsl:for-each>
		</td>
            </tr>
         </xsl:for-each>
      </table>

	<p style="text-align:left; padding-left:8px">
		<small>Copyright (c) 2008, Vivek Thampi</small>
	</p>

   </body>
</html>
</xsl:template>
</xsl:stylesheet>

<%@ Page Language="VB" CodeFile="Default.aspx.vb" Inherits="myGraph" %>
<%@ Register TagPrefix="zgw" Namespace="ZedGraph.Web" Assembly="ZedGraph.Web" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Default</title>
</head>
<body>
		Demo graph, using RawImage mode.<br>
		In raw mode the image is generated and sent to the browser on the fly
		(right click the image, select properties and look at the URL).<br><br>
        <img src="graph.aspx" />
        <br /><br />

        Default render mode is now ImageTag.<br>
        In this mode an IMG tag is generated in-place, and the image is generated and saved in the specified folder (RenderedImagePath property, default to ~/ZedGraphImages/). Combined with the CacheDuration property it becomes powerful.<br>
        Note: the path must already exist, be under the website root and be writable.<br />
        <br />
        
        <ZGW:ZEDGRAPHWEB id="ZedGraphWeb1" runat="server" width="500" Height="375"></ZGW:ZEDGRAPHWEB>
        <br />

		Another demo graph using ImageTag mode<br />
        <ZGW:ZEDGRAPHWEB id="ZedGraphWeb2" runat="server" width="500" Height="375"></ZGW:ZEDGRAPHWEB>
</body>
</html>

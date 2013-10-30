'============================================================================
'ZedGraph Class Library - A Flexible Charting Library for .Net
'Copyright (C) 2005 John Champion and Jerry Vos
'
'This library is free software; you can redistribute it and/or
'modify it under the terms of the GNU Lesser General Public
'License as published by the Free Software Foundation; either
'version 2.1 of the License, or (at your option) any later version.
'
'This library is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
'Lesser General Public License for more details.
'
'You should have received a copy of the GNU Lesser General Public
'License along with this library; if not, write to the Free Software
'Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
'=============================================================================
Imports ZedGraph
Imports System.Drawing

Public Class myGraph
	Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

	'This call is required by the Web Form Designer.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub

	'NOTE: The following placeholder declaration is required by the Web Form Designer.
	'Do not delete or move it.
	Private designerPlaceholderDeclaration As System.Object

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
		'CODEGEN: This method call is required by the Web Form Designer
		'Do not modify it using the code editor.
		InitializeComponent()
	End Sub

#End Region

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here
	End Sub

	Private Sub OnRenderGraph1(ByVal g As System.Drawing.Graphics, _
			  ByVal masterPane As ZedGraph.MasterPane) Handles ZedGraphWeb1.RenderGraph

		' Get a reference to the GraphPane instance in the ZedGraphControl
		Dim myPane As GraphPane = masterPane(0)

		' Set the title and axis labels
		myPane.Title.Text = "Cat Stats"
		myPane.YAxis.Title.Text = "Big Cats"
		myPane.XAxis.Title.Text = "Population"

		' Make up some data points
		Dim labels() As String = {"Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard"}
		Dim x() As Double = {100, 115, 75, 22, 98, 40}
		Dim x2() As Double = {120, 175, 95, 57, 113, 110}
		Dim x3() As Double = {204, 192, 119, 80, 134, 156}

		' Generate a red bar with "Curve 1" in the legend
		Dim myCurve As BarItem = myPane.AddBar("Here", x, Nothing, Color.Red)
		' Fill the bar with a red-white-red color gradient for a 3d look
		myCurve.Bar.Fill = New Fill(Color.Red, Color.White, Color.Red, 90.0F)

		' Generate a blue bar with "Curve 2" in the legend
		myCurve = myPane.AddBar("There", x2, Nothing, Color.Blue)
		' Fill the bar with a Blue-white-Blue color gradient for a 3d look
		myCurve.Bar.Fill = New Fill(Color.Blue, Color.White, Color.Blue, 90.0F)

		' Generate a green bar with "Curve 3" in the legend
		myCurve = myPane.AddBar("Elsewhere", x3, Nothing, Color.Green)
		' Fill the bar with a Green-white-Green color gradient for a 3d look
		myCurve.Bar.Fill = New Fill(Color.Green, Color.White, Color.Green, 90.0F)

		' Draw the Y tics between the labels instead of at the labels
		myPane.YAxis.MajorTic.IsBetweenLabels = True

		' Set the YAxis labels
		myPane.YAxis.Scale.TextLabels = labels
		' Set the YAxis to Text type
		myPane.YAxis.Type = AxisType.Text

		' Set the bar type to stack, which stacks the bars by automatically accumulating the values
		myPane.BarSettings.Type = BarType.Stack

		' Make the bars horizontal by setting the BarBase to "Y"
		myPane.BarSettings.Base = BarBase.Y

		' Fill the axis background with a color gradient
		myPane.Chart.Fill = New Fill(Color.White, _
		 Color.FromArgb(255, 255, 166), 45.0F)

		masterPane.AxisChange(g)
	End Sub

	Private Sub OnRenderGraph2(ByVal g As System.Drawing.Graphics, _
			  ByVal masterPane As ZedGraph.MasterPane) Handles ZedGraphWeb2.RenderGraph

		' Get a reference to the GraphPane instance in the ZedGraphControl
		Dim myPane As GraphPane = masterPane(0)

		' Set the titles and axis labels
		myPane.Title.Text = "StickItem Demo Chart"
		myPane.XAxis.Title.Text = "X Label"
		myPane.YAxis.Title.Text = "My Y Axis"

		Dim list As New PointPairList()
		Dim i As Integer, x As Double, y As Double, z As Double
		For i = 0 To 99
			x = i
			y = Math.Sin(i / 8.0)
			z = Math.Abs(Math.Cos(i / 8.0)) * y
			list.Add(x, y, z)
		Next i

		Dim myCurve As StickItem = myPane.AddStick("Some Sticks", list, Color.Blue)
		myCurve.Line.Width = 2.0F
		myPane.XAxis.MajorGrid.IsVisible = True
		myPane.XAxis.Scale.Max = 100

		' Fill the axis background with a color gradient
		myPane.Chart.Fill = New Fill(Color.White, _
		 Color.LightGoldenrodYellow, 45.0F)

		masterPane.AxisChange(g)
	End Sub

End Class

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

Public Class graph
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

	Private Sub OnRenderGraph(ByVal g As System.Drawing.Graphics, _
			  ByVal masterPane As ZedGraph.MasterPane) Handles ZedGraphWeb1.RenderGraph

		' Get a reference to the GraphPane instance in the ZedGraphControl
		Dim myPane As GraphPane = masterPane(0)

		' Set the titles and axis labels
		myPane.Title.Text = "Demonstration of Dual Y Graph"
		myPane.XAxis.Title.Text = "Time, Days"
		myPane.YAxis.Title.Text = "Parameter A"
		myPane.Y2Axis.Title.Text = "Parameter B"

		' Make up some data points based on the Sine function
		Dim list As New PointPairList
		Dim list2 As New PointPairList
		Dim i As Integer, x As Double, y As Double, y2 As Double

		For i = 0 To 35
			x = i * 5.0
			y = Math.Sin(i * Math.PI / 15.0) * 16.0
			y2 = y * 13.5
			list.Add(x, y)
			list2.Add(x, y2)
		Next i

		' Generate a red curve with diamond symbols, and "Alpha" in the legend
		Dim myCurve As LineItem
		myCurve = myPane.AddCurve("Alpha", list, Color.Red, SymbolType.Diamond)
		' Fill the symbols with white
		myCurve.Symbol.Fill = New Fill(Color.White)

		' Generate a blue curve with circle symbols, and "Beta" in the legend
		myCurve = myPane.AddCurve("Beta", list2, Color.Blue, SymbolType.Circle)
		' Fill the symbols with white
		myCurve.Symbol.Fill = New Fill(Color.White)
		' Associate this curve with the Y2 axis
		myCurve.IsY2Axis = True

		' Show the x axis grid
		myPane.XAxis.MajorGrid.IsVisible = True
		myPane.XAxis.CrossAuto = True

		' Make the Y axis scale red
		myPane.YAxis.Scale.FontSpec.FontColor = Color.Red
		myPane.YAxis.Title.FontSpec.FontColor = Color.Red
		' turn off the opposite tics so the Y tics don't show up on the Y2 axis
		myPane.YAxis.MajorTic.IsOpposite = False
		myPane.YAxis.MinorTic.IsOpposite = False
		' Don't display the Y zero line
		myPane.YAxis.MajorGrid.IsZeroLine = False
		' Align the Y axis labels so they are flush to the axis
		myPane.YAxis.Scale.Align = AlignP.Inside
		' Manually set the axis range
		myPane.YAxis.Scale.Min = -30
		myPane.YAxis.Scale.Max = 30

		' Enable the Y2 axis display
		myPane.Y2Axis.IsVisible = True
		' Make the Y2 axis scale blue
		myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue
		myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue
		' turn off the opposite tics so the Y2 tics don't show up on the Y axis
		myPane.Y2Axis.MajorTic.IsOpposite = False
		myPane.Y2Axis.MinorTic.IsOpposite = False
		' Display the Y2 axis grid lines
		myPane.Y2Axis.MajorGrid.IsVisible = True
		' Align the Y2 axis labels so they are flush to the axis
		myPane.Y2Axis.Scale.Align = AlignP.Inside
		myPane.Y2Axis.CrossAuto = True

		' Fill the axis background with a gradient
		myPane.Chart.Fill = New Fill(Color.White, Color.LightGray, 45.0F)

		masterPane.AxisChange(g)
	End Sub

End Class

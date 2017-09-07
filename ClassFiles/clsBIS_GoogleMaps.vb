Public Class clsBIS_GoogleMaps
    Public Class clsFormSettings

        Private Shared myFormX As Integer
        Private Shared myFormY As Integer
        Private Shared myFormHeight As Integer
        Private Shared myFormWidth As Integer
        Private Shared myFormTopMost As Boolean
        Private Shared myMapLat As Double
        Private Shared myMapLng As Double
        Private Shared myMapZoomlevel As Integer

        Public Property MapLat As Double
            Get
                MapLat = myMapLat
            End Get
            Set(value As Double)
                myMapLat = value
            End Set
        End Property
        Public Property MapLng As Double
            Get
                MapLng = myMapLng
            End Get
            Set(value As Double)
                myMapLng = value
            End Set
        End Property
                Public Property MapZoomLevel As Integer
            Get
                MapZoomLevel= myMapZoomlevel
            End Get
            Set(value As Integer)
                myMapZoomlevel = value
            End Set
        End Property
        Public Property FormX() As Integer
            Get
                FormX = myFormX
            End Get
            Set(ByVal Value As Integer)
                myFormX = Value
            End Set
        End Property

        Public Property FormY() As Integer
            Get
                FormY = myFormY
            End Get
            Set(ByVal Value As Integer)
                myFormY = Value
            End Set
        End Property

        Public Property FormHeight() As Integer
            Get
                FormHeight = myFormHeight
            End Get
            Set(ByVal Value As Integer)
                myFormHeight = Value
            End Set
        End Property

        Public Property FormWidth() As Integer
            Get
                FormWidth = myFormWidth
            End Get
            Set(ByVal Value As Integer)
                myFormWidth = Value
            End Set
        End Property

        Public Property FormTopMost() As Boolean
            Get
                FormTopMost = myFormTopMost
            End Get
            Set(ByVal Value As Boolean)
                myFormTopMost = Value
            End Set
        End Property

    End Class       ' clsFormSettings
    Public Class clsHistoryFormSettings

        Private Shared myFormX As Integer
        Private Shared myFormY As Integer
        Private Shared myFormHeight As Integer
        Private Shared myFormWidth As Integer
        Private Shared myFormTopMost As Boolean

        Public Property FormX() As Integer
            Get
                FormX = myFormX
            End Get
            Set(ByVal Value As Integer)
                myFormX = Value
            End Set
        End Property

        Public Property FormY() As Integer
            Get
                FormY = myFormY
            End Get
            Set(ByVal Value As Integer)
                myFormY = Value
            End Set
        End Property

        Public Property FormHeight() As Integer
            Get
                FormHeight = myFormHeight
            End Get
            Set(ByVal Value As Integer)
                myFormHeight = Value
            End Set
        End Property

        Public Property FormWidth() As Integer
            Get
                FormWidth = myFormWidth
            End Get
            Set(ByVal Value As Integer)
                myFormWidth = Value
            End Set
        End Property

        Public Property FormTopMost() As Boolean
            Get
                FormTopMost = myFormTopMost
            End Get
            Set(ByVal Value As Boolean)
                myFormTopMost = Value
            End Set
        End Property

    End Class       ' clsHistoryFormSettings

    Public Class clsPlotPoints

        Private myNumberOfPoints As Integer
        Private myPointNumber As Integer

        Private myPtLatitude As Double
        Private myPtLongitude As Double

        Private myLayerName As String = ""
        Private myUnitId As String = ""
        Private myRegNo As String = ""
        Private myPtText1 As String = ""
        Private myPtText2 As String = ""
        Private myCompany As String = ""
        Private myFleet As String = ""
        Private myEventType As String = "0"     ' 0 = Normal, 1 = Event, 2 = Ign ON, 3 = Ign OFF
        Private myIgnState As String = "0"      ' [ 0 => unknown, 1 => OFF, 2 => ON]
        Private myPointHeading As String = "0"

        ' Added 20060424
        Private myDeviceType As String = ""

        ' 20090520 
        Private myDriverName As String = ""
        Private myDriverId As String = ""

        Private myInput1 As Double = 0.0
        Private myInput2 As Double = 0.0
        Private myInput3 As Double = 0.0
        Private myInput4 As Double = 0.0
        Private myInput5 As Double = 0.0
        Private myInput6 As Double = 0.0

        Private myTransdate As String
        Private myLocation As String = ""
        ' Added 20060413  KSM
        Private myInputs As String = "0000"     ' Only catering for 4 inputs on display query at this stage

        Private myTextArray(10001, 18) As String ' Changed from 4 to 5  20060413
        '                                           Changed to add 6 additional Inputs, Company, Fleet, UnitId and RegNo 20060420  KSM

        Private myPointArray(10001, 1) As Double

        Public Property Location() As String
            Get
                Location = myLocation
            End Get
            Set(ByVal Value As String)
                myLocation = Value
            End Set
        End Property
        Public Property LayerName() As String
            Get
                LayerName = myLayerName
            End Get
            Set(ByVal Value As String)
                myLayerName = Value
            End Set
        End Property    ' LayerName

        Public Property NumberOfPoints() As Integer
            Get
                NumberOfPoints = myNumberOfPoints
            End Get
            Set(ByVal Value As Integer)
                myNumberOfPoints = Value
            End Set
        End Property    ' NumberOfPoints

        Public Property PointNumber() As Integer
            Get
                PointNumber = myPointNumber
            End Get
            Set(ByVal Value As Integer)
                myPointNumber = Value
            End Set
        End Property    ' PointNumber

        Public Property UnitId() As String
            Get
                UnitId = myTextArray(myPointNumber, 14)
            End Get
            Set(ByVal Value As String)
                myUnitId = Value
            End Set
        End Property        ' UnitId

        Public Property RegNo() As String
            Get
                RegNo = myTextArray(myPointNumber, 15)
            End Get
            Set(ByVal Value As String)
                myRegNo = Value
            End Set
        End Property        ' RegNo


        Public Property DeviceType() As String
            Get
                DeviceType = myTextArray(myPointNumber, 17)
            End Get
            Set(ByVal Value As String)
                myDeviceType = Value
            End Set
        End Property

        Public Property TransDate() As String
            Get
                TransDate = myTextArray(myPointNumber, 16)
            End Get
            Set(ByVal Value As String)
                myTransdate = Value
            End Set
        End Property

        Public Property PtLatitude() As Double
            Get
                PtLatitude = myPointArray(myPointNumber, 0)
            End Get
            Set(ByVal Value As Double)
                myPtLatitude = Value
            End Set
        End Property    ' PtLatitude

        Public Property PtLongitude() As Double
            Get
                PtLongitude = myPointArray(myPointNumber, 1)
            End Get
            Set(ByVal Value As Double)
                myPtLongitude = Value
            End Set
        End Property    ' PtLongitude

        Public Property PtText1() As String
            Get
                PtText1 = myTextArray(myPointNumber, 0)
            End Get
            Set(ByVal Value As String)
                myPtText1 = Value
            End Set
        End Property    ' PtText1

        Public Property PtText2() As String
            Get
                PtText2 = myTextArray(myPointNumber, 1)
            End Get
            Set(ByVal Value As String)
                myPtText2 = Value
            End Set
        End Property        ' PtText2

        Public Property EventType() As String
            Get
                EventType = myTextArray(myPointNumber, 2)
            End Get
            Set(ByVal Value As String)
                myEventType = Value
            End Set
        End Property

        Public Property IgnState() As String
            Get
                IgnState = myTextArray(myPointNumber, 3)
            End Get
            Set(ByVal Value As String)
                myIgnState = Value
            End Set
        End Property    ' IgnState

        Public Property PointHeading() As String
            Get
                PointHeading = myTextArray(myPointNumber, 4)
            End Get
            Set(ByVal Value As String)
                myPointHeading = Value
            End Set
        End Property    ' PointHeading


        Public Property Company() As String
            Get
                Company = myTextArray(myPointNumber, 12)        'myCompany
            End Get
            Set(ByVal Value As String)
                myCompany = Value
            End Set
        End Property        ' Company

        Public Property Fleet() As String
            Get
                Fleet = myTextArray(myPointNumber, 13)      'myFleet
            End Get
            Set(ByVal Value As String)
                myFleet = Value
            End Set
        End Property        ' Fleet

        Public Property DeviceInputs() As String
            Get
                DeviceInputs = myTextArray(myPointNumber, 5)
            End Get
            Set(ByVal Value As String)
                myInputs = Value
            End Set
        End Property        ' DeviceInputs

        Public Property Input1() As Double
            Get
                Input1 = CDbl(myTextArray(myPointNumber, 6))    'myInput1
            End Get
            Set(ByVal Value As Double)
                myInput1 = Value
            End Set
        End Property        ' Input1


        Public Property Input2() As Double
            Get
                Input2 = CDbl(myTextArray(myPointNumber, 7))        'myInput2
            End Get
            Set(ByVal Value As Double)
                myInput2 = Value
            End Set
        End Property        ' Input2

        Public Property Input3() As Double
            Get
                Input3 = CDbl(myTextArray(myPointNumber, 8))        'myInput3
            End Get
            Set(ByVal Value As Double)
                myInput3 = Value
            End Set
        End Property        ' Input3

        Public Property Input4() As Double
            Get
                Input4 = CDbl(myTextArray(myPointNumber, 9))       'myInput4
            End Get
            Set(ByVal Value As Double)
                myInput4 = Value
            End Set
        End Property        ' Input4

        Public Property Input5() As Double
            Get
                Input5 = CDbl(myTextArray(myPointNumber, 10))        'myInput5
            End Get
            Set(ByVal Value As Double)
                myInput5 = Value
            End Set
        End Property        ' Input5

        Public Property Input6() As Double
            Get
                Input6 = CDbl(myTextArray(myPointNumber, 11))       'myInput6
            End Get
            Set(ByVal Value As Double)
                myInput6 = Value
            End Set
        End Property        ' Input6

        Public Property DriverName() As String
            Get
                DriverName = myDriverName
            End Get
            Set(ByVal value As String)
                myDriverName = value
            End Set
        End Property    ' DriverName

        Public Property DriverId() As String
            Get
                DriverId = myDriverId
            End Get
            Set(ByVal value As String)
                myDriverId = value
            End Set
        End Property    ' DriverId

        Public Property UpdateValues() As Boolean
            Get
                UpdateValues = False
            End Get
            Set(ByVal Value As Boolean)
                If Value = True Then
                    Try
                        myPointArray(myPointNumber, 0) = myPtLatitude
                        myPointArray(myPointNumber, 1) = myPtLongitude
                        myTextArray(myPointNumber, 0) = myPtText1
                        myTextArray(myPointNumber, 1) = myPtText2
                        myTextArray(myPointNumber, 2) = myEventType
                        myTextArray(myPointNumber, 3) = myIgnState
                        myTextArray(myPointNumber, 4) = myPointHeading
                        myTextArray(myPointNumber, 5) = myInputs
                        'MsgBox("Point " & CStr(myPointNumber) & ";  " & myTextArray(myPointNumber, 5))
                        myTextArray(myPointNumber, 6) = CStr(myInput1)
                        myTextArray(myPointNumber, 7) = CStr(myInput2)
                        myTextArray(myPointNumber, 8) = CStr(myInput3)
                        myTextArray(myPointNumber, 9) = CStr(myInput4)
                        myTextArray(myPointNumber, 10) = CStr(myInput5)
                        myTextArray(myPointNumber, 11) = CStr(myInput6)
                        myTextArray(myPointNumber, 12) = myCompany
                        myTextArray(myPointNumber, 13) = myFleet
                        myTextArray(myPointNumber, 14) = myUnitId
                        myTextArray(myPointNumber, 15) = myRegNo
                        myTextArray(myPointNumber, 16) = myTransdate
                        myTextArray(myPointNumber, 17) = myDeviceType   ' Added 20060424
                    Catch ex As Exception
                        'MsgBox(ex.Message)
                    End Try
                End If
            End Set
        End Property    ' UpdateValues

    End Class       ' clsPlotPoints
End Class

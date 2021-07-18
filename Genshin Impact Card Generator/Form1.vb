Imports System.IO


'When finishing laying out textboxes use event handler for imageUpdate()
'Find a different font to use for UUID and one for subheader
'Find a UI layout and color scheme
Public Class Form1
#Region "Variables"
    'ID Card
    Dim UUID As String = Nothing
    Dim fileName As String = "ID Card"

    'Image
    Dim defaultImage As Bitmap
    Dim orgImage As Bitmap

    'Save Path
    Dim _path = AppDomain.CurrentDomain.BaseDirectory
    Dim dir As String = Path.GetDirectoryName(_path)
    Dim newDir = Directory.CreateDirectory($"{dir}\Saved Images")

    'Fonts
    Dim headerFont As Font = New Font("Monsterrat", 36, FontStyle.Regular)
    Dim subHeaderFont As Font = New Font("Monsterrat", 36, FontStyle.Regular)

    'Misc
    Dim images As Image() = { 'Will be changed to images class
        My.Resources.Klee_Banner,
        My.Resources.Achievement__2_
    }
#End Region

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pb1.BackgroundImage = images(0)
        pbTest.BackgroundImage = images(1)
        defaultImage = pb1.BackgroundImage
        orgImage = pb1.BackgroundImage
    End Sub

    Private Sub txtBoxUUID_TextChanged(sender As Object, e As EventArgs) Handles txtBoxUUID.TextChanged
        imageUpdate()
    End Sub

    Private Sub btnUUID_Click(sender As Object, e As EventArgs) Handles btnUUID.Click
        imageSave()
    End Sub

#Region "Methods"

    Private Sub imageUpdate()
        UUID = txtBoxUUID.Text
        Dim newImage As Bitmap = New Bitmap(defaultImage)
        imageRepeat(newImage)

        pb1.BackgroundImage = Nothing
        pb1.BackgroundImage = newImage
    End Sub

    Private Sub imageSave()
        UUID = txtBoxUUID.Text
        fileName = txtBoxName.Text
        Dim newImage As Bitmap = New Bitmap(defaultImage)
        If txtBoxName.Text IsNot String.Empty Then 'Check if ID Card.png already exists if it does increment file
            fileName = txtBoxName.Text
        Else 'Check if ID Card.png already exists if it does increment file
            fileName = "ID Card"
        End If
        imageRepeat(newImage)
        newImage.Save($"{newDir}/{fileName}.{Imaging.ImageFormat.Png}")
    End Sub

    Private Sub imageRepeat(img As Bitmap)
        Dim g As Graphics
        g = Graphics.FromImage(img)
        Using sf = New StringFormat() With {
                    .Alignment = StringAlignment.Center,
                    .LineAlignment = StringAlignment.Center
            }
            g.DrawString(UUID, headerFont, New SolidBrush(Color.White), New Rectangle(0, 0, img.Width, img.Height), sf)
        End Using
    End Sub

    Private Sub imageChange(sender As Object, e As EventArgs)
        Dim img As PictureBox
        img = CType(sender, PictureBox)
        pb1.BackgroundImage = img.BackgroundImage
        defaultImage = pb1.BackgroundImage
        img.BackgroundImage = orgImage
        orgImage = pb1.BackgroundImage
        imageUpdate()
    End Sub
#End Region

#Region "Custom Events"
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        For Each pb As PictureBox In pnlImages.Controls.OfType(Of PictureBox)
            AddHandler pb.Click, AddressOf imageChange
        Next

    End Sub
#End Region

End Class

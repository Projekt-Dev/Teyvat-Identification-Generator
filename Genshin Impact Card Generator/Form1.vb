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
    Dim rescSet As Resources.ResourceSet = My.Resources.ResourceManager.GetResourceSet(Globalization.CultureInfo.CurrentCulture, True, True)
    Dim imgList As New List(Of Image)
    Dim newSize As Size = New Size(420, 200)

#End Region

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        imageDefaults()

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

    Private Sub imageDefaults()
        For Each dict As DictionaryEntry In rescSet.OfType(Of Object)
            If TypeOf dict.Value Is Image Then
                imgList.Add(dict.Value)
            End If
        Next
        pb1.BackgroundImage = imgList(0)
        defaultImage = pb1.BackgroundImage
        orgImage = pb1.BackgroundImage

        For i = 1 To imgList.Count - 1
            Dim pb As PictureBox = New PictureBox
            pb.BackgroundImage = imgList(i)
            pb.BackgroundImageLayout = ImageLayout.Stretch
            pb.Size = newSize
            imgPanel.Controls.Add(pb)
            AddHandler pb.Click, AddressOf imageChange
        Next

    End Sub

#End Region

End Class

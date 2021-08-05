Imports System.IO


'When finishing laying out textboxes use event handler for imageUpdate()
'Find a different font to use for UUID and one for subheader
'Find a UI layout and color scheme
'Fix window scaling bug
Public Class Form1
#Region "Variables"
    'ID Card
    Dim UUID As String = Nothing
    Dim fileName As String = "ID Card"

    'Image
    Dim defaultImage As Bitmap
    Dim orgImage As Bitmap
    Dim imgSize As Size = New Size(420, 200)

    Dim imgPath = Path.GetDirectoryName($"{_path}\Images")
    Dim imgList As New List(Of Image)

    'Saves
    Dim _path = AppDomain.CurrentDomain.BaseDirectory
    Dim dir As String = Path.GetDirectoryName(_path)
    Dim saveDir = Directory.CreateDirectory($"{dir}\Saved Images")

    'Fonts
    Dim headerFont As Font = New Font("Monsterrat", 36, FontStyle.Regular)
    Dim subHeaderFont As Font = New Font("Monsterrat", 36, FontStyle.Regular)

    'Misc
    Dim rescSet As Resources.ResourceSet = My.Resources.ResourceManager.GetResourceSet(Globalization.CultureInfo.CurrentCulture, True, True)
    Dim util As New utilities

#End Region

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        formDefaults()
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
        If txtBoxName.Text IsNot String.Empty Then 'Check if ID Card.png already exists if it does increment file[Not Added]
            fileName = txtBoxName.Text
        Else 'Check if ID Card.png already exists if it does increment file[Not Added]
            fileName = "ID Card"
        End If
        imageRepeat(newImage)
        newImage.Save($"{saveDir}/{fileName}.{Imaging.ImageFormat.Png}")
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

    'Changes current image to a image click from the picturebox
    Private Sub imageChange(sender As Object, e As EventArgs)
        Dim pb As PictureBox
        pb = CType(sender, PictureBox)
        pb1.BackgroundImage = pb.BackgroundImage
        defaultImage = pb1.BackgroundImage
        pb.BackgroundImage = orgImage
        orgImage = pb1.BackgroundImage
        imageUpdate()
    End Sub

    'Anything we would want to run on form load put in here.
    Private Sub formDefaults()

        'Go through resources and if an image is found add to list?
        For Each dict As DictionaryEntry In rescSet.OfType(Of Object)
            If TypeOf dict.Value Is Image Then
                imgList.Add(dict.Value)
            End If
        Next

        'Goes through imgList and if an image with a width smaller than the given number is found it will be removed and then
        For Each img As Image In imgList.ToList
            If img.Width < 800 Then
                imgList.Remove(img)
            End If
        Next

        pb1.BackgroundImage = imgList(0)
        defaultImage = pb1.BackgroundImage
        orgImage = pb1.BackgroundImage
        'util.shuffleArray(imgList) 'Shuffle list of images

        'Creates a picturebox for every image in the imgList with some default settings
        For i = 1 To imgList.Count - 1

            Dim pb As New PictureBox() With {
                .BackgroundImage = imgList(i),
                .BackgroundImageLayout = ImageLayout.Stretch,
                .Size = imgSize
                }
            imgPanel.Controls.Add(pb) 'add picturebox to the imgPanel control

            'Event handler - every picturebox click would activate imageChange method
            AddHandler pb.Click, AddressOf imageChange
        Next

    End Sub

#End Region

End Class
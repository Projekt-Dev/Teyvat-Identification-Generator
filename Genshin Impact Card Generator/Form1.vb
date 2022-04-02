Imports System.IO

'When finishing laying out textboxes use event handler for imageUpdate()
'Find a different font to use for UUID and one for subheader
'Find a UI layout and color scheme
'Fix window scaling bug
Public Class Form1
#Region "Variables"
    'ID Card
    Dim UUID As String = Nothing

    'Image
    Dim defaultImage As Bitmap
    Dim orgImage As Bitmap
    Dim imgSize As New Size(420, 200)
    Dim imgList As New List(Of Image)

    'Saves
    ReadOnly _path = AppDomain.CurrentDomain.BaseDirectory
    ReadOnly dir As String = Path.GetDirectoryName(_path)
    ReadOnly saveDir = Directory.CreateDirectory($"{dir}\Saved Images")

    'Fonts
    ReadOnly headerFont As New Font("Montserrat", 36, FontStyle.Regular)
    ReadOnly subHeaderFont As New Font("Montserrat", 36, FontStyle.Regular)

    'Misc
    ReadOnly rescSet As Resources.ResourceSet = My.Resources.ResourceManager.GetResourceSet(Globalization.CultureInfo.CurrentCulture, True, True)
    ReadOnly util As New utilities
#End Region

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormDefaults()
    End Sub
    'Anything we would want to run on form load put in here.
    Private Sub FormDefaults()
        'GrabImages()
        FolderImages()
    End Sub


#Region "Button Events"
    Private Sub TxtBoxUUID_TextChanged(sender As Object, e As EventArgs) Handles txtBoxUUID.TextChanged
        'Check for character limit of 9
        Dim charLimit = 9
        Dim currentCharCount = txtBoxUUID.Text.Length
        If currentCharCount > charLimit Then
            MessageBox.Show("UUID's are 9 numbers long.")
            txtBoxUUID.Text = txtBoxUUID.Text.Remove(txtBoxUUID.Text.Length - 1)
        Else
            ImageUpdate()
        End If
    End Sub
    'Makes the TextBox only allow numbers
    Private Sub TxtBoxUUID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBoxUUID.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> ".") Then
            e.Handled = True
        End If
    End Sub
    Private Sub BtnUUID_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ImageSave()
    End Sub

#End Region

#Region "Methods"

    Private Sub ImageUpdate()
        UUID = txtBoxUUID.Text
        Dim newImage As New Bitmap(defaultImage)
        ImageText(newImage)

        pb1.BackgroundImage = Nothing
        pb1.BackgroundImage = newImage
    End Sub

    Private Sub ImageSave()
        UUID = txtBoxUUID.Text
        Dim fileIncrement As Integer = 0
        Dim fileName = $"ID Card - {fileIncrement}"
        Dim newImage As New Bitmap(defaultImage)

        'Check if ID Card - 0.png already exists if it does increment file
        While File.Exists($"{saveDir}/{fileName}.{Imaging.ImageFormat.Png}")
            If File.Exists($"{saveDir}/{fileName}.{Imaging.ImageFormat.Png}") Then
                fileIncrement += 1
                fileName = $"ID Card - {fileIncrement}"
            Else
                Exit While
            End If
        End While

        fileName = $"ID Card - {fileIncrement}"
        ImageText(newImage)
        newImage.Save($"{saveDir}/{fileName}.{Imaging.ImageFormat.Png}")
    End Sub

    'Add text to the image
    Private Sub ImageText(img As Bitmap)
        Dim g As Graphics
        g = Graphics.FromImage(img)
        Using sf = New StringFormat() With {
                    .Alignment = StringAlignment.Center,
                    .LineAlignment = StringAlignment.Center
            }
            g.DrawString(UUID, headerFont, New SolidBrush(Color.White), New Rectangle(0, 125, img.Width, img.Height), sf)
        End Using
    End Sub

    'Changes current image to a image clicked from a PictureBox
    Private Sub ImageChange(sender As Object, e As EventArgs)
        Dim pb As PictureBox
        pb = CType(sender, PictureBox)
        pb1.BackgroundImage = pb.BackgroundImage
        defaultImage = pb1.BackgroundImage
        pb.BackgroundImage = orgImage
        orgImage = pb1.BackgroundImage
        ImageUpdate()
    End Sub

    'Get all images from resources and add them to the image selection.
    'Private Sub GrabImages()
    '    'Go through resources and if an image is found add to list
    '    Dim t_List As New List(Of Image)
    '    For Each dict As DictionaryEntry In rescSet.OfType(Of Object)
    '        If TypeOf dict.Value Is Image Then
    '            t_List.Add(dict.Value)
    '        End If
    '    Next
    '    Dim rand As New Random
    '    imgList = t_List.OrderBy(Function(x) rand.Next()).ToList()
    '    'util.Sort(Of Image)(imgList)
    '    'Goes through imgList and if an image with a width smaller than the given number is found it will be removed
    '    For Each img As Image In imgList.ToList
    '        If img.Width < 800 Then
    '            imgList.Remove(img)
    '        End If
    '    Next

    '    imgList.OrderBy(Function(f) f.Tag).ToList()


    '    pb1.BackgroundImage = imgList(0)
    '    defaultImage = pb1.BackgroundImage
    '    orgImage = pb1.BackgroundImage

    '    'Creates a PictureBox for every image in the imgList with some default settings
    '    For i = 1 To imgList.Count - 1

    '        Dim pb As New PictureBox() With {
    '            .BackgroundImage = imgList(i),
    '            .BackgroundImageLayout = ImageLayout.Stretch,
    '            .Size = imgSize
    '            }
    '        imgPanel.Controls.Add(pb) 'add PictureBox to the imgPanel control

    '        'Event handler - every PictureBox click would activate imageChange method
    '        AddHandler pb.Click, AddressOf ImageChange
    '    Next
    'End Sub

    Private Sub FolderImages()
        Dim folderImg As List(Of String) = Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}\Images", "*.png").ToList

        pb1.BackgroundImage = New Bitmap(folderImg(0))
        defaultImage = pb1.BackgroundImage
        orgImage = pb1.BackgroundImage

        'Creates a PictureBox for every image in the imgList with some default settings
        For i = 1 To folderImg.Count - 1

            Dim pb As New PictureBox() With {
                .BackgroundImage = New Bitmap(folderImg(i)),
                .BackgroundImageLayout = ImageLayout.Stretch,
                .Size = imgSize
                }
            imgPanel.Controls.Add(pb) 'add PictureBox to the imgPanel control

            'Event handler - every PictureBox click would activate imageChange method
            AddHandler pb.Click, AddressOf ImageChange
        Next

    End Sub

#End Region

End Class
Imports System.Drawing.Drawing2D
Imports System.IO

NotInheritable Class Images
#Region "Variables"
    'Image
    Private Shared defaultImage As Bitmap
    Private Shared orgImage As Bitmap
    Private Shared imgSize As New Size(420, 200)
    Private Shared ReadOnly imgList As New List(Of Image)
    Private Shared avatar As Bitmap
    Private Shared ReadOnly bannerImg As New List(Of Image)
    Private Shared profileImg As List(Of String)

    'ID Card
    Private Shared UUID As String = String.Empty

    'Saves
    Private Shared ReadOnly path = AppDomain.CurrentDomain.BaseDirectory
    Private Shared ReadOnly dir As String = System.IO.Path.GetDirectoryName(path)
    Private Shared ReadOnly saveDir = Directory.CreateDirectory($"{dir}\Saved Images")

    'Fonts
    Private Shared ReadOnly headerFont As New Font("Montserrat", 36, FontStyle.Regular)
    Private Shared ReadOnly subHeaderFont As New Font("Montserrat", 36, FontStyle.Regular)

    'Misc
    Private Shared ReadOnly images As New Images
    Private Shared ReadOnly avaPB As PictureBox
#End Region

    Private Shared Function CreateBannerAsync(banner As Image, avatar As Image)
        banner = CropToBanner(banner)
        Dim border = CircleToBorder(avatar)
        avatar = ClipImageToCircle(avatar)

        Dim amap As Bitmap = TryCast(avatar, Bitmap)
        Dim bmap As Bitmap = TryCast(border, Bitmap)
        bmap?.MakeTransparent()
        amap?.MakeTransparent()

        Dim _banner = CopyRegionIntoBanner(bmap, amap, banner)

        Return _banner
    End Function

    Private Shared Function CopyRegionIntoBanner(bmap As Bitmap, amap As Bitmap, banner As Image) As Object
        Using g = Graphics.FromImage(banner)
            Dim x = (banner.Width / 2) - 400
            Dim y = (banner.Height / 2) - 155
            Dim _x = (banner.Width / 2) - 404
            Dim _y = (banner.Height / 2) - 159

            g.DrawImage(bmap, CInt(_x), CInt(_y), 233, 233)
            g.DrawImage(amap, CInt(x), CInt(y), 225, 225)

            Return banner
        End Using
    End Function

    Private Shared Function CircleToBorder(ava As Image) As Object
        Dim destination As Image = New Bitmap(ava.Width, ava.Height, ava.PixelFormat)
        Dim radius = ava.Width / 3
        Dim x = ava.Width / 2
        Dim y = ava.Height / 2

        Using g = Graphics.FromImage(destination)
            Dim r = New Rectangle(x - radius, y - radius, radius * 2, radius * 2)
            Dim myPen = New Pen(Color.Crimson, x)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.CompositingQuality = CompositingQuality.HighQuality
            g.PixelOffsetMode = PixelOffsetMode.HighQuality
            g.SmoothingMode = SmoothingMode.AntiAlias

            Dim path = New GraphicsPath
            path.AddEllipse(r)
            g.SetClip(path)
            g.DrawEllipse(myPen, r)

            Return destination
        End Using

    End Function

    Private Shared Function ClipImageToCircle(ava As Image) As Image
        Dim destination As Image = New Bitmap(ava.Width, ava.Height, ava.PixelFormat)
        Dim radius = ava.Width / 3
        Dim x = ava.Width / 2
        Dim y = ava.Height / 2

        Using g = Graphics.FromImage(destination)
            Dim r As New Rectangle(x - radius, y - radius, radius * 2, radius * 2)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.CompositingQuality = CompositingQuality.HighQuality
            g.PixelOffsetMode = PixelOffsetMode.HighQuality
            g.SmoothingMode = SmoothingMode.AntiAlias

            Using brush As Brush = New SolidBrush(Color.Transparent)
                g.FillRectangle(brush, 0, 0, destination.Width, destination.Height)
            End Using

            Dim path = New GraphicsPath
            path.AddEllipse(r)
            g.SetClip(path)
            g.DrawImage(ava, 0, 0)

            Return destination
        End Using
    End Function

    Private Shared Function CropToBanner(banner As Image) As Image
        Dim OrgWidth = banner.Width
        Dim OrgHeight = banner.Height

        Dim destinationSize = New Size(840, 400)
        Dim heightRatio = Convert.ToDecimal(OrgHeight / destinationSize.Height)
        Dim widthRatio = Convert.ToDecimal(OrgWidth / destinationSize.Width)
        Dim ratio = Math.Min(heightRatio, widthRatio)

        Dim heightScale = Convert.ToInt32(destinationSize.Height * ratio)
        Dim widthScale = Convert.ToInt32(destinationSize.Width * ratio)
        Dim startX = (OrgWidth - widthScale) / 2
        Dim startY = (OrgHeight - heightScale) / 2

        Dim srcRectangle = New Rectangle(startX, startY, widthScale, heightScale)
        Dim bitmap = New Bitmap(destinationSize.Width, destinationSize.Height)
        Dim destinationRectangle = New Rectangle(0, 0, bitmap.Width, bitmap.Height)

        Using g = Graphics.FromImage(bitmap)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.DrawImage(banner, destinationRectangle, srcRectangle, GraphicsUnit.Pixel)
            Return bitmap
        End Using
    End Function

    Public Shared Sub GrabImages()
        Dim t_bannerImg = Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}\Images\Banners", "*.png").ToList
        profileImg = Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}\Images\Pfp", "*.png").ToList

        For Each b In t_bannerImg
            bannerImg.Add(Images.CropToBanner(New Bitmap(b)))
        Next

        Form1.pb1.BackgroundImage = New Bitmap(bannerImg(0))
        defaultImage = Form1.pb1.BackgroundImage
        orgImage = Form1.pb1.BackgroundImage
        avatar = New Bitmap(profileImg.First)

        Form1.pb1.BackgroundImage = CreateBannerAsync(orgImage, New Bitmap(profileImg.First))

        'Creates a PictureBox for every image in the imgList with some default settings
        For i = 1 To bannerImg.Count - 1

            Dim pb As New PictureBox() With {
                .BackgroundImage = New Bitmap(bannerImg(i)),
                .BackgroundImageLayout = ImageLayout.Stretch,
                .Size = imgSize
                }
            Form1.pnlBanners.Controls.Add(pb) 'add PictureBox to the pnlBanners control

            'Event handler - every PictureBox click would activate BannerChange method
            AddHandler pb.Click, AddressOf BannerChange
        Next
        For i = 0 To profileImg.Count - 1

            Dim pb As New PictureBox() With {
                .BackgroundImage = New Bitmap(profileImg(i)),
                .BackgroundImageLayout = ImageLayout.Stretch,
                .Size = New Size(200, 200)
                }
            Form1.pnlAvatars.Controls.Add(pb) 'add PictureBox to the pnlAvatars control

            'Event handler - every PictureBox click would activate AvatarChange method
            AddHandler pb.Click, AddressOf AvatarChange
        Next
        ImageText(Form1.pb1.BackgroundImage)
    End Sub

    'Changes current image to a image clicked from a PictureBox
    Private Shared Sub BannerChange(sender As Object, e As EventArgs)
        Dim pb As PictureBox
        pb = CType(sender, PictureBox)
        Form1.pb1.BackgroundImage = pb.BackgroundImage
        defaultImage = Form1.pb1.BackgroundImage
        pb.BackgroundImage = orgImage
        orgImage = Form1.pb1.BackgroundImage
        ImageUpdate()
    End Sub

    Private Shared Sub AvatarChange(sender As Object, e As EventArgs)
        Dim apb As PictureBox
        apb = CType(sender, PictureBox)
        'if the avatar and background image are different update image, if not do nothing
        If avatar IsNot apb.BackgroundImage Then
            avatar = apb.BackgroundImage
            ImageUpdate()
        End If
    End Sub

    Public Shared Sub ImageUpdate()
        UUID = Form1.txtBoxUUID.Text
        Dim newImage = CreateBannerAsync(defaultImage, avatar)
        ImageText(newImage)

        Form1.pb1.BackgroundImage = Nothing
        Form1.pb1.BackgroundImage = newImage
    End Sub

    Public Shared Sub ImageSave()
        Dim fileIncrement As Integer = 0
        Dim fileName = $"ID Card - {fileIncrement}"
        Dim newImage = CreateBannerAsync(Form1.pb1.BackgroundImage, avatar)

        'Check if ID Card - 0.png already exists if it does increment file
        While File.Exists($"{saveDir}/{fileName}.{Imaging.ImageFormat.Png}")
            If File.Exists($"{saveDir}/{fileName}.{Imaging.ImageFormat.Png}") Then
                fileIncrement += 1
                fileName = $"ID Card - {fileIncrement}"
            Else
                Exit While
            End If
        End While
        newImage.Save($"{saveDir}/{fileName}.{Imaging.ImageFormat.Png}")
    End Sub

    'Add text to the image
    Private Shared Sub ImageText(img As Bitmap)
        Dim g As Graphics
        g = Graphics.FromImage(img)
        Using sf = New StringFormat() With {
                    .Alignment = StringAlignment.Center,
                    .LineAlignment = StringAlignment.Center
            }
            'g.DrawString(UUID, headerFont, New SolidBrush(Color.Black), New Rectangle(0, 125, img.Width, img.Height), sf)
            g.DrawString($"{UUID}", headerFont, New SolidBrush(Color.Red), New Rectangle(-25, -35, img.Width, img.Height), sf)
        End Using
    End Sub

End Class
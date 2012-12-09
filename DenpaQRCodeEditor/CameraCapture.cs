using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using DenpaQRCodeEditor.WebCam;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode;


namespace DenpaQRCodeEditor
{
    public partial class CameraCapture : Form
    {
        private QRCodeReader _reader = new QRCodeReader();
        public byte[] ByteArray;
        public byte[] previous_bytes = null;
        public bool AutoReturn = false;

        public CameraCapture()
        {
            InitializeComponent();
        }

        public static byte[] GetByteArray(bool autoreturn = false, Byte[] previous_data = null )
        {
            var capture = new CameraCapture();
            capture.AutoReturn = autoreturn;
            capture.previous_bytes = previous_data;
            return capture.ShowDialog() == DialogResult.OK ? capture.ByteArray : null;
        }

        private void WebCamCaptureImageCaptured(object source, WebcamEventArgs e)
        {
            pictureCamera.Image = e.WebCamImage;

            try
            {
                var bmp = new Bitmap(pictureCamera.Image);
                var binary = new BinaryBitmap(new HybridBinarizer(new RGBLuminanceSource(bmp, bmp.Width, bmp.Height)));

                var result = _reader.decode(binary);
                if (((ArrayList)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS]) != null)
                    ByteArray = (byte[])((ArrayList)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS])[0];
                else
                    ByteArray = System.Text.Encoding.ASCII.GetBytes(result.Text);
                if (previous_bytes != null)
                {
                    if (previous_bytes.Length == ByteArray.Length)
                    {
                        bool data_same_as_previous = true;
                        for (int i = 0; (i < previous_bytes.Length) && data_same_as_previous; i++)
                            if (previous_bytes[i] != ByteArray[i])
                                data_same_as_previous = false;
                        if(data_same_as_previous)
                            throw new Exception("Data captures is same as previous data");
                    }
                }
                if (AutoReturn)
                    DialogResult = DialogResult.OK;
                else
                    if (MessageBox.Show(@"Data captured, Do you want to return it?", "Data Captured", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        DialogResult = DialogResult.OK;
                    }
            }
            catch// (ReaderException ex)
            {
                ByteArray = null;
            }
        }

        private void CameraCapture_Load(object sender, System.EventArgs e)
        {
            webcamCapture.CaptureWidth = pictureCamera.Width;
            webcamCapture.CaptureHeight = pictureCamera.Height;

            webcamCapture.TimeToCapture_milliseconds = 40;

            // Start the video capture. let the control handle the frame numbers.
            webcamCapture.Start(0);
        }

        private void CameraCapture_FormClosing(object sender, FormClosingEventArgs e)
        {
            webcamCapture.Stop();
        }

        private void btnStartStop_Click(object sender, System.EventArgs e)
        {
            if (webcamCapture.IsRunning())
            {
                webcamCapture.Stop();
                btnStartStop.Text = @"Start";
            }
            else
            {
                webcamCapture.Start(0);
                btnStartStop.Text = @"Stop";
            }
        }
    }

}

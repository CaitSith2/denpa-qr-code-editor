using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using PushmoLevelEditor.Properties;
using com.google.zxing;
using com.google.zxing.qrcode;
using com.google.zxing.common;

/*
 * Todolist:
 * 
 * TODO: add undo/redo
 * TODO: figure out the rest of the flags of the pushmo...
 * TODO: make drawing even faster by using draw-per-change instead of draw-the-all-thing-on-every-change
 */

namespace PushmoLevelEditor
{
    public partial class FormEditor : Form
    {
        private byte[] QRByteArray;
        private Bitmap qr_code;

        public FormEditor()
        {
            InitializeComponent();
            menuFileNew_Click(null, null);
        }
        
        #region Menu -> File

        private void menuFileNew_Click(object sender, EventArgs e)
        {
            var data = new Byte[0x6A];
            QRByteArray = data;
            if (hexBox1.ByteProvider != null)
            {
                IDisposable byteProvider = hexBox1.ByteProvider as IDisposable;
                if (byteProvider != null)
                    byteProvider.Dispose();
                hexBox1.ByteProvider = null;
            }
            hexBox1.ByteProvider = new Be.Windows.Forms.DynamicByteProvider(QRByteArray);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;
            comboBox9.SelectedIndex = 0;
            comboBox10.SelectedIndex = 0;
            comboBox11.SelectedIndex = 0;
            comboBox12.SelectedIndex = 0;
            numericUpDown1.Value = 0;
            textBox1.Text = "New Denpa";
            checkBox1.Checked = false;
            checkBox1_CheckedChanged(sender, e);
            button3_Click_1(sender, e);
            
        }

        private void menuFileOpen_Click(object sender, EventArgs e)
        {
        }

        private void SavePushmo()
        {
        }

        private void menuFileSave_Click(object sender, EventArgs e)
        {
        }

        private void menuFileSaveAs_Click(object sender, EventArgs e)
        {
        }

        private void menuFileImport_Click(object sender, EventArgs e)
        {
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Menu -> QR Code

        private byte[] decrypt_byte_array(byte[] bytearray)
        {
            //int i;
            //if (bytearray == null)
            //    return null;
            //for (i = 0; i < (bytearray.Length / 2); i++)
            //{
            //    switch (bytearray[(i * 2) + 1])
            //    {
            //        case 0x00:
            //        case 0x10:
            //        case 0x20:
            //        case 0x30:
            //        case 0x40:
            //        case 0x50:
            //        case 0x60:
            //        case 0x70:
            //        case 0x80:
            //        case 0x90:
            //        case 0xA0:
            //        case 0xB0:
            //        case 0xC0:
            //        case 0xD0:
            //        case 0xE0:
            //        case 0xF0:
            //            bytearray[(i * 2) + 1] = 0;
            //            break;

            //        case 0x01:
            //        case 0x81:
            //            bytearray[(i * 2)] = (byte)((bytearray[(i * 2)] ^ 1) - (((bytearray[(i * 2)] & 1) == 1) ? 2 : 0));
            //            bytearray[(i * 2) + 1] = 0;
            //            break;

            //        case 0x02:
            //        case 0x82:
            //            bytearray[(i * 2) + 0] -= 4;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x03:
            //        case 0x83:
            //            bytearray[(i * 2) + 0] -= (byte)(((bytearray[(i * 2) + 0] % 4)== 3)?15:7);
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x04:
            //        case 0x84:
            //            bytearray[(i * 2) + 0] -= 16;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x05:
            //        case 0x45:
            //        case 0x85: 
            //        case 0xC5:
            //            var sub_table = new byte[16] { 0x17, 0x13, 0x18, 0x1B, 0x17, 0x23, 0x17, 0x1B, 0x17, 0x13, 0x17, 0x1B, 0x17, 0x23, 0x17, 0x1B };
            //            bytearray[(i * 2) + 0] -= sub_table[bytearray[(i * 2) + 0] & 0x0F];
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x25:
            //        case 0x65:
            //        case 0xA5:
            //        case 0xE5:
            //            break;
            //        case 0x06:
            //        case 0x86:
            //            bytearray[(i * 2) + 0] -= (byte)(((bytearray[(i * 2) + 0] % 8) < 4) ? 0x1C : 0x2C);
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x07:
            //        case 0x47:
            //        case 0x87:
            //        case 0xC7:
            //            bytearray[(i * 2) + 0] -= (byte)(((bytearray[(i * 2) + 0] % 8) == 7) ? 0x3F : 0x2F);
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x08:
            //        case 0x18:
            //        case 0x28:
            //        case 0x38:
            //        case 0x88:
            //        case 0x98:
            //        case 0xA8:
            //        case 0xB8:
            //            bytearray[(i * 2) + 0] -= 64;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x09:
            //        case 0x29:
            //        case 0x89:
            //        case 0xA9:
            //            var sub_table_09 = new byte[16] { 0x4F, 0x43, 0x4F, 0x53, 0x4F, 0x53, 0x4F, 0x53, 0x4F, 0x63, 0x4F, 0x53, 0x4F, 0x53, 0x4F, 0x53 };
            //            bytearray[(i * 2) + 0] -= sub_table_09[bytearray[(i * 2) + 0] & 0x0F];
            //            bytearray[(i * 2) + 1] = 0;
            //            break;

            //        case 0x0F:
            //        case 0x2F:
            //        case 0x4F:
            //        case 0x6F:
            //        case 0x8F:
            //        case 0xAF:
            //        case 0xCF:
            //        case 0xEF:
            //            bytearray[(i * 2)] += 0x21;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;

            //        case 0x11:
            //        case 0x71:
            //        case 0x91:
            //        case 0xF1:
            //            var offset = bytearray[(i * 2)];
            //            bytearray[(i * 2)] = (byte)((bytearray[(i * 2)] ^ 1) - (((bytearray[(i * 2)] & 1) == 1) ? 2 : 0));
            //            if((offset & 0x1F) != 0x01)
            //                bytearray[(i * 2)] -= 0x20;
            //            if ((offset & 0x1F) == 0x11)
            //                bytearray[(i * 2)] -= 0x30;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;

            //        case 0x3F:
            //        case 0xBF:
            //            bytearray[(i * 2)] += 0x81;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;

            //        case 0x1F:
            //        case 0x5F:
            //        case 0x9F:
            //        case 0xDF:
            //            bytearray[(i * 2)] += 0x41;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
                    
                    
                    
            //        case 0x77:
            //        case 0xF7:
            //            if ((bytearray[(i * 2)] % 8) == 7)
            //                bytearray[(i * 2)] += 0x61;
            //            else
            //                bytearray[(i * 2)] += 0x51;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x78:
            //        case 0xF8:
            //            bytearray[(i * 2) + 0] += 0x40;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x79:
            //        case 0xF9:
            //            if ((bytearray[(i * 2)] % 8) == 1)
            //                bytearray[(i * 2)] += 0x3D;
            //            else if ((bytearray[(i * 2)] % 2) == 0)
            //                bytearray[(i * 2)] += 0x31;
            //            else
            //                bytearray[(i * 2)] += 0x2D;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x7A:
            //        case 0xFA:
            //            if ((bytearray[(i * 2)] % 8) < 4)
            //                bytearray[(i * 2)] += 0x2C;
            //            else
            //                bytearray[(i * 2)] += 0x1C;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x7B:
            //        case 0xFB:
            //            if ((bytearray[(i * 2)] % 8) == 3)
            //                bytearray[(i * 2)] += 0x21;
            //            else if ((bytearray[(i * 2)] % 8) == 7)
            //                bytearray[(i * 2)] += 0x11;
            //            else
            //                bytearray[(i * 2)] += 0x19;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x7C:
            //        case 0xFC:
            //            bytearray[(i * 2) + 0] += 16;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x7D:
            //        case 0xFD:
            //            if ((bytearray[(i * 2)] % 4) == 1)
            //                bytearray[(i * 2)] += 0x0D;
            //            else if ((bytearray[(i * 2)] % 4) == 3)
            //                bytearray[(i * 2)] += 0x05;
            //            else
            //                bytearray[(i * 2)] += 0x09;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x3E:
            //        case 0x7E:
            //        case 0xBE:
            //        case 0xFE:
            //            bytearray[(i * 2) + 0] += 4;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x7F:
            //        case 0xFF:
            //            bytearray[(i * 2) + 0] += 1;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        default:
            //            break;
            //    }
               
            //}

            return bytearray;
        }

        private void menuQRCodeRead_Click(object sender, EventArgs e)
        {
            
        }

        private ByteMatrix GetQRMatrix(int size)
        {
            var writer = new QRCodeWriter();
            const string encoding = "ISO-8859-1";
            if (hexBox1.ByteProvider == null)
                return null;
            if (hexBox1.ByteProvider.Length == 0)
                return null;
            QRByteArray = new Byte[0];
            var data = new Byte[0];
            for (int i = 0; i < hexBox1.ByteProvider.Length; i++)
            {
                data = new Byte[QRByteArray.Length + 1];
                QRByteArray.CopyTo(data, 0);
                data[i] = hexBox1.ByteProvider.ReadByte(i);
                QRByteArray = data;
            }

            data = QRByteArray;
            if (data == null)
                return null;
            var str = Encoding.GetEncoding(encoding).GetString(data);
            var hints = new Hashtable { { EncodeHintType.CHARACTER_SET, encoding } };
            return writer.encode(str, BarcodeFormat.QR_CODE, size, size, hints); 
        }

        private void menuQRCodeMake_Click(object sender, EventArgs e)
        {
        }

        private void menuQRCodeMakeCard_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Grid Events

        #endregion

        private void RadioColorCheckedChange(object sender, EventArgs e)
        {
        }
        
        private void TbtnToolClick(object sender, EventArgs e)
        {
        }

        private void btnEditPalette_Click(object sender, EventArgs e)
        {
        }

        private void btnDeleteSwitch_Click(object sender, EventArgs e)
        {
        }

        private void btnDeleteManhole_Click(object sender, EventArgs e)
        {
        }

        private void RadioSwitchCheckedChanged(object sender, EventArgs e)
        {
        }

        private void RadioManholeCheckedChanged(object sender, EventArgs e)
        {
        }

        private void ShiftButtonClick(object sender, EventArgs e)
        {
        }

        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
        }

        #region Check for updates
        private void bwCheckForUpdates_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private bool IsNewerAvailable(string newerVersion)
        {
            var thisVersion = Version.Parse(Application.ProductVersion);
            var remoteVersion = Version.Parse(newerVersion);
            return remoteVersion.CompareTo(thisVersion) > 0;
        }

        private void bwCheckForUpdates_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
        #endregion

        public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                // Writes a block of bytes to this stream using data from a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            // error occured, return false
            return false;
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void propertyGrid_Click(object sender, EventArgs e)
        {

        }

        private void hexBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var matrix = GetQRMatrix(100);
            if (matrix == null)
                return;
            var img = new Bitmap(200, 200);
            var g = Graphics.FromImage(img);
            g.Clear(Color.White);
            for (var y = 0; y < matrix.Height; ++y)
                for (var x = 0; x < matrix.Width; ++x)
                    if (matrix.get_Renamed(x, y) != -1)
                        g.FillRectangle(Brushes.Black, x * 2, y * 2, 2, 2);
            if (button1.Text == "Feature")
                picBox.Image = img;
            else
                picBox.Image = picBox2.Image;
            qr_code = img;
        }

        private void hexBox1_KeyUp(object sender, KeyEventArgs e)
        {
            hexBox1_KeyPress(sender,null);
        }

        private void saveQRCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = "PNG Files|*.png|Jpeg Files|*.jpg|Bitmap Files|*.bmp|GIF Files|*.gif" };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            var imgFormat = ImageFormat.Png;
            switch (Path.GetExtension(sfd.FileName))
            {
                case ".jpg":
                    imgFormat = ImageFormat.Jpeg;
                    break;
                case ".png":
                    imgFormat = ImageFormat.Png;
                    break;
                case ".bmp":
                    imgFormat = ImageFormat.Bmp;
                    break;
                case ".gif":
                    imgFormat = ImageFormat.Gif;
                    break;
            }
            try
            {
                qr_code.Save(sfd.FileName, imgFormat);
            }
            catch
            {
            }
        }

        private void openQRCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "All Supported|*.png;*.jpg;*.bmp;*.gif|PNG Files|*.png|Jpeg Files|*.jpg|Bitmap Files|*.bmp|GIF Files|*.gif" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                var bmp = new Bitmap(Image.FromFile(ofd.FileName));
                var binary = new BinaryBitmap(new HybridBinarizer(new RGBLuminanceSource(bmp, bmp.Width, bmp.Height)));
                var reader = new QRCodeReader();
                var result = reader.decode(binary);
                var byteArray = (byte[])((ArrayList)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS])[0];
                QRByteArray = decrypt_byte_array(byteArray);
                if (hexBox1.ByteProvider != null)
                {
                    IDisposable byteProvider = hexBox1.ByteProvider as IDisposable;
                    if (byteProvider != null)
                        byteProvider.Dispose();
                    hexBox1.ByteProvider = null;
                }
                hexBox1.ByteProvider = new Be.Windows.Forms.DynamicByteProvider(QRByteArray);
                hexBox1_KeyPress(sender, null);
            }
            catch (ReaderException ex)
            {
                MessageBox.Show("Error Loading:" + Environment.NewLine + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error Loading:" + Environment.NewLine + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte temp;
            int color_index = comboBox1.SelectedIndex;
            bool solid_color_enable;
            int antenna_index = comboBox2.SelectedIndex;

            if ((color_index <= 6))
            {
                if ((((antenna_index >= 7) && (antenna_index <= 12)) || ((antenna_index >= 19) && (antenna_index <= 24))))
                {
                    int[][] color_index_table = new int[6][];
                    color_index_table[0] = new int[7] { 6, 1, 0, 2, 3, 4, 5 };
                    color_index_table[1] = new int[7] { 6, 1, 2, 0, 3, 4, 5 };
                    color_index_table[2] = new int[7] { 6, 1, 2, 3, 0, 4, 5 };
                    color_index_table[3] = new int[7] { 6, 1, 2, 3, 4, 0, 5 };
                    color_index_table[4] = new int[7] { 6, 1, 2, 3, 4, 5, 0 };
                    color_index_table[5] = new int[7] { 6, 0, 1, 2, 3, 4, 5 };
                    if (antenna_index <= 12)
                        antenna_index -= 7;
                    else
                        antenna_index -= 19;
                    color_index = color_index_table[antenna_index][color_index];
                }
                solid_color_enable = (color_index != 0);
                //We now modify this index, based upon Antenna power selected.
                temp = hexBox1.ByteProvider.ReadByte(0x12);
                temp &= 0x07;
                temp |= (byte)((color_index - ((solid_color_enable)?1:0)) << 3);
                hexBox1.ByteProvider.WriteByte(0x12,temp);
                hexBox1.ByteProvider.WriteByte(0x13,0);
                if(solid_color_enable)
                    hexBox1.ByteProvider.WriteByte(0x24, 0x5C);
                else
                    hexBox1.ByteProvider.WriteByte(0x24, 0x00);
                hexBox1.ByteProvider.WriteByte(0x25, 0);
            }
            else
            {
                temp = hexBox1.ByteProvider.ReadByte(0x12);
                temp &= 0x07;
                temp |= (byte)((comboBox1.SelectedIndex - 7) << 3);
                hexBox1.ByteProvider.WriteByte(0x12, temp);
                hexBox1.ByteProvider.WriteByte(0x13, 0);
                hexBox1.ByteProvider.WriteByte(0x24, 0xDF);
                hexBox1.ByteProvider.WriteByte(0x25, 0);
            }
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(sender, e);  //Colors depend on Antenna power
            comboBox4_SelectedIndexChanged(sender, e);  //So does the Hair style.
            int index = comboBox2.SelectedIndex;
            int temp = hexBox1.ByteProvider.ReadByte(0x10) & 0xC0;
            if (index == 0)
            {
                hexBox1.ByteProvider.WriteByte(0x28, 0);
                hexBox1.ByteProvider.WriteByte(0x29, 0);
            }
            else if ((index >= 1) && (index <= 12))
            {
                temp |= ((index - 1) & 0x3F);
                hexBox1.ByteProvider.WriteByte(0x10, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x11, 0);
                hexBox1.ByteProvider.WriteByte(0x28, 0xD6);
                hexBox1.ByteProvider.WriteByte(0x29, 0);
            }
            else if ((index >= 13) && (index <= 24))
            {
                temp |= ((index - 13) & 0x3F);
                hexBox1.ByteProvider.WriteByte(0x10, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x11, 0);
                hexBox1.ByteProvider.WriteByte(0x28, 0xD7);
                hexBox1.ByteProvider.WriteByte(0x29, 0);
            }
            else
            {
                temp |= ((index - 25) & 0x3F);
                hexBox1.ByteProvider.WriteByte(0x10, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x11, 0);
                hexBox1.ByteProvider.WriteByte(0x28, 0xD8);
                hexBox1.ByteProvider.WriteByte(0x29, 0);
            }
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0)
            {
                byte[] unicode_str = System.Text.Encoding.Unicode.GetBytes(textBox1.Text);
                
                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    hexBox1.ByteProvider.WriteByte(0x34 + (i*4), unicode_str[(i*2)]);
                    hexBox1.ByteProvider.WriteByte(0x35 + (i * 4), 0);
                    hexBox1.ByteProvider.WriteByte(0x36 + (i * 4), unicode_str[(i*2)+1]);
                    hexBox1.ByteProvider.WriteByte(0x37 + (i * 4), 0);
                }
                hexBox1.ByteProvider.WriteByte(0x34 + (textBox1.TextLength * 4), 0);
                hexBox1.ByteProvider.WriteByte(0x35 + (textBox1.TextLength * 4), 0);
                hexBox1.ByteProvider.WriteByte(0x36 + (textBox1.TextLength * 4), 0);
                hexBox1.ByteProvider.WriteByte(0x37 + (textBox1.TextLength * 4), 0);
                hexBox1.Refresh();
                hexBox1_KeyPress(null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "QR Code")
            {
                picBox.Image = qr_code;
                button1.Text = "Feature";
            }
            else
            {
                picBox.Image = picBox2.Image;
                button1.Text = "QR Code";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] all = System.Reflection.Assembly.GetEntryAssembly().
            GetManifestResourceNames();

            foreach (string one in all)
            {
                MessageBox.Show(one);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox3.SelectedIndex;
            int temp;
            temp = hexBox1.ByteProvider.ReadByte(0x14);
            temp &= 0xE0;
            if ((index >= 0) && (index <= 10))
            {
                temp |= (index - 0);
                hexBox1.ByteProvider.WriteByte(0x14, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x15, 0);
                hexBox1.ByteProvider.WriteByte(0x26, 0);
                hexBox1.ByteProvider.WriteByte(0x27, 0);
            }
            else if ((index >= 11) && (index <= 17))
            {
                temp |= (index - 11);
                hexBox1.ByteProvider.WriteByte(0x14, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x15, 0);
                hexBox1.ByteProvider.WriteByte(0x26, 0x28);
                hexBox1.ByteProvider.WriteByte(0x27, 0);
            }
            else
            {
                temp |= (index - 18);
                hexBox1.ByteProvider.WriteByte(0x14, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x15, 0);
                hexBox1.ByteProvider.WriteByte(0x26, 0x30);
                hexBox1.ByteProvider.WriteByte(0x27, 0);
            }
            hexBox1.Refresh();
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    picBox2.Image = new Bitmap(Resources.head_0_0);
                    break;
                case 1:
                    picBox2.Image = new Bitmap(Resources.head_0_1);
                    break;
                case 2:
                    picBox2.Image = new Bitmap(Resources.head_0_2);
                    break;
                case 3:
                    picBox2.Image = new Bitmap(Resources.head_0_3);
                    break;
                case 4:
                    picBox2.Image = new Bitmap(Resources.head_0_4);
                    break;
                case 5:
                    picBox2.Image = new Bitmap(Resources.head_0_5);
                    break;
                case 6:
                    picBox2.Image = new Bitmap(Resources.head_0_6);
                    break;
                case 7:
                    picBox2.Image = new Bitmap(Resources.head_0_7);
                    break;
                case 8:
                    picBox2.Image = new Bitmap(Resources.head_0_8);
                    break;
                case 9:
                    picBox2.Image = new Bitmap(Resources.head_0_9);
                    break;
                case 10:
                    picBox2.Image = new Bitmap(Resources.head_0_A);
                    break;
                case 11:
                    picBox2.Image = new Bitmap(Resources.head_1_0);
                    break;
                case 12:
                    picBox2.Image = new Bitmap(Resources.head_1_1);
                    break;
                case 13:
                    picBox2.Image = new Bitmap(Resources.head_1_2);
                    break;
                case 14:
                    picBox2.Image = new Bitmap(Resources.head_1_3);
                    break;
                case 15:
                    picBox2.Image = new Bitmap(Resources.head_1_4);
                    break;
                case 16:
                    picBox2.Image = new Bitmap(Resources.head_1_5);
                    break;
                case 17:
                    picBox2.Image = new Bitmap(Resources.head_1_6);
                    break;
                case 18:
                    picBox2.Image = new Bitmap(Resources.head_2_0);
                    break;
                case 19:
                    picBox2.Image = new Bitmap(Resources.head_2_1);
                    break;
                case 20:
                    picBox2.Image = new Bitmap(Resources.head_2_2);
                    break;
                case 21:
                    picBox2.Image = new Bitmap(Resources.head_2_3);
                    break;
                case 22:
                    picBox2.Image = new Bitmap(Resources.head_2_4);
                    break;
                case 23:
                    picBox2.Image = new Bitmap(Resources.head_2_5);
                    break;
            }
            hexBox1_KeyPress(null, null);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                hexBox1.ByteProvider.WriteByte(0x00, 0x62);
                hexBox1.ByteProvider.WriteByte(0x01, 0x00);
                hexBox1.ByteProvider.WriteByte(0x02, 0x58);
                hexBox1.ByteProvider.WriteByte(0x03, 0x00);
                hexBox1.ByteProvider.WriteByte(0x04, 0x38);
                hexBox1.ByteProvider.WriteByte(0x05, 0x00);
                hexBox1.ByteProvider.WriteByte(0x06, 0x30);
                hexBox1.ByteProvider.WriteByte(0x07, 0x00);
                checkBox1.Text = "Japanese Denpa";
            }
            else
            {
                hexBox1.ByteProvider.WriteByte(0x00, 0x41);
                hexBox1.ByteProvider.WriteByte(0x01, 0x00);
                hexBox1.ByteProvider.WriteByte(0x02, 0x68);
                hexBox1.ByteProvider.WriteByte(0x03, 0x00);
                hexBox1.ByteProvider.WriteByte(0x04, 0x34);
                hexBox1.ByteProvider.WriteByte(0x05, 0x00);
                hexBox1.ByteProvider.WriteByte(0x06, 0x33);
                hexBox1.ByteProvider.WriteByte(0x07, 0x00);
                checkBox1.Text = "American Denpa";
            }
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void newDenpaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuFileNew_Click(sender, e);
        }

        private void openQRCodeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            menuFileOpen_Click(sender, e);
        }

        private void saveQRCodeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            menuFileSave_Click(sender, e);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuFileExit_Click(sender, e);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void newDenpaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            menuFileNew_Click(sender, e);
        }

        private void openQRCodeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            openQRCodeToolStripMenuItem_Click(sender, e);
        }

        private void saveQRCodeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            saveQRCodeToolStripMenuItem_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuFileExit_Click(sender, e);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            hexBox1.ByteProvider.WriteByte(0x0D, 0x00);
            hexBox1.ByteProvider.WriteByte(0x0F, 0x00);
            Random random = new Random();
            var rand_data = new Byte[1];
            random.NextBytes(rand_data);
            hexBox1.ByteProvider.WriteByte(0x0C, rand_data[0]);
            random.NextBytes(rand_data);
            hexBox1.ByteProvider.WriteByte(0x0E, rand_data[0]);
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox4.SelectedIndex;
            int antenna = comboBox2.SelectedIndex;
            int temp = hexBox1.ByteProvider.ReadByte(0x14) & 0x1F;
            int temp2 = hexBox1.ByteProvider.ReadByte(0x16) & 0xF8;
            if (index < 9)
            {
                temp |= ((index & 0x7) << 5);
                temp2 |= ((index & 0x18) >> 3);
                hexBox1.ByteProvider.WriteByte(0x14, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x15, 0);
                hexBox1.ByteProvider.WriteByte(0x16, (byte)temp2);
                hexBox1.ByteProvider.WriteByte(0x17, 0);
                hexBox1.ByteProvider.WriteByte(0x2A, 0x00);
                hexBox1.ByteProvider.WriteByte(0x2B, 0);
            }
            else
            {
                temp |= (((index - 9) & 0x7) << 5);
                temp2 |= (((index - 9) & 0x18) >> 3);
                hexBox1.ByteProvider.WriteByte(0x14, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x15, 0);
                hexBox1.ByteProvider.WriteByte(0x16, (byte)temp2);
                hexBox1.ByteProvider.WriteByte(0x17, 0);
                if (antenna == 0)
                    hexBox1.ByteProvider.WriteByte(0x2A, 0x0C);
                else
                    hexBox1.ByteProvider.WriteByte(0x2A, 0x0B);
                hexBox1.ByteProvider.WriteByte(0x2B, 0);
            }
            hexBox1.Refresh();
            switch (index)
            {
                case 0:
                    picBox2.Image = new Bitmap(Resources.face_shape_0);
                    break;
                case 1:
                    picBox2.Image = new Bitmap(Resources.face_shape_1);
                    break;
                case 2:
                    picBox2.Image = new Bitmap(Resources.face_shape_2);
                    break;
                case 3:
                    picBox2.Image = new Bitmap(Resources.face_shape_3);
                    break;
                case 4:
                    picBox2.Image = new Bitmap(Resources.face_shape_4);
                    break;
                case 5:
                    picBox2.Image = new Bitmap(Resources.face_shape_5);
                    break;
                case 6:
                    picBox2.Image = new Bitmap(Resources.face_shape_6);
                    break;
                case 7:
                    picBox2.Image = new Bitmap(Resources.face_shape_7);
                    break;
                case 8:
                    picBox2.Image = new Bitmap(Resources.face_shape_8);
                    break;
                case 9:
                    picBox2.Image = new Bitmap(Resources.hair_style_00);
                    break;
                case 10:
                    picBox2.Image = new Bitmap(Resources.hair_style_01);
                    break;
                case 11:
                    picBox2.Image = new Bitmap(Resources.hair_style_02);
                    break;
                case 12:
                    picBox2.Image = new Bitmap(Resources.hair_style_03);
                    break;
                case 13:
                    picBox2.Image = new Bitmap(Resources.hair_style_04);
                    break;
                case 14:
                    picBox2.Image = new Bitmap(Resources.hair_style_05);
                    break;
                case 15:
                    picBox2.Image = new Bitmap(Resources.hair_style_06);
                    break;
                case 16:
                    picBox2.Image = new Bitmap(Resources.hair_style_07);
                    break;
                case 17:
                    picBox2.Image = new Bitmap(Resources.hair_style_08);
                    break;
                case 18:
                    picBox2.Image = new Bitmap(Resources.hair_style_09);
                    break;
                case 19:
                    picBox2.Image = new Bitmap(Resources.hair_style_0A);
                    break;
                case 20:
                    picBox2.Image = new Bitmap(Resources.hair_style_0B);
                    break;
                case 21:
                    picBox2.Image = new Bitmap(Resources.hair_style_0C);
                    break;
                case 22:
                    picBox2.Image = new Bitmap(Resources.hair_style_0D);
                    break;
                case 23:
                    picBox2.Image = new Bitmap(Resources.hair_style_0E);
                    break;
                case 24:
                    picBox2.Image = new Bitmap(Resources.hair_style_0F);
                    break;
                case 25:
                    picBox2.Image = new Bitmap(Resources.hair_style_10);
                    break;
                case 26:
                    picBox2.Image = new Bitmap(Resources.hair_style_11);
                    break;
                case 27:
                    picBox2.Image = new Bitmap(Resources.hair_style_12);
                    break;
                case 28:
                    picBox2.Image = new Bitmap(Resources.hair_style_13);
                    break;
                case 29:
                    picBox2.Image = new Bitmap(Resources.hair_style_14);
                    break;
                case 30:
                    picBox2.Image = new Bitmap(Resources.hair_style_15);
                    break;
                case 31:
                    picBox2.Image = new Bitmap(Resources.hair_style_16);
                    break;     
            }
            hexBox1_KeyPress(null, null);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox5.SelectedIndex;
            int temp = hexBox1.ByteProvider.ReadByte(0x18) & 0xE0;
            temp |= (index & 0x1F);
            hexBox1.ByteProvider.WriteByte(0x18, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x19, 0);
            hexBox1.Refresh();
            switch (index)
            {
                case 0:
                    picBox2.Image = new Bitmap(Resources.hair_color_0);
                    break;
                case 1:
                    picBox2.Image = new Bitmap(Resources.hair_color_1);
                    break;
                case 2:
                    picBox2.Image = new Bitmap(Resources.hair_color_2);
                    break;
                case 3:
                    picBox2.Image = new Bitmap(Resources.hair_color_3);
                    break;
                case 4:
                    picBox2.Image = new Bitmap(Resources.hair_color_4);
                    break;
                case 5:
                    picBox2.Image = new Bitmap(Resources.hair_color_5);
                    break;
                case 6:
                    picBox2.Image = new Bitmap(Resources.hair_color_6);
                    break;
                case 7:
                    picBox2.Image = new Bitmap(Resources.hair_color_7);
                    break;
                case 8:
                    picBox2.Image = new Bitmap(Resources.hair_color_8);
                    break;
                case 9:
                    picBox2.Image = new Bitmap(Resources.hair_color_9);
                    break;
                case 10:
                    picBox2.Image = new Bitmap(Resources.hair_color_A);
                    break;
                case 11:
                    picBox2.Image = new Bitmap(Resources.hair_color_B);
                    break;    
            }
            hexBox1_KeyPress(null, null);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox6.SelectedIndex;
            int temp = hexBox1.ByteProvider.ReadByte(0x18) & 0x1F;
            temp |= ((index & 0x07) << 5);
            hexBox1.ByteProvider.WriteByte(0x18, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x19, 0);
            temp = hexBox1.ByteProvider.ReadByte(0x1A) & 0xFC;
            temp |= ((index & 0x18) >> 3);
            hexBox1.ByteProvider.WriteByte(0x1A, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x1B, 0);
            hexBox1.Refresh();
            switch (index)
            {
                case 0:
                    picBox2.Image = new Bitmap(Resources.eyes_00);
                    break;
                case 1:
                    picBox2.Image = new Bitmap(Resources.eyes_01);
                    break;
                case 2:
                    picBox2.Image = new Bitmap(Resources.eyes_02);
                    break;
                case 3:
                    picBox2.Image = new Bitmap(Resources.eyes_03);
                    break;
                case 4:
                    picBox2.Image = new Bitmap(Resources.eyes_04);
                    break;
                case 5:
                    picBox2.Image = new Bitmap(Resources.eyes_05);
                    break;
                case 6:
                    picBox2.Image = new Bitmap(Resources.eyes_06);
                    break;
                case 7:
                    picBox2.Image = new Bitmap(Resources.eyes_07);
                    break;
                case 8:
                    picBox2.Image = new Bitmap(Resources.eyes_08);
                    break;
                case 9:
                    picBox2.Image = new Bitmap(Resources.eyes_09);
                    break;
                case 10:
                    picBox2.Image = new Bitmap(Resources.eyes_0A);
                    break;
                case 11:
                    picBox2.Image = new Bitmap(Resources.eyes_0B);
                    break;
                case 12:
                    picBox2.Image = new Bitmap(Resources.eyes_0C);
                    break;
                case 13:
                    picBox2.Image = new Bitmap(Resources.eyes_0D);
                    break;
                case 14:
                    picBox2.Image = new Bitmap(Resources.eyes_0E);
                    break;
                case 15:
                    picBox2.Image = new Bitmap(Resources.eyes_0F);
                    break;
                case 16:
                    picBox2.Image = new Bitmap(Resources.eyes_10);
                    break;
                case 17:
                    picBox2.Image = new Bitmap(Resources.eyes_11);
                    break;
                case 18:
                    picBox2.Image = new Bitmap(Resources.eyes_12);
                    break;
                case 19:
                    picBox2.Image = new Bitmap(Resources.eyes_13);
                    break;
                case 20:
                    picBox2.Image = new Bitmap(Resources.eyes_14);
                    break;
                case 21:
                    picBox2.Image = new Bitmap(Resources.eyes_15);
                    break;
                case 22:
                    picBox2.Image = new Bitmap(Resources.eyes_16);
                    break;
                case 23:
                    picBox2.Image = new Bitmap(Resources.eyes_17);
                    break;
                case 24:
                    picBox2.Image = new Bitmap(Resources.eyes_18);
                    break;
                case 25:
                    picBox2.Image = new Bitmap(Resources.eyes_19);
                    break;
                case 26:
                    picBox2.Image = new Bitmap(Resources.eyes_1A);
                    break;
                case 27:
                    picBox2.Image = new Bitmap(Resources.eyes_1B);
                    break;
                case 28:
                    picBox2.Image = new Bitmap(Resources.eyes_1C);
                    break;
                case 29:
                    picBox2.Image = new Bitmap(Resources.eyes_1D);
                    break;
                case 30:
                    picBox2.Image = new Bitmap(Resources.eyes_1E);
                    break;
                case 31:
                    picBox2.Image = new Bitmap(Resources.eyes_1F);
                    break;
            }
            hexBox1_KeyPress(null, null);
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox7.SelectedIndex;
            int temp = hexBox1.ByteProvider.ReadByte(0x1C) & 0x3F;
            temp |= ((index & 0x03) << 6);
            hexBox1.ByteProvider.WriteByte(0x1C, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x1D, 0);
            temp = hexBox1.ByteProvider.ReadByte(0x1E) & 0xFE;
            temp |= ((index & 0x04) >> 2);
            hexBox1.ByteProvider.WriteByte(0x1E, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x1F, 0);
            hexBox1.Refresh();
            switch (index)
            {
                case 0:
                    picBox2.Image = new Bitmap(Resources.eye_brow_0);
                    break;
                case 1:
                    picBox2.Image = new Bitmap(Resources.eye_brow_1);
                    break;
                case 2:
                    picBox2.Image = new Bitmap(Resources.eye_brow_2);
                    break;
                case 3:
                    picBox2.Image = new Bitmap(Resources.eye_brow_3);
                    break;
                case 4:
                    picBox2.Image = new Bitmap(Resources.eye_brow_4);
                    break;
                case 5:
                    picBox2.Image = new Bitmap(Resources.eye_brow_5);
                    break;
                case 6:
                    picBox2.Image = new Bitmap(Resources.eye_brow_6);
                    break;
                case 7:
                    picBox2.Image = new Bitmap(Resources.eye_brow_7);
                    break;
            }
            hexBox1_KeyPress(null, null);
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox8.SelectedIndex;
            int temp = hexBox1.ByteProvider.ReadByte(0x1C) & 0x87;
            temp |= ((index & 0x0F) << 3);
            hexBox1.ByteProvider.WriteByte(0x1A, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x1B, 0);
            hexBox1.Refresh();
            switch (index)
            {
                case 0:
                    picBox2.Image = new Bitmap(Resources.nose_0);
                    break;
                case 1:
                    picBox2.Image = new Bitmap(Resources.nose_1);
                    break;
                case 2:
                    picBox2.Image = new Bitmap(Resources.nose_2);
                    break;
                case 3:
                    picBox2.Image = new Bitmap(Resources.nose_3);
                    break;
                case 4:
                    picBox2.Image = new Bitmap(Resources.nose_4);
                    break;
                case 5:
                    picBox2.Image = new Bitmap(Resources.nose_5);
                    break;
                case 6:
                    picBox2.Image = new Bitmap(Resources.nose_6);
                    break;
                case 7:
                    picBox2.Image = new Bitmap(Resources.nose_7);
                    break;
                case 8:
                    picBox2.Image = new Bitmap(Resources.nose_8);
                    break;
                case 9:
                    picBox2.Image = new Bitmap(Resources.nose_9);
                    break;
                case 10:
                    picBox2.Image = new Bitmap(Resources.nose_A);
                    break;
                case 11:
                    picBox2.Image = new Bitmap(Resources.nose_B);
                    break;
                case 12:
                    picBox2.Image = new Bitmap(Resources.nose_C);
                    break;
                case 13:
                    picBox2.Image = new Bitmap(Resources.nose_D);
                    break;
                case 14:
                    picBox2.Image = new Bitmap(Resources.nose_E);
                    break;
                case 15:
                    picBox2.Image = new Bitmap(Resources.nose_F);
                    break;  
            }
            hexBox1_KeyPress(null, null);
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox9.SelectedIndex;
            int temp = hexBox1.ByteProvider.ReadByte(0x16) & 0xE7;
            if (index < 2)
            {
                hexBox1.ByteProvider.WriteByte(0x30, 0x0C);
                hexBox1.ByteProvider.WriteByte(0x31, 0);
                temp |= ((index & 0x03) << 3);
                hexBox1.ByteProvider.WriteByte(0x16, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x17, 0);
            }
            else
            {
                hexBox1.ByteProvider.WriteByte(0x30, 0x00);
                hexBox1.ByteProvider.WriteByte(0x31, 0);
                temp |= (((index - 2) & 0x03) << 3);
                hexBox1.ByteProvider.WriteByte(0x16, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x17, 0);
            }
            hexBox1.Refresh();
            switch (index)
            {
                case 0:
                    picBox2.Image = new Bitmap(Resources.face_color_4);
                    break;
                case 1:
                    picBox2.Image = new Bitmap(Resources.face_color_5);
                    break;
                case 2:
                    picBox2.Image = new Bitmap(Resources.face_color_0);
                    break;
                case 3:
                    picBox2.Image = new Bitmap(Resources.face_color_1);
                    break;
                case 4:
                    picBox2.Image = new Bitmap(Resources.face_color_2);
                    break;
                case 5:
                    picBox2.Image = new Bitmap(Resources.face_color_3);
                    break;
            }
            hexBox1_KeyPress(null, null);
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox10.SelectedIndex;
            int temp = hexBox1.ByteProvider.ReadByte(0x1C) & 0xC0;
            temp |= (index & 0x1F);
            hexBox1.ByteProvider.WriteByte(0x1C, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x1D, 0);
            hexBox1.Refresh();
            switch (index)
            {
                case 0:
                    picBox2.Image = new Bitmap(Resources.mouth_00);
                    break;
                case 1:
                    picBox2.Image = new Bitmap(Resources.mouth_01);
                    break;
                case 2:
                    picBox2.Image = new Bitmap(Resources.mouth_02);
                    break;
                case 3:
                    picBox2.Image = new Bitmap(Resources.mouth_03);
                    break;
                case 4:
                    picBox2.Image = new Bitmap(Resources.mouth_04);
                    break;
                case 5:
                    picBox2.Image = new Bitmap(Resources.mouth_05);
                    break;
                case 6:
                    picBox2.Image = new Bitmap(Resources.mouth_06);
                    break;
                case 7:
                    picBox2.Image = new Bitmap(Resources.mouth_07);
                    break;
                case 8:
                    picBox2.Image = new Bitmap(Resources.mouth_08);
                    break;
                case 9:
                    picBox2.Image = new Bitmap(Resources.mouth_09);
                    break;
                case 10:
                    picBox2.Image = new Bitmap(Resources.mouth_0A);
                    break;
                case 11:
                    picBox2.Image = new Bitmap(Resources.mouth_0B);
                    break;
                case 12:
                    picBox2.Image = new Bitmap(Resources.mouth_0C);
                    break;
                case 13:
                    picBox2.Image = new Bitmap(Resources.mouth_0D);
                    break;
                case 14:
                    picBox2.Image = new Bitmap(Resources.mouth_0E);
                    break;
                case 15:
                    picBox2.Image = new Bitmap(Resources.mouth_0F);
                    break;
                case 16:
                    picBox2.Image = new Bitmap(Resources.mouth_10);
                    break;
                case 17:
                    picBox2.Image = new Bitmap(Resources.mouth_11);
                    break;
                case 18:
                    picBox2.Image = new Bitmap(Resources.mouth_12);
                    break;
                case 19:
                    picBox2.Image = new Bitmap(Resources.mouth_13);
                    break;
                case 20:
                    picBox2.Image = new Bitmap(Resources.mouth_14);
                    break;
                case 21:
                    picBox2.Image = new Bitmap(Resources.mouth_15);
                    break;
                case 22:
                    picBox2.Image = new Bitmap(Resources.mouth_16);
                    break;
                case 23:
                    picBox2.Image = new Bitmap(Resources.mouth_17);
                    break;
                case 24:
                    picBox2.Image = new Bitmap(Resources.mouth_18);
                    break;
                case 25:
                    picBox2.Image = new Bitmap(Resources.mouth_19);
                    break;
                case 26:
                    picBox2.Image = new Bitmap(Resources.mouth_1A);
                    break;
                case 27:
                    picBox2.Image = new Bitmap(Resources.mouth_1B);
                    break;
                case 28:
                    picBox2.Image = new Bitmap(Resources.mouth_1C);
                    break;
                case 29:
                    picBox2.Image = new Bitmap(Resources.mouth_1D);
                    break;
                case 30:
                    picBox2.Image = new Bitmap(Resources.mouth_1E);
                    break;
                case 31:
                    picBox2.Image = new Bitmap(Resources.mouth_1F);
                    break;
            }
            hexBox1_KeyPress(null, null);
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox11.SelectedIndex;
            int temp = hexBox1.ByteProvider.ReadByte(0x1E) & 0x07;
            if (index < 1)
            {
                hexBox1.ByteProvider.WriteByte(0x2C, 0);
                hexBox1.ByteProvider.WriteByte(0x2D, 0);
                temp |= ((index & 0x07) << 3);
                hexBox1.ByteProvider.WriteByte(0x1E, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x1F, 0);
            }
            else
            {
                hexBox1.ByteProvider.WriteByte(0x2C, 0x5A);
                hexBox1.ByteProvider.WriteByte(0x2D, 0);
                temp |= (((index - 1) & 0x07) << 3);
                hexBox1.ByteProvider.WriteByte(0x1E, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x1F, 0);
            }
            hexBox1.Refresh();
            switch (index)
            {
                case 0:
                    picBox2.Image = new Bitmap(Resources.cheek_0);
                    break;
                case 1:
                    picBox2.Image = new Bitmap(Resources.cheek_1);
                    break;
                case 2:
                    picBox2.Image = new Bitmap(Resources.cheek_2);
                    break;
                case 3:
                    picBox2.Image = new Bitmap(Resources.cheek_3);
                    break;
                case 4:
                    picBox2.Image = new Bitmap(Resources.cheek_4);
                    break;
                case 5:
                    picBox2.Image = new Bitmap(Resources.cheek_5);
                    break;
                case 6:
                    picBox2.Image = new Bitmap(Resources.cheek_6);
                    break;
                case 7:
                    picBox2.Image = new Bitmap(Resources.cheek_7);
                    break;
            }
            hexBox1_KeyPress(null, null);
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox12.SelectedIndex;
            int temp = hexBox1.ByteProvider.ReadByte(0x20) & 0xE0;
            if (index < 1)
            {
                hexBox1.ByteProvider.WriteByte(0x2E, 0);
                hexBox1.ByteProvider.WriteByte(0x2F, 0);
                temp |= ((index & 0x1F));
                hexBox1.ByteProvider.WriteByte(0x20, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x21, 0);
            }
            else
            {
                hexBox1.ByteProvider.WriteByte(0x2E, 0x30);
                hexBox1.ByteProvider.WriteByte(0x2F, 0);
                temp |= (((index - 1) & 0x1F));
                hexBox1.ByteProvider.WriteByte(0x20, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x21, 0);
            }
            hexBox1.Refresh();
            switch (index)
            {
                case 0:
                    picBox2.Image = new Bitmap(Resources.glasses_0);
                    break;
                case 1:
                    picBox2.Image = new Bitmap(Resources.glasses_1);
                    break;
                case 2:
                    picBox2.Image = new Bitmap(Resources.glasses_2);
                    break;
                case 3:
                    picBox2.Image = new Bitmap(Resources.glasses_3);
                    break;
                case 4:
                    picBox2.Image = new Bitmap(Resources.glasses_4);
                    break;
                case 5:
                    picBox2.Image = new Bitmap(Resources.glasses_5);
                    break;
                case 6:
                    picBox2.Image = new Bitmap(Resources.glasses_6);
                    break;
                case 7:
                    picBox2.Image = new Bitmap(Resources.glasses_7);
                    break;
                case 8:
                    picBox2.Image = new Bitmap(Resources.glasses_8);
                    break;
                case 9:
                    picBox2.Image = new Bitmap(Resources.glasses_9);
                    break;
                case 10:
                    picBox2.Image = new Bitmap(Resources.glasses_A);
                    break;
                case 11:
                    picBox2.Image = new Bitmap(Resources.glasses_B);
                    break;
                case 12:
                    picBox2.Image = new Bitmap(Resources.glasses_C);
                    break;
                case 13:
                    picBox2.Image = new Bitmap(Resources.glasses_D);
                    break;
                case 14:
                    picBox2.Image = new Bitmap(Resources.glasses_E);
                    break;
                case 15:
                    picBox2.Image = new Bitmap(Resources.glasses_F);
                    break;
            }
            hexBox1_KeyPress(null, null);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int index = (int)numericUpDown1.Value;
            int temp = hexBox1.ByteProvider.ReadByte(0x10) & 0x3F;
            temp |= ((index & 0x03) << 6);
            hexBox1.ByteProvider.WriteByte(0x10, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x11, 0);
            temp = hexBox1.ByteProvider.ReadByte(0x12) & 0xF8;
            temp |= ((index & 0x1C) >> 2);
            hexBox1.ByteProvider.WriteByte(0x12, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x13, 0);
            hexBox1.ByteProvider.WriteByte(0x22, (byte)((index & 0x1E0) >> 5));
            hexBox1.ByteProvider.WriteByte(0x23, 0);
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            String[] names = new String[] {
                "Abe","Abraham","Agustin","Ahmed","Al","Ali","Amado",
                "Andre","Angel","Antonia","Arlie","Benedict","Benjamin","Blaine","Bob",
                "Bobbie","Branden","Brant","Bruce","Bryant","Bryon","Buck","Bud",
                "Carmine","Chance","Chas","Chong","Chris","Christopher","Clair","Clarence",
                "Claude","Clayton","Colby","Columbus","Corey","Cornelius","Daren","Darin",
                "Darnell","Dave","Dean","Demetrius","Derek","Deshawn","Donnell","Drew",
                "Dylan","Edgar","Edmundo","Edward","Eldridge","Eli","Emery","Emilio",
                "Emory","Erasmo","Erik","Erin","Ernesto","Ethan","Federico","Ferdinand",
                "Fernando","Fidel","Florentino","Forest","Francisco","Fred","Garfield","Garrett",
                "Geraldo","Gregg","Hans","Harold","Haywood","Hector","Hilton","Homer",
                "Houston","Hubert","Hyman","Irvin","Ivory","Jaime","Jayson","Jere",
                "Jerrell","Julian","Julius","Kelvin","Kenton","Kim","Kraig","Leandro",
                "Leo","Leopoldo","Louie","Lucas","Luciano","Manual","Marcel","Marcellus",
                "Mariano","Mario","Marion","Marlin","Marlon","Maurice","Mauro","Merlin",
                "Micah","Miguel","Mikel","Mohamed","Morgan","Olen","Orlando","Orval",
                "Otha","Quentin","Quinn","Refugio","Rich","Richie","Rickey","Rod",
                "Rodolfo","Rodrick","Sammy","Scotty","Seth","Shad","Shawn","Shon",
                "Taylor","Thurman","Wade","Walter","Walton","Wiley","Wilson","Zachary"
            };
            button3_Click_1(sender, e);
            Random random = new Random();
            comboBox1.SelectedIndex = random.Next(comboBox1.Items.Count);
            comboBox2.SelectedIndex = random.Next(comboBox2.Items.Count);
            comboBox3.SelectedIndex = random.Next(comboBox3.Items.Count);
            comboBox4.SelectedIndex = random.Next(comboBox4.Items.Count);
            comboBox5.SelectedIndex = random.Next(comboBox5.Items.Count);
            comboBox6.SelectedIndex = random.Next(comboBox6.Items.Count);
            comboBox7.SelectedIndex = random.Next(comboBox7.Items.Count);
            comboBox8.SelectedIndex = random.Next(comboBox8.Items.Count);
            comboBox9.SelectedIndex = random.Next(comboBox9.Items.Count);
            comboBox10.SelectedIndex = random.Next(comboBox10.Items.Count);
            comboBox11.SelectedIndex = random.Next(comboBox11.Items.Count);
            comboBox12.SelectedIndex = random.Next(comboBox12.Items.Count);
            numericUpDown1.Value = random.Next(512);
            textBox1.Text = names[random.Next(names.Length)];
        }

        private void advancedInterfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hexBox1.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            button1.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            advancedInterfaceToolStripMenuItem.Checked = !advancedInterfaceToolStripMenuItem.Checked;

            if (!advancedInterfaceToolStripMenuItem.Checked)
                if (button1.Text == "QR Code")
                    button1_Click(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }
    }
}

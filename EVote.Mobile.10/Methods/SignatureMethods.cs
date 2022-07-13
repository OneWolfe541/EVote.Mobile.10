using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using EVote.Extensions;
using EVote.LocalDatabase;
using EVote.Utilities.Models;
using EVote.Logging;

namespace EVote.Methods
{
    public static class SignatureMethods
    {
        public static BitmapImage LoadSignatureFromFile(VoterDataModel voter, string folder)
        {
            var bitmap = new BitmapImage();

            // Get file name from Voter ID
            string path = folder + "\\" + voter.VoterID.ToString() + "." + AppSettings.System.SignatureType;            

            if (Windows10.IsWindows10())
            {
                bitmap = new BitmapImage(new Uri(path));
            }
            else
            {
                try
                {
                    // File stream loading avoids some of the caching issues inheirent with more direct methods
                    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        // The BeginInit and CacheOption prevents the local file from being locked
                        // which in turn allows the file to be overwritten or deleted while still loaded on the page
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();
                    }
                }
                catch (Exception e)
                {
                    bitmap = null;
                    throw (e);
                }
            }

            return bitmap;
        }

        public static BitmapImage LoadSignatureFromDatabase(VoterDataModel voter)
        {
            string server = System.Environment.MachineName;
            string ConnectionString = null;
            if (server == "WIN7")
            {
                ConnectionString = "XXXX";
            }
            else if (server == "DT10")
            {
                ConnectionString = "XXXX";
            }
            else
            {
                ConnectionString = "XXXX";
            }

            using (var context = new ElectionContext(ConnectionString))
            {
                var record = context.Signatures.Where(sig => sig.VoterId == voter.VoterID).OrderByDescending(ord => ord.LastModified).FirstOrDefault();

                if(record != null)
                {
                    var imageByte = record.SignatureImage;

                    if (imageByte != null)
                    {
                        BitmapImage picture = new BitmapImage();
                        using (var stream = new MemoryStream(imageByte))
                        {
                            picture.BeginInit();
                            picture.CacheOption = BitmapCacheOption.OnLoad;
                            picture.StreamSource = stream;
                            picture.EndInit();
                        }
                        picture.Freeze();

                        return picture;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public static BitmapImage LoadSignatureFromTrainingDatabase(VoterDataModel voter)
        {
            string server = System.Environment.MachineName;
            string ConnectionString = null;
            if (server == "WIN7")
            {
                ConnectionString = "XXXX";
            }
            else if (server == "DT10")
            {
                ConnectionString = "XXXX";
            }
            else
            {
                ConnectionString = "XXXX";
            }

            using (var context = new ElectionContext(ConnectionString))
            {
                var record = context.TrainingSignatures.Where(sig => sig.VoterId == voter.VoterID).OrderByDescending(ord => ord.LastModified).FirstOrDefault();

                if (record != null)
                {
                    var imageByte = record.SignatureImage;

                    if (imageByte != null)
                    {
                        BitmapImage picture = new BitmapImage();
                        using (var stream = new MemoryStream(imageByte))
                        {
                            picture.BeginInit();
                            picture.CacheOption = BitmapCacheOption.OnLoad;
                            picture.StreamSource = stream;
                            picture.EndInit();
                        }
                        picture.Freeze();

                        return picture;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public static Byte[] LoadImageDataFromFile(VoterDataModel voter, string folder)
        {
            string path = folder + "\\" + voter.VoterID.ToString() + "." + AppSettings.System.SignatureType;
            return System.IO.File.ReadAllBytes(path);
        }

        public static async Task<LocalDatabase.Signature> SaveToDatabaseAsync(Byte[] image, VoterDataModel voter)
        {
            string server = System.Environment.MachineName;
            string ConnectionString = null;
            if (server == "WIN7")
            {
                ConnectionString = "XXXX";
            }
            else if (server == "DT10")
            {
                ConnectionString = "XXXX";
            }
            else
            {
                ConnectionString = "XXXX";
            }

            using (var context = new ElectionContext(ConnectionString))
            {
                //var voters = await context.Voters.FindAsync(voter.VoterID);
                EVote.LocalDatabase.Signature signature = new EVote.LocalDatabase.Signature()
                {
                    SignatureId = Guid.NewGuid(),
                    VoterId = voter.VoterID,
                    SignatureImage = image,
                    LastModified = DateTime.Now
                };

                try
                {
                    context.Signatures.Add(signature);
                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    EVoteLogger _signatureLogger = new EVoteLogger("EVoteLogs", true);
                    _signatureLogger.WriteLog("Signature Save Error: " + e.Message);
                }

                return signature;
            }
        }

        public static async Task<LocalDatabase.TrainingSignature> SaveToTrainingDatabaseAsync(Byte[] image, VoterDataModel voter)
        {
            string server = System.Environment.MachineName;
            string ConnectionString = null;
            if (server == "WIN7")
            {
                ConnectionString = "XXXX";
            }
            else if (server == "DT10")
            {
                ConnectionString = "XXXX";
            }
            else
            {
                ConnectionString = "XXXX";
            }

            using (var context = new ElectionContext(ConnectionString))
            {
                //var voters = await context.Voters.FindAsync(voter.VoterID);
                TrainingSignature signature = new TrainingSignature()
                {
                    SignatureId = Guid.NewGuid(),
                    VoterId = voter.VoterID,
                    SignatureImage = image,
                    LastModified = DateTime.Now
                };

                try
                {
                    context.TrainingSignatures.Add(signature);
                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    EVoteLogger _signatureLogger = new EVoteLogger("EVoteLogs", true);
                    _signatureLogger.WriteLog("Signature Save Error: " + e.Message);
                }

                return signature;
            }
        }
    }
}

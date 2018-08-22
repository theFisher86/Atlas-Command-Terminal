using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libMBIN.Models;
using libMBIN.Models.Structs;
using Atlas_Command_Terminal.Models;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using System.Reflection;
using Windows.Storage.Pickers;
using System.IO;
using Windows.Storage;

namespace Atlas_Command_Terminal.Models
{
    class LoadMBIN
    {
        public async static Task<List<MBINField>> LoadFileAsync()
        {
            Stream stream = await MbinLoader();
            return GetMBINFields(stream);
        }


        public static async Task<Stream> MbinLoader()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".mbin");

            StorageFile file = await openPicker.PickSingleFileAsync();
            //Stream stream = await file.OpenStreamForReadAsync();
            return await file.OpenStreamForWriteAsync();
        }

        public static List<MBINField> GetMBINFields(Stream file)
        {
            NMSTemplate template = null;
            using (libMBIN.MBINFile mbin = new libMBIN.MBINFile(file))
            {
                mbin.Load(); // load the header information from the file
                template = mbin.GetData(); // populate the data struct.
            }

            if (template != null) {
                return IterateFields(template, template.GetType());
            }
            else
            {
                Helpers.BasicDialogBox("MBIN Loading Error...", "Unable to load the MBIN!");
                return null;
            }
        }

        public static List<MBINField> IterateFields(NMSTemplate data, Type type )
        {
            List<MBINField> mbinContents = new List<MBINField>();

            IOrderedEnumerable<FieldInfo> fields = type.GetFields().OrderBy(field => field.MetadataToken);
            if (fields != null)
            {
                foreach (FieldInfo fieldInfo in fields)
                {
                    Debug.WriteLine($"type = {fieldInfo.FieldType}, name = {fieldInfo.Name}, value = {fieldInfo.GetValue(data)}");      //write all fields to debug
                                                                                                                                        //Check for NMSAttribute ignore -code by @GaticusHax
                    var attributes = (NMSAttribute[])fieldInfo.GetCustomAttributes(typeof(NMSAttribute), false);                        //
                    libMBIN.Models.NMSAttribute attrib = null;                                                                          //
                    if (attributes.Length > 0) attrib = attributes[0];                                                                  //
                    bool ignore = false;                                                                                                //
                    if (attrib != null) ignore = attrib.Ignore;                                                                         //

                    if (!ignore)                                                                                                        // Add the field to the mbinContents list
                    {                                                                                                                   //
                        mbinContents.Add(new MBINField                                                                                  //
                        {                                                                                                               //
                            Name = fieldInfo.Name,                                                                                      //
                            Value = fieldInfo.GetValue(data).ToString(),                                                                //
                            NMSType = fieldInfo.FieldType.ToString()                                                                    //
                        });                                                                                                             //
                    }                                                                                                                   //
                }
            }
            else
            {
                Helpers.BasicDialogBox("Error Getting Fields...", "Couldn't get the fields for some reason.\n Data: " + data.ToString() + "\n Will return blank List");
                mbinContents = null;
            }
            return mbinContents;
        }
    }
}

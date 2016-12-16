using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace IO
{
    public static class Directories
    {
        public static Boolean Create(String path)
        {
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public static Boolean Exist(String path)
        {
            try
            {
                return Directory.Exists(path);
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public static Boolean Remove(String path)
        {
            try
            {
                Directory.Delete(path);
                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
        }
    }
}

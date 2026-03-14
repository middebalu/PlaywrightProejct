using System;
using System.Collections.Generic;
using System.Text;

namespace PlayWrightDemo
{
    internal class DownloadTextAriaException:Exception
    {

        public DownloadTextAriaException()
        {
            
        }
        public DownloadTextAriaException(String message):base(message)
        {
           
        }
    }
}

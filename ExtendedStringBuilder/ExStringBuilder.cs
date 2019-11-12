﻿using System;
using System.IO;
using System.Text;

namespace ExtendedStringBuilder
{
    public class ExStringBuilder
    {
        private StringBuilder output = new StringBuilder();

        private string filename;
        private FileStream fileStream;

        public string Output
        {
            get
            {
                return output.ToString();
            }
        }

        /// <summary>
        /// Use this when you want to enable Writing to the file once you append data
        /// </summary>
        /// <param name="filename"></param>
        public ExStringBuilder(string filename)
        {
            this.filename = filename;

            if (string.IsNullOrWhiteSpace(filename))
            {
                this.fileStream = new FileStream(this.filename, FileMode.Create, FileAccess.Write);
            }
            else
            {
                throw new Exception("Please provide a valid filename");
            }
        }

        public void AppendLine(string str)
        {
            string data;

            lock (output)
            {
                data = str;
                // output.AppendLine(data);
            }
            SaveData(data + Environment.NewLine);
        }

        public void Append(string str)
        {
            lock (output)
            {
                output.Append(str);
                SaveData(str);
            }
        }

        private void SaveData(string data)
        {
            lock (fileStream)
            {
                byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(data);
                this.fileStream.Write(dataBytes, 0, dataBytes.Length);
            }
        }

        /// <summary>
        /// If you call this method will release the file lock and empty the output, no need to use dispose after
        /// </summary>
        public void Close()
        {
            fileStream.Close();
            fileStream.Dispose();
            output.Clear();
        }

        public void Dispose()
        {
            fileStream.Close();
            fileStream.Dispose();
            output.Clear();
        }
    }
}
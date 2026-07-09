using System;
using System.IO;
using CsvHelper;
using UnityEngine;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 
/// </summary>
public class CSVFileLoad
{
    /// <summary>
    /// CSV Load
    /// </summary>
    /// <typeparam name="T">get data model</typeparam>
    /// <typeparam name="fileName">fileName</typeparam>
    /// <returns></returns>
    public static List<T> CSVLoad<T>(string fileName)
    {
        using (var reader = new StreamReader(fileName + ".csv"))
        {
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                var records = csv.GetRecords<T>().ToList();
                return records;
            }
        }

    }
}
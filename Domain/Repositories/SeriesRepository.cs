using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Domain.Repositories
{
    internal sealed class SeriesRepository
    {
        private HashSet<Series> series;
        internal static readonly SeriesRepository instance = new SeriesRepository();
        private IFormatter formatter;
        private readonly string filePath;

        private SeriesRepository()
        {
            this.series = new HashSet<Series>();
            this.formatter = new BinaryFormatter();
            this.filePath = @"series.bin";
            this.LoadData();
        }

        internal void SaveData()
        {
            try
            {
                using (
                    var streamWriter = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    this.formatter.Serialize(streamWriter, this.series);
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"File missing at {this.filePath}." +
                                                "Failed to save data to file!");
            }
            catch (DirectoryNotFoundException ex)
            {
                throw ex;
            }
            catch (SerializationException ex)
            {
                
            }
            catch (IOException)
            {
                bool checkFile = true;
                while (checkFile)
                {
                    if (IsFileReady(this.filePath))
                    {
                        using (
                            var stream = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            this.formatter.Serialize(stream, this.series);
                        }
                        checkFile = false;
                    }
                }
            }
        }

        private void LoadData()
        {
            var series = new HashSet<Series>();

            try
            {
                using (
                    var streamReader = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                        FileShare.Read))
                {
                    series = (HashSet<Series>)this.formatter.Deserialize(streamReader);
                    this.series = series;
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"File missing at {this.filePath}." +
                                                "Load failed!");
            }
            catch (DirectoryNotFoundException ex)
            {
                throw ex;
            }
            catch (SerializationException ex)
            {
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        internal IEnumerable<Series> GetAll()
        {
            return this.series;
        }

        internal void AddSeries(Series newSeries)
        {
            Series repositorySeries;
            if (this.TryGetSeries(newSeries, out repositorySeries))
            {
                this.series.Remove(repositorySeries);
                this.series.Add(newSeries);
            }
            else
            {
                this.series.Add(newSeries);
            }
            this.SaveData();
        }

        private bool TryGetSeries(Series series, out Series repositorySeries)
        {
            repositorySeries = this.FindById(series.Id);
            return repositorySeries != null;
        }

        private Series FindById(Guid seriesId)
        {
            return this.series.FirstOrDefault(x => x.Id == seriesId);
        }

        internal void DeleteSeries(Guid seriesId)
        {
            this.series.RemoveWhere(s => s.Id == seriesId);
        }

        private static bool IsFileReady(string sFilename)
        {
            try
            {
                using (FileStream inputStream = File.Open(sFilename, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    if (inputStream.Length > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
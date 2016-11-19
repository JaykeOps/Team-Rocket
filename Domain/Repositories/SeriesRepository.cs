using Domain.Entities;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using Domain.Services;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

namespace Domain.Repositories
{
    public sealed class SeriesRepository
    {
        private HashSet<Series> series;
        public static readonly SeriesRepository instance = new SeriesRepository();
        private IFormatter formatter;
        private string filePath;
       

        private SeriesRepository()
        {

            this.series= new HashSet<Series>();
            this.formatter = new BinaryFormatter();
            this.filePath = @"..//..//series.bin";
            this.LoadData();
            
           

        }

        


        public void SaveData()
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
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public void LoadData()
        {
            var series = new HashSet<Series>();

            try
            {
                using (
                    var streamReader = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.Read,
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
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Series> GetAll()
        {
            return this.series;
        }

        public void AddSeries(Series newSeries)
        {
            this.series.Add(newSeries);
            
        }
    }
}
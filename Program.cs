using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Azure.Storage.Blobs;

    namespace PhotoSharingApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("StorageAccount");
                string containerName = "photos";
                BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
                container.CreateIfNotExists();
                Console.WriteLine("Code completed successfully");


                //upload an image
                string blobName = "Hritpindo";
                string fileName = "Hritpindo.png";
                BlobClient blobClient = container.GetBlobClient(blobName);
                blobClient.Upload(fileName, true); //overwrite existing one



                //validate everything is working fine
                var blobs = container.GetBlobs();
                foreach (var blob in blobs)
                {
                    Console.WriteLine($"{blob.Name} --> Created On: {blob.Properties.CreatedOn:yyyy-MM-dd HH:mm:ss}  Size: {blob.Properties.ContentLength}");
                }



            }
        }
    }








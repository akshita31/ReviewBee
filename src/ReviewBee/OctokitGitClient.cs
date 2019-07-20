using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using System.Web.Mvc;

namespace ReviewBee
{
    public class OctokitGitClient : IGitClient
    {
        private GitHubClient _client;

        public OctokitGitClient()
        {
            _client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
        }

        public async Task<IEnumerable<PullRequestFile>> GetPullRequestFiles(string owner, string name, int pullRequestNumber)
        {
           
            var prFiles = await _client.PullRequest.Files(owner, name,pullRequestNumber);
           return prFiles.Select(file => new PullRequestFile(file.FileName, file.Patch));
        }

        public async Task DownloadGithubRepo(string owner, string name, string path,string commitId)
        {
            var id = (await _client.Repository.Get(owner, name)).Id;
            IReadOnlyList<RepositoryContent> repoContent = await _client.Repository.Content.GetAllContentsByRef(id, path, commitId);

            var repoArchive = await _client.Repository.Content.GetArchive(id, ArchiveFormat.Zipball, commitId);
            StoreByteArrayToZip(repoArchive);
            //var compressedFileStream = new MemoryStream(repoArchive);
            //using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Read, false))
            //{

            //}


            Console.WriteLine("hello");
            Console.WriteLine(repoContent.Count);
        }


        private void StoreByteArrayToZip(byte[] content)
        {
            using (var compressedFileStream = new MemoryStream())
            //Create an archive and store the stream in memory.
            using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Update, false))
            {
                //Create a zip entry for each attachment
                var zipEntry = zipArchive.CreateEntry("repository.zip");
                using (var originalFileStream = new MemoryStream(content))
                using (var zipEntryStream = zipEntry.Open())
                {
                    //Copy the attachment stream to the zip entry stream
                    originalFileStream.CopyTo(zipEntryStream);
                }

                zipArchive.ExtractToDirectory(@"C:/");
                //return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = "Filename.zip" };
            }
        }
    }


    public static class Zip
    {
        public static byte[] Decompress(byte[] zippedData)
        {
            byte[] decompressedData = null;
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (MemoryStream inputStream = new MemoryStream(zippedData))
                {
                    using (GZipStream zip = new GZipStream(inputStream, CompressionMode.Decompress))
                    {
                        zip.CopyTo(outputStream);
                    }
                }
                decompressedData = outputStream.ToArray();
            }

            return decompressedData;
        }

        public static byte[] Compress(byte[] plainData)
        {
            byte[] compressesData = null;
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (GZipStream zip = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    zip.Write(plainData, 0, plainData.Length);
                }
                //Dont get the MemoryStream data before the GZipStream is closed 
                //since it doesn’t yet contain complete compressed data.
                //GZipStream writes additional data including footer information when its been disposed
                compressesData = outputStream.ToArray();
            }

            return compressesData;
        }

        public static void TestMethod1()
        {
            string text = "Hello World";

            byte[] data = System.Text.Encoding.UTF8.GetBytes(text);

            byte[] compressedData = Zip.Compress(data);

            byte[] decompressedData = Zip.Decompress(compressedData);



            string textAfterComDecompress = System.Text.Encoding.UTF8.GetString(decompressedData);
        }

    }


}

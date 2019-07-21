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
        private DirectoryInfo _defaultBaseDirectory;
        private GitHubClient _client;

        public OctokitGitClient(DirectoryInfo defaultBaseDirectory)
        {
            _defaultBaseDirectory = defaultBaseDirectory;
            _client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
        }

        public async Task<IEnumerable<PullRequestFile>> GetPullRequestFiles(string owner, string name, int pullRequestNumber)
        {

            var prFiles = await _client.PullRequest.Files(owner, name, pullRequestNumber);
            return prFiles.Select(file => new PullRequestFile(file.FileName, file.Patch));
        }

        public async Task DownloadGithubRepo(string owner, string name, string path, string commitId)
        {
            var id = (await _client.Repository.Get(owner, name)).Id;
            IReadOnlyList<RepositoryContent> repoContent = await _client.Repository.Content.GetAllContentsByRef(id, path, commitId);

            var repoArchive = await _client.Repository.Content.GetArchive(id, ArchiveFormat.Zipball, commitId);

            var repoDirectory = Path.Combine(_defaultBaseDirectory.FullName, $"{owner}-{name}-{commitId}");
            
            StoreByteArrayToZip(repoArchive, Directory.CreateDirectory(repoDirectory));
            Console.WriteLine("hello");
            Console.WriteLine(repoContent.Count);
        }

        private void StoreByteArrayToZip(byte[] content, DirectoryInfo downloadDirectory)
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

                zipArchive.ExtractToDirectory(downloadDirectory.FullName);
                //return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = "Filename.zip" };
            }
        }
    }
}

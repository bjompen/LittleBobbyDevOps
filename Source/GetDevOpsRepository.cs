using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Management.Automation;

namespace PipeHow.LittleBobbyDevOps
{
    /// <summary>
    /// Get an Azure DevOps repository.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "DevOpsRepository")]
    public class GetDevOpsRepository : PSCmdlet
    {
        /// <summary>
        /// <para type="description">The collection uri to the DevOps organisation.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty()]
        public string CollectionUri { get; set; }
        
        /// <summary>
        /// <para type="description">The name of the project.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty()]
        public string ProjectName { get; set; }
        
        /// <summary>
        /// <para type="description">The name of the repository.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty()]
        public string RepositoryName { get; set; }

        /// <summary>
        /// <para type="description">The Personal Access Token used for authentication.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty()]
        public string PAT { get; set; }

        protected override void ProcessRecord()
        {
            var creds = new VssBasicCredential(string.Empty, PAT);

            // Connect to Azure DevOps Services
            var connection = new VssConnection(new Uri(CollectionUri), creds);

            // Get a GitHttpClient to talk to the Git endpoints
            using (var gitClient = connection.GetClient<GitHttpClient>())
            {
                // Get data about a specific repository
                var repo = gitClient.GetRepositoryAsync(ProjectName, RepositoryName).Result;

                WriteObject(repo);
            }
        }
    }

    [Cmdlet(VerbsCommon.Get, "DevOpsRepositoryBranches")]
    public class GetDevOpsRepositoryBranches : PSCmdlet
    {
        /// <summary>
        /// <para type="description">The collection uri to the DevOps organisation.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty()]
        public string CollectionUri { get; set; }

        /// <summary>
        /// <para type="description">The Personal Access Token used for authentication.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty()]
        public string PAT { get; set; }

        /// <summary>
        /// <para type="description">The ID of the repo to get branches from.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty()]
        public string repositoryId { get; set; }

        protected override void ProcessRecord()
        {
            var creds = new VssBasicCredential(string.Empty, PAT);

            // Connect to Azure DevOps Services
            var connection = new VssConnection(new Uri(CollectionUri), creds);

            // Get a GitHttpClient to talk to the Git endpoints
            using (var gitClient = connection.GetClient<GitHttpClient>())
            {
                // Get data about a specific repository
                var branches = gitClient.GetBranchesAsync(repositoryId).Result;

                WriteObject(branches);
            }
        }
    }
}

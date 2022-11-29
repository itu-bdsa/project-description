namespace GitInsight.Entities;
using Newtonsoft.Json;
// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);


[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class License
    {
        [JsonProperty("key")]
        public string ?key { get; set; }

        [JsonProperty("name")]
        public string ?name { get; set; }

        [JsonProperty("spdx_id")]
        public string ?spdx_id { get; set; }

        [JsonProperty("url")]
        public string ?url { get; set; }

        [JsonProperty("node_id")]
        public string ?node_id { get; set; }
    }

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Owner
    {
        [JsonProperty("login")]
        public string ?login { get; set; }

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("node_id")]
        public string ?node_id { get; set; }

        [JsonProperty("avatar_url")]
        public string ?avatar_url { get; set; }

        [JsonProperty("gravatar_id")]
        public string ?gravatar_id { get; set; }

        [JsonProperty("url")]
        public string ?url { get; set; }

        [JsonProperty("html_url")]
        public string ?html_url { get; set; }

        [JsonProperty("followers_url")]
        public string ?followers_url { get; set; }

        [JsonProperty("following_url")]
        public string ?following_url { get; set; }

        [JsonProperty("gists_url")]
        public string ?gists_url { get; set; }

        [JsonProperty("starred_url")]
        public string ?starred_url { get; set; }

        [JsonProperty("subscriptions_url")]
        public string ?subscriptions_url { get; set; }

        [JsonProperty("organizations_url")]
        public string ?organizations_url { get; set; }

        [JsonProperty("repos_url")]
        public string ?repos_url { get; set; }

        [JsonProperty("events_url")]
        public string ?events_url { get; set; }

        [JsonProperty("received_events_url")]
        public string ?received_events_url { get; set; }

        [JsonProperty("type")]
        public string ?type { get; set; }

        [JsonProperty("site_admin")]
        public bool site_admin { get; set; }
    }

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Permissions
    {
        [JsonProperty("admin")]
        public bool admin { get; set; }

        [JsonProperty("maintain")]
        public bool maintain { get; set; }

        [JsonProperty("push")]
        public bool push { get; set; }

        [JsonProperty("triage")]
        public bool triage { get; set; }

        [JsonProperty("pull")]
        public bool pull { get; set; }
    }

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Fork
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("node_id")]
        public string ?node_id { get; set; }

        [JsonProperty("name")]
        public string ?name { get; set; }

        [JsonProperty("full_name")]
        public string ?full_name { get; set; }

        [JsonProperty("private")]
        public bool @private { get; set; }

        [JsonProperty("owner")]
        public Owner ?owner { get; set; }

        [JsonProperty("html_url")]
        public string ?html_url { get; set; }

        [JsonProperty("description")]
        public string ?description { get; set; }

        [JsonProperty("fork")]
        public bool fork { get; set; }

        [JsonProperty("url")]
        public string ?url { get; set; }

        [JsonProperty("forks_url")]
        public string ?forks_url { get; set; }

        [JsonProperty("keys_url")]
        public string ?keys_url { get; set; }

        [JsonProperty("collaborators_url")]
        public string ?collaborators_url { get; set; }

        [JsonProperty("teams_url")]
        public string ?teams_url { get; set; }

        [JsonProperty("hooks_url")]
        public string ?hooks_url { get; set; }

        [JsonProperty("issue_events_url")]
        public string ?issue_events_url { get; set; }

        [JsonProperty("events_url")]
        public string ?events_url { get; set; }

        [JsonProperty("assignees_url")]
        public string ?assignees_url { get; set; }

        [JsonProperty("branches_url")]
        public string ?branches_url { get; set; }

        [JsonProperty("tags_url")]
        public string ?tags_url { get; set; }

        [JsonProperty("blobs_url")]
        public string ?blobs_url { get; set; }

        [JsonProperty("git_tags_url")]
        public string ?git_tags_url { get; set; }

        [JsonProperty("git_refs_url")]
        public string ?git_refs_url { get; set; }

        [JsonProperty("trees_url")]
        public string ?trees_url { get; set; }

        [JsonProperty("statuses_url")]
        public string ?statuses_url { get; set; }

        [JsonProperty("languages_url")]
        public string ?languages_url { get; set; }

        [JsonProperty("stargazers_url")]
        public string ?stargazers_url { get; set; }

        [JsonProperty("contributors_url")]
        public string ?contributors_url { get; set; }

        [JsonProperty("subscribers_url")]
        public string ?subscribers_url { get; set; }

        [JsonProperty("subscription_url")]
        public string ?subscription_url { get; set; }

        [JsonProperty("commits_url")]
        public string ?commits_url { get; set; }

        [JsonProperty("git_commits_url")]
        public string ?git_commits_url { get; set; }

        [JsonProperty("comments_url")]
        public string ?comments_url { get; set; }

        [JsonProperty("issue_comment_url")]
        public string ?issue_comment_url { get; set; }

        [JsonProperty("contents_url")]
        public string ?contents_url { get; set; }

        [JsonProperty("compare_url")]
        public string ?compare_url { get; set; }

        [JsonProperty("merges_url")]
        public string ?merges_url { get; set; }

        [JsonProperty("archive_url")]
        public string ?archive_url { get; set; }

        [JsonProperty("downloads_url")]
        public string ?downloads_url { get; set; }

        [JsonProperty("issues_url")]
        public string ?issues_url { get; set; }

        [JsonProperty("pulls_url")]
        public string ?pulls_url { get; set; }

        [JsonProperty("milestones_url")]
        public string ?milestones_url { get; set; }

        [JsonProperty("notifications_url")]
        public string ?notifications_url { get; set; }

        [JsonProperty("labels_url")]
        public string ?labels_url { get; set; }

        [JsonProperty("releases_url")]
        public string ?releases_url { get; set; }

        [JsonProperty("deployments_url")]
        public string ?deployments_url { get; set; }

        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }

        [JsonProperty("updated_at")]
        public DateTime updated_at { get; set; }

        [JsonProperty("pushed_at")]
        public DateTime pushed_at { get; set; }

        [JsonProperty("git_url")]
        public string ?git_url { get; set; }

        [JsonProperty("ssh_url")]
        public string ?ssh_url { get; set; }

        [JsonProperty("clone_url")]
        public string ?clone_url { get; set; }

        [JsonProperty("svn_url")]
        public string ?svn_url { get; set; }

        [JsonProperty("homepage")]
        public object ?homepage { get; set; }

        [JsonProperty("size")]
        public int size { get; set; }

        [JsonProperty("stargazers_count")]
        public int stargazers_count { get; set; }

        [JsonProperty("watchers_count")]
        public int watchers_count { get; set; }

        [JsonProperty("language")]
        public object ?language { get; set; }

        [JsonProperty("has_issues")]
        public bool has_issues { get; set; }

        [JsonProperty("has_projects")]
        public bool has_projects { get; set; }

        [JsonProperty("has_downloads")]
        public bool has_downloads { get; set; }

        [JsonProperty("has_wiki")]
        public bool has_wiki { get; set; }

        [JsonProperty("has_pages")]
        public bool has_pages { get; set; }

        [JsonProperty("has_discussions")]
        public bool has_discussions { get; set; }

        [JsonProperty("forks_count")]
        public int forks_count { get; set; }

        [JsonProperty("mirror_url")]
        public object ?mirror_url { get; set; }

        [JsonProperty("archived")]
        public bool archived { get; set; }

        [JsonProperty("disabled")]
        public bool disabled { get; set; }

        [JsonProperty("open_issues_count")]
        public int open_issues_count { get; set; }

        [JsonProperty("license")]
        public License ?license { get; set; }

        [JsonProperty("allow_forking")]
        public bool allow_forking { get; set; }

        [JsonProperty("is_template")]
        public bool is_template { get; set; }

        [JsonProperty("web_commit_signoff_required")]
        public bool web_commit_signoff_required { get; set; }

        [JsonProperty("topics")]
        public List<object> ?topics { get; set; }

        [JsonProperty("visibility")]
        public string ?visibility { get; set; }

        [JsonProperty("forks")]
        public int forks { get; set; }

        [JsonProperty("open_issues")]
        public int open_issues { get; set; }

        [JsonProperty("watchers")]
        public int watchers { get; set; }

        [JsonProperty("default_branch")]
        public string ?default_branch { get; set; }

        [JsonProperty("permissions")]
        public Permissions ?permissions { get; set; }
    }


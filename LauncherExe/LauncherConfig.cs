using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LauncherExe
{
	[DataContract]
	public class LauncherSettings
	{
		[DataMember]
		public string ManifestUrl { get; set; }

		[DataMember]
		public string InstallFolder { get; set; }

		[DataMember]
		public string GameRelativePath { get; set; }

		[DataMember]
		public string AccountManagerRelativePath { get; set; }
	}

	[DataContract]
	public class LauncherManifest
	{
		[DataMember]
		public string Version { get; set; }

		[DataMember]
		public List<ManifestFile> Files { get; set; }
	}

	[DataContract]
	public class ManifestFile
	{
		[DataMember(Name = "path")]
		public string RelativePath { get; set; }

		[DataMember(Name = "url")]
		public string Url { get; set; }

		[DataMember(Name = "sha256")]
		public string Sha256 { get; set; }

		[DataMember(Name = "extract")]
		public bool Extract { get; set; }

		[DataMember(Name = "extractTo")]
		public string ExtractTo { get; set; }
	}
}

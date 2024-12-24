//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

public class AzureBlobContentSource
{
	public Uri ContainerUrl { get; set; } = null!;
	public string Prefix { get; set; } = null!;
	public IEnumerable<string> Extensions { get; set; } = null!;
}

public class Consent
{
	public string Id { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string VoiceTalentName { get; set; } = null!;
	public string CompanyName { get; set; } = null!;
	public string Locale { get; set; } = null!;
	public string ProjectId { get; set; } = null!;
	public Uri AudioUrl { get; set; } = null!;
	public DateTime CreatedDateTime { get; set; }
	public DateTime LastActionDateTime { get; set; }
	public Status? Status { get; set; }
}

public enum DatasetKind
{
	AudioAndScript = 1,
	LongAudio = 2,
	AudioOnly = 3
}

public class Dataset
{
	public DatasetKind Kind { get; set; }
	public AzureBlobContentSource Audios { get; set; } = null!;
	public AzureBlobContentSource Scripts { get; set; } = null!;
}

public class Endpoint
{
	public Guid Id { get; set; }
	public string Description { get; set; } = null!;
	public string ModelId { get; set; } = null!;
	public string ProjectId { get; set; } = null!;
	public DateTime CreatedDateTime { get; set; }
	public DateTime LastActionDateTime { get; set; }
	public Status? Status { get; set; }
}

public class Model
{
	public string Id { get; set; } = null!;
	public string VoiceName { get; set; } = null!;
	public string Description { get; set; } = null!;
	public Recipe Recipe { get; set; } = null!;
	public string Locale { get; set; } = null!;
	public string TrainingSetId { get; set; } = null!;
	public string ProjectId { get; set; } = null!;
	public string ConsentId { get; set; } = null!;
	public ModelProperties Properties { get; set; } = null!;
	public string EngineVersion { get; set; } = null!;
	public DateTime CreatedDateTime { get; set; }
	public DateTime LastActionDateTime { get; set; }
	public Status? Status { get; set; }
}

public class ModelProperties
{
	public string FailureReason { get; set; } = null!;
	public IEnumerable<string> PresetStyles { get; set; } = null!;
	public IReadOnlyDictionary<string, string> StyleTrainingSetIds { get; set; } = null!;
}

public class PaginatedResources<T>
{
	public IEnumerable<T> Value { get; set; } = null!;
	public Uri NextLink { get; set; } = null!;
}

public class PersonalVoice
{
	public string Id { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string ConsentId { get; set; } = null!;
	public AzureBlobContentSource Audios { get; set; } = null!;
	public string ProjectId { get; set; } = null!;
	public IReadOnlyDictionary<string, string> Properties { get; set; } = null!;
	public DateTime CreatedDateTime { get; set; }
	public DateTime LastActionDateTime { get; set; }
	public Status? Status { get; set; }
	public Guid SpeakerProfileId { get; set; }
}

public enum ProjectKind
{
	ProfessionalVoice,
	PersonalVoice
}

public class Project
{
	public string Id { get; set; } = null!;
	public string Description { get; set; } = null!;
	public ProjectKind Kind { get; set; }

	/// <summary>
	/// The time-stamp when the object was created.
	/// The time stamp is encoded as ISO 8601 date and time format
	/// ("YYYY-MM-DDThh:mm:ssZ", see https://en.wikipedia.org/wiki/ISO_8601#Combined_date_and_time_representations).
	/// </summary>
	public DateTime CreatedDateTime { get; set; }
}

public enum RecipeKind
{
	Default = 1,
	CrossLingual = 2,
	MultiStyle = 3
}

public class Recipe
{
	public string Version { get; set; } = null!;
	public RecipeKind Kind { get; set; }
	public string Description { get; set; } = null!;
}

public enum Status
{
	NotStarted = 1,
	Running = 2,
	Succeeded = 3,
	Failed = 4,
	Disabling = 5,
	Disabled = 6
}

public class TrainingSet
{
	public string Id { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string Locale { get; set; } = null!;
	public string ProjectId { get; set; } = null!;
	public DateTime CreatedDateTime { get; set; }
	public DateTime LastActionDateTime { get; set; }
	public Status? Status { get; set; }
}

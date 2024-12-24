using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

public class PersonalVoiceSample
{
	private const string key = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
	private const string region = "YYYYYY";
	private const string projId = "pv-proj1";
	private const string consId = "pv-cons1";
	private const string pvId = "pvid1";

	public static async Task CreateAsync()
	{
		var client = new CustomVoiceClient(region, key);
		try
		{
			var project = await client.CreateProjectAsync(projId, ProjectKind.PersonalVoice, "CreateProjectAsync");
			var consent = await client.UploadConsentAsync(consId, projId, "千葉慎二", "日本マイクロソフト株式会社", "ja-JP", "seimei.mp3");
			var personalVoice = await client.CreatePersonalVoiceAsync(pvId, projId, "CreatePersonalVoiceAsync", consId, "pv").ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}
	}

	public static async Task SynthesisAsync()
	{
		var client = new CustomVoiceClient(region, key);
		try
		{
			var personalVoice = await client.GetPersonalVoiceAsync(pvId).ConfigureAwait(false);
			await SynthesisAsync("en-US", "Hi, my name is Shinji Chiba. This is my personl voice. I only spoke in Japanese, but you will hear me speaking in English. Thank you.", null, personalVoice.SpeakerProfileId);
//			await SynthesisAsync("zh-CN", "嗨，我叫千叶真司．这是我个人的声音．我只用日语说话，但你会听到我用英语说话．谢谢．", "out.wav", personalVoice.SpeakerProfileId);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}
	}

	public static async Task DestroyAsync()
	{
		var client = new CustomVoiceClient(region, key);
		try
		{
			await client.DeleteConsentAsync(consId).ConfigureAwait(false);
			await client.DeletePersonalVoiceAsync(pvId).ConfigureAwait(false);
			await client.DeleteProjectAsync(projId).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}
	}

	public static async Task SynthesisAsync(string lang, string text, string? outFile, Guid speakerProfileId)
	{
		Console.WriteLine($"[{lang}] {text}");

		var ssml = $"<speak version='1.0' xml:lang='ja-JP' xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts'><voice name='DragonLatestNeural'>" +
			$"<mstts:ttsembedding speakerProfileId='{speakerProfileId}'/><mstts:express-as style='Prompt'>" +
			$"<lang xml:lang='{lang}'> {text} </lang></mstts:express-as></voice></speak>";

		var speechConfig = SpeechConfig.FromSubscription(key, region);
		AudioConfig audioConfig;
		if (outFile != null)
			audioConfig = AudioConfig.FromWavFileOutput(outFile);
		else
			audioConfig = AudioConfig.FromDefaultSpeakerOutput();
		using var synthesizer = new SpeechSynthesizer(speechConfig, audioConfig);
		using var result = await synthesizer.SpeakSsmlAsync(ssml).ConfigureAwait(false);
		if (result.Reason == ResultReason.SynthesizingAudioCompleted)
			Console.WriteLine("Succeeded.");
		else if (result.Reason == ResultReason.Canceled)
		{
			var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
			Console.WriteLine($"Canceled: Reason={cancellation.Reason}");
			if (cancellation.Reason == CancellationReason.Error)
				Console.WriteLine($"Result: {result.ResultId} ({cancellation.ErrorDetails})");
		}
	}
}

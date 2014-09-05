using System;
using Android.App;
using Android.Content;
using Android.Speech;
using Android.Widget;
using Android.OS;

namespace SpeechToText
{
    [Activity(Label = "SpeechToText", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const int VOICE = 10;



        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += RecordVoice;
        }

        private void RecordVoice(object s, EventArgs e)
        {
            var result = FindViewById<TextView>(Resource.Id.ResultText);
            result.Text = string.Empty;
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Speak Now :)");
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
            StartActivityForResult(voiceIntent, VOICE);
        }

        protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
        {
            if (requestCode == VOICE)
            {
                if (resultVal == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    var result = FindViewById<TextView>(Resource.Id.ResultText);
                    result.Text = matches[0].ToString();
                }
            }
            base.OnActivityResult(requestCode, resultVal, data);
        }
    }
}


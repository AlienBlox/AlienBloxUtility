using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;

namespace AlienBloxUtility.Utilities.Commands
{
    public class DownloadCurrentMusic : CommandBase
    {
        public override string CommandName => "downcurrentmusic";

        public override bool DocumentationEnabled => false;

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            MusicHelper.DownloadCurrentMusic(AlienBloxUtility.CacheLocation);
        }
    }
}